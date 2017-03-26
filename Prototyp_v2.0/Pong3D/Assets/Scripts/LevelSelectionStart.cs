using UnityEngine;
using System.Collections;

public class LevelSelectionStart : MonoBehaviour 
{
	// Use this for initialization
	void Start(){}
	// Update is called once per frame
	void Update(){

		
	}

	public void onClick()
	{
		GetComponent<AudioSource>().Play();
		Application.LoadLevel("LevelSelection");
	}
}