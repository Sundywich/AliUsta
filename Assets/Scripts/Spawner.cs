using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Unity.VisualScripting;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;
using Quaternion = UnityEngine.Quaternion;

public class Spawner : MonoBehaviour
{
    [Header("Object To Spawn")]
    public GameObject objectToSpawn;


    [Header("Variables")]
    public float spawnRate;
    [SerializeField] private float Timer;
    public bool canSpawnObjects = true;
    public int maxObjectCount = 8;

    private void Start()
    {       
        InvokeRepeating("SpawnObject", 0f, spawnRate);
        canSpawnObjects = true;
    }

    private void SpawnObject()
    {
        int objectCount = CountObjectsInFrontOfTheSpawner();

        if (objectCount < maxObjectCount)
        {
            GameObject spawnedObject = Instantiate(objectToSpawn, transform.position, transform.rotation);
            Vector3 currentPositionOfTheObject = spawnedObject.transform.position;
            currentPositionOfTheObject.y -= 2f;
            spawnedObject.transform.position = currentPositionOfTheObject;
        }
    }

    private int CountObjectsInFrontOfTheSpawner()
    {
        RaycastHit2D[] hits;
        hits = Physics2D.RaycastAll(transform.position, transform.up * -1, 5f);

        int count = 0;

        foreach (RaycastHit2D hit in hits)
        {

            if (hit.collider.gameObject.CompareTag("CigKofte"))
            {
                print("hey");
                count++;
            }
        }

        return count;
    }
}
