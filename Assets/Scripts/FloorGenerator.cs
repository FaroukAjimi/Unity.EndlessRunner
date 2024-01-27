using System.Collections.Generic;
using UnityEngine;

public class FloorGenerator : MonoBehaviour
{
    public List<GameObject> startFloorPrefabs;
    public List<GameObject> easyFloorPrefabs;
    public List<GameObject> intermediateFloorPrefabs;
    public List<GameObject> hardFloorPrefabs;
    public int numberOfFloors = 10;
    public float floorLength = 10f;
    public Transform playerTransform;
    public int blocksToSpawn = 10;
    public float destructionDistance = 20f;

    private List<GameObject> activeFloors = new List<GameObject>();
    private int currentIndex = 0;
    private int blocksPassed = 0;

    private List<List<GameObject>> floorVariations = new List<List<GameObject>>();

    void Start()
    {
        InitializeFloorVariations();
        InitializeFloors();
    }

    void InitializeFloorVariations()
    {
        // Add a list for the start floor type (use only the first prefab for start)
        floorVariations.Add(startFloorPrefabs.GetRange(0, 1));

        // Add lists for each difficulty level
        floorVariations.Add(easyFloorPrefabs);
        floorVariations.Add(intermediateFloorPrefabs);
        floorVariations.Add(easyFloorPrefabs); // Repeat easy
        floorVariations.Add(hardFloorPrefabs);
    }

    void Update()
    {
        if (playerTransform.position.z > activeFloors[activeFloors.Count - 1].transform.position.z - numberOfFloors * floorLength)
        {
            SpawnFloor();
        }

        CheckBlocksPassed();
    }

    void InitializeFloors()
    {
        for (int i = 0; i < numberOfFloors; i++)
        {
            SpawnFloor();
        }
    }

    void SpawnFloor()
    {
        List<GameObject> variation = floorVariations[currentIndex % floorVariations.Count];
        GameObject floorPrefab = GetRandomPrefab(variation);

        GameObject floor = GetPooledFloor(floorPrefab);
        floor.transform.position = Vector3.forward * (currentIndex * floorLength);
        currentIndex++;

        activeFloors.Add(floor);
    }

    GameObject GetRandomPrefab(List<GameObject> prefabList)
    {
        // Randomly select a prefab from the list
        return prefabList[Random.Range(0, prefabList.Count)];
    }

    GameObject GetPooledFloor(GameObject prefab)
    {
        for (int i = 0; i < activeFloors.Count; i++)
        {
            if (!activeFloors[i].activeInHierarchy && activeFloors[i].CompareTag(prefab.tag))
            {
                activeFloors[i].SetActive(true);
                return activeFloors[i];
            }
        }

        // If no inactive floor of the specified type is found, instantiate a new one
        GameObject newFloor = Instantiate(prefab);
        newFloor.SetActive(true);
        return newFloor;
    }

    void CheckBlocksPassed()
    {
        int passedBlocks = Mathf.FloorToInt((playerTransform.position.z - activeFloors[0].transform.position.z) / floorLength);

        if (passedBlocks > blocksPassed)
        {
            DestroyPassedFloors();
            SpawnNewFloors();
            blocksPassed = passedBlocks;
        }
    }

    void DestroyPassedFloors()
    {
        float destroyZPos = playerTransform.position.z - destructionDistance;

        // Use a temporary list to store the floors to be destroyed
        List<GameObject> floorsToDestroy = new List<GameObject>();

        // Iterate through active floors
        for (int i = 0; i < activeFloors.Count; i++)
        {
            if (activeFloors[i] != null && activeFloors[i].transform.position.z < destroyZPos)
            {
                floorsToDestroy.Add(activeFloors[i]);
            }
        }

        // Destroy the floors in the temporary list
        foreach (GameObject floor in floorsToDestroy)
        {
            if (floor != null)
            {
                floor.SetActive(false);
            }
        }
    }

    void SpawnNewFloors()
    {
        for (int i = 0; i < blocksToSpawn; i++)
        {
            SpawnFloor();
        }
    }
}
