using UnityEngine;
using System.Collections;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace GameWorthy {

	public class MedalSystem {
		private static int totalBronze = 0;
		private static int totalSilver = 0;
		private static int totalGold = 0;
		private static int totalPlatinum = 0;
		private static int totalObsidiam = 0;

		public static int TotalBronze {
			get { return totalBronze; }
			set { 
				totalBronze = value; 
				Save();
			}
		}

		public static int TotalSilver {
			get { return totalSilver; }
			set {
				totalSilver = value;
				Save ();
			}
		}

		public static int TotalGold {
			get { return totalGold; }
			set {
				totalGold = value;
				Save ();
			}
		}

		public static int TotalPlatinum {
			get { return totalPlatinum; }
			set {
				totalPlatinum = value;
				Save ();
			}
		}

		public static int TotalObsidiam {
			get { return totalObsidiam; }
			set {
				totalObsidiam = value;
				Save();
			}
		}

		/// <summary>
		/// Reads and sets saved medals
		/// </summary>
		public static void Initiate() {
			Environment.SetEnvironmentVariable("MONO_REFLECTION_SERIALIZER", "yes");
			Load ();
		}

			private static void Save() {
			BinaryFormatter bf = new BinaryFormatter ();
			FileStream file = File.Create (Application.persistentDataPath + "/medals.dat");

			Medals medals = new Medals ();
			medals.bronzes = totalBronze;
			medals.silvers = totalSilver;
			medals.golds = totalGold;
			medals.platinum = totalPlatinum;
			medals.obsidiam = totalObsidiam;

			bf.Serialize (file, medals);
			file.Close ();
		}

		private static void Load() {
			//File.Delete (Application.persistentDataPath + "/medals.dat");
			if (File.Exists (Application.persistentDataPath + "/medals.dat")) {
				BinaryFormatter bf = new BinaryFormatter();
				FileStream file = File.Open(Application.persistentDataPath + "/medals.dat", FileMode.Open);
				Medals medals = (Medals) bf.Deserialize(file);
				file.Close();

				totalBronze = medals.bronzes;
				totalSilver = medals.silvers;
				totalGold = medals.golds;
				totalPlatinum = medals.platinum;
				totalObsidiam = medals.obsidiam;
			}
		}
	}

	[Serializable]
	public class Medals {
		public int bronzes = 0;
		public int silvers = 0;
		public int golds = 0;
		public int platinum = 0;
		public int obsidiam = 0;
	}
}
