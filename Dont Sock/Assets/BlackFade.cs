using UnityEngine;
using System.Collections;
using DG.Tweening;

public class BlackFade : MonoBehaviour {

	private SpriteRenderer spriteRender = null;

	void Awake() {
		transform.position = Vector3.zero;
	}

	void Start () {
		spriteRender = gameObject.GetComponent<SpriteRenderer> ();
		spriteRender.DOColor(new Color(0,0,0,0),1);
	}	
}
