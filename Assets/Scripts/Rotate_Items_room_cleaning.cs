// dnSpy decompiler from Assembly-CSharp.dll class: Rotate_Items_room_cleaning
using System;
using System.Collections;
using UnityEngine;

public class Rotate_Items_room_cleaning : MonoBehaviour
{
	private void Start()
	{
	}

	private void Update()
	{
	}

	private IEnumerator Rotate()
	{
		yield return new WaitForSeconds(0.1f);
		this.click_s.Play();
		this.rotate_s.Play();
		SoundManager.Instance.Celebration_s();
		this.rotate_item.GetComponent<tk2dButton>().enabled = false;
		this.rotate_item.GetComponent<BoxCollider>().enabled = false;
		iTween.RotateTo(this.rotate_item, iTween.Hash(new object[]
		{
			"z",
			this.demo_item.transform.rotation.z,
			"time",
			1.5,
			"eastype",
			iTween.EaseType.linear,
			"islocal",
			true
		}));
		iTween.MoveTo(this.rotate_item, iTween.Hash(new object[]
		{
			"x",
			this.demo_item.transform.position.x,
			"y",
			this.demo_item.transform.position.y,
			"time",
			1.5,
			"eastype",
			iTween.EaseType.linear,
			"islocal",
			true
		}));
		yield return new WaitForSeconds(1.51f);
		yield break;
	}

	public GameObject rotate_item;

	public GameObject demo_item;

	public AudioSource click_s;

	public AudioSource rotate_s;
}
