// dnSpy decompiler from Assembly-CSharp.dll class: Black_Basket_Collider_For_Clean_Cloth
using System;
using System.Collections;
using UnityEngine;

public class Black_Basket_Collider_For_Clean_Cloth : MonoBehaviour
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
		if (base.gameObject.name == "black_basket_collider")
		{
			if (col.gameObject.name == "All_Black_Cloth_In_Machine")
			{
				SoundManager.Instance.Celebration_s();
				Dress_Washing_Main._inst.Hand_machine_to_black_basket.SetActive(false);
				Dress_Washing_Main._inst.All_Black_cloth_in_machine.SetActive(false);
				for (int i = 0; i < Dress_Washing_Main._inst.All_clean_Black_Cloth_In_Basket.Length; i++)
				{
					Dress_Washing_Main._inst.All_clean_Black_Cloth_In_Basket[i].GetComponent<SpriteRenderer>().enabled = true;
				}
				Dress_Washing_Main._inst.Main_Clr_Box_Collider.SetActive(false);
				Dress_Washing_Main._inst.color_Basket_Hand.SetActive(true);
				Dress_Washing_Main._inst.door_close.SetActive(true);
				Dress_Washing_Main._inst.door_open.SetActive(false);
				GameManager.Instance.cloth = 2;
				UnityEngine.Debug.Log(GameManager.Instance.cloth);
				Dress_Washing_Main._inst.v_o_put_cloth.Play();
			}
		}
		else if (base.gameObject.name == "clr_basket_collider" && col.gameObject.name == "All_Clr_Cloth_in_Machine")
		{
			SoundManager.Instance.Celebration_s();
			Dress_Washing_Main._inst.Hand_machine_to_clr_basket.SetActive(false);
			Dress_Washing_Main._inst.All_Clr_cloth_in_machine.SetActive(false);
			for (int j = 0; j < Dress_Washing_Main._inst.All_clean_Clr_Cloth_In_Basket.Length; j++)
			{
				Dress_Washing_Main._inst.All_clean_Clr_Cloth_In_Basket[j].GetComponent<SpriteRenderer>().enabled = true;
			}
			Dress_Washing_Main._inst.door_open.SetActive(false);
			Dress_Washing_Main._inst.door_close.SetActive(false);
			iTween.MoveTo(GameObject.Find("Next_Btn_All_Items"), iTween.Hash(new object[]
			{
				"x",
				0f,
				"y",
				0f,
				"time",
				1.5,
				"eastype",
				iTween.EaseType.linear,
				"islocal",
				true
			}));
		}
		yield break;
	}
}
