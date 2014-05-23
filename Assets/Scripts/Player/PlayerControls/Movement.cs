#region # Using Reference #
using UnityEngine;
using System;
using System.Collections;
#endregion

public class Movement : MonoBehaviour
{
    #region # Properties #
    private CharacterController controller;
    private KUInterface kin;
    private string KinectTag = "Kinect";

    #region # Input Controls #
    private string leftComm = "a";
    private string rightComm = "d";
    private string backComm = "s";
    private string forwardComm = "w";
    private string jumpComm = "space";
    private string flyComm = "v";
    private string crouchComm = "c";
    #endregion

    #region # Navigation Velocity #
    private float velocity = 17.0f;
    private float jumpForce = 30.0f;
    private float gravityForce = 60.0f;

    private float crouchDelta = 6;
    private float getUpSpeed = 20;
    private float friction = 3;
    private float sensitivity = 3;
    #endregion

    private float xShift;
    private float zShift;
    private float pHeight;
    private bool isCrouch;

    public EnergyController energy;
    #endregion

    #region # Inherit Methods #
    private void Start()
    {
        this.controller = GetComponent<CharacterController>();
        this.pHeight = this.controller.height;
        this.isCrouch = false;
        this.xShift = 0;
        this.zShift = 0;
    }

    private void Update()
    {
        if (kin == null) SetUpKinect();

        Vector3 dir;
        xShift = GetShift(this.xShift, GetLeftOrRight());
        zShift = GetShift(this.zShift, GetForwardOrBack());

        dir = new Vector3(this.xShift, 0, this.zShift);
        dir = this.transform.TransformDirection(dir) * this.velocity;  //transform direction to local space

        dir.y = this.controller.velocity.y - this.gravityForce * Time.deltaTime;        //apply gravity

        // Jump
        if (IsJumping() && this.controller.isGrounded)
            dir.y = this.jumpForce * this.gravityForce * Time.deltaTime;

        // Fly
        if (IsFlying() && this.energy.Discharge())
            dir.y = this.jumpForce * this.gravityForce * Time.deltaTime;

        // Crouch
        if (IsCrouching())
        {
            if (!this.isCrouch)
            {
                this.controller.height = Mathf.Clamp(this.controller.height - this.crouchDelta, this.pHeight - this.crouchDelta, this.pHeight);
                isCrouch = true;
            }
        }
        else
            this.isCrouch = false;

        // Stand up
        if (!this.isCrouch && this.controller.height != this.pHeight)
        {
            this.controller.height = Mathf.Clamp(this.controller.height + this.getUpSpeed * Time.deltaTime, 0, this.pHeight);

            this.transform.position = new Vector3(this.transform.position.x,
                 Mathf.Clamp(this.transform.position.y + this.getUpSpeed * Time.deltaTime, 0, this.transform.position.y + crouchDelta),
                this.transform.position.z);
        }

        this.controller.Move(dir * Time.deltaTime);      //move our character

    }
    #endregion

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

    #region # Jumping && Crouching #
    private bool IsCrouching()
    {
        bool val = false;
        if (Input.GetKey(crouchComm) || KPlayerMove.KinectCrouch(kin))
            val = true;
        return val;
    }

    private bool IsJumping()
    {
        // Replace with kinect command
        bool val = false;
        if (Input.GetKey(this.jumpComm) || KPlayerMove.KinectJump(kin))
            val = true;

        return val;
    }

    private bool IsFlying()
    {
        // Replace with kinect command
        bool val = false;
        if (Input.GetKey(this.flyComm) || KPlayerMove.KinectFly(kin))
            val = true;

        return val;
    }
    #endregion

    #region # Determine Direction #
    private enum Direction { posDir, neutral, negDir };

    private Direction GetLeftOrRight()
    {
        // Replace with Kinect logic
        Direction shiftDirection = Direction.neutral;
        if (Input.GetKey(this.leftComm) || KPlayerMove.KinectLeft(kin))
            shiftDirection = Direction.negDir;
        else if (Input.GetKey(this.rightComm) || KPlayerMove.KinectRight(kin))
            shiftDirection = Direction.posDir;

        return shiftDirection;
    }

    private Direction GetForwardOrBack()
    {
        // Replace with Kinect logic
        Direction shiftDirection = Direction.neutral;
        if (Input.GetKey(this.backComm) || KPlayerMove.KinectBack(kin))
            shiftDirection = Direction.negDir;
        else if (Input.GetKey(this.forwardComm) || KPlayerMove.KinectForward(kin))
            shiftDirection = Direction.posDir;

        return shiftDirection;
    }

    private float GetShift(float previousShift, Direction direct)
    {
        float shift = 0;
        switch (direct)
        {
            case Direction.posDir:
                shift = Mathf.Clamp(previousShift + 1f * sensitivity * Time.deltaTime, 0, 1);
                break;
            case Direction.negDir:
                shift = Mathf.Clamp(previousShift - 1f * sensitivity * Time.deltaTime, -1, 0);
                break;
            case Direction.neutral:
                if (previousShift > 0)
                {

                    shift = Mathf.Clamp(previousShift - 1f * friction * Time.deltaTime, 0, 1);
                }
                else if (previousShift < 0)
                {
                    shift = Mathf.Clamp(previousShift + 1f * friction * Time.deltaTime, -1, 0);
                }
                break;
        }

        return shift;
    }
    #endregion
}
