// dnSpy decompiler from Assembly-CSharp.dll class: Room_Cleanining_Long_Lamp
using System;
using System.Collections;
using UnityEngine;

public class Room_Cleanining_Long_Lamp : MonoBehaviour
{
	private void Start()
	{
	}

	private void Update()
	{
	}

	private IEnumerator Rotate_Long_Lamp()
	{
		yield return new WaitForSeconds(0.1f);
		this.click_s.Play();
		this.rotate_s.Play();
		this.long_lamp.GetComponent<tk2dButton>().enabled = false;
		this.long_lamp.GetComponent<BoxCollider>().enabled = false;
		iTween.RotateTo(this.long_lamp, iTween.Hash(new object[]
		{
			"z",
			this.demo_long_lamp.transform.rotation.z,
			"time",
			1.5,
			"eastype",
			iTween.EaseType.linear,
			"islocal",
			true
		}));
		iTween.MoveTo(this.long_lamp, iTween.Hash(new object[]
		{
			"x",
			this.demo_long_lamp.transform.position.x,
			"y",
			this.demo_long_lamp.transform.position.y,
			"time",
			1.5,
			"eastype",
			iTween.EaseType.linear,
			"islocal",
			true
		}));
		yield return new WaitForSeconds(1.51f);
		this.light_btn.SetActive(true);
		yield break;
	}

	public GameObject long_lamp;

	public GameObject demo_long_lamp;

	public GameObject light_btn;

	public AudioSource click_s;

	public AudioSource rotate_s;
}
