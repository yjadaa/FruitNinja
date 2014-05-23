using UnityEngine;
using System.Collections;

public class Fruit3Enemy : MonoBehaviour {

	private int hits;
    public GameObject Destroyed;
	// Use this for initialization
	void Start () {
		hits = 2;
	}
	
	// Update is called once per frame
	void Update () {
		if( hits == 0 )
			{
                GameObject DestroyObject = Instantiate(Destroyed, transform.position, transform.rotation) as GameObject;
                Destroy(DestroyObject, 1);
				Destroy(gameObject);
			}
	}
	
	void OnTriggerEnter(Collider other) {
        if(other.name == "RightSwordCollider" || other.name == "LeftSwordCollider")
		{
			hits--;
		}
    }
}
