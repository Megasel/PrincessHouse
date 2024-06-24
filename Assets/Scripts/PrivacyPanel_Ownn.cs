// dnSpy decompiler from Assembly-CSharp.dll class: PrivacyPanel_Ownn
using System;
using UnityEngine;

public class PrivacyPanel_Ownn : MonoBehaviour
{
	private void Start()
	{
		if (PlayerPrefs.GetInt("PrivacyPolicy", 0) == 1)
		{
			base.gameObject.SetActive(false);
		

		}
	}

	private void Update()
	{
	}

	public void AcceptPrivacyPolicy()
	{
		PlayerPrefs.SetInt("PrivacyPolicy", 1);
		
	}

	

	
}
