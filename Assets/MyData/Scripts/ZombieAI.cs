using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class ZombieAI : MonoBehaviour
{
    public Animator thisAnimator;
    public float PlayerDetectDistance, shootRange = 10f, damage;
    public Transform LookAter;
    NavMeshAgent thisAgent;
    float tempDistance;
    float shootDelay, shootDelayTemp;
    Transform PlayerTransform;
    bool isPlayerOnFront;
    bool isPlayerDetected;

    RaycastHit hit;

    // Start is called before the first frame update
    void Start()
    {
        shootDelay = 2f;// Random.Range(1f, 2f);

        shootRange = Random.Range(shootRange, shootRange * 2f);

        thisAgent = gameObject.GetComponent<NavMeshAgent>();
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
        else
        {
            isPlayerOnFront = false;
        }

        if (tempDistance <= PlayerDetectDistance)
        {
            if (tempDistance <= shootRange && isPlayerOnFront)
            {
                thisAgent.isStopped = true;
                thisAnimator.SetBool("isRun", false);
                thisAnimator.SetBool("Shoot", true);

                Shoot();

                LookAter.LookAt(PlayerTransform.position);
                //transform.LookAt(PlayerTransform.position);
            }
            else
            {
                thisAgent.isStopped = false;
                thisAgent.SetDestination(PlayerTransform.position);
                thisAnimator.SetBool("isRun", true);
                thisAnimator.SetBool("Shoot", false);

            }
            if (isPlayerOnFront)
            {
                //Shoot();
            }
        }
        else
        {
            thisAgent.isStopped = true;
            thisAnimator.SetBool("isRun", false);
        }
    }
    void Shoot()
    {
        shootDelayTemp += Time.deltaTime;

        if (shootDelayTemp >= shootDelay)
        {
            PlayerTransform.GetComponent<playercontroller>().Damage(damage);
            shootDelayTemp = 0;
        }
    }
}