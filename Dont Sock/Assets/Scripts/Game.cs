using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Game : MonoBehaviour { 

	[SerializeField] private Sock sockPrefab = null;
	[SerializeField] private Transform drawer = null;

	private List<Sock> socks = new List<Sock> ();

	private int currentLevel = 1;
	public int CurrentLevel {
		get { return currentLevel; }
		set { currentLevel = value; }
	}

	void Start() {
		PopulateSocks ();
	}

	void PopulateSocks() {
		int nSocks = LevelDB.GetLevelSockAmount (CurrentLevel);
		for (int i = 0; i < nSocks; i++) {
			
		}
		//Sock s = (Instantiate(sock.gameObject) as GameObject).GetComponent<Sock>();
	}

}
