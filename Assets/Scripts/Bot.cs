using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityStandardAssets.Characters.ThirdPerson;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(ThirdPersonCharacter))]
[RequireComponent(typeof(Animator))]

public class Bot : BaseUnit
{
    private NavMeshAgent agent;
    private ThirdPersonCharacter controller;
    private Transform playerPos;
    private Animator anim; 
    private Int32 destId = 0;

    [Header("Передвижение персонажа")]
    public GameObject patrolWay;
    [SerializeField] private Transform[] patrolPoints;
    [SerializeField] private Single _timeOut = 0;
    [SerializeField] private Single _timeWait = 2;
    [SerializeField] private Single _stoppingDistance = 1;
    [SerializeField] private Single _activeDistance = 10;
    [SerializeField] private Boolean _shootingState;
    [SerializeField] private Boolean _patrolState;
    [SerializeField] private Boolean _isTarget;
    
    [Header("Поиск цели")]
    [SerializeField] private List<Transform> visibleTargets = new List<Transform>();
    [SerializeField] private Single _maxAngle = 30;
    [SerializeField] private Single _maxRadius = 20;
    [SerializeField] private LayerMask targetMask;
    [SerializeField] private LayerMask obstacleMask;

    private Single _shootDistance = 1000;
    private Int32 _damage = 20;
    protected Transform _gunT;
    protected ParticleSystem _muzzleFlash;
    protected GameObject _hitParticle;
    protected Boolean _shooting = false;
    protected override void Awake()
    {
        base.Awake();

        agent = GetComponent<NavMeshAgent>();
        controller = GetComponent<ThirdPersonCharacter>();
        anim = GetComponent<Animator>();
        agent.updatePosition = true;
        agent.updateRotation = true;
        playerPos = GameObject.FindGameObjectWithTag("Player").transform;
        patrolPoints = patrolWay.GetComponentsInChildren<Transform>();
        _gunT = GameObject.FindGameObjectWithTag("GunT").transform;
        _muzzleFlash = _gunT.GetComponentInChildren<ParticleSystem>();
        _hitParticle = Resources.Load<GameObject>("Flare");

        SetDestination();

        StartCoroutine("FindTargets", 0.1);
    }

    IEnumerator FindTargets(Single delay)
    {
        while (true)
        {
            yield return new WaitForSeconds(delay);
            FindVisibleTargets();
        }

    }

    IEnumerator Shoot(RaycastHit playerHit)
    {
        yield return new WaitForSeconds(1);
        _muzzleFlash.Play();
        playerHit.collider.GetComponent<ISetDamage>().SetDamage(_damage);
        PlayHitParticle(playerHit);
        _shooting = false;
    }
    void Update()
    {
        _patrolState = visibleTargets.Count <= 0;
        
        if (agent)
        {
            if(_patrolState)
            {
                agent.stoppingDistance = 1;

                if (agent.remainingDistance > agent.stoppingDistance)
                {
                    controller.Move(agent.desiredVelocity, false, false);
                    anim.SetBool("move", true);
                }
                else
                {
                    controller.Move(Vector3.zero, false, false);
                    anim.SetBool("move", false);

                    _timeOut += 0.1f;
                    if(_timeOut > _timeWait)
                    {
                        _timeOut = 0;
                        
                        ChooseDestination();
                        SetDestination();
                    }                   
                }
            }
            else
            {                                       
                RaycastHit hit;
                if (Physics.Raycast(GetRay(), out hit, _shootDistance))
                {                    
                    if (!_shooting & hit.collider.tag == "Player")
                    {
                        agent.stoppingDistance = 5;
                        agent.SetDestination(hit.transform.position);

                        StartCoroutine(Shoot(hit));
                        _shooting = true;
                    }                   
                }

            }
            
        }
    }

    private void PlayHitParticle(RaycastHit hit)
    {
        GameObject tempHit = Instantiate(_hitParticle, hit.point, Quaternion.LookRotation(hit.normal));
        tempHit.transform.parent = hit.transform;
        Destroy(tempHit, 0.5f);
    }
    private Ray GetRay()
    {
        return new Ray(new Vector3(Position.x, Position.y + 1, Position.z), transform.forward);
    }

    private void ChooseDestination()
    {
        destId++;
        if (destId >= patrolPoints.Length)
            destId = 0;
    }

    private void SetDestination()
    {
        agent.SetDestination(patrolPoints[destId].position);
    }
    private void FindVisibleTargets()
    {
        visibleTargets.Clear();

        Collider[] targetInViewRadius = Physics.OverlapSphere(Position, _maxRadius, targetMask);
        for(Int32 i = 0; i < targetInViewRadius.Length; i++)
        {
            Transform target = targetInViewRadius[i].transform;
            Vector3 dirToTarget = (target.position - Position).normalized;
            if(Vector3.Angle(transform.forward, dirToTarget) < _maxAngle / 2)
            {
                Single distToTarget = Vector3.Distance(Position, target.position);
                if (!Physics.Raycast(Position, dirToTarget, distToTarget, obstacleMask))
                    visibleTargets.Add(target);                
            }
        }
    }
}
