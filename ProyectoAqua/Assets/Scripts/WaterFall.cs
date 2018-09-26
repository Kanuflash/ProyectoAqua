using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterFall : MonoBehaviour {

    public Vector2 direction;
    
    // Use this for initialization
	void Start () {
        if (direction == Vector2.zero)
        {
            Debug.LogError("No tengo direccion para mover al personaje asi que me borro :_(");
            Destroy(this);
        }
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            //collision.GetComponent<Player>().
        }
    }
}
