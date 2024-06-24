// dnSpy decompiler from Assembly-CSharp.dll class: Basket_Collider_Room_Cleaning
using System;
using System.Collections;
using UnityEngine;

public class Basket_Collider_Room_Cleaning : MonoBehaviour
{
	private void Start()
	{
	}

	private void Update()
	{
	}

	private IEnumerator OnTriggerEnter(Collider col)
	{
		yield return new WaitForSeconds(0.001f);
		if (base.gameObject.name == "Collider_Basket" && col.gameObject.tag == "garbage")
		{
			UnityEngine.Object.Destroy(col.gameObject);
			this.hand.SetActive(false);
			//Handheld.Vibrate();
			SoundManager.Instance.Celebration_s();
			SoundManager.Instance.superb_part();
			iTween.ShakeRotation(this.Basket, iTween.Hash(new object[]
			{
				"z",
				-7f,
				"time",
				0.3,
				"eastype",
				iTween.EaseType.linear,
				"islocal",
				true
			}));
			GameManager.Instance.count++;
			this.ads_count++;
			yield return new WaitForSeconds(0.5f);
			iTween.MoveTo(this.Basket, iTween.Hash(new object[]
			{
				"x",
				10.14f,
				"y",
				-2.03f,
				"time",
				0.5,
				"eastype",
				iTween.EaseType.linear,
				"islocal",
				true
			}));
			if (GameManager.Instance.count == 17)
			{
				iTween.MoveTo(Room_Cleaning_Main._inst.Grid_1, iTween.Hash(new object[]
				{
					"x",
					0f,
					"y",
					0f,
					"time",
					0.5,
					"eastype",
					iTween.EaseType.linear,
					"islocal",
					true
				}));
				Room_Cleaning_Main._inst.hand_shower_g.SetActive(true);
			}
			if (this.ads_count == 7)
			{
				Room_Cleaning_Main._inst.Ads.SetActive(true);
				yield return new WaitForSeconds(0.1f);
				Room_Cleaning_Main._inst.Ads.SetActive(false);
			}
		}
		yield break;
	}

	public GameObject Basket;

	public GameObject hand;

	private int ads_count;
}
