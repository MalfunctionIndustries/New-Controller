using UnityEngine;
using System.Collections;

public class CameraFollow2 : MonoBehaviour {

    public Transform player;

	void Update () {


        Vector3 midPoint = player.position + (Camera.main.ScreenToWorldPoint(Input.mousePosition) - player.position) / 4;

        Vector3 targetPos = new Vector3(midPoint.x, midPoint.y, -10);

        transform.position = Vector3.Slerp(transform.position, targetPos, Time.deltaTime * 5);
	}
}
