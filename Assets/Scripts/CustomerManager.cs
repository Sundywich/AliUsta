using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerManager : MonoBehaviour
{
    [Header("Variables to Spawn the Customers")]
    public int howManyCustomersToSpawn;
    public int customerCount;
    public float spawnRate;
    public GameObject Customer;
    public static CustomerManager Instance;
    private int totalSpawnedCustomer = 0;
    private bool spawnIsDone = false;

    // To make a hand-made IEnumerator
    private float timer;

    [Header("General Variables")]
    public static int score;

    [Header("Variables to Move the Customers")]
    public Transform CashoutPlace;
    public Cashout Cashout;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        CashoutPlace = GameObject.FindGameObjectWithTag("CashoutPlace").transform;
        Cashout = GameObject.FindGameObjectWithTag("CashoutPlace").GetComponent<Cashout>();
    }

    private void Start()
    {
        timer = 0;      
    }

    private void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnRate && customerCount < howManyCustomersToSpawn && !spawnIsDone) // Using timer float instead of WaitForSeconds because it is much more easier to manage (InvokeRepeating can also be used)
        {
            GameObject spawnedCustomer = Instantiate(Customer, transform.position, transform.rotation); // Spawn a customer and get its reference
            customerCount++; // Increase the customer count on the scene currently
            totalSpawnedCustomer++;
             
            Vector3 newPositionForSpawnedCustomer = CashoutPlace.position + Vector3.right * customerCount * 1.8f; // Calculate new position for the spawned customer to move
            spawnedCustomer.GetComponent<Customer>().placeToGo = newPositionForSpawnedCustomer; // Set that new position


            timer = 0; // Set timer back to zero for code to count it again
        }

        else if (totalSpawnedCustomer >= howManyCustomersToSpawn)
        {
            spawnIsDone = true;
            return;
        }
    }

    IEnumerator SpawnCustomers()
    {
        for(int i = 0; i < howManyCustomersToSpawn; i++)
        {
            yield return new WaitForSeconds(spawnRate);
        }
    }

    public int GetCustomerCount()
    {
        return customerCount;
    }
}
