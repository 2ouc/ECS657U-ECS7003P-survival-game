using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.AI;
public class raycastfire : MonoBehaviour {
	public GameObject FireButton;
	public float force = 500f;
	public float damage = 50f;
	public float range = 100f;
	public Image AIM;
	public Image AimEdges;
	public Image RotateImage;
	public LayerMask mask;
	public int projectilecount = 1;
	public float inaccuracy = 0.1f;
	public GameObject lineprefab;
	public Transform currentmuzzle;//leave this empty please
	public GameObject impactnormal;
	public GameObject impactconcrete;
	public GameObject impactwood;
	public GameObject[] impactblood;
	public GameObject impactwater;
	public GameObject impactmetal;
	public GameObject impactglass;
	public GameObject impactmelee;
	public GameObject impactnodecal;
	public GameObject ShootingButton;
	public Transform Raypos;
	public static bool playerDetected;
//	public GameObject Shell;
//	public Transform ShellPosition;
	public GameObject HeadImage;
	public AudioClip HeadShot;
	private AudioSource MyAudioPlayer;
	public GameObject HeadShotCamera;
	public GameObject ShootingCamera;
	public GameObject BloodSplashPrefab;
	RaycastHit hit = new RaycastHit();
	public GameObject Bullet;
	public Transform BulletPosition;
	public GameObject GunCamera;
	//public GameObject BulletCamera;
	public static raycastfire raycastObjects;
	public static bool BulletClonned;
	public static bool GunCameraState;
	private GameObject tempHead;
	//public Transform Player;
	public float DamageMultiplayer;
	public GameObject Player;
	public static bool AlreadyAutoShoot;
	static bool headdetect;
	
	EnemyDamageController controller;
	Vector3 Distance;
	private void Start()
    {
		MyAudioPlayer = GetComponent<AudioSource>();
		

	}
	public void fireMelee()
	{
		Vector3 fwrd = transform.forward;

		Vector3 camUp = transform.up;
		Vector3 camRight = transform.right;

		Vector3 wantedvector = fwrd;
		wantedvector += Random.Range(-.1f, .1f) * camUp + Random.Range(-.1f, .1f) * camRight;
		Ray ray = new Ray(transform.position, wantedvector);

		RaycastHit hit = new RaycastHit();
		if (Physics.Raycast(ray, out hit, 3f, mask))
		{
			Raypos = hit.collider.transform;
			if (hit.rigidbody) hit.rigidbody.AddForceAtPosition(500 * fwrd, hit.point);
			hit.transform.SendMessageUpwards("Damage", 50f, SendMessageOptions.DontRequireReceiver);
			GameObject decal;
			if (hit.transform.tag == "flesh")
			{
				//bodypart bp = hit.transform.GetComponent<bodypart>();
				//if (bp != null) bp.applyBlood( hit.point, ray.direction.normalized);

				decal = Instantiate(impactblood[0], hit.point, Quaternion.FromToRotation(Vector3.up, hit.normal)) as GameObject;
				Debug.Log("Object instatiated");
				decal.transform.localRotation = decal.transform.localRotation * Quaternion.Euler(0, Random.Range(-90, 90), 0);
				decal.transform.parent = hit.transform;
			}
			else
			{
				decal = Instantiate(impactmelee, hit.point, Quaternion.FromToRotation(Vector3.up, hit.normal)) as GameObject;
				decal.transform.localRotation = decal.transform.localRotation * Quaternion.Euler(0, Random.Range(-90, 90), 0);
				decal.transform.parent = hit.transform;
			}
		}
	}
	private void Update()
	{
		
		/* if (GunCameraState==true)
		 {
			 GunCamera.SetActive(true);
		 }*/
		if (Physics.Raycast(transform.position, transform.forward, out hit, 1000f))
		{
			if (hit.collider.CompareTag("Head"))
			{
			//	Debug.Log("Work With Auto Shoot");


			}
			if (hit.collider.CompareTag("flesh") || hit.collider.CompareTag("Body"))
			{
				//AIM.color = Color.red;
				RotateImage.gameObject.GetComponent<Image>().color = Color.red;
				if (PlayerPrefs.GetInt("AutoShoot", 1) == 1)
				{

					iTween.ShakeScale(AimEdges.gameObject, new Vector2(0f, 1f), 0.1f);

					//iTween.RotateTo(AimEdges.gameObject, iTween.Hash("rotation", 90f, iTween.EaseType.easeInOutSine, "time", 1.3f));
					iTween.RotateTo(AimEdges.gameObject, iTween.Hash("x", 180f));
					AimEdges.gameObject.GetComponent<Image>().color = Color.red;


					playerDetected = true;
				}
				else
				{
					playerDetected = false;

				}
			}
			else
			{
				playerDetected = false;
				RotateImage.gameObject.GetComponent<Image>().color = Color.white;
				AIM.color = Color.white;
			}
		}
		else
		{
			playerDetected = false;
			AIM.color = Color.white;
		}
		if (PlayerPrefs.GetInt("AutoShoot") == 1)
		{
			ShootingButton.SetActive(false);


		}
		else
		{

			ShootingButton.SetActive(true);

		}
	}

