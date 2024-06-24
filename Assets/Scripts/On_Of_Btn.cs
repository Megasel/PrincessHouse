// dnSpy decompiler from Assembly-CSharp.dll class: On_Of_Btn
using System;
using System.Collections;
using UnityEngine;

public class On_Of_Btn : MonoBehaviour
{
	private void Start()
	{
	}

	private void Update()
	{
	}

	private IEnumerator light_Btn()
	{
		yield return new WaitForSeconds(0.1f);
		if (this.light.activeInHierarchy)
		{
			this.light.SetActive(false);
		}
		else
		{
			this.light.SetActive(true);
		}
		yield break;
	}

	public GameObject light;
}
