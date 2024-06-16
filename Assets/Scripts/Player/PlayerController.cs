using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Threading;

//[RequireComponent(typeof(AudioSource))]
public class PlayerController : MonoBehaviour
{

    private Rigidbody2D rb2D;
    private Animator animate;

    private float moveSpeed;
    public float moveHorizontal;

    private float jumpForce;
    private bool isJumping;
    private bool doubleJump;
    private bool isIdle;

    private float timeReset;
    private float timeCount;

    private bool checkPoint;
    private bool playerStart;
    private bool playerDeath;
    private bool playerFinish;


    PlayerBehaviour _playerBehaviour;

    [SerializeField] AudioClip audioJump;
    [SerializeField] AudioClip audioDoubleJump;
    [SerializeField] AudioClip audioDeath;
    [SerializeField] AudioClip audioGameOver;
    [SerializeField] AudioClip[] audioKiks;
    [SerializeField] AudioClip audioIdle;
    [SerializeField] AudioSource audioSource;


    void Start()
    {
        rb2D = gameObject.GetComponent<Rigidbody2D>();
        _playerBehaviour = GameObject.Find("Player").GetComponent<PlayerBehaviour>();
        animate = gameObject.GetComponent<Animator>();

        
        GameManager.Instance._lives = GameManager.Instance._maxLives;

        
        moveSpeed = 1f;

        jumpForce = 40f;
        isJumping = false;
        doubleJump = false;
        isIdle = false;
        timeReset = 0f;
        timeCount = 2f;

        checkPoint = false;
        playerStart = false;
        playerDeath = false;
        playerFinish = false;

        audioSource = GetComponent<AudioSource>();


    }


