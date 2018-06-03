using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Posicionador : MonoBehaviour {

	RectTransform pos;

	// Use this for initialization
	void Start () {
		pos = GetComponent<RectTransform>();
		pos.SetAsFirstSibling();
		pos.anchoredPosition = new Vector2(-450, creaFondo.alea.Next(-600, 600));
	}
	
	// Update is called once per frame
	public void posi () {
		pos.anchoredPosition = new Vector2(-450, creaFondo.alea.Next(-600, 600));
	}
}
