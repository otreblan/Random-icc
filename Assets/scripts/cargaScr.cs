using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;//  <--- Sin esto el script no funciona
using System.Threading.Tasks;
using UnityEngine.Networking;

public class cargaScr : MonoBehaviour {

	public Image image;
	//public Sprite a;
	//string ruta;

    
	Firebase.Storage.FirebaseStorage storage;
	Firebase.Storage.StorageReference imaReference;
	Firebase.Storage.StorageReference vidReference;
    
    Firebase.Storage.StorageReference imaReferencePrue;

	WWW wWW;

	// Use this for initialization
	IEnumerator Start () {
	//	ruta = "jar:file://" + Application.dataPath + "!/assets/" + "algo.jpg";
		storage = Firebase.Storage.FirebaseStorage.DefaultInstance;
		imaReference = storage.GetReferenceFromUrl("gs://randomapp-dd930.appspot.com/Imagenes");
		vidReference = storage.GetReferenceFromUrl("gs://randomapp-dd930.appspot.com/Videos");

        imaReferencePrue = storage.GetReference("Imagenes/32349861_238186500260682_1163204644155949056_n.jpg");

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

		image.sprite = Sprite.Create(wWW.texture, new Rect(0 ,0 ,wWW.texture.width, wWW.texture.height), new Vector2(0.0f, 0.0f));

       
	}
	// Update is called once per frame
	void Update () {
		
	}
}
