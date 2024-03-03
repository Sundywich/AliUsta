using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cashout : MonoBehaviour
{
    [SerializeField] private int currentOrder;
    [SerializeField] private bool orderHasGiven = false; 

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Customer"))
        {
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
                orderHasGiven = true;
            }
        }
    }
}
