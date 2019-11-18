using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class controles : MonoBehaviour
{
    Rigidbody m_Rigidbody;
    Rigidbody ball_Rigidbody;
    public float m_Speed;
    float rot_Speed;
    private GameObject ball;
    private GameObject porteria;
    private Transform target;
    private float xrandrob;
    private float zrandrob;
    private float xrandball;
    private float xrandporteria;
    private float zrandball;
    private Vector3 shotangle;
    private bool pache;
    private bool rotatePache;
    private string fuzzy_speed;
    private string fuzzy_rotate;
    private float velocidad_normal;
    private float velocidad_lenta;
    private float velocidad_rapida;
    private float rotacion_normal;
    private float rotacion_lenta;
    private float rotacion_rapida;


    //fuzzify
    private void fuzzify_speed(float distance)
    {
        velocidad_rapida = 0;
        velocidad_normal = 0;
        velocidad_lenta = 0;

        if (distance <= 4) {
            float assign1 = distance / 4;
            if(assign1 >= 0.5)
            {
                velocidad_normal = assign1;
                velocidad_lenta = 1 - assign1;
                
            }
            else
            {
                velocidad_normal = 1 - assign1;
                velocidad_lenta = assign1;
            }
        }

      else if(distance>4 && distance <= 8)
        {
            float assign2 = distance / 8;
            if (assign2 >= 0.5)
            {
                velocidad_rapida = assign2;
                velocidad_normal = 1 - assign2;

            }

            else
            {
                velocidad_rapida = 1- assign2;
                velocidad_normal = assign2;

            }
        }

        else
        {
            velocidad_rapida = 1;
        }


    }

    private void fuzzify_rot(float angleS)
    {
        rotacion_rapida = 0;
        rotacion_normal = 0;
        rotacion_lenta = 0;

        if (angleS <= 90)
        {
            float assign1 = angleS / 90;
            if (assign1 >= 0.5)
            {
                rotacion_normal = assign1;
                rotacion_lenta = 1 - assign1;

            }
            else
            {
                rotacion_normal = 1 - assign1;
                rotacion_lenta = assign1;
            }
        }

        else if (angleS > 90 && angleS <= 180)
        {
            float assign2 = angleS / 180;
            if (assign2 >= 0.5)
            {
                rotacion_rapida = assign2;
                rotacion_normal = 1 - assign2;

            }

            else
            {
                rotacion_rapida = 1 - assign2;
                rotacion_normal = assign2;

            }
        }

        else
        {
            rotacion_rapida = 1;
        }


    }




    private float fuzzy_and(float x, float y)
    {
        return x * y;
    }

    private float fuzzy_or(float x, float y)
    {
        return (x + y) - (x * y);
    }
    void Start()
    {

        //Fetch the Rigidbody component you attach from your GameObject
        m_Rigidbody = GetComponent<Rigidbody>();
        //Set the speed of the GameObject
        m_Speed = 2.0f;
        rot_Speed = 30.0f;
        ball = GameObject.FindGameObjectWithTag("Ball");
        porteria = GameObject.FindGameObjectWithTag("Porteria");
        target = ball.transform;
        xrandrob = Random.Range(0.0f, -17.0f);
        zrandrob = Random.Range(-22.0f, -32.0f);
        xrandball = Random.Range(0.0f, -17.0f);
        xrandporteria = Random.Range(-5.0f, 5.0f);
        zrandball = Random.Range(-22.0f, -32.0f);

        transform.position = new Vector3(xrandrob, 17, zrandrob);
        ball.transform.position = new Vector3(xrandball, 17, zrandball);
        ball_Rigidbody = ball.GetComponent<Rigidbody>();
        pache = false;
        rotatePache = false;
    }

    void Update()
    {
        Vector3 targetDir = target.position - transform.position;
        Vector3 forward = transform.forward;
        float angle = Vector3.Angle(targetDir, forward);
        //Debug.Log(angle);

        //distance check
        float dist = Vector3.Distance(ball.transform.position, transform.position);
        print("Distance to other: " + dist);


        if (Input.GetKey(KeyCode.UpArrow))
        {
            //Move the Rigidbody forwards constantly at speed you define (the blue arrow axis in Scene view)
            m_Rigidbody.velocity = transform.forward * m_Speed;
        }

        if (Input.GetKey(KeyCode.DownArrow))
        {
            //Move the Rigidbody backwards constantly at the speed you define (the blue arrow axis in Scene view)
            m_Rigidbody.velocity = -transform.forward * m_Speed;
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            //Rotate the sprite about the Y axis in the positive direction
            transform.Rotate(new Vector3(0, 1, 0) * Time.deltaTime * rot_Speed, Space.World);
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            //Rotate the sprite about the Y axis in the negative direction
            transform.Rotate(new Vector3(0, -1, 0) * Time.deltaTime * rot_Speed, Space.World);
        }

        if (Input.GetKey(KeyCode.D))
        {
            //Rotate the sprite about the Y axis in the negative direction
            ball.transform.Rotate(new Vector3(0, -1, 0) * Time.deltaTime * rot_Speed, Space.World);
        }

        if (Input.GetKey(KeyCode.W))
        {
            ball_Rigidbody.velocity = ball.transform.forward * 5;
        }
        if (pache)
        {
            //get angle to goalie
            shotangle = new Vector3(porteria.transform.position.x + xrandporteria, porteria.transform.position.y, porteria.transform.position.z);
            if (!rotatePache)
            {
                ball.transform.LookAt(shotangle);
                
            }

            ball_Rigidbody.velocity = ball.transform.forward * 4;


        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ball"){
            m_Speed = 0;
            pache = true;

        }
    }

    void OnCollisionExit(Collision other)
    {

        m_Speed = 0.5f;
    }
}