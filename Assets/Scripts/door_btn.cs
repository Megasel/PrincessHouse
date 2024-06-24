// dnSpy decompiler from Assembly-CSharp.dll class: door_btn
using System;
using System.Collections;
using UnityEngine;

public class door_btn : MonoBehaviour
{
	private void Start()
	{
	}

	private void Update()
	{
	}

	private IEnumerator door_Btn()
	{
		yield return new WaitForSeconds(0.1f);
		this._door.GetComponent<SpriteRenderer>().flipX = true;
		this._door.GetComponent<tk2dButton>().enabled = false;
		yield break;
	}

	public GameObject _door;
}
