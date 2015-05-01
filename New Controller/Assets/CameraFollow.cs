using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {

	public Transform target;

	public Transform farBG;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		Vector3 targetPos = new Vector3(target.position.x, target.position.y, transform.position.z);
		Vector3 bgPos = new Vector3(transform.position.x, transform.position.y, farBG.position.z);

		transform.position = targetPos;
		farBG.position = bgPos;

	}
}
