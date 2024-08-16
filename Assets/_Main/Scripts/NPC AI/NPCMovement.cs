using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace IE.RSB
{
    public class NPCMovement : MonoBehaviour
{
    private NavMeshAgent agent;
    private Animator anim;
    [SerializeField]
    private Transform[] target;
    [SerializeField]
    private string[] AnimName;
    [SerializeField]
    private float TargetTime;




    // public static EnemyMovement instance;
    private void OnEnable()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        StartCoroutine(Starter(0));


    }
    private IEnumerator Starter(int num)
    {
        yield return new WaitForSecondsRealtime(0.1f);
        gameObject.GetComponent<Animator>().SetTrigger(AnimName[num]);
        if (target[num] != null && num < 1)
        {
            agent.SetDestination(target[num].position);

        }
        else
        {

            Invoke("Target", TargetTime);
        }
    }
    public void Target()
    {
        agent.SetDestination(target[1].position);
        gameObject.GetComponent<Animator>().SetTrigger(AnimName[2]);
    }



    public void SetTargets(int num)
    {
        Debug.Log("New Target");
        StartCoroutine(Starter(num));
    }
}
}

