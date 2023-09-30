using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // Reference to the character's Transform
    public Vector3 offset = new Vector3(0f, 0f, -10f); // Offset the camera from the character

    void LateUpdate()
    {
        if (target != null)
        {
            // Calculate the target position with the offset, but set X and Z to 0
            Vector3 targetPosition = new Vector3(0f, target.position.y, 0f) + offset;

            // Interpolate smoothly between the current camera position and the target position
            transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * 5f);
        }
    }
}
