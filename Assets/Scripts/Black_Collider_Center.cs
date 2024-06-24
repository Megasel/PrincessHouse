// dnSpy decompiler from Assembly-CSharp.dll class: Black_Collider_Center
using System;
using System.Collections;
using UnityEngine;

public class Black_Collider_Center : MonoBehaviour
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
			SoundManager.Instance.Celebration_s();
			base.gameObject.GetComponent<SpriteRenderer>().enabled = false;
			base.gameObject.GetComponent<BoxCollider>().enabled = false;
			base.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().enabled = false;
			col.gameObject.GetComponent<SpriteRenderer>().enabled = true;
			this.Dirt_cloth_dirt.SetActive(true);
			GameManager.Instance.black_count++;
			if (GameManager.Instance.black_count == 3)
			{
				Dress_Washing_Main._inst.black_Basket_Hand.SetActive(false);
				Dress_Washing_Main._inst.Hand_Cap.SetActive(true);
				Dress_Washing_Main._inst.door_close.GetComponent<tk2dButton>().enabled = true;
			}
		}
		yield break;
	}

	public string _gameobject;

	public string _colgameobject;

	public GameObject Dirt_cloth_dirt;
}
