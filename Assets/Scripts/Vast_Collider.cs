// dnSpy decompiler from Assembly-CSharp.dll class: Vast_Collider
using System;
using System.Collections;
using UnityEngine;

public class Vast_Collider : MonoBehaviour
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
		if (base.gameObject.name == "vast_Collider")
		{
			if (col.gameObject.name == "seeds_tool")
			{
				this.filling_seed.Play();
				Plant_mini_game_Main._inst.hands_seed_box.SetActive(false);
				col.gameObject.GetComponent<SpriteRenderer>().enabled = false;
				col.gameObject.GetComponent<BoxCollider>().enabled = false;
				UnityEngine.Object.Destroy(col.gameObject.GetComponent<Drag_Plant_Mini_game>());
				this.Seed_Bag_Anim.GetComponent<Animator>().enabled = true;
				yield return new WaitForSeconds(3.1f);
				this.filling_seed.Stop();
				yield return new WaitForSeconds(3.1f);
				iTween.MoveTo(Plant_mini_game_Main._inst.water_tool, iTween.Hash(new object[]
				{
					"x",
					4.22f,
					"y",
					-0.22f,
					"time",
					1.5,
					"eastype",
					iTween.EaseType.linear,
					"islocal",
					true
				}));
				Plant_mini_game_Main._inst.hands_water_box.SetActive(true);
			}
			else if (col.gameObject.name == "water_tool")
			{
				Plant_mini_game_Main._inst.hands_water_box.SetActive(false);
				col.gameObject.GetComponent<SpriteRenderer>().enabled = false;
				col.gameObject.GetComponent<BoxCollider>().enabled = false;
				UnityEngine.Object.Destroy(col.gameObject.GetComponent<Drag_Plant_Mini_game>());
				this.Water_tool_Anim.GetComponent<Animator>().enabled = true;
				this.shower.Play();
				yield return new WaitForSeconds(6.1f);
				this.shower.Stop();
				iTween.ScaleTo(Plant_mini_game_Main._inst.Bg, iTween.Hash(new object[]
				{
					"x",
					1.6f,
					"y",
					1.6f,
					"time",
					1.5,
					"eastype",
					iTween.EaseType.linear,
					"islocal",
					true
				}));
				iTween.MoveTo(Plant_mini_game_Main._inst.Bg, iTween.Hash(new object[]
				{
					"x",
					0f,
					"y",
					-0.5f,
					"time",
					1.5,
					"eastype",
					iTween.EaseType.linear,
					"islocal",
					true
				}));
				for (int i = 0; i < Plant_mini_game_Main._inst.new_leaves.Length; i++)
				{
					iTween.ScaleTo(Plant_mini_game_Main._inst.new_leaves[i], iTween.Hash(new object[]
					{
						"x",
						1f,
						"y",
						1f,
						"time",
						10.5,
						"eastype",
						iTween.EaseType.linear,
						"islocal",
						true
					}));
					iTween.ScaleTo(Plant_mini_game_Main._inst.Straw_Berry[i], iTween.Hash(new object[]
					{
						"x",
						1f,
						"y",
						1f,
						"time",
						10.5,
						"eastype",
						iTween.EaseType.linear,
						"islocal",
						true
					}));
				}
				yield return new WaitForSeconds(10.1f);
				for (int j = 0; j < Plant_mini_game_Main._inst.new_leaves.Length; j++)
				{
					Plant_mini_game_Main._inst.Straw_Berry[j].GetComponent<BoxCollider>().enabled = true;
				}
				Plant_mini_game_Main._inst.vast_collider.SetActive(false);
				Plant_mini_game_Main._inst.hands_leaves.SetActive(true);
				iTween.MoveTo(Plant_mini_game_Main._inst.Strawberry_Basket, iTween.Hash(new object[]
				{
					"x",
					2.6f,
					"y",
					-0.3f,
					"time",
					1.5,
					"eastype",
					iTween.EaseType.linear,
					"islocal",
					true
				}));
			}
		}
		yield break;
	}

	public GameObject Seed_Bag_Anim;

	public GameObject Water_tool_Anim;

	public AudioSource shower;

	public AudioSource filling_seed;
}
