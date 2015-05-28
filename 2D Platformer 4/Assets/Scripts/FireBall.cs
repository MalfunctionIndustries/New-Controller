using UnityEngine;
using System.Collections;

public class FireBall : MonoBehaviour
{

    public Transform player;
    public float throwForce;

    public PlayerFireBall playerController;

    private Rigidbody2D rigidbody;
    private bool thrown = false;

    public ParticleSystem explosion;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {

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

            }
        }
        else
        {

            //rigidbody.isKinematic = false;
            if (Input.GetButtonDown("Fire1"))
            {
                // return ball to player
                thrown = false;

            }
            if (Input.GetButtonDown("Fire2"))
            {
                explosion.Play();
                
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

    public void ResetBall()
    {
        thrown = false;
    }

}
