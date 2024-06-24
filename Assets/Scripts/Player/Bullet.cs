using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    [SerializeField]
    private float bulletLife = 3f;

    public float bulletSpeed = 75f;
    private float bulletLifeReset;
    

    public int bulletDamage = 10;

    public Rigidbody2D rb;
    public Animator anim;

    public AudioManager audio;

    
    void Awake() {
        audio = FindObjectOfType<AudioManager>().GetComponent<AudioManager>();
        bulletLifeReset += bulletLife;
    }

    void Start()
    {
        rb.velocity = transform.up * bulletSpeed;
    }

    void FixedUpdate() {
        bulletLife -= Time.deltaTime;
        if (bulletLife <= 0) {
            DestroyBullet();
            bulletLife += bulletLifeReset;
        }
    }

    void OnTriggerEnter2D(Collider2D hitInfo) {
        Enemy enemy = hitInfo.GetComponent<Enemy>();

        if (enemy != null) {
            enemy.TakeDamage(bulletDamage);
        }
        
        if (hitInfo.gameObject.layer != 6) {
            anim.SetBool("hit", true);
            rb.velocity = new Vector2(0f, 0f);
            audio.PlayFull("TurretShotImpact");
            bulletDamage = bulletDamage / 2;
        }

    }

    public void DestroyBullet() {
        Destroy(gameObject);
    }
}
