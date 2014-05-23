using UnityEngine;
using System.Collections;

public class StrawberyEnemy : MonoBehaviour {

	public float MoveSpeed= 15f;
	public float AttackRange = 1000f;
	public float TouchRange = 0f;
	
	
	// Use this for initialization
	void Start () 
	{
		GameObject gamePlayer = GameObject.FindWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () 
	{
		//if close to player, damage him
		if(isTouching())
		{
			
		}
		else if(isClose())
		{
			//if player in range, orient and move towards him
			
			transform.LookAt(Camera.main.transform.position);
			Vector3 moveDirection = Camera.main.transform.position - transform.position;
			moveDirection.Normalize();
			
			transform.position = transform.position + moveDirection * MoveSpeed * Time.deltaTime;
			
		}
	}
	
	void OnTriggerEnter(Collider other) {
        if(other.name == "First Person Controller")
		{
			Destroy(this.gameObject);
		}
    }
	
	public bool isClose()
	{
		return Vector3.Distance(Camera.main.transform.position, transform.position) < AttackRange;
	}
	
	public bool isTouching()
	{
		return Vector3.Distance(Camera.main.transform.position, transform.position) < TouchRange;
	}
}
