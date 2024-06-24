// dnSpy decompiler from Assembly-CSharp.dll class: tk2dUIDemo3Controller
using System;
using System.Collections;
using UnityEngine;

public class tk2dUIDemo3Controller : tk2dUIBaseDemoController
{
	private IEnumerator Start()
	{
		this.overlayRestPosition = this.overlayInterface.position;
		this.HideOverlay();
		Vector3 instructionsRestPos = this.instructions.position;
		this.instructions.position = this.instructions.position + this.instructions.up * 10f;
		base.StartCoroutine(base.coMove(this.instructions, instructionsRestPos, 1f));
		yield return new WaitForSeconds(3f);
		base.StartCoroutine(base.coMove(this.instructions, instructionsRestPos - this.instructions.up * 10f, 1f));
		yield break;
	}

	public void ToggleCase(tk2dUIToggleButton button)
	{
		float xAngle = (float)((!button.IsOn) ? 0 : -66);
		base.StartCoroutine(base.coTweenAngle(button.transform, xAngle, 0.5f));
	}

	private IEnumerator coRedButtonPressed()
	{
		base.StartCoroutine(base.coShake(this.perspectiveCamera, Vector3.one, Vector3.one, 1f));
		yield return new WaitForSeconds(0.3f);
		this.ShowOverlay();
		yield break;
	}

	private void ShowOverlay()
	{
		this.overlayInterface.gameObject.SetActive(true);
		Vector3 position = this.overlayRestPosition;
		position.y = -2.5f;
		this.overlayInterface.position = position;
		base.StartCoroutine(base.coMove(this.overlayInterface, this.overlayRestPosition, 0.15f));
	}

	private IEnumerator coHideOverlay()
	{
		Vector3 v = this.overlayRestPosition;
		v.y = -2.5f;
		yield return base.StartCoroutine(base.coMove(this.overlayInterface, v, 0.15f));
		this.HideOverlay();
		yield break;
	}

	private void HideOverlay()
	{
		this.overlayInterface.gameObject.SetActive(false);
	}

	public Transform perspectiveCamera;

	public Transform overlayInterface;

	private Vector3 overlayRestPosition = Vector3.zero;

	public Transform instructions;
}
