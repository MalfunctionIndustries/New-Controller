using UnityEngine;
using System.Collections;

public class IgnoreCollision : MonoBehaviour {

    public Collider2D[] IgnoreList;

	// Use this for initialization
	void Start () {

        for(int i = 0; i<IgnoreList.Length; i++)
        {
            Physics2D.IgnoreCollision(GetComponent<Collider2D>(), IgnoreList[i]);
        }

	}

}
