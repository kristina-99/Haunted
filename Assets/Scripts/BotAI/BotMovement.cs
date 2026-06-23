using UnityEngine;
using UnityEngine.AI;

public class BotMovement : MonoBehaviour
{
    public Transform startPoint;
    public Transform target;
    private NavMeshAgent agent;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.Warp(startPoint.position);
    }

    // Update is called once per frame
    void Update()
    {
        agent.SetDestination(target.position);
    }
}
