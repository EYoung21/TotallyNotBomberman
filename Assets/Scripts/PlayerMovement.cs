using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public float speed;
    private Rigidbody2D rb2d;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public GameObject bombPrefab;

    public float rateOfBomb = 2;

    private float lastTimeFired = 0;
    
    private void SpawnBomb() {
        // Instantiate(projectilePrefab, transform.position + UnityEngine.Vector3.up, UnityEngine.Quaternion.identity);
        Instantiate(bombPrefab, transform.position, UnityEngine.Quaternion.identity);
        
        // float counter = 0;

        // float waitTime = 4;
        // while (counter < waitTime)
        // {
        //     //Increment Timer until counter >= waitTime
        //     counter += Time.deltaTime;
        //     // Debug.Log("We have waited for: " + counter + " seconds");
        //     // //Wait for a frame so that Unity doesn't freeze
        //     // //Check if we want to quit this function
        //     // if (quit)
        //     // {
        //     //     //Quit function
        //     //     yield break;
        //     // }
        //     // yield return null;
        // }
        // Destroy(bombPrefab);

    }

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        if (input.magnitude > 1.0f) {
            input.Normalize();
        }
        rb2d.linearVelocity = input * speed;

        if (Input.GetKeyDown(KeyCode.RightShift) && (lastTimeFired + 1 / rateOfBomb) < Time.time) {
            lastTimeFired = Time.time;
            SpawnBomb();
        }

    }
}
