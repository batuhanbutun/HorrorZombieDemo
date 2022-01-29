using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieController : MonoBehaviour
{
   [SerializeField] private GameStats _gameStats;
   [SerializeField] private Transform target;
   [SerializeField] private NavMeshAgent agent;
   [SerializeField] private Animator myAnim;
   [SerializeField] private PlayerController _player;
   [SerializeField] private float currentHealth;

    private float attackCd = 0f;
    void Start()
    {
        currentHealth = 100f;
    }

    
    void Update()
    {
        if (_gameStats.isGameOver == false)
        {
            attackCd -= Time.deltaTime;
            Movement();
        }
        else
        {
            myAnim.SetBool("isAttack", false);
        }
    }

    private void Movement()
    {
        myAnim.SetBool("isAttack", false);
        float distance = Vector3.Distance(target.position, transform.position);
        if (distance < 2.2f)
        {
            lookTarget();
            myAnim.SetBool("isRunning", false);
            myAnim.SetBool("isAttack", true);
             Attack();
        }
        if (distance <= 5f && distance > 2.2f)
        {
            agent.SetDestination(target.position);
            myAnim.SetBool("isRunning", true);
        }
    }
    private void lookTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    private void Attack()
    {
        if (attackCd <= 0f)
        {
            _player.GetDamage(30);
            attackCd = 1.5f / 1f;
        }

    }

    public void getDamage(float damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0f)
        {
            Die();
        }
    }

    private void Die()
    {
        myAnim.SetBool("isDead", true);
        Destroy(gameObject, 1f);
    }
}
