using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private NavMeshAgent agent;

    [SerializeField] private Transform player;

    [SerializeField] private LayerMask whatIsGround, WhatIsPlayer;



    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        ChasePlayer();
    }
    private void Update()
    {
        ChasePlayer();
    }
    private void Patroling()
    {

    }
    private void ChasePlayer()
    {
        agent.SetDestination(player.position);
    }
    private void Attack()
    {

        transform.LookAt(player);
    }
}
