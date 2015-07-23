using UnityEngine;
using System.Collections;

public class Sock : MonoBehaviour {

	static int orderInLayer = -1000;

	private SpriteRenderer sprite;
	private Rigidbody2D body;
	public Rigidbody2D Body
	{
		get { return body; }
	}

	void Start () {
		this.body = GetComponent<Rigidbody2D> ();
		this.sprite = GetComponent<SpriteRenderer> ();
		sprite.sortingOrder = orderInLayer;
	}

	public void On() {
		orderInLayer++;
		sprite.sortingOrder = orderInLayer;
	}

	public void Off() {
		Off (Vector3.zero);
	}

	public void Off(Vector3 _force) {
		body.velocity = _force;
	}
}