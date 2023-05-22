using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Zoom : MonoBehaviour
{
    private float initialDistance;
    private float initialFov;

    private void Start()
    {
        Screen.orientation = ScreenOrientation.LandscapeLeft;
    }

    void Update()
    {
        if (UnityEngine.Input.touchCount == 2)
        {
            Touch touch0 = UnityEngine.Input.GetTouch(0);
            Touch touch1 = UnityEngine.Input.GetTouch(1);

            if (touch0.phase == TouchPhase.Began || touch1.phase == TouchPhase.Began)
            {
                initialDistance = Vector2.Distance(touch0.position, touch1.position);
                initialFov = Camera.main.fieldOfView;
            }
            else if (touch0.phase == TouchPhase.Moved || touch1.phase == TouchPhase.Moved)
            {
                float currentDistance = Vector2.Distance(touch0.position, touch1.position);
                float deltaDistance = initialDistance - currentDistance;

                float newFov = Mathf.Clamp(initialFov + deltaDistance * 0.05f, 11, 60);
                Camera.main.fieldOfView = newFov;
            }
        }
    }
}
