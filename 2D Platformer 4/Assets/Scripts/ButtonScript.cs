using UnityEngine;
using System.Collections;

public class ButtonScript : MonoBehaviour {

    public float timer = 0.5f;

    public GameObject door;

    private bool pressed = false;
    private Vector2 initialScale;

    private float countdown = 0;

    void Start()
    {
        initialScale = door.transform.localScale;
    }

	// Update is called once per frame
	void LateUpdate () {
        if (pressed)
        {
            transform.localScale = new Vector2(1, 0.5f);
            door.transform.localScale = Vector2.zero;
            countdown += Time.deltaTime;
            if(countdown > timer)
            {
                pressed = false;
            }
        }
        else
        {
            transform.localScale = Vector2.one;
            door.transform.localScale = initialScale;
        }
	}

    public void Press()
    {
        countdown = 0;
        pressed = true;
    }

}
