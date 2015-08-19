using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Menu : MonoBehaviour {

	[SerializeField] private List<RectTransform> menusToCenter = null;

	void Start () {
		foreach (RectTransform rect in menusToCenter) {
			rect.position = Vector2.zero;
			rect.sizeDelta = Vector2.zero;
		}
	}
}
