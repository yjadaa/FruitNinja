using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour
{

    public Transform Target;

    void LateUpdate()
    {
        this.transform.position = new Vector3(Target.position.x, transform.position.y, Target.transform.position.z);
    }
}
