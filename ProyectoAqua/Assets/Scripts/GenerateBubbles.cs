using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateBubbles : MonoBehaviour {
    public float timeToSpawnBubble;
    public GameObject bubbleGrow;

    private WaitForSeconds waitForSpawn;

    private void Start()
    {
        if (bubbleGrow)
        {
            waitForSpawn = new WaitForSeconds(timeToSpawnBubble);
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
        yield return waitForSpawn;

        Instantiate(bubbleGrow, transform);
    }
}
