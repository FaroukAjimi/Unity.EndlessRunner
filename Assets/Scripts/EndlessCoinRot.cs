using UnityEngine;

public class EndlessCoinRot : MonoBehaviour
{
    // Set this variable to the speed of rotation
    public float rotationSpeed = 150f;

    // Start is called before the first frame update
    void Start()
    {
        // Randomly set the initial rotation on the Y-axis
        transform.rotation = Quaternion.Euler(180f, Random.Range(0f, 360f), transform.rotation.z);
    }

    // Update is called once per frame
    void Update()
    {
        // Rotate the coin endlessly on the Y-axis
        transform.Rotate(0f, rotationSpeed * Time.deltaTime, 0f);
    }
}
