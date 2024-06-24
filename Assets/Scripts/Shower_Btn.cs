// dnSpy decompiler from Assembly-CSharp.dll class: Shower_Btn
using System;
using System.Collections;
using UnityEngine;

public class Shower_Btn : MonoBehaviour
{
	private void Start()
	{
	}

	private void Update()
	{
	}

	private IEnumerator Shower_Btn_n()
	{
		yield return new WaitForSeconds(0.1f);
		this.btn++;
		if (this.btn == 1)
		{
			this.Shower.Play();
			this.shower_s.Play();
			this.shower_wheel.GetComponent<Animator>().enabled = true;
			this.shower_wheel.GetComponent<Animator>().Rebind();
			yield return new WaitForSeconds(1.1f);
			for (int i = 0; i < this.Bubbles.Length; i++)
			{
				this.Bubbles[i].SetActive(true);
				yield return new WaitForSeconds(0.1f);
			}
		}
		if (this.btn == 2)
		{
			this.btn = 0;
			this.Shower.Stop();
			this.shower_s.Stop();
			this.shower_wheel.GetComponent<Animator>().enabled = true;
			this.shower_wheel.GetComponent<Animator>().Rebind();
			yield return new WaitForSeconds(2.1f);
			for (int j = 0; j < this.Bubbles.Length; j++)
			{
				this.Bubbles[j].SetActive(false);
				yield return new WaitForSeconds(0.02f);
			}
		}
		yield break;
	}

	public GameObject shower_wheel;

	public ParticleSystem Shower;

	public AudioSource shower_s;

	public GameObject[] Bubbles;

	private int btn;
}
