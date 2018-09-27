using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour {

	// Use this for initialization
	[SerializeField]
	AudioClip audio;
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void OnTriggerEnter2D(Collider2D other) {
		if(other.transform.tag == "BubbleCopyTag"){
			GameManager.instance.collect();
			AudioSource.PlayClipAtPoint(audio, transform.position);
			other.GetComponent<Grow>().DestroyBubble();
			Destroy(gameObject);
		}
	}
}
