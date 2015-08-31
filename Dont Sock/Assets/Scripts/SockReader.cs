using UnityEngine;
using System.Collections;

public class SockReader : MonoBehaviour {

	[SerializeField] private SpriteRenderer sockToFind = null;
	[SerializeField] private Transform secondSock = null;
	[SerializeField] private Animator anim = null;
	[SerializeField] private AudioClip correctClip;
	[SerializeField] private AudioClip incorrectClip = null;
	private AudioSource audioSource = null;

	private int sockToFindID = 0;
	private Game game = null;

	void Start() {
		audioSource = gameObject.GetComponent<AudioSource> ();
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.tag == "Sock") {
			Sock sock = other.GetComponent<Sock>();
			if(sock.ID == sockToFindID) {
				sock.Locked = true;
				sock.transform.parent = secondSock;
				sock.transform.localPosition = Vector3.zero;
				sock.transform.localScale = Vector3.one;
				sock.transform.rotation = Quaternion.identity;
				if(game) {
					game.NextLevel();
				}
				audioSource.clip = correctClip;
				audioSource.Play ();
				anim.SetInteger("state",0);
			}
			else {
				game.GameOver();
				audioSource.clip = incorrectClip;
				audioSource.Play ();
				anim.SetInteger("state",0);
			}
		}
	}

	public void SetTarget(int _targetID) {
		sockToFindID = _targetID;
		sockToFind.sprite = SockDB.GetSockSprite(sockToFindID);
	}

	public void Show() {
		anim.SetInteger("state",1);
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

	public void SetAnimationSpeed(float _speed) {
		anim.speed = _speed;
	}
}
