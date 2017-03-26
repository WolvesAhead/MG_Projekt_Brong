using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Player1Control : MonoBehaviour
{
    public static GameObject playerPaddle;
    public GameObject bottomBorder;
    public Rigidbody rbball;
    public Rigidbody rbball2;
    public GameObject ball;
    public GameObject shield;
    public GameObject CCCloak;
    public GameObject GGCloak;
    public static float speed = 6f;
    public static float rightLimit;
    public static float leftLimit;
    public static int player1Score = 000;
    public static Rigidbody ItemInstance;
    public static bool shieldstatus = false;
    public static bool firstballisHere = false;
    public static bool isClone = false;
    public static bool powerballstatus = false;
    public static bool powerballCollected = false;
    public static bool gluestatus = false;
    public static bool mehrbällestatus = false;
    public static bool glued = false;
    public static bool controlChange = false;
    public float controlChangeTime = 5f;
    public float shieldTime = 8f;
    public float glueTime = 12f;
    float contactPointGlue;
    public Image circleShield;
    public Image circleGlue;
    public Image circleControlChange;
    float speedItemTimerShield = 8f;
    float speedItemTimerGlue = 12f;
    float speedItemTimerControlChange = 5f;

    void Start(){}
    void Update()
    {
        leftLimit = bottomBorder.GetComponent<Renderer>().bounds.min.x + (transform.localScale.x / 2);
        rightLimit = bottomBorder.GetComponent<Renderer>().bounds.max.x - (transform.localScale.x / 2);

        if (Input.GetKey("5"))
        {
            Application.Quit();
        }
        #region Shield
        // shield activation      
       

        if (shieldstatus)
        {
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

        #region Controlchange
        // ControllChange 
        if (controlChange)
        {
            CCCloak.SetActive(true);
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

            if (Input.GetKey(KeyCode.D) && transform.position.x > leftLimit + 0.1)
            {
                transform.position += Vector3.left * speed * Time.deltaTime;
            }

            if (Input.GetKey(KeyCode.A) && transform.position.x < rightLimit - 0.1)
            {
                transform.position += Vector3.right * speed * Time.deltaTime;
            }
        }
        else
        {
            // Steuerung
            if (Input.GetKey(KeyCode.D) && transform.position.x < rightLimit - 0.1)
            {
                transform.position += Vector3.right * speed * Time.deltaTime;
            }

            if (Input.GetKey(KeyCode.A) && transform.position.x > leftLimit + 0.1)
            {
                transform.position += Vector3.left * speed * Time.deltaTime;
            }
            CCCloak.SetActive(false);
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

            if (glued == true)
            {
                if (isClone == false)
                {
                    rbball.transform.position = new Vector3((transform.position.x + contactPointGlue), -4.5f, -0.7f);  
                }

                if (isClone)
                {
                    ItemInstance.transform.position = new Vector3((transform.position.x + contactPointGlue), -4.5f, -0.7f);
                }
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
            GGCloak.SetActive(false);
        }
        #endregion
    }

    void OnCollisionEnter(Collision collision)
    {
        #region ITEMS
        //Big Paddle
        if (collision.transform.tag == "bigPaddle")
        {
            if (this.gameObject.transform.localScale.x <= 3)
            {
                GetComponent<AudioSource>().Play();
                this.gameObject.transform.localScale += new Vector3(1, 0, 0);
            }
        }

        //Small Paddle
        if (collision.transform.tag == "smallPaddle")
        {
            if (this.gameObject.transform.localScale.x >= 1)
            {
                GetComponent<AudioSource>().Play();
                this.gameObject.transform.localScale -= new Vector3(1, 0, 0);
            }
        }

        //Mehr Bälle
        if (collision.transform.tag == "mehrbaelle")
        {
            GetComponent<AudioSource>().Play();
            mehrbällestatus = true;
            DestroyObjectsBottomBorder.ballCount1++;
            ItemInstance = Instantiate(rbball, new Vector3(transform.position.x, transform.position.y + 1f, transform.position.z), Quaternion.identity) as Rigidbody;
            ItemInstance.AddForce(0, 150, 0);
        }

        //Steuerung umkehren
        if (collision.transform.tag == "ControlChange")
        {
            GetComponent<AudioSource>().Play();
            Player2Control.controlChange = true;
        }

        //Shield
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

        //Powerball
        if (collision.transform.tag == "powerballItem")
        {
            GetComponent<AudioSource>().Play();
            powerballCollected = true;   
        }

        if (powerballCollected == true && collision.transform.tag == "ball" || powerballCollected == true && BallsScript.startposition == true)
        {
            powerballstatus = true;
            powerballCollected = false;
        }

        //Glue
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