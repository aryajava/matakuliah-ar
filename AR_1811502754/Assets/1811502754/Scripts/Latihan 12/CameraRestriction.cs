using UnityEngine;
using UnityEditor;
public class CameraRestriction : MonoBehaviour
{
    public float restrictionAngle = -45f;

    void Update()
    {
        var rotation = TransformUtils.GetInspectorRotation(gameObject.transform);
        if (rotation.x < restrictionAngle)
        {
            TransformUtils.SetInspectorRotation(gameObject.transform, new Vector3(restrictionAngle,rotation.y, rotation.z));
        }
    }
}
