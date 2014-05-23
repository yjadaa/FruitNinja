#region # Using #
using UnityEngine;
using System.Collections;
#endregion

public class Sweets : MonoBehaviour
{
    #region # Properties
    private string playerTag = "Player";

    private float BounceSpeed = 5f;
    private float MaxBounce = 0.5f;
    private Vector3 BounceDirection = new Vector3(0, 1, 0);

    private bool bounceUp = true;
    private float bounceAmount = 0.0f;
    private float bounceIncrement;

    private float healthValue = 50;
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

    private void OnTriggerEnter(Collider collisionObj)
    {
        if (collisionObj.tag == playerTag)
        {
            HealthController hc = collisionObj.GetComponent<HealthController>();
            if (hc != null)
            {
                hc.Heal(healthValue);
                Destroy(this.gameObject);
            }
        }
    }
}
