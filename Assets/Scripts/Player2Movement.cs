using UnityEngine;

public class Player2Movement : MonoBehaviour
{

    public float speed;
    private Rigidbody2D rb2d;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public GameObject bombPrefab;

    public float rateOfBomb = 2;

    private float lastTimeFired = 0;

    private float inputX = 0;
    private float inputY = 0;

    private SpriteRenderer playerSpriteRenderer;

    public Sprite spriteUp;
    public Sprite spriteDown;
    public Sprite spriteRight;

    public Sprite[] framesRight;
    public Sprite[] framesUp;
    public Sprite[] framesDown;

    float frameTimer;
    float framesPerSecond = 10;

    int currentFrameIndex = 0;
    
    private string lastDirection = "down";
    
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
        playerSpriteRenderer = GetComponent<SpriteRenderer>();


        frameTimer = (1f / framesPerSecond);
        currentFrameIndex = 0;
    }

    // Update is called once per frame
    void Update()
    {

        if (lastDirection == "down") {
            playerSpriteRenderer.sprite = spriteDown;
        }
        else if (lastDirection == "up") {
            playerSpriteRenderer.sprite = spriteUp;
        }
        else if (lastDirection == "right") {
            playerSpriteRenderer.sprite = spriteRight;
        }
        else if (lastDirection == "left") {
            playerSpriteRenderer.sprite = spriteRight;
            playerSpriteRenderer.flipX = true;
        }

        // Vector2 input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        inputY = 0;
        inputX = 0;
        playerSpriteRenderer.flipX = false;
        
        if (Input.GetKey(KeyCode.W)) {
            // playerSpriteRenderer.sprite = spriteUp;

            if (frameTimer <= 0) {
                currentFrameIndex++;
                // if (currentFrameIndex >= frames.Length) {
                //     Destroy(gameObject);
                //     return;
                // }
                frameTimer = (1f / framesPerSecond);
                playerSpriteRenderer.sprite = framesUp[currentFrameIndex];
            }

            inputY = 1;
            lastDirection = "up";
        }
        if (Input.GetKey(KeyCode.A)) {
            if (frameTimer <= 0) {
                currentFrameIndex++;
                // if (currentFrameIndex >= frames.Length) {
                //     Destroy(gameObject);
                //     return;
                // }
                frameTimer = (1f / framesPerSecond);
                playerSpriteRenderer.sprite = framesRight[currentFrameIndex];
            }
            // playerSpriteRenderer.sprite = spriteRight;
            playerSpriteRenderer.flipX = true;
            inputX = -1;
            lastDirection = "left";
        }
        if (Input.GetKey(KeyCode.S)) {
            // playerSpriteRenderer.sprite = spriteDown;
            inputY = -1;
            if (frameTimer <= 0) {
                currentFrameIndex++;
                // if (currentFrameIndex >= frames.Length) {
                //     Destroy(gameObject);
                //     return;
                // }
                frameTimer = (1f / framesPerSecond);
                playerSpriteRenderer.sprite = framesDown[currentFrameIndex];
            }
            lastDirection = "down";

        }
        if (Input.GetKey(KeyCode.D)) {
            if (frameTimer <= 0) {
                currentFrameIndex++;
                // if (currentFrameIndex >= frames.Length) {
                //     Destroy(gameObject);
                //     return;
                // }
                frameTimer = (1f / framesPerSecond);
                playerSpriteRenderer.sprite = framesRight[currentFrameIndex];
            }
            // playerSpriteRenderer.sprite = spriteRight;
            inputX = 1;
            lastDirection = "right";
        }


        // if (input.magnitude > 1.0f) {
        //     input.Normalize();
        // }
        rb2d.linearVelocity = new Vector2(inputX, inputY) * speed;

        if (Input.GetKeyDown(KeyCode.LeftShift) && (lastTimeFired + 1 / rateOfBomb) < Time.time) {
            lastTimeFired = Time.time;
            SpawnBomb();
        }

    }
}
