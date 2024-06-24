// dnSpy decompiler from Assembly-CSharp.dll class: Germs_Plant_Mini_game
using System;
using System.Collections;
using UnityEngine;

public class Germs_Plant_Mini_game : MonoBehaviour
{
	private void Start()
	{
	}

	private void Update()
	{
	}

	private IEnumerator Germs_Btn()
	{
		yield return new WaitForSeconds(0.1f);
		this.brust.Play();
		base.StartCoroutine(this.Particle_Play());
		yield return new WaitForSeconds(0.3f);
		SoundManager.Instance.Celebration_s();
		this.germs.SetActive(false);
		yield break;
	}

	private IEnumerator Particle_Play()
	{
		yield return new WaitForSeconds(0.0001f);
		this.hit.Play();
		this.pos = Camera.main.ScreenToWorldPoint(UnityEngine.Input.mousePosition);
		this.hit.transform.position = new Vector3(this.pos.x, this.pos.y, 0f);
		yield break;
	}

	public GameObject germs;

	public AudioSource brust;

	public ParticleSystem hit;

	private Vector3 pos;
}
