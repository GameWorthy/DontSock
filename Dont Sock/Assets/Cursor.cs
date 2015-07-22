using UnityEngine;
using System.Collections;

public class Cursor : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 cursorPosition = Input.mousePosition;
		cursorPosition = Camera.main.ScreenToWorldPoint (cursorPosition);
		transform.position = cursorPosition;
	}
}
