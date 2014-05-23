#region # Using References #
using UnityEngine;
using System.Collections;
#endregion

public class EnergyController : MonoBehaviour
{
    private float maxEnergy = 100;
    private float rechargeRate = 5;
    private float dischargeRate = 35;

    private float _energy;
    public float normalizedEnergy { get { return this._energy / this.maxEnergy; } }
    private bool isUsable;

    private void OnEnable()
    {
        this._energy = this.maxEnergy;
        this.isUsable = true;
    }

    private void Update()
    {
        if (Time.deltaTime == 0 || Time.timeScale == 0)
            return;

        if (this._energy > this.maxEnergy)
            this.isUsable = true;

        if (this._energy >= 0)
            this._energy = Mathf.Clamp(this._energy + this.rechargeRate * Time.deltaTime, 0, this.maxEnergy);
    }

    public bool Discharge()
    {
        bool val = false;
        if (this._energy > 0 && this.isUsable)
        {
            this._energy = Mathf.Clamp(this._energy - this.dischargeRate * Time.deltaTime, 0, this.maxEnergy);
            val = true;

            if(this._energy == 0)
                this.isUsable = false;
        }
           
        
        return val;
    }
}
