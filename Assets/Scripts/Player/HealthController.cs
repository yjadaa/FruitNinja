using UnityEngine;
using System.Collections;

public class HealthController : MonoBehaviour
{
    private float maxHealth = 100;
    private float healingSpeed = 0;
    private PlayerBehavior pState;
    private float _health;
    public float normalizedHealth { get { return this._health / maxHealth; } }
    public AudioClip HealSound;
    private void OnEnable()
    {
        this._health = maxHealth;
    }

    void Start()
    {
        GameObject gamePlayer = GameObject.FindWithTag("Player");
        pState = gamePlayer.GetComponent<PlayerBehavior>();


    }

    

    public void Damage(float hitDamage)
    {
        this._health = hitDamage;

        // Play hurt audio
        //Shake();
    }

    public void Heal(float healValue)
    {
        this._health += healValue;
        this._health = Mathf.Clamp(this._health, 0, maxHealth);

        // Play heal audio
        audio.PlayOneShot(HealSound,0.1f);
    }

    private void Shake()
    {
        Hashtable ht = new Hashtable();
        ht.Add("x", 0.5f);
        ht.Add("y", 0.5f);
        ht.Add("time", 0.2f);

        iTween.ShakePosition(this.gameObject, ht);
    }
}
