using UnityEngine;

public class Camera3PersonMove : MonoBehaviour
{
    // Karakter dan Player
    public GameObject character;
    public GameObject player;

    // Kamera
    public GameObject cameraPos;
    public Camera cam;
    public float yOffset = 1f;
    public float sensitivity = 2f;

    // raycasthit untuk kamera
    private RaycastHit _camHit;

    public Vector3 camDist;
    public float scrollSensitivity = 5f;
    public float scrollDampening = 8f;
    public float zoomMin = 3.5f;
    public float zoomMax = 15f;
    public float zoomDefault = 5f;
    public float zoomDistance;
    public float collisonSensitivity = 4.5f;

    void Start()
    {
        // inisial posisi kamera
        camDist = cam.transform.localPosition;
        // set default zoom
        zoomDistance = zoomDefault;
        // apply default zoom
        camDist.z = zoomDistance;
        // remove cursor
        Cursor.visible =false;
    }

    void Update()
    {
        // kamera pos
        var position1 = character.transform.position;
        cameraPos.transform.position = new Vector3(position1.x, position1.y + yOffset, + position1.z);
        // kamera rotation
        var rotation = cameraPos.transform.rotation;
        rotation = Quaternion.Euler(rotation.eulerAngles.x - Input.GetAxis("Mouse Y") * sensitivity / 2, rotation.eulerAngles.y + Input.GetAxis("Mouse X") * sensitivity, rotation.eulerAngles.z);
        cameraPos.transform.rotation = rotation;
        // player rotation
        player.transform.rotation = rotation;
        // scroll zoom
        if (Input.GetAxis("Mouse ScrollWheel") != 0f)
        {
            var scrollAmount = Input.GetAxis("Mouse ScrollWheel") * scrollSensitivity;
            scrollAmount *= (zoomDistance * 0.3f);
            zoomDistance += scrollAmount * -1f;
            zoomDistance = Mathf.Clamp(zoomDistance,zoomMin, zoomMax);
        }
        if (camDist.z != zoomDistance * -1f) camDist.z = Mathf.Lerp(camDist.z, -zoomDistance, Time.deltaTime * scrollDampening);

        var transform2 = cam.transform;
        transform2.localPosition = camDist;
        // cek untuk collosions
        GameObject obj = new GameObject();
        obj.transform.SetParent(transform2.parent);
        var position = cam.transform.localPosition;
        obj.transform.localPosition = new Vector3(position.x, position.y, position.z);

        if(Physics.Linecast(cameraPos.transform.position, obj.transform.position, out _camHit))
        {
            var transform1 = cam.transform;
            transform1.position = _camHit.point;
            var localPosition = transform1.localPosition;
            localPosition = new Vector3(localPosition.x, localPosition.y, localPosition.z + collisonSensitivity);
            transform1.localPosition = localPosition;
        }

        Destroy(obj);

        if(cam.transform.localPosition.z > -1f)
        cam.transform.localPosition = new Vector3(cam.transform.localPosition.x, cam.transform.localPosition.y, -1f);
    }
}
