using System.Collections.Generic;
using UnityEngine;

public class FloorGenerator : MonoBehaviour
{
    public GameObject floorPrefab;
    public int numberOfFloors = 10;
    public float floorLength = 10f;
    public Transform playerTransform;

    private List<GameObject> activeFloors = new List<GameObject>();
    private int currentIndex = 0;

    void Start()
    {
        InitializeFloors();
    }

  void Update()
{
    if (playerTransform.position.z > activeFloors[activeFloors.Count - 1].transform.position.z - numberOfFloors * floorLength)
    {
        SpawnFloor();
    }
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
        GameObject floor = GetPooledFloor();
        floor.transform.position = Vector3.forward * (currentIndex * floorLength);
        currentIndex++;

        activeFloors.Add(floor);
    }

    GameObject GetPooledFloor()
    {
        for (int i = 0; i < activeFloors.Count; i++)
        {
            if (!activeFloors[i].activeInHierarchy)
            {
                activeFloors[i].SetActive(true);
                return activeFloors[i];
            }
        }

        // If no inactive floor is found, instantiate a new one
        GameObject newFloor = Instantiate(floorPrefab);
        newFloor.SetActive(true);
        return newFloor;
    }
}
