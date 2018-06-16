using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;//  <--- Sin esto el script no funciona
using System.Threading.Tasks;
using UnityEngine.Networking;

public class cargaScr : MonoBehaviour {

	float memePropor; //esta es la proporción de la textura del meme

	public RawImage image;

	public Sprite spriteFondo;
	public Sprite spriteFondoBlur;
	public Animator canvasAnimator;
	Queue<Texture2D> memes;
	RectTransform rectTransform;
	Texture meme;
    
	Firebase.Storage.FirebaseStorage storage;
	Firebase.Storage.StorageReference imaReference;
	Firebase.Storage.StorageReference vidReference;
	Firebase.Storage.StorageReference textReference;
       
	WWW wWWText;

	string[] nombres;

	bool memeEnPantalla = false;
	bool cargaMemeCorriendo;//esto da verdadero si la corrutina del cargado del meme está corriendo
    
	IEnumerator Start () {
		rectTransform = GetComponent<RectTransform>();
		memes = new Queue<Texture2D>(2);

		storage = Firebase.Storage.FirebaseStorage.DefaultInstance;
		imaReference = storage.GetReferenceFromUrl("gs://randomapp-dd930.appspot.com/Imagenes");
		vidReference = storage.GetReferenceFromUrl("gs://randomapp-dd930.appspot.com/Videos");

		textReference = imaReference.Child("INDEX.txt");

		textReference.GetDownloadUrlAsync().ContinueWith((Task<System.Uri> task) => {
            if (!task.IsFaulted && !task.IsCanceled)
            {
                //Debug.Log("Download URL: " + task.Result);
                // ... now download the file via WWW or UnityWebRequest.
				wWWText = new WWW(System.Convert.ToString(task.Result));
            }
        });

		yield return new WaitUntil(() => wWWText != null);
		yield return new WaitUntil(() => wWWText.isDone);

		nombres = wWWText.text.Split('\n');

		StartCoroutine(cargaImagen());

       
	}

    
	public void memeSiguente(){      
		if (!memeEnPantalla)
		{
			StartCoroutine(cargaMeme());
			cambiaFondo();
		}
		else if (!cargaMemeCorriendo)
		{
			StartCoroutine(cargaMeme());
			canvasAnimator.Play("cambiaMeme");

		}
	}

	IEnumerator cargaMeme(){
		
		cargaMemeCorriendo = true;
		yield return new WaitForSeconds(0.5f);
		yield return new WaitUntil(() => memes.Count > 0);

		Destroy(meme);//Con esto porfín la memoría ya no se llena de texturas ni se crashea

		meme = memes.Dequeue();
		image.texture = meme;


		memePropor = (meme.width * 1f) / (meme.height * 1f);

		if (memePropor < 1)
		{
			rectTransform.localScale = new Vector3(memePropor, 1f, 1f);
		}
		else
		{
			rectTransform.localScale = new Vector3(1f, (1f/memePropor), 1f);
		}

        
		yield return new WaitForSeconds(0.5f);
		cargaMemeCorriendo = false;
	}


	void cambiaFondo(){
		//Esto cambia los sprites de la cosas del fondo a la versión borrosa

		foreach (GameObject fondo in creaFondo.fondosArr)
		{
			Image fondoImage = fondo.GetComponent<Image>();
			fondoImage.sprite = spriteFondoBlur;
			fondoImage.SetNativeSize();
		}
		memeEnPantalla = true;
    }


	IEnumerator cargaImagen(){
		Firebase.Storage.StorageReference imaReferen = imaReference.Child(nombres[Random.Range(0, nombres.Length)]);

		WWW wWW = null;
        
		imaReferen.GetDownloadUrlAsync().ContinueWith((Task<System.Uri> task) => {
            if (!task.IsFaulted && !task.IsCanceled)
            {
                //Debug.Log("Download URL: " + task.Result);
                // ... now download the file via WWW or UnityWebRequest.
                wWW = new WWW(System.Convert.ToString(task.Result));
            }
        });

        yield return new WaitUntil(() => wWW != null);
        yield return new WaitUntil(() => wWW.isDone);

		memes.Enqueue(wWW.texture);
		yield return new WaitUntil(() => memes.Count < 2);
		StartCoroutine(cargaImagen());
	}
}
