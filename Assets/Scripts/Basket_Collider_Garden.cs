// dnSpy decompiler from Assembly-CSharp.dll class: Basket_Collider_Garden
using System;
using System.Collections;
using UnityEngine;

public class Basket_Collider_Garden : MonoBehaviour
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
			SoundManager.Instance.Celebration_s();
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
			if (GameManager.Instance.count == 15)
			{
				Garden_Main._inst.hand_Flower_Area.SetActive(true);
				Garden_Main._inst.garden_btn.GetComponent<tk2dButton>().enabled = true;

				yield return new WaitForSeconds(0.1f);
				Garden_Main._inst.Ads.SetActive(false);
			}
		}
		yield break;
	}

	public GameObject Basket;

	public GameObject hand;
}
