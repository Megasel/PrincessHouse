// dnSpy decompiler from Assembly-CSharp.dll class: DeterGent_Coll
using System;
using System.Collections;
using UnityEngine;

public class DeterGent_Coll : MonoBehaviour
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
		if (base.gameObject.name == "machine_up_right_Btn")
		{
			if (col.gameObject.name == "detergent_1")
			{
				DeterGent_Coll.drag = 1;
				SoundManager.Instance.Celebration_s();
				SoundManager.Instance.Click_s();
				col.gameObject.GetComponent<SpriteRenderer>().enabled = false;
				col.gameObject.GetComponent<BoxCollider>().enabled = false;
				Dress_Washing_Main._inst.detergent_anim[0].enabled = true;
				Dress_Washing_Main._inst.detergent_anim[0].Rebind();
				yield return new WaitForSeconds(0.1f);
				Dress_Washing_Main._inst.filling_s.Play();
				Dress_Washing_Main._inst.Hand_Detergent_1.SetActive(false);
				Dress_Washing_Main._inst.Hand_Detergent_2.SetActive(false);
				for (int i = 0; i < Dress_Washing_Main._inst.detergent_drag.Length; i++)
				{
					Dress_Washing_Main._inst.detergent_drag[i].enabled = false;
				}
				yield return new WaitForSeconds(1.3f);
				iTween.ScaleTo(Dress_Washing_Main._inst.liquid_green, iTween.Hash(new object[]
				{
					"x",
					1f,
					"y",
					1f,
					"time",
					4.0,
					"eastype",
					iTween.EaseType.linear,
					"islocal",
					true
				}));
				Dress_Washing_Main._inst.filling_s.Stop();
				yield return new WaitForSeconds(3.78f);
				Dress_Washing_Main._inst.Hand_Detergent_1.SetActive(false);
				Dress_Washing_Main._inst.Hand_Detergent_2.SetActive(false);
				col.gameObject.GetComponent<SpriteRenderer>().enabled = true;
				Dress_Washing_Main._inst.on_of_bar_hand.SetActive(true);
				Dress_Washing_Main._inst.On_Off_bar.GetComponent<BoxCollider>().enabled = true;
			}
			else if (col.gameObject.name == "detergent_2")
			{
				DeterGent_Coll.drag = 1;
				SoundManager.Instance.Celebration_s();
				SoundManager.Instance.Click_s();
				col.gameObject.GetComponent<SpriteRenderer>().enabled = false;
				col.gameObject.GetComponent<BoxCollider>().enabled = false;
				Dress_Washing_Main._inst.detergent_anim[1].enabled = true;
				Dress_Washing_Main._inst.detergent_anim[1].Rebind();
				yield return new WaitForSeconds(0.1f);
				Dress_Washing_Main._inst.filling_s.Play();
				Dress_Washing_Main._inst.Hand_Detergent_1.SetActive(false);
				Dress_Washing_Main._inst.Hand_Detergent_2.SetActive(false);
				for (int j = 0; j < Dress_Washing_Main._inst.detergent_drag.Length; j++)
				{
					Dress_Washing_Main._inst.detergent_drag[j].enabled = false;
				}
				yield return new WaitForSeconds(1.3f);
				Dress_Washing_Main._inst.filling_s.Stop();
				iTween.ScaleTo(Dress_Washing_Main._inst.white_powder, iTween.Hash(new object[]
				{
					"x",
					1f,
					"y",
					1f,
					"time",
					4.0,
					"eastype",
					iTween.EaseType.linear,
					"islocal",
					true
				}));
				yield return new WaitForSeconds(3.78f);
				Dress_Washing_Main._inst.Hand_Detergent_1.SetActive(false);
				Dress_Washing_Main._inst.Hand_Detergent_2.SetActive(false);
				col.gameObject.GetComponent<SpriteRenderer>().enabled = true;
				Dress_Washing_Main._inst.on_of_bar_hand.SetActive(true);
				Dress_Washing_Main._inst.On_Off_bar.GetComponent<BoxCollider>().enabled = true;
			}
		}
		yield break;
	}

	public static int drag;
}
