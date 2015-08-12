using UnityEngine;
using System.Collections;
using DG.Tweening;

public class Drawer : MonoBehaviour {

	[SerializeField] private Transform drawer = null;

	public Transform DrawerTransform
	{
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
		drawer.DOLocalMove (openPosition, 1f);	
	}

	public void Close() {
		drawer.DOLocalMove (closePosition, 1f);
	}
}
