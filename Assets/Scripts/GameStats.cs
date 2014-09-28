using UnityEngine;
using System.Collections.Generic;

public class GameStats : MonoBehaviour {

	public float GUIMargin = 10f;
	public float GUIHeight = 20f;

	public static uint currentScore;
	public static float multiplier = 1f;

	public static uint[] scores;

	void Start() {
		Init ();
	}

	public static void Init() {
		if (scores == null) {
			scores = new uint[10];
			for (uint i = 0; i < scores.Length; i++) {
				scores[i] = i;
			}
		}
	}

	public static void SubmitScore(uint score) {
		int i = sort(score, scores);
		uint[] temp = new uint[scores.Length + 1];
		for (int j = 0; j < i; j++) {
			temp[j] = scores[j];
		}
		temp[i] = score;
		for (int k = i; k < scores.Length; k++) {
			temp[k + 1] = scores[k];
		}
		scores = temp;
		currentScore = 0;
	}

	// returns the index to replace with the new number
	private static int sort(uint num, uint[] arr) {
		if (num < arr[0]) return 0;
		for (int i = 0; i < arr.Length - 1; i++) {
			if (num >= arr[i] && num <= arr[i + 1]) return i + 1;
		}
		return arr.Length;
	}

	public static void Read() {

	}

	public static void Write() {

	}

	void OnGUI() {
		Debug.Log("score " + currentScore);
		GUI.Box(new Rect(GUIMargin, GUIMargin, 100, GUIHeight), "" + currentScore);
	}
}
