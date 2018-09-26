using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DephSpriteMap : MonoBehaviour {

	// Use this for initialization
	void Start () {
        int i= 1;
        foreach( SpriteRenderer sprite in GetComponentsInChildren<SpriteRenderer>())
        {
            sprite.sortingOrder = ++i;
        }

	}
}
