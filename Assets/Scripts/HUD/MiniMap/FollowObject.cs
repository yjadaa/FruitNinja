using UnityEngine;
using System.Collections;

public class FollowObject : MonoBehaviour
{

    public Transform followTarget;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = new Vector3(followTarget.position.x, this.transform.position.y, this.followTarget.position.z);
        this.transform.eulerAngles = new Vector3(this.transform.eulerAngles.x, this.followTarget.transform.eulerAngles.y, this.transform.eulerAngles.z);
    }
}
