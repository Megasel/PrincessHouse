// dnSpy decompiler from Assembly-CSharp.dll class: PopupHandler
using System;
using System.Reflection;
using UnityEngine;

public class PopupHandler : MonoBehaviour
{
	private void Start()
	{
		this.showingRatePopup = false;
		if (PlayerPrefs.GetInt("AppRate", 0) != 1)
		{
			this.ratePopupCount = PlayerPrefs.GetInt("ratePopupCount", 1);
			if (this.ratePopupCount % 3 == 0)
			{
				this.rateUsPopUp.SetActive(true);
				this.showingRatePopup = true;
			}
			PlayerPrefs.SetInt("ratePopupCount", this.ratePopupCount + 1);
		}
		if (PlayerPrefs.GetInt("UnlockAll", 0) != 1)
		{
			this.HasToShow();
		}
	}

	public void HasToShow()
	{
		if (!PlayerPrefs.HasKey("StartDate"))
		{
			PlayerPrefs.SetString("StartDate", DateTime.Now.ToBinary().ToString());
		}
		long dateData = Convert.ToInt64(PlayerPrefs.GetString("StartDate"));
		this.oldDate = DateTime.FromBinary(dateData);
		UnityEngine.Debug.Log("oldDate: " + this.oldDate);
		this.currentDate = DateTime.Now;
		UnityEngine.Debug.Log("Date : " + this.currentDate);
		TimeSpan timeSpan = this.currentDate.Subtract(this.oldDate);
		UnityEngine.Debug.Log(string.Concat(new object[]
		{
			"Difference: ",
			timeSpan,
			" : Days : ",
			timeSpan.Days
		}));
		if (timeSpan.TotalDays >= 5.0)
		{
			if (this.purchaseRandomPopUpCount % 2 == 0 && !this.showingRatePopup)
			{
				this.purchasePopupOnHalfPrice.SetActive(true);
			}
		}
		else
		{
			this.purchaseRandomPopUpCount = PlayerPrefs.GetInt("purchaseCount", 1);
			if (this.purchaseRandomPopUpCount % 2 == 0 && !this.showingRatePopup)
			{
				this.purchasePopup.SetActive(true);
			}
			PlayerPrefs.SetInt("purchaseCount", this.purchaseRandomPopUpCount + 1);
		}
	}

	public void RateUs()
	{
		
	}

	private int ratePopupCount;

	private int purchaseRandomPopUpCount;

	private bool showingRatePopup;

	public GameObject purchasePopup;

	public GameObject purchasePopupOnHalfPrice;

	public GameObject rateUsPopUp;

	private DateTime currentDate;

	private DateTime oldDate;
}
