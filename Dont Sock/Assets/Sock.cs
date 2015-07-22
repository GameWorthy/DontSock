using UnityEngine;
using System.Collections;

public class Sock : MonoBehaviour {

	private Rigidbody2D body;
	public Rigidbody2D Body
	{
		get { return body; }
	}

	void Start () {
		this.body = GetComponent<Rigidbody2D> ();
	}

	public void On() {
		body.gravityScale = 1;
	}

	public void Off() {
		body.gravityScale = 0;
	}
}