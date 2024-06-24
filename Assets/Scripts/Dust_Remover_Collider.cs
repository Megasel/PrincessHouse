// dnSpy decompiler from Assembly-CSharp.dll class: Dust_Remover_Collider
using System;
using System.Collections;
using UnityEngine;

public class Dust_Remover_Collider : MonoBehaviour
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
		if (base.gameObject.name == "dust_remover_coll" && col.gameObject.tag == "mud_mask_green")
		{
			col.gameObject.GetComponent<SpriteMask>().enabled = true;
			col.gameObject.GetComponent<BoxCollider>().enabled = false;
			this.count++;
			this.fill += 0.083f;
			iTween.ScaleTo(Task_Bar._inst.bar_dust_1_f, iTween.Hash(new object[]
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
			if (this.count == 12)
			{
				this.count = 0;
				Task_Bar._inst.bar_dust_1.SetActive(false);
				Room_Cleaning_Main._inst.hand_green_table.SetActive(false);
				Room_Cleaning_Main._inst.hand_purple_table.SetActive(true);
				UnityEngine.Object.Destroy(this.drag_tool.GetComponent<Drag_Tool>());
				iTween.MoveTo(this.drag_tool, iTween.Hash(new object[]
				{
					"x",
					-0.84f,
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
					0.95f,
					"y",
					1.54f,
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
				Room_Cleaning_Main._inst.purple_mud_sm.SetActive(true);
				Task_Bar._inst.bar_dust_2.SetActive(true);
				this.fill = 0f;
			}
		}
		if (base.gameObject.name == "dust_remover_coll" && col.gameObject.tag == "mud_mask_purple")
		{
			col.gameObject.GetComponent<SpriteMask>().enabled = true;
			col.gameObject.GetComponent<BoxCollider>().enabled = false;
			this.count++;
			this.fill += 0.0415f;
			iTween.ScaleTo(Task_Bar._inst.bar_dust_2_f, iTween.Hash(new object[]
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
			if (this.count == 21)
			{
				this.count = 0;
				Task_Bar._inst.bar_dust_2.SetActive(false);
				this.fill = 0f;
				Room_Cleaning_Main._inst.hand_purple_table.SetActive(false);
				Room_Cleaning_Main._inst.hand_pink_table.SetActive(true);
				iTween.MoveTo(this.drag_tool, iTween.Hash(new object[]
				{
					"x",
					3.95f,
					"y",
					-0.96f,
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
					-5.1f,
					"y",
					1.54f,
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
				Room_Cleaning_Main._inst.pink_mud_sm.SetActive(true);
				Task_Bar._inst.bar_dust_3.SetActive(true);
			}
		}
		if (base.gameObject.name == "dust_remover_coll" && col.gameObject.tag == "mud_mask_pink")
		{
			col.gameObject.GetComponent<SpriteMask>().enabled = true;
			col.gameObject.GetComponent<BoxCollider>().enabled = false;
			this.count++;
			this.fill += 0.16f;
			iTween.ScaleTo(Task_Bar._inst.bar_dust_3_f, iTween.Hash(new object[]
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
			if (this.count == 6)
			{
				this.count = 0;
				if (base.GetComponent<AudioSource>().isPlaying)
				{
					base.GetComponent<AudioSource>().Stop();
				}
				Task_Bar._inst.bar_dust_3.SetActive(false);
				Room_Cleaning_Main._inst.hand_pink_table.SetActive(false);
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
				iTween.MoveTo(Room_Cleaning_Main._inst.Grid_1, iTween.Hash(new object[]
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
				iTween.MoveTo(Room_Cleaning_Main._inst.Grid_2, iTween.Hash(new object[]
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
				yield return new WaitForSeconds(1f);
				Room_Cleaning_Main._inst.hand_mud_carpet_g.SetActive(true);
				Grid_button._inst.grid_btn[2].enabled = true;
				Room_Cleaning_Main._inst.Ads.SetActive(true);
				yield return new WaitForSeconds(0.1f);
				Room_Cleaning_Main._inst.Ads.SetActive(false);
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
