using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Monster : BattleData
{

    public enum myState
    {
        none,create, move, battle, hit
    }

    public myState nowState;
    public StageLevel stageLevel;
    public List<GameObject> target;
    public LayerMask CrashMask;
    
    

    void changeState(myState s)
    {
        if (nowState == s) return;
        nowState = s;
        switch (s)
        {
            case myState.create:
                setState();
                changeState(myState.move);
                break;
            case myState.move:
                StartCoroutine(move());
                break;
            case myState.battle:
                StartCoroutine(onBattle());
                break;
        }
    }


    void StateProcess()
    {
        switch (nowState)
        {
            case myState.move:

                break;
            case myState.battle:

                break;
        }
    }

    void Start()
    {
        nowState = myState.none;
        changeState(myState.create);
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
        float delta = Time.deltaTime * data.moveSpeed;
        while (true)
        {
            transform.Translate(delta * Vector3.left) ;
            yield return null;
        }
    }

    IEnumerator onBattle()
    {
        while (true)
        {
            if (target[0] != null)
            {
                target[0].GetComponent<BattleData>().onHit(data.attackPower);
                yield return new WaitForSeconds(data.attackSpeed);
            }
            else
            {
                target.RemoveAt(0);
                yield return null;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((1 << collision.gameObject.layer & CrashMask) != 0)
        {
            StopAllCoroutines();
            target.Add(collision.gameObject);
            target = target.Distinct().ToList();
            changeState(myState.battle);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if ((1 << collision.gameObject.layer & CrashMask) != 0)
        {
            StopAllCoroutines();
            target.Remove(collision.gameObject);
            if(target.Count <= 1) { 
                changeState(myState.move);
            }
        }
    }

    private IEnumerator doDetecting(Collider2D collision)
    {
        while (true)
        {
            yield return null;
        }
    }

    void StopCoroutin()
    {
        StopAllCoroutines();
        //if(myStateCo != null)StopCoroutine(myStateCo);
    }

    void setState()
    {
        transform.parent = null;
        //data.hp = data.hp * stageLevel.Level;
        //data.MaxHp = data.MaxHp * stageLevel.Level;
        //data.attackPower = data.attackPower * stageLevel.Level;
        //data.attackSpeed -= (stageLevel.Level * 0.2f);
        //data.cost = data.cost * stageLevel.Level;
        //data.moveSpeed = data.moveSpeed * stageLevel.Level;
    }
}
