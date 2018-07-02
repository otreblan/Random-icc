using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Video : MonoBehaviour {
 

	public void video(){
		//Handheld.PlayFullScreenMovie("gundam.mp4", Color.black, FullScreenMovieControlMode.Full, FullScreenMovieScalingMode.AspectFit);
		Handheld.PlayFullScreenMovie(Application.persistentDataPath + "/gundam.mp4", Color.black, FullScreenMovieControlMode.Full, FullScreenMovieScalingMode.AspectFit);
		//Handheld.PlayFullScreenMovie("https://firebasestorage.googleapis.com/v0/b/randomapp-dd930.appspot.com/o/Videos%2FMobile%20Suit%20Gundam%20Iron%20Blooded%20Orphans%20(Season%202)%20-%20Opening%203%20(HD)%20-%20Rage%20of%20Dust.mp4?alt=media&token=902b1809-8f84-45dc-ad38-a66170aa0df6", Color.black, FullScreenMovieControlMode.Full, FullScreenMovieScalingMode.AspectFit);
		//Handheld.PlayFullScreenMovie("https://youtu.be/8ipyR85BP6c", Color.black, FullScreenMovieControlMode.Full, FullScreenMovieScalingMode.AspectFit);
        
	}
}
