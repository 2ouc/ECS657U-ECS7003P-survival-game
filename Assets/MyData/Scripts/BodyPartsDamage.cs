using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyPartsDamage : MonoBehaviour
{
    public float DamageMultiplayer;
    // public GameObject WCamera;
    EnemyDamageController controller;
    
   // public GameObject Camera;
    private void Start()
    {
        controller = gameObject.GetComponentInParent<EnemyDamageController>();
    }
    public void Damage(float damage)
    {
        if (gameObject.CompareTag("Head"))
        {
            //  EnemyDamageController.isHeadSot = true;
        }
        else
        {
            controller.DamageMain(50f * DamageMultiplayer);
        }

        //Debug.Log(gameObject.tag);

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("ActionBullet"))
        {
            Debug.Log("PlayerShould Die");
            
          //  GameObject.FindGameObjectWithTag("MainCamera").GetComponent<raycastfire>().enabled = true;
            ///  EnemyDamageController.DamageData.Health = 0f;
            //hit.transform.SendMessageUpwards ("Damage",damage, SendMessageOptions.DontRequireReceiver);
            //raycastfire.HeadShotComplete = true;


            Destroy(GameObject.FindGameObjectWithTag("ActionBullet"));
         
            
            //raycastfire.raycastObjects.GunCamera.SetActive(true);
            GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>().enabled=true;
            GameObject.FindGameObjectWithTag("Player").GetComponent<playercontroller>().enabled = true;
            Time.timeScale = 1f;
            raycastfire.GunCameraState = true;
            EnemyDamageController.isHeadSot = true;
            GameObject.FindGameObjectWithTag("MainCamera").GetComponent<raycastfire>().enabled = true;
            controller.DamageMain(500f);
            //PlayerPrefs.SetInt("AutoShoot", 1);
            GameObject.FindGameObjectWithTag("FireButton").GetComponent<ControlFreak2.TouchButton>().enabled = true;

            StartCoroutine(raycastfire.AutoShootActive());
            StartCoroutine(falseBulletClonned());


        }
        else
        {
            controller.DamageMain(10f * DamageMultiplayer);
        }
    }
    public IEnumerator falseBulletClonned()
    {
        
        yield return new WaitForSeconds(0.8f);
        GameObject.FindGameObjectWithTag("FireButton").GetComponent<ControlFreak2.TouchButton>().gameObject.SetActive(true);
        raycastfire.BulletClonned = false;

    }
}
