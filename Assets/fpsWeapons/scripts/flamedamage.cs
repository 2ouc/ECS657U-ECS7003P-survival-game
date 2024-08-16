using UnityEngine;
using System.Collections;

public class flamedamage : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void OnParticleCollision (GameObject other ) {
		//Debug.Log(other.name);
		other.SendMessage("Damage", 5f,SendMessageOptions.DontRequireReceiver);
        if (other.GetComponent<EnemyDamageController>())
        {
			other.GetComponent<EnemyDamageController>().DamageMain(5f);

		}

	}
}
