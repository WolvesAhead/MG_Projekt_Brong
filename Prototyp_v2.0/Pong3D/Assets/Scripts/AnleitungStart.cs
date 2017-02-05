using UnityEngine;
using System.Collections;

public class AnleitungStart : MonoBehaviour
{
  	// Use this for initialization
    void Start(){}
    // Update is called once per frame
    void Update(){}

    public void onClick()
    {
        GetComponent<AudioSource>().Play();
        Application.LoadLevel("HowTo");
    }

    public void BackToMenu()
    {
        GetComponent<AudioSource>().Play();
        Application.LoadLevel("MainMenu");
    }
}