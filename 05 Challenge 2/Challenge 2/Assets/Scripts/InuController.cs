using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class InuController : MonoBehaviour
{
    
    //movement
    public float speed;
    private Rigidbody2D rb2d;

    Animator anim;

    public Text countText;
    public Text playerLives;
    public Text gameOverText;

    private int count = 0;
    private int lives= 3;


    public GameObject player;

    public AudioClip musicClipOne;
    public AudioClip musicClipWin;
    public AudioClip musicClipLose;
    public AudioSource musicSource;
    public AudioSource musicSourceTwo;

    private bool facingRight = true;

    private float horizontal;
    private bool vertical;
   




    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
        gameOverText.text = "";
        countText.text = "Score: " + count.ToString();
        playerLives.text = "Lives: " + lives.ToString();
        
        //audio
        musicSource.clip = musicClipOne;
        musicSource.Play();
        musicSource.loop = true;
    }

    void FixedUpdate()
    //Players Movement
    {
        float moveHorizonal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        Vector2 movement = new Vector2(moveHorizonal, moveVertical);
        rb2d.AddForce(movement * speed);

        horizontal = Input.GetAxis("Horizontal") * speed;
        //vertical = Input.GetAxis("Vertical");
        
        //jump animation


        if (vertical == true )
        {
            anim.SetInteger("State", 2);
        }
        
        
        if (horizontal == 0 && vertical == false)
        {
           anim.SetInteger("State", 0);
        }


        if ( horizontal > 0 && vertical == false)
        {
            
            anim.SetInteger("State", 1);
            if (facingRight == false)
            {
                Flip();
            }
        }
        if (horizontal < 0 && vertical == false)
        {
            anim.SetInteger("State", 1);
            if (facingRight == true)
            {
                Flip();
            }
            
        }



        if (Input.GetKey("escape"))
            Application.Quit();
            
    }
    


   
    //Score and Lives
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //pickup and win text


        if (collision.collider.tag == "PickUp")
        {
            count += 1;
            countText.text = "Score: " + count.ToString();
            Destroy(collision.collider.gameObject);

            if (count >= 8)
            //Win Text display
            {
                gameOverText.text = "You win! Created by Erica!";
                musicSourceTwo.clip = musicClipWin;
                musicSourceTwo.Play();
                musicSourceTwo.loop = false;
                               
                Destroy(player);
            }
            if (count == 4)
            //equal to the number pickups on the first playfield
            //transports to second field
            {
                lives = 3;
                playerLives.text = "Lives: " + lives.ToString();
                transform.position = new Vector2(50.0f, 0.0f);
                
            }

        }

        //enemies and lose text
        if (collision.collider.tag == "Enemy")
        {
            lives -= 1;
            playerLives.text = "Lives: " + lives.ToString();
            Destroy(collision.collider.gameObject);

            if (lives <= 0)
            //Win Text display
            {
                
                gameOverText.text = "You lose...try again...Erica";
                musicSourceTwo.clip = musicClipLose;
                musicSourceTwo.Play();
                musicSourceTwo.loop = false;

                //anim.SetInteger("State", 4);
                Destroy(player);

            }


        }


    }


    //Jumping
    private void OnCollisionStay2D(Collision2D collision)
    {



        if (collision.collider.tag == "Ground")
        {
            vertical = false;
            if (Input.GetKey(KeyCode.W))
            {
                rb2d.AddForce(new Vector2(0, 3), ForceMode2D.Impulse);
                vertical = true;
            }
        }


    }


    //Update is called once per frame
    void Update()
    {
          

    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector2 Scaler = transform.localScale;
        Scaler.x = Scaler.x * -1;
        transform.localScale = Scaler;
    }


}
