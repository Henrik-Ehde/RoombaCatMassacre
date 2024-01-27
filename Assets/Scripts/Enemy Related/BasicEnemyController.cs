using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemyController : MonoBehaviour
{
    private Transform target;
    public Rigidbody rb;

    Transform friend;
    BasicEnemyController friendController;

    [HideInInspector] public Transform enemyContainer;

    //public float moveSpeed = 5f;
    //private float moveSpeedActual;
    public float collisionForce;

    public Vector3 targetPosition;

    public float bigAcceleration;
    public float smallAcceleration;

    public float maxHealth;
    float health;

    Vector3 originalScale;
    public float maxBigSize;
    public float minBigSize;
    public float smallSize;

    Vector3 targetDirection;


    string state = "big";

    public float fleeingTime;
    public float retryTime;

    [Header("Random Movement")]
    public float maxRandomOffset;
    float randomPositionTime;
    public float randomPositionInterval;



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


    void RandomPosition()
    {
        Vector2 randomCircle = Random.insideUnitCircle;
        targetPosition = target.position + new Vector3(randomCircle.x, 0, randomCircle.y) * maxRandomOffset;
        randomPositionTime = Time.time + randomPositionInterval;

    }



    private void FixedUpdate()
    {
        float distance = Vector3.Distance(transform.position, targetPosition);
        if (distance < 1 || Time.time > randomPositionTime) RandomPosition();

        if (state == "big") BigBehaviour();
        else if (state == "fleeing") FleeingBehaviour();
        else if (state == "converging") Converging();

        Vector3 lookRotation = Vector3.RotateTowards(transform.forward, targetDirection, 360, 0.5f);
        transform.rotation = Quaternion.LookRotation(lookRotation);

        

        //transform.position = Vector3.MoveTowards(transform.position, target.position, moveSpeedActual);
    }

    void BigBehaviour()
    {
        targetDirection = (targetPosition - transform.position).normalized;
        rb.AddForce(targetDirection * bigAcceleration);
    }

    void FleeingBehaviour()
    {
        targetDirection = (transform.position - targetPosition).normalized;
        rb.AddForce(targetDirection * smallAcceleration);
    }

    void Converging()
    {
        if (friend && friendController.state != "big")
        {
            targetDirection = (friend.position - transform.position).normalized;
            rb.AddForce(targetDirection * smallAcceleration);
        }

        else FindAFriend(0);
        

    }




    private void OnCollisionEnter(Collision collision)
    {
        GameObject other = collision.gameObject;

        if (other.tag == "Player")
        {
            if (state == "big")
            {
                other.GetComponentInParent<Roomba>().Damage();

                Vector3 direction = (transform.position - other.transform.position).normalized;

                other.GetComponentInParent<Rigidbody>().AddForce(-direction * collisionForce);
                GetComponent<Rigidbody>().AddForce(direction * collisionForce);
            }

            else
            {
                Destroy(gameObject);
            }
        }

        else if (other.gameObject.tag == "Enemy")
        {
            BasicEnemyController otherEnemy = other.GetComponent<BasicEnemyController>();
            if (state == "converging" && otherEnemy.state != "big")
            {
                Destroy(other.gameObject);
                state = "big";
                transform.localScale = originalScale;
                health = maxHealth;
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
        state = "fleeing";
        StartCoroutine(FindAFriend(fleeingTime));
    }


    IEnumerator FindAFriend(float waitTime)
    {
        friend = null;
        yield return new WaitForSeconds(waitTime);

        float smallestDistance = Mathf.Infinity;
        foreach (Transform otherTransform in enemyContainer)
        {
            if (otherTransform != transform)
            {
                BasicEnemyController other = otherTransform.GetComponent<BasicEnemyController>();
                float distance = Vector3.Distance(transform.position, otherTransform.position);
                if (other.state != "big" && distance < smallestDistance)
                {
                    friend = otherTransform;
                    friendController = other;
                    smallestDistance = distance;
                }
            }
        }

        if (friend == null) StartCoroutine(FindAFriend(retryTime));
        else state = "converging";
    }
}
