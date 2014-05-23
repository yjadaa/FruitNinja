using UnityEngine;
using System.Collections.Generic;

public class PlayerBehavior : MonoBehaviour
{

    private int lasersDeflected;
	private bool swordLeftflag = true;
	private bool swordRightflag = true;
	private bool shieldflag = false;
	public float FULL_HEALTH = 100f;
	public AudioClip GhostVoice;
	public AudioClip DamageSound;
	
	private Vector3 tempVect;
    private Vector3 tempVectSwordRight;
	private Vector3 randomPosition = new Vector3(-1000f,-1000f,-1000f);

	private GameObject swordLeftObject;
	private GameObject swordRightObject;
	private GameObject shieldObject;
	private GameObject shieldCollider;
    private HealthController hc;
	private GameObject player;
    private GestureRecognizer.ARM_STATE rightArm = GestureRecognizer.ARM_STATE.AS_NONE;
    private GestureRecognizer.ARM_STATE leftArm = GestureRecognizer.ARM_STATE.AS_NONE;
    public Gesture currentGesture = Gesture.NONE;
    private GameObject selector;
	public GameObject Destroyed;
	private ObjectScan radar;
	
	public float fruit3HealthDamage = 5f;
	public float fruit4HealthDamage = 15f;
	public float bananaHealthDamage = 3f;
	public float strawberyHealthDamage = 1f;
	public float AppleHealthDamage = 8f;

    public enum Gesture
    {
        SELECT,
        THROW,
        NONE
    };
	

    // Use this for initialization
    void Start()
    {
        selector = GameObject.FindWithTag("Selector");
		
		swordLeftObject = GameObject.FindWithTag("SwordLeft");
		swordRightObject = GameObject.FindWithTag("SwordRight");
		shieldObject = GameObject.FindWithTag("Shield");
		shieldCollider = GameObject.FindWithTag ("ShieldCollider");
		player = GameObject.FindWithTag("Player");
		radar = GameObject.FindWithTag("Radar").GetComponent<ObjectScan>();
        hc = player.GetComponent<HealthController>();
		tempVect = shieldObject.transform.position - transform.position;
        tempVectSwordRight = swordRightObject.transform.position - transform.position;
		//shieldObject.transform.position = randomPosition;
    }

    // Update is called once per frame
    void Update()
    {
		
        GameObject gestureObject = GameObject.FindWithTag( "Gesture" );
        if (gestureObject != null)
        {
            bool rChange = false;
            bool lChange = false;

            GestureRecognizer gRec = gestureObject.GetComponent<GestureRecognizer>();
            if (rightArm != gRec.rightArm)
            {
                rightArm = gRec.rightArm;
                rChange = true;
            }
            if (leftArm != gRec.leftArm)
            {
                leftArm = gRec.leftArm;
                lChange = true;
            }
			
             if (leftArm == GestureRecognizer.ARM_STATE.AS_POS_Y &&
                rightArm == GestureRecognizer.ARM_STATE.AS_POS_Y )
            {
                currentGesture = Gesture.SELECT;
                this.Select();
                
            }
            else if (leftArm == GestureRecognizer.ARM_STATE.AS_POS_Z &&
                rightArm == GestureRecognizer.ARM_STATE.AS_POS_Z &&
                currentGesture ==Gesture.SELECT)
            {
                //currentGesture = Gesture.THROW;
                this.Throw();
            }
			else if (leftArm == GestureRecognizer.ARM_STATE.AS_NEG_Y &&
                rightArm == GestureRecognizer.ARM_STATE.AS_NEG_Y &&
                currentGesture ==Gesture.SELECT) {
				Selector s = selector.GetComponent<Selector>();
		        s.Disable();
				s.Hide();
                if (swordRightflag == false && shieldflag == false)
                {
                    swordLeftflag = true;
                    swordRightflag = true;
                    //swordRightObject.transform.position = tempVectSwordRight + transform.position;
                }
                currentGesture = Gesture.NONE;
			}


			if (leftArm == GestureRecognizer.ARM_STATE.AS_NEG_X &&
                rightArm == GestureRecognizer.ARM_STATE.AS_POS_X && currentGesture != Gesture.SELECT)
			{
				swordLeftflag = false;
				swordRightflag = false;
				shieldflag = true;
			}  else if (currentGesture != Gesture.SELECT && currentGesture != Gesture.THROW) {
				swordLeftflag = true;
				swordRightflag = true;
				shieldflag = false;
			}
			if (leftArm == GestureRecognizer.ARM_STATE.AS_NEG_Y &&
                rightArm == GestureRecognizer.ARM_STATE.AS_POS_Y && currentGesture != Gesture.SELECT && currentGesture != Gesture.THROW)
			{
				audio.PlayOneShot(GhostVoice,0.1f);
				foreach(GameObject fruit in radar.fruits)
					{
                        if (fruit != null)
                        {
                            GameObject DestroyObject = Instantiate(Destroyed, fruit.transform.position, fruit.transform.rotation) as GameObject;
                            Destroy(DestroyObject, 5);
                            Destroy(fruit);
                        }
					}
				
			}
			swordLeftObject.active = swordLeftflag;
			swordRightObject.active = swordRightflag;
			if (shieldflag) {
				shieldObject.renderer.enabled = true;
				shieldCollider.collider.enabled =true;
			} else {
				shieldObject.renderer.enabled = false;
				shieldCollider.collider.enabled =false;
			}
        }
    }
	
	void OnTriggerEnter(Collider other) {
		 if(other.name == "Strawbery(Clone)" || other.name == "Fruit3" || other.name == "Fruit4" || other.name == "Apple01" || other.name == "Banana" || other.name == "Apple02")
		{
			
			audio.PlayOneShot(DamageSound, 0.05f);
			if(other.name == "Apple01" || other.name == "Apple02")
			{
				FULL_HEALTH = Mathf.Clamp(FULL_HEALTH - AppleHealthDamage, 0f,100f);
			}
			if (other.name == "Fruit3")
			{
				FULL_HEALTH = Mathf.Clamp(FULL_HEALTH - fruit3HealthDamage, 0f,100f);
			}
			if (other.name == "Fruit4")
			{
				FULL_HEALTH = Mathf.Clamp(FULL_HEALTH - fruit4HealthDamage, 0f,100f);
			}
			if (other.name == "Banana")
			{
				FULL_HEALTH = Mathf.Clamp(FULL_HEALTH - bananaHealthDamage, 0f,100f);
			}
			if (other.name == "Strawbery(Clone)")
			{
				FULL_HEALTH = Mathf.Clamp(FULL_HEALTH - strawberyHealthDamage, 0f,100f);
			}
            hc.Damage(FULL_HEALTH);
		}
    }
	
    public void Select()
    {
        swordLeftflag = false;
        swordRightflag = false;
        //swordRightObject.transform.position = randomPosition;
		Selector s = selector.GetComponent<Selector>();
        s.Enable();
		s.Show();
    }


    public void Throw()
    {
        Selector s = selector.GetComponent<Selector>();
        s.Throw();
    }

}
