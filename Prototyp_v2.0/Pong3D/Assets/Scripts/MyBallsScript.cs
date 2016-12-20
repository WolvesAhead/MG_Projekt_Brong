using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MyBallsScript : MonoBehaviour
{
    public Rigidbody rb;
    public int ballSpeed = 4;
   static public bool startposition;
    public GameObject playerPaddle;
    public  GameObject playerPaddle2;
    public GameObject Fireball;
    public GameObject Firepaddle;
    public Text scoreText;
    public Image circleShield;
    public Image circleGlue;
    public Image circleControlChange;

    //public static int anzahlBälle1 = 1;
    public float gameTimer = 0;
     

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        
    }

    void Awake ()
    {
        startposition = true;
    }

    // Update is called once per frame
    void Update()
    {
       
        //Debug.Log(BlockPhys.brickZähler);
        
        gameTimer += Time.deltaTime;
        if (gameTimer > 30)
        {
            if (ballSpeed < 6)
            {
                ballSpeed += 1;
            }
            gameTimer = 0;
        }


        scoreText.text = ((int)Player1Control.player1Score).ToString();

        if (Player1Control.powerballCollected == true)
        {
            Firepaddle.SetActive(true);
        }
        if (Player1Control.powerballstatus == true)    
        {
            Firepaddle.SetActive(false);
            Fireball.SetActive(true);
                if (transform.position.x > 5.9 || transform.position.x < -5.9)
            {
                transform.GetComponent<Collider>().isTrigger = false;
               

            }
            else
            {
                transform.GetComponent<Collider>().isTrigger = true;
            }

            if(transform.position.y > 4.5 )
            {
                transform.GetComponent<Collider>().isTrigger = false;
                Fireball.SetActive(false);
                Player1Control.powerballstatus = false;
                
            }
        }


        if ((startposition == true) && !(gameObject.name.Contains("(Clone)")))
        {
            
            transform.position = new Vector3(playerPaddle.transform.position.x, -4.5f, -0.7f);
        }

            
            


            if(Input.GetKey(KeyCode.RightArrow) && Input.GetKey(KeyCode.UpArrow)&& (startposition || Player1Control.glued && Player1Control.isClone == false) )
            {
                rb.velocity = new Vector3(0,0,0);
                Debug.Log("rechts");
                GetComponent<Rigidbody>().AddForce(600, 300, 0);
                startposition = false;
            }

            if(Input.GetKey(KeyCode.LeftArrow) && Input.GetKey(KeyCode.UpArrow) && (startposition || Player1Control.glued && Player1Control.isClone == false) )
            {
                rb.velocity = new Vector3(0,0,0);
                GetComponent<Rigidbody>().AddForce(-600, 300, 0);
                Debug.Log("Links");
                startposition = false;
            }

           if(Input.GetKey(KeyCode.UpArrow) && (startposition) && transform.position.y == -4.5f)
            {
                rb.velocity = new Vector3(0,0,0);
                rb.AddForce(0, 300, 0);
                Debug.Log("Oben1(ball)");
                startposition = false;
               
            }
           else if(Input.GetKey(KeyCode.UpArrow) && (Player1Control.glued && Player1Control.isClone == false))
        {
            rb.velocity = new Vector3(0, 0, 0);
            GetComponent<Rigidbody>().AddForce(0, 300, 0);
            Debug.Log("Oben2 (ball)");
            startposition = false;
            Player1Control.glued = false;

        }

        else if (Input.GetKey(KeyCode.UpArrow) && (Player1Control.glued && Player1Control.isClone))
        {
            Player1Control.ItemInstance.velocity = new Vector3(0, 0, 0);
            Player1Control.ItemInstance.AddForce(0, 300, 0);
            Debug.Log("Oben2(clone)");
            startposition = false;
            Player1Control.glued = false;
        }
        #region Kuti steuerung
        /* if(Input.GetKey(KeyCode.C) && Input.GetKey(KeyCode.DownArrow)&& (startposition || Player1Control.glued && Player1Control.isClone == false) )
         {
             rb.velocity = new Vector3(0,0,0);
             Debug.Log("rechts");
             GetComponent<Rigidbody>().AddForce(600, 300, 0);
             startposition = false;
         }

         if(Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.DownArrow) && (startposition || Player1Control.glued && Player1Control.isClone == false) )
         {
             rb.velocity = new Vector3(0,0,0);
             GetComponent<Rigidbody>().AddForce(-600, 300, 0);
             Debug.Log("Links");
             startposition = false;
         }

        if(Input.GetKey(KeyCode.DownArrow) && (startposition) && transform.position.y == -4.5f)
         {
             rb.velocity = new Vector3(0,0,0);
             rb.AddForce(0, 300, 0);
             Debug.Log("Oben1(ball)");
             startposition = false;

         }
        else if(Input.GetKey(KeyCode.DownArrow) && (Player1Control.glued && Player1Control.isClone == false))
     {
         rb.velocity = new Vector3(0, 0, 0);
         GetComponent<Rigidbody>().AddForce(0, 300, 0);
         Debug.Log("Oben2 (ball)");
         Player1Control.glued = false;

     }

     else if (Input.GetKey(KeyCode.DownArrow) && (Player1Control.glued && Player1Control.isClone))
     {
         Player1Control.ItemInstance.velocity = new Vector3(0, 0, 0);
         Player1Control.ItemInstance.AddForce(0, 300, 0);
         Debug.Log("Oben2(clone)");

         Player1Control.glued = false;
     }*/
        #endregion

        if (startposition == false)
        {
            rb.velocity = ballSpeed * (rb.velocity.normalized);
        }


        // das it für den clone Ball damit der auch ein geschwindichkeit hat
        if ((gameObject.name.Contains("(Clone)")))
        {
            rb.velocity = ballSpeed * (rb.velocity.normalized);

        }

       /* if (DestroyObjectsBottomBorder.ballCount1  == 0)
        {
            Player1Control.playerPaddle.transform.localScale = new Vector3(2, 0, 0);
            Debug.Log("player 1 no Balls");
        }
*/
        //Debug.DrawLine(Vector3.zero, transform.position, Color.blue,5 ,false);
    }

    public void Serve()   //Bedeutet Ball wird jetzt wieder aufs Paddle gesetzt, bereit zum schießen
    {

        startposition = true;
    }

    public void Standby()   //Ball wird unter dem Spielfeld gehalten bis die Anzahl auf 0 ist und Serve aufgerufen wird
    {
        transform.position = new Vector3(playerPaddle.transform.position.x, -4.5f, 5f);
    }

    public  void ResetPowerups()  //Alle Powerups auf normal wenn du ein Tor kassierst
    {
        if (playerPaddle.transform.localScale.x >= 1.5f )
        {
            playerPaddle.transform.localScale = new Vector3(1.5f, 0.2f, 0.6f);
        }
        if (playerPaddle.transform.localScale.x <= 1.5f && EndGame.endgameStarted == true)
        {
            playerPaddle.transform.localScale = new Vector3(1.5f, 0.2f, 0.6f);
        }
        Player1Control.powerballstatus = false;
        Player1Control.powerballCollected = false;
        Player1Control.shieldstatus = false;
        Firepaddle.SetActive(false);
        Player1Control.gluestatus = false;
        Player1Control.glued = false;
        circleControlChange.fillAmount = 0;
        circleGlue.fillAmount = 0;
        circleShield.fillAmount = 0;
        Fireball.SetActive(false);
    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.name.Contains("Brick") || collision.gameObject.name.Contains("bricks"))
        {
            GetComponent<AudioSource>().Play();
        }

        foreach (ContactPoint contact in collision.contacts)
        {
            //Punkt auf dem Paddle
            

            //Wert zur Winkelberechnung an Hand der Paddle länge
            
            //Debug.Log("paddlescale" + (playerPaddle.transform.localScale.x / 2));

            // "Winkel" errechnung 
            if (collision.transform.tag == "Player1Paddle" && playerPaddle.transform.position.x < Player1Control.rightLimit - 0.1 && playerPaddle.transform.position.x > Player1Control.leftLimit + 0.1) // damit den ball nicht das paddle folgt wenn das hackt und geht mehr als die grennzung
            {
                float winkel = contact.point.x - playerPaddle.transform.position.x;
                float winkelX = winkel / (playerPaddle.transform.localScale.x / 2);
                rb.velocity = new Vector3(winkelX * 5, rb.velocity.y, 0);


            }
            if (collision.transform.tag == "Player2Paddle" && playerPaddle2.transform.position.x < Player2Control.rightLimit - 0.1 && playerPaddle2.transform.position.x > Player2Control.leftLimit + 0.1)
            {
                float winkel = contact.point.x - playerPaddle2.transform.position.x;
                float winkelX2 = winkel / (playerPaddle2.transform.localScale.x / 2);
                rb.velocity = new Vector3(winkelX2 * 5, rb.velocity.y, 0);

            }


            //Debug.Log(contact.point.x);
        }
    }
}

