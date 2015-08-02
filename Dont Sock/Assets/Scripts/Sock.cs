using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Sock : MonoBehaviour {

	static float orderInLayer = -0.0f;
	static int sock_id = 0;

	private static List<Color> colors = new List<Color> {
		Color.white,
		Color.blue,
		Color.cyan,
		Color.gray,
		Color.green,
		Color.magenta,
		Color.red,
		Color.yellow,
		new Color(34,53,255)
	};

	private SpriteRenderer sprite;
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
		this.sprite = GetComponent<SpriteRenderer> ();
		this.transform.localRotation = Quaternion.Euler (0,0,Random.Range(0,360));
		id = sock_id;
		this.sprite.color = colors [id];
		LayerUp ();
		sock_id++;
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