	public void fire()
	{
		for (int i = 0; i < projectilecount; i++)
		{
			firebullet();
		}
	}

	void firebullet()
	{
		//StartCoroutine(genericShooter.instance.ejectshell(0f));
		
		Vector3 fwrd = transform.forward;

		Vector3 camUp = transform.up;
		Vector3 camRight = transform.right;

		Vector3 wantedvector = fwrd;
		wantedvector += Random.Range(-inaccuracy, inaccuracy) * camUp + Random.Range(-inaccuracy, inaccuracy) * camRight;
		Ray ray = new Ray(transform.position, wantedvector);
		RaycastHit hit = new RaycastHit();

		if (Physics.Raycast(ray, out hit, range, mask))
		{

			if (hit.rigidbody) hit.rigidbody.AddForceAtPosition(force * fwrd, hit.point);

			hit.transform.SendMessageUpwards("Damage", damage, SendMessageOptions.DontRequireReceiver);
			GameObject decal;
			GameObject line;
			GameObject BulletTravel;
			line = Instantiate(lineprefab, transform.position, transform.rotation) as GameObject;
			LineRenderer linerender = line.GetComponent<LineRenderer>();
			linerender.SetPosition(0, currentmuzzle.transform.position);
			linerender.SetPosition(1, hit.point);

			//Debug.Log(hit.transform.gameObject.name, hit.collider.gameObject);
			if (hit.collider.tag == "Untagged")
			{
				//decal = Instantiate(impactnormal, hit.point, Quaternion.FromToRotation(Vector3.up, hit.normal)) as GameObject;
				//decal.transform.localRotation = decal.transform.localRotation * Quaternion.Euler(0, Random.Range(-90, 90), 0);
				//decal.transform.parent = hit.transform;
			}
			else if (hit.collider.tag == "concrete")
			{
				decal = Instantiate(impactconcrete, hit.point, Quaternion.FromToRotation(Vector3.up, hit.normal)) as GameObject;
				decal.transform.localRotation = decal.transform.localRotation * Quaternion.Euler(0, Random.Range(-90, 90), 0);
				decal.transform.parent = hit.transform;
			}
			else if (hit.collider.tag == "nodecal")
			{
				decal = Instantiate(impactnodecal, hit.point, Quaternion.FromToRotation(Vector3.up, hit.normal)) as GameObject;
				decal.transform.localRotation = decal.transform.localRotation * Quaternion.Euler(0, Random.Range(-90, 90), 0);
				decal.transform.parent = hit.transform;
			}
			else if (hit.collider.tag == "metal")

			{
				decal = Instantiate(impactmetal, hit.point, Quaternion.FromToRotation(Vector3.up, hit.normal)) as GameObject;
				decal.transform.localRotation = decal.transform.localRotation * Quaternion.Euler(0, Random.Range(-90, 90), 0);
				decal.transform.parent = hit.transform;
			}
			else if (hit.collider.tag == "wood")
			{
				decal = Instantiate(impactwood, hit.point, Quaternion.FromToRotation(Vector3.up, hit.normal)) as GameObject;
				decal.transform.localRotation = decal.transform.localRotation * Quaternion.Euler(0, Random.Range(-90, 90), 0);
				decal.transform.parent = hit.transform;
			}
			else if (hit.collider.tag == "water")
			{
				decal = Instantiate(impactwater, hit.point, Quaternion.FromToRotation(Vector3.up, hit.normal)) as GameObject;
				decal.transform.localRotation = decal.transform.localRotation * Quaternion.Euler(0, Random.Range(-90, 90), 0);
				decal.transform.parent = hit.transform;
			}
			else if (hit.collider.tag == "glass")
			{
				decal = Instantiate(impactglass, hit.point, Quaternion.FromToRotation(Vector3.up, hit.normal)) as GameObject;
				decal.transform.localRotation = decal.transform.localRotation * Quaternion.Euler(0, Random.Range(-90, 90), 0);
				decal.transform.parent = hit.transform;
			}
			else if (hit.collider.tag == "flesh" || hit.collider.tag == "Body")
			{

			//	GameObject.FindGameObjectWithTag("FireButton").SetActive(false);
				decal = Instantiate(impactblood[1], hit.point, Quaternion.FromToRotation(Vector3.up, hit.normal)) as GameObject;
				decal.transform.localRotation = decal.transform.localRotation * Quaternion.Euler(0, Random.Range(-90, 90), 0);
				decal.transform.parent = hit.transform;
				////Blood Particles////
				GameObject BloodSplashs = GameObject.Instantiate(BloodSplashPrefab, hit.point, Quaternion.LookRotation(hit.normal));
				Destroy(BloodSplashs, 1.2f);
				if (hit.collider.gameObject.GetComponent<BodyPartsDamage>())
					hit.collider.gameObject.GetComponent<BodyPartsDamage>().Damage(damage);//, SendMessageOptions.DontRequireReceiver);
				hit.transform.SendMessage("Damage", damage, SendMessageOptions.DontRequireReceiver);
			}

			//Debug.LogError("Root IS : "+hit.transform.root.name);
			
			if (hit.collider.gameObject.layer == LayerMask.NameToLayer("HeadShotNow") && GameManager.instance.CanHeadShot == true || hit.collider.tag=="Head" && GameManager.instance.CanHeadShot == true)
			{


				gameObject.transform.GetComponent<Camera>().enabled = false;
				tempHead = hit.transform.gameObject;
				GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterController>().enabled = false;
				GameObject.FindGameObjectWithTag("Player").GetComponent<playercontroller>().enabled = false;



			//	Debug.LogError("Gameobject Nameis"+hit.transform.root.gameObject.name);

				if (PlayerPrefs.GetInt("AutoShoot") == 1)
                {
					Debug.Log("Active Back AutoShoot");
					AlreadyAutoShoot = true;

                }
                else
                {
					AlreadyAutoShoot = false;
				}
				PlayerPrefs.SetInt("AutoShoot",0);

				//StartCoroutine(AutoShootActive());
				FireButton.GetComponent<ControlFreak2.TouchButton>().enabled = false;
				hit.collider.gameObject.layer = LayerMask.NameToLayer("HeadShotDone");
				//GameObject.FindGameObjectWithTag("FireButton").SetActive(false);
			//	Debug.LogError("Play HeadShot Animation");
				BulletPosition.transform.LookAt(hit.point);
				//Debug.LogError("Root Of Object name is:"+hit.collider.transform.root);
				//BulletTravel = Instantiate(Bullet, BulletPosition.transform.position, transform.rotation);
				BulletTravel = Instantiate(Bullet, transform.position, transform.rotation);
				Rigidbody BulletBody = BulletTravel.GetComponent<Rigidbody>();
				BulletTravel.transform.rotation = Quaternion.LookRotation(hit.point.normalized);
				Debug.Log("VelocityAding");
				BulletBody.AddForceAtPosition(transform.forward * Time.deltaTime * 400f, hit.point, ForceMode.VelocityChange);
				BulletBody.transform.LookAt(hit.transform.position);
				hit.collider.transform.root.gameObject.GetComponentInChildren<Animator>().enabled = false;
				hit.collider.transform.root.gameObject.GetComponentInChildren<NavMeshAgent>().enabled = false;
				hit.collider.transform.root.gameObject.GetComponentInChildren<EnemyAIController>().enabled = false;


				//	hit.collider.transform.root.gameObject.GetComponent<Rigidbody>().useGravity = false;

				Time.timeScale = 0.6f;
				
				
				StartCoroutine(DestroySprite());
				StartCoroutine(ActiveAutoBack());



            }
            else
            {
				hit.transform.SendMessageUpwards("Damage", 50f, SendMessageOptions.DontRequireReceiver);
			}
            

		}
		//	GameObject Bull = Instantiate(Shell, ShellPosition);
	}
	public IEnumerator DestroySprite()
	{

		MyAudioPlayer.clip = HeadShot;
		MyAudioPlayer.Play();
		if (GameManager.HeadShotVisual == true)
		{
			Debug.Log("BulletClone");
			RaycastHit hit;
			//var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

			/*if (Physics.Raycast(ray, out hit))
			{
				if (hit.rigidbody != null)
				{
					//hit.rigidbody.AddForceAtPosition(ray.direction * 20f, hit.point);

				}
			}*/

			//HeadShotCamera.SetActive(true);
			//ShootingCamera.GetComponent<Camera>().enabled = false;

		}
		GameManager.HeadShotVisual = true;
		yield return new WaitForSeconds(1.5f);
		HeadImage.SetActive(false);

		Debug.Log("HeadShotSoundPlay");

		//Destroy(sprite);
	}
	public IEnumerator ActiveAutoBack()
    {
		tempHead.layer = LayerMask.NameToLayer("HeadShotNow");

		
		
		yield return new WaitForSeconds(4f);
		
		Debug.LogWarning("BackCamera On Miss Head Shot");
		GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterController>().enabled = true;
		GameObject.FindGameObjectWithTag("Player").GetComponent<playercontroller>().enabled = true;
		
			Destroy(GameObject.FindGameObjectWithTag("ActionBullet"));
		GameObject.FindGameObjectWithTag("MainCamera").SetActive(true);
		GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>().enabled=true;
		//if (AlreadyAutoShoot == true)
		//{
		//	GameManager.instance.ShootToggle(true);


		//}
		GameObject.FindGameObjectWithTag("FireButton").GetComponent<ControlFreak2.TouchButton>().enabled = true;
        if (hit.transform.root != null)
        {
			Debug.Log("SetBack To The Head");
			

		}
		
		//raycastfire.raycastObjects.GunCamera.SetActive(true);

		Time.timeScale = 1f;
		
	}
    public static IEnumerator AutoShootActive()
    {
		yield return new WaitForSeconds(3f);
	//	PlayerPrefs.SetInt("AutoShoot", 1);
	}
	public void hideBtn()
    {
		FireButton.SetActive(false);
	}
}
