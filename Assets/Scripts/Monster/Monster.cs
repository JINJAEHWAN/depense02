using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Unity.VisualScripting;

public class Monster : BattleData
{

    public enum State
    {
        create, move, battle, death
    }
    public State myState = State.create;

    void changeState(State s)
    {
        if (myState == s) return;
        myState = s;
        switch (myState)
        {
            case State.move:
                    StartCoroutine(move());
                    break;
            case State.battle:
                        animator.SetBool("IsWalk", false);
                        StopAllCoroutines();
                        StartCoroutine(onBattle());
                        break;
            case State.death:
                        animator.SetBool("IsDeath", true);
                        break;
        }
    }
    void StateProcess()
    {
        switch (myState)
        {
            case State.move:
                rayFollow();
                break;
            case State.battle:
                if (data.hp <= 0)
                {
                    changeState(State.death);
                }
                break;
        }
    }


    public StageLevel stageLevel;
    public List<BattleData> targets;
    BattleData target;
    public LayerMask CrashMask;
    [SerializeField] Animator animator;


    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        deathAlarm += () => changeState(State.death);
        changeState(State.move);
    }

    void Update()
    {
        StateProcess();
        
    }

    IEnumerator move()
    {
        while (!target)
        {
            transform.position += (Vector3)(Vector2.left * data.moveSpeed * Time.deltaTime);
            animator.SetBool("IsWalk", true);
            yield return null;
        }
    }

    IEnumerator onBattle()
    {

        while (targets.Count > 0)
        {
            // Check if the first target is not null before proceeding
            if (targets[0] != null)
            {
                animator.SetTrigger("IsAttack");
                targets[0].GetComponent<BattleData>().onHit(data.attackPower);

                yield return new WaitForSeconds(data.attackSpeed);

                // After attacking, check if the target is still valid
                if (targets[0] != null)
                {
                    targets[0].GetComponent<BattleData>().deathAlarm += () => changeState(State.move);
                }
            }
            else
            {
                // If the target is null, remove it from the list
                targets.RemoveAt(0);
            }

            // Check if the targets list is empty or the first target is null
            if (targets.Count == 0 || targets[0] == null)
            {
                changeState(State.move);
                yield break;
            }
        }

    }
    public void rayFollow()
    {
        Collider2D hit = Physics2D.OverlapBox(this.gameObject.transform.position, new Vector2(0.5f, 4.0f), 0, CrashMask);
        if (hit != null)
        {
            BattleData bd = hit.GetComponentInParent<BattleData>();
            if (bd != null && !targets.Contains(bd))
            {
                targets.Add(bd);
                changeState(State.battle);
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
    public void Ondead()
    {
        Destroy(transform.gameObject);
    }
}
