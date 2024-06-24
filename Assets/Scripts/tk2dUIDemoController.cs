// dnSpy decompiler from Assembly-CSharp.dll class: tk2dUIDemoController
using System;
using System.Collections;
using UnityEngine;

[AddComponentMenu("2D Toolkit/Demo/tk2dUIDemoController")]
public class tk2dUIDemoController : tk2dUIBaseDemoController
{
	private void Awake()
	{
		base.ShowWindow(this.window1.transform);
		base.HideWindow(this.window2.transform);
	}

	private void OnEnable()
	{
		this.nextPage.OnClick += this.GoToPage2;
		this.prevPage.OnClick += this.GoToPage1;
	}

	private void OnDisable()
	{
		this.nextPage.OnClick -= this.GoToPage2;
		this.prevPage.OnClick -= this.GoToPage1;
	}

	private void GoToPage1()
	{
		this.timeSincePageStart = 0f;
		base.AnimateHideWindow(this.window2.transform);
		base.AnimateShowWindow(this.window1.transform);
		this.currWindow = this.window1;
	}

	private void GoToPage2()
	{
		this.timeSincePageStart = 0f;
		if (this.currWindow != this.window2)
		{
			this.progressBar.Value = 0f;
			this.currWindow = this.window2;
			base.StartCoroutine(this.MoveProgressBar());
		}
		base.AnimateHideWindow(this.window1.transform);
		base.AnimateShowWindow(this.window2.transform);
	}

	private IEnumerator MoveProgressBar()
	{
		while (this.currWindow == this.window2 && this.progressBar.Value < 1f)
		{
			this.progressBar.Value = this.timeSincePageStart / 2f;
			yield return null;
			this.timeSincePageStart += tk2dUITime.deltaTime;
		}
		while (this.currWindow == this.window2)
		{
			float smoothTime = 0.5f;
			this.progressBar.Value = Mathf.SmoothDamp(this.progressBar.Value, this.slider.Value, ref this.progressBarChaseVelocity, smoothTime, float.PositiveInfinity, tk2dUITime.deltaTime);
			yield return 0;
		}
		yield break;
	}

	public tk2dUIItem nextPage;

	public GameObject window1;

	public tk2dUIItem prevPage;

	public GameObject window2;

	public tk2dUIProgressBar progressBar;

	private float timeSincePageStart;

	private const float TIME_TO_COMPLETE_PROGRESS_BAR = 2f;

	private float progressBarChaseVelocity;

	public tk2dUIScrollbar slider;

	private GameObject currWindow;
}
