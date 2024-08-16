using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public enum PickUpType
{
    Guns, Grenade, Health,CashReward
}
public class PickUpHandler : MonoBehaviour
{
    public PickUpType Type;
    public int Index;
    public AudioClip PickedClip;
    public static bool AddHealth;
    public Image Health;
    private Image HealthBar;
    public static bool RewardReceived;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
           
            if (PlayerPrefs.GetInt("Sound", 1) == 1)
            {
                this.GetComponent<AudioSource>().PlayOneShot(PickedClip);
            }
            if(Type == PickUpType.Guns)
            {
                other.gameObject.GetComponent<weaponselector>().PickUpWeapon(Index);
            }
            else if (Type == PickUpType.Grenade)
            {

            } 
            else if (Type == PickUpType.Health)
            {
                other.gameObject.GetComponent<playercontroller>().HealthReceived();
                Destroy(gameObject);

                // AddHealth = true;
                //   StartCoroutine(DestroyObject());
            }
            else if (Type == PickUpType.CashReward)
            {
                other.gameObject.GetComponent<playercontroller>().CashReward();
                Destroy(gameObject);
            }
            

            

        }
    }
    // Start is called before the first frame update
    void Start()
    {
      //  Debug.Log("StartHit Points" + playercontroller.instance.hitpoints);
    }

    // Update is called once per frame
    void Update()
    {
        //transform.Rotate(Vector3.right, 10.0f * Time.deltaTime, Space.World);
        
        if (playercontroller.instance.hitpoints > 100f)
        {
            playercontroller.instance.hitpoints = 100f;
        }
    }
   
    public IEnumerator DestroyObject()
    {
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }
}
