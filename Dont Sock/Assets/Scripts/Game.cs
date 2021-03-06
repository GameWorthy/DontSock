﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using GameWorthy;


public class Game : MonoBehaviour {
	private enum MenuState	{
		OFF,
		MAIN_MENU,
		IN_GAME,
		GAME_OVER,
		SETTINGS,
		MEDALS
	}

	private MenuState menuState = MenuState.OFF;

	[SerializeField] private Sock sockPrefab = null;
	[SerializeField] private Timer timer = null;
	[SerializeField] private Drawer drawer = null;
	[SerializeField] private SockReader sockReader = null;
	[SerializeField] private LevelAnimation levelAnimation = null;
	[SerializeField] private Menu menu = null;
	[SerializeField] private List<SpriteRandomizer> spriteRandomizers = null;
	[SerializeField] private List<Color> cameraColors = null;

	private bool gameInProgress = false;

	private int highestScore;
	private int totalScore;//sum of all games

	private List<Sock> socks = new List<Sock> ();

	private int currentLevel = 0;
	public int CurrentLevel {
		get { return currentLevel; }
		private set { currentLevel = value; }
	}

	void Start() {
		//TODO:Create a saver class to deal with encryption saving data
		highestScore = PlayerPrefs.GetInt ("highest_score",0);
		totalScore = PlayerPrefs.GetInt ("total_score",0);


		Screen.orientation = ScreenOrientation.Portrait;
		sockReader.SetGame (this);
		menuState = MenuState.MAIN_MENU;
		ShowMenu ();

		MedalSystem.Initiate ();
		menu.UpdateMedalsText ();
	}

	void Update() {

		menu.SetAnimationState ((int)menuState);

		if (Input.GetKeyDown (KeyCode.G)) {
			NextLevel();
		}

		if (Input.GetKeyDown (KeyCode.K)) {
			ShowAllSocks();
		}

		if (Input.GetKeyDown (KeyCode.R)) {
			highestScore = 0;
		}
	}

	public void StartGame() {
		if (gameInProgress) {
			return;
		}
		CurrentLevel = 0;
		gameInProgress = true;
		menuState = MenuState.IN_GAME;
		timer.ShowTimer ();
		NextLevel ();
	}

	private IEnumerator DelayedSockIdSet(Sock _sock, int _id) {
		yield return null;
		_sock.SetID (_id);
	}

	public void NextLevel() {

		if (!gameInProgress) {
			return;
		}

		CurrentLevel++;
		StartCoroutine (NextLevelPresentation ());
	}

	public void GameOver() {
		gameInProgress = false;
		menuState = MenuState.GAME_OVER;
		drawer.Close ();
		timer.ResetCursor ();
		sockReader.ReaderOff ();
		sockReader.Hide ();

		if (currentLevel > highestScore) {
			highestScore = currentLevel;
			PlayerPrefs.SetInt("highest_score", highestScore);
			menu.ActivateHighScore();
		}

		//give medals
		if (currentLevel >= 45) {
			MedalSystem.TotalPlatinum++;
			menu.SetMedalColor (new Color(203f/255f,251f/255f,1,1));
		} else if (currentLevel >= 30) {
			MedalSystem.TotalGold++;
			menu.SetMedalColor (new Color(1,237f/255f,0,1));
		} else if (currentLevel >= 18) {
			MedalSystem.TotalSilver++;
			menu.SetMedalColor (new Color(203f/255f,203f/255f,203f/255f,1));
		} else if (currentLevel >= 6) {
			MedalSystem.TotalBronze++;
			menu.SetMedalColor (new Color(218f/255f,103f/255f,0f,1));
		} else {
			menu.SetMedalColor(new Color(0f,0f,0f,0f));
		}

		menu.UpdateMedalsText ();

		menu.SetHighScore (highestScore);
		menu.SetCurrentScore (currentLevel);
		totalScore += currentLevel;
		PlayerPrefs.SetInt ("total_score", totalScore);
	}

	public void ShowMenu() {
		foreach (SpriteRandomizer sr in spriteRandomizers) {
			sr.Randomize();
		}
		Camera.main.backgroundColor = cameraColors[Random.Range(0,cameraColors.Count)];
		menuState = MenuState.MAIN_MENU;
		timer.HideTimer ();
	}

	public void ShowSettings() {
		menuState = MenuState.SETTINGS;
	}

	public void ShowMedals() {
		menuState = MenuState.MEDALS;
	}
	
	void PopulateSocks(int _totalSocks) {
		int[] uniqueSockIds = SockDB.GetRandomUniqueSocks (_totalSocks);
		for (int i = 0; i < _totalSocks; i++) {
			Sock s = (Instantiate(sockPrefab.gameObject) as GameObject).GetComponent<Sock>();
			
			if(i == 0) {
				s.RestartLayerOrder();
			}
			
			s.transform.parent = drawer.DrawerTransform;
			s.transform.localPosition = new Vector3(
				Random.Range(-1.9f,1.9f),
				Random.Range(-2.3f,2.3f),
				s.transform.localPosition.z
				);
			s.name = "sock (" + uniqueSockIds[i] + ")";
			StartCoroutine(DelayedSockIdSet(s,uniqueSockIds[i]));
			socks.Add(s);
		}

		sockReader.SetTarget (uniqueSockIds[Random.Range(0,(int)(uniqueSockIds.Length) - 1)]);
	}

	void ClearSocks() {
		foreach (Sock s in socks) {
			if(s)Destroy(s.gameObject);
		}
		socks = new List<Sock> ();
	}

	void ShowAllSocks() {
		for (int i = 0; i < SockDB.TotalSocks; i++) {
			Sock s = (Instantiate(sockPrefab.gameObject) as GameObject).GetComponent<Sock>();
			
			if(i == 0) {
				s.RestartLayerOrder();
			}

			s.transform.localPosition = new Vector3(
				(int)i%11,
				-(int)i/11,
				0
				);
			s.name = i.ToString();
			this.StartCoroutine(DelayedSockIdSet(s,i));
		}
	}

	IEnumerator NextLevelPresentation() {
		sockReader.ReaderOff ();
		drawer.Close ();
		timer.ResetCursor ();

		yield return new WaitForSeconds (drawer.DrawerSpeed/2);

		levelAnimation.Play ();
		levelAnimation.SetText ("Day " + CurrentLevel);
		ClearSocks ();
		PopulateSocks (LevelDB.GetLevelSockAmount (CurrentLevel));

		yield return new WaitForSeconds (drawer.DrawerSpeed/2);

		drawer.Open ();

		yield return new WaitForSeconds (0.55f);

		sockReader.Show();
		float waitTime = 1f;
		if (currentLevel == 1) {
			sockReader.SetAnimationSpeed (0.5f);
			waitTime *= 2;
		} else {
			sockReader.SetAnimationSpeed (1f);
		}

		yield return new WaitForSeconds (drawer.DrawerSpeed);

		timer.StartTimer (LevelDB.GetLevelTime (CurrentLevel), ()=>{GameOver();timer.ResetCursor();});
		sockReader.ReaderOn ();
	}
}
