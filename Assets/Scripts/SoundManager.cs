// dnSpy decompiler from Assembly-CSharp.dll class: SoundManager
using System;
using UnityEngine;
using YG;

public class SoundManager : MonoBehaviour
{
	public static SoundManager Instance { get; private set; }

	private void Awake()
	{
		if (SoundManager.Instance == null)
		{
			SoundManager.Instance = this;
			UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
		}
		else
		{
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	private void Start()
	{
        if (YandexGame.savesData.isSoundOn == 1)
		{
			GetComponent<AudioSource>().enabled = true;
		}
		else
		{
            GetComponent<AudioSource>().enabled = false;
        }
            this.Controller = base.GetComponent<AudioSource>();
	}


	public void superb_part()
	{
		this.superb_partticle.Play();
	}

	public void Click_s()
	{
		int num = UnityEngine.Random.Range(0, this.clickSound.Length);
		this.Controller.clip = this.clickSound[num];
		UnityEngine.Debug.Log(this.clickSound[num]);
		this.Controller.Play();
	}

	public void Part_s()
	{
		int num = UnityEngine.Random.Range(0, this.particleSounds.Length);
		this.Controller.clip = this.particleSounds[num];
		this.Controller.Play();
	}

	public void Celebration_s()
	{
		
	}

	public void StopSound()
	{
		this.Controller.Stop();
		this.Controller.clip = null;
	}

	public void b_light_on()
	{
		this.Controller.clip = this.light_on;
		this.Controller.Play();
	}

	public void b_light_off()
	{
		this.Controller.clip = this.light_off;
		this.Controller.Play();
	}

	public void v_o_blower()
	{
		this.Controller.clip = this.vo_blowr;
		this.Controller.Play();
	}

	public void v_o_boom()
	{
		this.Controller.clip = this.vo_boom;
		this.Controller.Play();
	}

	public void v_o_dust_remover()
	{
		this.Controller.clip = this.vo_dust_remover;
		this.Controller.Play();
	}

	public void v_o_dust_paint_brush()
	{
		this.Controller.clip = this.vo_paintbrush;
		this.Controller.Play();
	}

	public void v_o_shower()
	{
		this.Controller.clip = this.vo_shower;
		this.Controller.Play();
	}

	public void v_o_spider()
	{
		this.Controller.clip = this.vo_spider_boom;
		this.Controller.Play();
	}

	public void v_o_spounge()
	{
		this.Controller.clip = this.vo_spounge;
		this.Controller.Play();
	}

	public void v_o_viper()
	{
		this.Controller.clip = this.vo_viper;
		this.Controller.Play();
	}

	public void v_o_pluck_leaves()
	{
		this.Controller.clip = this.vo_pluck_leaves;
		this.Controller.Play();
	}

	public void v_o_put_strawberry()
	{
		this.Controller.clip = this.vo_put_strawberry;
		this.Controller.Play();
	}

	public AudioClip[] clickSound;

	public AudioClip[] particleSounds;


	private AudioSource Controller;

	public AudioClip TelePhone;

	public AudioClip printer;

	public AudioClip light_on;

	public AudioClip light_off;

	public AudioClip vo_blowr;

	public AudioClip vo_boom;

	public AudioClip vo_dust_remover;

	public AudioClip vo_paintbrush;

	public AudioClip vo_shower;

	public AudioClip vo_spider_boom;

	public AudioClip vo_spounge;

	public AudioClip vo_viper;

	public AudioClip vo_pluck_leaves;

	public AudioClip vo_put_strawberry;

	public ParticleSystem superb_partticle;
}
