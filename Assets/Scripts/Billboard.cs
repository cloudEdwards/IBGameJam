using UnityEngine;

public class Billboard : MonoBehaviour
{
    void LateUpdate()
    {
        // Get camera rotation but zero out Y so sprite stays upright
        Vector3 camEuler = Camera.main.transform.rotation.eulerAngles;
        transform.rotation = Quaternion.Euler(camEuler.x, transform.rotation.eulerAngles.y, 0f);
    }
}