using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class explosionForce : MonoBehaviour {
	public float radius = 5.0f;
	public float power = 200.0f;
	public float waitTime = 5.0f;
	public float damage = 150f;
	private AudioSource myaudio;
	public AudioClip[] explodeSounds; 
	public Vector3 myrotation;
	
	private void OnEnable()
    {
  //      if (GetComponent<AudioSource>())
  //      {
		//	if (PlayerPrefs.GetInt("Sound", 1) == 0)
		//		GetComponent<AudioSource>().Stop();

		//}
    }
    // Use this for initialization
    void Start () {
		transform.rotation = Quaternion.Euler(myrotation);
		myaudio = GetComponent<AudioSource>();

		Destroy (gameObject, waitTime);
		int n = Random.Range(1,explodeSounds.Length);
		myaudio.clip = explodeSounds[n];
		myaudio.pitch = 0.9f + 0.1f *Random.value;

		if (PlayerPrefs.GetInt("Sound", 1) == 1)
			myaudio.PlayOneShot(myaudio.clip);
		
		explodeSounds[n] = explodeSounds[0];
		explodeSounds[0] = myaudio.clip;
		Vector3 explosionPos = transform.position;
		float dist = Vector3.Distance(explosionPos, GameObject.FindGameObjectWithTag("Player").transform.position);
		Collider[] colliders = Physics.OverlapSphere(explosionPos, radius);
		foreach (Collider hit in colliders) 
		{
			if (hit.GetComponent<Rigidbody>() != null)
			{
				Rigidbody rb = hit.GetComponent<Rigidbody>();
				rb.AddExplosionForce(power, explosionPos, radius, 3.0f);
				GameObject PowerOfPlayer = GameObject.FindGameObjectWithTag("HealthBar");
				if (dist < 5f)
				{
										PowerOfPlayer.gameObject.GetComponent<Image>().fillAmount = PowerOfPlayer.gameObject.GetComponent<Image>().fillAmount - 0.00005f;
					
				}
			}
            if (!hit.CompareTag("Player"))
            {
				hit.transform.SendMessageUpwards ("Damage",damage, SendMessageOptions.DontRequireReceiver);
			
			}
		}
	
	}
	

}
