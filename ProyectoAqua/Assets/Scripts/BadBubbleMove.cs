using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BadBubbleMove : MonoBehaviour {
	
	[SerializeField]
	float speed = 2f;
	Rigidbody2D rb ;
	float timeToResetVelocity = 0.5f;
    private Animator animator;

    Player player;
	// Use this for initialization
	void Start () {
		rb = transform.GetComponent<Rigidbody2D>();
		rb.velocity = new Vector2(0, speed);
        animator = GetComponent<Animator>();

	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            
            player = collision.transform.GetComponent<Player>();
            if(player.canBeDamaged)
            {
                player.receiveDmg();
                Vector2 direction = collision.transform.position - transform.position;
                direction *= 6;

                Invoke("DelayResetVelocity", timeToResetVelocity);
                player.modifyFloatVelocity(direction);

                if (animator)
                {
                    animator.SetTrigger("Destruction");
                }
            }
        }
		else if (animator)
        {
            animator.SetTrigger("Destruction");
        }
			
    }
   
    private void DelayResetVelocity()
    {
        player.resetFloatVelocity();
    }

    public void DestroyEventAnimation()
    {
        Destroy(gameObject);
    }
}
