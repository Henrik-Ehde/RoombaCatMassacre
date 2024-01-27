using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cat : MonoBehaviour
{
    public Transform cat;
    public float rotationSpeed;
    public float smoothRotation;
    float rotation;

    public Transform muzzle;
    public GameObject projectile;

    public float rateOfFire;
    public float projectileSpeed;
    float nextShotTime;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float rotationDirection = Input.GetAxis("CatRotation");
        rotation = Mathf.Lerp(rotation, rotationSpeed * rotationDirection, smoothRotation * Time.deltaTime);
        cat.Rotate(0, rotation * Time.fixedDeltaTime, 0);

        if (Input.GetKey(KeyCode.K) && Time.time > nextShotTime)
        {
            Fire();
        }
    }

    private void Fire()
    {
        nextShotTime = Time.time + 1 / rateOfFire;
        GameObject newProjectile = Instantiate(projectile, muzzle.position,transform.rotation);
        newProjectile.transform.forward = muzzle.up;
        newProjectile.GetComponent<Bullet>().speed = projectileSpeed;
    }
}