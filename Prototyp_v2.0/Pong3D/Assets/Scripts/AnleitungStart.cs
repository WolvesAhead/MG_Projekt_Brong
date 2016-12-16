using UnityEngine;
using System.Collections;

public class AnleitungStart : MonoBehaviour
{
  
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void onClick()
    {
        Application.LoadLevel("HowTo");
    }

    public void BackToMenu()
    {
        Application.LoadLevel("MainMenu");
    }
}