using UnityEngine;
using System.Collections;

/// MouseLook rotates the transform based on the mouse delta.
/// Minimum and Maximum values can be used to constrain the possible rotation

/// To make an FPS style character:
/// - Create a capsule.
/// - Add the MouseLook script to the capsule.
///   -> Set the mouse look to use LookX. (You want to only turn character but not tilt it)
/// - Add FPSInputController script to the capsule
///   -> A CharacterMotor and a CharacterController component will be automatically added.

/// - Create a camera. Make the camera a child of the capsule. Reset it's transform.
/// - Add a MouseLook script to the camera.
///   -> Set the mouse look to use LookY. (You want the camera to tilt up and down like a head. The character already turns.)
//[AddComponentMenu("Camera-Control/Mouse Look")]
public class MouseLook : MonoBehaviour {



	public float sensitivityX = 15F;
	private Vector3 tempVectShield;
    private Vector3 tempVectSwordRight;
	private Vector3 randomPosition = new Vector3(-1000f,-1000f,-1000f);
	
	private GameObject swordLeftObject;
	private GameObject swordRightObject;
	private GameObject shieldObject;
	Vector3 reOrigin;
	
	public float rotate;
	private GameObject gestureObject;
	

	
	void Update ()
	{
		GestureRecognizer gRec = gestureObject.GetComponent<GestureRecognizer>();
		if (gRec.rightLeg == GestureRecognizer.LEG_STATE.AS_NONE && gRec.rightArm == GestureRecognizer.ARM_STATE.AS_POS_X && gRec.leftArm == GestureRecognizer.ARM_STATE.AS_NEG_Y) {
			rotate = 1;
		} else if (gRec.rightLeg == GestureRecognizer.LEG_STATE.AS_NONE && gRec.leftArm == GestureRecognizer.ARM_STATE.AS_NEG_X && gRec.rightArm == GestureRecognizer.ARM_STATE.AS_NEG_Y) {
			rotate = -1;
		} else {
			rotate = 0;

		}

		//if(transform.eulerAngles.y != 200.5) {
		transform.eulerAngles = new Vector3(0,200.5f,0);
		//}
		//transform.Rotate(reOrigin);
		//transform.rotation.Set(reOrigin.x,reOrigin.y,reOrigin.z,reOrigin.w);
		//transform.eulerAngles.Set( 0,reOrigin,0);
		Debug.Log ("Main" + transform.eulerAngles + "SwordRightObject" + swordRightObject.transform.localEulerAngles);
			
	

	}
	
	void Start ()
	{
		
		reOrigin = transform.eulerAngles;
		gestureObject = GameObject.FindWithTag( "Gesture" );
		swordLeftObject = GameObject.FindWithTag("SwordLeft");
		swordRightObject = GameObject.FindWithTag("SwordRight");
		shieldObject = GameObject.FindWithTag("Shield");
		rotate = 0;

	}
}