using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float forwardSpeed = 5f;
    public float jumpForce = 8f;
    private float DirectionY;
    private float DirectionX;
    public float gravity = 10.0f;
    private CharacterController Controller;
    public Animator JakeAnim;

    // Enum to represent the lanes
    private enum Lane
    {
        Center,
        Right,
        Left
    }

    private Lane currentLane = Lane.Center;

    // Public variables for lane positions
    public Transform centerLane;
    public Transform rightLane;
    public Transform leftLane;

    void Start()
    {
        Controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        // Move the player forward
        Vector3 Direction = Vector3.forward;

        // Detect lane change input
        if (Input.GetKeyDown(KeyCode.RightArrow) && currentLane == Lane.Center)
        {
             JakeAnim.SetBool("SJump", true);
            SwitchLane(Lane.Right);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow) && currentLane == Lane.Left)
        {
             JakeAnim.SetBool("SJump", true);
            SwitchLane(Lane.Center);
        }
        // Check for center lane input
        else if (Input.GetKeyDown(KeyCode.LeftArrow) && currentLane == Lane.Center)
        {
             JakeAnim.SetBool("SJump", true);
            SwitchLane(Lane.Left);
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow) && currentLane == Lane.Right)
        {
             JakeAnim.SetBool("SJump", true);
            SwitchLane(Lane.Center);
        }
        else{
             if (Controller.isGrounded)
             {
                JakeAnim.SetBool("SJump", false);
             }
        }

        if (Controller.isGrounded)
        {
            if (Input.GetButtonDown("Jump"))
            {
                JakeAnim.SetBool("Jump", true);
                DirectionY = jumpForce;
            }
            else{
                JakeAnim.SetBool("Jump", false);
            }
        }

        DirectionY -= gravity * Time.deltaTime;
        Direction.y = DirectionY;

        // Adjust the magnetizing behavior based on the current lane
        if (currentLane == Lane.Right)
        {
            // Magnetize towards the right lane
            DirectionX = rightLane.position.x - transform.position.x;
        }
        else if (currentLane == Lane.Left)
        {
            // Magnetize towards the left lane
            DirectionX = leftLane.position.x - transform.position.x;
        }
        else
        {
            // Magnetize towards the center lane
            DirectionX = centerLane.position.x - transform.position.x;
        }

        Controller.Move(new Vector3(DirectionX, DirectionY, Direction.z) * forwardSpeed * Time.deltaTime);
    }

    // Function to switch lanes
    private void SwitchLane(Lane newLane)
    {
       
        // Set the new lane
        currentLane = newLane;

        // Reset player's position to the new lane's position
        Vector3 newPosition = transform.position;

        switch (currentLane)
        {
            case Lane.Right:
                newPosition.x = rightLane.position.x;
                break;
            case Lane.Left:
                newPosition.x = leftLane.position.x;
                break;
            case Lane.Center:
                newPosition.x = centerLane.position.x;
                break;
        }

        transform.position = newPosition;
    }
}
