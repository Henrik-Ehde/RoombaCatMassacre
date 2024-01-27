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
    public float acceleration;
    public float reverseAcceleration;
    public float speedWhileRotating;

    public float iFrames;
    public bool hittable;

    public TMP_Text livesText;

    int currentPlayer=0;



    public float lives = 9;
    // Start is called before the first frame update
    public void SwapPlayer()
    {
        currentPlayer++;
        currentPlayer %= 2;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float rotationInput = Input.GetAxis("Horizontal"+currentPlayer);
        rotation = Mathf.Lerp(rotation, rotationRate * rotationInput, smoothRotation * Time.deltaTime);
        transform.Rotate(0, rotation * Time.fixedDeltaTime, 0);


        float verticalInput = Input.GetAxis("Vertical"+currentPlayer);
        float force;
        if (verticalInput < 0) force = reverseAcceleration * verticalInput;
        else force = acceleration * verticalInput;

        if (rotationInput != 0) force *= speedWhileRotating;

        rb.AddForce(transform.forward * force);
    }

    public void Damage()
    {
        if (hittable)
        {
            lives -= 1;
            //if (lives< 1) Die();
            //livesText.text = "Lives: " + lives;
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
