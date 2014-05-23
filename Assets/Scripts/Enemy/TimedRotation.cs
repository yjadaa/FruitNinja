using UnityEngine;
using System.Collections;

public class TimedRotation : MonoBehaviour {

	public float AmountOfRotationPerSecondInDegrees = 60.0f;
	public Vector3 RotationAxis = new Vector3(0,1,0);
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		transform.Rotate(RotationAxis,Time.deltaTime * AmountOfRotationPerSecondInDegrees);
	}
}
