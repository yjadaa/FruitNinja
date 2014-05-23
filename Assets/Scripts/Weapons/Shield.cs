using UnityEngine;
using System.Collections;

public class Shield : MonoBehaviour {
	
	public AudioClip ShieldSound;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	 void OnTriggerEnter(Collider other) {
        if(other.name == "Fruit3" || other.name == "Strawbery(Clone)" || other.name == "Apple01" || other.name == "Banana" || other.name == "Apple02")
		{
			audio.PlayOneShot(ShieldSound, 1f);
			Destroy(other.gameObject);			
		}
    }
}
