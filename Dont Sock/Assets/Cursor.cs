using UnityEngine;
using System.Collections;

public class Cursor : MonoBehaviour {

	private Sock connectedSock = null;
	private DistanceJoint2D joint;


	void Start () {
		joint = GetComponent<DistanceJoint2D> ();
	}
	
	void Update () {

		if (Input.GetMouseButtonDown (0) ||
		    Input.touchCount > 0 ) {
			TryFindSock();
		}

		if (!connectedSock)
			return;

		if (Input.GetMouseButtonUp (0) ||
			Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended) {
			connectedSock.Off ();
			connectedSock = null;
		}


		Vector3 cursorPosition = Input.mousePosition;
		cursorPosition = Camera.main.ScreenToWorldPoint (cursorPosition);
		transform.position = cursorPosition;
	}

	void TryFindSock() {

		Vector3 mousePosition = Camera.main.ScreenToWorldPoint (Input.mousePosition);

		RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);
		if(hit.collider != null) {
			connectedSock = hit.collider.gameObject.GetComponent<Sock>();
			Vector3 distance = mousePosition - connectedSock.transform.position;
			joint.connectedBody = connectedSock.Body;
			joint.connectedAnchor = distance;
			joint.distance = 0.05f;
			connectedSock.On ();
		}
	}
}
