using UnityEngine;

public class CameraForward : MonoBehaviour
{
    public float forwardSpeed = 5f;

    void Update()
    {
        // Move the camera forward
        transform.Translate(Vector3.forward * forwardSpeed * Time.deltaTime);
    }
}
