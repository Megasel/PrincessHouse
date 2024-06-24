// dnSpy decompiler from Assembly-CSharp.dll class: cloth_coll
using System;
using System.Collections;
using UnityEngine;

public class cloth_coll : MonoBehaviour
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
		if (base.gameObject.name == "cloth_coll")
		{
			if (col.gameObject.tag == "water_1")
			{
				col.gameObject.GetComponent<SpriteMask>().enabled = true;
				col.gameObject.GetComponent<BoxCollider>().enabled = false;
				this.count1++;
				this.count_main++;
				if (!base.GetComponent<AudioSource>().isPlaying)
				{
					base.GetComponent<AudioSource>().Play();
				}
				if (this.count1 == 6)
				{
					Fridge_Mini_Game_Main._inst.hand_water_1.SetActive(false);
					Fridge_Mini_Game_Main._inst.water_1.SetActive(false);
					if (base.GetComponent<AudioSource>().isPlaying)
					{
						base.GetComponent<AudioSource>().Stop();
					}
					SoundManager.Instance.Celebration_s();
				}
			}
			if (col.gameObject.tag == "water_2")
			{
				col.gameObject.GetComponent<SpriteMask>().enabled = true;
				col.gameObject.GetComponent<BoxCollider>().enabled = false;
				this.count2++;
				this.count_main++;
				if (!base.GetComponent<AudioSource>().isPlaying)
				{
					base.GetComponent<AudioSource>().Play();
				}
				if (this.count2 == 6)
				{
					if (base.GetComponent<AudioSource>().isPlaying)
					{
						base.GetComponent<AudioSource>().Stop();
					}
					SoundManager.Instance.Celebration_s();
					Fridge_Mini_Game_Main._inst.hand_water_2.SetActive(false);
					Fridge_Mini_Game_Main._inst.water_2.SetActive(false);
				}
			}
			if (this.count_main == 12)
			{
				UnityEngine.Object.Destroy(this.drag_tool.GetComponent<Drag_Mini_Game_Fridge>());
				iTween.MoveTo(this.drag_tool, iTween.Hash(new object[]
				{
					"x",
					12.44f,
					"y",
					2.11f,
					"time",
					1.0,
					"eastype",
					iTween.EaseType.linear,
					"islocal",
					true
				}));
				Fridge_Mini_Game_Main._inst.trey_items.SetActive(true);
			}
		}
		yield break;
	}

	private int count1;

	private int count2;

	private int count_main;

	public GameObject drag_tool;
}
