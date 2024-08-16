
using UnityEngine;

public class SoundController : MonoBehaviour
{
    AudioSource thisSource;
    // Start is called before the first frame update
    void OnEnable()
    {
        thisSource = gameObject.GetComponent<AudioSource>();

        if(PlayerPrefs.GetInt("Sound",1) == 1)
        {
            thisSource.Play();
        }
        else
        {
            thisSource.Stop();
        }
    }
}
