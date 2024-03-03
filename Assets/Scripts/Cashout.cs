using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Cashout : MonoBehaviour
{
    [Header ("Order Variables")]
    [HideInInspector] public static int currentOrder;
    [SerializeField] private GameObject currentCustomer;
    [SerializeField] private bool orderHasGiven = false;
    [HideInInspector] public static bool thereIsACustomer = false;

    [Header("UI")]
    public TextMeshProUGUI orderText;


    private void Update()
    {
        if(!orderHasGiven)
        {
            orderText.text = "Current Order: " + currentOrder;
        }
        else
        {
            orderText.text = "Congrats! ";
        }
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Customer"))
        {
            currentCustomer = collision.gameObject;
            orderHasGiven = false;
            currentOrder = collision.GetComponent<Customer>().orderAmount;
            thereIsACustomer = true;
        }
        if (collision.CompareTag("Player"))
        {
            if(collision.transform.childCount >= currentOrder && !orderHasGiven && thereIsACustomer)
            {
                for(int i = 0; i < currentOrder; i++)
                {
                    Destroy(collision.transform.GetChild(i).gameObject);
                }
                print("Order has taken");
                CustomerManager.score++;             
                orderHasGiven = true;
                Destroy(currentCustomer);
                thereIsACustomer = false;
                CustomerManager.Instance.customerCount--;

                GameObject[] array = GameObject.FindGameObjectsWithTag("Customer");
                for (int a = 0; a < array.Length; a++)
                {
                    GameObject Customer = array[a + 1];
                    Customer.GetComponent<Customer>().placeToGo = this.transform.position + 1.8f * (a + 1) * Vector3.right;
                }
                
            }
        }
    }

}
