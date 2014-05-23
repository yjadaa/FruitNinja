#region # Using Reference #
using UnityEngine;
using System.Collections;
#endregion

public class SwordLeft : MonoBehaviour
{
    #region # Properties #
    private KUInterface kin;

    private float movementSpeed = 10.0f;
    private Vector3 refOrigin = new Vector2(-0.2f, -1.0f);
    private bool isInitialized = false;
    private Vector3 pointTo;
    #endregion

    #region # Inherit Methods #
    private void Start()
    {
        GameObject kinectContainer = GameObject.FindWithTag("Kinect");
        if (kinectContainer != null)
        {
            this.isInitialized = true;
            this.kin = kinectContainer.GetComponent<KUInterface>();
        }
    }

    private void Update()
    {
        if (!this.isInitialized) return;
        if (!this.kin.isReady) return;
       MoveXY();
    }
    #endregion

    #region # Methods 
    private void MoveXY()
    {
        // Calc rotation angle
        Vector2 camDir = new Vector2(Camera.main.transform.forward.x, Camera.main.transform.forward.z);
        float ang = Vector2.Angle(refOrigin, camDir);
        if (Vector3.Cross(refOrigin, camDir).z > 0)
            ang = 360 - ang;

        // Translate real world vector to game vector
        Vector3 armVec = kin.GetJointPos(KinectWrapper.Joints.HAND_LEFT) - kin.GetJointPos(KinectWrapper.Joints.ELBOW_LEFT);
        Vector3 translatedArm = new Vector3(armVec.x, -armVec.y, -armVec.z);
        translatedArm = Quaternion.Euler(0, ang, 0) * translatedArm.normalized;     // rotate

        // Calc new look at vector
        Vector3 worldExtension = transform.position + translatedArm;
        if (this.pointTo == null)
            this.pointTo = worldExtension;
        else
            this.pointTo = pointTo + (worldExtension - pointTo) * movementSpeed * Time.deltaTime;

        // Look at
        this.transform.LookAt(this.pointTo, new Vector3(0, 1, 0));
    }
    #endregion 
}