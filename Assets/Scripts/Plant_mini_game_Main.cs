// dnSpy decompiler from Assembly-CSharp.dll class: Plant_mini_game_Main
using System;
using System.Collections;
using UnityEngine;

public class Plant_mini_game_Main : MonoBehaviour
{
	private void Start()
	{
		if (Plant_mini_game_Main._inst == null)
		{
			Plant_mini_game_Main._inst = this;
		}
		GameManager.Instance.count = 0;
	}

	private void Update()
	{
	}

	private IEnumerator jar_Cap_btn()
	{
		yield return new WaitForSeconds(0.1f);
		this.Cap.GetComponent<tk2dButton>().enabled = false;
		this.Cap.GetComponent<Animator>().enabled = true;
		this.hand_jar.SetActive(false);
		iTween.ScaleTo(this.Bg, iTween.Hash(new object[]
		{
			"x",
			1.5f,
			"y",
			1.5f,
			"time",
			1.5,
			"eastype",
			iTween.EaseType.linear,
			"islocal",
			true
		}));
		iTween.MoveTo(this.Bg, iTween.Hash(new object[]
		{
			"x",
			-0.59f,
			"y",
			0.11f,
			"time",
			1.5,
			"eastype",
			iTween.EaseType.linear,
			"islocal",
			true
		}));
		SoundManager.Instance.Click_s();
		yield return new WaitForSeconds(3.1f);
		for (int i = 0; i < this.germs.Length; i++)
		{
			this.germs[i].GetComponent<BoxCollider>().enabled = true;
			this.germs[i].GetComponent<Animator>().enabled = true;
		}
		this.hand_germs_jar.SetActive(true);
		yield break;
	}

	public static Plant_mini_game_Main _inst;

	public GameObject[] Dirt_leaves;

	public GameObject[] new_leaves;

	public GameObject[] Straw_Berry;

	public GameObject[] germs;

	public GameObject[] germs_in_jar;

	public GameObject Cap;

	public GameObject hand_jar;

	public GameObject hand_germs_jar;

	public GameObject jar_all_items;

	public GameObject hands_leaves;

	public GameObject seed_tool;

	public GameObject water_tool;

	public GameObject vast_collider;

	public GameObject Strawberry_Basket;

	public GameObject hands_seed_box;

	public GameObject hands_water_box;

	public GameObject Bg;
}
