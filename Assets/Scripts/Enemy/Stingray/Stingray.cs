using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Animator anim;
    private Rigidbody2D rb;

    [SerializeField]
    private GameObject player;

    public int Health = 100;
    public int Damage = 15;
    public float Speed = 0.15f;
    public float maxFollowRange = 15f;

    public float offset = 0f;

    private Transform playerPos;

    private float enemyRot;

    // Start is called before the first frame update
    void Awake()
    {
        player = GameObject.Find("PLAYER");

        rb = GetComponent<Rigidbody2D>();
        playerPos = player.GetComponent<Transform>();
    }

    private void Start() {
        anim.SetBool("flying", true);
    }

    private void FixedUpdate() {
        Vector3 difference = playerPos.position - rb.transform.position;
        difference.Normalize();

        float distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);

        Debug.Log(distanceToPlayer);

        if (distanceToPlayer > 8f) {
            // Apply an impulse force towards the player
            rb.AddForce(difference * Speed, ForceMode2D.Impulse);
        } else if (distanceToPlayer < 4f) {
            // Apply an impulse force away from the player
            rb.AddForce(-difference * Speed, ForceMode2D.Impulse);
        }
            // No force applied if within a neutral range

        // if (distanceToPlayer > 8f) {
        //     rb.AddForce(transform.up * Speed);  
        // } else if (distanceToPlayer < 4f) {
        //     rb.AddForce(-transform.up * Speed);
        // } else {
        //     rb.AddForce(transform. * 0);
        // }

        enemyRot = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        rb.transform.rotation = Quaternion.Euler(0f, 0f, enemyRot + offset);


    }

    public void TakeDamage(int damage) {
        Health -= damage;
        anim.SetTrigger("hit");

        if (Health <= 0) {
            Die();
        }
    }

    void Die() {
        anim.SetBool("dead", true);
        anim.SetBool("flying", false);
    }

    public void DestroyObject() {
        Destroy(gameObject);
    }
}
