using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
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

    [Header("General Variables and UI")]
    public GameObject Player;
    public static int score;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI timeText;
    public float TourTime = 31f;
    public GameObject TourMenu;
    public static int tourCount;
    public TextMeshProUGUI inHandText;
    public float deltaTime;

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
        timer = 0; // For spawning customers not counting time
        Time.timeScale = 0;
        TourMenu.SetActive(true);
    }

    private void Update()
    {
        #region Spawn Customers 
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
        }
        #endregion
        
        deltaTime = Time.deltaTime;
             
        timeText.text = "Time: " + TourTime.ToString();
        scoreText.text = "Current: " + score.ToString();
        inHandText.text = "Cig Kofte in Hand: " + Player.transform.childCount.ToString();

        if(TourTime <= 0)
        {
            Time.timeScale = 0;
            TourMenu.SetActive(true);
            TourTime = 31f;
        }
        else
        {
            TourTime -= Time.deltaTime;
        }
    }


    public int GetCustomerCount()
    {
        return customerCount;
    }

    public void StartNewTour()
    {
        tourCount++;
        switch (tourCount)
        {
            case 1:
                howManyCustomersToSpawn = howManyCustomersToSpawn;
                break;
            case 2:
                howManyCustomersToSpawn += 5;
                break;
            case 3:
                howManyCustomersToSpawn += 5;
                spawnRate -= 1;
                break;
            case 4:
                howManyCustomersToSpawn += 10;
                break;
            case 5:
                howManyCustomersToSpawn += 5;
                spawnRate -= 0.5f;
                break;
            default: 
                break;
        }

        // Destroy all customers
        foreach(GameObject Customer in GameObject.FindGameObjectsWithTag("Customer"))
        {
            Destroy(Customer);
        }

        // Destroy all cig koftes
        for(int i = 0; i < Player.transform.childCount; i++)
        {
            Destroy(Player.transform.GetChild(i).gameObject);
        }
        
        // After destroying all customers
        timer = 0;
        spawnIsDone = false;
        customerCount = 0;
        Cashout.currentOrder = 0;
        Cashout.thereIsACustomer = false;
        Time.timeScale = 1;       
        TourMenu.SetActive(false);
        
    }
}
