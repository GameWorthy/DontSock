using UnityEngine;
using System.Collections;

public class SockReader : MonoBehaviour {

	[SerializeField] private SpriteRenderer sockToFind = null;
	[SerializeField] private Transform secondSock = null;
	private int sockToFindID = 0;
	private Game game = null;

	void OnTriggerEnter2D(Collider2D other) {
		if (other.tag == "Sock") {
			Sock sock = other.GetComponent<Sock>();
			if(sock.ID == sockToFindID) {
				if(game) {
					game.NextLevel();
				}
			}
			else {
				game.GameOver();
			}
		}
	}

	public void SetTarget(int _targetID) {
		sockToFindID = _targetID;
		sockToFind.sprite = SockDB.GetSockSprite(sockToFindID);
	}

	public void SetGame(Game _game) {
		this.game = _game;
	}
	
	public void ReaderOn() {
		this.GetComponent<Collider2D> ().enabled = true;
	}
	
	public void ReaderOff() {
		this.GetComponent<Collider2D> ().enabled = false;
	}
}
