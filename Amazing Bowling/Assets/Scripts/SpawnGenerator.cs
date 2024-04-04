using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// NOEL KIM
/// A01259986
/// Generates props within a defined area at the start of the game.
/// Handles prop instantiation and provides functionality to reset their positions.
/// </summary>
public class SpawnGenerator : MonoBehaviour
{
    /// <summary>
    /// Array of prefab GameObjects that can be spawned.
    /// </summary>
    public GameObject[] propPrefabs;

    /// <summary>
    /// The BoxCollider component that defines the spawning area.
    /// </summary>
    private BoxCollider area;

    /// <summary>
    /// The number of props to spawn.
    /// </summary>
    public int count = 100;

    /// <summary>
    /// A list to keep track of the instantiated prop GameObjects.
    /// </summary>
    private List<GameObject> props = new List<GameObject>();

    /// <summary>
    /// Retrieves the BoxCollider component and spawns the specified number of props at the start.
    /// </summary>
    void Start()
    {
        area = GetComponent<BoxCollider>();
        for (int i = 0; i < count; i++)
        {
            Spawn();
        }
        area.enabled = false;
    }

    /// <summary>
    /// Instantiates a single prop at a random position within the spawning area.
    /// </summary>
    private void Spawn()
    {
        int selection = Random.Range(0, propPrefabs.Length);
        GameObject selectedPrefab = propPrefabs[selection];
        Vector3 spawnPos = GetRandomPosition();
        GameObject instance = Instantiate(selectedPrefab, spawnPos, Quaternion.identity);
        props.Add(instance);
    }

    /// <summary>
    /// Calculates a random position within the BoxCollider's bounds.
    /// </summary>
    /// <returns>A random position within the BoxCollider.</returns>
    private Vector3 GetRandomPosition()
    {
        Vector3 basePosition = transform.position;
        Vector3 size = area.size;
        float posX = basePosition.x + Random.Range(-size.x / 2f, size.x / 2f);
        float posY = basePosition.y + Random.Range(-size.y / 2f, size.y / 2f);
        float posZ = basePosition.z + Random.Range(-size.z / 2f, size.z / 2f);
        Vector3 spawnPos = new Vector3(posX, posY, posZ);
        return spawnPos;
    }

    /// <summary>
    /// Resets the position of all spawned props to new random positions within the spawning area.
    /// </summary>
    public void Reset()
    {
        foreach (var prop in props)
        {
            prop.transform.position = GetRandomPosition();
            prop.SetActive(true);
        }
    }

    // Update method is not used in this script.
}
