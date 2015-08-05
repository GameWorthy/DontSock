using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Timer : MonoBehaviour {

	[SerializeField] private Text timeText = null;

	private int seconds;
	private System.Action callBack;

	public void StartTimer(int _seconds, System.Action _callBack) {
		StopTimer ();
		seconds = _seconds;
		callBack = _callBack;
		StartCoroutine("IStartTimer");
	}

	public void StopTimer(){
		StopCoroutine ("IStartTimer");
	}
	
	private IEnumerator IStartTimer() {
		SetTimerText(seconds);

		while (seconds > 0) {
			yield return new WaitForSeconds (1f);
			seconds--;
			SetTimerText(seconds);
		}

		if (callBack != null) {
			callBack.Invoke();
		}

		yield return null;
	}

	private void SetTimerText(int _seconds) {
		string time = _seconds.ToString ();
		if (_seconds < 10) {
			time = "0" + _seconds;
		}
		timeText.text = "00:" + time;
	}
}
