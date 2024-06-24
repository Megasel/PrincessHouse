// dnSpy decompiler from Assembly-CSharp.dll class: Grass_Btn
using System;
using System.Collections;
using UnityEngine;

public class Grass_Btn : MonoBehaviour
{
	private void Start()
	{
	}

	private void Update()
	{
	}

	private IEnumerator grass_Btn(int j)
	{
		yield return new WaitForSeconds(0.01f);
		SoundManager.Instance.Click_s();
		for (int i = 0; i < this.grass_btn.Length; i++)
		{
			this.grass_btn[i].enabled = false;
		}
		this.grass_btn[j - 1].enabled = false;
		this.current_grass_pos.GetComponent<tk2dButton>().enabled = false;
		this.current_grass_pos.GetComponent<BoxCollider>().size = new Vector3(0f, 0f, 0f);
		iTween.MoveTo(this.tool_grass_pos, iTween.Hash(new object[]
		{
			"x",
			this.current_grass_pos.transform.position.x,
			"y",
			this.current_grass_pos.transform.position.y,
			"time",
			1.5,
			"eastype",
			iTween.EaseType.linear,
			"islocal",
			true
		}));
		yield return new WaitForSeconds(1.5f);
		this.grass_tool.enabled = true;
		this.grass_tool.Rebind();
		yield return new WaitForSeconds(1f);
		this.current_grass_pos.GetComponent<SpriteRenderer>().enabled = false;
		this.grass_p.Play();
		yield return new WaitForSeconds(1f);
		iTween.MoveTo(this.tool_grass_pos, iTween.Hash(new object[]
		{
			"x",
			10f,
			"y",
			0f,
			"time",
			1.5,
			"eastype",
			iTween.EaseType.linear,
			"islocal",
			true
		}));
		SoundManager.Instance.Celebration_s();
		for (int k = 0; k < this.grass_btn.Length; k++)
		{
			this.grass_btn[k].enabled = true;
		}
		this.grass_btn[j - 1].enabled = false;
		yield break;
	}

	public GameObject current_grass_pos;

	public GameObject tool_grass_pos;

	public Animator grass_tool;

	public ParticleSystem grass_p;

	public tk2dButton[] grass_btn;
}
