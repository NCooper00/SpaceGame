using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeapon : MonoBehaviour
{

    public Transform firePointRight;
    public Transform firePointLeft;

    private Transform playerPos;

    private float distance;

    public GameObject bulletPrefab;
    public AudioManager audio;

    public float fireRate = 1f;
    public float agroRange = 10f;
    private float fireRateReset;

    private bool RIGHT;

    
    void Awake() {
        fireRateReset += fireRate;
        audio = FindObjectOfType<AudioManager>().GetComponent<AudioManager>();


        RIGHT = true;

    }

    void FixedUpdate() {
        playerPos = GameObject.Find("PLAYER").GetComponent<Transform>();

        distance = Vector3.Distance(transform.position, playerPos.position);

        fireRate -= Time.deltaTime;

        if (fireRate <= 0 && distance <= agroRange) {
            fireRate = fireRateReset;
            Shoot();
        }
    }

    void Shoot() {
        audio.Play("TurretShot");

        if (RIGHT) {
            Instantiate(bulletPrefab, firePointRight.position, firePointRight.rotation);
            RIGHT = false;
        } else {
            Instantiate(bulletPrefab, firePointLeft.position, firePointLeft.rotation);
            RIGHT = true;
        }
    }
}
