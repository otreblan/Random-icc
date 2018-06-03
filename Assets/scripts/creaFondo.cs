using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class creaFondo : MonoBehaviour {

	public static System.Random alea;
	public GameObject insta;

	// Use this for initialization
	IEnumerator Start () {//sin el IEnumerator el yield return new WaitForSeconds(1) no funciona
		alea = new System.Random();
		for (int i = 0; i < 10; i++)
		{
			yield return new WaitForSeconds(1);
			Instantiate(insta, transform);
		}
	}
}
