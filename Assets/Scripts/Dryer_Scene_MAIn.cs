// dnSpy decompiler from Assembly-CSharp.dll class: Dryer_Scene_MAIn
using System;
using UnityEngine;

public class Dryer_Scene_MAIn : MonoBehaviour
{
	private void Start()
	{
		if (Dryer_Scene_MAIn._inst == null)
		{
			Dryer_Scene_MAIn._inst = this;
		}
		GameManager.Instance.count = 0;
	}

	private void Update()
	{
	}

	public static Dryer_Scene_MAIn _inst;

	public Animator[] Cloth_Anim;

	public ParticleSystem Smoke_p;

	public GameObject All_hook_items;

	public GameObject hand_hooks;
}
