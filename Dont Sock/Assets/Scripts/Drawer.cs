using UnityEngine;
using System.Collections;
using DG.Tweening;

public class Drawer : MonoBehaviour {

	[SerializeField] private bool isOpen = true;
	[SerializeField] private Transform drawer = null;
	[SerializeField] private AudioSource audioSource = null;
	[SerializeField] private AudioClip openClip = null;
	[SerializeField] private AudioClip closeClip = null;
	private float drawerSpeed = 0.35f;

	public float DrawerSpeed {
		get { return drawerSpeed;}
		private set {drawerSpeed = value;}
	}

	public Transform DrawerTransform {
		get {return drawer;}
		private set {drawer = value;}
	}

	private Vector3 closePosition = Vector3.zero;
	private Vector3 openPosition = Vector3.zero;
	void Start() {
		closePosition = drawer.localPosition;
		openPosition = closePosition - new Vector3 (0, 6.3f, 0);//opens down
	}

	public void Open() {
		if (!isOpen) {
			audioSource.clip = openClip;
			audioSource.Play ();
			drawer.DOLocalMove (openPosition, DrawerSpeed);	
			isOpen = true;
		}
	}

	public void Close() {
		if (isOpen) {
			audioSource.clip = closeClip;
			audioSource.Play ();
			drawer.DOLocalMove (closePosition, DrawerSpeed);
			isOpen = false;
		}
	}
}
