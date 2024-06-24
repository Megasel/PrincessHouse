// dnSpy decompiler from Assembly-CSharp.dll class: Rotate_Scripts
using System;
using System.Collections;
using UnityEngine;

public class Rotate_Scripts : MonoBehaviour
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
		this.rotate_item.GetComponent<tk2dButton>().enabled = false;
		GameManager.Instance.count++;
		this.rotate_item.GetComponent<BoxCollider>().enabled = false;
		iTween.RotateTo(this.rotate_item, iTween.Hash(new object[]
		{
			"z",
			this.rotation.z,
			"time",
			1.5,
			"eastype",
			iTween.EaseType.linear,
			"islocal",
			true
		}));
		yield break;
	}

	public GameObject rotate_item;

	public AudioSource click_s;

	public AudioSource rotate_s;

	public Vector3 rotation;
}
