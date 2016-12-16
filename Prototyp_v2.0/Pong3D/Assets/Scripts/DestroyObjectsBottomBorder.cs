using UnityEngine;
using System.Collections;

public class DestroyObjectsBottomBorder : MonoBehaviour
{

    public MyBallsScript mbs;       //Instanzierung um Funktionen anderer Scripte aufzurufen
    public MyBallsScript2 mbs2;
    public static int ballCount1 = 1;       //Anzahl Bälle
    public static int ballCount2 = 1;
    public bool servstatus1 = false;
    public bool servstatus2 = false;
    float servetimer1 = 2f;
    float servetimer2 = 2f;


    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (servstatus1)
        {
            servetimer1 -= Time.deltaTime;
            if (servetimer1 < 0)
            {
                mbs.Serve();
                servetimer1 = 2f;
                servstatus1 = false;
            }

           }
       

        if (servstatus2)
        {
            if (servstatus2)
            {
                servetimer2 -= Time.deltaTime;
                if (servetimer2 < 0)
                {
                    mbs2.Serve();
                    servetimer2 = 2f;
                    servstatus2 = false;
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
            if (col.gameObject.transform.position.y > 4)  //Ball 1 trifft Border oben
            {
                if (col.gameObject.name.Contains("(Clone)"))     //Wenn Klon -> zerstören, Punkte und Zähler runter
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
                else                 //Wenn nicht Klon = Hauptball -> Punkte, Standbyfunktion aufrufen und Zähler runter
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
                    mbs.Standby();
                    ballCount1--;
                }
                if (ballCount1 == 0)     //Wenn durch eins davon Anzahl Bälle = 0 wird, Zähler wieder auf 1 und Serve Funktion aufrufen
                {
                    //GetComponent<AudioSource>().Play();
                    ballCount1++;
                    mbs.Serve();
                    //servstatus1 = true;
                }
            }

            else if (col.gameObject.transform.position.y < -4)  //Ball 1 trifft Border unten alles genauso nur gibt keine Punkte
            {
                if (col.gameObject.name.Contains("(Clone)"))
                {
                    Destroy(col.gameObject);
                    ballCount1--;
                }
                else
                {
                    mbs.Standby();
                    ballCount1--;
                }
                if (ballCount1 == 0)
                {
                    //GetComponent<AudioSource>().Play();
                    ballCount1++;
                    mbs.ResetPowerups();
                    // mbs.Serve();
                    servstatus1 = true;
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
                    mbs2.Standby();
                    ballCount2--;
                }
                if (ballCount2 == 0)
                {
                    //GetComponent<AudioSource>().Play();
                    ballCount2++;
                    mbs2.Serve();
                    //servstatus2 = true;
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
                    mbs2.Standby();
                    ballCount2--;
                }
                if (ballCount2 == 0)
                {
                    //GetComponent<AudioSource>().Play();
                    ballCount2++;
                    mbs2.ResetPowerups2();
                    Debug.Log("Lost");
                    // mbs2.Serve();
                    servstatus2 = true;
                }
            }
        }

    }
}
