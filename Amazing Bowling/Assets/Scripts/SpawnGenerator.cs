using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This class is responsible for generating props in the game world.
public class SpawnGenerator : MonoBehaviour
{
    // Array to hold the prefab GameObjects that can be spawned.
    public GameObject[] propPrefabs;

    // Reference to the BoxCollider component that defines the spawning area.
    private BoxCollider area;

    // The number of props to spawn.
    public int count = 100;

    // A list to keep track of the instantiated prop GameObjects.
    private List<GameObject> props = new List<GameObject>();

    // Start is called before the first frame update.
    void Start()
    {
        // Retrieve the BoxCollider component attached to the same GameObject as this script.
        area = GetComponent<BoxCollider>();

        // Loop to instantiate the specified number of props.
        for (int i = 0; i < count; i++)
        {
            // Call the Spawn method to create a new prop.
            Spawn();
        }

        // Optionally disable the collider component if it's no longer needed.
        area.enabled = false;
    }  

    // This method handles the instantiation of a single prop.
    private void Spawn()
    {
        // Select a random prefab from the array of available prefabs.
        int selection = Random.Range(0, propPrefabs.Length);
        GameObject selectedPrefab = propPrefabs[selection];

        // Calculate a random position within the BoxCollider bounds.
        Vector3 spawnPos = GetRandomPosition();

        // Instantiate the selected prefab at the calculated position with no rotation (identity).
        GameObject instance = Instantiate(selectedPrefab, spawnPos, Quaternion.identity);

        // Add the new instance to the list of props.
        props.Add(instance); 
    }

    // Calculate a random position within the BoxCollider's bounds.
    private Vector3 GetRandomPosition()
    {
        // Start with the base position of the spawning area.
        Vector3 basePosition = transform.position;
        // Get the size of the BoxCollider.
        Vector3 size = area.size;

        // Calculate a random position within the bounds.
        float posX = basePosition.x + Random.Range(-size.x / 2f, size.x / 2f);
        float posY = basePosition.y + Random.Range(-size.y / 2f, size.y / 2f);
        float posZ = basePosition.z + Random.Range(-size.z / 2f, size.z / 2f);

        // Combine the calculated coordinates into a single Vector3 position.
        Vector3 spawnPos = new Vector3(posX, posY, posZ);

        return spawnPos;
    }

    // This method resets the position of all spawned props.
    public void Reset()
    {
        // Iterate through each prop in the list.
        for(int i = 0; i < props.Count; i++)
        {
            // Relocate each prop to a new random position.
            props[i].transform.position = GetRandomPosition();
            // Reactivate the prop if it has been previously deactivated.
            props[i].SetActive(true);
        }
    }

    // The Update method is called once per frame. Currently, there's no behavior defined in Update for this script.
}
