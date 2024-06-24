// dnSpy decompiler from Assembly-CSharp.dll class: Alpha_Collider_Wash_Cleaning
using System;
using System.Collections;
using UnityEngine;

public class Alpha_Collider_Wash_Cleaning : MonoBehaviour
{
	private void Start()
	{
	}

	private void Update()
	{
	}

	private IEnumerator OnTriggerEnter(Collider col)
	{
		yield return new WaitForSeconds(0.1f);
		if (base.gameObject.name == this.game_object && col.gameObject.name == this.col_game_object)
		{
			UnityEngine.Debug.Log(this.col_game_object);
			base.gameObject.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
			base.gameObject.GetComponent<BoxCollider>().enabled = false;
			iTween.ScaleTo(col.gameObject, iTween.Hash(new object[]
			{
				"x",
				0f,
				"y",
				0f,
				"time",
				0.0,
				"eastype",
				iTween.EaseType.linear,
				"islocal",
				true
			}));
			this.hand.SetActive(false);
			GameManager.Instance.count++;
			SoundManager.Instance.Celebration_s();
		}
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
		yield break;
	}

	public string game_object;

	public string col_game_object;

	public GameObject hand;
}
