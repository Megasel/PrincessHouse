// dnSpy decompiler from Assembly-CSharp.dll class: Scene_Loaded3
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scene_Loaded3 : MonoBehaviour
{
	private void Start()
	{
	}

	private void Update()
	{
	}

	public void Next_Scene()
	{
		SceneManager.LoadScene(this.Scene_Name);
	}

	public string Scene_Name;
}
