using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SockDB : MonoBehaviour {

	private static List<Sprite> socksSprites = new List<Sprite>();
	private static int totalSocks = 0;
	public static int TotalSocks {
		get { return totalSocks; }
		private set { totalSocks = value; }
	}
	
	void Awake () {
		foreach (Sprite s in Resources.LoadAll<Sprite> ("Socks")) {
			socksSprites.Add(s);
		}
		TotalSocks = socksSprites.Count;
	}

	public static Sprite GetSockSprite(int index) {
		if (index >= TotalSocks)
			index = 0;
		return socksSprites[index];
	}
	
	public static int[] GetRandomUniqueSocks(int _nSocks) {
		int[] socks = new int[_nSocks];

		int start = Random.Range (0, TotalSocks - 1);
		int step = Random.Range (4, 11);
		int loop = 1;
		int id = 0;
		for (int i = 0; i < _nSocks; i++) {
			id += step;
			if(id >= TotalSocks) {
				id = step - loop++;
			}
			socks[i] = id;
		}
		
		return socks;
	}

}
