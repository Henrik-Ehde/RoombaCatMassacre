using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Cat : MonoBehaviour
{
    public Transform cat;
    public Roomba roomba;
    public float rotationSpeed;
    public float smoothRotation;
    float rotation;

    public Transform muzzle;
    public GameObject projectile;

    public float rateOfFire;
    public float projectileSpeed;
    public float projectileDamage;
    float nextShotTime;

    public float maxAmmo;
    float ammo;
    public float ammoRechargeRate;
    public float ammoVacuumRecharge;

    int currentPlayer=1;
    public float minSwapTime;
    public float maxSwapTime;

    float kills = 0;
    public TMP_Text killsText;

    [Header("Dash")]
    public float dashForce;
    public float dashCooldown;
    float dashReadyTime;
    bool spraying = false;

    public SoundContainer fireSounds;
    public BasicSound swapAnnounceSound, ramboFireSound, vaccuumReadySound, dashSound, suckSound;

    // Start is called before the first frame update
    void Start()
    {
        ammo = maxAmmo;
        StartCoroutine(PlayerSwapping());
    }

    IEnumerator PlayerSwapping()
    {
        if (Random.value < 0.5) SwapPlayer();
        
        while (true)
        {
            float sfxTime = Random.Range(minSwapTime, maxSwapTime) - 3;
            yield return new WaitForSeconds(sfxTime);

            AudioManager.Instance.PlaySoundBaseOnTarget(swapAnnounceSound, AudioManager.Instance.transform, true);

            yield return new WaitForSeconds(3);

            SwapPlayer();
        }
    }

    void SwapPlayer()
    {
        roomba.SwapPlayer();

        currentPlayer++;
        currentPlayer %= 2;
    }

    // Update is called once per frame
    void FixedUpdate()
    {


        if (!spraying)
        {
            float rotationDirection = Input.GetAxis("Horizontal" + currentPlayer);
            rotation = Mathf.Lerp(rotation, rotationSpeed * rotationDirection, smoothRotation * Time.deltaTime);
            cat.Rotate(0, rotation * Time.fixedDeltaTime, 0);

            ammo += ammoRechargeRate * Time.fixedDeltaTime;
            if (ammo > maxAmmo) ammo = maxAmmo;

            if (Input.GetAxis("Vertical" + currentPlayer) != 0 && Time.time > nextShotTime && ammo > 1)
            {
                Fire();
                ammo--;
            }
        }


        if (Input.GetAxis("Special" + currentPlayer) != 0 && Time.time > dashReadyTime)
        {
            Dash();
        }
    }

    void Dash()
    {
        AudioManager.Instance.PlaySoundBaseOnTarget(dashSound, transform, true);

        float direction = Input.GetAxis("Special" + currentPlayer);
        roomba.Dash(dashForce, direction);
        dashReadyTime = Time.time + dashCooldown;
        StartCoroutine(DashCooldown());

    }

    IEnumerator DashCooldown()
    {
        yield return new WaitForSeconds(dashCooldown);
        AudioManager.Instance.PlaySoundBaseOnTarget(vaccuumReadySound, AudioManager.Instance.transform, true);
    }

    public IEnumerator Spray(float duration, int shots, float spread, float direction)
    {
        AudioManager.Instance.PlaySoundBaseOnTarget(ramboFireSound, transform, true);

        spraying = true;
        float t = 0;
        float nextShotTime = 0;

        while (t<duration)
        {
            cat.Rotate(0, direction * spread * Time.deltaTime / duration , 0);

            if (t > nextShotTime)
            {
                Fire();
                nextShotTime += duration / shots;
            }

            t += Time.deltaTime;
            yield return null;
        }

        spraying = false;
    }

    public void VacuumRecharge()
    {
        ammo += ammoVacuumRecharge;

        kills++;
        killsText.text = kills + " dust rats murdered";

        AudioManager.Instance.PlaySoundBaseOnTarget(suckSound, transform, true);

    }

    private void Fire()
    {
        
        nextShotTime = Time.time + 1 / rateOfFire;
        GameObject newProjectile = Instantiate(projectile, muzzle.position,transform.rotation);
        newProjectile.transform.forward = muzzle.up;
        Bullet bullet = newProjectile.GetComponent<Bullet>();
        bullet.speed = projectileSpeed;
        bullet.damage = projectileDamage;

        AudioManager.Instance.PlaySoundBaseOnTarget(fireSounds, transform, true);

    }
}
