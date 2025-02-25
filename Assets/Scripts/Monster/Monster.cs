using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Unity.VisualScripting;

public class Monster : BattleData
{

    public enum myState
    {
        create, move, battle, death
    }

    public myState nowState;
    public StageLevel stageLevel;
    public List<BattleData> targets;
    BattleData target;
    public LayerMask CrashMask;
    [SerializeField] Animator animator;


    void changeState(myState s)
    {
        if (nowState == s) return;
        nowState = s;
        switch (s)
        {
            case myState.move:
                StartCoroutine(move());
                break;
            case myState.battle:
                animator.SetBool("IsWalk", false);
                StopAllCoroutines();
                StartCoroutine(onBattle());
                break;
            case myState.death:
                animator.SetTrigger("IsDeath");
                break;
        }
    }


    void StateProcess()
    {
        switch (nowState)
        {
            case myState.move:
                rayFollow();
                break;
            case myState.battle:

                break;
        }
    }

    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        deathAlarm += () => changeState(myState.death);
        changeState(myState.move);
    }

    void Update()
    {
        StateProcess();
        if(data.hp <= 0)
        {
            StopAllCoroutines();
        }
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

        while( targets.Count > 0)
        {
            if(targets[0]!= null && targets.Count != 0)
            {
                animator.SetTrigger("IsAttack");
                targets[0].GetComponentInParent<BattleData>().onHit(data.attackPower);

                yield return new WaitForSeconds(data.attackSpeed);
            }
            if (targets.Count > 0)
                targets[0].GetComponent<BattleData>().deathAlarm += () => changeState(myState.move); // Á×¿´À» ¶§ normal state.
            if (targets.Count == 0 || targets[0] == null)
            {
                changeState(myState.move);
            }
            if (data.hp <= 0)
            {
                changeState(myState.death);
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
                changeState(myState.battle);
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
