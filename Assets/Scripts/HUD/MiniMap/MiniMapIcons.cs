using UnityEngine;
using System.Collections;

public class MiniMapIcons : MonoBehaviour
{
    public ObjectScan scan;
    public Texture2D sweetIcon;
    public Texture2D fruitIcon;

    private string sweetTag = "Sweet";
    private string fruitTag = "Fruit";

    private void OnGUI()
    {
        foreach (GameObject go in scan.SweetsList)
        {
            if (go != null)
            {
                Vector3 objPos = this.camera.WorldToViewportPoint(go.transform.position);
                Rect texture = new Rect(Screen.width * (camera.rect.x + (objPos.x * camera.rect.width)) - 2,
                    Screen.height * (1 - (camera.rect.y + (objPos.y * camera.rect.height))) - 2, 20, 20);

                GUI.DrawTexture(texture, sweetIcon);
            }
        }

        foreach (GameObject go in scan.fruits)
        {
            if (go != null)
            {
                Vector3 objPos = this.camera.WorldToViewportPoint(go.transform.position);
                Rect texture = new Rect(Screen.width * (camera.rect.x + (objPos.x * camera.rect.width)) - 2,
                    Screen.height * (1 - (camera.rect.y + (objPos.y * camera.rect.height))) - 2, 20, 20);

                GUI.DrawTexture(texture, fruitIcon);
            }
        }
    }
}
