// dnSpy decompiler from Assembly-CSharp.dll class: Dress_Washing_Main
using System;
using System.Collections;
using UnityEngine;

public class Dress_Washing_Main : MonoBehaviour
{
	private void Start()
	{
		GameManager.Instance.cloth = 1;
		GameManager.Instance.count = 0;
		if (Dress_Washing_Main._inst == null)
		{
			Dress_Washing_Main._inst = this;
		}
	}

	private void Update()
	{
	}

	private IEnumerator Main_Btn()
	{
		yield return new WaitForSeconds(0.1f);
		yield break;
	}

	public IEnumerator door_close_b()
	{
		yield return new WaitForSeconds(0.1f);
		SoundManager.Instance.Celebration_s();
		SoundManager.Instance.Click_s();
		this.door_close.GetComponent<tk2dButton>().enabled = false;
		this.door_close.SetActive(false);
		this.Hand_Cap.SetActive(false);
		this.door_open.SetActive(true);
		Dress_Washing_Main._inst.hand_center.SetActive(true);
		if (GameManager.Instance.cloth == 1)
		{
			Dress_Washing_Main._inst.Box_Center_Black.GetComponent<BoxCollider>().size = new Vector3(1f, 1f, 2f);
		}
		else if (GameManager.Instance.cloth == 2)
		{
			Dress_Washing_Main._inst.Box_Center_Clr.GetComponent<BoxCollider>().size = new Vector3(1f, 1f, 2f);
		}
		Dress_Washing_Main._inst.Machine_Collider.SetActive(true);
		UnityEngine.Debug.Log(GameManager.Instance.cloth);
		yield break;
	}

	private IEnumerator door_open_b()
	{
		yield return new WaitForSeconds(0.1f);
		SoundManager.Instance.Celebration_s();
		SoundManager.Instance.Click_s();
		this.door_open_s.Play();
		this.door_open.GetComponent<tk2dButton>().enabled = false;
		this.door_open.SetActive(false);
		this.Hand_Cap.SetActive(false);
		this.door_close.SetActive(true);
		yield break;
	}

	private IEnumerator Main_Cloth_Btn(int j)
	{
		yield return new WaitForSeconds(0.1f);
		SoundManager.Instance.Click_s();
		this.Btn_BlacK_cloth.GetComponent<tk2dButton>().enabled = false;
		this.Btn_Color_cloth.GetComponent<tk2dButton>().enabled = false;
		yield return new WaitForSeconds(0.1f);
		if (j == 1)
		{
			this.Color_Basket.SetActive(false);
			this.color_Basket_Hand.SetActive(true);
		}
		if (j == 2)
		{
			this.Black_Basket.SetActive(false);
			this.black_Basket_Hand.SetActive(true);
		}
		yield break;
	}

