using UnityEngine;
using System.Collections.Generic;

public class GameStats {

	public static uint[] scores = new uint[0];

	public static void SubmitScore(uint score) {
		int i = sort(score, scores, 0, scores.Length);
		uint[] temp = new uint[scores.Length + 1];
		for (int j = 0; j < i; j++) {
			temp[j] = scores[j];
		}
		temp[i] = score;
		for (int k = i; k < scores.Length; k++) {
			temp[k + 1] = scores[k];
		}
		scores = temp;
	}

	// returns the index to replace with the new number
	private static int sort(uint num, uint[] arr, int left, int right) {
		if (right - left <= 0) return left;
		int mid = (right - left) / 2  + left; // do it this way to avoid int overflow
		if (num < arr[mid]) return sort(num, arr, left, mid);
		else return sort (num, arr, mid, right);
	}

	public static void Read() {

	}

	public static void Write() {

	}
}
