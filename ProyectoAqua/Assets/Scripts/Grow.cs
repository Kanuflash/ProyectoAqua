using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grow : MonoBehaviour {

    public float speed =1;
    private WaitForSeconds waitToSpawn;
    private bool active;
    private Player player;
    private GenerateBubbles generateBubble;

    private void Start()
    {
        generateBubble = GetComponentInParent<GenerateBubbles>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(player == null && collision.tag == "Player" )
            player = collision.GetComponent<Player>();
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player" && Input.GetKeyDown(KeyCode.N) && player.currentLife == Player.Life.Small)
        {          
            GameManager.instance.pause = true;
            StartCoroutine(AnimationGrow());                  
        }
    }

    private IEnumerator AnimationGrow()
    {
        bool continueLoop = true;
        while (continueLoop)
        {
            yield return new WaitForFixedUpdate();
            if(transform.position.x == player.transform.position.x && player.transform.position.y == transform.position.y)
            {
                continueLoop = false;
            }
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position,speed * Time.deltaTime);
            
        }

        player.grow();
        ResetGrow();    
        Destroy(gameObject);   
    }

    private void ResetGrow()
    {
        if (generateBubble)
        {
            generateBubble.Spawn();
            Destroy(gameObject);
        }
    }
}
