// dnSpy decompiler from Assembly-CSharp.dll class: leave_down
using System;
using System.Collections;
using UnityEngine;

public class leave_down : MonoBehaviour
{
	private void Start()
	{
	}

	private void Update()
	{
	}

	private IEnumerator leave_btn()
	{
		yield return new WaitForSeconds(0.1f);
		this.Leave.GetComponent<BoxCollider>().enabled = false;
		this.hand.SetActive(false);
		this.Leave.GetComponent<Animator>().enabled = true;
		iTween.MoveTo(this.Leave, iTween.Hash(new object[]
		{
			"y",
			-2.45f,
			"time",
			1.5,
			"eastype",
			iTween.EaseType.linear,
			"islocal",
			true
		}));
		GameManager.Instance.count++;
		UnityEngine.Debug.Log(GameManager.Instance.count);
		SoundManager.Instance.Click_s();
		SoundManager.Instance.Celebration_s();
		if (GameManager.Instance.count == 6)
		{
			Plant_mini_game_Main._inst.vast_collider.SetActive(true);
			iTween.MoveTo(Plant_mini_game_Main._inst.seed_tool, iTween.Hash(new object[]
			{
				"x",
				3.95f,
				"y",
				0.33f,
				"time",
				1.5,
				"eastype",
				iTween.EaseType.linear,
				"islocal",
				true
			}));
			GameManager.Instance.count = 0;
			Plant_mini_game_Main._inst.hands_seed_box.SetActive(true);
		}
		yield return new WaitForSeconds(1.5f);
		this.Leave.GetComponent<Animator>().enabled = false;
		yield break;
	}

	public GameObject Leave;

	public GameObject hand;
}
