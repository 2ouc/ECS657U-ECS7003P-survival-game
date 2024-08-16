using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class playercontroller : MonoBehaviour {
	public static playercontroller DamageGrenade;
	public bool respondstoInput = true;
	public Transform mycamera;
	private Transform reference;
	public float DeletingValue;
	public float jumpHeight = 2.0f;
	public float jumpinterval = 1.5f;
	private float nextjump = 1.2f;
	public float maxhitpoints = 1000f;
	public float hitpoints = 1000f;
	public float regen = 100f;
	public Text healthtext;
	public Image HealthBar;
	public AudioClip[] hurtsounds;
	public RawImage[] painflashtexture;
	private float alpha;
	public Transform recoilCamera;
	public string healthstring;
	public float gravity = 20.0f;
	public float rotatespeed = 4.0f;
	private float speed;
	public float normalspeed = 4.0f;
	public float runspeed = 8.0f;
	public float crouchspeed = 1.0f;
	public float crouchHeight = 1;
	private bool crouching = false;
	public float normalHeight = 2.0f;
	public float camerahighposition = 1.75f;
	public float cameralowposition = 0.9f;
	private float cameranewpositionY;
	private Vector3 cameranewposition;
	private float cameranextposition;
    public float dampTime = 2.0f;
	public GameObject[] DamageEffect;
	public static bool isTakingDamage;
	public Image DamageColor;
	public static bool AdHealth;
	public static bool GainHealth;
	private float moveAmount;
	public float smoothSpeed = 2.0f;
	public static float HealRemains;
	private Vector3 forward = Vector3.forward;
	private Vector3 moveDirection = Vector3.zero;
	private Vector3 right;
	public static float temp;
	private float movespeed;
	public Vector3 localvelocity;


	public bool climbladder = false;
	public Quaternion ladderRotation;
	public Vector3 ladderposition;
	public Vector3 ladderforward;
	public Vector3 climbdirection;




	public float climbspeed = 2.0f;


	public bool canclimb = true;
	private Vector3 addVector = Vector3.zero;


	public bool running = false;
	public bool canrun = true;

	public AudioSource myAudioSource;
	public AudioSource myAudioSource2;
	public AudioClip walkloop;
	public AudioClip runloop;
	public AudioClip crouchloop;
	public AudioClip climbloop;
	public AudioClip jumpclip;
	public AudioClip landclip;
	Vector3 targetDirection = Vector3.zero;
	//public Transform playermesh;
	//public Vector3 playermeshNormalPosition;
	//public Vector3 playermeshForwardPosition;
	//public Transform playerskinnedmesh;
	private bool canrun2 = true;
	public bool hideselectedweapon = false;
	Vector3 targetVelocity;
	public float falldamage;
	private float airTime;
	public float falltreshold = 2f;
	private bool prevGrounded;
	//public Transform Deadplayer;
	public Transform weaponroot;
	public static playercontroller instance;

	Animator weaponanimator;
	public Transform head;
	Animator headanimator;
	public LayerMask mask;
	CharacterController controller;
	playerrotate rotatescript;
	weaponselector inventory; 
	private float nextcheck;

	bool isRun;
	void Awake ()
	{
		reference = new GameObject().transform;
		weaponanimator = weaponroot.GetComponent<Animator>();
		headanimator = head.GetComponent<Animator>();
		controller = GetComponent<CharacterController>();
		rotatescript = GetComponent<playerrotate>();
		inventory = GetComponent<weaponselector>();	
	}
    private void OnEnable()
    {
		instance = this;
	}

    void Start () 
	{
		speed = normalspeed;
		//painflashtexture[Random.Range(0,2)].CrossFadeAlpha(0f,0f,false);
		cameranextposition = camerahighposition;
		//painflashtexture[Random.Range(0, 2)].gameObject.SetActive(false);
		//painflashtexture[Random.Range(0, 2)].gameObject.SetActive(true);

		Color tempColor = painflashtexture[Random.Range(0, 2)].color;
		tempColor.a = 0f;
		painflashtexture[Random.Range(0, 2)].color = tempColor;
	}

	void Update () 
	{
        if (AdHealth == true) {
			HealthBar.GetComponent<Image>().fillAmount += 0.135f;
			
			Debug.Log("Health Adding");
			//AdHealth = false;
		}
		reference.eulerAngles = new Vector3(0, mycamera.eulerAngles.y, 0);
		forward = reference.forward;
		right = new Vector3(forward.z, 0, -forward.x);
		float hor = ControlFreak2.CF2Input.GetAxisRaw("Horizontal");
		float ver = ControlFreak2.CF2Input.GetAxisRaw("Vertical");

		Vector3 velocity = controller.velocity;
		localvelocity = transform.InverseTransformDirection(velocity);

		bool ismovingforward =localvelocity.z > .5f;
		//Debug.Log(ver);
		if(ver >= .95f)
        {
			speed = runspeed;
			isRun = true;

		}
		else
        {
			speed = normalspeed;
			isRun = false;
		}

		if (climbladder && !controller.isGrounded && canclimb ) 
		{

			//playermesh.transform.localPosition = Vector3.MoveTowards(playermesh.transform.localPosition,playermeshForwardPosition, Time.deltaTime * 2f);

			inventory.hideweapons = true;
			airTime = 0f;


			crouching = false;
			//playerskinnedmesh.GetComponent<Renderer>().material.SetFloat("_cutoff", 0f)
			if ((localvelocity.magnitude / speed) >= 0.1f && PlayerPrefs.GetInt("Sound",1) ==1)
			{
				myAudioSource.clip = climbloop;
				if (!myAudioSource.isPlaying)
				{
					myAudioSource.loop = true;
					myAudioSource.Play();
				}

			}
			else
			{
				myAudioSource.Stop();
			}


			Vector3 wantedPosition = (ladderposition - transform.position);
			if( wantedPosition.magnitude > 0.05f)
			{
				addVector = wantedPosition.normalized;


			}
			else
			{
				addVector = Vector3.zero;
			}
			//meshanimator.SetBool ("climbladder", true);
			//meshanimator.SetFloat("ver", Input.GetAxis("Vertical"));
			rotatescript.climbing = true;
			rotatescript.ladderforward = ladderforward;
			targetDirection = (ver * climbdirection);
			targetDirection = targetDirection.normalized;
			targetDirection += addVector;

			moveDirection = targetDirection * climbspeed;

		} 
		else 
		{

			inventory.hideweapons = false;
			rotatescript.climbing = false;

			targetDirection = (hor * right) + (ver * forward);
			targetDirection = targetDirection.normalized;
			targetVelocity = targetDirection;
			if (controller.isGrounded) 
			{
				
				airTime = 0f;
				
				if(ControlFreak2.CF2Input.GetButtonDown("Crouch")) 
				{ 
					crouchcheck ();

				}
				if (!crouching)
				{   

					canrun = true;
					controller.center = new Vector3(0f,normalHeight / 2f,0f);
					controller.height = normalHeight;
					canrun2 = true;
					cameranextposition = camerahighposition;
					canclimb = true;

				}	
				else if (crouching )
				{

					canrun = false;
					controller.center = new Vector3(0f,crouchHeight / 2f,0f);
					controller.height = crouchHeight;
					canrun2 = false;
					cameranextposition = cameralowposition;
					canclimb = false;

				}
				// Jump
				if (ControlFreak2.CF2Input.GetButton ("Jump") && Time.time > nextjump)
				{
					nextjump = Time.time + jumpinterval;
					moveDirection.y = jumpHeight;
					if(PlayerPrefs.GetInt("Sound", 1) == 1)
						myAudioSource.PlayOneShot(jumpclip);
					weaponanimator.SetBool("jump",true);
					headanimator.SetBool("jump",true);

					if (crouching)
					{
						crouchcheck ();
					}
				} 
				else 
				{
					weaponanimator.SetBool("jump",false);
					headanimator.SetBool("jump",false);
				}
				if ((localvelocity.magnitude/speed) >= 0.1f)
				{

					if (speed == runspeed)
					{
						if (myAudioSource.clip == walkloop || myAudioSource.clip == crouchloop)
						{
							myAudioSource.clip = runloop;
						}
						
					}
					else if (speed == crouchspeed)
					{
						if (myAudioSource.clip == walkloop || myAudioSource.clip == runloop)
						
						{
							myAudioSource.clip = crouchloop;
						}
					}
					else
					{
							
						myAudioSource.clip = walkloop;	
					}

					if (!myAudioSource.isPlaying && PlayerPrefs.GetInt("Sound", 1) == 1)
					{
						myAudioSource.loop = true;
						myAudioSource.Play();
					}

				}
				else
				{
					myAudioSource.Pause();
				}
			}

			else 
			{
				
				airTime += Time.deltaTime;
				moveDirection.y -= (gravity) * Time.deltaTime;
				nextjump = Time.time + jumpinterval;
				myAudioSource.Pause();
				
			}

			if (/*ControlFreak2.CF2Input.GetButton ("Sprint")*/ isRun && !crouching) //&& canrun && canrun2 && ismovingforward
			{
				speed = runspeed;
				running = true;
				
			}
			else if(crouching)
			{
				speed = crouchspeed;
				running = false;
			}
			else
			{
				speed = normalspeed;
				running = false;
				
				
			}
			if (respondstoInput)
			{
				targetVelocity *= speed;
				moveDirection.z = targetVelocity.z;
				moveDirection.x = targetVelocity.x;
			}
			else
			{
				moveDirection.z = 0;
				moveDirection.x = 0;
			}

		}

		if (hitpoints <= 0)
		{
			//die
			//Instantiate(Deadplayer, transform.position, transform.rotation);

			GameManager.instance.LevelFail();
			respondstoInput = false;
			//Destroy(gameObject);
		}
		
		cameranewpositionY = Mathf.Lerp(Camera.main.transform.localPosition.y,cameranextposition, Time.deltaTime * 4f);

		weaponanimator.SetBool ("grounded", controller.isGrounded);
		weaponanimator.SetFloat("speed",(localvelocity.magnitude), dampTime , .1f);
		headanimator.SetBool ("grounded", controller.isGrounded);
		headanimator.SetFloat("speed",(localvelocity.magnitude), dampTime , .1f);

		cameranewposition = new Vector3(Camera.main.transform.localPosition.x,cameranewpositionY,Camera.main.transform.localPosition.z);
		Camera.main.transform.localPosition = cameranewposition;


		controller.Move (moveDirection * Time.deltaTime);
		if (!prevGrounded && controller.isGrounded )
		{
			
			//doland
			if (airTime > falltreshold)
			{
				Damage(falldamage * airTime * 2f);
			}

			if (!myAudioSource.isPlaying && Time.time > nextcheck && PlayerPrefs.GetInt("Sound", 1) == 1)
			{
				myAudioSource2.PlayOneShot(landclip);

			}
			nextcheck = Time.time + 0.8f;	
				
		}
		else if (prevGrounded && !controller.isGrounded)
		{
			//dojump
			if(PlayerPrefs.GetInt("Sound", 1) == 1)
				myAudioSource2.PlayOneShot(jumpclip);

		}
		prevGrounded = controller.isGrounded;
		if (hitpoints < maxhitpoints)
		{
			hitpoints += regen * Time.deltaTime;
			 temp= Mathf.Round((hitpoints / maxhitpoints) * 100f);
			healthstring = (Mathf.Round((hitpoints / maxhitpoints) * 100f)).ToString();
			healthtext.text = (healthstring);
			////This is health bar Value ////
			HealthBar.fillAmount = temp/100;
		   // HealRemains = float.Parse(healthtext.text);
			DeletingValue = 100f - HealRemains/100f;
			////////////////////////Debug Hit Points////////////////////////
			//Debug.Log("Deleting Value is:"+ hitpoints);
			//float alpha = (hitpoints / 100f);
		}
        if (isTakingDamage == true)
        {
			
				DamageColor.gameObject.SetActive(true);
				DamageColor.GetComponent<Image>().color = new Color(0.9f, 0f, 0f, 0.3f);
			
        }
        else
        {
			//Debug.Log("NotDamaging");
			DamageColor.color = Color.Lerp(DamageColor.color,Color.clear,1.8f*Time.deltaTime);
        }
	}
	IEnumerator DamageEffectWait()
    {
		
		GameObject tempColor = DamageEffect[Random.Range(0, 2)];
        for (float i = 0; i < 1f; i+=Time.deltaTime)
        {
			yield return null;
			tempColor.GetComponent<RawImage>().color = Color.red;
			//DamageEffect[Random.Range(0, 2)].GetComponent<RawImage>().color = tempColor;

		}
		
		DamageEffect[Random.Range(0, 2)].GetComponent<RawImage>().color = Color.red;

	}
	public void Damage (float damage) 
	{
		//Debug.Log("Value To Delete : "+DeletingValue);
		camerarotate cameracontroller = recoilCamera.GetComponent<camerarotate>();
		GameObject PowerOfPlayer = GameObject.FindGameObjectWithTag("HealthBar");

	//	Debug.Log("Show Damage Effect");
		//isTakingDamage = true;
		StopCoroutine(DamageEffectWait());
		//StartCoroutine(DamageEffectWait());

		cameracontroller.SendMessage("dorecoil", damage/3f,SendMessageOptions.DontRequireReceiver);
		if (!myAudioSource.isPlaying && hitpoints >= 0 && PlayerPrefs.GetInt("Sound", 1) == 1)
		{
			int n = Random.Range(1,hurtsounds.Length);
			myAudioSource2.clip = hurtsounds[n];
			isTakingDamage = true;
			StartCoroutine(DamageTrue());
			myAudioSource2.pitch = 0.9f + 0.1f *Random.value;
			if(PlayerPrefs.GetInt("Sound", 1) == 1)
				myAudioSource2.Play();
			hurtsounds[n] = hurtsounds[0];
			hurtsounds[0] = myAudioSource2.clip;
        }
        else
        {
			//isTakingDamage = false;
		}
		//damaged = true;

		hitpoints = hitpoints - damage;
	}
	void crouchcheck()
	{
		//check ceiling!
		Ray heightray = new Ray (transform.position, Vector3.up);
		RaycastHit ceilinghit = new RaycastHit();

		if (!Physics.Raycast (heightray, out ceilinghit, 2.2f, mask)) 
		{
			crouching = !crouching;

		}
	}
	IEnumerator DamageTrue()
    {
		
		yield return new WaitForSeconds(2f);
		isTakingDamage = false;
	}
	/// <summary>
	/// /////////////Reward Recieved///////////////////
	/// </summary>
	public void HealthReceived()
	{


		Debug.Log("Hit Point Before" + playercontroller.instance.hitpoints);
		if (playercontroller.instance.hitpoints < 80f && playercontroller.instance.hitpoints > 0 || HealthBar.fillAmount<=0.8)
		{
			playercontroller.instance.hitpoints += 10f;

			//HealthBar.fillAmount = 10000 / playercontroller.instance.hitpoints;
			HealthBar.fillAmount = playercontroller.instance.hitpoints / 10000;
			healthstring = (Mathf.Round((hitpoints / maxhitpoints) * 100f)).ToString();
			//Destroy(ga);
			//   Debug.Log()

		}
		else if (playercontroller.instance.hitpoints >= 80f)
		{
			playercontroller.instance.hitpoints += 10000f;
			healthstring = (Mathf.Round((hitpoints / maxhitpoints) * 100f)).ToString();
			HealthBar.fillAmount = 10000 / playercontroller.instance.hitpoints;
			playercontroller.instance.hitpoints += 10f;


		}
		Debug.Log("Hit Point After" + playercontroller.instance.hitpoints);



		Debug.Log("HealthCollected");
		//AddHealth = false;

	}
	public void CashReward()
    {
		//PlayerPrefs.SetInt("TotalCash",PlayerPrefs.GetInt("TotalCash")+1000);
		Debug.Log("Set Enemy Kill Reward");

	}



}