using UnityEngine;
using System.Collections;

public class DoorScript : MonoBehaviour {

    public GameObject door;

    private bool open = false;
    private Vector2 initialScale;

    void Start()
    {
        initialScale = door.transform.localScale;
    }

	// Update is called once per frame
	public void toggleDoor () {

        if(!open)
        {
            open = true;
            Debug.Log("OPENED");
            door.transform.localScale = Vector2.zero;
        }
        else
        {
            open = false;
            Debug.Log("CLOSED");
            door.transform.localScale = initialScale;
        }
	}
}
