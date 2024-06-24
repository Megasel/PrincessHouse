// dnSpy decompiler from Assembly-CSharp.dll class: Alpha_Collider_Dryer_Scene
using System;
using System.Collections;
using UnityEngine;

public class Alpha_Collider_Dryer_Scene : MonoBehaviour
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
		if (base.gameObject.name == this.game_object && col.gameObject.name == this.col_game_object)
		{
			SoundManager.Instance.Click_s();
			SoundManager.Instance.Celebration_s();
			Alpha_Collider_Dryer_Scene._Drag = 1;
			UnityEngine.Debug.Log(this.col_game_object);
			base.gameObject.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
			base.gameObject.GetComponent<BoxCollider>().enabled = false;
			iTween.ScaleTo(col.gameObject, iTween.Hash(new object[]
			{
				"x",
				0f,
				"y",
				0f,
				"time",
				0.0,
				"eastype",
				iTween.EaseType.linear,
				"islocal",
				true
			}));
			this.hand.SetActive(false);
			GameManager.Instance.count++;
			yield return new WaitForSeconds(0.1f);
		}
		if (GameManager.Instance.count == 4)
		{
			Dryer_Scene_MAIn._inst.All_hook_items.SetActive(true);
			Dryer_Scene_MAIn._inst.hand_hooks.SetActive(true);
			GameManager.Instance.count = 0;
			SoundManager.Instance.Celebration_s();
		}
		yield break;
	}

	public string game_object;

	public string col_game_object;

	public GameObject hand;

	public static int _Drag;
}
