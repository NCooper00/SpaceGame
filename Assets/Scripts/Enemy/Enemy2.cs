using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2 : MonoBehaviour
{
    // public Transform player; // Reference to the player's transform
    // public float followRange = 20f; // Maximum distance to follow the player
    // public float minDistance = 5f; // Minimum distance to keep from the player
    // public float maxDistance = 15f; // Maximum distance to keep from the player
    // public float wanderSpeed = 2f; // Speed of wandering movement
    // public float followSpeed = 4f; // Speed of following the player
    // public float wanderRadius = 10f; // Radius within which the creature wanders

    public Animator anim;
    private Rigidbody2D rb;

    [SerializeField]
    private GameObject player;

    private Transform playerPos;

    // public Transform player; // Reference to the player's transform
    public float followRange = 20f; // Maximum distance to follow the player
    public float maxDistance = 15f; // Maximum distance to keep from the player
    public float wanderSpeed = 2f; // Speed of wandering movement
    public float followSpeed = 4f; // Speed of following the player
    public float wanderRadius = 10f; // Radius within which the creature wanders
    public float smoothTime = 0.3f; // Smooth time for floaty movement

    private Vector2 wanderTarget;
    private Vector2 velocity = Vector3.zero;

    // private Vector3 wanderTarget;

    void Awake()
    {
        player = GameObject.Find("PLAYER");

        rb = GetComponent<Rigidbody2D>();
        playerPos = player.GetComponent<Transform>();
    }



    void Start()
    {
        // Initialize the wander target
        wanderTarget = transform.position;
    }

    void Update()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);

        if (distanceToPlayer < followRange)
        {
            if (distanceToPlayer > maxDistance)
            {
                // Follow the player
                FollowPlayer();
            }
            else
            {
                // Wander around the player
                WanderAroundPlayer();
            }
        }
        else
        {
            // Wander around its current position
            Wander();
        }
    }

    void FollowPlayer()
    {
        Vector2 direction = (player.transform.position - transform.position).normalized;
        Vector2 targetPosition = (Vector2)transform.position + direction * followSpeed * Time.deltaTime;
        transform.position = Vector2.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
        RotateTowards(direction);
    }

    void WanderAroundPlayer()
    {
        if (Vector2.Distance(transform.position, wanderTarget) < 1f)
        {
            wanderTarget = (Vector2)player.transform.position + Random.insideUnitCircle * wanderRadius;
        }

        Vector2 direction = (wanderTarget - (Vector2)transform.position).normalized;
        Vector2 targetPosition = (Vector2)transform.position + direction * wanderSpeed * Time.deltaTime;
        transform.position = Vector2.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
        RotateTowards(direction);
    }

    void Wander()
    {
        if (Vector2.Distance(transform.position, wanderTarget) < 1f)
        {
            wanderTarget = (Vector2)transform.position + Random.insideUnitCircle * wanderRadius;
        }

        Vector2 direction = (wanderTarget - (Vector2)transform.position).normalized;
        Vector2 targetPosition = (Vector2)transform.position + direction * wanderSpeed * Time.deltaTime;
        transform.position = Vector2.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
        RotateTowards(direction);
    }

    void RotateTowards(Vector2 direction)
    {
        if (direction != Vector2.zero)
        {
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, 0, angle), Time.deltaTime * followSpeed);
        }
    }
}


    // void Start()
    // {
    //     // Initialize the wander target
    //     wanderTarget = transform.position;
    // }

    // void Update()
    // {
    //     float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);

    //     if (distanceToPlayer < followRange)
    //     {
    //         if (distanceToPlayer > maxDistance)
    //         {
    //             // Follow the player
    //             FollowPlayer();
    //         }
    //         else if (distanceToPlayer < minDistance)
    //         {
    //             // Move away from the player
    //             MoveAwayFromPlayer();
    //         }
    //         else
    //         {
    //             // Wander around the player
    //             WanderAroundPlayer();
    //         }
    //     }
    //     else
    //     {
    //         // Wander around its current position
    //         Wander();
    //     }
    // }

    // void FollowPlayer()
    // {
    //     Vector3 direction = (player.transform.position - transform.position).normalized;
    //     transform.position += direction * followSpeed * Time.deltaTime;
    // }

    // void MoveAwayFromPlayer()
    // {
    //     Vector3 direction = (transform.position - player.transform.position).normalized;
    //     transform.position += direction * followSpeed * Time.deltaTime;
    // }

    // void WanderAroundPlayer()
    // {
    //     if (Vector3.Distance(transform.position, wanderTarget) < 1f)
    //     {
    //         wanderTarget = player.transform.position + Random.insideUnitSphere * wanderRadius;
    //     }

    //     Vector3 direction = (wanderTarget - transform.position).normalized;
    //     transform.position += direction * wanderSpeed * Time.deltaTime;
    // }

    // void Wander()
    // {
    //     if (Vector3.Distance(transform.position, wanderTarget) < 1f)
    //     {
    //         wanderTarget = transform.position + Random.insideUnitSphere * wanderRadius;
    //     }

    //     Vector3 direction = (wanderTarget - transform.position).normalized;
    //     transform.position += direction * wanderSpeed * Time.deltaTime;
    // }
// }