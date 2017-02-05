using UnityEngine;
using System.Collections;

public class Level1Start : MonoBehaviour 
{
	// Use this for initialization
	void Start(){}
	// Update is called once per frame
	void Update(){}

	public void onClick()
	{
		GetComponent<AudioSource>().Play();
		Application.LoadLevel("Brong");
	}
}