using UnityEngine;
using System.Collections;

public class AppleMovement : MonoBehaviour {

    #region # Properties

    private float BounceSpeed = 5f;
    private float MaxBounce = 0.5f;
    private Vector3 BounceDirection = new Vector3(0, 1, 0);
    private bool bounceUp = true;
    private float bounceAmount = 0.0f;
    private float bounceIncrement;

    #endregion

    private void Start()
    {
        bounceIncrement = Time.deltaTime * BounceSpeed;
    }

    private void Update()
    {
        BounceObj();
    }

    private void BounceObj()
    {
        Vector3 amt = BounceDirection * bounceIncrement;

        if (bounceUp)
        {
            bounceAmount += bounceIncrement;
            if (bounceAmount > MaxBounce)
                bounceUp = false;
        }
        else
        {
            bounceAmount -= bounceIncrement;
            amt = -1 * amt;
            if (bounceAmount < -MaxBounce)
                bounceUp = true;
        }

        transform.Translate(amt);
    }

    
}
