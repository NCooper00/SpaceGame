using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{

    [SerializeField]
    private float bulletLife = 3f;

    public float bulletSpeed = 75f;
    private float bulletLifeReset;
    

    public int bulletDamage = 10;

    public Rigidbody2D rb;
    public Animator anim;

    public AudioManager targetAudio;

    
    void Awake() {
        targetAudio = FindObjectOfType<AudioManager>().GetComponent<AudioManager>();
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
        Player player = hitInfo.GetComponent<Player>();

        if (player != null) {
            player.TakeDamage(bulletDamage);
        }
        
        if (hitInfo.gameObject.layer != 7) {
            anim.SetBool("hit", true);
            rb.velocity = new Vector2(0f, 0f);
            targetAudio.PlayFull("TurretShotImpact");
            bulletDamage = bulletDamage / 2;
        }

    }

    public void DestroyBullet() {
        Destroy(gameObject);
    }
}