	private IEnumerator On_Off_Btn()
	{
		yield return new WaitForSeconds(0.1f);
		SoundManager.Instance.Click_s();
		SoundManager.Instance.Celebration_s();
		this.on_of_bar_hand.SetActive(false);
		this.On_Off_bar.GetComponent<BoxCollider>().enabled = false;
		this.red_light.SetActive(false);
		this.green_light.SetActive(true);
		Dress_Washing_Main._inst.Machine_Collider.SetActive(false);
		yield return new WaitForSeconds(1.1f);
		this.machine_s.Play();
		this.Machine_part.Play();
		this.Bubble_part.Play();
		iTween.ScaleTo(Dress_Washing_Main._inst.liquid_green, iTween.Hash(new object[]
		{
			"x",
			0f,
			"y",
			0f,
			"time",
			4.0,
			"eastype",
			iTween.EaseType.linear,
			"islocal",
			true
		}));
		iTween.ScaleTo(Dress_Washing_Main._inst.white_powder, iTween.Hash(new object[]
		{
			"x",
			0f,
			"y",
			0f,
			"time",
			4.0,
			"eastype",
			iTween.EaseType.linear,
			"islocal",
			true
		}));
		if (GameManager.Instance.cloth == 1)
		{
			iTween.RotateBy(Dress_Washing_Main._inst.All_Black_cloth_in_machine, iTween.Hash(new object[]
			{
				"z",
				90f,
				"speed",
				50.0,
				"eastype",
				iTween.EaseType.linear,
				"islocal",
				true
			}));
		}
		else if (GameManager.Instance.cloth == 2)
		{
			iTween.RotateBy(Dress_Washing_Main._inst.All_Clr_cloth_in_machine, iTween.Hash(new object[]
			{
				"z",
				90f,
				"speed",
				50.0,
				"eastype",
				iTween.EaseType.linear,
				"islocal",
				true
			}));
		}
		this.washing_Machine_anim.GetComponent<Animator>().enabled = true;
		yield return new WaitForSeconds(10.1f);
		this.washing_Machine_anim.GetComponent<Animator>().enabled = false;
		iTween.ScaleTo(Dress_Washing_Main._inst.washing_Machine_anim, iTween.Hash(new object[]
		{
			"x",
			1f,
			"y",
			1f,
			"time",
			0.1,
			"eastype",
			iTween.EaseType.linear,
			"islocal",
			true
		}));
		iTween.RotateTo(Dress_Washing_Main._inst.washing_Machine_anim, iTween.Hash(new object[]
		{
			"z",
			0f,
			"time",
			0.1,
			"eastype",
			iTween.EaseType.linear,
			"islocal",
			true
		}));
		yield return new WaitForSeconds(1.1f);
		if (GameManager.Instance.cloth == 1)
		{
			iTween.RotateBy(Dress_Washing_Main._inst.All_Black_cloth_in_machine, iTween.Hash(new object[]
			{
				"z",
				90f,
				"speed",
				10.0,
				"eastype",
				iTween.EaseType.linear,
				"islocal",
				true
			}));
		}
		else if (GameManager.Instance.cloth == 2)
		{
			iTween.RotateBy(Dress_Washing_Main._inst.All_Clr_cloth_in_machine, iTween.Hash(new object[]
			{
				"z",
				90f,
				"speed",
				10.0,
				"eastype",
				iTween.EaseType.linear,
				"islocal",
				true
			}));
		}
		yield return new WaitForSeconds(3.1f);
		this.Machine_part.Stop();
		this.Bubble_part.Stop();
		this.machine_s.Stop();
		if (GameManager.Instance.cloth == 1)
		{
			iTween.Stop(Dress_Washing_Main._inst.All_Black_cloth_in_machine);
		}
		else if (GameManager.Instance.cloth == 2)
		{
			iTween.Stop(Dress_Washing_Main._inst.All_Clr_cloth_in_machine);
		}
		Dress_Washing_Main._inst.door_close.SetActive(false);
		Dress_Washing_Main._inst.door_open.SetActive(true);
		if (GameManager.Instance.cloth == 1)
		{
			Dress_Washing_Main._inst.All_Black_cloth_in_machine.GetComponent<BoxCollider>().enabled = true;
			Dress_Washing_Main._inst.All_Black_cloth_in_machine.GetComponent<BoxCollider>().size = new Vector3(2f, 2f, 2f);
		}
		else if (GameManager.Instance.cloth == 2)
		{
			Dress_Washing_Main._inst.All_Clr_cloth_in_machine.GetComponent<BoxCollider>().enabled = true;
			Dress_Washing_Main._inst.All_Clr_cloth_in_machine.GetComponent<BoxCollider>().size = new Vector3(2f, 2f, 2f);
		}
		this.red_light.SetActive(true);
		this.green_light.SetActive(false);
		if (GameManager.Instance.cloth == 1)
		{
			this.Hand_machine_to_black_basket.SetActive(true);
			this.Black_Basket_Collider.SetActive(true);
		}
		else if (GameManager.Instance.cloth == 2)
		{
			this.Hand_machine_to_clr_basket.SetActive(true);
			this.Clr_Basket_Collider.SetActive(true);
		}
		yield break;
	}

	public static Dress_Washing_Main _inst;

	public GameObject Main_Black_Box_Collider;

	public GameObject Main_Clr_Box_Collider;

	public GameObject Btn_BlacK_cloth;

	public GameObject Btn_Color_cloth;

	public GameObject Black_Basket;

	public GameObject Color_Basket;

	public GameObject color_Basket_Hand;

	public GameObject black_Basket_Hand;

	public GameObject hand_center;

	public GameObject Box_Center_Black;

	public GameObject Box_Center_Clr;

	public GameObject Hand_Cap;

	public GameObject Hand_Detergent_1;

	public GameObject Hand_Detergent_2;

	public GameObject on_of_bar_hand;

	public GameObject Hand_machine_to_black_basket;

	public GameObject Hand_machine_to_clr_basket;

	public GameObject door_open;

	public GameObject door_close;

	public GameObject Machine_Collider;

	public GameObject Machine_Collider_clr;

	public GameObject On_Off_bar;

	public GameObject red_light;

	public GameObject green_light;

	public Animator[] detergent_anim;

	public BoxCollider[] detergent_drag;

	public GameObject liquid_green;

	public GameObject white_powder;

	public GameObject washing_Machine_anim;

	public GameObject All_Black_cloth_in_machine;

	public GameObject All_Clr_cloth_in_machine;

	public GameObject[] All_clean_Black_Cloth_In_Basket;

	public GameObject[] All_clean_Clr_Cloth_In_Basket;

	public GameObject Black_Basket_Collider;

	public GameObject Clr_Basket_Collider;

	public ParticleSystem Machine_part;

	public ParticleSystem Bubble_part;

	public AudioSource machine_s;

	public AudioSource door_open_s;

	public AudioSource v_o_put_cloth;

	public AudioSource deter_gent;

	public AudioSource filling_s;
}
