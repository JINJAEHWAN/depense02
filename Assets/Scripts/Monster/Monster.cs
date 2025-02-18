using System.Collections;
using UnityEngine;

public class Monster : BattleData
{

    public enum myState
    {
        create, move, battle, hit
    }

    public myState nowState = myState.create;

    void changeState(myState s)
    {
        if (nowState == s) return;
        switch (s)
        {
            case myState.create:
                setState();
                myStateCo = StartCoroutine(move());
            break;
        }
    }

    Coroutine myStateCo;
    public LayerMask CrashMask;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        changeState(myState.create);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator move()
    {
        float delta = Time.deltaTime * 2.0f;
        transform.parent = null;
        while (true)
        {
            transform.Translate(delta * Vector3.left*2.0f) ;
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
        //data.hp = data.hp * StageLevel;
        //data.MaxHp;
        //data.attackPower;
        //data.attackSpeed;
        //data.cost;
    }
}
