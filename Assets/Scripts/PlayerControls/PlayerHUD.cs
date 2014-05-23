using UnityEngine;
using System.Collections.Generic;

public class PlayerHUD: MonoBehaviour {

	private Rect healthBackPos, healthFrontPos, healthtxtPos;
	
	public Texture2D health;
	public Texture2D back;
	
	private PlayerBehavior pState;
	// Use this for initialization
	void Start () {
		
		int h = Screen.height;
		
		healthtxtPos = new Rect(30, 150, 100, 30);
		healthFrontPos = new Rect(80,150,100,30);
		healthBackPos = new Rect(80,150,100,30);
		
		GameObject gamePlayer = GameObject.FindWithTag("Player");
		if(gamePlayer != null)
			pState = gamePlayer.GetComponent<PlayerBehavior>();
		else 
			pState = null;
		
	}
	
	// Update is called once per frame
	void OnGUI () 
	{
		
		healthFrontPos.width = (int)(pState.FULL_HEALTH);
		
		GUI.Label(healthtxtPos, "Health");
		GUI.DrawTexture(healthBackPos,back);
		GUI.DrawTexture(healthFrontPos,health);
	}
}
