using UnityEngine;
using System.Collections;

public class PreviewPlayer : MonoBehaviour 
{
	public Rigidbody rbball;
	private float speed = 12f;

  

    // Use this for initialization
    void Start() 
	{
        

    }
	
	// Update is called once per frame
	void Update() 

	{
        transform.position = new Vector3(rbball.transform.position.x, transform.position.y, transform.position.z);
        if (rbball.velocity.y < 0 )
        {/*
			if(rbball.transform.position.x > transform.position.x)
			{
				transform.position = Vector3.right * speed * Time.deltaTime;
			}
			else if(rbball.transform.position.x < transform.position.x)
			{
				transform.position = Vector3.left * speed * Time.deltaTime;
			}*/
            transform.position = new Vector3(rbball.transform.position.x , transform.position.y, transform.position.z);
        }
		/*else
		{
			if(rbball.transform.position.x + 0.2 > transform.position.x)
			{
				transform.position += Vector3.right * Time.deltaTime;
			}
			else if(rbball.transform.position.x + 0.2 < transform.position.x)
			{
				transform.position += Vector3.left * Time.deltaTime;
			}
		}*/


	}


}
