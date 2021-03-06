﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AudioController : MonoBehaviour {

	//path to Audio folders folder
	public static string audioPath = "./Assets/Resources/";
	public static string audioSEPathShort = "Audio/SE/";
	public static string audioBGMPathShort = "Audio/BGM/";
	public static string audioBGSPathShort = "Audio/BGS/";
	public static string audioCryPathShort = "Audio/Cries/";

	//will contain a list of all desired Audio files
  public static List<System.IO.FileInfo> audioSEFiles = new List<System.IO.FileInfo> ();
	public static List<System.IO.FileInfo> audioBGMFiles = new List<System.IO.FileInfo> ();
	public static List<System.IO.FileInfo> audioBGSFiles = new List<System.IO.FileInfo> ();
	public static List<System.IO.FileInfo> audioCryFiles = new List<System.IO.FileInfo> ();


	//specify all file types to include in the array here.
	private static string[] extensionsAllowed = {"*.wav","*.mp3","*.ogg"};

	//flag used to make createAudioList be called once before playing an audio file the first time
	private static bool listsCreated = false;


	//runs through all specified directories looking for the specified files types
	//serves for quicker checking for existence of audio files
	static void createAudioList() {
		string[] info;
		listsCreated = true;
		foreach (string ext in extensionsAllowed) {
			info = System.IO.Directory.GetFiles(audioPath+audioSEPathShort, ext);
			if (info.Length>0) {
				foreach (string f in info) {
					audioSEFiles.Add(new System.IO.FileInfo(f));
				}
			}

			info = System.IO.Directory.GetFiles(audioPath+audioBGMPathShort, ext);
			if (info.Length>0) {
				foreach (string f in info) {
					audioBGMFiles.Add(new System.IO.FileInfo(f));
				}
			}

			info = System.IO.Directory.GetFiles(audioPath+audioBGSPathShort, ext);
			if (info.Length>0) {
				foreach (string f in info) {
					audioCryFiles.Add(new System.IO.FileInfo(f));
				}
			}

			info = System.IO.Directory.GetFiles(audioPath+audioCryPathShort, ext);
			if (info.Length>0) {
				foreach (string f in info) {
					audioBGSFiles.Add(new System.IO.FileInfo(f));
				}
			}
		}
	}



	//different types of audio declared seperately so they can be played at the same time
	static AudioSource audioSourceSE;
	static GameObject tempObjectSE;
	static AudioSource audioSourceBGM;
	static GameObject tempObjectBGM;
	static AudioSource audioSourceBGS;
	static GameObject tempObjectBGS;
	static AudioSource audioSourceCry;
	static GameObject tempObjectCry;
	//coroutine to play audio
	//path expected starting in Resources
	//audioType expected to be "se", "bgm", "bgs", or "cry"
	static IEnumerator playAudio(string filename, string audioType, float volume = 1.0f, bool loop = false, float pitch = 1.0f) {
		if (audioType.ToLower().Equals("se")) {
			tempObjectSE = new GameObject ();
			tempObjectSE.AddComponent<AudioSource> ();
			audioSourceSE  = tempObjectSE.GetComponent<AudioSource> ();
			filename = System.IO.Path.GetFileNameWithoutExtension(filename);
			audioSourceSE.clip = Resources.Load(audioSEPathShort+filename, typeof(AudioClip)) as AudioClip;
			audioSourceSE.volume = volume;
			audioSourceSE.loop = loop;
			audioSourceSE.pitch = pitch;

			audioSourceSE.Play();

		}
		else if (audioType.ToLower().Equals("bgm")) {
			tempObjectBGM = new GameObject ();
			tempObjectBGM.AddComponent<AudioSource> ();
			audioSourceBGM  = tempObjectBGM.GetComponent<AudioSource> ();
			filename = System.IO.Path.GetFileNameWithoutExtension(filename);
			audioSourceBGM.clip = Resources.Load(audioBGMPathShort+filename, typeof(AudioClip)) as AudioClip;
			audioSourceBGM.volume = volume;
			audioSourceBGM.loop = loop;
			audioSourceBGM.pitch = pitch;
			audioSourceBGM.Play();
		}
		else if (audioType.ToLower().Equals("bgs")) {
			tempObjectBGS = new GameObject ();
			tempObjectBGS.AddComponent<AudioSource> ();
			audioSourceBGS  = tempObjectBGS.GetComponent<AudioSource> ();
			filename = System.IO.Path.GetFileNameWithoutExtension(filename);
			audioSourceBGS.clip = Resources.Load(audioBGSPathShort+filename, typeof(AudioClip)) as AudioClip;
			audioSourceBGS.volume = volume;
			audioSourceBGS.loop = loop;
			audioSourceBGS.pitch = pitch;
			audioSourceBGS.Play();
		}
		else if (audioType.ToLower().Equals("cry")) {
			tempObjectCry = new GameObject ();
			tempObjectCry.AddComponent<AudioSource> ();
			audioSourceCry  = tempObjectCry.GetComponent<AudioSource> ();
			filename = System.IO.Path.GetFileNameWithoutExtension(filename);
			audioSourceCry.clip = Resources.Load(audioCryPathShort+filename, typeof(AudioClip)) as AudioClip;
			audioSourceCry.volume = volume;
			audioSourceCry.loop = loop;
			audioSourceCry.pitch = pitch;
			audioSourceCry.Play();
		}
		else {
			Debug.Log("Invalid audio type of" + audioType);
		}
		yield return null;
	}




	public static void playSE(string filename, float volume = 1.0f) {
		if (!listsCreated) {
			AudioController.createAudioList();
		}
		if (audioSourceSE!=null && audioSourceSE.isPlaying) {
			return;
		} else {
			Destroy(audioSourceSE);
			Destroy(tempObjectSE);
		}
		if (volume<0.0f) {
			Debug.Log("Volume must be greater than or equal to 0.0");
			volume = 1.0f;
		} else if (volume>1.0f) {
			Debug.Log("Volume must be less than or equal to 1.0");
			volume = 1.0f;
		}
		//check for the exact filename (with specified extension)
		if (audioSEFiles.Exists(x => x.Name==filename)) {
			StaticCoroutine.DoCoroutine(AudioController.playAudio(filename, "se", volume));
		}
		//if the filename doesn't have an extension, check each extension
		else if (filename == System.IO.Path.GetFileNameWithoutExtension(filename)) {
			foreach (string ext in extensionsAllowed) {
				//Remove function used to remove the * character needed in searching directories
				if (audioSEFiles.Exists(x => x.Name==(filename+(ext.Remove(0, 1))))) {

					StaticCoroutine.DoCoroutine(AudioController.playAudio(filename, "se", volume));
					return;
				}
			}
			Debug.Log("Audio file " + filename + " not found.  Ensure the file exists, and is an accepted file typed.");
		}
		else {
			Debug.Log("Audio file " + filename + " not found.  Ensure you typed the correct extension");
		}

	}

	public static void playBGM(string filename, float volume = 1.0f, float pitch = 1.0f) {
		if (!listsCreated) {
			AudioController.createAudioList();
		}
		if (audioSourceBGM!=null && audioSourceBGM.isPlaying) {
			return;
		} else {
			Destroy(audioSourceBGM);
			Destroy(tempObjectBGM);
		}
		if (volume<0.0f) {
			Debug.Log("Volume must be greater than or equal to 0.0");
			volume = 1.0f;
		} else if (volume>1.0f) {
			Debug.Log("Volume must be less than or equal to 1.0");
			volume = 1.0f;
		}
		//check for the exact filename (with specified extension)
		if (audioBGMFiles.Exists(x => x.Name==filename)) {
			StaticCoroutine.DoCoroutine(AudioController.playAudio(filename, "bgm", volume, true, pitch));
		}
		//if the filename doesn't have an extension, check each extension
		else if (filename == System.IO.Path.GetFileNameWithoutExtension(filename)) {
			foreach (string ext in extensionsAllowed) {
				//Remove function used to remove the * character needed in searching directories
				if (audioBGMFiles.Exists(x => x.Name==(filename+(ext.Remove(0, 1))))) {
					StaticCoroutine.DoCoroutine(AudioController.playAudio(filename, "bgm", volume, true, pitch));
					return;
				}
			}
			Debug.Log("Audio file " + filename + " not found.  Ensure the file exists, and is an accepted file typed.");
		}
		else {
			Debug.Log("Audio file " + filename + " not found.  Ensure you typed the correct extension");
		}

	}

	//type expected to be "se", "bgm", "bgs", or "cry"
	public static void fadeOutSound(string type, float seconds = 1.0f) {

	}

	public static void changeVolumeSE() {

	}

	public static void changeVolumeBG() {
		if (audioSourceBGM==null || !audioSourceBGM.isPlaying) {
			//CancelInvoke("changeVolumeBG");
			return;
		}
	}

	public static void stopBGM() {
		if (audioSourceBGM==null) {
			return;
		}
		audioSourceBGM.Stop();
		audioSourceBGM = null;
	}


}
