// dnSpy decompiler from Assembly-CSharp.dll class: Kitchen_Main
using System;
using UnityEngine;

public class Kitchen_Main : MonoBehaviour
{
	private void Start()
	{
		if (Kitchen_Main._inst == null)
		{
			Kitchen_Main._inst = this;
		}
		GameManager.Instance.count = 0;
	}

	private void Update()
	{
	}

	public static Kitchen_Main _inst;

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

	public GameObject Ads;
}
