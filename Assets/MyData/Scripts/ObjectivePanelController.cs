using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectivePanelController : MonoBehaviour {

	public GameObject ObjectivePanel,OkBtn;
	public Text ObjText;
	public AudioClip inPositionClip;
	public AudioSource thisSource;
	
	public static ObjectivePanelController instance;
	// Use this for initialization
	void OnEnable () {
		instance = this;
	}

	public void ShowObjectivePanel(string Message)
	{
		
		ObjectivePanel.SetActive (true);
		StartCoroutine (ObjMesssageWait(Message));
	}
	IEnumerator ObjMesssageWait(string Message)
	{
		ObjText.text = "";
		char[] tempMessage = Message.ToCharArray ();

		//CharacterWait = (CharacterWait / tempMessage.Length)/1.5f;
//		Debug.Log (CharacterWait);
		for (int i = 0; i < tempMessage.Length; i++) {
			ObjText.text += tempMessage [i];
			yield return null;
		}
		OkBtn.SetActive (true);
		Time.timeScale = 0;
	}
	public void OK()
	{
		Time.timeScale = 1f;
		if(PlayerPrefs.GetInt("Sound",1) == 1)
        {
			thisSource.PlayOneShot(inPositionClip);
        }

		//GameManager.isAim = true;
		OkBtn.SetActive (false);
        //TimerController.instance.StartTimer(180f);
        ObjectivePanel.SetActive (false);

	}
}
