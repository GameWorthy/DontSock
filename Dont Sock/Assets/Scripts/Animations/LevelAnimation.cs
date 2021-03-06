﻿using UnityEngine;
using System.Collections;

public class LevelAnimation : MonoBehaviour {

	[SerializeField] private TextMesh text = null;
	[SerializeField] private Animator animator = null;

	void Start() {
		Stop ();
		SetText ();
	}

	void Stop() {
		animator.speed = 0;
	}

	public void Play() {
		animator.Play (0);
		animator.speed = 1;
	}

	public void SetText (string textToSet = "") {
		text.text = textToSet;
	}
}
