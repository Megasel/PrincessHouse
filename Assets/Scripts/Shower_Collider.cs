// dnSpy decompiler from Assembly-CSharp.dll class: Shower_Collider
using System;
using System.Collections;
using UnityEngine;

public class Shower_Collider : MonoBehaviour
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
		if (base.gameObject.name == "shower_coll" && col.gameObject.tag == "mirror_bubble")
		{
			col.gameObject.GetComponent<SpriteRenderer>().enabled = true;
			col.gameObject.GetComponent<BoxCollider>().enabled = false;
			this.count++;
			if (!base.GetComponent<AudioSource>().isPlaying)
			{
				base.GetComponent<AudioSource>().Play();
			}
			if (this.count == 6)
			{
				if (base.GetComponent<AudioSource>().isPlaying)
				{
					base.GetComponent<AudioSource>().Stop();
				}
				UnityEngine.Object.Destroy(this.drag_tool.GetComponent<Drag_Tool>());
				iTween.MoveTo(this.drag_tool, iTween.Hash(new object[]
				{
					"x",
					9.44f,
					"y",
					2.11f,
					"time",
					1.0,
					"eastype",
					iTween.EaseType.linear,
					"islocal",
					true
				}));
				Room_Cleaning_Main._inst.hand_window.SetActive(false);
				this.shower_part.Stop();
				this.count = 0;
				yield return new WaitForSeconds(1.0001f);
				iTween.MoveTo(this.shef_cleaner, iTween.Hash(new object[]
				{
					"x",
					5.44f,
					"y",
					2.11f,
					"time",
					2.5,
					"eastype",
					iTween.EaseType.linear,
					"islocal",
					true
				}));
				this.Sprite_Mask.SetActive(true);
				Room_Cleaning_Main._inst.hand_window.SetActive(true);
				Task_Bar._inst.bar_shelf_cleaner.SetActive(true);
			}
		}
		if (base.gameObject.name == "shelf_cleaner_coll" && col.gameObject.tag == "mask_bubble")
		{
			col.gameObject.GetComponent<SpriteMask>().enabled = true;
			col.gameObject.GetComponent<BoxCollider>().enabled = false;
			this.count1++;
			this.fill += 0.083f;
			iTween.ScaleTo(Task_Bar._inst.bar_shelf_cleaner_f, iTween.Hash(new object[]
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
			if (this.count1 == 12)
			{
				if (base.GetComponent<AudioSource>().isPlaying)
				{
					base.GetComponent<AudioSource>().Stop();
				}
				Task_Bar._inst.bar_shelf_cleaner.SetActive(false);
				UnityEngine.Object.Destroy(this.drag_tool.GetComponent<Drag_Tool>());
				iTween.MoveTo(this.drag_tool, iTween.Hash(new object[]
				{
					"x",
					9.44f,
					"y",
					2.11f,
					"time",
					1.0,
					"eastype",
					iTween.EaseType.linear,
					"islocal",
					true
				}));
				Room_Cleaning_Main._inst.hand_window.SetActive(false);
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
				Room_Cleaning_Main._inst.hand_dust_remover_g.SetActive(true);
				Grid_button._inst.grid_btn[1].enabled = true;
			}
		}
		yield break;
	}

	public GameObject drag_tool;

	public GameObject col_game_object;

	public GameObject shef_cleaner;

	public GameObject Sprite_Mask;

	public ParticleSystem shower_part;

	private int count;

	private int count1;

	private int count2;

	private int count3;

	private float fill;
}
