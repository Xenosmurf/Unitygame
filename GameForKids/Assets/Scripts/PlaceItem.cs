using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceItem : MonoBehaviour
{
    private Rigidbody rb;
    private bool isSelected = false;
    private Vector3 lastPosition;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        lastPosition = transform.position;
    }

    void Update()
    {
        // Check for touch or mouse input
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began || Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // Check if the ray hits a furniture object
            if (Physics.Raycast(ray, out hit) && hit.collider.gameObject == gameObject)
            {
                isSelected = true;
                lastPosition = hit.point;
            }
        }

        // Check if the object is selected and being dragged
        if (isSelected && (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved || Input.GetMouseButton(0)))
        {
            Vector3 currentPos = GetTouchPosition();

            // Calculate the movement direction and distance
            Vector3 movement = currentPos - lastPosition;

            // Move the object using rigidbody velocity or position
            //rb.velocity = movement * 10f;
            rb.MovePosition(transform.position + movement);

            lastPosition = currentPos;
        }

        // Deselect the object when touch or mouse input is released
        if (isSelected && (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended || Input.GetMouseButtonUp(0)))
        {
            isSelected = false;
        }
    }

    // Get the current touch position in world space
    private Vector3 GetTouchPosition()
    {
        Vector3 touchPos;

        if (Input.touchCount > 0)
        {
            touchPos = Input.GetTouch(0).position;
        }
        else
        {
            touchPos = Input.mousePosition;
        }

        touchPos.z = 10f; // Distance from camera to touch point

        return Camera.main.ScreenToWorldPoint(touchPos);
    }
}
