// dnSpy decompiler from Assembly-CSharp.dll class: GeneralScript
using System;
using System.Collections;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class GeneralScript : MonoBehaviour
{
	public void Awake()
	{
		try
		{
			if (!(GeneralScript._instance != null) || !(GeneralScript._instance != this))
			{
				GeneralScript._instance = this;
			}
		}
		catch (Exception ex)
		{
		}
	}

	public void Start()
	{
		this.isExceptionHandlingSetup = false;
		this.SetupExceptionHandling();
	}

	public void TakeScreenShot(GameObject objectToTempHide = null, GameObject objectToShowOnScreenShot = null)
	{
		this.currentDateTime = DateTime.Now;
		this.fileNameByDate = string.Concat(new string[]
		{
			this.currentDateTime.Year.ToString(),
			this.currentDateTime.Month.ToString(),
			this.currentDateTime.Day.ToString(),
			"_",
			this.currentDateTime.Hour.ToString(),
			this.currentDateTime.Minute.ToString(),
			this.currentDateTime.Second.ToString()
		});
		base.StartCoroutine(this.ScreenShot(objectToTempHide, objectToShowOnScreenShot));
	}

	private IEnumerator ScreenShot(GameObject objectToTempHide, GameObject objectToShowOnScreenShot)
	{
		if (objectToTempHide != null)
		{
			objectToTempHide.SetActive(false);
		}
		if (objectToShowOnScreenShot != null)
		{
			objectToShowOnScreenShot.SetActive(true);
		}
		yield return new WaitForEndOfFrame();
		this.textureToSave = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
		this.textureToSave.ReadPixels(new Rect(0f, 0f, (float)Screen.width, (float)Screen.height), 0, 0);
		yield return new WaitForEndOfFrame();
		this.textureToSave.Apply();
		if (objectToTempHide != null)
		{
			objectToTempHide.SetActive(true);
		}
		if (objectToShowOnScreenShot != null)
		{
			objectToShowOnScreenShot.SetActive(false);
		}
		this.SaveImageForSharing(this.textureToSave, "GameDistrict", "TempImages" + this.fileNameByDate);
		yield break;
	}

	private void SaveImageForSharing(Texture2D texture, string directoryName, string pictureName)
	{
		if (!Directory.Exists(Application.persistentDataPath + "/" + directoryName))
		{
			Directory.CreateDirectory(Application.persistentDataPath + "/" + directoryName);
		}
		if (File.Exists(string.Concat(new string[]
		{
			Application.persistentDataPath,
			"/",
			directoryName,
			"/",
			pictureName
		})))
		{
			File.Delete(string.Concat(new string[]
			{
				Application.persistentDataPath,
				"/",
				directoryName,
				"/",
				pictureName
			}));
		}
		byte[] bytes = texture.EncodeToPNG();
		File.WriteAllBytes(string.Concat(new string[]
		{
			Application.persistentDataPath,
			"/",
			directoryName,
			"/",
			pictureName,
			".png"
		}), bytes);
		this.imageFinalPath = string.Concat(new string[]
		{
			Application.persistentDataPath,
			"/",
			directoryName,
			"/",
			pictureName,
			".png"
		});
		this.ShareImage();
	}

    private void ShareImage()
    {
    }

	public void SetupExceptionHandling()
	{
		if (!this.isExceptionHandlingSetup)
		{
			this.isExceptionHandlingSetup = true;
			Application.RegisterLogCallback(new Application.LogCallback(this.HandleException));
		}
	}

	private void HandleException(string condition, string stackTrace, LogType type)
	{
		if (type == LogType.Exception)
		{
		}
	}

	public void SendExceptionEmail(string exception, string function = "function not set")
	{
		this.SendEmail(function + "\n" + exception);
	}

	public void SendEmail(string Exception)
	{
		string str = string.Concat(new object[]
		{
			SystemInfo.deviceModel,
			": deviceModel\n",
			SystemInfo.deviceName,
			" : deviceName\n",
			SystemInfo.deviceType,
			": deviceType\n",
			SystemInfo.deviceUniqueIdentifier,
			"\n",
			SystemInfo.operatingSystem,
			": operatingSystem\n",
			SystemInfo.systemMemorySize,
			" : systemMemorySize\n",
			SystemInfo.processorCount,
			" : processorCount\n",
			SystemInfo.processorType,
			": processorType\n",
			Screen.currentResolution.width,
			" : currentResolution.width\n",
			Screen.currentResolution.height,
			" : currentResolution.height\n",
			Screen.dpi,
			"\n",
			Screen.fullScreen,
			"\n",
			SystemInfo.graphicsDeviceName,
			" : graphicsDeviceName\n",
			SystemInfo.graphicsDeviceVendor,
			" : graphicsDeviceVendor\n",
			SystemInfo.graphicsMemorySize,
			" : graphicsDeviceVendor\n",
			SystemInfo.maxTextureSize
		});
		base.StartCoroutine(this.Sending(Exception + "\n" + str));
	}

	private IEnumerator Sending(string errorMessage)
	{
		MailMessage mail = new MailMessage();
		mail.From = new MailAddress("happymelongamesfabric@gmail.com");
		mail.To.Add("happymelongamesfabric@gmail.com");
		mail.Subject = "Mom And Baby Washing Laundry Cloth";
		mail.Body = errorMessage;
		SmtpClient smtpServer = new SmtpClient("smtp.gmail.com");
		smtpServer.Port = 587;
		smtpServer.Credentials = (new NetworkCredential("happymelongamesfabric@gmail.com", "happyMelongames@") as ICredentialsByHost);
		smtpServer.EnableSsl = true;
		ServicePointManager.ServerCertificateValidationCallback = ((object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) => true);
		smtpServer.Send(mail);
		yield return null;
		yield break;
	}

	public static GeneralScript _instance;


	private Texture2D textureToSave;

	private Vector2 previewImageSize;

	private DateTime currentDateTime;

	private string fileNameByDate;

	private string fileName;

	private string imageFinalPath;

	private bool isExceptionHandlingSetup;
}
