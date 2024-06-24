// dnSpy decompiler from Assembly-CSharp.dll class: Center_Collider_Black_Cloth
using System;
using System.Collections;
using UnityEngine;

public class Center_Collider_Black_Cloth : MonoBehaviour
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
		if (base.gameObject.name == "cloth_Center_Boxcollider_Black" && col.gameObject.tag == "black_basket_cloth")
		{
			this.count++;
			SoundManager.Instance.Celebration_s();
			SoundManager.Instance.Click_s();
			base.gameObject.GetComponent<SpriteRenderer>().enabled = false;
			base.gameObject.GetComponent<BoxCollider>().enabled = false;
			col.gameObject.GetComponent<SpriteRenderer>().enabled = true;
		}
		if (base.gameObject.name == "cloth_Center_Boxcollider_Black" && col.gameObject.tag == "black_basket_cloth")
		{
			this.count++;
			SoundManager.Instance.Celebration_s();
			SoundManager.Instance.Click_s();
			base.gameObject.GetComponent<SpriteRenderer>().enabled = false;
			base.gameObject.GetComponent<BoxCollider>().enabled = false;
			col.gameObject.GetComponent<SpriteRenderer>().enabled = true;
		}
		yield break;
	}

	private int count;
}
