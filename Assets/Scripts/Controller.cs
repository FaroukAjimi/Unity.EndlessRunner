using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    public float initialForwardSpeed = 5f;
    public float jumpForce = 8f;
    private float DirectionY;
    private float DirectionX;
    public float gravity = 10.0f;
    public CharacterController Controller;
    public Animator JakeAnim;

    private enum Lane
    {
        Center,
        Right,
        Left
    }

    private Lane currentLane = Lane.Center;

    public Transform centerLane;
    public Transform rightLane;
    public Transform leftLane;

    private float timeElapsed;
    public float speedIncreaseInterval = 10f;  // Increase speed every X seconds
    public float speedIncreaseAmount = 1f;     // Amount to increase speed by

    // Adjustable crouch parameters
    public float crouchDuration = 1.5f;
    private bool isCrouching = false;
    private float originalControllerHeight;
    private float originalColliderHeight;

    void Start()
    {
        Controller = GetComponent<CharacterController>();


        // Store original heights for resetting
        originalControllerHeight = Controller.height;
        originalColliderHeight = GetComponent<CapsuleCollider>().height;

        timeElapsed = 0f;
    }

    void Update()
    {
        // Move the player forward
        Vector3 Direction = Vector3.forward;

        // Increase forward speed over time
        timeElapsed += Time.deltaTime;
        if (timeElapsed > speedIncreaseInterval)
        {
            initialForwardSpeed += speedIncreaseAmount;
            timeElapsed = 0f;
        }

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
        else
        {
            if (Controller.isGrounded)
            {
                JakeAnim.SetBool("SJump", false);
            }
        }

        // Crouch input handling
        if (Input.GetKeyDown(KeyCode.DownArrow) && !isCrouching)
        {
            StartCoroutine(Crouch());
        }

        if (Controller.isGrounded)
        {
            if (Input.GetButtonDown("Jump"))
            {
                JakeAnim.SetBool("Jump", true);
                DirectionY = jumpForce;
            }
            else
            {
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

        Controller.Move(new Vector3(DirectionX * initialForwardSpeed, DirectionY  * 5, Direction.z * initialForwardSpeed) * Time.deltaTime);
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

    // Coroutine for crouching
    // Coroutine for crouching
// Coroutine for crouching
private IEnumerator Crouch()
{
    isCrouching = true;

    // Store the original positions for resetting
    float originalControllerBottom = Controller.bounds.min.y;
    float originalColliderBottom = GetComponent<CapsuleCollider>().bounds.min.y;

    // Store the original heights for resetting
    float originalControllerHeight = Controller.height;
    float originalColliderHeight = GetComponent<CapsuleCollider>().height;

    // Reduce CharacterController and CapsuleCollider heights
    Controller.height /= 2f;
    GetComponent<CapsuleCollider>().height /= 2f;

    // Manually adjust position to simulate crouching effect
    transform.position -= new Vector3(0f, originalControllerBottom - Controller.bounds.min.y, 0f);
    JakeAnim.SetBool("Roll", true);

    yield return new WaitForSeconds(crouchDuration);

    // Restore original heights
    Controller.height = originalControllerHeight;
    GetComponent<CapsuleCollider>().height = originalColliderHeight;

    isCrouching = false;
    JakeAnim.SetBool("Roll", false);
}


}
