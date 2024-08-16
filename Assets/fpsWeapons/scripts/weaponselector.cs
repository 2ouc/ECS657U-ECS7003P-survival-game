using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class weaponselector : MonoBehaviour 
{
	
	public Transform[] GamePlayGuns;
	public bool[] HaveWeapons;
	public int[] WeaponsAmmo;
	public int grenade;
	public float lastGrenade;
	public float selectInterval = 2f;
	private float nextselect = 2f;

	private int previousWeapon = 0;
	public  AudioClip switchsound;
	public AudioSource myaudioSource;
	public bool canswitch;

	public bool hideweapons = false;
	bool oldhideweapons = false;
	public Text ammotext;
	public Text grenadetext;
	public int currentammo = 10;

	public GameObject AIM;
	public RectTransform mycanvas;

	public AudioClip uiswitchsound;
	public AudioClip uiswitchsound2;
	public AudioSource myaudio;
	
	public Sprite[]weaponsprites;
	public GameObject weaponspriteprefab;
	private int currentweapon;
	
	private int weaponIndex;
	
	int temp = 0;


	
	
	void Awake()
	{

		

		//for(int i = 0; i < GamePlayGuns.Length; i++){
		//	GamePlayGuns[i].gameObject.SetActive(false);
		//}

		grenade=10;
		currentweapon = 0;
		canswitch = true;
		
		if(WeaponsAmmo.Length==0) WeaponsAmmo=new int[GamePlayGuns.Length];

		//for test ONLY, to enable all weapons
		HaveWeapons=new bool[GamePlayGuns.Length];
		HaveWeapons[0]=true; // Knife
		for (int i=0;i< GamePlayGuns.Length;i++){
			HaveWeapons[i]=true;
		}
	}

	void Start()
	{
		
			StartCoroutine(selectWeaponCustom(PlayerPrefs.GetInt("SelectedGun")));
		
		
        //StartCoroutine(selectWeaponCustom(7));

    }
	void Update()
	{
		
		if (WeaponsAmmo[currentweapon] == -1)
			ammotext.text = "";
		else
			ammotext.text = (currentammo + " / " + WeaponsAmmo[currentweapon]);

		grenadetext.text = grenade.ToString();

		if (hideweapons!= oldhideweapons)
		{
			if(hideweapons)
			{
				StartCoroutine(hidecurrentWeapon(weaponIndex));
			}
			else
			{
				StartCoroutine(unhidecurrentWeapon(weaponIndex));
			}
		}
	}
	
	public void playSwithSound(){
		myaudioSource.PlayOneShot(switchsound, 1);
	}

	public void PickAmmo(int weaponNumber,int amountAmmo){
		WeaponsAmmo[weaponNumber]+=amountAmmo;
		if(weaponNumber==currentweapon)
			GamePlayGuns[weaponNumber].gameObject.BroadcastMessage("pickAmmo",WeaponsAmmo[weaponNumber]);
	}
	public void PickUpWeapon(int index)
    {
		if(currentweapon != index)
        {
			StartCoroutine(selectWeaponCustom(index));
        }
        else
        {
			PickAmmo(index, 60);
		}
    }

	public void InitCurrentWeaponAmmo(int amountAmmo){
		if(WeaponsAmmo.Length==0)WeaponsAmmo=new int[GamePlayGuns.Length];
		//Debug.Log("currentWeapon="+currentWeapon);
		WeaponsAmmo[currentweapon]+=amountAmmo;
	}

	public void UpdateCurrentWeaponAmmo(int amountAmmo){
		WeaponsAmmo[currentweapon]=amountAmmo;
	}
	//public 
	IEnumerator hidecurrentWeapon(int index)
	{
		GamePlayGuns[index].gameObject.BroadcastMessage("doRetract",SendMessageOptions.DontRequireReceiver);
		yield return new WaitForSeconds (0.15f);
		GamePlayGuns[index].gameObject.SetActive(false);
		oldhideweapons = hideweapons;

	}

	IEnumerator unhidecurrentWeapon(int index)
	{
		yield return new WaitForSeconds (0.15f);
		GamePlayGuns[index].gameObject.SetActive(true);
		GamePlayGuns[index].gameObject.BroadcastMessage("doNormal",SendMessageOptions.DontRequireReceiver);
		oldhideweapons = hideweapons;
	}

	IEnumerator selectWeapon(int index)
	{
		GamePlayGuns[previousWeapon].gameObject.BroadcastMessage("doRetract",SendMessageOptions.DontRequireReceiver);
		yield return new WaitForSeconds (0.5f);
		GamePlayGuns[previousWeapon].gameObject.SetActive(false);
		GamePlayGuns[index].gameObject.SetActive(true);
		GamePlayGuns[index].gameObject.BroadcastMessage("doNormal",SendMessageOptions.DontRequireReceiver);
	}
	IEnumerator selectWeaponCustom(int index)
	{

		GamePlayGuns[previousWeapon].gameObject.BroadcastMessage("doRetract", SendMessageOptions.DontRequireReceiver);
		yield return new WaitForSeconds(0.5f);
		GamePlayGuns[previousWeapon].gameObject.SetActive(false);

		GamePlayGuns[index % GamePlayGuns.Length].gameObject.SetActive(true);
		GamePlayGuns[index % GamePlayGuns.Length].gameObject.BroadcastMessage("doNormal", SendMessageOptions.DontRequireReceiver);
		previousWeapon = index;
		currentweapon = index;
		//Weapons[index].gameObject.BroadcastMessage("pickAmmo", WeaponsAmmo[index]);

	}

	public void ChangeGunCustomNext()
    {
		temp++;

		if(temp == GamePlayGuns.Length)
        {
			temp = 0;
		}
		StartCoroutine(selectWeaponCustom(temp));
    }
	public void ChangeGunCustomPrevious()
    {
		temp--;

		if (temp < 0)
		{
			temp = GamePlayGuns.Length-1;
		}
		StartCoroutine(selectWeaponCustom(temp));
    }

	public void showAIM(bool show){
		if(show)
			AIM.SetActive(true);
		else
			AIM.SetActive(false);
	}
}



	
