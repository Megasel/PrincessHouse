// dnSpy decompiler from Assembly-CSharp.dll class: Scene_Loaded
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scene_Loaded : MonoBehaviour
{
	private void Start()
	{
	}

	private void Update()
	{
	}

	public void Next_Scene()
	{
		GameManager.Instance.count = 0;
		GameManager.Instance.black_count = 0;
		GameManager.Instance.clr_count = 0;
		GameManager.Instance.cloth = 0;
		GameManager.Instance.Count_Mini_fridge = 0;
		SceneManager.LoadScene(this.Scene_Name);
	}

	public string Scene_Name;
}
