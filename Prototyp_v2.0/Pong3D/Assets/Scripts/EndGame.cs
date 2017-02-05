using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EndGame : MonoBehaviour
{
    public bool gameEnded = false;
    public static bool endgameStarted = false;
    public float scoreTimer = 0f;
    public static int player1Life;
    public static int player2Life;
    public BallsScript bs;
    public BallsScript2 bs2;
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
    void Start(){}
    // Update is called once per frame
    void Update()
    {
        lifeBorder1 = player1Life * 0.19999f;
        lifeBorder2 = player2Life * 0.19999f;
       
        if ((Input.GetKey(KeyCode.W)|| Input.GetKey(KeyCode.UpArrow)) && gameEnded == true)
        { 
            BlockPhys.brickZähler = 104;
            BlockPhys2.brickZähler = 112;
            BlockPhys3.brickZähler = 187;
            endgameStarted = false;
            Application.LoadLevel("MainMenu");
        }

        if (MatchBallScript.P1Torkassiert == true )
        {
            player1Life = player1Life -( 1/2);
            lifeBorder1 = player1Life * 0.19999f;
            Healthbar1.fillAmount = lifeBorder1;
            MatchBallScript.P1Torkassiert = false;
            
            if(player1Life < 1)
            {   
                WinLose2.SetActive(true);
                playerPaddle.SetActive(false);
                playerPaddle2.SetActive(false);
                Score1.SetActive(false);
                Score2.SetActive(false);
                MatchBall.SetActive(false);
                Healthbar1.fillAmount = 0;
                Healthbar2.fillAmount = 0;
                gameEnded = true;
            }
        }

        if (MatchBallScript.P2Torkassiert == true)
        {
            player2Life = player2Life -(1/2);
            lifeBorder2 = player2Life * 0.19999f;
            Healthbar2.fillAmount = lifeBorder2;
            MatchBallScript.P2Torkassiert = false;

            if(player2Life < 1)
            {
                WinLose1.SetActive(true);
                playerPaddle.SetActive(false);
                playerPaddle2.SetActive(false);
                Score1.SetActive(false);
                Score2.SetActive(false);
                MatchBall.SetActive(false);
                Healthbar1.fillAmount = 0;
                Healthbar2.fillAmount = 0;
                gameEnded = true;
            }
        }

        if (BlockPhys.brickZähler == 0 || BlockPhys2.brickZähler == 0||BlockPhys3.brickZähler  == 0)
        {
            startEndgame();
            endgameStarted = true;

            if (LifesCount == false)
            {
                countLifes();

                if (player1Life < 1)
                {
                    WinLose2.SetActive(true);
                    MatchBall.SetActive(false);
                    Healthbar1.fillAmount = 0;
                    Healthbar2.fillAmount = 0;
                    playerPaddle.SetActive(false);
                    playerPaddle2.SetActive(false);
                    Score1.SetActive(false);
                    Score2.SetActive(false);
                    gameEnded = true;
                }

                if (player2Life < 1)
                {
                    WinLose1.SetActive(true);
                    playerPaddle.SetActive(false);
                    playerPaddle2.SetActive(false);
                    MatchBall.SetActive(false);
                    Healthbar1.fillAmount = 0;
                    Healthbar2.fillAmount = 0;
                    Score1.SetActive(false);
                    Score2.SetActive(false);
                    gameEnded = true;
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

        if (IconTime == true)
        {
            BattleModeIcon.fillAmount += 0.05f;
        }
        else
        {
            BattleModeIcon.fillAmount -= 0.05f;
        }
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
        LifesCount = true;
    }

    void startEndgame()
    {
        BallsScript.startposition = false;
        BallsScript2.startposition = false;
        rbball.velocity = new Vector3(0, 0, 0);
        rbball2.velocity = new Vector3(0, 0, 0);
        rbball.transform.position = new Vector3(rbball.transform.position.x, rbball.transform.position.y, 5);
        rbball2.transform.position = new Vector3(rbball2.transform.position.x, rbball2.transform.position.y, 5);
        battlemodeTimer += Time.deltaTime;
        bs.ResetPowerups();
        bs2.ResetPowerups2();

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