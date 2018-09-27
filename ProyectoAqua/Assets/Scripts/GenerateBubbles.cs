using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateBubbles : MonoBehaviour {
    public float timeToSpawnBubble;
    public GameObject bubbleGrow;

    private WaitForSeconds waitForSound;
    private WaitForSeconds waitForSpawn;

    private void Start()
    {
        float soundBubble = GameManager.instance.burbujaAlga.length;
        
        
        if (bubbleGrow)
        {
            waitForSound = new WaitForSeconds(Mathf.Max(0, timeToSpawnBubble - soundBubble));

            waitForSpawn = new WaitForSeconds(soundBubble);
            Spawn();
        }
        else
        {
            Debug.LogError("No tengo burbujas que crear asi que me destruyo :_(");
        }
        
    }

    public void Spawn()
    {
        StartCoroutine(SpawnCoroutine());
    }

    IEnumerator SpawnCoroutine()
    {
        yield return waitForSound;
        AudioSource.PlayClipAtPoint(GameManager.instance.burbujaAlga, transform.position);
        yield return waitForSpawn;

        Instantiate(bubbleGrow, transform);
    }
}
