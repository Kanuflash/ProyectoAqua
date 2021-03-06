﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public GameObject bubbleCopy;
    private GameObject actualBubbleCopy;
    // Use this for initialization
    public Transform copiaBarata;
    public Transform burbujaBuena;
    public AudioClip divideSound;
    public AudioClip damageSound;
    public enum Life{Big, Small};

	[SerializeField]
	public Life currentLife = Life.Big;

	Rigidbody2D rb;
	Animator animator;	

	[SerializeField]
	float speedInterval = 0.2f;
	[SerializeField]
	float maxBigVerticalSpeed = 1.5f;
	[SerializeField]
	float maxBigHorizontalSpeed = 2f;
	[SerializeField]
	float maxSmallVerticalSpeed = 2.5f;
	[SerializeField]
	float maxSmallHorizontalSpeed = 3f;
    [SerializeField]
    float secondsReturnVelocity = 0.5f;

    [SerializeField]
	int friction = 4;

	public bool canBeDamaged = true;
	Vector2 standarVelocity;
	Vector2 movementVelocity;

	//Velocidad sometida por defecto (corrientes, flotar, etc...)
	[SerializeField]
	Vector2 floatVelocity = new Vector2( 0, 0.5f);

    public AudioClip[] pasos;
    public float timeToSoundPasos =2;
    private float actualTimeToSoundPasos;

    
    void Start () {
		rb = gameObject.GetComponent<Rigidbody2D>();
		animator = gameObject.GetComponent<Animator>();

		movementVelocity = new Vector2(0, 0);
        actualTimeToSoundPasos = timeToSoundPasos;

        resetFloatVelocity();
        DontDestroyOnLoad(gameObject);
    }
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.B))divide();
		//if(Input.GetKeyDown(KeyCode.N))grow();
	}

	void FixedUpdate()
	{
		if(!GameManager.instance.pause)
		{
			standarVelocity = floatVelocity;
			float maxVerticalSpeed = 0, maxHorizontalSpeed = 0;
			switch(currentLife){
				case Life.Big:
					maxVerticalSpeed = maxBigVerticalSpeed;
					maxHorizontalSpeed = maxBigHorizontalSpeed;
				break;
				case Life.Small:
					maxVerticalSpeed = maxSmallVerticalSpeed;
					maxHorizontalSpeed = maxSmallHorizontalSpeed;
				break;
			}

			if(Input.GetAxis("Vertical") > 0) movementVelocity += new Vector2(0, speedInterval);
			else if (Input.GetAxis("Vertical") < 0) movementVelocity -= new Vector2(0, speedInterval);
			else{
				if(movementVelocity.y > 0) movementVelocity -= new Vector2(0, speedInterval/friction);
				else if(movementVelocity.y < 0) movementVelocity += new Vector2(0, speedInterval/friction);
			}

			if(Input.GetAxis("Horizontal") > 0) movementVelocity += new Vector2(speedInterval, 0);
			else if (Input.GetAxis("Horizontal") < 0) movementVelocity -= (new Vector2(speedInterval, 0));
			else{
				if(movementVelocity.x > 0) movementVelocity -= new Vector2(speedInterval/friction, 0);
				else if(movementVelocity.x < 0) movementVelocity += new Vector2(speedInterval/friction, 0);
			}


			if( movementVelocity.y > maxVerticalSpeed ) movementVelocity = new Vector2(movementVelocity.x, maxVerticalSpeed);
			else if( movementVelocity.y < -maxVerticalSpeed ) movementVelocity = new Vector2(movementVelocity.x, -maxVerticalSpeed);

			if( movementVelocity.x > maxHorizontalSpeed ) movementVelocity = new Vector2(maxHorizontalSpeed, movementVelocity.y);
			else if( movementVelocity.x < -maxHorizontalSpeed ) movementVelocity = new Vector2(-maxHorizontalSpeed, movementVelocity.y);

			switch(currentLife){
				case Life.Big:
					rb.velocity = movementVelocity + (standarVelocity *0.25f);
				break;
				case Life.Small:
					rb.velocity = movementVelocity + standarVelocity;
				break;
			}
            if(Input.GetAxis("Vertical") > 0 || Input.GetAxis("Horizontal") > 0)
            {
                actualTimeToSoundPasos -= Time.fixedDeltaTime;
                if (actualTimeToSoundPasos < 0)
                {
                    actualTimeToSoundPasos = timeToSoundPasos;
                    AudioSource.PlayClipAtPoint(pasos[Random.Range(0, pasos.Length)], transform.position,0.5f);
                }
            }
            
		}
		else
		{
			rb.velocity = Vector2.zero;
		}
	}


	void divide(){
		if(currentLife == Life.Big)
		{
            AudioSource.PlayClipAtPoint(divideSound,transform.position);
			animator.SetBool("Divide", true);
			//transform.localScale *= 0.5f;
			currentLife = Life.Small;
		}
	}
	void stopDivide(){
		animator.SetBool("Divide", false);
        actualBubbleCopy = Instantiate(bubbleCopy, copiaBarata);
        actualBubbleCopy.transform.parent = null;
        transform.position = burbujaBuena.position;
	}
    void stopDamage()
    {
        animator.SetBool("DañoRecibido", false);
        canBeDamaged = true;
    }

    public void grow(){
		if(currentLife == Life.Small)
		{
			animator.SetBool("Grow", true);
			//transform.localScale *= 2f;
			currentLife = Life.Big;
            if (actualBubbleCopy)
                actualBubbleCopy.GetComponent<Grow>().DestroyBubble();
		}
	}
	void stopGrow(){
        GameManager.instance.pause = false;
		animator.SetBool("Grow", false);
	}
	void die(){
		GameManager.instance.die();
	}
	public void receiveDmg(){
		if(currentLife == Life.Big)
        {
            AudioSource.PlayClipAtPoint(damageSound, transform.position);
            animator.SetBool("DañoRecibido", true);
			currentLife = Life.Small;
			canBeDamaged = false;
		} 
		else die();
	}

	public void modifyFloatVelocity(Vector2 vel){
		floatVelocity = vel;
	}

	public void resetFloatVelocity(){
        StartCoroutine(resetVelocityCoroutine());
	}

    IEnumerator resetVelocityCoroutine()
    {
        bool continueLoop = true;
        Vector2 initialVelocity = floatVelocity;
        Vector2 finalVelocity = new Vector2(0, 0.4f);
        float timeSinceStarted = 0;
        float percentageComplete ;

        while (continueLoop)
        {
            yield return new WaitForFixedUpdate();
            timeSinceStarted += Time.fixedDeltaTime;
            percentageComplete = timeSinceStarted / secondsReturnVelocity;
            if (percentageComplete >= 1)
            {
                percentageComplete = 1;
                continueLoop = false;
            }
            floatVelocity = Vector2.Lerp(initialVelocity, finalVelocity, percentageComplete);
        }
    }
}
