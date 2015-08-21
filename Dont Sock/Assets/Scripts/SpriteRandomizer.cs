using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpriteRandomizer : MonoBehaviour {


	[SerializeField] private List<Sprite> sprites = null;
	[SerializeField] private bool randomOnStart = false;

	void Start() {
		if (randomOnStart) {
			Randomize();
		}
	}

	public void Randomize () {
		this.GetComponent<SpriteRenderer>().sprite = sprites[Random.Range(0,sprites.Count)];	
	}
}
