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

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player" && Input.GetKeyDown(KeyCode.N))
        {
            player = collision.GetComponent<Player>();
            GameManager.instance.pause = true;
            StartCoroutine(AnimationGrow());
                    
        }
    }

    private IEnumerator AnimationGrow()
    {
        bool continueLoop = true;
        while (continueLoop)
        {
            if(transform.position== player.transform.position)
            {
                continueLoop = false;
            }
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position,speed * Time.deltaTime);
            yield return new WaitForFixedUpdate();
        }

        player.grow();
        ResetGrow();       
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
