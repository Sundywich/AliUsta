using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Customer : MonoBehaviour
{
    private Rigidbody2D rb;

    [Header("Movement Variables")]
    public float speed = 2f;
    [HideInInspector] public Vector3 placeToGo;

    // Start is called before the first frame update
    private void Awake()
    {     
        rb = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, placeToGo, speed * Time.deltaTime); 
    }

}
