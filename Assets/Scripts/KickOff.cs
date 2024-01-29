using UnityEngine;

public class KickOff : MonoBehaviour
{
    public float rotationSpeed = 5f; // Adjust the speed of rotation

    void Update()
    {
        // Check if the player's rotation is not close to the target rotation (0 degrees)
        if (Mathf.Abs(transform.eulerAngles.y) > 0.1f)
        {
            // Calculate the target rotation
            Quaternion targetRotation = Quaternion.Euler(0f, 0f, 0f);

            // Use Quaternion.Lerp to smoothly interpolate between the current rotation and the target rotation
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }
}
