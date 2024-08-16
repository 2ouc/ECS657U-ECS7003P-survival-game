using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ShareLink : MonoBehaviour
{
	[SerializeField]
	private string Url;

	public void GameLinkShare()
    {
		StartCoroutine(TakeScreenshotAndShare());
		//FindObjectOfType<MainMenuSound>().BtnSoundPlay();
    }


	
	private IEnumerator TakeScreenshotAndShare()
	{
		yield return new WaitForEndOfFrame();

		//Texture2D ss = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
		//ss.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
		//ss.Apply();

		Texture2D image = Resources.Load("image", typeof(Texture2D)) as Texture2D;

		yield return null;

		string filePath = Path.Combine(Application.temporaryCachePath, "shared img.png");
		//File.WriteAllBytes(filePath, ss.EncodeToPNG());
		File.WriteAllBytes(filePath, image.EncodeToPNG());

		// To avoid memory leaks
		//	DestroyImmediate(image);
		
		
		
		new NativeShare().AddFile(filePath)
			.SetSubject("Subject goes here").SetText("Game Link").SetUrl(Url)
			.SetCallback((result, shareTarget) => Debug.Log("Share result: " + result + ", selected app: " + shareTarget))
			.Share();
		

		// Share on WhatsApp only, if installed (Android only)
		//if( NativeShare.TargetExists( "com.whatsapp" ) )
		//	new NativeShare().AddFile( filePath ).AddTarget( "com.whatsapp" ).Share();
	}
}
