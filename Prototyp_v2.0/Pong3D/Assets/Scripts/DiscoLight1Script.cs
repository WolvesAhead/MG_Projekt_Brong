using UnityEngine;
using System.Collections;

public class DiscoLight1Script : MonoBehaviour 
{
	public Light lt;

	// Use this for initialization
	void Start () 
	{
		lt = GetComponent<Light>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		float newColor1 = 1f;
		float newColor2 = 1f;
		float newColor3 = 1f;

		lt.color -= Color.white / 2.0f * Time.deltaTime;
		
		Debug.Log(lt.color);
	}
}
