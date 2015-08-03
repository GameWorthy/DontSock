﻿using UnityEngine;
using System.Collections;

public class LevelDB {

	private static object[,] levelsMetadata = new object[,]{
		//index 0: base_level
		//index 1: time_limit
		//index 2: sock_amount

		{1,		20f,	10},
		{3,		19f,	15},
		{6,		18f,	20},
		{9,		17f,	25},
		{12,	16f,	30},
		{15,	15f,	35},
		{18,	14f,	40},
		{21,	13f,	45},
		{24,	12f,	50},
		{27,	11f,	55},
		{30,	10f,	60},
		{33,	09f,	65},
		{36,	08f,	70},
		{39,	07f,	75},
		{42,	06f,	80},
		{45,	05f,	85}
	};

	public static float GetLevelTime(int _level) {


/*
		Debug.Log(GetIndexByLevel(0));
		Debug.Log(GetIndexByLevel(1));
		Debug.Log(GetIndexByLevel(2));
		Debug.Log(GetIndexByLevel(3));
		Debug.Log(GetIndexByLevel(4));
		Debug.Log(GetIndexByLevel(5));
		Debug.Log(GetIndexByLevel(6));
		Debug.Log(GetIndexByLevel(7));
		Debug.Log(GetIndexByLevel(8));
		Debug.Log(GetIndexByLevel(9));
		Debug.Log(GetIndexByLevel(10));
*/
		int index = GetIndexByLevel(_level);
		return (float)levelsMetadata[index,1];
	}

	public static int GetLevelSockAmount(int _level) {
		int index = GetIndexByLevel(_level);
		return (int)levelsMetadata[index,2];	
	}

	private static int cachedLevel = 0;
	private static int cachedIndex = 0;
	///Gets levelsMetadata index by level
	///Uses caching
	private static int GetIndexByLevel(int _level) {

		if(cachedLevel == _level) {
			return cachedIndex;
		}

		cachedLevel = _level;

		int i = 0;
		int length = levelsMetadata.GetLength(0);

		while(i < length) {
			i++;
			if ((int)levelsMetadata[i,0] >= _level) {
				cachedIndex = i;
				return i;
			}
		}
		
		return length;

	}
}
