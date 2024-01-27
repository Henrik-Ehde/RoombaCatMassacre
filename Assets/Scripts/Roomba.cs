using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Roomba : MonoBehaviour
{
    public Rigidbody rb;
    public float rotationRate;
    public float smoothRotation;
    float rotation;
    public float forwardSpeed;
    public float acceleration;
    public float reverseAcceleration;
    public float reverseSpeed;

    public float iFrames;
    public bool hittable;

    public TMP_Text livesText;



    public float lives = 9;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float rotationInput = Input.GetAxis("Horizontal");
        Debug.Log(rotationInput);
        rotation = Mathf.Lerp(rotation, rotationRate * rotationInput, smoothRotation * Time.deltaTime);
        transform.Rotate(0, rotation * Time.fixedDeltaTime, 0);


        if (Input.GetKey(KeyCode.W))
        {
            //rb.MovePosition(transform.position + transform.forward * Time.fixedDeltaTime * forwardSpeed);
            rb.AddForce(transform.forward * acceleration);
        }

        if (Input.GetKey(KeyCode.S))
        {
            //rb.MovePosition(transform.position - transform.forward * Time.fixedDeltaTime * reverseSpeed);
            rb.AddForce(-transform.forward * reverseAcceleration);

        }
    }

    public void Damage()
    {
        if (hittable)
        {
            lives -= 1;
            if (lives< 1) Die();
            livesText.text = "Lives: " + lives;
            StartCoroutine(Invulnerable());
        }
    }

    IEnumerator Invulnerable()
    {
        hittable = false;
        yield return new WaitForSeconds(iFrames);
        hittable = true;

    }

    void Die()
    {
        Time.timeScale = 0;
    }
}
