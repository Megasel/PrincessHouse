// dnSpy decompiler from Assembly-CSharp.dll class: Mud_Remover_btn
using System;
using System.Collections;
using UnityEngine;

public class Mud_Remover_btn : MonoBehaviour
{
	private void Start()
	{
	}

	private void Update()
	{
	}

	private IEnumerator mud_Btn(int j)
	{
		yield return new WaitForSeconds(0.01f);
		SoundManager.Instance.Click_s();
		for (int i = 0; i < this.mud_btn.Length; i++)
		{
			this.mud_btn[i].enabled = false;
		}
		this.mud_btn[j - 1].enabled = false;
		this.current_mud_pos.GetComponent<tk2dButton>().enabled = false;
		this.current_mud_pos.GetComponent<BoxCollider>().size = new Vector3(0f, 0f, 0f);
		iTween.MoveTo(this.Mud_Controller_all_items, iTween.Hash(new object[]
		{
			"x",
			this.current_mud_pos.transform.position.x,
			"y",
			this.current_mud_pos.transform.position.y,
			"time",
			1.5,
			"eastype",
			iTween.EaseType.linear,
			"islocal",
			true
		}));
		iTween.MoveTo(this.trolly, iTween.Hash(new object[]
		{
			"x",
			3.36,
			"y",
			-0.3,
			"time",
			1.5,
			"eastype",
			iTween.EaseType.linear,
			"islocal",
			true
		}));
		yield return new WaitForSeconds(1.5f);
		this.current_mud_pos.GetComponent<SpriteRenderer>().enabled = false;
		this.mud_pos_in_tool.GetComponent<SpriteRenderer>().enabled = true;
		this.mud_p.Play();
		this.hand_ind.SetActive(true);
		yield return new WaitForSeconds(1f);
		SoundManager.Instance.Celebration_s();
		yield break;
	}

	public GameObject Mud_Controller_all_items;

	public GameObject current_mud_pos;

	public GameObject tool_mud_pos;

	public GameObject mud_pos_in_tool;

	public ParticleSystem mud_p;

	public tk2dButton[] mud_btn;

	public GameObject trolly;

	public GameObject hand_ind;
}
