using UnityEngine;

public class GerakOtomatis : MonoBehaviour
{
    void Update()
    {
        transform.Translate (1f*Time.deltaTime, 0f, 0f);
    }
}