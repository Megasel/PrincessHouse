// dnSpy decompiler from Assembly-CSharp.dll class: Grid_button_Kitchen
using System;
using System.Collections;
using UnityEngine;

public class Grid_button_Kitchen : MonoBehaviour
{
	private void Start()
	{
		if (Grid_button_Kitchen._inst == null)
		{
			Grid_button_Kitchen._inst = this;
		}
	}

	private void Update()
	{
	}

	private IEnumerator shower_Btn()
	{
		yield return new WaitForSeconds(0.1f);
		this.grid_btn[0].enabled = false;
		Kitchen_Main._inst.hand_shower_g.SetActive(false);
		iTween.MoveTo(this.Bg, iTween.Hash(new object[]
		{
			"x",
			-5.56f,
			"y",
			-4.6f,
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
		yield return new WaitForSeconds(1.5f);
		this.Hand_Window.SetActive(true);
		iTween.MoveTo(this.t_shower, iTween.Hash(new object[]
		{
			"x",
			5.44f,
			"y",
			2.11f,
			"time",
			1.5,
			"eastype",
			iTween.EaseType.linear,
			"islocal",
			true
		}));
		SoundManager.Instance.v_o_shower();
		yield break;
	}

	private IEnumerator Dust_Remover_Btn()
	{
		yield return new WaitForSeconds(0.1f);
		this.grid_btn[1].enabled = false;
		Kitchen_Main._inst.hand_dust_remover_g.SetActive(false);
		Kitchen_Main._inst.hand_green_table.SetActive(true);
		Kitchen_Main._inst.green_mud_sm.SetActive(true);
		iTween.MoveTo(this.Bg, iTween.Hash(new object[]
		{
			"x",
			3.6f,
			"y",
			-1.96f,
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
		yield return new WaitForSeconds(1.5f);
		iTween.MoveTo(this.dust_Remover, iTween.Hash(new object[]
		{
			"x",
			-1.51f,
			"y",
			1.6f,
			"time",
			1.5,
			"eastype",
			iTween.EaseType.linear,
			"islocal",
			true
		}));
		Task_Bar._inst.bar_dust_1.SetActive(true);
		SoundManager.Instance.v_o_boom();
		yield break;
	}

	private IEnumerator Mud_Remover_carpet()
	{
		yield return new WaitForSeconds(0.1f);
		this.grid_btn[2].enabled = false;
		Kitchen_Main._inst.hand_mud_carpet_g.SetActive(false);
		iTween.MoveTo(this.Bg, iTween.Hash(new object[]
		{
			"x",
			-2.55f,
			"y",
			1.9f,
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
			1.827f,
			"y",
			1.827f,
			"time",
			1.5,
			"eastype",
			iTween.EaseType.linear,
			"islocal",
			true
		}));
		yield return new WaitForSeconds(1.5f);
		Kitchen_Main._inst.carpet_mud_sm.SetActive(true);
		Kitchen_Main._inst.hand_mud_carpet.SetActive(true);
		iTween.MoveTo(this.dust_Remover_carpet, iTween.Hash(new object[]
		{
			"x",
			3.7f,
			"y",
			-0.67f,
			"time",
			1.5,
			"eastype",
			iTween.EaseType.linear,
			"islocal",
			true
		}));
		Task_Bar._inst.bar_floor_dust.SetActive(true);
		SoundManager.Instance.v_o_dust_remover();
		yield break;
	}

	private IEnumerator Spider_Boom()
	{
		yield return new WaitForSeconds(0.1f);
		this.grid_btn[3].enabled = false;
		Kitchen_Main._inst.hand_spider_g.SetActive(false);
		iTween.MoveTo(this.Bg, iTween.Hash(new object[]
		{
			"x",
			-5.59f,
			"y",
			-2.98f,
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
		iTween.MoveTo(this.spider_remover, iTween.Hash(new object[]
		{
			"x",
			1.98f,
			"y",
			1.61f,
			"time",
			1.5,
			"eastype",
			iTween.EaseType.linear,
			"islocal",
			true
		}));
		yield return new WaitForSeconds(1.5f);
		Kitchen_Main._inst.hand_spider.SetActive(true);
		Kitchen_Main._inst.spider_sm.SetActive(true);
		Task_Bar._inst.bar_spider.SetActive(true);
		SoundManager.Instance.v_o_spider();
		yield break;
	}

	private IEnumerator Water_Viper()
	{
		yield return new WaitForSeconds(0.1f);
		this.grid_btn[4].enabled = false;
		Kitchen_Main._inst.hand_water_g.SetActive(false);
		iTween.MoveTo(this.Bg, iTween.Hash(new object[]
		{
			"x",
			-3.85f,
			"y",
			2.85f,
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
			1.827f,
			"y",
			1.827f,
			"time",
			1.5,
			"eastype",
			iTween.EaseType.linear,
			"islocal",
			true
		}));
		iTween.MoveTo(this.water_viper, iTween.Hash(new object[]
		{
			"x",
			4.16f,
			"y",
			-0.67f,
			"time",
			1.5,
			"eastype",
			iTween.EaseType.linear,
			"islocal",
			true
		}));
		iTween.MoveTo(Kitchen_Main._inst.Grid_2, iTween.Hash(new object[]
		{
			"x",
			-10f,
			"y",
			0f,
			"time",
			1.0,
			"eastype",
			iTween.EaseType.linear,
			"islocal",
			true
		}));
		yield return new WaitForSeconds(1.5f);
		Kitchen_Main._inst.hand_pink_water.SetActive(true);
		Kitchen_Main._inst.water_pink_sm.SetActive(true);
		Task_Bar._inst.bar_pink_water.SetActive(true);
		SoundManager.Instance.v_o_viper();
		yield break;
	}

	public static Grid_button_Kitchen _inst;

	public GameObject t_shower;

	public GameObject t_shower_cleaner;

	public GameObject Hand_Window;

	public GameObject Bg;

	public GameObject dust_Remover;

	public GameObject dust_Remover_carpet;

	public GameObject water_viper;

	public GameObject spider_remover;

	public tk2dButton[] grid_btn;
}
