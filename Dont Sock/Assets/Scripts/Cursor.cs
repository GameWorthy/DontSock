using UnityEngine;
using System.Collections;
using DG.Tweening;

public class Cursor : MonoBehaviour {

	[SerializeField] private SpriteRenderer sr = null;

	private Sock connectedSock = null;
	private Vector3 lastPosition;
	private Vector3 currentPosition;
	
	public void Move(Vector3 _to) {
		Vector3 cursorPosition = Camera.main.ScreenToWorldPoint (_to);
		transform.position = cursorPosition;
		
		if (connectedSock && !connectedSock.Locked) {
			this.connectedSock.UpdatePosition (cursorPosition);
		}		
	}

	void Update() {
		if(!connectedSock)
			sr.sprite = null;
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

		if (connectedSock) {
			sr.sprite = SockDB.GetSockSprite(connectedSock.ID);
			sr.transform.position = connectedSock.transform.position;
			sr.transform.rotation = connectedSock.transform.rotation;
			sr.transform.localScale = Vector3.one;
			float tweenTime = 0.20f;
			sr.transform.DOLocalMove(new Vector3(0,1,1),tweenTime);
			sr.transform.DORotate(Vector3.zero,tweenTime);
			sr.transform.DOScale(Vector3.one * 1.5f, tweenTime);
		}
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
