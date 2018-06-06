using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cargaScr : MonoBehaviour {

	public MonoBehaviour raw;

	Firebase.Storage.FirebaseStorage storage;
	Firebase.Storage.StorageReference imaReference;
	Firebase.Storage.StorageReference vidReference;

	// Use this for initialization
	void Start () {
		storage = Firebase.Storage.FirebaseStorage.DefaultInstance;
		imaReference = storage.GetReferenceFromUrl("gs://randomapp-dd930.appspot.com/Imagenes");
		vidReference = storage.GetReferenceFromUrl("gs://randomapp-dd930.appspot.com/Videos");
		//raw.guiTexture
	}

	// Update is called once per frame
	void Update () {
		
	}
}
