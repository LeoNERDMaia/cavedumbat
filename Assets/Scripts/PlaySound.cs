using UnityEngine;
using System.Collections;

public class PlaySound : MonoBehaviour {

	//public AudioClip audio;

	public void DoPlaySound() {
		this.audio.Play();
		//AudioSource.PlayClipAtPoint(audio, this.transform.position);
	}

}
