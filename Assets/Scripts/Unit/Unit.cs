using UnityEngine;
using System.Collections;
using UnityEngine.InputSystem.Controls;
using NUnit.Framework;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine.Events;
using Unity.VisualScripting;
using static UnityEditor.PlayerSettings;
using UnityEngine.AI;
public class Unit : BattleData
{
    public List<BattleData> targets;
    BattleData target;
    float deltaAttack = 0f;
    [SerializeField] Animator animator;
    public LayerMask crashMask;

    Vector3 targetPos;

    public enum State
    {
        Create, Normal, Battle, Death
    }
    public State myState = State.Create;

    void ChangeState(State s)
    {
        if (myState == s) return;
        myState = s;
        switch (myState)
        {
            case State.Normal:
                {
                    targetPos = new Vector3(17, 0, 0);//어디까지 갈 지 정하기                    
                }
                break;
            case State.Battle:
                animator.SetBool("IsWalking", false);
                StopAllCoroutines();
                break;
            case State.Death:
                animator.SetTrigger("IsDead");
                break;
        }
    }

    void StateProcess()
    {
        switch (myState)
        {
            case State.Normal:
                MoveTo(targetPos);
                rayFollow();//ray가 맞으면 타겟을 add
                break;
            case State.Battle:
                OnBattle();
                break;
        }
    }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        deathAlarm += () => ChangeState(State.Death);
        ChangeState(State.Normal);
        targets = new List<BattleData>();
    }


    // Update is called once per frame
    void Update()
    {
        StateProcess();
    }

    public void OnBattle()
    {
        deltaAttack += Time.deltaTime;
        if (deltaAttack > 0 && targets.Count > 0)
        {
            animator.SetTrigger("IsAttack");
            deltaAttack = -data.attackSpeed;

            target = targets[0];
            target.GetComponentInParent<BattleData>().onHit(data.attackPower);
        }

        if (targets.Count == 0 || targets[0] == null)
        {
            ChangeState(State.Normal);
        }

        if(targets.Count > 0)
            targets[0].GetComponentInParent<BattleData>().deathAlarm += () => ChangeState(State.Normal); // 죽였을 때 normal state.

        if(data.hp <= 0)
        {
            ChangeState(State.Death);
        }

    }

    //public void LostTarget()
    //{
    //    ChangeState(State.Normal);
    //}

    public void MoveTo(Vector3 pos)
    {
        if (!target)
        {
            transform.position += (Vector3)(Vector2.right * data.moveSpeed * Time.deltaTime);
            animator.SetBool("IsWalking", true);
        }

    }


    public void rayFollow()
    {
        Collider2D hit = Physics2D.OverlapBox(this.gameObject.transform.position, new Vector2(0.5f,4.0f), 0, crashMask);
        if (hit != null)
        {
            BattleData bd = hit.GetComponentInParent<BattleData>();
            if (bd != null && !targets.Contains(bd))
            {
                targets.Add(bd);
                ChangeState(State.Battle);
            }
        }
    }
    public void OnAttack()
    {
        if (target != null)
        {
            target.data.hp -= data.attackPower;
            if (target.data.hp <= 0)
            {
                target.data.hp = 0;
                target.GetComponent<Collider2D>().enabled = false;
                
                targets.RemoveAt(0);
            }
        }
    }
    public void OnDead()
    {
        Destroy(transform.gameObject);
    }


}
