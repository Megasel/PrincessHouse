// dnSpy decompiler from Assembly-CSharp.dll class: Splash_Scene
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Splash_Scene : MonoBehaviour
{
	private void Start()
	{
		base.StartCoroutine(this.Start_Action());
	}

	private void Update()
	{
	}

	private IEnumerator Start_Action()
	{
		yield return new WaitForSeconds(3f);
		SceneManager.LoadScene("Main_Scene");
		yield break;
	}
}
