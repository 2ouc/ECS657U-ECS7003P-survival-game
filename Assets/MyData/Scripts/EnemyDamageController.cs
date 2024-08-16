using UnityEngine;
using UnityEngine.UI;
using System.Collections;
public class EnemyDamageController : MonoBehaviour
{
    public float Health;
    public GameObject DeadbodyReplacement;
    public GameObject[] RewardSpawnOnDeath;
    public GameObject Kill;
    public static EnemyDamageController DamageData;

    public static bool isHeadSot;
   // public Transform MoveTo;
    bool isDead;

    public void DamageMain(float value)
    {
        if (isDead)
        {
            return;
        }

        EnemyAIController.isPlayerDetected = true;

        Health -= value;
        if (Health <= 0)
        {
            isDead = true;
            GameManager.RequiredDeath++;
            
         //   Debug.LogError("EDC Required Death"+ GameManager.RequiredDeath);
        //    Debug.LogError("Total Kills:"+ GameManager.TotalDeath);
            if (isDead == true && isHeadSot==false)
            {
                GameObject KillSprite = Instantiate(Kill, transform.position, Quaternion.Euler(new Vector3(45, 0, 0)));
//                KillSprite.GetComponent<RectTransform>().sizeDelta = new Vector2(93.9f, 172.3f);
                GameObject Toparent = GameObject.FindGameObjectWithTag("CanvasToChild");
                KillSprite.transform.SetParent(Toparent.transform, true);
                Debug.Log("CoroutineShould Run");
                Destroy(KillSprite,1f);
                
            }
            GameObject go = Instantiate(DeadbodyReplacement, transform.position, transform.rotation);
            Rigidbody[] rg = go.GetComponentsInChildren<Rigidbody>();
            float tempForce = Random.Range(-2f, 2f);
            foreach (Rigidbody item in rg)
            {
                item.AddForce(transform.forward * 25f * tempForce,ForceMode.Impulse);
            }
            GameManager.instance.CountDead(isHeadSot);
            isHeadSot = false;

            GenerateItem();

            Destroy(go,3f);
           // StartCoroutine(DestroySprite());
            Destroy(gameObject);
        }
    }
    void GenerateItem()
    {
        Destroy( Instantiate( RewardSpawnOnDeath[Random.Range(0, RewardSpawnOnDeath.Length)],
            transform.position + (Vector3.up/2f),
            Quaternion.identity), 10f);
    }
   /* public IEnumerator DestroySprite() 
    {
        yield return new WaitForSeconds(1.5f);
        Debug.Log("Coroutine Run");
        Destroy(Kill);
    }*/
}
