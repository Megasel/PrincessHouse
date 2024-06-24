// dnSpy decompiler from Assembly-CSharp.dll class: Task_Bar
using System;
using UnityEngine;

public class Task_Bar : MonoBehaviour
{
	private void Start()
	{
		if (Task_Bar._inst == null)
		{
			Task_Bar._inst = this;
		}
	}

	private void Update()
	{
	}

	public static Task_Bar _inst;

	public GameObject bar_shelf_cleaner;

	public GameObject bar_dust_1;

	public GameObject bar_dust_2;

	public GameObject bar_dust_3;

	public GameObject bar_floor_dust;

	public GameObject bar_pink_water;

	public GameObject bar_blue_water;

	public GameObject bar_spider;

	public GameObject bar_paint_1;

	public GameObject bar_paint_2;

	public GameObject bar_shelf_cleaner_f;

	public GameObject bar_dust_1_f;

	public GameObject bar_dust_2_f;

	public GameObject bar_dust_3_f;

	public GameObject bar_floor_dust_f;

	public GameObject bar_pink_water_f;

	public GameObject bar_blue_water_f;

	public GameObject bar_spider_f;

	public GameObject bar_paint_1_f;

	public GameObject bar_paint_2_f;
}
