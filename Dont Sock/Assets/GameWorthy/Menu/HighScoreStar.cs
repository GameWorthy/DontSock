using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using DG.Tweening;

public class HighScoreStar : MonoBehaviour {

	private bool active = true;
	private float timer = 0;
	[SerializeField] private Image image;

	void Update () {
		if (active) {
			timer -= Time.deltaTime;
			if(timer <= 0) {
				timer = 0.05f;
				//image.color = GenerateRandomColor();
			}
		}
	}

	public void Activate() {
		active = true;
		image.transform.localScale = Vector3.one * 50;
		image.transform.DOScale(Vector3.one,0.4f);
		image.color = new Color(255,0,0,255);
	}

	public void Deactivate() {
		active = false;
		image.color = new Color(0,0,0,0);
	}

	public Color GenerateRandomColor() {
		float red = Random.Range(0.3f,1f);
		float green = Random.Range(0.3f,1f);
		float blue = Random.Range(0.3f,1f);
		
		Color color = new Color(red, green, blue);
		return color;
	}
}
