using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 0.5f;
    public Rigidbody2D playerRigidbody;
    public float jumpForce = 300f;

    private Animator playerAnimator;
    private SpriteRenderer playerSprite;

    //creando el layer del suelo
    public LayerMask groundLayer;

    Vector3 startPosition;



    int saltos;

    public static PlayerController sharedInstanceController;


    private void Awake()
    {
        if (sharedInstanceController == null) {
            sharedInstanceController = this;
        }
    }



    //FUNCIÓN START
    void Start()
    {
        playerRigidbody = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
        playerSprite = GetComponent<SpriteRenderer>();

        startPosition = this.transform.position;
        saltos = 2;
    }

    void StartGame() {
        this.transform.position = startPosition;
        playerRigidbody.velocity = Vector2.zero;
    }


    //FUNCIÓN UPDATE
    void Update()
    {

        if (GameManager.sharedInstance.currentGameState == GameState.inGame ) {

            Physics2D.gravity = new Vector2(0, -9.81f);
            //MOVIMIENTO - Siempre estára constante en ver si se mueve 
            playerRigidbody.velocity = new Vector2(Input.GetAxis("Horizontal") * speed, playerRigidbody.velocity.y);
            
            //SALTO
            if (Input.GetButtonDown("Jump") && saltos > 0)
            {
                Jump();
                saltos--;
            }

            
            //ANIMACION DE MOVIMIENTO
            ComprobarCaminado();
            //ANIMACIÓN DE AGACHAR
            ComprobarAgachado();
            //ANIMACIÓN DE SALTO - VELOCIDAD EN 
            ComprobarSalto();
            
        } else if (GameManager.sharedInstance.currentGameState == GameState.menu || GameManager.sharedInstance.currentGameState == GameState.gameOver) {
            playerRigidbody.velocity = Vector2.zero;
            Physics2D.gravity = Vector2.zero;
            playerAnimator.SetBool("IsOnTheGround", true);

        }

        
       
       

        //DEBUGGERS
        //1.RAYCAST
        //2.VELOCIDAD EN Y DEL PERSONAJE
        Debug.DrawRay(this.transform.position, Vector2.down * 0.5f, Color.red);
       
        //Debug.Log(playerRigidbody.velocity);
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy")) {

            Die();
        }
    }

    public void Die() {
        playerAnimator.SetBool("isDead", true);
        GameManager.sharedInstance.GameOver();
    }


    private void ComprobarSalto() {

        //Velocidad en y
        if (playerRigidbody.velocity.y > 0)
        {
            playerAnimator.SetInteger("velocityY", 1);
        }
        else if (playerRigidbody.velocity.y < 0)
        {
            playerAnimator.SetInteger("velocityY", -1);
        }
        else
        {
            playerAnimator.SetInteger("velocityY", 0);
        }

        //Tocando el piso?
        if (IsTouchingTheground())
        {
            playerAnimator.SetBool("IsOnTheGround", true);
        }
        else
        {
            playerAnimator.SetBool("IsOnTheGround", false);
        }

    }

    private void ComprobarCaminado() {
        if (Input.GetAxis("Horizontal") == 0)
        {
            playerAnimator.SetBool("IsWalking", false);
        }
        else if (Input.GetAxis("Horizontal") > 0)
        {
            playerSprite.flipX = false;
            playerAnimator.SetBool("IsWalking", true);
        }
        else
        {
            playerSprite.flipX = true;
            playerAnimator.SetBool("IsWalking", true);
        }
    }

    private void ComprobarAgachado() {
        if (Input.GetKey(KeyCode.DownArrow))
        {
            playerAnimator.SetBool("isCrouch", true);
        }
        else
        {
            playerAnimator.SetBool("isCrouch", false);
        }


        

    }


    //FUNCIÓN DE SALTO
    private void Jump() {
        if (IsTouchingTheground())
        {

            if (saltos == 2)
            {
                playerRigidbody.AddForce(Vector2.up * jumpForce);
            }
            else if (saltos == 1)
            {
                playerRigidbody.AddForce(Vector2.up * (jumpForce));
            }

        }
       
    }


    //ESTÁ TOCANDO EL SUELO?
    bool IsTouchingTheground()
    {
        //Parametros de raycast -> (el objeto de donde saldra el raycast, la dirección, la distancia del rayo, la capa que
        // debe detectar)
        if (Physics2D.Raycast(this.transform.position, Vector2.down, 0.9f, groundLayer))
        {
            saltos = 2;

            return true;
        }
        else{
            if (saltos == 1) {
                return true;
            }
            else {

                return false;

            }

        }
    }
}
