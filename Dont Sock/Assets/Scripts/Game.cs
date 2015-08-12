using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Game : MonoBehaviour { 

	[SerializeField] private Sock sockPrefab = null;
	[SerializeField] private Timer timer = null;
	[SerializeField] private Drawer drawer = null;
	[SerializeField] private SockReader sockReader = null;
	[SerializeField] private Text text = null;

	private List<Sock> socks = new List<Sock> ();

	private int currentLevel = 0;
	public int CurrentLevel {
		get { return currentLevel; }
		private set { currentLevel = value; }
	}

	void Start() {
		Screen.orientation = ScreenOrientation.Portrait;
		
		sockReader.SetGame (this);
		NextLevel ();
	}

	void Update() {
		if (Input.GetKeyDown (KeyCode.G)) {
			NextLevel();
		}
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

		text.text = currentLevel + " " + uniqueSockIds.Length;
		sockReader.SetTarget (uniqueSockIds[Random.Range(0,uniqueSockIds.Length - 1)]);
	}

	private IEnumerator DelayedSockIdSet(Sock _sock, int _id) {
		yield return null;
		_sock.SetID (_id);
	}

	public void NextLevel() {
		CurrentLevel++;
		StartCoroutine (NextLevelPresentation ());
	}

	public void GameOver() {
		//TODO: stuff...
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
		yield return new WaitForSeconds (drawer.DrawerSpeed);
		ClearSocks ();
		PopulateSocks (LevelDB.GetLevelSockAmount (CurrentLevel));
		timer.StartTimer (LevelDB.GetLevelTime (CurrentLevel), null);
		drawer.Open ();
		yield return new WaitForSeconds (drawer.DrawerSpeed);
		sockReader.ReaderOn ();
	}
}
