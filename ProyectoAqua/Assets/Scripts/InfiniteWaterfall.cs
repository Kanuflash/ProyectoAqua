using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfiniteWaterfall : MonoBehaviour {

    public float scrollSpeed;
    private Vector2 savedOffset;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		transform.GetComponent<Renderer>().material.SetTextureOffset("_MainTex", new Vector2(Time.time, 0.0f));
	}
}
