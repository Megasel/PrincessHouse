// dnSpy decompiler from Assembly-CSharp.dll class: Viper_coll
using System;
using System.Collections;
using UnityEngine;

public class Viper_coll : MonoBehaviour
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
				Room_Cleaning_Main._inst.hand_pink_water.SetActive(false);
				Room_Cleaning_Main._inst.hand_blue_water.SetActive(true);
				UnityEngine.Object.Destroy(this.drag_tool.GetComponent<Drag_Tool>());
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
				iTween.MoveTo(Room_Cleaning_Main._inst.Bg, iTween.Hash(new object[]
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
				iTween.ScaleTo(Room_Cleaning_Main._inst.Bg, iTween.Hash(new object[]
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
				this.drag_tool.AddComponent<Drag_Tool>();
				Room_Cleaning_Main._inst.water_blue_sm.SetActive(true);
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
			if (!base.GetComponent<AudioSource>().isPlaying)
			{
				base.GetComponent<AudioSource>().Play();
			}
			if (this.count == 13)
			{
				if (base.GetComponent<AudioSource>().isPlaying)
				{
					base.GetComponent<AudioSource>().Stop();
				}
				Task_Bar._inst.bar_blue_water.SetActive(false);
				this.count = 0;
				Room_Cleaning_Main._inst.hand_blue_water.SetActive(false);
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
				iTween.MoveTo(Room_Cleaning_Main._inst.Bg, iTween.Hash(new object[]
				{
					"x",
					0f,
					"y",
					0f,
					"time",
					1.0,
					"eastype",
					iTween.EaseType.linear,
					"islocal",
					true
				}));
				iTween.ScaleTo(Room_Cleaning_Main._inst.Bg, iTween.Hash(new object[]
				{
					"x",
					1f,
					"y",
					1f,
					"time",
					1.0,
					"eastype",
					iTween.EaseType.linear,
					"islocal",
					true
				}));
				yield return new WaitForSeconds(1f);
				iTween.MoveTo(GameObject.Find("Next_Btn_All_Items"), iTween.Hash(new object[]
				{
					"x",
					0f,
					"y",
					0f,
					"time",
					1.0,
					"eastype",
					iTween.EaseType.linear,
					"islocal",
					true
				}));
			}
		}
		yield break;
	}

	public GameObject drag_tool;

	private int count;

	private int count1;

	private int count2;

	private int count3;

	private float fill;
}
