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

            Vector3 up = transform.position;
            up.y += 2;
            Vector3 upDir = up - transform.position;  //where you want to face - where you are
            Vector2 upDir2D = new Vector2(upDir.x, upDir.y);
            RaycastHit2D upHit = Physics2D.Raycast(transform.position, upDir2D, 2, playerBoxLayerMask);
            processRay(upHit);

            Vector3 right = transform.position;
            right.x += 2;
            Vector3 rightDir = right - transform.position;
            Vector2 rightDir2D = new Vector2(rightDir.x, rightDir.y);
            RaycastHit2D rightHit = Physics2D.Raycast(transform.position, rightDir2D, 2, playerBoxLayerMask);
            processRay(rightHit);


            Vector3 down = transform.position;
            down.y -= 2;
            Vector3 downDir = down - transform.position;
            Vector2 downDir2D = new Vector2(downDir.x, downDir.y);
            RaycastHit2D downHit = Physics2D.Raycast(transform.position, downDir2D, 2, playerBoxLayerMask);
            processRay(downHit);


            Vector3 left = transform.position;
            left.x -= 2;
            Vector3 leftDir = left - transform.position;
            Vector2 leftDir2D = new Vector2(leftDir.x, leftDir.y);
            RaycastHit2D leftHit = Physics2D.Raycast(transform.position, leftDir2D, 2, playerBoxLayerMask);
            processRay(leftHit);
            

			Destroy(gameObject);
		}






        // Vector3 dir3D = crosshair.transform.position - laserStart.transform.position;
		
		// Vector2 dir2D = new Vector2(dir3D.x, dir3D.y);



        // RaycastHit2D hit = Physics2D.Raycast(transform.position, dir2D, length, balloonLayerMask);
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
