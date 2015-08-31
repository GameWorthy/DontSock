using UnityEngine;
using System.Collections;

public class LevelDB {

	private static object[,] levelsMetadata = new object[,]{
		//index 0: base_level
		//index 1: time_limit
		//index 2: sock_amount

		{1 ,	10,	10},
		{3 ,	10,	15},
		{5 ,	10,	20},
		{7 ,	10,	25},
		{9 ,	10,	30},
		{11,	10,	35},
		{12,	10,	40},
		{13,	10,	45},
		{14,	10,	50},
		{15,	10,	55},
		{16,	10,	60},
		{19,	10,	65},
		{21,	10,	70},
		{24,	10,	75},
		{27,	10,	80},
		{30,	10,	85},
		{33,	10,	50},
		{36,	10,	95},
		{39,	10,	100},
	};

	public static int GetLevelTime(int _level) {
		int index = GetIndexByLevel(_level);
		return (int)levelsMetadata[index,1];
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
		int length = levelsMetadata.GetLength(0) - 1;

		while(i < length) {
			int ii = i + 1;

			if(ii > length) {
				cachedIndex = ii;
				return ii;
			}

			if (_level >= (int)levelsMetadata[i,0]  &&
				_level <  (int)levelsMetadata[ii,0]) {
				cachedIndex = i;
				return i;
			}
			i++;
		}
		
		return length;
	}


}
