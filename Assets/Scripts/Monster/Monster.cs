using System.Collections;
using UnityEngine;

public class Monster : BattleData
{

    public enum myState
    {
        none,create, move, battle, hit
    }

    public myState nowState = myState.none;
    StageLevel stageLevel;
    Coroutine myStateCo;
    public GameObject target;
    public LayerMask CrashMask;
    
    

    void changeState(myState s)
    {
        if (nowState == s) return;
        switch (s)
        {
            case myState.create:
                StopCoroutin();
                changeState(myState.move);
                break;
            case myState.move:
                StopCoroutin();
                myStateCo = StartCoroutine(move());
                break;
            case myState.battle:
                StopAllCoroutines();
                StopCoroutin();
                myStateCo = StartCoroutine(onBattle());
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
        changeState(myState.move);
        stageLevel = FindFirstObjectByType<StageLevel>();
    }

    void Update()
    {
        StateProcess();
    }

    IEnumerator move()
    {
        float delta = Time.deltaTime * 2.0f;
        transform.parent = null;
        while (true)
        {
            transform.Translate(delta * Vector3.left*data.moveSpeed) ;
            yield return null;
        }
    }

    IEnumerator onBattle()
    {
        while (true)
        {
            target.GetComponent<BattleData>().onHit(data.attackPower);
            yield return new WaitForSeconds(data.attackSpeed);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((1 << collision.gameObject.layer & CrashMask) != 0)
        {
            StopCoroutine(myStateCo);
            target = collision.gameObject;
            changeState(myState.battle);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        changeState(myState.move);
    }

    void StopCoroutin()
    {
        if(myStateCo != null)StopCoroutine(myStateCo);
        myStateCo = null;
    }

    void setState()
    {
        data.hp = data.hp * stageLevel.Level;
        data.MaxHp = data.MaxHp * stageLevel.Level;
        data.attackPower = data.attackPower * stageLevel.Level;
        data.attackSpeed -= (stageLevel.Level * 0.2f);
        data.cost = data.cost * stageLevel.Level;
        data.moveSpeed = data.moveSpeed * stageLevel.Level;
    }
}
