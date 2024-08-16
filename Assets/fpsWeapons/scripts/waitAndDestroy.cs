using UnityEngine;
using System.Collections;

public class waitAndDestroy : MonoBehaviour {
	public float waitTime = 5f;
	// Use this for initialization
	void Start () 
	{
        if (GetComponent<AudioSource>())
        {
			if (PlayerPrefs.GetInt("Sound", 1) != 1)
            {
				GetComponent<AudioSource>().Stop();
            }

		}
		Destroy (gameObject, waitTime);
	
	}

}
