using UnityEngine;
using EZCameraShake;

public class Player : MonoBehaviour
{

    [SerializeField]
    private int MaxHealth = 1000;
    private int Health = 1000;

    public float Speed = 5f;
    public float BoostSpeedMult = 1.5f;
    public float TurnSpeed = 10f;
    public float TurnDeg = 0.25f;
    public float BoostSpeed = 2f;

    public Quaternion Rotate;

    private bool Moving = false;
    private bool Boosting = false;
    private bool Turning = false;
    private bool rocketSoundPlaying;


    private Rigidbody2D _rigidbody;
    public Animator animator;
    public AudioManager audio;

    Vector2 movement;

    void Awake() {
        _rigidbody = GetComponent<Rigidbody2D>();
        audio = FindObjectOfType<AudioManager>().GetComponent<AudioManager>();
    }

    void Start()
    {
        Rotate = _rigidbody.transform.rotation;
        // animator = GetComponent<Animator>();

        rocketSoundPlaying = false;

    }

    void Update()
    {
        if (Input.GetButton("Horizontal")) {
            Turning = true;
        } else if (Input.GetButtonUp("Horizontal")) {
            Turning = false;
        }
        if (Input.GetButtonDown("Vertical")) {
            Moving = true;
        } else if (Input.GetButtonUp("Vertical")) {
            Moving = false; 
        }

        if (Input.GetKey(KeyCode.LeftShift) && movement.y > 0) {
            Boosting = true;
        } else {
            Boosting = false;
        }



        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        if (movement.x == 0 && movement.y == 0) {
            animator.SetBool("Idle", true);
        } else {
            animator.SetBool("Idle", false);
        }

        if (movement.x != 0 && !rocketSoundPlaying || movement.y != 0 && !rocketSoundPlaying) {
            PlayRocketSound();
        } else if (movement.x == 0 && movement.y == 0 && rocketSoundPlaying) {
            StopRocketSound();
        }

        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Speed", movement.sqrMagnitude);

        animator.SetBool("boosting", Boosting);
    }

    void FixedUpdate() {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");
        var impulse = ((TurnDeg * -x) * Mathf.Deg2Rad) * _rigidbody.inertia;

        if (Turning) {
            _rigidbody.AddTorque(impulse, ForceMode2D.Impulse);
        }
        if (Moving && !Boosting) {
            _rigidbody.AddForce(transform.up * Speed * y);
        } else if (Moving && Boosting) {
            _rigidbody.AddForce(transform.up * (Speed * BoostSpeedMult) * y);
        }

    }

    public void TakeDamage(int damage) {
        Health -= damage;
        animator.SetTrigger("hit");
        // CameraShaker.Instance.ShakeOnce(4f, 2f, .1f, 1f);

        if (Health <= 0) {
            Die();
        }
    }

    void Die() {
        Debug.Log("Am Ded");
    }

    void PlayRocketSound() {
        audio.Play("Rocket");
        rocketSoundPlaying = true;
    }

    void StopRocketSound() {
        audio.Stop("Rocket");
        rocketSoundPlaying = false;
    }

    public int ReturnHealth() {
        return Health;
    }

}
