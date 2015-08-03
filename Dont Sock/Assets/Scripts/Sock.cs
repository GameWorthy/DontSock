using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Sock : MonoBehaviour {

	static float orderInLayer = -0.0f;

	private SpriteRenderer spriteRender;
	private Quaternion initialRotation;
	private Vector3 initialScale;

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

		initialScale = transform.localScale;
		initialRotation = transform.localRotation;

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
		this.transform.localScale = new Vector3 (
			Mathf.Abs(this.transform.localScale.x * 1.25f),
			this.transform.localScale.y * 1.25f,
			1
		);
		this.transform.localRotation = Quaternion.identity;
		this.spriteRender.color = new Color (255,255,255,0.65f);
		LayerUp ();
	}

	public void Off() {
		Off (Vector3.zero);
	}

	public void Off(Vector3 _force) {
		this.transform.localScale = initialScale;
		this.transform.localRotation = initialRotation;
		this.transform.localPosition -= Vector3.up;
		this.spriteRender.color = Color.white;
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