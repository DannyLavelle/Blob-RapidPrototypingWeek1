using UnityEngine;

public class RotateObject : MonoBehaviour
{
    public float rotationSpeed = 90f; // Degrees per second, you can set this in the Inspector.

    private void Update()
    {
        // Rotate the object by the specified speed every second.
        // The 'Time.deltaTime' factor ensures that the rotation is frame rate-independent.
        transform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime);
    }
}
