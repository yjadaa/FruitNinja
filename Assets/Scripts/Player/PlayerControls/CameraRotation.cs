#region # Using Reference #
using UnityEngine;
using System;
using System.Collections;
#endregion

public class CameraRotation : MonoBehaviour
{
    #region # Properties #
    private KUInterface kin;
    private string KinectTag = "Kinect";

    private float shift;
    private float rotVelocity = 10;
    private float slowDown = 40;
    private string leftRotComm = "q";
    private string rightRotComm = "e";

	float reOrigin;
	
	private GameObject gestureObject;
    #endregion

    #region # Inherit Methods #
    private void Start()
    {
        this.shift = 0;
		gestureObject = GameObject.FindWithTag( "Gesture" );
        if (this.rigidbody)
            this.rigidbody.freezeRotation = true;
		reOrigin = 200.5f;
		
    }

    private void Update()
    {
        if (kin == null) SetUpKinect();
		
		GestureRecognizer gRec = gestureObject.GetComponent<GestureRecognizer>();
		
		
        if (IsLeftRotation())
        {
            this.shift = Mathf.Clamp(this.shift - 1f * this.rotVelocity * Time.deltaTime, -this.rotVelocity, 0);
        }
        else if (IsRightRotation())
        {
            this.shift = Mathf.Clamp(this.shift + 1f * this.rotVelocity * Time.deltaTime, 0, this.rotVelocity);
        }
        else
        {
            if (this.shift > 0)
            {
                this.shift = Mathf.Clamp(this.shift - 1f * this.slowDown * Time.deltaTime, 0, this.rotVelocity);
            }
            else
            {
                this.shift = Mathf.Clamp(this.shift + 1f * this.slowDown * Time.deltaTime, -this.rotVelocity, 0);
            }
        }

        this.transform.Rotate(0, this.shift, 0);
        //if((gRec.rightArm == GestureRecognizer.ARM_STATE.AS_NEG_Y && gRec.leftArm == GestureRecognizer.ARM_STATE.AS_NEG_Y))
        //{
        //    this.transform.Rotate(0, this.shift, 0);
        //} else {
        //    this.transform.eulerAngles = new Vector3(0,reOrigin,0);
        //}
    }
    #endregion

    #region # Methods #
    private void SetUpKinect()
    {
        GameObject kinectContainer = GameObject.FindWithTag(KinectTag);
        if (kinectContainer != null)
        {
            KMonitor km = kinectContainer.GetComponent<KMonitor>();
            if (km.IsInitialized)
                this.kin = kinectContainer.GetComponent<KUInterface>();
        }
    }

    private bool IsLeftRotation()
    {
        bool val = false;
       // if (Input.GetKey(this.leftRotComm) || KCameraMove.KinectLeft(kin))
            if (Input.GetKey(this.leftRotComm) )
            val = true;
        return val;
    }

    private bool IsRightRotation()
    {
        bool val = false;
      //  if (Input.GetKey(this.rightRotComm) || KCameraMove.KinectRight(kin)
             if (Input.GetKey(this.rightRotComm))
            val = true;
        return val;
    }
    #endregion
}
