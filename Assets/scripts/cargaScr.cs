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
	//public Sprite a;
	//string ruta;
	Queue<Sprite> memes;
    
	Firebase.Storage.FirebaseStorage storage;
	Firebase.Storage.StorageReference imaReference;
	Firebase.Storage.StorageReference vidReference;
    
    Firebase.Storage.StorageReference imaReferencePrue;

	WWW wWW;

	bool memeEnPantalla = false;

	// Use this for initialization
	IEnumerator Start () {
		//	ruta = "jar:file://" + Application.dataPath + "!/assets/" + "algo.jpg";
		memes = new Queue<Sprite>(10);

		storage = Firebase.Storage.FirebaseStorage.DefaultInstance;
		imaReference = storage.GetReferenceFromUrl("gs://randomapp-dd930.appspot.com/Imagenes");
		vidReference = storage.GetReferenceFromUrl("gs://randomapp-dd930.appspot.com/Videos");

		imaReferencePrue = imaReference.Child("wrn8vj4j72y01.jpg");

        //imaReferencePrue.GetFileAsync(ruta);//está incompleto, creo
		// Fetch the download URL

		imaReferencePrue.GetDownloadUrlAsync().ContinueWith((Task<System.Uri> task) => {
            if (!task.IsFaulted && !task.IsCanceled)
            {
				Debug.Log("Download URL: " + task.Result);
				// ... now download the file via WWW or UnityWebRequest.
				wWW = new WWW(System.Convert.ToString(task.Result));
            }
        });

		yield return new WaitUntil(() => wWW != null);
		yield return new WaitUntil(() => wWW.isDone);

		memes.Enqueue(Sprite.Create(wWW.texture, new Rect(0 ,0 ,wWW.texture.width, wWW.texture.height), new Vector2(0.0f, 0.0f)));

       
	}
	// Update is called once per frame
	public void memeSiguente(){
		StartCoroutine(cargaMeme());
		if (!memeEnPantalla)
		    StartCoroutine(cambiaFondo());
	}

	IEnumerator cargaMeme(){
		//print(1);
		yield return new WaitUntil(() => memes.Count > 0);
		//print(2);
        image.sprite = memes.Dequeue();
	}

	IEnumerator cambiaFondo(){
		GameObject[] fondos = GameObject.FindGameObjectsWithTag("Fondo");
		yield return new WaitUntil(() => fondos != null);
		foreach (GameObject fondo in fondos)
		{
			Image fondoImage = fondo.GetComponent<Image>();
			fondoImage.sprite = spriteFondoBlur;
			fondoImage.SetNativeSize();
		}
		memeEnPantalla = true;
    }
}
