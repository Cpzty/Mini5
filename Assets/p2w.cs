using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class p2w : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ball")
        {
            Application.Quit();

        }
    }
}
