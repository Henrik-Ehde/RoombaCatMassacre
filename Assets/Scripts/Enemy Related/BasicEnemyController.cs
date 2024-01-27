using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemyController : MonoBehaviour
{
    private Transform target;
    public Rigidbody rb;

    //public float moveSpeed = 5f;
    private float moveSpeedActual;
    public float collisionForce;

    Vector2 targetPosition;

    public float bigAcceleration;
    public float smallAcceleration;

    public float maxHealth;
    public float health;

    Vector3 originalScale;
    public float maxBigSize;
    public float minBigSize;
    public float smallSize;

    Vector3 targetDirection;

    string state = "big";



    private void Start()
    {
        health = maxHealth;
        //moveSpeedActual = moveSpeed / 100f;
        originalScale = transform.localScale;
    }

    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }

    private void FixedUpdate()
    {

        if (state == "big") BigBehaviour();
        else if (state == "fleeing") FleeingBehaviour();

        Vector3 lookRotation = Vector3.RotateTowards(transform.forward, targetDirection, 360, 0.5f);
        transform.rotation = Quaternion.LookRotation(lookRotation);

        

        //transform.position = Vector3.MoveTowards(transform.position, target.position, moveSpeedActual);
    }

    void BigBehaviour()
    {
        targetDirection = (target.position - transform.position).normalized;
        rb.AddForce(targetDirection * bigAcceleration);
    }

    void FleeingBehaviour()
    {
        targetDirection = (transform.position - target.position).normalized;
        rb.AddForce(targetDirection * smallAcceleration);
    }
    private void OnCollisionEnter(Collision collision)
    {
        GameObject other = collision.gameObject;

        if (other.tag == "Player")
        {
            if (state == "big")
            {
                other.GetComponentInParent<Roomba>().Damage();

                // calculate force vector
                Vector3 direction = (transform.position - other.transform.position).normalized;
                // normalize force vector to get direction only and trim magnitude


                other.GetComponentInParent<Rigidbody>().AddForce(-direction * collisionForce);
                GetComponent<Rigidbody>().AddForce(direction * collisionForce);
            }

            else
            {
                Debug.Log("Dö");
                Destroy(gameObject);
            }
        }
 
    }

    public void Damage(float damage)
    {
        health -= damage;

        if (health <= 0) Shrink();
        else transform.localScale = originalScale * Mathf.Lerp(minBigSize, maxBigSize, health / maxHealth);
    }

    public void Shrink()
    {
        transform.localScale = originalScale * smallSize;
        state = "small";
    }



}
