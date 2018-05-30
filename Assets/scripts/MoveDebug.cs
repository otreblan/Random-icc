using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveDebug : MonoBehaviour {

	RectTransform move;
	float inpu;

	// Use this for initialization
	void Start () {
		move = GetComponent<RectTransform>();
	}
	
	// Update is called once per frame
	void Update () {
		//inpu = Input.GetAxis("Horizontal")*Time.deltaTime();
		move.anchoredPosition +=Time.deltaTime * 100.0f* new Vector2(Input.GetAxis("Horizontal"),Input.GetAxis("Vertical"));
	}
}
