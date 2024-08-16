using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public float damage;
    // Start is called before the first frame update
    void Start()
    {
        if(PlayerPrefs.GetInt("Sound", 1) != 1)
        {
            GetComponent<AudioSource>().Stop();
        }

        Destroy(gameObject,2f);
    }

    // Update is called once per frame
    void OnCollisionEnter(Collision col)
    {
        //Debug.Log(col.gameObject.tag);

        if (col.gameObject.CompareTag("Player"))
        {
            col.transform.SendMessageUpwards("Damage", damage, SendMessageOptions.DontRequireReceiver);
            
        }
        Destroy(gameObject);
    }
}
