// dnSpy decompiler from Assembly-CSharp.dll class: Fan_Anim
using System;
using System.Collections;
using UnityEngine;

public class Fan_Anim : MonoBehaviour
{
	private void Start()
	{
	}

	private void Update()
	{
	}

	private IEnumerator Fane_Btn()
	{
		yield return new WaitForSeconds(0.1f);
		if (this.fan.enabled)
		{
			this.fan.enabled = false;
			this.fan_s.Stop();
		}
		else if (!this.fan.enabled)
		{
			this.fan.enabled = true;
			this.fan_s.Play();
		}
		yield break;
	}

	public Animator fan;

	public AudioSource fan_s;
}
