using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemyController : MonoBehaviour
{
    private Transform target;

    public float moveSpeed = 5f;
    private float moveSpeedActual;
    public float collisionForce;

    private void Start()
    {
        moveSpeedActual = moveSpeed / 100f;
    }

    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }

    private void FixedUpdate()
    {
        transform.position = Vector3.MoveTowards(transform.position, target.position, moveSpeedActual);

        Vector3 targetDirection = target.position - transform.position;
        Vector3 lookRotation = Vector3.RotateTowards(transform.forward, targetDirection, 360, 0.5f);
        transform.rotation = Quaternion.LookRotation(lookRotation);

    }

    private void OnCollisionEnter(Collision collision)
    {
        GameObject other = collision.gameObject;

        if (other.tag == "Player")
        {
            other.GetComponentInParent<Roomba>().Damage();
            
            // calculate force vector
            Vector3 direction = (transform.position - other.transform.position).normalized;
            // normalize force vector to get direction only and trim magnitude


            other.GetComponentInParent<Rigidbody>().AddForce(-direction * collisionForce);
            GetComponent<Rigidbody>().AddForce(direction * collisionForce);
        }
    }



}
