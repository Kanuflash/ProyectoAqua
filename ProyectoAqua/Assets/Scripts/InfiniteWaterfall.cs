using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfiniteWaterfall : MonoBehaviour {

    public float scrollSpeed;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		transform.GetComponent<Renderer>().sharedMaterial.mainTextureOffset = new Vector2(Time.time* scrollSpeed, 0.0f);
	}
}
