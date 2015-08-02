using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SockDB : MonoBehaviour {

	private static List<Sprite> socksSprites = new List<Sprite>();
	private static int totalSocks = 0;
	public static int TotalSocks
	{
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
}
