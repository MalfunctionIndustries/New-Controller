using UnityEngine;
using System.Collections;

[RequireComponent (typeof(Controller2D))]

public class PlayerFireBall : MonoBehaviour {

    public Vector3 respawnPoint;

    public float jumpHeight = 4;
    public float timeToJumpApex = 0.4f;
    float accelerationTimeAirborne = 0.2f;
    float accelerationTimeGrounded = 0.1f;

    float footstepTimer = 0;

    public float moveSpeed = 8;

    private bool teleporting = false;

    float gravity;
    float jumpVelocity;
    public Vector3 velocity;
    float velocityXSmoothing;

    public FireBall BallScript;

    Controller2D controller;

    public AudioSource MoveAS;
    public AudioSource JumpAS;

    public AudioClip[] footsteps;

    public AudioClip[] jumpSounds;

    private bool noBall = true;

	void Start () 
    {
        Cursor.lockState = CursorLockMode.Confined;

        controller = GetComponent<Controller2D> ();

        gravity = -(2 * jumpHeight) / Mathf.Pow(timeToJumpApex, 2);
        jumpVelocity = Mathf.Abs(gravity) * timeToJumpApex;
	}
	
    
    void Update()
    {
        if (Input.GetButtonDown("Restart"))
        {
            Die();
        }
        
        if (!teleporting)
        {
            if (controller.collisions.above || controller.collisions.below)
            {
                velocity.y = 0;
            }
        }
        else
        {
            teleporting = false;
        }
        

        Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        if (Input.GetButtonDown("Jump") && controller.collisions.below)
        {
            velocity.y = jumpVelocity;
            JumpAS.clip = jumpSounds[Random.Range(0, jumpSounds.Length)];
            JumpAS.Play();
        }

        

        float targetVelocityX = input.x * moveSpeed;
        velocity.x = Mathf.SmoothDamp(velocity.x, targetVelocityX, ref velocityXSmoothing, (controller.collisions.below)?accelerationTimeGrounded:accelerationTimeAirborne);

        if (controller.collisions.below)
            footstepTimer += Mathf.Abs(velocity.x);
        else
            footstepTimer = 149;

        if (footstepTimer >= 150)
        {
            footstepTimer = 0;
            MoveAS.pitch = Random.Range(1.2f, 1.8f);
            MoveAS.clip = footsteps[Random.Range(0, footsteps.Length)];
            MoveAS.Play();
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    private bool justPressed = false;

    void OnTriggerStay2D(Collider2D other)
    {
        if (Input.GetButton("Interact"))
        {
            if (other.tag == "Lever" && justPressed == false)
            {
                other.GetComponent<DoorScript>().toggleDoor();
                justPressed = true;
            }
        }
        else
        {
            justPressed = false;
        }

        if (other.tag == "Button")
        {
            other.GetComponent<ButtonScript>().Press();
        }

        if(other.tag == "Checkpoint")
        {
            respawnPoint = other.gameObject.GetComponent<Checkpoint>().getSpawnPoint();
        }

        if (other.tag == "Deathzone")
        {
            Die();
        }

        if (noBall && other.tag == "FindBall")
        {
            noBall = false;
            BallScript.enabled = true;
        }

    }

    public void Teleport(Vector2 newPosition, Vector3 newVelocity)
    {
        velocity = newVelocity;
        transform.position = newPosition;
        teleporting = true;
    }

    public void Die()
    {
        velocity = Vector3.zero;
        transform.position = respawnPoint;
        BallScript.ResetBall();
    }
}



