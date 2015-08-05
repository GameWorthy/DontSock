using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Sock : MonoBehaviour {

	static float orderInLayer = -0.0f;

	private SpriteRenderer spriteRender = null;

	private int id = 0;
	public int ID
	{
		get { return id; }
		private set { id = value; }
	}


	private Rigidbody2D body;
	public Rigidbody2D Body
	{
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
		LayerUp ();
	}

	public void Off() {
		Off (Vector3.zero);
	}

	public void Off(Vector3 _force) {
		body.velocity = _force;
	}

	public void LayerUp() {
		orderInLayer -= 0.001f;
		this.transform.localPosition = new Vector3 (
			transform.localPosition.x,
			transform.localPosition.y,
			orderInLayer);
	}

	public void UpdatePosition(Vector3 _newPos) {
		transform.position = new Vector3 (_newPos.x, _newPos.y, orderInLayer);
	}
}