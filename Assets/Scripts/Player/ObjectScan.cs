#region # Using Reference #
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
#endregion

public class ObjectScan : MonoBehaviour
{
    private string sweetTag = "Sweet";
    private string fruitTag = "Fruit";

    private List<GameObject> sweets;
    public List<GameObject> fruits;
    public List<GameObject> SweetsList { get { return sweets; } }

    private void Start()
    {
        this.sweets = new List<GameObject>();
        this.fruits = new List<GameObject>();
    }

    private void OnTriggerEnter(Collider collisionObj)
    {
        if (collisionObj.tag == sweetTag)
            this.sweets.Add(collisionObj.gameObject);

		if(collisionObj.tag == fruitTag)
			this.fruits.Add(collisionObj.gameObject);		
    }

    private void OnTriggerExit(Collider collisionObj)
    {
        if (collisionObj.tag == sweetTag)
            this.sweets.Remove(collisionObj.gameObject);

        if (collisionObj.tag == fruitTag)
            this.fruits.Remove(collisionObj.gameObject);
    }
}
