using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Game : MonoBehaviour { 

	[SerializeField] private Sock sockPrefab = null;
	[SerializeField] private Transform drawer = null;
	[SerializeField] private SockReader sockReader = null;
	[SerializeField] private Text text = null;

	private List<Sock> socks = new List<Sock> ();

	private int currentLevel = 1;
	public int CurrentLevel {
		get { return currentLevel; }
		private set { currentLevel = value; }
	}

	void Start() {
		PopulateSocks ();
		sockReader.SetGame (this);
	}

	void Update() {
		if (Input.GetKeyDown (KeyCode.G)) {
			NextLevel();
		}
	}

	void PopulateSocks() {
		int nSocks = LevelDB.GetLevelSockAmount (CurrentLevel);
		int[] uniqueSockIds = SockDB.GetRandomUniqueSocks (nSocks);
		for (int i = 0; i < nSocks; i++) {
			Sock s = (Instantiate(sockPrefab.gameObject) as GameObject).GetComponent<Sock>();
			s.transform.parent = drawer;
			s.transform.localPosition = new Vector3(
					Random.Range(-2.5f,2.5f),
					Random.Range(-2.5f,2.5f),
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
		ClearSocks ();
		PopulateSocks ();
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
}
