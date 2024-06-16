using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    private Rigidbody2D rb2D;
    private Animator animate;

    public float moveSpeed;

    private float jumpForce;
    private int totalJump;
    private int maxJump;
    private bool isJumping;

    private float moveHorizontal;
    private bool faceRight = true;


    // Start is called before the first frame update
    void Start()
    {
        rb2D = gameObject.GetComponent<Rigidbody2D>();
        animate = gameObject.GetComponent<Animator>();


        //moveSpeed = 2f;
        jumpForce = 20f;
        totalJump = 0;
        maxJump = 2;
        isJumping = false;

    }

    // Update is called once per frame
    void Update()
    {
        // left = -1 , no = 0 , right = 1 
        moveHorizontal = Input.GetAxisRaw("Horizontal");

        // defineix el valor del paràmetre Speed
        animate.SetFloat("Speed", Mathf.Abs(moveHorizontal));


        // gira el Sprite en funció de la direcció
        if (moveHorizontal > 0 && !faceRight)
        {
            faceRight = !faceRight;
        }
        else if (moveHorizontal < 0 && faceRight)
        {
            faceRight = !faceRight;
        }

        // Salta si toca al terra i permet un doble salt
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (!isJumping)
            {
                rb2D.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
            }
            else if (isJumping && totalJump < maxJump)
            {
                totalJump++;
  
                rb2D.AddForce(new Vector2(moveHorizontal * moveSpeed*10, jumpForce), ForceMode2D.Impulse);
            }
        }
        
    }

    // Update with phisic engine inside Unity
    private void FixedUpdate()
    {
        if(moveHorizontal > 0.1f || moveHorizontal < -0.1f)
        {
            rb2D.AddForce(new Vector2(moveHorizontal * moveSpeed, 0f), ForceMode2D.Impulse);
            //print(moveSpeed);
        }
    }

    // Aquesta actualització s'executa després que tots els processos hagin sigut executats
    // D'aquesta manera ens assegurem que la inversió del personatge és realitza despés del "Animate"
    private void LateUpdate() {
        if (!faceRight)
        {
            Vector3 flipScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
            transform.localScale = flipScale;
            //GetComponent<SpriteRenderer>().flipX = true;
        }
    }
    /*
     You have an animator attached to the game object, if the scale value is controlled by the animation clip, you cannot change it.
    A typical workaround is give the game object an empty parent and change its scale.

    Player    <---- Change scale here
        Model <---- Animator here
     */



    // Funcions colisionadors
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Platform")
        {
            totalJump = 0;
            isJumping = false;
        }
        if (collision.gameObject.tag == "Death")
        {
            GameManager.Instance._playerHealth.Health = 0;
            print("Game Over");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Platform")
        {
            // Es resta el salt inicial al sortir d'una superfície.
            totalJump++;
            isJumping = true;
        }
    }

}
