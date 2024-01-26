using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopController : MonoBehaviour
{
    public Transform cat;
    public float rotationSpeed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.J))
        {
            cat.Rotate(0, 0, rotationSpeed * Time.fixedDeltaTime);
        }

        if (Input.GetKey(KeyCode.K))
        {
            cat.Rotate(0, 0, -rotationSpeed * Time.fixedDeltaTime);
        }

    }
}
