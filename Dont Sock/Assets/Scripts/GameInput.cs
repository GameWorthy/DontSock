using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameInput : MonoBehaviour {

	[SerializeField] private List<Cursor> cursors = new List<Cursor>();

	private bool resetTouches = false;

	void Update () {

		if (Input.touchCount > 0) {

			for (int i = 0; i < Input.touchCount; i++) {
				Touch t = Input.touches [i];
				Cursor c = cursors [i];

				c.Move (t.position);
				if (t.phase == TouchPhase.Began) {
					c.Down ();
				} else if (t.phase == TouchPhase.Ended ||
					t.phase == TouchPhase.Canceled) {
					c.Up ();
				}
			}

			resetTouches = true;

			return;//Don't read the mouse events
		}
		else if (resetTouches) {
			foreach (Cursor c in cursors) {
				c.Up();
			}
			resetTouches = false;
		}

		cursors[0].Move(Input.mousePosition);

		if(Input.GetMouseButtonDown(0)) {
			cursors[0].Down();
		}

		if (Input.GetMouseButtonUp (0)) {
			cursors[0].Up ();
		}
	}
}
