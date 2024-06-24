// dnSpy decompiler from Assembly-CSharp.dll class: Trolly_Collider
using System;
using System.Collections;
using UnityEngine;

public class Trolly_Collider : MonoBehaviour
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
		if (base.gameObject.name == "trolly_Collider")
		{
			UnityEngine.Debug.Log(this.count);
			UnityEngine.Debug.Log(col.gameObject);
			if (col.gameObject.name == "mud_in_collector_drag_1")
			{
				this.count++;
				SoundManager.Instance.Part_s();
				this.mud_collector_Anim.enabled = true;
				this.mud_collector_Anim.Rebind();
				UnityEngine.Object.Destroy(col.gameObject.GetComponent<Drag_Mud_Collector>());
				col.gameObject.GetComponent<BoxCollider>().enabled = false;
				this.hand_1.SetActive(false);
				this.mud_Collector_All_items.SetActive(false);
				yield return new WaitForSeconds(0.1f);
				col.gameObject.AddComponent<Drag_Mud_Collector>();
				yield return new WaitForSeconds(1.55f);
				SoundManager.Instance.Celebration_s();
				this.mud_1_Trolly.SetActive(true);
				this.mud_btn_1.enabled = true;
				this.mud_btn_2.enabled = true;
				yield return new WaitForSeconds(1.1f);
				iTween.MoveTo(this.Bg, iTween.Hash(new object[]
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
				iTween.ScaleTo(this.Bg, iTween.Hash(new object[]
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
				iTween.MoveTo(this.Trolly, iTween.Hash(new object[]
				{
					"x",
					10.14f,
					"y",
					-2.03f,
					"time",
					0.5,
					"eastype",
					iTween.EaseType.linear,
					"islocal",
					true
				}));
			}
			else if (col.gameObject.name == "mud_in_collector_drag_2")
			{
				this.count++;
				SoundManager.Instance.Part_s();
				this.mud_collector_Anim.enabled = true;
				this.mud_collector_Anim.Rebind();
				UnityEngine.Object.Destroy(col.gameObject.GetComponent<Drag_Mud_Collector>());
				col.gameObject.GetComponent<BoxCollider>().enabled = false;
				this.hand_2.SetActive(false);
				this.mud_Collector_All_items_2.SetActive(false);
				yield return new WaitForSeconds(0.1f);
				col.gameObject.AddComponent<Drag_Mud_Collector>();
				yield return new WaitForSeconds(3.1f);
				this.mud_2_Trolly.SetActive(true);
				this.mud_btn_1.enabled = true;
				this.mud_btn_2.enabled = true;
				SoundManager.Instance.Celebration_s();
				yield return new WaitForSeconds(1.1f);
				iTween.MoveTo(this.Bg, iTween.Hash(new object[]
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
				iTween.ScaleTo(this.Bg, iTween.Hash(new object[]
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
				iTween.MoveTo(this.Trolly, iTween.Hash(new object[]
				{
					"x",
					10.14f,
					"y",
					-2.03f,
					"time",
					0.5,
					"eastype",
					iTween.EaseType.linear,
					"islocal",
					true
				}));
			}
			yield return new WaitForSeconds(2.1f);
		}
		yield break;
	}

	public Animator mud_collector_Anim;

	public GameObject mud_Collector_All_items;

	public GameObject mud_Collector_All_items_2;

	public GameObject Trolly;

	public GameObject mud_1_Trolly;

	public GameObject mud_2_Trolly;

	public GameObject hand_1;

	public GameObject hand_2;

	public GameObject Bg;

	private int count;

	public tk2dButton mud_btn_1;

	public tk2dButton mud_btn_2;
}
