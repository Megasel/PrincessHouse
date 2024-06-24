// dnSpy decompiler from Assembly-CSharp.dll class: WashRoom_Main
using System;
using System.Collections;
using UnityEngine;

public class WashRoom_Main : MonoBehaviour
{
	private void Start()
	{
		GameManager.Instance.count = 0;
		if (WashRoom_Main._inst == null)
		{
			WashRoom_Main._inst = this;
		}
		base.StartCoroutine(this.Start_Action());
	}

	private void Update()
	{
	}

	private IEnumerator Start_Action()
	{
		yield return new WaitForSeconds(0.1f);
		yield break;
	}

	public static WashRoom_Main _inst;

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
