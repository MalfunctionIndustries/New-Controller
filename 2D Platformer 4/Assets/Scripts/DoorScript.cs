using UnityEngine;
using System.Collections;

public class DoorScript : MonoBehaviour {

    public GameObject door;
    public Sprite leverOff, leverOn;

    public SpriteRenderer lever;

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
            lever.sprite = leverOn;
            open = true;
            door.transform.localScale = Vector2.zero;
        }
        else
        {
            lever.sprite = leverOff;
            open = false;
            door.transform.localScale = initialScale;
        }
	}
}
