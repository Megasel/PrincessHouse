// dnSpy decompiler from Assembly-CSharp.dll class: Selection_Scene
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Selection_Scene : MonoBehaviour
{
	private void Start()
	{
	}

	private void Update()
	{
	}

	public void Room_Scene()
	{
		SceneManager.LoadScene("Room_Cleaning_Scene");
	}

	public void Wash_Room_Scene()
	{
		SceneManager.LoadScene("Wash_Cleaning");
	}

	public void Dress_Wash_Scene()
	{
		SceneManager.LoadScene("Dress_Wash_Scene");
	}

	public void Kitchen_Scene()
	{
		SceneManager.LoadScene("Kitchen_Scene");
	}

	public void Garden_Scene()
	{
		SceneManager.LoadScene("Garden_Cleaning_Scene");
	}

	public void Fridge_Mini_Game()
	{
		SceneManager.LoadScene("Mini_Game_Fridge");
	}

	public void Plant_Mini_Game()
	{
		SceneManager.LoadScene("Plant_Mini_Game");
	}
}
