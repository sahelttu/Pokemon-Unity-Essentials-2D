﻿using UnityEngine;
using System.Collections;

public class BGM_Player : MonoBehaviour {


	public AudioClip bgmSource;
	public float volume;

	public BGM_Player (AudioClip p_bgmSource, float p_volume){
		bgmSource = p_bgmSource;
		volume = p_volume;
	}

	void Start () {
		AudioController.stopBGM();
		AudioController.playBGM(bgmSource.name, volume);
	}

	void Destroy () {
		AudioController.stopBGM();
	}

	void OnDisable () {
		AudioController.stopBGM();
	}
}
