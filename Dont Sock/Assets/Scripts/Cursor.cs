using UnityEngine;
using System.Collections;

public class Cursor : MonoBehaviour {
	
	private Sock connectedSock = null;
	private Vector3 lastPosition;
	private Vector3 currentPosition;
	
	public void Move(Vector3 _to) {
		Vector3 cursorPosition = Camera.main.ScreenToWorldPoint (_to);
		transform.position = cursorPosition;
		
		if (connectedSock) {
			this.connectedSock.UpdatePosition(cursorPosition + Vector3.up);
		}
		
	}
	
	public void Up() {
		if (!connectedSock) {
			return;
		}
		
		connectedSock.Off ((currentPosition - lastPosition) * 10f);//times force
		connectedSock = null;
	}
	
	public void Down() {
		TryFindSock(transform.position);
	}
	
	void FixedUpdate() {
		lastPosition = currentPosition;
		currentPosition = transform.position;		
	}
	
	public void TryFindSock(Vector3 _from) {
		
		RaycastHit2D hit = Physics2D.Raycast(_from, Vector2.zero);
		if(hit.collider != null) {
			connectedSock = hit.collider.gameObject.GetComponent<Sock>();
			connectedSock.On ();
		}
	}
}
