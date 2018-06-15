using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;//  <--- Sin esto el script no funciona
using System.Threading.Tasks;
using UnityEngine.Networking;

public class cargaScr : MonoBehaviour {

	public Image image;

	public Sprite spriteFondo;
	public Sprite spriteFondoBlur;
	public Animator canvasAnimator;
	Queue<Sprite> memes;
    
	Firebase.Storage.FirebaseStorage storage;
	Firebase.Storage.StorageReference imaReference;
	Firebase.Storage.StorageReference vidReference;
	Firebase.Storage.StorageReference textReference;
       
	WWW wWWText;

	string[] nombres;

	bool memeEnPantalla = false;

	// Use this for initialization
	IEnumerator Start () {
		memes = new Queue<Sprite>(10);

		storage = Firebase.Storage.FirebaseStorage.DefaultInstance;
		imaReference = storage.GetReferenceFromUrl("gs://randomapp-dd930.appspot.com/Imagenes");
		vidReference = storage.GetReferenceFromUrl("gs://randomapp-dd930.appspot.com/Videos");

		textReference = imaReference.Child("INDEX.txt");

		textReference.GetDownloadUrlAsync().ContinueWith((Task<System.Uri> task) => {
            if (!task.IsFaulted && !task.IsCanceled)
            {
                Debug.Log("Download URL: " + task.Result);
                // ... now download the file via WWW or UnityWebRequest.
				wWWText = new WWW(System.Convert.ToString(task.Result));
            }
        });

		yield return new WaitUntil(() => wWWText != null);
		yield return new WaitUntil(() => wWWText.isDone);

		nombres = wWWText.text.Split('\n');

		StartCoroutine(cargaImagen());

       
	}


	// Update is called once per frame
	public void memeSiguente(){
		StartCoroutine(cargaMeme());
		if (!memeEnPantalla)
		{
			StartCoroutine(cambiaFondo());
		}
		else
		{
			canvasAnimator.Play("cambiaMeme");

		}
	}

	IEnumerator cargaMeme(){
		//print(1);
		yield return new WaitForSeconds(0.5f);
		yield return new WaitUntil(() => memes.Count > 0);
		//creaFondo.cambiaMeme = false;
		print(2);
        image.sprite = memes.Dequeue();
		StartCoroutine(cargaImagen());
	}


	IEnumerator cambiaFondo(){
		//Esto cambia los sprites de la cosas del fondo a la versión borrosa
		GameObject[] fondos = GameObject.FindGameObjectsWithTag("Fondo");
		yield return new WaitUntil(() => fondos != null);
		foreach (GameObject fondo in fondos)
		{
			Image fondoImage = fondo.GetComponent<Image>();
			fondoImage.sprite = spriteFondoBlur;
			fondoImage.SetNativeSize();
		}
		memeEnPantalla = true;
		print(memeEnPantalla);
    }


	IEnumerator cargaImagen(){
		Firebase.Storage.StorageReference imaReferen = imaReference.Child(nombres[Random.Range(0, nombres.Length)]);

		WWW wWW = null;
        //imaReferencePrue.GetFileAsync(ruta);//está incompleto, creo
        // Fetch the download URL
        
		imaReferen.GetDownloadUrlAsync().ContinueWith((Task<System.Uri> task) => {
            if (!task.IsFaulted && !task.IsCanceled)
            {
                Debug.Log("Download URL: " + task.Result);
                // ... now download the file via WWW or UnityWebRequest.
                wWW = new WWW(System.Convert.ToString(task.Result));
            }
        });

        yield return new WaitUntil(() => wWW != null);
        yield return new WaitUntil(() => wWW.isDone);

        memes.Enqueue(Sprite.Create(wWW.texture, new Rect(0, 0, wWW.texture.width, wWW.texture.height), new Vector2(0.0f, 0.0f)));
		if (memes.Count < 10)
		{
			StartCoroutine(cargaImagen());
		}
	}
}
