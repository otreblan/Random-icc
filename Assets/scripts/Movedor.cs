using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movedor : MonoBehaviour {
	RectTransform tra;
	int senti;
	//System.Random random;

	// Use this for initialization
	void Start () {
		tra = this.GetComponent<RectTransform>();
		tra.SetAsFirstSibling();
		//random = new System.Random();

		switch (CreaFondo.random.Next(2))
		{
			case 0:
				senti = 10;
				break;
			case 1:
				senti = -10;
				break;
		}
	}   
	
	// Update is called once per frame
	void Update () {
		tra.Rotate(new Vector3(0.0f, 0.0f, Time.deltaTime*senti));
	}
}
