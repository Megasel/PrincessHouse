// dnSpy decompiler from Assembly-CSharp.dll class: tk2dUIAudioManager
using System;
using UnityEngine;

[AddComponentMenu("2D Toolkit/UI/Core/tk2dUIAudioManager")]
public class tk2dUIAudioManager : MonoBehaviour
{
	public static tk2dUIAudioManager Instance
	{
		get
		{
			if (tk2dUIAudioManager.instance == null)
			{
				tk2dUIAudioManager.instance = (UnityEngine.Object.FindObjectOfType(typeof(tk2dUIAudioManager)) as tk2dUIAudioManager);
				if (tk2dUIAudioManager.instance == null)
				{
					tk2dUIAudioManager.instance = new GameObject("tk2dUIAudioManager").AddComponent<tk2dUIAudioManager>();
				}
			}
			return tk2dUIAudioManager.instance;
		}
	}

	private void Awake()
	{
		if (tk2dUIAudioManager.instance == null)
		{
			tk2dUIAudioManager.instance = this;
		}
		else if (tk2dUIAudioManager.instance != this)
		{
			UnityEngine.Object.Destroy(this);
			return;
		}
		this.Setup();
	}

	private void Setup()
	{
		if (this.audioSrc == null)
		{
			this.audioSrc = base.gameObject.GetComponent<AudioSource>();
		}
		if (this.audioSrc == null)
		{
			this.audioSrc = base.gameObject.AddComponent<AudioSource>();
			this.audioSrc.playOnAwake = false;
		}
	}

	public void Play(AudioClip clip)
	{
		this.audioSrc.PlayOneShot(clip, AudioListener.volume);
	}

	private static tk2dUIAudioManager instance;

	private AudioSource audioSrc;
}
