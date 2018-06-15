using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class creaFondo : MonoBehaviour {

	public static System.Random alea;
	public GameObject insta;
	public int fondoNum = 10;

	Animator animator;

	//public static bool cambiaMeme;
	// Use this for initialization
	void Start () {//sin el IEnumerator el yield return new WaitForSeconds(1) no funciona

		alea = new System.Random();
		for (int i = 0; i < fondoNum; i++)
		{
			//yield return new WaitForSeconds(1);
			Instantiate(insta, transform);
		}
		StartCoroutine(movedor(GameObject.FindGameObjectsWithTag("Fondo")));
	}
	IEnumerator movedor(GameObject[] fondos){
		foreach (GameObject fondo in fondos)
        {
			animator = fondo.GetComponent<Animator>();
			yield return new WaitForSeconds(1.0f);
			animator.enabled = true;
			animator.SetFloat("Blend", Random.Range(0.1f, 5.0f));
        }
	}

	/*public void llamaMeme(){
		//esta función cambia el cambiaMeme a verdadero para cambiar el meme
		cambiaMeme = true;
	}*/
}
