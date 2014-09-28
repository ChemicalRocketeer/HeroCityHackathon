using UnityEngine;
using System.Collections;

public class Menu : MonoBehaviour {

	void Start() {
		GameStats.Init();
	}

	void OnGUI() {
		float yVal = 50f;
		float margin = 10f;
		float height = 20f;
		GUI.Box(new Rect(Screen.width / 2 - 50, yVal, 200, height), "High Scores: ");
		yVal += margin + height;
		foreach (uint score in GameStats.scores) {
			GUI.Box(new Rect(Screen.width / 2 - 50, yVal, 200, height), "" + score);
			yVal += margin + height;
		}
		yVal += margin;
		if (GUI.Button(new Rect(Screen.width / 2 - 5, yVal, 120, height), "Start")) {
			Application.LoadLevel("Scene1");
		}
	}
}
