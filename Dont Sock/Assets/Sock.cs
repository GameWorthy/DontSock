using UnityEngine;
using System.Collections;

public class Sock : MonoBehaviour {

	static float orderInLayer = -0.0f;

	private SpriteRenderer sprite;
	private Rigidbody2D body;
	public Rigidbody2D Body
	{
		get { return body; }
	}

	void Start () {
		this.body = GetComponent<Rigidbody2D> ();
		this.sprite = GetComponent<SpriteRenderer> ();
		LayerUp ();
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
}