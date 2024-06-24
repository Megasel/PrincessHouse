// dnSpy decompiler from Assembly-CSharp.dll class: Blower_Coll
using System;
using System.Collections;
using UnityEngine;

public class Blower_Coll : MonoBehaviour
{
	private void Start()
	{
	}

	private void Update()
	{
	}

	private IEnumerator OnTriggerEnter(Collider col)
	{
		yield return new WaitForSeconds(0.01f);
		if (base.gameObject.name == "snow remove_coll")
		{
			if (col.gameObject.tag == "snow_1")
			{
				if (!base.GetComponent<AudioSource>().isPlaying)
				{
					base.GetComponent<AudioSource>().Play();
				}
				col.gameObject.GetComponent<SpriteMask>().enabled = true;
				col.gameObject.GetComponent<BoxCollider>().enabled = false;
				Fridge_Mini_Game_Main._inst.water_shower.Play();
				this.count1++;
				this.count_main++;
				if (this.count1 == 9)
				{
					if (base.GetComponent<AudioSource>().isPlaying)
					{
						base.GetComponent<AudioSource>().Stop();
					}
					Fridge_Mini_Game_Main._inst.snow_hand_1.SetActive(false);
				}
			}
			if (col.gameObject.tag == "snow_2")
			{
				if (!base.GetComponent<AudioSource>().isPlaying)
				{
					base.GetComponent<AudioSource>().Play();
				}
				col.gameObject.GetComponent<SpriteMask>().enabled = true;
				col.gameObject.GetComponent<BoxCollider>().enabled = false;
				Fridge_Mini_Game_Main._inst.water_shower.Play();
				this.count2++;
				this.count_main++;
				if (this.count2 == 12)
				{
					if (base.GetComponent<AudioSource>().isPlaying)
					{
						base.GetComponent<AudioSource>().Stop();
					}
					Fridge_Mini_Game_Main._inst.snow_hand_2.SetActive(false);
					Fridge_Mini_Game_Main._inst.water_1.SetActive(true);
				}
			}
			if (col.gameObject.tag == "snow_3")
			{
				if (!base.GetComponent<AudioSource>().isPlaying)
				{
					base.GetComponent<AudioSource>().Play();
				}
				col.gameObject.GetComponent<SpriteMask>().enabled = true;
				col.gameObject.GetComponent<BoxCollider>().enabled = false;
				Fridge_Mini_Game_Main._inst.water_shower.Play();
				this.count3++;
				this.count_main++;
				if (this.count3 == 7)
				{
					if (base.GetComponent<AudioSource>().isPlaying)
					{
						base.GetComponent<AudioSource>().Stop();
					}
					Fridge_Mini_Game_Main._inst.snow_hand_3.SetActive(false);
				}
			}
			if (col.gameObject.tag == "snow_4")
			{
				if (!base.GetComponent<AudioSource>().isPlaying)
				{
					base.GetComponent<AudioSource>().Play();
				}
				col.gameObject.GetComponent<SpriteMask>().enabled = true;
				col.gameObject.GetComponent<BoxCollider>().enabled = false;
				Fridge_Mini_Game_Main._inst.water_shower.Play();
				this.count4++;
				this.count_main++;
				if (this.count4 == 10)
				{
					if (base.GetComponent<AudioSource>().isPlaying)
					{
						base.GetComponent<AudioSource>().Stop();
					}
					Fridge_Mini_Game_Main._inst.snow_hand_4.SetActive(false);
					Fridge_Mini_Game_Main._inst.water_2.SetActive(true);
				}
			}
			if (this.count_main == 35)
			{
				UnityEngine.Object.Destroy(this.drag_tool.GetComponent<Drag_Mini_Game_Fridge>());
				iTween.MoveTo(this.drag_tool, iTween.Hash(new object[]
				{
					"x",
					14.44f,
					"y",
					2.11f,
					"time",
					1.0,
					"eastype",
					iTween.EaseType.linear,
					"islocal",
					true
				}));
				Fridge_Mini_Game_Main._inst.water_shower.Stop();
				Fridge_Mini_Game_Main._inst.water_2.SetActive(true);
				Fridge_Mini_Game_Main._inst.Sprite_mask_Snow.SetActive(false);
				Fridge_Mini_Game_Main._inst.snow_hand_1.SetActive(false);
				Fridge_Mini_Game_Main._inst.snow_hand_2.SetActive(false);
				Fridge_Mini_Game_Main._inst.snow_hand_3.SetActive(false);
				Fridge_Mini_Game_Main._inst.snow_hand_4.SetActive(false);
				iTween.MoveTo(Fridge_Mini_Game_Main._inst.cloth_tool, iTween.Hash(new object[]
				{
					"x",
					4.86f,
					"y",
					-0.74f,
					"time",
					1.5,
					"eastype",
					iTween.EaseType.linear,
					"islocal",
					true
				}));
				yield return new WaitForSeconds(1.01f);
				Fridge_Mini_Game_Main._inst.Sprite_mask_water_1.SetActive(true);
				Fridge_Mini_Game_Main._inst.Sprite_mask_water_2.SetActive(true);
				Fridge_Mini_Game_Main._inst.hand_water_1.SetActive(true);
				Fridge_Mini_Game_Main._inst.hand_water_2.SetActive(true);
				yield return new WaitForSeconds(1.01f);
				Fridge_Mini_Game_Main._inst.water_1.GetComponent<SpriteRenderer>().maskInteraction = SpriteMaskInteraction.VisibleOutsideMask;
				Fridge_Mini_Game_Main._inst.water_2.GetComponent<SpriteRenderer>().maskInteraction = SpriteMaskInteraction.VisibleOutsideMask;
			}
		}
		yield break;
	}

	private int count1;

	private int count2;

	private int count3;

	private int count4;

	private int count_main;

	public GameObject drag_tool;
}
