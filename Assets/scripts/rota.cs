using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rota : MonoBehaviour {

	public RectTransform rotador;
	public float rotVel;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		rotador.Rotate(0.0f, 0.0f, rotVel * Time.deltaTime);
	}
}
