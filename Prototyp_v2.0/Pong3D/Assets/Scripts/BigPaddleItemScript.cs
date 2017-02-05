using UnityEngine;
using System.Collections;

public class BigPaddleItemScript : MonoBehaviour 
{
    // Use this for initialization
    GameObject paddle1;
    GameObject paddle2;

    void Start () 
    {
	    paddle1 = GameObject.Find("Player1Paddle");
        paddle2 = GameObject.Find("Player2Paddle");
    }
	// Update is called once per frame
	void Update (){}
    void OncollisonEnter (Collision col)
    {
        if (col.gameObject.transform.tag == "Player1paddle")
        {
            paddle1.gameObject.transform.localScale += new Vector3(3f, 0, 0);
        }
        
        if(col.gameObject.transform.tag == "Player2paddle")
        {
            paddle2.gameObject.transform.localScale += new Vector3(3f, 0, 0);
        }
    }
}