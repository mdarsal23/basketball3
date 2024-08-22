using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class PlaceHoop : MonoBehaviour
{
    public GameObject hoopPrefab;  // The basketball hoop prefab to place
    private GameObject placedHoop; // Reference to the placed hoop
    private ARRaycastManager raycastManager;
    private Vector2 touchPosition;

    static List<ARRaycastHit> hits = new List<ARRaycastHit>();

    void Start()
    {
        raycastManager = GetComponent<ARRaycastManager>();
    }

    void Update()
    {
        // Check if there's a touch on the screen
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            touchPosition = touch.position;

            // Only proceed if the touch phase is Began or Moved
            if (touch.phase == TouchPhase.Began)
            {
                PlaceHoopOnPlane();
            }
        }
    }

    void PlaceHoopOnPlane()
    {
        // Perform the raycast
        if (raycastManager.Raycast(touchPosition, hits, TrackableType.PlaneWithinPolygon))
        {
            // Get the first hit point
            Pose hitPose = hits[0].pose;

            if (placedHoop == null)
            {
                // If the hoop hasn't been placed yet, instantiate it at the hit position
                placedHoop = Instantiate(hoopPrefab, hitPose.position, hitPose.rotation);
            }
            else
            {
                // If the hoop is already placed, move it to the new position
                placedHoop.transform.position = hitPose.position;
                placedHoop.transform.rotation = hitPose.rotation;
            }
        }
    }
}
