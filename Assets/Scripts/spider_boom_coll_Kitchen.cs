// dnSpy decompiler from Assembly-CSharp.dll class: spider_boom_coll_Kitchen
using System;
using System.Collections;
using UnityEngine;

public class spider_boom_coll_Kitchen : MonoBehaviour
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
		if (base.gameObject.name == "spider_boom_coll" && col.gameObject.tag == "spider")
		{
			col.gameObject.GetComponent<SpriteMask>().enabled = true;
			col.gameObject.GetComponent<BoxCollider>().enabled = false;
			this.count++;
			this.fill += 0.09f;
			if (!base.GetComponent<AudioSource>().isPlaying)
			{
				base.GetComponent<AudioSource>().Play();
			}
			iTween.ScaleTo(Task_Bar._inst.bar_spider_f, iTween.Hash(new object[]
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
			if (this.count == 11)
			{
				this.count = 0;
				if (base.GetComponent<AudioSource>().isPlaying)
				{
					base.GetComponent<AudioSource>().Stop();
				}
				Task_Bar._inst.bar_spider.SetActive(false);
				Kitchen_Main._inst.hand_spider.SetActive(false);
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
				Kitchen_Main._inst.hand_water_g.SetActive(true);
				Grid_button_Kitchen._inst.grid_btn[4].enabled = true;
			}
		}
		yield break;
	}

	public GameObject drag_tool;

	private int count;

	private float fill;
}
