using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;
using ControlFreak2.Demos.Guns;

public class EnemyAIController : MonoBehaviour
{
    public Transform LookAter;
    public Animator thisAnimator;
    public GameObject MuzzleFlash;
    public GameObject BulletObj;
    public Transform BackwardsPoint;
    public Transform BulletInitializePoint;
    

    public float PlayerDetectDistance, shootRange=10f;

    NavMeshAgent thisAgent;
    Rigidbody thisRigidbody;
    float tempDistance;
    int burst;
    float shootDelay, burstWait, burstWaitTemp, shootDelayTemp;
    Transform PlayerTransform;
    bool isShoot;
    bool flag;
    bool isPlayerOnFront;
    public static bool isPlayerDetected;

    RaycastHit hit;

    // Start is called before the first frame update
    void Start()
    {
        isPlayerDetected = false;
        burst = 3;
        shootDelay = Random.Range(1f, 2f);
        burstWait = 0.2f;

        shootRange = Random.Range(shootRange, shootRange * 2f);

        thisAgent = gameObject.GetComponent<NavMeshAgent>();
        thisRigidbody = gameObject.GetComponent<Rigidbody>();
        PlayerTransform = GameManager.instance.Player;
    }

    // Update is called once per frame
    void Update()
    {
        tempDistance = Vector3.Distance(transform.position, PlayerTransform.position);
        Vector3 dir = PlayerTransform.position - transform.position;
        if (Physics.Raycast(transform.position + transform.up, dir, out hit, tempDistance))
        {
            //Debug.Log(hit.collider.tag);
            if (hit.collider.gameObject.CompareTag("Player") || hit.collider.gameObject.CompareTag("Body"))
            {
                isPlayerOnFront = true;
            }
            else
            {
                isPlayerOnFront = false;
            }
        }
        Debug.DrawRay(transform.position + transform.up, dir * hit.distance, Color.red);

        if(tempDistance <= PlayerDetectDistance || isPlayerDetected)
        {
            //Shoot();

            if (tempDistance <= shootRange && isPlayerOnFront )
            {
                //thisAgent.SetDestination(transform.position);
                //thisRigidbody.velocity = Vector3.zero;
                thisAnimator.SetBool("isRun", false);
                isShoot = true;

                LookAter.LookAt(PlayerTransform.position);
                //transform.LookAt(PlayerTransform.position);
                if(tempDistance < 5f)
                {
                    thisAgent.isStopped = false;

                    thisAgent.SetDestination(BackwardsPoint.position);
                    thisAnimator.SetBool("isBackward", true);
                }
                else
                {
                    thisAgent.isStopped = true;

                    thisAnimator.SetBool("isBackward", false);
                }
            }
            else
            {
                //LookAter.rotation = Quaternion.identity;
                thisAgent.isStopped = false;

                thisAgent.SetDestination(PlayerTransform.position);
                thisAnimator.SetBool("isRun",true);
                isShoot = false;

            }
            if (isPlayerOnFront)
            {
                Shoot();
            }
        }
        else
        {
            thisAgent.isStopped = true;

            thisAnimator.SetBool("isRun", false);
            isShoot = false;
        }
    }
    void Shoot()
    {
        //transform.LookAt(PlayerTransform.position);
        shootDelayTemp += Time.deltaTime;

        if(BulletInitializePoint)
            BulletInitializePoint.LookAt(PlayerTransform.position + (Vector3.up * Random.Range(1, 1.2f)));

        if(shootDelayTemp >= shootDelay)
        {
            StartCoroutine(Shooting());
            shootDelayTemp = 0;
        }
    }
    IEnumerator Shooting()
    {
        //StartCoroutine(EnableShooting());
        if (!flag)
        {
            for (float i = 0; i <= 1; i += (Time.deltaTime * 2f))
            {
                thisAnimator.SetFloat("Shoot", Mathf.Lerp(0, 1, i));
                yield return null;
            }
            flag = true;
        }

        for (int i = 0; i < burst; i++)
        {
            yield return new WaitForSeconds(burstWait);
            //MuzzleFlash.SetActive(true);
            if(BulletInitializePoint)
                Fire();
            yield return null;
            //MuzzleFlash.SetActive(false);
        }
    }
    void Fire()
    {
        Rigidbody rig = Instantiate(BulletObj, BulletInitializePoint.position, BulletInitializePoint.rotation).GetComponent<Rigidbody>();

        rig.AddForce(BulletInitializePoint.forward * 100f, ForceMode.Impulse);
    }

}