using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Menu : MonoBehaviour {

	[SerializeField] private Animator menuAnimator = null;
	[SerializeField] private List<RectTransform> menusToCenter = null;
	[SerializeField] private Text highScore;	
	[SerializeField] private Text currentScore;
	[SerializeField] private HighScoreStar highScoreStar;
	[SerializeField] private AudioSource audioSource = null;
	[SerializeField] private AudioClip highScoreClip = null;
	[SerializeField] private Text soundSettingsText = null;

	private bool soundIsOn;

	void Start () {

		soundIsOn = (PlayerPrefs.GetInt ("sound_setting", 0) == 1);
		ToggleSoundSettings();

		foreach (RectTransform rect in menusToCenter) {
			rect.position = Vector2.zero;
			rect.sizeDelta = Vector2.zero;
		}
	}
	
	public void SetHighScore(int _score) {
		highScore.text = FormatNumber (_score);
	}
	
	public void SetCurrentScore(int _score) {
		currentScore.text = FormatNumber (_score);
	}
	
	public void ActivateHighScore() {
		audioSource.clip = highScoreClip;
		audioSource.Play ();
		highScoreStar.Activate ();
	}

	public void DeactivateHighScore() {
		highScoreStar.Deactivate ();
	}

	public void SetAnimationState(int _state) {
		menuAnimator.SetInteger ("state", _state);
	}

	private string FormatNumber(int number) {
		return number.ToString("N0");
	}

	public void ToggleSoundSettings() {
		soundIsOn = !soundIsOn;
		int setting = soundIsOn ? 0 : 1;

		PlayerPrefs.SetInt ("sound_setting",setting);
		if (soundIsOn) {
			soundSettingsText.text = "SOUND: ON";
			AudioListener.volume = 1;
		} else {	
			soundSettingsText.text = "SOUND: OFF";
			AudioListener.volume = 0;
		}
	}

	public void SettingsSupport() {
		Application.OpenURL ("mailto:gameworthyfeedback@gmail.com");
	}

	public void SettingsAbout() {
		Application.OpenURL ("http://gameworthystudios.com/");
	}
}










