using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottomController : MonoBehaviour
{
    public Rigidbody2D rb;
    public float rotationRate;
    public float forwardSpeed;
    public float reverseSpeed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.A))
        {
            rb.rotation += rotationRate * Time.fixedDeltaTime;
        }

        if (Input.GetKey(KeyCode.D))
        {
            rb.rotation -= rotationRate * Time.fixedDeltaTime;
        }

        if (Input.GetKey(KeyCode.W))
        {
            rb.MovePosition(transform.position + transform.up * Time.fixedDeltaTime * forwardSpeed);
        }

        if (Input.GetKey(KeyCode.S))
        {
            rb.MovePosition(transform.position - transform.up * Time.fixedDeltaTime * reverseSpeed);
        }
    }
}
