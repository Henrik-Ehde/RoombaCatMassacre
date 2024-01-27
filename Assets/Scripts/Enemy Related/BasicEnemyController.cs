using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemyController : MonoBehaviour
{
    private Transform target;

    public float moveSpeed = 5f;
    private float moveSpeedActual;

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



}
