// dnSpy decompiler from Assembly-CSharp.dll class: Viper_coll_Kitchen
using System;
using System.Collections;
using UnityEngine;

public class Viper_coll_Kitchen : MonoBehaviour
{
	private void Start()
	{
	}

	private void Update()
	{
	}

	private IEnumerator OnTriggerEnter(Collider col)
	{
		yield return new WaitForSeconds(0.0001f);
		if (base.gameObject.name == "water_viper_coll" && col.gameObject.tag == "water_pink")
		{
			col.gameObject.GetComponent<SpriteMask>().enabled = true;
			col.gameObject.GetComponent<BoxCollider>().enabled = false;
			this.count++;
			this.fill += 0.09f;
			iTween.ScaleTo(Task_Bar._inst.bar_pink_water_f, iTween.Hash(new object[]
			{
				"x",
				this.fill,
				"time",
				0.3,
				"eastype",
				iTween.EaseType.linear,
				"islocal",
				true
			}));
			if (!base.GetComponent<AudioSource>().isPlaying)
			{
				base.GetComponent<AudioSource>().Play();
			}
			if (this.count == 11)
			{
				Task_Bar._inst.bar_pink_water.SetActive(false);
				if (base.GetComponent<AudioSource>().isPlaying)
				{
					base.GetComponent<AudioSource>().Stop();
				}
				this.fill = 0f;
				this.count = 0;
				Kitchen_Main._inst.hand_pink_water.SetActive(false);
				Kitchen_Main._inst.hand_blue_water.SetActive(true);
				UnityEngine.Object.Destroy(this.drag_tool.GetComponent<Drag_Tool_Kitchen>());
				iTween.MoveTo(this.drag_tool, iTween.Hash(new object[]
				{
					"x",
					-0.66f,
					"y",
					-1.28f,
					"time",
					1.0,
					"eastype",
					iTween.EaseType.linear,
					"islocal",
					true
				}));
				iTween.MoveTo(Kitchen_Main._inst.Bg, iTween.Hash(new object[]
				{
					"x",
					3.79f,
					"y",
					2.5f,
					"time",
					1.0,
					"eastype",
					iTween.EaseType.linear,
					"islocal",
					true
				}));
				iTween.ScaleTo(Kitchen_Main._inst.Bg, iTween.Hash(new object[]
				{
					"x",
					1.83f,
					"y",
					1.83f,
					"time",
					1.0,
					"eastype",
					iTween.EaseType.linear,
					"islocal",
					true
				}));
				yield return new WaitForSeconds(1f);
				this.drag_tool.AddComponent<Drag_Tool_Kitchen>();
				Kitchen_Main._inst.water_blue_sm.SetActive(true);
				Task_Bar._inst.bar_blue_water.SetActive(true);
			}
		}
		if (base.gameObject.name == "water_viper_coll" && col.gameObject.tag == "water_blue")
		{
			col.gameObject.GetComponent<SpriteMask>().enabled = true;
			col.gameObject.GetComponent<BoxCollider>().enabled = false;
			this.count++;
			this.fill += 0.076f;
			iTween.ScaleTo(Task_Bar._inst.bar_blue_water_f, iTween.Hash(new object[]
			{
				"x",
				this.fill,
				"time",
				0.3,
				"eastype",
				iTween.EaseType.linear,
				"islocal",
				true
			}));
			if (this.count == 13)
			{
				Task_Bar._inst.bar_blue_water.SetActive(false);
				this.count = 0;
				Kitchen_Main._inst.hand_blue_water.SetActive(false);
				UnityEngine.Object.Destroy(this.drag_tool.GetComponent<Drag_Tool_Kitchen>());
				iTween.MoveTo(this.drag_tool, iTween.Hash(new object[]
				{
					"x",
					10.568161f,
					"y",
					-0.42f,
					"time",
					1.0,
					"eastype",
					iTween.EaseType.linear,
					"islocal",
					true
				}));
				iTween.MoveTo(Kitchen_Main._inst.Bg, iTween.Hash(new object[]
				{
					"x",
					-5f,
					"y",
					-5f,
					"time",
					1.0,
					"eastype",
					iTween.EaseType.linear,
					"islocal",
					true
				}));
				iTween.ScaleTo(Kitchen_Main._inst.Bg, iTween.Hash(new object[]
				{
					"x",
					2f,
					"y",
					2f,
					"time",
					1.0,
					"eastype",
					iTween.EaseType.linear,
					"islocal",
					true
				}));
				yield return new WaitForSeconds(1f);
				iTween.MoveTo(this.Paint_brush, iTween.Hash(new object[]
				{
					"x",
					4.51f,
					"y",
					2.11f,
					"time",
					1.0,
					"eastype",
					iTween.EaseType.linear,
					"islocal",
					true
				}));
				this.paint_1_sm.SetActive(true);
				this.paint_2_sm.SetActive(true);
				this.hand_Paint_1.SetActive(true);
				Task_Bar._inst.bar_paint_1.SetActive(true);
			}
		}
		yield break;
	}

	public GameObject drag_tool;

	public GameObject Paint_brush;

	public GameObject paint_1_sm;

	public GameObject paint_2_sm;

	public GameObject hand_Paint_1;

	public GameObject hand_Paint_2;

	private int count;

	private int count1;

	private int count2;

	private int count3;

	private float fill;
}
