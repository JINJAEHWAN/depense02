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

    void changeState(myState s)
    {
        if (nowState == s) return;
        switch (s)
        {
            case myState.create:
                changeState(myState.move);
            break;
            case myState.move:
                myStateCo = StartCoroutine(move());
            break;
            case myState.battle:

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

    Coroutine myStateCo;
    public LayerMask CrashMask;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        changeState(myState.move);
    }

    // Update is called once per frame
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((1 << collision.gameObject.layer & CrashMask) != 0)
        {
            StopCoroutine(myStateCo);
            //StopAllCoroutines();
            //StartCoroutine(Battle());
        }
    }
    void setState()
    {
        data.hp = data.hp * stageLevel.Level;
        data.MaxHp = data.MaxHp * stageLevel.Level;
        data.attackPower = data.attackPower * stageLevel.Level;
        data.attackSpeed = data.attackSpeed * stageLevel.Level;
        data.cost = data.cost * stageLevel.Level;
        data.moveSpeed = data.moveSpeed * stageLevel.Level;
    }
}
