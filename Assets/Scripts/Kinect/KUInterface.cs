/************************************************************************
*                                                                       *
*				      KinectSDK-Unity3D C# Wrapper:                     *
*	Attach to a GameObject and ensure that KUInterfaceCPP.dll is in     *
*	 the game's working directory. Call KUInterface.GetJointPos to      *
*                 retrieve joint position information.                  *
*		   (see included BSD license for licensing information)         *
************************************************************************/

using UnityEngine;
using System;
using System.Runtime.InteropServices;
using System.Collections;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Text;

public class KUInterface : MonoBehaviour {

    public bool isReady = false;
    public int scaleFactor = 1000;  //scales joint positions

    private float cameraAngle;
    private float lastCameraAngleChange = -50.0f;

    void Start() {

        //initialize Kinect sensor
        KinectWrapper.NuiContextInit(ref isReady);

        if (isReady) {
            UnityEngine.Debug.Log("Sensor Initialized.");
        } else {
            UnityEngine.Debug.Log("Could Not Initialize Sensor.");
        }

        if (scaleFactor == 0) {
            UnityEngine.Debug.Log("WARNING: KUInterface.scaleFactor is set to zero. All joint positions will be the zero vector.");
        }
    }


    public Vector3 GetJointPos(KinectWrapper.Joints joint) {

        KinectWrapper.SkeletonTransform trans = new KinectWrapper.SkeletonTransform();
        KinectWrapper.GetSkeletonTransform((int)joint, ref trans);       
        return(new Vector3(trans.x * scaleFactor, trans.y * scaleFactor, trans.z * scaleFactor));
    }


    public float GetCameraAngle() {

        KinectWrapper.GetCameraAngle(ref cameraAngle);
        return (cameraAngle);
    }


    public bool SetCameraAngle(int angle) {

        if (Time.time - lastCameraAngleChange > 30) {
            lastCameraAngleChange = Time.time;
            return (KinectWrapper.SetCameraAngle(angle));
        } else {
            return (false);
        }
    }


    void OnApplicationQuit() {

        KinectWrapper.NuiContextUnInit();
        isReady = false;
        UnityEngine.Debug.Log("Sensor Uninitialized.");
    }


    void Update() {
		
		if(!isReady) return;
		
        KinectWrapper.NuiUpdate();
    }
	
	void OnGUI()
	{
		if(!isReady) return;
		
		//displayPositions();
	}
	
