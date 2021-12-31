using UnityEngine;

/*
 * Create a color line between two objects
 * Measure distance in meters
 */

public class MouseScript : MonoBehaviour
{
    // Field variable to store current value to use in next frame
    private Vector3 oldPos;

    // Update is called once per frame
    private void Update()
    {
        GetMousePosition();
    }

    // Get the mouse position in 3D world space
    private void GetMousePosition()
    {
        // Check for Left mouse Key 
        if (!Input.GetMouseButtonDown(0)) return;
            if (!Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out var hit, 100)) return;
            
            /*
            * hit.point is the position, where the Raycast from the camera
            * position towards the ground hit the first collider
            * var positionInWorld = hit.point;
            */
            
            // Store current position
            var objCurrentPos = hit.transform.position;
            
            // Calculate distance between currentPos and oldPos
            var distance = Vector3.Distance(objCurrentPos, oldPos);
            
            // Log distance between two points
            Debug.Log("Distance between: " + objCurrentPos + " and " + oldPos + " = " + distance);
            
            // Store the currentPos in oldPos to reuse later
            oldPos = objCurrentPos;
    }
}