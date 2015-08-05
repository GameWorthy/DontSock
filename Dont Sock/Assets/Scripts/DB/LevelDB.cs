using UnityEngine;
using System.Collections;

public class LevelDB {

	private static object[,] levelsMetadata = new object[,]{
		//index 0: base_level
		//index 1: time_limit
		//index 2: sock_amount

		{1 ,	20,	10},
		{3 ,	19,	15},
		{6 ,	18,	20},
		{9 ,	17,	25},
		{12,	16,	30},
		{15,	15,	35},
		{18,	14,	40},
		{21,	13,	45},
		{24,	12,	50},
		{27,	11,	55},
		{30,	10,	60},
		{33,	09,	65},
		{36,	08,	70},
		{39,	07,	75},
		{42,	06,	80},
		{45,	05,	85}
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
