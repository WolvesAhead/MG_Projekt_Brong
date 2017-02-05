using UnityEngine;
using System.Collections;

public class ShieldScript : MonoBehaviour 
{
    public float rotationspeed = 50f;
    
	// Use this for initialization
	void Start (){}
	// Update is called once per frame
	void Update () 
	{
    	transform.Rotate(Vector3.right * Time.deltaTime * rotationspeed, Space.World);
    }
}