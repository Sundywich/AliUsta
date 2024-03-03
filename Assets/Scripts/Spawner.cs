using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Unity.VisualScripting;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;
using Quaternion = UnityEngine.Quaternion;
using TMPro;
using UnityEngine.SocialPlatforms.Impl;

public class Spawner : MonoBehaviour
{
    [Header("Object To Spawn")]
    public GameObject objectToSpawn;


    [Header("Variables")]
    public float spawnRate;
    [SerializeField] private float Timer;
    public bool canSpawnObjects = true;
    public int maxObjectCount = 8;
    public int level = 1;

    [Header("UI")]
    public TextMeshProUGUI requiresText;

    private void Start()
    {       
        InvokeRepeating("SpawnObject", 0f, spawnRate);
        canSpawnObjects = true;
    }

    private void Update()
    {
        switch(level)
        {
            case 1:
                spawnRate = spawnRate;
                requiresText.text = "Requires: 5 points";
                break;
            case 2:
                spawnRate += 0.5f;
                requiresText.text = "Requires: 5 points";
                break;
            case 3:
                spawnRate += 1;
                requiresText.text = "Requires: 10 points";
                break;
            case 4:
                spawnRate += 1;
                requiresText.text = "Requires: 10 points";
                break;
            case 5:
                spawnRate += 1.5f;
                requiresText.text = "MAX";
                break;
        }
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
                // print("hey");
                count++;
            }
        }

        return count;
    }

    public void Upgrade()
    {
        if(CustomerManager.score > 5 && level == 1)
        {
            CustomerManager.score -= 5;
            CustomerManager.Instance.scoreText.text = "Current: " + CustomerManager.score.ToString();
            level = 2;
        }
        else if(CustomerManager.score > 5 && level == 2)
        {
            CustomerManager.score -= 5;
            CustomerManager.Instance.scoreText.text = "Current: " + CustomerManager.score.ToString();
            level = 3;
        }
        else if(CustomerManager.score > 10 && level == 3)
        {
            CustomerManager.score -= 10;
            CustomerManager.Instance.scoreText.text = "Current: " + CustomerManager.score.ToString();
            level = 4;
        }
        else if (CustomerManager.score > 10 && level == 4)
        {
            CustomerManager.score -= 10;
            CustomerManager.Instance.scoreText.text = "Current: " + CustomerManager.score.ToString();
            level = 5;
        }
        
    }
}
