// dnSpy decompiler from Assembly-CSharp.dll class: GameManager
using System;
using UnityEngine;

public class GameManager
{
	private GameManager()
	{
	}

	public static GameManager Instance
	{
		get
		{
			if (GameManager.instance == null)
			{
				GameManager.instance = new GameManager();
			}
			return GameManager.instance;
		}
	}

	private static GameManager instance;

	public bool check;

	public int i_is_getInt;

	public bool b_tab_1;

	public bool b_tab_2;

	public bool b_tab_3;

	public bool b_tab_4;

	public bool b_tab_5;

	public bool b_gift_p_1;

	public bool b_gift_p_2;

	public bool b_gift_p_3;

	public bool b_gift_p_4;

	public bool b_gift_p_5;

	public bool b_gift_p_6;

	public bool b_gift_p_7;

	public bool b_gift_p_8;

	public bool b_gift_p_9;

	public Vector3 is_old_position;

	public bool b_is_tf;

	public float f_is_scaling;

	public int count;

	public int black_count;

	public int clr_count;

	public int cloth;

	public int Count_Mini_fridge;
}
