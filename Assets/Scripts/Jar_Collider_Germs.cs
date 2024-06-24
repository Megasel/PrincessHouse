// dnSpy decompiler from Assembly-CSharp.dll class: Jar_Collider_Germs
using System;
using System.Collections;
using UnityEngine;

public class Jar_Collider_Germs : MonoBehaviour
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
		if (base.gameObject.name == this._gameObject && col.gameObject.name == "jar_collider")
		{
			SoundManager.Instance.Celebration_s();
			base.gameObject.GetComponent<BoxCollider>().enabled = false;
			base.gameObject.GetComponent<SpriteRenderer>().enabled = false;
			UnityEngine.Object.Destroy(base.gameObject.GetComponent<Drag_Plant_Mini_game>());
			this.germ_in_jar.SetActive(true);
			Plant_mini_game_Main._inst.hand_germs_jar.SetActive(true);
			GameManager.Instance.count++;
			if (GameManager.Instance.count == 6)
			{
				Plant_mini_game_Main._inst.hand_germs_jar.SetActive(false);
				iTween.ScaleTo(Plant_mini_game_Main._inst.Bg, iTween.Hash(new object[]
				{
					"x",
					1f,
					"y",
					1f,
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
					0f,
					"time",
					1.5,
					"eastype",
					iTween.EaseType.linear,
					"islocal",
					true
				}));
				iTween.MoveTo(Plant_mini_game_Main._inst.jar_all_items, iTween.Hash(new object[]
				{
					"x",
					10f,
					"y",
					0f,
					"time",
					1.5,
					"eastype",
					iTween.EaseType.linear,
					"islocal",
					true
				}));
				Plant_mini_game_Main._inst.hands_leaves.SetActive(true);
				for (int i = 0; i < Plant_mini_game_Main._inst.Dirt_leaves.Length; i++)
				{
					Plant_mini_game_Main._inst.Dirt_leaves[i].GetComponent<BoxCollider>().size = new Vector3(0.7f, 0.7f, 1f);
					GameManager.Instance.count = 0;
				}
			}
		}
		yield break;
	}

	private int count;

	public string _gameObject;

	public GameObject germ_in_jar;
}
