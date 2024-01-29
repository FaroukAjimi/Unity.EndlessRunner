using UnityEngine;
using UnityEngine.UI;

public class PlayerTriggers : MonoBehaviour
{
    public Animator anim;
    private bool isPlayerStopped = false;
    private CharacterController characterController;
    private PlayerController pc;
    public int coins = 0;
    public bool groundcheck = false;
    public GameObject fail;

    // Reference to the UI Text component displaying the coin count
    public Text coinCountText;
    public Text ScoreCount;
    public int score = 0;
    public int scoreMultiplier = 1;
    public Text ScoreMultiplierText;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        pc = GetComponent<PlayerController>();
        UpdateCoinCountText();
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the entering collider has a specific tag
        if (other.CompareTag("Coin"))
        {
            coins += 1;
            Destroy(other.gameObject);
            UpdateCoinCountText(); // Update the UI text when the coin count changes
        }
    }

    private void UpdateCoinCountText()
    {
        // Ensure that the Text component is assigned
        if (coinCountText != null)
        {
            // Update the coin count text with the current value
            coinCountText.text = coins.ToString("D4");
        }
        else
        {
            Debug.LogError("coinCountText is not assigned!");
        }
    }

    private void Update()
    {
       
        // Check if the player is currently stopped
        isPlayerStopped = Mathf.Approximately(characterController.velocity.z, 0f);
        if (!isPlayerStopped)
        {
            score += 1 * scoreMultiplier;
            ScoreCount.text = score.ToString("D7");
        }
        if (groundcheck)
        {
            if (pc.Controller.isGrounded)
            {
                pc.enabled = false;
            }
        }
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.CompareTag("Fail"))
        {
            // Check if the player was stopped
            if (isPlayerStopped)
            {
                fail.SetActive(true);
                // Player was stopped, log failure
                Debug.Log("Fail - Game Over!");
                anim.applyRootMotion = !anim.applyRootMotion;
                anim.SetBool("Fail", true);
                if (isPlayerStopped)
                {
                    if (pc.Controller.isGrounded)
                    {
                        pc.enabled = false;
                    }
                    else
                    {
                        groundcheck = true;
                    }
                }
            }
            else
            {
                // Player was not stopped, continue with the game
                Debug.Log("Continue...");
            }
        }
    }
}
