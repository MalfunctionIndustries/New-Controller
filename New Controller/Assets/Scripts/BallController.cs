using UnityEngine;
using System.Collections;

public class BallController : MonoBehaviour {

	public GameObject player;
	private Rigidbody2D rigidbody;

	bool thrown = false;

	// Use this for initialization
	void Start () {
		Physics2D.IgnoreCollision (player.GetComponent<Collider2D>(), GetComponent<Collider2D> ());
		rigidbody = GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (!thrown) {
			transform.position = player.transform.position;

			if(Input.GetButtonDown("Fire1"))
			{
				thrown = true;

				Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);

				Vector3 mousePos = mouseRay.GetPoint(-Camera.main.transform.position.z);


				rigidbody.isKinematic = false;
				rigidbody.velocity = Vector3.zero;
				rigidbody.AddForce((mousePos - transform.position)*100);

			}

		} else {
			if(Input.GetButtonDown("Fire1"))
			{
				rigidbody.isKinematic = true;
				thrown = false;
			}
			if(Input.GetButtonDown("Fire2"))
			{
				player.GetComponent<Player>().velocity = rigidbody.velocity * 5;

				rigidbody.isKinematic = true;
				thrown = false;

				// TELEPORT
				RaycastHit2D hit = Physics2D.Raycast(transform.position, -Vector2.up, 1);

				if(hit)
				{
					player.transform.position = new Vector2(hit.point.x, hit.point.y + 1);
				} else {
					player.transform.position = transform.position;
				}

			}
		}
	}
}
