using UnityEngine;

public class ThrowBall : MonoBehaviour
{
    public GameObject ballPrefab; // Reference to the basketball prefab
    public Transform spawnPoint; // The point where the ball will be spawned
    public float throwForceMultiplier = 10f; // Multiplier for the throw force

    private GameObject currentBall; // The current ball in play
    private Vector2 startTouchPosition; // Starting position of the touch
    private Vector2 endTouchPosition; // Ending position of the touch
    private Rigidbody ballRigidbody;

    void Update()
    {
        // Check if there's a touch on the screen
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                // Capture the start position of the touch
                startTouchPosition = touch.position;

                // Instantiate a new ball at the spawn point
                currentBall = Instantiate(ballPrefab, spawnPoint.position, spawnPoint.rotation);
                ballRigidbody = currentBall.GetComponent<Rigidbody>();
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                // Capture the end position of the touch
                endTouchPosition = touch.position;

                // Calculate the swipe direction and apply force
                Vector2 swipeDirection = endTouchPosition - startTouchPosition;
                Vector3 throwDirection = new Vector3(swipeDirection.x, swipeDirection.y, swipeDirection.magnitude).normalized;

                ballRigidbody.AddForce(throwDirection * throwForceMultiplier, ForceMode.Impulse);
            }
        }
    }
}
