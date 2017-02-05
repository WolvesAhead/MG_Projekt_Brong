using UnityEngine;
using System.Collections;

public class ItemPrefabScript : MonoBehaviour 
{
    private Collider ColliderItem;
    
	// Use this for initialization
	void Start (){}
    // Update is called once per frame
    void Update() 
    {
        transform.Rotate(Vector3.up * Time.deltaTime*300, Space.World);

        if (transform.position.y < -7f || transform.position.y > 7f)
        {
            Destroy(gameObject);
        }
    }
 
    void OnCollisionEnter(Collision col)
    {
        Destroy(gameObject);
    }   
}