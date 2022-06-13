using UnityEngine;

public class Pencahayaan : MonoBehaviour
{
    private Light lampu;
    public bool saklar;

    // Start is called before the first frame update
    void Start()
    {
        lampu = GetComponent<Light>();  //variabel lampu
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown (KeyCode.Space)) {     //jika tombol space di tekan   
        lampu.enabled=!lampu.enabled;       // bisa nyala jika tidak mati
        }
    }
}
