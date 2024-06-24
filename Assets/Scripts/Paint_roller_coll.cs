// dnSpy decompiler from Assembly-CSharp.dll class: Paint_roller_coll
using System;
using System.Collections;
using UnityEngine;

public class Paint_roller_coll : MonoBehaviour
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
		if (base.gameObject.name == "paint_rooler_coll" && col.gameObject.tag == "paint_1")
		{
			col.gameObject.GetComponent<SpriteMask>().enabled = true;
			col.gameObject.GetComponent<BoxCollider>().enabled = false;
			this.count++;
			this.fill += 0.25f;
			iTween.ScaleTo(Task_Bar._inst.bar_paint_1_f, iTween.Hash(new object[]
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
			if (this.count == 4)
			{
				this.count = 0;
				Task_Bar._inst.bar_paint_1.SetActive(false);
				this.fill = 0f;
				this.paint_1_hand.SetActive(false);
				UnityEngine.Object.Destroy(this.drag_tool.GetComponent<Drag_Tool_Kitchen>());
				iTween.MoveTo(this.drag_tool, iTween.Hash(new object[]
				{
					"x",
					-0.66f,
					"y",
					0.88f,
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
					-1.53f,
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
				if (base.GetComponent<AudioSource>().isPlaying)
				{
					base.GetComponent<AudioSource>().Stop();
				}
				yield return new WaitForSeconds(1f);
				this.drag_tool.AddComponent<Drag_Tool_Kitchen>();
				this.paint_2_hand.SetActive(true);
				Task_Bar._inst.bar_paint_2.SetActive(true);
			}
		}
		if (base.gameObject.name == "paint_rooler_coll" && col.gameObject.tag == "paint_2")
		{
			col.gameObject.GetComponent<SpriteMask>().enabled = true;
			col.gameObject.GetComponent<BoxCollider>().enabled = false;
			this.count++;
			this.fill += 0.25f;
			if (!base.GetComponent<AudioSource>().isPlaying)
			{
				base.GetComponent<AudioSource>().Play();
			}
			iTween.ScaleTo(Task_Bar._inst.bar_paint_2_f, iTween.Hash(new object[]
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
			if (this.count == 4)
			{
				if (base.GetComponent<AudioSource>().isPlaying)
				{
					base.GetComponent<AudioSource>().Stop();
				}
				this.count = 0;
				Task_Bar._inst.bar_paint_2.SetActive(false);
				this.paint_2_hand.SetActive(false);
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
				iTween.ScaleTo(Kitchen_Main._inst.Bg, iTween.Hash(new object[]
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
					1.5,
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

	public GameObject paint_1_hand;

	public GameObject paint_2_hand;

	private int count;

	private int count1;

	private int count2;

	private int count3;

	private float fill;
}
