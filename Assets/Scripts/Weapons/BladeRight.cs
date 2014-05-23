using UnityEngine;
using System.Collections;

public class BladeRight : MonoBehaviour {
	
	public AudioClip SwordHitSound;
    public GameObject Destroyed;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	 void OnTriggerEnter(Collider other) {
        if(other.name == "Fruit3" || other.name == "Fruit4" || other.name == "Apple01" || other.name == "Banana" || other.name == "Apple02" || other.name == "Strawbery(Clone)")
		{
			audio.PlayOneShot(SwordHitSound, 0.5f);
		}
		if(other.name == "Strawbery(Clone)" || other.name == "Banana")
		{
            GameObject DestroyObject = Instantiate(Destroyed, other.transform.position, other.transform.rotation) as GameObject;
            Destroy(DestroyObject, 1);
			Destroy(other.gameObject);
		}
    }
}
