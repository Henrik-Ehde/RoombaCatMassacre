using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Roomba : MonoBehaviour
{
    public Rigidbody rb;
    public Cat cat;
    public float rotationRate;
    public float smoothRotation;
    float rotation;
    public float acceleration;
    public float reverseAcceleration;
    public float speedWhileRotating;

    public float iFrames;
    public bool hittable;

    public float lives = 9;
    public TMP_Text livesText;


    int currentPlayer=0;

    [Header("Spray")]
    public float duration = 0.7f;
    public int shots = 8;
    public float spread = 120;
    public float cooldown;
    float sprayReadyTime;

    public SoundContainer hurtSounds;
    public BasicSound gameOverSound;


    public void SwapPlayer()
    {
        currentPlayer++;
        currentPlayer %= 2;
    }

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

        if (Input.GetAxis("Special" + currentPlayer) != 0 && Time.time > sprayReadyTime)
        {
            Spray();
        }
    }

    public void Damage()
    {
        if (hittable)
        {
            AudioManager.Instance.PlaySoundBaseOnTarget(hurtSounds, transform, true);

            lives -= 1;
            if (lives< 1) Die();
            livesText.text = lives + " lives";
            StartCoroutine(Invulnerable());
        }
    }

    void Spray()
    {
        float direction = Input.GetAxis("Special" + currentPlayer);
        cat.StartCoroutine(cat.Spray(duration, shots, spread, direction));
        sprayReadyTime += cooldown;
    }

    public void Dash(float force, float direction)
    {
        rb.AddForce(transform.right * force * direction, ForceMode.Impulse);
        StartCoroutine(Invulnerable());
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
        AudioManager.Instance.PlaySoundBaseOnTarget(gameOverSound, AudioManager.Instance.transform, true);
    }
}
