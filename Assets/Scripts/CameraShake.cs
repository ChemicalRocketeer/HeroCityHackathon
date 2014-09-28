using UnityEngine;
using System.Collections;

public class CameraShake : MonoBehaviour {
	
	public static float DefaultIntensity {
		get {
			return main.defaultIntensity;
		}
		set {
			main.defaultIntensity = value;
		}
	}
	
	public static float DefaultDuration {
		get {
			return main.defaultDuration;
		}
		set {
			main.defaultDuration = value;
		}
	}
	
	public float defaultIntensity = .05f;
	public float defaultDuration = 0.5f; // duration in seconds
	
	private bool shaking = false;
	private float intensity;
	private float decay; // amount to reduce intensity per second
	private Vector3 originPosition;
	private Quaternion originRotation;
	private static CameraShake main;
	
	public void Start() {
		main = Camera.main.GetComponent<CameraShake>();
	}
	
	public void Update () {
		if (intensity > 0){
			camera.transform.localPosition = originPosition + Random.insideUnitSphere * intensity;
			transform.rotation = new Quaternion(originRotation.x, originRotation.y, 
			                                    originRotation.z + Random.Range (-intensity, intensity) * .04f,
			                                    originRotation.w);
			intensity -= decay * Time.deltaTime;
		} else if (shaking) {
			intensity = 0;
			decay = 0;
			transform.localPosition = originPosition;
			transform.localRotation = originRotation;
			shaking = false;
		}
	}
	
	public static void Shake() {
		if (main != null) {
			main.shake();
		}
	}
	
	public static void Shake(float intensity) {
		if (main != null) {
			main.shake(intensity);
		}
	}
	
	public static void Shake(float intensity, float duration){
		if (main != null) {
			main.shake(intensity, duration);
		}
	}
	
	public void shake() {
		shake(defaultIntensity, defaultDuration);
	}
	
	public void shake(float intensity) {
		shake(intensity, defaultDuration);
	}
	
	public void shake(float intensity, float duration) {
		if (!shaking) {
			originPosition = main.transform.localPosition;
			originRotation = main.transform.localRotation;
		}
		this.intensity += intensity;
		decay = (this.intensity / duration + decay) / 2;
		main.shaking = true;
	}
}