using UnityEngine;
using System.Collections;

public class ItemPositionScript : MonoBehaviour 
{
    public Transform position1;
    public Transform position2;
    public Transform position3;
    public Transform position4;
    public Transform position5;
    public Transform position6;
    public GameObject shield;
    public GameObject glue;
    public GameObject controlchange;
    public GameObject shield2;
    public GameObject glue2;
    public GameObject controlchange2;

    // Use this for initialization
    void Start (){}
    // Update is called once per frame
    void Update()
    {
        if (Player1Control.shieldstatus)
        {
            shield.transform.position = position1.position;
        }
        else if (Player1Control.shieldstatus == false)
        {
            shield.transform.position = new Vector3(-1.7f, -7f, -0.7f);
        }

        if (Player1Control.gluestatus)
        {
            glue.transform.position = position2.position;
        }
        else if (Player1Control.gluestatus == false)
        {
            glue.transform.position = new Vector3(-1.7f, -7f, -0.7f);
        }

        if (Player1Control.controlChange)
        {
            controlchange.transform.position = position3.position;
        }
        else if (Player1Control.controlChange == false)
        {
            controlchange.transform.position = new Vector3(-1.7f, -7f, -0.7f);
        }

        if (Player2Control.shieldstatus)
        {
            shield2.transform.position = position4.position;
        }
        else if (Player2Control.shieldstatus == false)
        {
            shield2.transform.position = new Vector3(1.7f, 7f, -0.7f);
        }

        if (Player2Control.gluestatus)
        {
            glue2.transform.position = position5.position;
        }
        else if (Player2Control.gluestatus == false)
        {
            //gameObject.SetActive(false);
            glue2.transform.position = new Vector3(1.7f, 7f, 0.7f);
        }

        if (Player2Control.controlChange)
        {
            controlchange2.transform.position = position6.position;
        }
        else if (Player2Control.controlChange == false)
        {
            controlchange2.transform.position = new Vector3(1.7f, 7f, 0.7f);
        }
    }  
}