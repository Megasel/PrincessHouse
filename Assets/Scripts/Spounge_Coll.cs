// dnSpy decompiler from Assembly-CSharp.dll class: Spounge_Coll
using System;
using System.Collections;
using UnityEngine;

public class Spounge_Coll : MonoBehaviour
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
		if (base.gameObject.name == "shelf_cleaner_coll" && col.gameObject.tag == "mud_mask")
		{
			col.gameObject.GetComponent<SpriteMask>().enabled = true;
			col.gameObject.GetComponent<BoxCollider>().enabled = false;
			this.count++;
			if (!base.GetComponent<AudioSource>().isPlaying)
			{
				base.GetComponent<AudioSource>().Play();
			}
			if (this.count == 22)
			{
				if (base.GetComponent<AudioSource>().isPlaying)
				{
					base.GetComponent<AudioSource>().Stop();
				}
				SoundManager.Instance.Celebration_s();
				UnityEngine.Object.Destroy(this.drag_tool.GetComponent<Drag_Mini_Game_Fridge>());
				iTween.MoveTo(this.drag_tool, iTween.Hash(new object[]
				{
					"x",
					16.44f,
					"y",
					2.11f,
					"time",
					1.0,
					"eastype",
					iTween.EaseType.linear,
					"islocal",
					true
				}));
				Fridge_Mini_Game_Main._inst.mud_hand.SetActive(false);
				Fridge_Mini_Game_Main._inst.mud.SetActive(false);
				iTween.MoveTo(Fridge_Mini_Game_Main._inst.blowr_tool, iTween.Hash(new object[]
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
				Fridge_Mini_Game_Main._inst.snow_hand_1.SetActive(true);
				Fridge_Mini_Game_Main._inst.snow_hand_2.SetActive(true);
				Fridge_Mini_Game_Main._inst.snow_hand_3.SetActive(true);
				Fridge_Mini_Game_Main._inst.snow_hand_4.SetActive(true);
			}
		}
		yield break;
	}

	private int count;

	public GameObject drag_tool;
}
