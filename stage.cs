using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    public GameObject initialObject; 
    public GameObject nextObject;    
    public Transform spawnPoint;     
    public float healthThreshold = 0; 

    private GameObject currentObject;

    void Start()
    {
        currentObject = Instantiate(initialObject, spawnPoint.position, spawnPoint.rotation);
    }

    void Update()
    {
        if (currentObject.GetComponent<enemy>().CurHealth <= healthThreshold)
        {
            SpawnNextObject();
        }
    }

    void SpawnNextObject()
    {
        Destroy(currentObject);
        
        currentObject = Instantiate(nextObject, spawnPoint.position, spawnPoint.rotation);
    }
}