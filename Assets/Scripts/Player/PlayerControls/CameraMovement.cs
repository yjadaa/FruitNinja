#region # Using Reference #
using UnityEngine;
using System;
using System.Collections;
#endregion

public class CameraMovement : MonoBehaviour
{
    #region # Properties #
    private KUInterface kin;
    private string KinectTag = "Kinect";
    private GameObject gestureObject;
    float reOrigin;
    GestureRecognizer gRec;
    private float cameraSmoothing = 3;

    private float shift;
    private float rotVelocity = 5;
    private float slowDown = 50;
    private string leftRotComm = "q";
    private string rightRotComm = "e";
    private float rotationAngle;

    #endregion

    private void Start()
    {
        gestureObject = GameObject.FindWithTag("Gesture");
        this.rotationAngle = 0;
        reOrigin = 200.5f;
    }

    private void Update()
    {
        if (kin == null) SetUpKinect();
        gRec = gestureObject.GetComponent<GestureRecognizer>();
        this.shift = ShiftCamera(this.shift);
        this.rotationAngle += this.shift;
        CameraRotate();
    }

    #region # Y Rotation #

    private float ShiftCamera(float shft)
    {
        float val = 0;

        if (IsLeftRotation())
        {
            val = Mathf.Clamp(shft - 1f * this.rotVelocity * Time.deltaTime, -this.rotVelocity, 0);
        }
        else if (IsRightRotation())
        {
            val = Mathf.Clamp(shft + 1f * this.rotVelocity * Time.deltaTime, 0, this.rotVelocity);
        }
        else // gradual stop
        {
            if (shft > 0)
            {
                this.shift = Mathf.Clamp(shft - 1f * this.slowDown * Time.deltaTime, 0, this.rotVelocity);
            }
            else
            {
                val = Mathf.Clamp(shft + 1f * this.slowDown * Time.deltaTime, -this.rotVelocity, 0);
            }
        }

        return val;
    }

    private bool IsLeftRotation()
    {
        bool val = false;
        if (Input.GetKey(this.leftRotComm) || KPlayerMove.KinectHeadLeftTilt(this.kin))
            val = true;
        return val;
    }

    private bool IsRightRotation()
    {
        bool val = false;
        if (Input.GetKey(this.rightRotComm) || KPlayerMove.KinectHeadRightTilt(this.kin))
            val = true;
        return val;
    }
    #endregion

    #region # Camera Shifting #
    private void CameraRotate()
    {
        if (this.kin == null) return;

        Vector3 rotateSpine = kin.GetJointPos(KinectWrapper.Joints.SHOULDER_CENTER) - kin.GetJointPos(KinectWrapper.Joints.HIP_CENTER);
        Vector3 rotateShoulder = kin.GetJointPos(KinectWrapper.Joints.SHOULDER_LEFT) - kin.GetJointPos(KinectWrapper.Joints.SHOULDER_RIGHT);

        float xRot = (float)Math.Asin(rotateSpine.normalized.z);
        float yRot = (float)Math.Asin(rotateShoulder.normalized.z);
        Quaternion target = Quaternion.Euler(new Vector3(-(float)(180.0f / Math.PI * xRot), this.rotationAngle - (float)(180.0f / Math.PI * yRot), 0));
        //if ((gRec.rightArm == GestureRecognizer.ARM_STATE.AS_NEG_Y && gRec.leftArm == GestureRecognizer.ARM_STATE.AS_NEG_Y))

        Vector3 leftHip = kin.GetJointPos(KinectWrapper.Joints.HIP_LEFT); 
        Vector3 leftHand = kin.GetJointPos(KinectWrapper.Joints.HAND_LEFT); 
        Vector3 rightHip = kin.GetJointPos(KinectWrapper.Joints.HIP_RIGHT); 
        Vector3 rightHand = kin.GetJointPos(KinectWrapper.Joints.HAND_RIGHT); 
        if( leftHand.y < leftHip.y && rightHand.y < rightHip.y)
        {
            this.transform.rotation = Quaternion.Slerp(transform.rotation, target, Time.deltaTime * cameraSmoothing);
        }
        //else
        //{
            //this.transform.eulerAngles = new Vector3(0, reOrigin, 0);
        //}
        
    }
    #endregion

    #region # Initialize Kinect #
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
    #endregion
}