	private void displayPositions()
	{
		Rect pos;
		Vector3 vec;
		
		vec = GetJointPos(KinectWrapper.Joints.HEAD);
		pos = new Rect(30,60,300,30);
		GUI.Label(pos, String.Format("Head : {0:0.00},{1:0.00},{2:0.00}" , vec.x, vec.y,vec.z));
		
		vec = GetJointPos(KinectWrapper.Joints.SHOULDER_CENTER);
		pos = new Rect(30,90,300,30);
		GUI.Label(pos, String.Format("Neck : {0:0.00},{1:0.00},{2:0.00} " , vec.x, vec.y,vec.z));
		
		vec = GetJointPos(KinectWrapper.Joints.SHOULDER_LEFT);
		pos = new Rect(30,120,300,30);
		GUI.Label(pos, String.Format("Left Shoulder : {0:0.00},{1:0.00},{2:0.00} " , vec.x, vec.y,vec.z));
		
		vec = GetJointPos(KinectWrapper.Joints.SHOULDER_RIGHT);
		pos = new Rect(30,150,300,30);
		GUI.Label(pos, String.Format("Right Shoulder : {0:0.00},{1:0.00},{2:0.00} " , vec.x, vec.y,vec.z));
		
		vec = GetJointPos(KinectWrapper.Joints.ELBOW_LEFT);
		pos = new Rect(30,180,300,30);
		GUI.Label(pos, String.Format("Left Elbow : {0:0.00},{1:0.00},{2:0.00} " , vec.x, vec.y,vec.z));
		
		vec = GetJointPos(KinectWrapper.Joints.ELBOW_RIGHT);
		pos = new Rect(30,210,300,30);
		GUI.Label(pos, String.Format("Right Elbow : {0:0.00},{1:0.00},{2:0.00} " , vec.x, vec.y,vec.z));
		
		vec = GetJointPos(KinectWrapper.Joints.WRIST_LEFT);
		pos = new Rect(30,240,300,30);
		GUI.Label(pos, String.Format("Left Wrist : {0:0.00},{1:0.00},{2:0.00} " , vec.x, vec.y,vec.z));
		
		vec = GetJointPos(KinectWrapper.Joints.WRIST_RIGHT);
		pos = new Rect(30,270,300,30);
		GUI.Label(pos, String.Format("Right Wrist : {0:0.00},{1:0.00},{2:0.00} " , vec.x, vec.y,vec.z));
		
		vec = GetJointPos(KinectWrapper.Joints.SPINE);
		pos = new Rect(30,300,300,30);
		GUI.Label(pos, String.Format("Spine : {0:0.00},{1:0.00},{2:0.00} " , vec.x, vec.y,vec.z));
		
		vec = GetJointPos(KinectWrapper.Joints.KNEE_LEFT);
		pos = new Rect(30,330,300,30);
		GUI.Label(pos, String.Format("Left Knee : {0:0.00},{1:0.00},{2:0.00} " , vec.x, vec.y,vec.z));
		
		vec = GetJointPos(KinectWrapper.Joints.KNEE_RIGHT);
		pos = new Rect(30,360,300,30);
		GUI.Label(pos, String.Format("Right Knee : {0:0.00},{1:0.00},{2:0.00} " , vec.x, vec.y,vec.z));
		
		vec = GetJointPos(KinectWrapper.Joints.FOOT_LEFT);
		pos = new Rect(30,390,300,30);
		GUI.Label(pos, String.Format("Left Foot : {0:0.00},{1:0.00},{2:0.00} " , vec.x, vec.y,vec.z));
		
		vec = GetJointPos(KinectWrapper.Joints.FOOT_RIGHT);
		pos = new Rect(30,420,300,30);
		GUI.Label(pos, String.Format("Right Foot : {0:0.00},{1:0.00},{2:0.00} " , vec.x, vec.y,vec.z));
		
		
	}
	
	
}


//-------------------------------------------------------------------------------------


public class KinectWrapper {  //interfaces with DLL

    public enum Joints {
        HIP_CENTER = 0,
        SPINE,
        SHOULDER_CENTER,
        HEAD,
        SHOULDER_LEFT,
        ELBOW_LEFT,
        WRIST_LEFT,
        HAND_LEFT,
        SHOULDER_RIGHT,
        ELBOW_RIGHT,
        WRIST_RIGHT,
        HAND_RIGHT,
        HIP_LEFT,
        KNEE_LEFT,
        ANKLE_LEFT,
        FOOT_LEFT,
        HIP_RIGHT,
        KNEE_RIGHT,
        ANKLE_RIGHT,
        FOOT_RIGHT,
        COUNT
    };


    [StructLayout(LayoutKind.Sequential)]
    public struct SkeletonTransform {

        public float x, y, z, w;
    }


    [StructLayout(LayoutKind.Sequential)]
    public struct KUVector4 {

        public float x, y, z, w;
    }


    //NUI Context Management
    [DllImport("KUInterfaceCPP")]
    public static extern void NuiContextInit(ref bool status);
    [DllImport("KUInterfaceCPP")]
    public static extern void NuiUpdate();
    [DllImport("KUInterfaceCPP")]
    public static extern void NuiContextUnInit();
    //Get Methods
    [DllImport("KUInterfaceCPP")]
    public static extern void GetSkeletonTransform(int joint, ref SkeletonTransform trans);
    [DllImport("KUInterfaceCPP")]
    public static extern void GetCameraAngle(ref float angle);
    //Set Methods
    [DllImport("KUInterfaceCPP")]
    public static extern bool SetCameraAngle(int angle);
}