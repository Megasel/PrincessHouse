// dnSpy decompiler from Assembly-CSharp.dll class: Garden_Main
using System;
using System.Collections;
using UnityEngine;

public class Garden_Main : MonoBehaviour
{
	private void Start()
	{
		if (Garden_Main._inst == null)
		{
			Garden_Main._inst = this;
		}
		GameManager.Instance.count = 0;
	}

	private void Update()
	{
	}

	private IEnumerator Garden_Flower_Area_Btn()
	{
		SoundManager.Instance.Click_s();
		yield return new WaitForSeconds(0.1f);
		this.hand_Flower_Area.SetActive(false);
		this.Trolly.SetActive(false);
		this.mud_items.SetActive(false);
		iTween.MoveTo(this.Bg, iTween.Hash(new object[]
		{
			"x",
			-2.7f,
			"y",
			-0.58f,
			"time",
			1.5,
			"eastype",
			iTween.EaseType.linear,
			"islocal",
			true
		}));
		iTween.ScaleTo(this.Bg, iTween.Hash(new object[]
		{
			"x",
			2f,
			"y",
			2f,
			"time",
			1.5,
			"eastype",
			iTween.EaseType.linear,
			"islocal",
			true
		}));
		iTween.MoveTo(this.Flower_Basket, iTween.Hash(new object[]
		{
			"x",
			2.7f,
			"y",
			0.79f,
			"time",
			1.5,
			"eastype",
			iTween.EaseType.linear,
			"islocal",
			true
		}));
		yield return new WaitForSeconds(1.5f);
		this.loading_Bar_Flower.SetActive(true);
		this.garden_btn.SetActive(false);
		yield break;
	}

	public static Garden_Main _inst;

	public GameObject hand_window;

	public GameObject hand_green_table;

	public GameObject hand_purple_table;

	public GameObject hand_pink_table;

	public GameObject hand_shower_g;

	public GameObject hand_dust_remover_g;

	public GameObject hand_mud_carpet_g;

	public GameObject hand_mud_carpet;

	public GameObject hand_spider_g;

	public GameObject hand_spider;

	public GameObject hand_water_g;

	public GameObject hand_blue_water;

	public GameObject hand_pink_water;

	public GameObject green_mud_sm;

	public GameObject purple_mud_sm;

	public GameObject pink_mud_sm;

	public GameObject carpet_mud_sm;

	public GameObject water_blue_sm;

	public GameObject water_pink_sm;

	public GameObject spider_sm;

	public GameObject Grid_1;

	public GameObject Grid_2;

	public GameObject Bg;

	public GameObject garden_btn;

	public GameObject Flower_Basket;

	public GameObject Trolly;

	public GameObject mud_items;

	public GameObject loading_Bar_Flower;

	public GameObject hand_Flower_Area;

	public GameObject Ads;
}
