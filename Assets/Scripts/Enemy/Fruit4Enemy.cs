using UnityEngine;
using System.Collections;

public class Fruit4Enemy : MonoBehaviour {
	
	public float MOVEMENT_DELAY = 1f;
	public float FIRE_DELAY = 2f;
	public float MOVEMENT_SPEED = 2f;
	public float AttackRange = 100f;
	public GameObject LaserPrefab;

    public AudioClip LaserBlast;
    public GameObject Destroyed;
	private float timeOfLastMovement;
	private float timeOfLastFire;
	private Vector3 droidPosOffset;
	
	private int hits;
	
	// Use this for initialization
	void Start () {
		timeOfLastMovement = Time.time;
		Random.seed = 2;
		hits = 3;
		droidPosOffset = new Vector3(0, 0, 0);
	}
	
	// Update is called once per frame
	void Update () {
		float currentTime = Time.time;
		
		if(currentTime - timeOfLastMovement > MOVEMENT_DELAY) {
			timeOfLastMovement = currentTime;
			Move();
		}
		if(currentTime - timeOfLastFire > FIRE_DELAY && isClose()) {
			timeOfLastFire = currentTime;
			Fire();
		}
		
		if( hits == 0 )
		{
            GameObject DestroyObject = Instantiate(Destroyed, transform.position, transform.rotation) as GameObject;
            Destroy(DestroyObject, 1);
			Destroy(gameObject);
		}
		transform.position = Vector3.Lerp(transform.position, transform.position + droidPosOffset, 1f * Time.deltaTime);
		transform.Rotate(new Vector3(0, 1, 0), 30 * Time.deltaTime);
	}
	
	void Fire() {
		GameObject laser = Instantiate(LaserPrefab, transform.position, transform.rotation) as GameObject;
        //audio.PlayOneShot( LaserBlast );
	}
	
	void Move() {
		float x = (0.5f - Random.value) * MOVEMENT_SPEED * 2f;
		float y = (0.5f - Random.value) * MOVEMENT_SPEED;
		float z = (0.5f - Random.value) * MOVEMENT_SPEED;
		
		if(transform.position.x > 4) {
			x = -1f * Mathf.Abs(x);
		}
		if(transform.position.x < -4) {
			x = Mathf.Abs(x);
		}
		if(transform.position.y > 3) {
			y = -1f * Mathf.Abs(y);
		}
		if(transform.position.y < 0.75) {
			y = Mathf.Abs(y);
		}
		if(transform.position.z > 4) {
			z = -1f * Mathf.Abs(z);
		}
		if(transform.position.z < 2) {
			z = Mathf.Abs(z);
		}
		
		droidPosOffset = new Vector3(x, y, z);
	}
	
	void OnTriggerEnter(Collider other) {
        if(other.name == "RightSwordCollider" || other.name == "LeftSwordCollider")
		{
			hits--;
		}
    }
	
	public bool isClose()
	{
		return Vector3.Distance(Camera.main.transform.position, transform.position) < AttackRange;
	}
}
