using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour {

    public Transform[] patrolPoints;
    //segundos que tarda en moverse de un punto a otro
    public float secondToMoveToPoint  = 2;

    bool isPatrol;
    int actualPoint;
    // Use this for initialization
	void Start ()
    {
        isPatrol = false;
        actualPoint = -1;
        if (patrolPoints.Length < 2)
        {
            Debug.LogWarning("no hay suficientes puntos para patrullar, me destruyo :(");
            Destroy(this);
        }
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        if (!isPatrol)
        {
            NextPatrolPoint(); 
        }
	}

    void NextPatrolPoint()
    {
        actualPoint++;
        if(actualPoint >= patrolPoints.Length)
        {
            actualPoint = 0;
        }
        isPatrol = true;
        StartCoroutine(PatrolToPoint());
    }

    IEnumerator PatrolToPoint()
    {
        Vector3 initialPosition = transform.position;
        Vector3 finalPosition = patrolPoints[actualPoint].position;
        float timeSinceStarted = 0;
        float percentageComplete = timeSinceStarted / secondToMoveToPoint;

        while (isPatrol)
        {
            yield return new WaitForFixedUpdate();
            timeSinceStarted += Time.fixedDeltaTime;
            percentageComplete = timeSinceStarted / secondToMoveToPoint;
            if (percentageComplete >= 1)
            {
                percentageComplete = 1;
                isPatrol = false;
            }
            transform.position = Vector3.Lerp(initialPosition, finalPosition, percentageComplete);
        }
    }
}
