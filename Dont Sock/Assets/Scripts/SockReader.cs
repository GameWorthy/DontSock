using UnityEngine;
using System.Collections;

public class SockReader : MonoBehaviour {

	[SerializeField] private SpriteRenderer sockToFind = null;
	private int sockToFindID = 0;

	void Start () {
		sockToFindID = (int) Random.Range (0, SockDB.TotalSocks - 1);
		sockToFind.sprite = SockDB.GetSockSprite(sockToFindID);
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.tag == "Sock") {
			Sock sock = other.GetComponent<Sock>();
			if(sock.ID == sockToFindID) {
				Debug.Log("Yes");
			}
			else {
				Destroy(sock.gameObject);
			}
		}
	}
}
