#region # Using Reference #
using UnityEngine;
using System.Collections;
#endregion

public class StatusBar : MonoBehaviour
{
    public HealthController healthController;
    public EnergyController energyController;

    public Texture foregroundTexture;
    public Texture backgroundTexture;
    public Texture2D healthTexture;
    public Texture2D energyTexture;

    private void OnGUI()
    {
        float energy = energyController.normalizedEnergy;
        float health = healthController.normalizedHealth;

        Rect healthRec = new Rect(10, 6, Screen.width / 2 - 10 - 40, backgroundTexture.height);
        Rect energyRec = new Rect(10, 26, Screen.width / 2 - 10 - 40, backgroundTexture.height);

        DrawBar(health, healthRec, healthTexture);
        DrawBar(energy, energyRec, energyTexture);

    }

    private void DrawBar(float value, Rect square, Texture2D image)
    {
        GUI.DrawTexture(square, backgroundTexture);
        square.width *= value;
        GUI.color = image.GetPixelBilinear(value, 0.5f);
        GUI.DrawTexture(square, foregroundTexture);
        GUI.color = Color.white;
    }
}
