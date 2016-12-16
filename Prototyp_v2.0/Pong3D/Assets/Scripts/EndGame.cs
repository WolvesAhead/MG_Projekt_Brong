using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EndGame : MonoBehaviour
{
    public bool gameEnded;
    static public bool endgameStarted;
    public float scoreTimer = 0f;
    static public int player1Life;
    static public int player2Life;
    public MyBallsScript mbs;
    public MyBallsScript2 mbs2;
    bool LifesCount = false;
    bool IconTime = false;
    public Image Healthbar1;
    public Image Healthbar2;
    public Image BattleModeIcon;
    public GameObject Score1;
    public GameObject Score2;
    public Rigidbody rbball;
    public Rigidbody rbball2;
    public GameObject MatchBall;
    public GameObject playerPaddle;
    public GameObject playerPaddle2;
    float battlemodeTimer = 0;
    public bool coinFlipMB = false;
    float lifeBorder1;
    float lifeBorder2;
    public GameObject WinLose1;
    public GameObject WinLose2;


    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Blöcke:" + BlockPhys3.brickZähler);

        lifeBorder1 = player1Life * 0.19999f;
        lifeBorder2 = player2Life * 0.19999f;
       
        if ((Input.GetKey(KeyCode.W)|| Input.GetKey(KeyCode.UpArrow)) && gameEnded == true)
        {
            Application.LoadLevel("MainMenu");
        }


        if (MatchBallScript.P1Torkassiert == true )
        {
            player1Life = player1Life -( 1/2);           
        Debug.Log("Leben Spieler 1:" + player1Life);
            lifeBorder1 = player1Life * 0.19999f;
            Healthbar1.fillAmount = lifeBorder1;
            MatchBallScript.P1Torkassiert = false;
            if(player1Life < 1)
            {   
                WinLose2.SetActive(true);
             
                MatchBall.SetActive(false);
                Debug.Log("Player 1 lost");
                gameEnded = true;
               
            }
        }

        if (MatchBallScript.P2Torkassiert == true)
        {
            player2Life = player2Life -(1/2);
             lifeBorder2 = player2Life * 0.19999f;
            Debug.Log("Leben Spieler 2:" + player2Life);
            Healthbar2.fillAmount = lifeBorder2;
            MatchBallScript.P2Torkassiert = false;
            if(player2Life < 1)
            {
                WinLose1.SetActive(true);
             
                MatchBall.SetActive(false);
                Debug.Log("player 2 lost");
                gameEnded = true;
            }

        }
        //Pause und Battlemodus
        if (BlockPhys.brickZähler == 0 || BlockPhys2.brickZähler == 0||BlockPhys3.brickZähler  == 0)
        {
            startEndgame();
            endgameStarted = true;
            /*  MoveBlock.MoveBlock1.SetActive(false);
              MoveBlock.MoveBlock2.SetActive(false);
              MoveBlock.MoveBlock3.SetActive(false);
              MoveBlock.MoveBlock4.SetActive(false);*/
            if (LifesCount == false)
            {
                countLifes();
                if (player1Life < 1)
                {
                    WinLose2.SetActive(true);

                    MatchBall.SetActive(false);
                    Debug.Log("Player 1 lost");
                    Healthbar1.fillAmount = 0;
                    Healthbar2.fillAmount = 0;
                }
                if (player2Life < 1)
                {
                    WinLose1.SetActive(true);

                    MatchBall.SetActive(false);
                    Debug.Log("player 2 lost");
                    Healthbar1.fillAmount = 0;
                    Healthbar2.fillAmount = 0;
                }

            }
            if (LifesCount == true )
            {
                scoreTimer += Time.deltaTime; 
                if (Player1Control.player1Score > 0)
                {
                    Player1Control.player1Score -= 50;
                }
                if (Healthbar1.fillAmount < lifeBorder1)
                {
                    Healthbar1.fillAmount += 0.005f;
                }

                if (Player2Control.player2Score > 0)
                {
                    Player2Control.player2Score -= 50;
                }
                if (Healthbar2.fillAmount < lifeBorder2)
                {
                    Healthbar2.fillAmount += 0.005f;
                }
            }
        }

       /* if (IconTime == true)
        {
            BattleModeIcon.fillAmount += 0.05f;
        }
        else
        {
            BattleModeIcon.fillAmount -= 0.05f;
        }

        if (MatchBall.transform.position.y >= 0) ;
        */
    }



    void countLifes()
    {
        if (Player1Control.player1Score >= 6000)
        {
            Player1Control.player1Score = 5000;
        }

        if (Player2Control.player2Score >= 6000)
        {
            Player2Control.player2Score = 5000;
        }
        player1Life = (Player1Control.player1Score / 1000);
        player2Life = (Player2Control.player2Score / 1000);
        Debug.Log("Leben Spieler 2:" + player2Life);
        Debug.Log("Leben Spieler 1:" + player1Life);
        LifesCount = true;
    }

    void startEndgame()
    {
       /* if(Player1Control.player1Score < 1000)
        {
            WinLose2.SetActive(true);

            MatchBall.SetActive(false);
            Debug.Log("Player 1 lost");
        }

        if (Player2Control.player2Score < 1000)
        {
            WinLose1.SetActive(true);

            MatchBall.SetActive(false);
            Debug.Log("player 2 lost");
        }
        */
        MyBallsScript.startposition = false;
        MyBallsScript2.startposition = false;
        rbball.velocity = new Vector3(0, 0, 0);
        rbball2.velocity = new Vector3(0, 0, 0);
        rbball.transform.position = new Vector3(rbball.transform.position.x, rbball.transform.position.y, 5);
        rbball2.transform.position = new Vector3(rbball2.transform.position.x, rbball2.transform.position.y, 5);
        battlemodeTimer += Time.deltaTime;
        mbs.ResetPowerups();
        mbs2.ResetPowerups2();
        if (battlemodeTimer > 2 && battlemodeTimer < 4)
        {
            IconTime = true;
        }
        if (battlemodeTimer > 4)
        {
            IconTime = false;
        }
        if (battlemodeTimer > 5)
        {
            Score1.SetActive(false);
            Score2.SetActive(false);
            Player1Control.speed = 8f;
            Player2Control.speed = 8f;
        }
        if (battlemodeTimer > 6 && battlemodeTimer < 7)
        {

            if(player1Life > player2Life)
            {

                MatchBall.transform.position = new Vector3(playerPaddle2.transform.position.x, 4.3f, -0.7f);
                MatchBall.SetActive(true);
            }

            if (player1Life < player2Life)
            {

                MatchBall.transform.position = new Vector3(playerPaddle.transform.position.x, -4.5f, -0.7f);
                MatchBall.SetActive(true);
            }

            if (player1Life == player2Life && coinFlipMB == false)
            {
                int random = Random.Range(0, 2);
          
                if (random == 0)
                {

                    MatchBall.transform.position = new Vector3(playerPaddle2.transform.position.x, 4.3f, -0.7f);
                    MatchBall.SetActive(true);
                    coinFlipMB = true;


                }
                else if (random == 1)
                {
                    MatchBall.transform.position = new Vector3(playerPaddle.transform.position.x, -4.5f, -0.7f);
                    MatchBall.SetActive(true);
                    coinFlipMB = true;
                }
               
            }
        }
    }
}
