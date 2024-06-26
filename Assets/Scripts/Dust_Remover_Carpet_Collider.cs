// dnSpy decompiler from Assembly-CSharp.dll class: Dust_Remover_Carpet_Collider
using System;
using System.Collections;
using UnityEngine;

public class Dust_Remover_Carpet_Collider : MonoBehaviour
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
		if (base.gameObject.name == "dust_remover_carpet_coll" && col.gameObject.tag == "carpet_mud")
		{
			col.gameObject.GetComponent<SpriteMask>().enabled = true;
			col.gameObject.GetComponent<BoxCollider>().enabled = false;
			this.count++;
			this.fill += 0.041f;
			iTween.ScaleTo(Task_Bar._inst.bar_floor_dust_f, iTween.Hash(new object[]
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
			if (this.count == 24)
			{
				if (base.GetComponent<AudioSource>().isPlaying)
				{
					base.GetComponent<AudioSource>().Stop();
				}
				this.count = 0;
				Task_Bar._inst.bar_floor_dust.SetActive(false);
				Room_Cleaning_Main._inst.hand_mud_carpet.SetActive(false);
				UnityEngine.Object.Destroy(this.drag_tool.GetComponent<Drag_Tool>());
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
				Room_Cleaning_Main._inst.hand_spider_g.SetActive(true);
				Grid_button._inst.grid_btn[3].enabled = true;
			}
		}
		yield break;
	}

	public GameObject drag_tool;

	private int count;

	private float fill;
}
