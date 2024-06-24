// dnSpy decompiler from Assembly-CSharp.dll class: Fridge_Mini_Game_Main
using System;
using UnityEngine;

public class Fridge_Mini_Game_Main : MonoBehaviour
{
	private void Start()
	{
		if (Fridge_Mini_Game_Main._inst == null)
		{
			Fridge_Mini_Game_Main._inst = this;
		}
		GameManager.Instance.count = 0;
	}

	private void Update()
	{
	}

	public GameObject mud_hand;

	public GameObject mud;

	public GameObject snow_hand_1;

	public GameObject snow_hand_2;

	public GameObject snow_hand_3;

	public GameObject snow_hand_4;

	public GameObject hand_water_1;

	public GameObject hand_water_2;

	public GameObject Sprite_mask_Snow;

	public GameObject water_1;

	public GameObject water_2;

	public GameObject Sprite_mask_water_1;

	public GameObject Sprite_mask_water_2;

	public GameObject spounge_tool;

	public GameObject blowr_tool;

	public GameObject cloth_tool;

	public static Fridge_Mini_Game_Main _inst;

	public GameObject trey_items;

	public ParticleSystem smoke_p;

	public ParticleSystem water_shower;

	public AudioSource blower_s;
}
