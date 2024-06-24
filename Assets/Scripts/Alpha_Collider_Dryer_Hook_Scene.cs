// dnSpy decompiler from Assembly-CSharp.dll class: Alpha_Collider_Dryer_Hook_Scene
using System;
using System.Collections;
using UnityEngine;

public class Alpha_Collider_Dryer_Hook_Scene : MonoBehaviour
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
			Alpha_Collider_Dryer_Hook_Scene._Drag = 1;
			SoundManager.Instance.Celebration_s();
			SoundManager.Instance.Click_s();
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
			this.suit.enabled = true;
		}
		if (GameManager.Instance.count == 4)
		{
			Dryer_Scene_MAIn._inst.hand_hooks.SetActive(false);
			SoundManager.Instance.Celebration_s();
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
		yield break;
	}

	public string game_object;

	public string col_game_object;

	public GameObject hand;

	public Animator suit;

	public static int _Drag;
}
