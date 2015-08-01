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
		this.transform.localRotation = Quaternion.Euler (0,0,Random.Range(0,90));
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

	public void UpdatePosition(Vector3 _newPos) {
		transform.position = new Vector3 (_newPos.x, _newPos.y, orderInLayer);
	}
}