using UnityEngine;
using System.Collections;

public class PositionController : MonoBehaviour 
{

	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}
	
	void OnDrawGizmos()
	{
		Gizmos.DrawWireSphere(transform.position,1.0f);
	}
	
}
