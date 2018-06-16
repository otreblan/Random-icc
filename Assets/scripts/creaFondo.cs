using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class creaFondo : MonoBehaviour {

	public static System.Random alea;
	public GameObject insta;
	public int fondoNum = 10;

	Animator animator;
	static public GameObject[] fondosArr;
	//Image image;

	//public static bool cambiaMeme;
	// Use this for initialization
	void Start () {//sin el IEnumerator el yield return new WaitForSeconds(1) no funciona
		fondosArr = new GameObject[fondoNum];
		alea = new System.Random();
		for (int i = 0; i < fondoNum; i++)
		{
			//yield return new WaitForSeconds(1);
			fondosArr[i] = Instantiate(insta, transform);
		}
		StartCoroutine(movedor(fondosArr));
	}

	IEnumerator movedor(GameObject[] fondos){
		foreach (GameObject fondo in fondos)
        {
			//image = fondo.GetComponent<Image>();

			yield return new WaitForSeconds(1.0f);
			animator = fondo.GetComponent<Animator>();
			fondo.SetActive(true);
			//animator.enabled = true;
			//image.enabled = true;
			animator.SetFloat("Blend", Random.Range(0.1f, 5.0f));//Esta cosa pone la velocidad de las cosas aleatoriamente
        }
	}

	/*public void llamaMeme(){
		//esta función cambia el cambiaMeme a verdadero para cambiar el meme
		cambiaMeme = true;
	}*/
}
