using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;


public class Game : MonoBehaviour {
	private enum MenuState	{
		OFF,
		MAIN_MENU,
		IN_GAME,
		GAME_OVER,
		SETTINGS
	}

	private MenuState menuState = MenuState.OFF;

	[SerializeField] private Sock sockPrefab = null;
	[SerializeField] private Timer timer = null;
	[SerializeField] private Drawer drawer = null;
	[SerializeField] private SockReader sockReader = null;
	[SerializeField] private LevelAnimation levelAnimation = null;
	[SerializeField] private Animator menuAnimator = null;
	[SerializeField] private List<SpriteRandomizer> spriteRandomizers = null;
	[SerializeField] private List<Color> cameraColors = null;

	private bool gameInProgress = false;

	private List<Sock> socks = new List<Sock> ();

	private int currentLevel = 0;
	public int CurrentLevel {
		get { return currentLevel; }
		private set { currentLevel = value; }
	}

	void Start() {
		Screen.orientation = ScreenOrientation.Portrait;
		sockReader.SetGame (this);
		menuState = MenuState.MAIN_MENU;
		ShowMenu ();
	}

	void Update() {

		menuAnimator.SetInteger ("state", (int)menuState);

		if (Input.GetKeyDown (KeyCode.G)) {
			NextLevel();
		}
	}

	public void StartGame() {
		if (gameInProgress) {
			return;
		}
		CurrentLevel = 0;
		gameInProgress = true;
		menuState = MenuState.IN_GAME;
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
	}

	public void ShowMenu() {
		foreach (SpriteRandomizer sr in spriteRandomizers) {
			sr.Randomize();
		}
		Camera.main.backgroundColor = cameraColors[Random.Range(0,cameraColors.Count)];
		menuState = MenuState.MAIN_MENU;
	}

	public void ShowSettings() {
		menuState = MenuState.SETTINGS;
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
			StartCoroutine(DelayedSockIdSet(s,uniqueSockIds[i]));
			socks.Add(s);
		}

		sockReader.SetTarget (uniqueSockIds[Random.Range(0,(int)(uniqueSockIds.Length/2) - 1)]);
	}

	void ClearSocks() {
		foreach (Sock s in socks) {
			if(s)Destroy(s.gameObject);
		}
		socks = new List<Sock> ();
	}

	IEnumerator NextLevelPresentation() {
		sockReader.ReaderOff ();
		drawer.Close ();
		yield return new WaitForSeconds (drawer.DrawerSpeed/2);
		levelAnimation.Play ();
		levelAnimation.SetText ("Day " + CurrentLevel);
		ClearSocks ();
		PopulateSocks (LevelDB.GetLevelSockAmount (CurrentLevel));
		//timer.StartTimer (LevelDB.GetLevelTime (CurrentLevel), ()=>{GameOver();});
		yield return new WaitForSeconds (drawer.DrawerSpeed/2);
		drawer.Open ();
		yield return new WaitForSeconds (drawer.DrawerSpeed);
		sockReader.ReaderOn ();
	}
}
