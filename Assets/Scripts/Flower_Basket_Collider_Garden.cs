// dnSpy decompiler from Assembly-CSharp.dll class: Flower_Basket_Collider_Garden
using System;
using System.Collections;
using UnityEngine;

public class Flower_Basket_Collider_Garden : MonoBehaviour
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
		if (base.gameObject.name == "Collider_Basket_flower")
		{
			if (col.gameObject.tag == "red_flower")
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
				this.count1++;
				SoundManager.Instance.Celebration_s();
				this.fill += 0.142f;
				iTween.ScaleTo(this.front_bar, iTween.Hash(new object[]
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
				this.red_flower.SetActive(true);
				if (this.count1 == 2)
				{
					this.red_flower_2.SetActive(true);
				}
				yield return new WaitForSeconds(0.5f);
				if (this.count == 7)
				{
					iTween.MoveTo(this.Bg, iTween.Hash(new object[]
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
					iTween.ScaleTo(this.Bg, iTween.Hash(new object[]
					{
						"x",
						1f,
						"y",
						1f,
						"time",
						1.5,
						"eastype",
						iTween.EaseType.linear,
						"islocal",
						true
					}));
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
					Garden_Main._inst.Trolly.SetActive(true);
					Garden_Main._inst.mud_items.SetActive(true);
					this.loading_bar.SetActive(false);
				}
			}
			else if (col.gameObject.tag == "yellow_flower")
			{
				UnityEngine.Object.Destroy(col.gameObject);
				this.hand.SetActive(false);
				this.yellow_flower.SetActive(true);
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
				SoundManager.Instance.Celebration_s();
				this.count++;
				this.fill += 0.142f;
				iTween.ScaleTo(this.front_bar, iTween.Hash(new object[]
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
				yield return new WaitForSeconds(0.5f);
				if (this.count == 7)
				{
					iTween.MoveTo(this.Bg, iTween.Hash(new object[]
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
					iTween.ScaleTo(this.Bg, iTween.Hash(new object[]
					{
						"x",
						1f,
						"y",
						1f,
						"time",
						1.5,
						"eastype",
						iTween.EaseType.linear,
						"islocal",
						true
					}));
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
					Garden_Main._inst.Trolly.SetActive(true);
					Garden_Main._inst.mud_items.SetActive(true);
					this.loading_bar.SetActive(false);
				}
			}
			else if (col.gameObject.tag == "red_green_flower")
			{
				UnityEngine.Object.Destroy(col.gameObject);
				this.hand.SetActive(false);
				this.Red_Green_flower.SetActive(true);
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
				SoundManager.Instance.Celebration_s();
				this.count++;
				this.fill += 0.142f;
				iTween.ScaleTo(this.front_bar, iTween.Hash(new object[]
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
				yield return new WaitForSeconds(0.5f);
				if (this.count == 7)
				{
					iTween.MoveTo(this.Bg, iTween.Hash(new object[]
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
					iTween.ScaleTo(this.Bg, iTween.Hash(new object[]
					{
						"x",
						1f,
						"y",
						1f,
						"time",
						1.5,
						"eastype",
						iTween.EaseType.linear,
						"islocal",
						true
					}));
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
					Garden_Main._inst.Trolly.SetActive(true);
					Garden_Main._inst.mud_items.SetActive(true);
					this.loading_bar.SetActive(false);
				}
			}
		}
		yield break;
	}

	public GameObject Basket;

	public GameObject hand;

	public GameObject red_flower;

	public GameObject red_flower_2;

	public GameObject yellow_flower;

	public GameObject Red_Green_flower;

	public GameObject Bg;

	private int count;

	private int count1;

	public GameObject front_bar;

	public GameObject loading_bar;

	private float fill;
}
