#region # Using Reference #
using UnityEngine;
using System;
using System.Collections;
#endregion

public static class KCameraMove
{
    #region # Kinect #
    public static bool KinectLeft(KUInterface kin)
    {
        if (kin == null) return false;

        bool val = false;
        Vector3 leftWrist = kin.GetJointPos(KinectWrapper.Joints.WRIST_LEFT);
        Vector3 leftElbow = kin.GetJointPos(KinectWrapper.Joints.ELBOW_LEFT);

        if (Math.Abs(leftWrist.x - leftElbow.x) > Vector3.Distance(leftWrist, leftElbow) * 0.9)
            val = true;

        return val;
    }

    public static bool KinectRight(KUInterface kin)
    {
        if (kin == null) return false;

        bool val = false;
        Vector3 rightWrist = kin.GetJointPos(KinectWrapper.Joints.WRIST_RIGHT);
        Vector3 rightElbow = kin.GetJointPos(KinectWrapper.Joints.ELBOW_RIGHT);

        if (Math.Abs(rightWrist.x - rightElbow.x) > Vector3.Distance(rightWrist, rightElbow) * 0.9)
            val = true;

        return val;
    }
    #endregion
}
