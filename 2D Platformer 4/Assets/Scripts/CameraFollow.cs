using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {

    public Transform player;

    public Transform background1;
    public Transform background2;
    public Transform background3;
   // public Transform background4;
   // public Transform background5;

	void Update () {


        Vector3 midPoint = player.position + (Camera.main.ScreenToWorldPoint(Input.mousePosition) - player.position) / 4;

        Vector3 targetPos = new Vector3(midPoint.x, midPoint.y, -10);

        transform.position = Vector3.Slerp(transform.position, targetPos, Time.deltaTime * 5);

        background1.position = new Vector3(transform.position.x, transform.position.y, 30);

        background2.position = new Vector3(transform.position.x / 4, transform.position.y/2 - 5, 25);
        background3.position = new Vector3(transform.position.x / 8, transform.position.y/4 - 8, 20);
        //background4.position = new Vector3(transform.position.x / 6, transform.position.y, 15);
       // background5.position = new Vector3(transform.position.x / 12, -12, 10);
	}
}
