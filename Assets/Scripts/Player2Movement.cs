using UnityEngine;
using System;

public class Player2Movement : MonoBehaviour
{

    public float speed;
    private Rigidbody2D rb2d;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public GameObject bombPrefab;

    public float rateOfBomb = 0.1f;

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

    float frameTimerLeft, frameTimerRight, frameTimerUp, frameTimerDown;
    float framesPerSecond = 10;

    int frameIndexLeft, frameIndexRight, frameIndexUp, frameIndexDown;
    
    private string lastDirection = "down";

    public LayerMask bombLayerMask;
    
    private void SpawnBomb() {
        // Instantiate(projectilePrefab, transform.position + UnityEngine.Vector3.up, UnityEngine.Quaternion.identity);
        // Instantiate(bombPrefab, transform.position, UnityEngine.Quaternion.identity);

        RaycastHit2D hereHit = Physics2D.Raycast(transform.position, transform.position, 1, bombLayerMask);
        if (hereHit.collider != null) {
            return; //bomb already exists here
        }

        Instantiate(bombPrefab, new Vector3(Mathf.Round(transform.position.x), Mathf.Round(transform.position.y), 0), UnityEngine.Quaternion.identity);
        
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

        // Initialize all frame timers and indices
        frameTimerUp = frameTimerDown = frameTimerLeft = frameTimerRight = (1f / framesPerSecond);
        frameIndexUp = frameIndexDown = frameIndexLeft = frameIndexRight = 0;
    }

    // Update is called once per frame
    void Update()
    {
        inputY = 0;
        inputX = 0;
        playerSpriteRenderer.flipX = false;

        bool isMoving = false;  // Add this flag to track if any movement is happening

        if (Input.GetKey(KeyCode.W)) {
            isMoving = true;
            frameTimerUp -= Time.deltaTime;
            if (frameTimerUp <= 0) {
                frameIndexUp++;
                if (frameIndexUp >= framesUp.Length) {
                    frameIndexUp = 0;
                }
                frameTimerUp = (1f / framesPerSecond);
                playerSpriteRenderer.sprite = framesUp[frameIndexUp];
                // Debug.Log("Current frame index: " + frameIndexUp);
                // Debug.Log("Direction: " + "up");
                // Debug.Log("Sprite: " + playerSpriteRenderer.sprite);
            }
            inputY = 1;
            lastDirection = "up";
        }
        if (Input.GetKey(KeyCode.A)) {
            isMoving = true;
            frameTimerLeft -= Time.deltaTime;
            if (frameTimerLeft <= 0) {
                frameIndexLeft++;
                if (frameIndexLeft >= framesRight.Length) {
                    frameIndexLeft = 0;
                }
                frameTimerLeft = (1f / framesPerSecond);
                playerSpriteRenderer.sprite = framesRight[frameIndexLeft];
                // Debug.Log("Current frame index: " + frameIndexLeft);
                // Debug.Log("Direction: " + "left");
                // Debug.Log("Sprite: " + playerSpriteRenderer.sprite);
            }
            playerSpriteRenderer.flipX = true;
            inputX = -1;
            lastDirection = "left";
        }
        if (Input.GetKey(KeyCode.S)) {
            isMoving = true;
            frameTimerDown -= Time.deltaTime;
            if (frameTimerDown <= 0) {
                frameIndexDown++;
                if (frameIndexDown >= framesDown.Length) {
                    frameIndexDown = 0;
                }
                frameTimerDown = (1f / framesPerSecond);
                playerSpriteRenderer.sprite = framesDown[frameIndexDown];
                // Debug.Log("Current frame index: " + frameIndexDown);
                // Debug.Log("Direction: " + "down");
                // Debug.Log("Sprite: " + playerSpriteRenderer.sprite);
            }
            inputY = -1;
            lastDirection = "down";
        }
        if (Input.GetKey(KeyCode.D)) {
            isMoving = true;
            frameTimerRight -= Time.deltaTime;
            if (frameTimerRight <= 0) {
                frameIndexRight++;
                if (frameIndexRight >= framesRight.Length) {
                    frameIndexRight = 0;
                }
                frameTimerRight = (1f / framesPerSecond);
                playerSpriteRenderer.sprite = framesRight[frameIndexRight];
                // Debug.Log("Current frame index: " + frameIndexRight);
                // Debug.Log("Direction: " + "right");
                // Debug.Log("Sprite: " + playerSpriteRenderer.sprite);
            }
            inputX = 1;
            lastDirection = "right";
        }

        // Only use static sprites if we're not moving
        if (!isMoving) {
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
        }

        rb2d.linearVelocity = new Vector2(inputX, inputY) * speed;

        if (Input.GetKeyDown(KeyCode.LeftShift) && (lastTimeFired + 1 / rateOfBomb) < Time.time) {
            lastTimeFired = Time.time;
            SpawnBomb();
        }
    }
}
