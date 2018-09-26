using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour {

    public Transform playerTransform;
    public float leftBound;
    public float rightBound;
    public float topBound;
    public float botBound;

    // Use this for initialization
    void Start () {
		if(playerTransform == null)
        {
            playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        }
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 position = new Vector3(playerTransform.position.x, playerTransform.position.y, transform.position.z);

        position.x = Mathf.Clamp(playerTransform.position.x, leftBound, rightBound);
        position.y = Mathf.Clamp(playerTransform.position.y, botBound, topBound);

        transform.position = position;
    }
}
