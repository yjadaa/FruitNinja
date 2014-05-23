#region # Using Reference #
using UnityEngine;
using System;
using System.Collections;
#endregion

public static class KPlayerMove
{
    #region # Constants #
    private const float crouchAngle = 160;
    private const float jumpKneeAngle = 110;
    #endregion

    #region # Kinect Movements #
    public static bool StartKinect(KUInterface kin)
    {
        if (kin == null) return false;

        bool val = false;
        if (kin.GetJointPos(KinectWrapper.Joints.HEAD).z > 30)
            val = true;

        return val;
    }

    #region # Navigation #
    public static bool KinectForward(KUInterface kin)
    {
        if (kin == null) return false;

        bool val = false;
        Vector3 rightAnkle = kin.GetJointPos(KinectWrapper.Joints.ANKLE_RIGHT);
        Vector3 leftAnkle = kin.GetJointPos(KinectWrapper.Joints.ANKLE_LEFT);
        Vector3 leftFoot = kin.GetJointPos(KinectWrapper.Joints.FOOT_LEFT);

        if (leftAnkle.z - rightAnkle.z > Vector3.Distance(leftAnkle, leftFoot))
        {
            val = true;
        }

        return val;
    }

    public static bool KinectBack(KUInterface kin)
    {
        if (kin == null) return false;

        bool val = false;
        Vector3 rightAnkle = kin.GetJointPos(KinectWrapper.Joints.ANKLE_RIGHT);
        Vector3 leftAnkle = kin.GetJointPos(KinectWrapper.Joints.ANKLE_LEFT);
        Vector3 leftFoot = kin.GetJointPos(KinectWrapper.Joints.FOOT_LEFT);

        if (leftAnkle.z - rightAnkle.z < -Vector3.Distance(leftAnkle, leftFoot))
        {
            val = true;
        }

        return val;
    }

    public static bool KinectRight(KUInterface kin)
    {
        if (kin == null) return false;

        bool val = false;
        Vector3 shoulderCenter = kin.GetJointPos(KinectWrapper.Joints.SHOULDER_CENTER);
        Vector3 hipCenter = kin.GetJointPos(KinectWrapper.Joints.HIP_CENTER);

        Vector3 upperBody = shoulderCenter - hipCenter;
        if (upperBody.normalized.x > 0.15)
            val = true;

        return val;
    }

    public static bool KinectLeft(KUInterface kin)
    {
        if (kin == null) return false;

        bool val = false;
        Vector3 shoulderCenter = kin.GetJointPos(KinectWrapper.Joints.SHOULDER_CENTER);
        Vector3 hipCenter = kin.GetJointPos(KinectWrapper.Joints.HIP_CENTER);

        Vector3 upperBody = shoulderCenter - hipCenter;
        if (upperBody.normalized.x < -0.15)
            val = true;

        return val;
    }
    #endregion

    #region # Vertical Navigation #
    public static bool KinectJump(KUInterface kin)
    {
        if (kin == null) return false;

        bool val = false;

        Vector3 lowerLeftLeg = kin.GetJointPos(KinectWrapper.Joints.ANKLE_LEFT) - kin.GetJointPos(KinectWrapper.Joints.KNEE_LEFT);

        Vector3 lowerRightLeg = kin.GetJointPos(KinectWrapper.Joints.ANKLE_RIGHT) - kin.GetJointPos(KinectWrapper.Joints.KNEE_RIGHT);
        Vector3 upperRighLeg = kin.GetJointPos(KinectWrapper.Joints.HIP_RIGHT) - kin.GetJointPos(KinectWrapper.Joints.KNEE_RIGHT);

        if (Vector3.Angle(lowerRightLeg, upperRighLeg) < jumpKneeAngle
             && kin.GetJointPos(KinectWrapper.Joints.ANKLE_RIGHT).y - kin.GetJointPos(KinectWrapper.Joints.ANKLE_LEFT).y > Vector3.Magnitude(lowerLeftLeg) * 0.6)
            val = true;

        return val;
    }

    public static bool KinectFly(KUInterface kin)
    {
        if (kin == null) return false;

        bool val = false;
        Vector3 leftLowerArm = kin.GetJointPos(KinectWrapper.Joints.WRIST_LEFT) - kin.GetJointPos(KinectWrapper.Joints.ELBOW_LEFT);
        Vector3 rightLowerArm = kin.GetJointPos(KinectWrapper.Joints.WRIST_RIGHT) - kin.GetJointPos(KinectWrapper.Joints.ELBOW_RIGHT);
        Vector3 leftHand = kin.GetJointPos(KinectWrapper.Joints.HAND_LEFT);
        Vector3 head = kin.GetJointPos(KinectWrapper.Joints.HEAD);

        if (leftLowerArm.normalized.y > 0.9 && rightLowerArm.normalized.y < -0.9 && leftHand.y > head.y)
            val = true;

        return val;
    }

    public static bool KinectCrouch(KUInterface kin)
    {
        if (kin == null) return false;

        bool val = false;
        Vector3 lowerLeftLeg = kin.GetJointPos(KinectWrapper.Joints.ANKLE_LEFT) - kin.GetJointPos(KinectWrapper.Joints.KNEE_LEFT);
        Vector3 upperLeftLeg = kin.GetJointPos(KinectWrapper.Joints.HIP_LEFT) - kin.GetJointPos(KinectWrapper.Joints.KNEE_LEFT);

        Vector3 lowerRightLeg = kin.GetJointPos(KinectWrapper.Joints.ANKLE_RIGHT) - kin.GetJointPos(KinectWrapper.Joints.KNEE_RIGHT);
        Vector3 upperRighLeg = kin.GetJointPos(KinectWrapper.Joints.HIP_RIGHT) - kin.GetJointPos(KinectWrapper.Joints.KNEE_RIGHT);

        if (Vector3.Angle(lowerLeftLeg, upperLeftLeg) < crouchAngle && Vector3.Angle(lowerRightLeg, upperRighLeg) < crouchAngle)
            val = true;

        return val;
    }
    #endregion

    #region # Head Tilting #
    public static bool KinectHeadLeftTilt(KUInterface kin)
    {
        if (kin == null) return false;

        bool val = false;
        Vector3 neck = kin.GetJointPos(KinectWrapper.Joints.HEAD) - kin.GetJointPos(KinectWrapper.Joints.SHOULDER_CENTER);

        if (neck.normalized.x < -0.15)
            val = true;

        return val;
    }

    public static bool KinectHeadRightTilt(KUInterface kin)
    {
        if (kin == null) return false;

        bool val = false;
        Vector3 neck = kin.GetJointPos(KinectWrapper.Joints.HEAD) - kin.GetJointPos(KinectWrapper.Joints.SHOULDER_CENTER);

        if (neck.normalized.x > 0.15)
            val = true;

        return val;
    }
    #endregion


    #endregion
}
