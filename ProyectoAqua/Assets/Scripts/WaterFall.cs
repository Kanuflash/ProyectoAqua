using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterFall : MonoBehaviour {

    public Vector2 direction;
    public float timeToResetVelocity = 0.5f;
    private Player player;
    
    // Use this for initialization
	void Start () {
        if (direction == Vector2.zero)
        {
            Debug.LogError("No tengo direccion para mover al personaje asi que me borro :_(");
            Destroy(this);
        }
	}

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (player == null)
            {
                player = collision.GetComponent<Player>();
            }
            player.modifyFloatVelocity(direction);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        
        if (collision.tag == "Player")
        {
            Invoke("DelayResetVelocity", timeToResetVelocity);
            player.modifyFloatVelocity(direction);
        }
    }

    private void DelayResetVelocity()
    {
        player.resetFloatVelocity();
    }
}
