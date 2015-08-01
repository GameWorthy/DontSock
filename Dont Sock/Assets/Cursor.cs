using UnityEngine;
using System.Collections;

public class Cursor : MonoBehaviour {

	private Sock connectedSock = null;
	private Vector3 lastPosition;
	private Vector3 currentPosition;

	void Start () {

	}
	
	void Update () {

		if (!connectedSock) {
			if (Input.GetMouseButtonDown (0) ||
			    Input.touchCount > 0 ) {
				TryFindSock();
			}
			return;
		}

		Vector3 cursorPosition = Camera.main.ScreenToWorldPoint (Input.mousePosition);

		transform.position = cursorPosition;

		this.connectedSock.UpdatePosition(cursorPosition);
		
		if (Input.GetMouseButtonUp (0) ||
			Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended) {
			connectedSock.Off ((cursorPosition - lastPosition) * 10f);//times force
			connectedSock = null;
		}

	}

	void FixedUpdate() {
		lastPosition = currentPosition;
		currentPosition = transform.position;		
	}

	void TryFindSock() {

		Vector3 mousePosition = Camera.main.ScreenToWorldPoint (Input.mousePosition);
		RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);

		if(hit.collider != null) {
			connectedSock = hit.collider.gameObject.GetComponent<Sock>();
			connectedSock.On ();
		}
	}
}
