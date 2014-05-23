using UnityEngine;
using System.Collections;

public class Selector : MonoBehaviour {

    private GameObject selectedObject = null;
    GameObject rightArm;
    PlayerBehavior pb;
	GameObject HookCollider;
	HookSelect hs;
	public AudioClip ThrowingSound;


	// Use this for initialization
	void Start () {
        rightArm = GameObject.FindWithTag("HookController");
        pb = GameObject.FindWithTag("Player").GetComponent<PlayerBehavior>();
		HookCollider = GameObject.FindWithTag("HookCollider");
		hs = HookCollider.GetComponent<HookSelect>();
		this.Disable();
		this.Hide();
	}
	
	// Update is called once per frame
	void Update () {
		selectedObject = hs.selectedObject;
        if (selectedObject != null)
        {
            selectedObject.transform.position = transform.position;
			this.Disable();
        }
	}


    public void Throw()
    {
        
        if (selectedObject != null)
        {
			audio.PlayOneShot(ThrowingSound, 1f);
            selectedObject.rigidbody.detectCollisions = true;
            Hook hook = rightArm.GetComponent<Hook>();
            selectedObject.rigidbody.AddForce(hook.rightArmPos * 20000);
            selectedObject = null;
			hs.selectedObject = null;
        }
    }
	
	public void Enable() {
		HookCollider.collider.enabled = true;
	}
	
	public void Show() {
		this.renderer.enabled = true;
	}
	
	public void Hide() {
		this.renderer.enabled = false;
	}
	
	public void Disable() {
		HookCollider.collider.enabled = false;
	}
}
