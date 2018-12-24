using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using GameWorthy;

public class Menu : MonoBehaviour {

	[SerializeField] private Animator menuAnimator = null;
	[SerializeField] private List<RectTransform> menusToCenter = null;
	[SerializeField] private Text highScore = null;	
	[SerializeField] private Text currentScore = null;
	[SerializeField] private HighScoreStar highScoreStar = null;
	[SerializeField] private AudioSource audioSource = null;
	[SerializeField] private AudioClip highScoreClip = null;
	[SerializeField] private AudioClip buttonClickClip = null;
	[SerializeField] private Text soundSettingsText = null;
	[SerializeField] private Text[] medalsText = null;
	[SerializeField] private Image medal = null;

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

	public void SetMedalColor(Color _color) {
		medal.color = _color;
	}

	public void SettingsSupport() {
		Application.OpenURL ("mailto:gameworthyfeedback@gmail.com");
	}

	public void SettingsAbout() {
		Application.OpenURL ("http://gameworthystudios.com/");
	}

	public void ButtonSound() {
		audioSource.clip = buttonClickClip;
		audioSource.Play ();
	}

	public void UpdateMedalsText() {
		medalsText [0].text = " x  " + MedalSystem.TotalBronze.ToString ();
		medalsText [1].text = " x  " + MedalSystem.TotalSilver.ToString ();
		medalsText [2].text = " x  " + MedalSystem.TotalGold.ToString ();
		medalsText [3].text = " x  " + MedalSystem.TotalPlatinum.ToString ();
	}
}










