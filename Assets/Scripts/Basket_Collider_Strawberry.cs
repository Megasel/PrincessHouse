// dnSpy decompiler from Assembly-CSharp.dll class: Basket_Collider_Strawberry
using System;
using System.Collections;
using UnityEngine;

public class Basket_Collider_Strawberry : MonoBehaviour
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
		if (base.gameObject.name == "Collider_Basket" && col.gameObject.tag == "strawberry")
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
			this.count++;
			this.strawberry[this.count - 1].SetActive(true);
			SoundManager.Instance.Celebration_s();
			yield return new WaitForSeconds(0.5f);
			if (this.count == 6)
			{
				UnityEngine.Debug.Log("cmp");
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
				iTween.MoveTo(GameObject.Find("Next_Btn_All_Items"), iTween.Hash(new object[]
				{
					"x",
					0f,
					"y",
					0f,
					"time",
					1.5,
					"eastype",
					iTween.EaseType.linear,
					"islocal",
					true
				}));
			}
		}
		yield break;
	}

	public GameObject hand;

	public GameObject Basket;

	public GameObject[] strawberry;

	private int count;
}
