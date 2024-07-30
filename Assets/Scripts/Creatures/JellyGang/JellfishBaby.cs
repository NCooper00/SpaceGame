using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JellfishBaby : MonoBehaviour
{

    private Transform target;

    public float thresholdDistance = 6f; // Set your desired threshold distance
    public float minimumDistance = 3f; // Set your desired threshold distance
    public float moveSpeed = 5.0f; // Set your desired movement speed

    // public float floatAmplitude = 0.5f; // Amplitude of the floaty movement
    // public float floatFrequency = 1.0f; // Frequency of the floaty movement

    private Rigidbody2D rb;

    public float moveRate = 0.5f;
    private float moveRateReset;

    [SerializeField] private float rotationSpeed = 1.5f;

    public Animator animator;



    // Awake is called before the first frame update
    void Awake()
    {

        target = GameObject.Find("JellyfishCore").transform;

        animator.SetBool("tooFar", false);
        animator.SetBool("justRight", true);

        moveRateReset += moveRate;
        rb = GetComponent<Rigidbody2D>();
        // Find the JellyfishCore GameObject
        GameObject jellyfishCore = GameObject.Find("JellyfishCore");

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (target != null && rb != null)
        {
            // Calculate the distance between the current object and the target
            float distance = Vector3.Distance(transform.position, target.position);

            // Check if the distance exceeds the threshold
            if (distance > thresholdDistance)
            {

                var direction = target.position - transform.position;

                transform.up = Vector3.MoveTowards(transform.up, direction, rotationSpeed * Time.deltaTime);

                animator.SetBool("tooFar", true);
                animator.SetBool("tooClose", false);
                animator.SetBool("justRight", false);

            }
            else if (distance < minimumDistance) 
            {
            
                var directionAway = transform.position - target.position;

                // transform.up = Vector3.MoveTowards(transform.up, -direction, rotationSpeed * Time.deltaTime);
                transform.up = Vector3.MoveTowards(transform.up, directionAway, rotationSpeed * Time.deltaTime);


                animator.SetBool("tooFar", false);
                animator.SetBool("tooClose", true);
                animator.SetBool("justRight", false);

            }
            else 
            {

                transform.up = Vector3.MoveTowards(transform.up, Vector3.up, rotationSpeed * Time.deltaTime);
                animator.SetBool("tooFar", false);
                animator.SetBool("tooClose", false);
                animator.SetBool("justRight", true);

            }

        }
    }

    // Impulse towards the target
    public void MoveTowardsTarget()
    {
        moveRate = moveRateReset;
        if (target != null && rb != null)
        {
            // Calculate the direction towards the target
            // Vector3 direction = (target.position - transform.position).normalized;

            // Apply an impulse to move back towards the target
            rb.AddForce(transform.up * moveSpeed, ForceMode2D.Impulse);
        }
        Debug.Log(moveRate);
    }
}
