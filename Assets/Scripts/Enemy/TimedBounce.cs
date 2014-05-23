using UnityEngine;
using System.Collections;

public class TimedBounce : MonoBehaviour {

	public float BounceSpeed = 5f;
	public float MaxBounce = 0.5f;
	public Vector3 BounceDirection = new Vector3(0,1,0);
	
	private bool bounceUp = true;
	private float bounceAmount = 0.0f;
	private float bounceIncrement; 
	
	// Use this for initialization
	void Start () 
	{
		//amount of distance to be travelled per frame
		bounceIncrement = Time.deltaTime * BounceSpeed;
	}
	
	// Update is called once per frame
	void Update () {
	
		Vector3 amt =  BounceDirection * bounceIncrement;
		
		if(bounceUp)
		{
			bounceAmount += bounceIncrement;
			if(bounceAmount > MaxBounce)
				bounceUp = false;
		}
		else
		{
			bounceAmount -= bounceIncrement;
			amt = -1*amt;
			if(bounceAmount < -MaxBounce)
				bounceUp = true;
		}

			transform.Translate(amt);		
		
	}
}
