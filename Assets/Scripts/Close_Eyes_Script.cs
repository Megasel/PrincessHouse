// dnSpy decompiler from Assembly-CSharp.dll class: Close_Eyes_Script
using System;
using System.Collections;
using UnityEngine;

public class Close_Eyes_Script : MonoBehaviour
{
	private IEnumerator Start()
	{
		yield return new WaitForSeconds(0.1f);
		this.close_eyes.SetActive(false);
		yield return new WaitForSeconds(3.1f);
		this.close_eyes.SetActive(true);
		yield return new WaitForSeconds(1.1f);
		this.close_eyes.SetActive(false);
		yield return new WaitForSeconds(3f);
		base.StartCoroutine(this.Start());
		yield break;
	}

	private void Update()
	{
	}

	public GameObject close_eyes;
}
