using UnityEngine;
using System.Collections;

public class HookSelect : MonoBehaviour {
	
	public AudioClip HookSound;
	public GameObject selectedObject = null;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	 void OnTriggerEnter(Collider other) {
		if (other.name == "Apple01" || other.name == "Apple02") {
        	selectedObject = other.gameObject;
			audio.PlayOneShot(HookSound, 1f);
		}
    }
}
