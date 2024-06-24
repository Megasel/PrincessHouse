// dnSpy decompiler from Assembly-CSharp.dll class: Clr_Collider_Center
using System;
using System.Collections;
using UnityEngine;

public class Clr_Collider_Center : MonoBehaviour
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
		if (base.gameObject.name == this._gameobject && col.gameObject.name == this._colgameobject)
		{
			base.gameObject.GetComponent<SpriteRenderer>().enabled = false;
			base.gameObject.GetComponent<BoxCollider>().enabled = false;
			SoundManager.Instance.Celebration_s();
			SoundManager.Instance.Click_s();
			base.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().enabled = false;
			col.gameObject.GetComponent<SpriteRenderer>().enabled = true;
			this.Dirt_cloth_dirt.SetActive(true);
			GameManager.Instance.clr_count++;
			UnityEngine.Debug.Log(GameManager.Instance.clr_count);
			if (GameManager.Instance.clr_count == 7)
			{
				SoundManager.Instance.Celebration_s();
				SoundManager.Instance.Click_s();
				Dress_Washing_Main._inst.color_Basket_Hand.SetActive(false);
				Dress_Washing_Main._inst.Hand_Cap.SetActive(true);
				Dress_Washing_Main._inst.door_close.GetComponent<tk2dButton>().enabled = true;
				GameManager.Instance.cloth = 2;
				UnityEngine.Debug.Log(GameManager.Instance.cloth);
			}
		}
		yield break;
	}

	public string _gameobject;

	public string _colgameobject;

	public GameObject Dirt_cloth_dirt;
}
