using UnityEngine;
using System.Collections;

public class PreviewBall : MonoBehaviour 
{
	public Rigidbody rb;
	public GameObject playerPaddle;
    public GameObject playerPaddle2;

	// Use this for initialization
	void Start() 
	{
		rb = GetComponent<Rigidbody>();
		rb.AddForce(Random.Range(-600f, 600f), 600, 0);
	}
	// Update is called once per frame
	void Update() 
	{
        rb.velocity = 6 * (rb.velocity.normalized);
	}

	void OnCollisionEnter(Collision collision)
    {
		foreach (ContactPoint contact in collision.contacts)
        {
            if (collision.transform.tag == "Player1Paddle")
            {
                playerPaddle.transform.position = new Vector3(playerPaddle.transform.position.x+Random.Range(-2, 2), playerPaddle.transform.position.y, playerPaddle.transform.position.z);
                float winkel = contact.point.x - playerPaddle.transform.position.x;
                float winkelX = winkel / (playerPaddle.transform.localScale.x / 2);
                rb.velocity = new Vector3(winkelX , rb.velocity.y, 0);
            }

            if (collision.transform.tag == "Player2Paddle")
            {
                playerPaddle2.transform.position = new Vector3(playerPaddle2.transform.position.x+Random.Range(-2, 2), playerPaddle2.transform.position.y, playerPaddle2.transform.position.z);
                float winkel = contact.point.x - playerPaddle2.transform.position.x;
                float winkelX2 = winkel / (playerPaddle2.transform.localScale.x / 2);
                rb.velocity = new Vector3(winkelX2 , rb.velocity.y, 0);
            }
        }
    }
}