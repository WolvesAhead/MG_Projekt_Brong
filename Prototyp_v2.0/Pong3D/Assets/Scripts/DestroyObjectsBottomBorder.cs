using UnityEngine;
using System.Collections;

public class DestroyObjectsBottomBorder : MonoBehaviour
{
    public BallsScript bs;
    public BallsScript2 bs2;
    public static int ballCount1 = 1;
    public static int ballCount2 = 1;
    public bool servestatus1 = false;
    public bool servestatus2 = false;
    float servetimer1 = 2f;
    float servetimer2 = 2f;

    // Use this for initialization
    void Start(){}
    // Update is called once per frame
    void Update()
    {
        if (servestatus1)
        {
            servetimer1 -= Time.deltaTime;

            if (servetimer1 < 0)
            {
                bs.Serve();
                servetimer1 = 2f;
                servestatus1 = false;
            }
        }

        if (servestatus2)
        {
            if (servestatus2)
            {
                servetimer2 -= Time.deltaTime;

                if (servetimer2 < 0)
                {
                    bs2.Serve();
                    servetimer2 = 2f;
                    servestatus2 = false;
                }
            }
        }
    }

    void OnCollisionEnter(Collision col)
    {
        if(col.gameObject.transform.tag == "Matchball" && col.gameObject.transform.position.y > 4)
        {
            EndGame.player2Life -= 1;
        }

        if (col.gameObject.transform.tag == "Matchball" && col.gameObject.transform.position.y < 4)
        {
            EndGame.player1Life -= 1;
        }

        if (col.gameObject.transform.tag == "ball")
        {
            if (col.gameObject.transform.position.y > 4)  
            {
                if (col.gameObject.name.Contains("(Clone)"))     
                {
                    Destroy(col.gameObject);

                    if (Player2Control.player2Score >= 500)
                    {
                        Player1Control.player1Score += 500;
                        Player2Control.player2Score -= 500;
                    }
                    else
                    {
                        int restPunkte;
                        restPunkte = Player2Control.player2Score;
                        Player1Control.player1Score += restPunkte;
                        Player2Control.player2Score -= restPunkte;
                    }
                    ballCount1--;
                }
                else
                {
                    if (Player2Control.player2Score >= 500)
                    {
                        Player1Control.player1Score += 500;
                        Player2Control.player2Score -= 500;
                    }
                    else
                    {
                        int restPunkte;
                        restPunkte = Player2Control.player2Score;
                        Player1Control.player1Score += restPunkte;
                        Player2Control.player2Score -= restPunkte;
                    }
                    bs.Standby();
                    ballCount1--;
                }

                if (ballCount1 == 0)
                {
                    ballCount1++;
                    bs.Serve();
                }
            }
            else if (col.gameObject.transform.position.y < -4)
            {
                if (col.gameObject.name.Contains("(Clone)"))
                {
                    Destroy(col.gameObject);
                    ballCount1--;
                }
                else
                {
                    bs.Standby();
                    ballCount1--;
                }

                if (ballCount1 == 0)
                {
                    GetComponent<AudioSource>().Play();
                    ballCount1++;
                    bs.ResetPowerups();
                    servestatus1 = true;
                }
            }
        }

        if (col.gameObject.transform.tag == "ball2")
        {
            if (col.gameObject.transform.position.y < -4)
            {
                if (col.gameObject.name.Contains("(Clone)"))
                {
                    Destroy(col.gameObject);

                    if (Player1Control.player1Score >= 500)
                    {
                        Player2Control.player2Score += 500;
                        Player1Control.player1Score -= 500;
                    }
                    else
                    {
                        int restPunkte;
                        restPunkte = Player1Control.player1Score;
                        Player2Control.player2Score += restPunkte;
                        Player1Control.player1Score -= restPunkte;
                    }
                    ballCount2--;
                }
                else
                {
                    if (Player1Control.player1Score >= 500)
                    {
                        Player2Control.player2Score += 500;
                        Player1Control.player1Score -= 500;
                    }
                    else
                    {
                        int restPunkte;
                        restPunkte = Player1Control.player1Score;
                        Player2Control.player2Score += restPunkte;
                        Player1Control.player1Score -= restPunkte;
                    }
                    bs2.Standby();
                    ballCount2--;
                }

                if (ballCount2 == 0)
                {
                    ballCount2++;
                    bs2.Serve();
                }
            }
            else if (col.gameObject.transform.position.y > 4)
            {
                if (col.gameObject.name.Contains("(Clone)"))
                {
                    Destroy(col.gameObject);
                    ballCount2--;
                }
                else
                {
                    bs2.Standby();
                    ballCount2--;
                }
                
                if (ballCount2 == 0)
                {
                    GetComponent<AudioSource>().Play();
                    ballCount2++;
                    bs2.ResetPowerups2();
                    servestatus2 = true;
                }
            }
        }
    }
}