using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageByContact : MonoBehaviour {

    float timeToResetVelocity = 0.5f;
    private Animator animator;

    Player player;

    private void Start()
    {
        animator = GetComponent<Animator>();
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
