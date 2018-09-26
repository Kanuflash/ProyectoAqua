using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grow : MonoBehaviour {

    public float secondToRespawn;
    private WaitForSeconds waitToSpawn;
    private bool active;
    private Player player;
    private GenerateBubbles generateBubble;

    private void Start()
    {
        active = true;
        waitToSpawn = new WaitForSeconds(secondToRespawn);
        generateBubble = GetComponentInParent<GenerateBubbles>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && active)
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
            transform.Translate(player.transform.position * Time.deltaTime);
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
        }
    }
}
