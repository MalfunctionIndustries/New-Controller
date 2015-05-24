using UnityEngine;
using System.Collections;

public class Ball : MonoBehaviour {
    
    public Transform player;
    public LayerMask raycastMask;
    public float throwForce;

    public Player playerController;

    private Rigidbody2D rigidbody;
    private bool thrown = false;
    
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

	void LateUpdate () {
        if (!thrown)
        {
            transform.position = player.position;
            rigidbody.isKinematic = true;
            //rigidbody.gravityScale = 0;
            if (Input.GetMouseButtonDown(0))
            {
                // throw ball
                thrown = true;
                rigidbody.isKinematic = false;

                Vector2 directionFromPlayer = Camera.main.ScreenToWorldPoint(Input.mousePosition) - player.position;

                directionFromPlayer = directionFromPlayer / directionFromPlayer.magnitude;
                rigidbody.AddForce(directionFromPlayer * throwForce);
            }
        }
        else
        {
            //rigidbody.isKinematic = false;
            if (Input.GetMouseButtonDown(0))
            {
                // return ball to player
                thrown = false;
            }
            if (Input.GetMouseButtonDown(1))
            {
                // teleport to ball
                thrown = false;

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
}
