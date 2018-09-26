using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour {

    public Transform playerTransform;
    public float leftBound;
    public float rightBound;
    public float topBound;
    public float botBound;
    public float smoothTimeX;
    public float smoothTimeY;

    private Vector2 velocity = Vector2.zero;

    // Use this for initialization
    void Start () {
		if(playerTransform == null)
        {
            playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        }
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        Vector3 position = new Vector3(transform.position.x, transform.position.y, transform.position.z);

        position.x = Mathf.SmoothDamp(position.x, playerTransform.position.x, ref velocity.x, smoothTimeX );
        position.y = Mathf.SmoothDamp(position.y, playerTransform.position.y, ref velocity.y, smoothTimeY);
        position.x = Mathf.Clamp(position.x, leftBound, rightBound);
        position.y = Mathf.Clamp(position.y, botBound, topBound);

        transform.position = position;
    }
}
