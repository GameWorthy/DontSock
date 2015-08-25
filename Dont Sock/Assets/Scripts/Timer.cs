using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using DG.Tweening;

public class Timer : MonoBehaviour {
	
	[SerializeField] private Transform cursorTransform = null;
	[SerializeField] private Animator anim = null;

	private int clockSeconds = 10;
	private float warningSecond = 2.5f;
	private float currentSeconds = 0;
	private bool counting = false;
	private System.Action callBack;
	private Vector3 initialPosition;

	void Start() {
		initialPosition = transform.localPosition;
		Debug.Log (initialPosition);
		HideTimer (0.05f);
	}

	public void ShowTimer(float _time = 0.2f) {
		transform.DOLocalMove (initialPosition,_time);
	}

	public void HideTimer(float _time = 0.2f) {
		transform.DOLocalMove (new Vector3(
				initialPosition.x,
				initialPosition.y + 5,
				initialPosition.z
			),_time);
	}

	public void StartTimer(int _seconds, System.Action _callBack) {
		anim.SetInteger ("state", 0);
		currentSeconds = Mathf.Clamp(_seconds, 0,clockSeconds);
		callBack = _callBack;
		counting = true;
	}

	public void StopTimer(){
		counting = false;
		anim.SetInteger ("state", 0);
	}

	public void ResetCursor() {
		StopTimer ();
		float zRotation = cursorTransform.rotation.eulerAngles.z;
		if (zRotation == 0) {
			zRotation = 360;
		}
		
		DOTween.To ((float f)=>{
			cursorTransform.transform.localRotation = Quaternion.Euler(0,0,f);
		}, zRotation, 360, 0.2f);
	}

	void Update() {
		if (!counting) {
			return;
		}


		currentSeconds -= Time.deltaTime;

		float x = (currentSeconds / clockSeconds) - 1;

		int animState = 0;
		if (currentSeconds < warningSecond) {
			animState = 1;
		}

		if (currentSeconds <= 0) {
			animState = 0;
			StopTimer();
			if(callBack != null) {
				callBack.Invoke();
				callBack = null;
			}
			x = 1;
		}

		cursorTransform.localRotation = Quaternion.Euler (new Vector3 (0,0,360 * x));
		anim.SetInteger ("state", animState);

	}
}
