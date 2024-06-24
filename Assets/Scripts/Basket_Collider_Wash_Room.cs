// dnSpy decompiler from Assembly-CSharp.dll class: Basket_Collider_Wash_Room
using System;
using System.Collections;
using UnityEngine;

public class Basket_Collider_Wash_Room : MonoBehaviour
{
	private void Start()
	{
		GameManager.Instance.count = 0;
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
			SoundManager.Instance.Click_s();
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
			SoundManager.Instance.Celebration_s();
			if (GameManager.Instance.count == 11)
			{
				iTween.MoveTo(WashRoom_Main._inst.Grid_1, iTween.Hash(new object[]
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
				WashRoom_Main._inst.hand_shower_g.SetActive(true);
			}
		}
		yield break;
	}

	public GameObject Basket;

	public GameObject hand;
}
