using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    public Transform player; // Reference to the player's transform
    public LayerMask obstacleMask; // Set the layers of obstacles you want to consider

    public float maxDistance = 2f; // Maximum distance from the player
    public float smoothSpeed = 5f; // Smoothing factor for camera movement

    private Vector3 offset; // Initial offset between camera and player

    void Start()
    {
        if (player == null)
        {
            Debug.LogError("Player transform not assigned to the camera!");
            return;
        }

    }

    void LateUpdate()
    {
        if (player == null)
            return;

        // Calculate the desired camera position based on player's position and offset
        Vector3 targetPosition = player.position + offset;

        // Use raycasting to check for obstacles between the camera and the player
        RaycastHit hit;
        if (Physics.Raycast(player.position, transform.position - player.position, out hit, maxDistance, obstacleMask))
        {
            // If an obstacle is detected, adjust the target position to avoid the obstacle
            targetPosition = hit.point;
            
        }
    }
}
