using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bashable : MonoBehaviour {

	// Use this for initialization
	void Start ()
    {
        if (gameObject.GetComponent<CircleCollider2D>() == null)
        {
            CircleCollider2D myCollider = gameObject.AddComponent<CircleCollider2D>();
            myCollider.radius = gameObject.GetComponent<SpriteRenderer> ().bounds.size.x / 2f;
        }
	}
}
