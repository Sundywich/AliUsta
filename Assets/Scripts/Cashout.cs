using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cashout : MonoBehaviour
{
    [Header ("Order Variables")]
    [SerializeField] private int currentOrder;
    [SerializeField] private GameObject currentCustomer;
    [SerializeField] private bool orderHasGiven = false;
    

    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Customer"))
        {
            currentCustomer = collision.gameObject;
            orderHasGiven = false;
            currentOrder = collision.GetComponent<Customer>().orderAmount;
        }
        if (collision.CompareTag("Player"))
        {
            if(collision.transform.childCount >= currentOrder && !orderHasGiven)
            {
                for(int i = 0; i < currentOrder; i++)
                {
                    Destroy(collision.transform.GetChild(i).gameObject);
                }
                print("Order has taken");
                CustomerManager.score++;             
                orderHasGiven = true;
                Destroy(currentCustomer);
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

    //private void OnTriggerStay2D(Collider2D collision)
    //{
    //    if(collision.CompareTag("Customer") && orderHasGiven)
    //    {
    //        Destroy(collision);
    //        orderHasGiven = false;
    //    }
    //}
}
