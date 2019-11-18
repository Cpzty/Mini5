using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{

    public float movementSpeed = 5;

    private Rigidbody rb;
    private Vector3 endPosition = new Vector3(-7, -0.5f, -0.10f);
    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (rb.position != endPosition)
        {
            Vector3 newPosition = Vector3.MoveTowards(rb.position, endPosition, movementSpeed * Time.deltaTime);
            rb.MovePosition(newPosition);
        }
    }
}