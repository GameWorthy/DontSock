﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Sock : MonoBehaviour {

	static float orderInLayer = 0.0f;

	private SpriteRenderer spriteRender = null;

	private int id = 0;
	public int ID {
		get { return id; }
		private set { id = value; }
	}

	private bool locked = false;
	public bool Locked {
		get {return locked;}
		set {
			locked = value;
			if(locked) {
				spriteRender.sortingLayerName = "TopDrawer";
				body.velocity = Vector3.zero;
			}
		}
	}

	public void RestartLayerOrder() {
		orderInLayer = 0.0f;
	}

	private Rigidbody2D body;
	public Rigidbody2D Body {
		get { return body; }
	}

	void Start () {
		this.body = GetComponent<Rigidbody2D> ();
		this.spriteRender = gameObject.GetComponent<SpriteRenderer> ();

		this.transform.localRotation = Quaternion.Euler (0,0,Random.Range(0,360));
		if (Random.value > 0.5f) {
			this.transform.localScale = new Vector3(
				-this.transform.localScale.x,
				this.transform.localScale.y,
				this.transform.localScale.z
				);
		}

		LayerUp ();
	}

	public void SetID(int _id) {
		if (this.spriteRender) {
			this.spriteRender.sprite = SockDB.GetSockSprite (_id);
		} else {
			Debug.LogError("No Renderer Found",this);
		}
		ID = _id;
	}

	public void On() {
		if (!Locked) {
			spriteRender.sortingLayerName = "TopDrawer";
			LayerUp ();
		}
	}

	public void Off() {
		Off (Vector3.zero);
	}

	public void Off(Vector3 _force) {
		if (!Locked) {
			spriteRender.sortingLayerName = "Sock";
			body.velocity = _force;
		}
	}

	public void LayerUp() {
		orderInLayer -= 0.001f;
		this.transform.localPosition = new Vector3 (
			transform.localPosition.x,
			transform.localPosition.y,
			orderInLayer);
	}

	public void UpdatePosition(Vector3 _newPos) {
		if (!Locked) {
			transform.position = new Vector3 (_newPos.x, _newPos.y, orderInLayer);
		}
	}
}