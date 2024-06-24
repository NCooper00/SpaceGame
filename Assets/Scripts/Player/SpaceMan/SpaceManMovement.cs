using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceManMovement : MonoBehaviour
{

    public float moveSpeed = 4f;

    private float direction;

    private bool facingRight = false;

    public Rigidbody2D rb;

    public Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate() {
        direction = Input.GetAxis("Horizontal");

        rb.velocity = new Vector2(moveSpeed * direction, rb.velocity.y);

        // if (rb.velocity.x != 0) {
        //     anim.SetBool("Move", true);
        // } else {
        //     anim.SetBool("Move", false);
        // }

        if (facingRight == false && direction > 0) {
            Flip();
        }else if (facingRight == true && direction < 0) {
            Flip();
        }
    }

    void Flip() {

        facingRight = !facingRight;
        transform.Rotate(0f, 180f, 0f);

    }
}