    void Update()
    {
        // left = -1 , no = 0 , right = 1 
        //moveHorizontal = Input.GetAxisRaw("Horizontal");

        // defineix el valor del paràmetre Speed del animate
        animate.SetFloat("Speed", Mathf.Abs(moveHorizontal));

        //si es troba en un compte de reset anula tos els moviments
        if (playerStart)
        {
            if (checkPoint)
            {
                moveHorizontal = 0;
                timeReset += Time.deltaTime;

                if (timeReset > 0.5f)
                {
                    playerStart = false;
                    timeReset = 0f;
                }
            }
            else
            {
                playerStart = false;
            }
        }
        else if(playerDeath)
        {
            Death();
        }
        else if (playerFinish)
        {
            Finish();
        }
        else
        {
            //constantment gasta stamina si esta en contacte amb el terra
            if (!isJumping)
            {
                PlayerWasteStamina();
            }
 

            if (Input.touchCount > 0)
            {
                // controls dispositius tàctis:
                foreach (Touch touch in Input.touches)
                {
                    // Obtenim la posició del toc
                    Vector2 touchPosition = touch.position;

                    // Comprova si el toc està a la meitat esquerra de la pantalla
                    if (touchPosition.x < Screen.width / 2)
                    {
                        // Comprova si el toc s'està mantenint (fase Moved o Stationary)
                        if (touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Stationary)
                        {
                            isIdle = true;
                        }
                    }
                    // Comprova si el toc està a la meitat dreta de la pantalla
                    else if (touchPosition.x >= Screen.width / 2)
                    {
                        // Comprova si el toc és un tap (fase Began)
                        if (touch.phase == TouchPhase.Began)
                        {
                            isIdle = false;
                            PlayerJump();
                        }
                    }
                }
            }
            else
            {
                // Controls per a PC
                if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.RightArrow))
                {
                    isIdle = false;
                    PlayerJump();
                }
                else if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.LeftArrow))
                {
                    isIdle = true;
                }
                else
                {
                    //si no es prem cap control tàctil o de teclat la variable es false
                    isIdle = false;
                }
            }




            //Executem la funció que variarà la velocitat en funció del boolea isIdle
            if (isIdle)
            {
                PlayerIdle();   
            }
            else
            {
                if (timeCount != 2f) timeCount = 2f;
                //aqui podem afegirles mecàniques per girar el personatge en nivells avançats
                moveHorizontal = 1f;
            }

            // if stamina = 0 -> Game Over
            if (GameManager.Instance._playerStamina.Stamina <= 0)
            {
                timeCount = 2f;
                playerDeath = true;
            }
        }


        





    }

    // Update with phisic engine inside Unity
    private void FixedUpdate()
    {
        if (moveHorizontal > 0.1f || moveHorizontal < -0.1f)
        {
            rb2D.AddForce(new Vector2(moveHorizontal * moveSpeed, 0f), ForceMode2D.Impulse);
            //print(moveSpeed);
        }
    }






    // Funcions colisionadors

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Platform"))
        {
            isJumping = false;
            doubleJump = false;
            animate.SetBool("Is_Jumping", false);
            animate.SetBool("Double_Jump", false);
            AudioClip audioKik = audioKiks[Random.Range(0, audioKiks.Length)];
            audioSource.PlayOneShot(audioKik, 1f);
        }

        if (collision.CompareTag("Death"))
        {
            playerDeath = true;
        }
        if (collision.CompareTag("Finish"))
        {
            playerFinish = true;
        }
        if (collision.CompareTag("Respawn"))
        {
            PlayerRegenStamina(10f);
            checkPoint = true;
            _playerBehaviour.SavePos();
            //busquem per el nom i eliminem l'objecte
            Destroy(GameObject.Find("Respawn"));
        }
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Platform"))
        {
            // Es resta el salt inicial al sortir d'una superfície.
            isJumping = true;
        }
    }


    // Funcions accions personatge

    private void PlayerJump()
    {
        if (!isJumping)
        {
            PlayerUseStamina(5f, false);
            rb2D.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
            isJumping = true;
            animate.SetBool("Is_Jumping", true);
            audioSource.PlayOneShot(audioJump, 1f);
        }
        else if (isJumping && !doubleJump)
        {
            PlayerUseStamina(8f, false);
            rb2D.velocity = new Vector2(rb2D.velocity.x, 0f);
            rb2D.AddForce(new Vector2(moveHorizontal * 8, jumpForce), ForceMode2D.Impulse);
            doubleJump = true;
            animate.SetBool("Double_Jump", true);
            audioSource.PlayOneShot(audioDoubleJump, 1f);
        }
    }


    private void PlayerIdle()
    {
        if (!isJumping)
        {
            if (moveHorizontal != 0f)
            {
                rb2D.AddForce(new Vector2(-moveHorizontal * moveSpeed * 2f, 0f), ForceMode2D.Impulse);
                //print(moveSpeed);
            }
            PlayerUseStamina(GameManager.Instance._playerStamina.StaminaSpeed, true);
            moveHorizontal = 0f;

            timeCount += Time.deltaTime;

            if (timeCount > .5f)
            {
                audioSource.PlayOneShot(audioIdle);
                timeCount = 0f;
            }

        }
    }


    // Funcions stamina

    private void PlayerWasteStamina()
    {
        GameManager.Instance._playerStamina.WasteStamina();
    }

    private void PlayerUseStamina(float staminaAmount, bool continuous)
    {
        GameManager.Instance._playerStamina.UseStamina(staminaAmount, continuous);
    }

    private void PlayerRegenStamina(float staminaAmount)
    {
        GameManager.Instance._playerStamina.RegenStamina(staminaAmount);
    }


    // funcions LVL

    private void Finish()
    {
        moveHorizontal = 0;
        timeReset += Time.deltaTime;

        animate.SetBool("Death", true);

        if (timeReset > 1f)
        {
            ResetGame();
            _playerBehaviour.UpLevel();
            Loader.Load(Loader.Scene.GameFinish);
        }
    }

    private void Death()
    {
        moveHorizontal = 0;
        animate.SetBool("Death", true);

        timeReset += Time.deltaTime;
        timeCount += Time.deltaTime;

        if(timeCount > 1.5f)
        {
            if (GameManager.Instance._lives > 0)
            {
                audioSource.PlayOneShot(audioDeath, 1f);
                timeCount = 0f;
            }
            else
            {
                audioSource.PlayOneShot(audioGameOver, 1f);
                timeCount = 0f;
            }
        }
        

        if (timeReset > 1f)
        {
            if (GameManager.Instance._lives > 0)
            {

                GameManager.Instance._lives--;
                // carreguem la posició del checkpoint si tenim vides disponibles i hi ha algun checkpoint guardat
                //podriem crear tambe un checkpoint cada vegada que és iniat un nivell
                if (SaveManager.SavedataExists() && checkPoint)
                {
                    ResetStats();
                    _playerBehaviour.LoadPos();
                        
                }
                else
                {
                    ResetStats();
                    _playerBehaviour.ResetPos();
                    _playerBehaviour.LoadPos();
                }

            }
            else
            {
                ResetGame();
                Loader.Load(Loader.Scene.GameOver);
            }
        }
        

    }



    private void ResetGame()
    {
        // Restablir els valors del savemanager
        _playerBehaviour.ResetPos();
        ResetStats();
        GameManager.Instance._lives = GameManager.Instance._maxLives;
        checkPoint = false;
        playerFinish = false;
    }

    private void ResetStats()
    {
        isJumping = false;
        doubleJump = false;
        timeReset = 0f;
        timeCount = 2f;
        playerDeath = false;
        playerStart = true;
        animate.SetBool("Death", false);
    }

}
