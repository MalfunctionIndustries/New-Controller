using UnityEngine;
using System.Collections;

public class Ball : MonoBehaviour {
    
    public Transform player;
    public LayerMask raycastMask;
    public float throwForce;

    public Player playerController;

    private Rigidbody2D rigidbody;
    private bool thrown = false;

    public AudioSource ThrowAS;
    public AudioSource TeleAS;
    public AudioSource CollideAS;

    public AudioClip[] throwBall;
    public AudioClip[] returnBall;
    public AudioClip[] teleport;
    public AudioClip[] collide;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

	void Update () {

        if (!thrown)
        {
            transform.position = player.position;
            rigidbody.isKinematic = true;
            //rigidbody.gravityScale = 0;
            if (Input.GetButtonDown("Fire1"))
            {
                // throw ball
                thrown = true;
                rigidbody.isKinematic = false;

                Vector2 directionFromPlayer = Camera.main.ScreenToWorldPoint(Input.mousePosition) - player.position;

                directionFromPlayer = directionFromPlayer / directionFromPlayer.magnitude;
                rigidbody.AddForce(directionFromPlayer * throwForce);

                ThrowAS.clip = throwBall[Random.Range(0, throwBall.Length)];
                ThrowAS.Play();

            }
        }
        else
        {

            //rigidbody.isKinematic = false;
            if (Input.GetButtonDown("Fire1"))
            {
                // return ball to player
                thrown = false;

                ThrowAS.clip = returnBall[Random.Range(0, throwBall.Length)];
                ThrowAS.Play();
            }
            if (Input.GetButtonDown("Fire2"))
            {
                // teleport to ball
                thrown = false;

                TeleAS.clip = teleport[Random.Range(0, throwBall.Length)];
                TeleAS.Play();

                Vector2 bottomPoint;
                Vector2 topPoint;

                bool stuffBelow = false;
                bool stuffAbove = false;
                // cast a ray down to check if there's space underneath the ball for the plare
                RaycastHit2D hitUnder = Physics2D.Raycast(transform.position, -Vector2.up, 1f, raycastMask);
                if (hitUnder)
                {
                    bottomPoint = hitUnder.point;
                    stuffBelow = true;
                }
                else
                {
                    bottomPoint = transform.position - Vector3.up;
                }

                // cast a ray up to check if there's space above
                RaycastHit2D hitAbove = Physics2D.Raycast(transform.position, Vector2.up, 1f, raycastMask);
                if (hitAbove)
                {
                    topPoint = hitAbove.point;
                    stuffAbove = true;
                }
                else
                {
                    topPoint = transform.position + Vector3.up;
                }

                if(stuffAbove && stuffBelow)
                {
                    // can't teleport
                } else if (stuffBelow)
                {
                    playerController.Teleport(bottomPoint + Vector2.up, rigidbody.velocity * 2);
                    //player.position = bottomPoint + Vector2.up;
                } else if(stuffAbove)
                {
                    playerController.Teleport(bottomPoint - Vector2.up, rigidbody.velocity * 2);
                    //player.position = topPoint - Vector2.up;
                }
                else
                {
                    playerController.Teleport(transform.position, rigidbody.velocity * 2);
                    //player.position = transform.position;
                }
            }
        }
	}

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Button")
        {
            other.GetComponent<ButtonScript>().Press();
        }
    }

    void OnCollisionEnter2D()
    {
        CollideAS.clip = collide[Random.Range(0, collide.Length)];
        CollideAS.Play();
    }

    public void ResetBall()
    {
        thrown = false;
    }

}
