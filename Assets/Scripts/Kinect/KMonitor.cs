#region # Using Reference #
using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
#endregion

public class KMonitor : MonoBehaviour
{
    private List<Vector3> _feet;
    private KUInterface kin;
    private bool _isInitialized;
    public bool IsInitialized { get { return this._isInitialized; } }
    private int MAXSIZE = 30;
    private int sizeCount;

    public float groundY;

    private void Start()
    {
        this.kin = GetComponent<KUInterface>();
        this._feet = new List<Vector3>();
        this.sizeCount = 0;
        this._isInitialized = false;
    }

    private void Update()
    {
       // return;
        if (!kin.isReady) return;
        if (!this._isInitialized) InitializeKinect();

        if (sizeCount < MAXSIZE)
        {
            this._feet.Add(kin.GetJointPos(KinectWrapper.Joints.FOOT_LEFT) - kin.GetJointPos(KinectWrapper.Joints.FOOT_RIGHT));
            sizeCount++;
        }
        //else
        //{
        //    this._feet.RemoveAt(0);
        //    this._feet.Add(kin.GetJointPos(KinectWrapper.Joints.FOOT_LEFT) - kin.GetJointPos(KinectWrapper.Joints.FOOT_RIGHT));
        //}
    }

    private float AverageGround()
    {
        float ground = 0;
        foreach (Vector3 ft in this._feet)
        {
            ground += ft.y;
        }
        ground /= this._feet.Count;
        return ground;
    }

    public void InitializeKinect()
    {
        float sum = 0;
        foreach (Vector3 v in this._feet)
        {
            sum += v.z;
        }

        if (Math.Abs(sum / MAXSIZE) > 0 && sizeCount == MAXSIZE)
        {
            this._isInitialized = true;
        }
    }
}
