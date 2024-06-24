// dnSpy decompiler from Assembly-CSharp.dll class: MAchine_Collider
using System;
using System.Collections;
using UnityEngine;

public class MAchine_Collider : MonoBehaviour
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
		if (base.gameObject.name == "machine_collider" && col.gameObject.name == "All_Black_Cloth_Center")
		{
			this.All_Black_Cloth.SetActive(false);
			this.All_Black_Cloth_in_Machine.SetActive(true);
			Dress_Washing_Main._inst.hand_center.SetActive(false);
			Dress_Washing_Main._inst.door_open.SetActive(false);
			Dress_Washing_Main._inst.door_close.SetActive(true);
			Dress_Washing_Main._inst.door_close.GetComponent<SpriteRenderer>().sortingOrder = 16;
			Dress_Washing_Main._inst.hand_center.SetActive(false);
			Dress_Washing_Main._inst.Hand_Detergent_1.SetActive(true);
			for (int i = 0; i < Dress_Washing_Main._inst.detergent_drag.Length; i++)
			{
				Dress_Washing_Main._inst.detergent_drag[i].enabled = true;
			}
		}
		if (base.gameObject.name == "machine_collider" && col.gameObject.name == "All_Clr_Cloth_Center")
		{
			this.All_Clr_Cloth.SetActive(false);
			this.All_Clr_Cloth_in_Machine.SetActive(true);
			Dress_Washing_Main._inst.hand_center.SetActive(false);
			Dress_Washing_Main._inst.door_open.SetActive(false);
			Dress_Washing_Main._inst.door_close.SetActive(true);
			Dress_Washing_Main._inst.door_close.GetComponent<SpriteRenderer>().sortingOrder = 16;
			Dress_Washing_Main._inst.hand_center.SetActive(false);
			Dress_Washing_Main._inst.Hand_Detergent_1.SetActive(true);
			for (int j = 0; j < Dress_Washing_Main._inst.detergent_drag.Length; j++)
			{
				Dress_Washing_Main._inst.detergent_drag[j].enabled = true;
			}
		}
		yield break;
	}

	public GameObject All_Black_Cloth;

	public GameObject All_Black_Cloth_in_Machine;

	public GameObject All_Clr_Cloth;

	public GameObject All_Clr_Cloth_in_Machine;
}
