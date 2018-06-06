using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rota : MonoBehaviour {
	
	public float rotVel;
	public bool invertido;


	RectTransform rotador;
	float pulsos;//esto podría haber sido público y modíficado con una animación para evitar el overflow
	Quaternion rotaAng;

	// Use this for initialization
	void Start () {
		rotador = GetComponent<RectTransform>();
	}
	
	// Update is called once per frame
	void Update () {
		//rotador.Rotate(0.0f, 0.0f, rotVel * Time.deltaTime);
		pulsos = Time.time*rotVel % 1;
		if (invertido)
		{
			pulsos = 1 - pulsos;
		}
		rotaAng = Quaternion.Euler(0.0f, 0.0f, 30.0f*Mathf.Sin(2 * Mathf.PI * pulsos));
		rotador.rotation = rotaAng;

		//Debug.Log(pulsos);
	}
}
