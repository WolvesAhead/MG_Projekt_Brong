using UnityEngine;
using System.Collections;

public class MatchBallScript : MonoBehaviour
{
    public Rigidbody rb;
    public int ballSpeed = 10;
    static public bool startposition = true;
    public GameObject playerPaddle;
    public GameObject playerPaddle2;
    public static bool P1Torkassiert = false;
    public static bool P2Torkassiert = false;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    // Update is called once per frame
    void Update()
    {
        if (startposition == false)
        {
            rb.velocity = ballSpeed * (rb.velocity.normalized);
        }

        if (startposition == true && transform.position.y < -4)
        {
            transform.position = new Vector3(playerPaddle.transform.position.x, -4.4f, -0.7f);

            if (Input.GetKey(KeyCode.RightArrow) && Input.GetKey(KeyCode.UpArrow))
            {
                rb.velocity = new Vector3(0, 0, 0);
                GetComponent<Rigidbody>().AddForce(600, 300, 0);
                startposition = false;
            }

            if (Input.GetKey(KeyCode.LeftArrow) && Input.GetKey(KeyCode.UpArrow))
            {
                rb.velocity = new Vector3(0, 0, 0);
                GetComponent<Rigidbody>().AddForce(-600, 300, 0);
                startposition = false;
            }

            if (Input.GetKey(KeyCode.UpArrow))
            {
                rb.velocity = new Vector3(0, 0, 0);
                rb.AddForce(0, 300, 0);
                startposition = false;
            }
        }

        if (startposition == true && transform.position.y > 4)
        {
            transform.position = new Vector3(playerPaddle2.transform.position.x, 4.3f, -0.7f);
            
            if (Input.GetKey(KeyCode.D) && Input.GetKey(KeyCode.W))
            {
                rb.velocity = new Vector3(0, 0, 0);
                GetComponent<Rigidbody>().AddForce(600, -300, 0);
                startposition = false;
            }

            if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.W))
            {
                rb.velocity = new Vector3(0, 0, 0);
                GetComponent<Rigidbody>().AddForce(-600, -300, 0);
                startposition = false;
            }

            if (Input.GetKey(KeyCode.W))
            {
                rb.velocity = new Vector3(0, 0, 0);
                GetComponent<Rigidbody>().AddForce(0, -300, 0);
                startposition = false;
            }
        }
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.transform.tag == "BottomBorder")
        {
            startposition = true;
            P1Torkassiert = true;
        }

        if (col.transform.tag == "TopBorder")
        {
            startposition = true;
            P2Torkassiert = true;
        }

        foreach (ContactPoint contact in col.contacts)
        { 
            if (col.transform.tag == "Player1Paddle" && playerPaddle.transform.position.x < Player1Control.rightLimit - 0.1 && playerPaddle.transform.position.x > Player1Control.leftLimit + 0.1) // damit den ball nicht das paddle folgt wenn das hackt und geht mehr als die grennzung
            {
                float winkel = contact.point.x - playerPaddle.transform.position.x;
                float winkelX = winkel / (playerPaddle.transform.localScale.x / 2);
                rb.velocity = new Vector3(winkelX * 5, rb.velocity.y, 0);
            }

            if (col.transform.tag == "Player2Paddle" && playerPaddle2.transform.position.x < Player2Control.rightLimit - 0.1 && playerPaddle2.transform.position.x > Player2Control.leftLimit + 0.1)
            {
                float winkel = contact.point.x - playerPaddle2.transform.position.x;
                float winkelX2 = winkel / (playerPaddle2.transform.localScale.x / 2);
                rb.velocity = new Vector3(winkelX2 * 5, rb.velocity.y, 0);
            }
        }
    }
}