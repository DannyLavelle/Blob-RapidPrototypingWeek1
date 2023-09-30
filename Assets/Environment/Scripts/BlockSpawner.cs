using UnityEngine;

public class BlockSpawner : MonoBehaviour
{
    public GameObject[] blockPrefabs; // Array of block prefabs to spawn
    public float spawnHeight = 45f; // Height above the spawner to spawn blocks
    public float moveDistance = 45f; // Distance to move the spawner upwards after spawning

    public Transform playerTransform;
    private float lastPlayerY;

    private void Start()
    {
       // Assuming you have a PlayerController script
        lastPlayerY = playerTransform.position.y;
    }

    private void Update()
    {
        // Define a threshold for Y position comparison
        float positionThreshold = 0.1f; // Adjust this value as needed

        // Check if the player's Y position is close to the object's Y position
        if (Mathf.Abs(playerTransform.position.y - transform.position.y) < positionThreshold)
        {
            Debug.Log("Triggered Spawn");
            SpawnBlock();
        }
    }

    private void SpawnBlock()
    {
        if (blockPrefabs.Length == 0)
        {
            Debug.LogWarning("No block prefabs assigned to the spawner.");
            return;
        }

        // Choose a random block prefab from the array
        GameObject randomBlockPrefab = blockPrefabs[Random.Range(0, blockPrefabs.Length)];

        // Calculate the spawn position 45 units above the spawner's position
        Vector3 spawnPosition = transform.position + Vector3.up * spawnHeight;

        // Instantiate the chosen block prefab at the calculated position
        Instantiate(randomBlockPrefab, spawnPosition, Quaternion.identity);

        // Move the spawner upwards by the specified distance
        transform.Translate(Vector3.up * moveDistance);
    }
}
