using UnityEngine;
using UnityEngine.SceneManagement;

public class BombScript : MonoBehaviour
{   
    public float lifeTime = 1;

    public LineRenderer laserLineRenderer;

    public LayerMask playerBoxLayerMask;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        lifeTime -= Time.deltaTime;
        if (lifeTime <= 0) {
            laserLineRenderer.enabled = true;
            
            // Configure LineRenderer for 8 points (4 lines, 2 points each)
            laserLineRenderer.positionCount = 8;
            
            // Up ray
            Vector3 up = transform.position;
            up.y += 2;
            Vector3 upDir = up - transform.position;
            Vector2 upDir2D = new Vector2(upDir.x, upDir.y);
            RaycastHit2D upHit = Physics2D.Raycast(transform.position, upDir2D, 2, playerBoxLayerMask);
            laserLineRenderer.SetPosition(0, transform.position);
            laserLineRenderer.SetPosition(1, upHit.collider != null ? upHit.point : up);
            processRay(upHit);

            // Right ray
            Vector3 right = transform.position;
            right.x += 2;
            Vector3 rightDir = right - transform.position;
            Vector2 rightDir2D = new Vector2(rightDir.x, rightDir.y);
            RaycastHit2D rightHit = Physics2D.Raycast(transform.position, rightDir2D, 2, playerBoxLayerMask);
            laserLineRenderer.SetPosition(2, transform.position);
            laserLineRenderer.SetPosition(3, rightHit.collider != null ? rightHit.point : right);
            processRay(rightHit);

            // Down ray
            Vector3 down = transform.position;
            down.y -= 2;
            Vector3 downDir = down - transform.position;
            Vector2 downDir2D = new Vector2(downDir.x, downDir.y);
            RaycastHit2D downHit = Physics2D.Raycast(transform.position, downDir2D, 2, playerBoxLayerMask);
            laserLineRenderer.SetPosition(4, transform.position);
            laserLineRenderer.SetPosition(5, downHit.collider != null ? downHit.point : down);
            processRay(downHit);

            // Left ray
            Vector3 left = transform.position;
            left.x -= 2;
            Vector3 leftDir = left - transform.position;
            Vector2 leftDir2D = new Vector2(leftDir.x, leftDir.y);
            RaycastHit2D leftHit = Physics2D.Raycast(transform.position, leftDir2D, 2, playerBoxLayerMask);
            laserLineRenderer.SetPosition(6, transform.position);
            laserLineRenderer.SetPosition(7, leftHit.collider != null ? leftHit.point : left);
            processRay(leftHit);

            // Add small delay before destroying to see the laser
            Destroy(gameObject, 0.1f);
        }
    }

    private void processRay(RaycastHit2D hit) {
        Debug.Log("hit: " + hit.collider);

        if (hit.collider != null && hit.collider.CompareTag("Player")) {
            Debug.Log("hit player");
            // Debug.Log("Hit a balloon!");
            // hit.collider.GetComponent<Balloon>().Pop();
            ReloadScene();
            
		}
        if (hit.collider != null && hit.collider.CompareTag("Box")) {
            Debug.Log("hit box");
            Destroy(hit.collider.gameObject);
        }
    }

    private void ReloadScene() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
