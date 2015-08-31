using UnityEngine;
using System.Collections;
using DG;

public class BlackFade : MonoBehaviour {

	private SpriteRenderer spriteRender = null;

	void Awake() {
		transform.position = Vector3.zero;
	}

	void Start () {
		spriteRender = gameObject.GetComponent<SpriteRenderer> ();
		//SpriteRenderer.DoColor ();
	}	
}
