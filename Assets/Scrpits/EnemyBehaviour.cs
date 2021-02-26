using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    Rigidbody2D enemyRgbd;
    Animator enemyAnimator;
    SpriteRenderer spriteEnemy;
    
    public float delay = 200f;
    public float jumpForce = 300f;
    int direction = 0;


    float timer = 0;

    
    void Start()
    {
        enemyRgbd = GetComponent<Rigidbody2D>();
        enemyAnimator = GetComponent<Animator>();
        spriteEnemy = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {

        //OTRO TAIMER 
        /*
        if (timeBeforeChangue < Time.time)
        {
            enemyRgbd.AddForce(new Vector2(direction,1)*jumpForce);

            

            timeBeforeChangue = Time.time + delay;
        }
        */
        if (GameManager.sharedInstance.currentGameState == GameState.inGame) {
            timer = timer + Time.deltaTime;


            if (timer > 2.0000000f)
            {
                enemyRgbd.AddForce(new Vector2(direction, 2) * jumpForce);
                direction = Random.Range(-2, 2);
                timer = 0;
            }

        }else if(GameManager.sharedInstance.currentGameState == GameState.menu){
            enemyRgbd.velocity = Vector2.zero;
            
        
        }
        

        ComprobarVelocidadY();
        ComprobarVelocidadX();


    }

    private void ComprobarVelocidadY() {
        if (enemyRgbd.velocity.y > 0)
        {
            enemyAnimator.SetInteger("velocityY", 1);
        }
        else if (enemyRgbd.velocity.y < 0)
        {
            enemyAnimator.SetInteger("velocityY", -1);
        }
        else {
            enemyAnimator.SetInteger("velocityY", 0);
        }
    
    }

    void ComprobarVelocidadX() {
        if (enemyRgbd.velocity.x < 0) {
            spriteEnemy.flipX = false;
        } else if (enemyRgbd.velocity.x > 0) {
            spriteEnemy.flipX = true;
        }
    }

}
