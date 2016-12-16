using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Player1Control : MonoBehaviour
{
    public static GameObject playerPaddle;
    public GameObject bottomBorder;

    public GameObject shield;
    public Rigidbody rbball;
    public Rigidbody rbball2;
    public GameObject ball;
    public GameObject CCCloak;
    public GameObject GGCloak;



    static public float speed = 6f;
    public static float rightLimit;
    public static float leftLimit;
    public static Rigidbody ItemInstance;
    

    public static bool shieldstatus = false;
    public static bool firstballisHere = false;
    public static bool isClone = false;
    public static bool powerballstatus = false;
    public static bool powerballCollected = false;
    public static bool gluestatus = false;
    public static bool glued = false;
    public static bool controlChange = false;


    public static int player1Score = 000;
    public  float controlChangeTime = 5f;
    public  float shieldTime = 8f;
    public  float glueTime = 12f;
    
    
    float contactPointGlue;

     public Image circleShield;
     public Image circleGlue;
     public Image circleControlChange;

    float speedItemTimerShield = 8f;
    float speedItemTimerGlue = 12f;
    float speedItemTimerControlChange = 5f;

    void Start()
    {
        Debug.Log(transform.localScale.x / 2);
        
       
    }

    void Update()
    {

        // left limit ist der wert von der punkt der am ende links auf dem bottomBorder liegt und zwar "-6,25 "
        // genau das gleiche geht für rightLimit 

        leftLimit = bottomBorder.GetComponent<Renderer>().bounds.min.x + (transform.localScale.x / 2); // 0,75 ist die hälfte unsere paddle (-6,25 + 0,75 = -5,5) ==> genau unesere limit
        rightLimit = bottomBorder.GetComponent<Renderer>().bounds.max.x - (transform.localScale.x / 2);

      //   float paddlepoint = transform.position.x;



        #region Controlchange
        // ControllChange 
        if (controlChange)
        {
            CCCloak.SetActive(true);
            //Timer
            controlChangeTime -= Time.deltaTime;
            circleControlChange.fillAmount = speedItemTimerControlChange / 5;
            speedItemTimerControlChange -= Time.deltaTime;



            if (controlChangeTime < 0)
            {
                speedItemTimerControlChange = 5;
                circleControlChange.fillAmount = 0;
                controlChange = false;
                controlChangeTime = 5f;
                
            }

            if (Input.GetKey(KeyCode.RightArrow) && transform.position.x > leftLimit + 0.1)
            {
                transform.position += Vector3.left * speed * Time.deltaTime;


            }
            if (Input.GetKey(KeyCode.LeftArrow) && transform.position.x < rightLimit - 0.1)
            {
                transform.position += Vector3.right * speed * Time.deltaTime;
            }
        }
        else
        {
            // Steuerung

            if (Input.GetKey(KeyCode.RightArrow) && transform.position.x < rightLimit - 0.1)
            {
                transform.position += Vector3.right * speed * Time.deltaTime;
            }
            if (Input.GetKey(KeyCode.LeftArrow) && transform.position.x > leftLimit + 0.1)
            {
                transform.position += Vector3.left * speed * Time.deltaTime;
            }

            

            CCCloak.SetActive(false);
        }

        #endregion

        #region Shield
        // shield activation      
        if (shieldstatus)
        {
            /*if (shieldstatus && shieldTime < 8)
            {
                shieldTime = 8f;
                speedItemTimerShield = 8f;
            }
            */
            shieldTime -= Time.deltaTime;
            circleShield.fillAmount = speedItemTimerShield / 8;
            speedItemTimerShield -= Time.deltaTime;
            shield.SetActive(true);


            if (shieldTime < 0)
            {
                speedItemTimerShield = 8;
                circleShield.fillAmount = 0; 
                shieldstatus = false;
                shieldTime = 8f;


            }
        }
        else
        {
            shield.SetActive(false);
        }

        #endregion

        #region Glue
        // glue activation      
        if (gluestatus)
        {
            GGCloak.SetActive(true);
            glueTime -= Time.deltaTime;
            circleGlue.fillAmount = speedItemTimerGlue / 12;
            speedItemTimerGlue -= Time.deltaTime;
            if (glued == true )
            {
                if (isClone == false)
                rbball.transform.position = new Vector3((transform.position.x + contactPointGlue), -4.5f, -0.7f);

                if (isClone)
                    ItemInstance.transform.position = new Vector3((transform.position.x + contactPointGlue), -4.5f, -0.7f);

            }

            if (glueTime < 0)
            {
                speedItemTimerGlue = 12f;
                gluestatus = false;
                circleGlue.fillAmount = 0;
             
                glueTime = 12f;
                glued = false;
                firstballisHere = false;


            }
        }
        else
        {
            GGCloak.SetActive(false);     // für dich Martin 
        }
       
        #endregion

    }


    void OnCollisionEnter(Collision collision)
    {


        #region ITEMS
        //////////////////BIGP PADDLE ITEM\\\\\\\\\\\\\\\\\\

        if (collision.transform.tag == "bigPaddle")
        {
            if (this.gameObject.transform.localScale.x <= 3)
            {
                GetComponent<AudioSource>().Play();
                this.gameObject.transform.localScale += new Vector3(1, 0, 0);
            }

            

        }

        //////////////////Small PADDLE ITEM- Bug verhindert, dass die X Größe ins Negative gerät und somit wieder größer wird\\\\\\\\\\\\\\\\\\

        if (collision.transform.tag == "smallPaddle")
        {
            if (this.gameObject.transform.localScale.x >= 1)
            {
                GetComponent<AudioSource>().Play();
                this.gameObject.transform.localScale -= new Vector3(1, 0, 0);
            }
        }

        //////////////////mehr bälle ITEM\\\\\\\\\\\\\\\\\\

        if (collision.transform.tag == "mehrbaelle")
        {
            GetComponent<AudioSource>().Play();

            DestroyObjectsBottomBorder.ballCount1++;
            Debug.Log("ball++. Du hast jetzt " + DestroyObjectsBottomBorder.ballCount1 + " Bälle");
            ItemInstance = Instantiate(rbball, new Vector3(transform.position.x, transform.position.y + 1f, transform.position.z), Quaternion.identity) as Rigidbody;
            ItemInstance.AddForce(0, 150, 0);
           


        }

        ///////////////// Steuerung umkehren \\\\\\\\\\\\\\\\
        if (collision.transform.tag == "ControlChange")
        {
            GetComponent<AudioSource>().Play();
            Debug.Log("Rechts is jetzt Links");
           // Player2Control.controlchangelight.SetActive(true);

            Player2Control.controlChange = true;
        

    /*if (Input.GetKey(KeyCode.RightArrow) && (transform.position.x > rightLimit - 0.1))
    {
        transform.position += Vector3.left * speed * Time.deltaTime;

    }
    if (Input.GetKey(KeyCode.LeftArrow)  && (transform.position.x < leftLimit + 0.1))
    {
        transform.position += Vector3.right * speed * Time.deltaTime;
    } 
    */
}


        ///////////////// Shield Item \\\\\\\\\\\\\\\\

        if (collision.transform.tag == "shieldItem" && shieldstatus == true)
        {

            GetComponent<AudioSource>().Play();
            speedItemTimerShield = 8f;
           
            shieldTime = 8f;
            shieldstatus = true;
          
            circleShield.fillAmount = 0;

        }
        if (collision.transform.tag == "shieldItem")
        {
            GetComponent<AudioSource>().Play();
            shieldstatus = true;


        }

        ///////////////// Powerball Item \\\\\\\\\\\\\\\\
        if (collision.transform.tag == "powerballItem")
        {
            GetComponent<AudioSource>().Play();
            powerballCollected = true;
            
           
        }
        if (powerballCollected == true && collision.transform.tag == "ball" || powerballCollected == true && MyBallsScript.startposition == true)
        {
            
            powerballstatus = true;
            powerballCollected = false;
          

        }

        ///////////////// Glue Item \\\\\\\\\\\\\\\\
        if (collision.transform.tag == "glueItem")
        {
            GetComponent<AudioSource>().Play();
            speedItemTimerGlue = 12f;

            glueTime = 12f;
            gluestatus = true;
            circleShield.fillAmount = 0;

        }
        if (collision.transform.tag == "glueItem")
        {
            GetComponent<AudioSource>().Play();
            gluestatus = true;
            
          


        }

        if (collision.transform.tag == "ball"  && gluestatus == true)
        {
       

           
            if ((collision.gameObject.name.Contains("(Clone)")))
            {
                isClone = true;
              
            }
            else
            {
                isClone = false;
          

            }

            foreach (ContactPoint contact in collision.contacts)
            {
                contactPointGlue = contact.point.x - transform.position.x;

            }
            glued = true;
        }


       


        #endregion





    }
}
