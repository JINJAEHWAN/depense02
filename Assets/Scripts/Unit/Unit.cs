using UnityEngine;
using System.Collections;
using UnityEngine.InputSystem.Controls;
using NUnit.Framework;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
public class Unit : BattleData
{
    public List<BattleData> targets;
    BattleData target;
    bool isMove = true;
    float deltaAttack = 0f;
    Animator animator;
    public LayerMask crashMask;

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if ((1 << collision.transform.gameObject.layer & crashMask)!=0)
    //    {
    //        BattleData bd = collision.gameObject.GetComponentInParent<BattleData>();
    //        if (bd != null && !targets.Contains(bd)) {
    //            targets.Add(bd); 
    //        }
            
    //    }
    //}

    //private void OnTriggerExit2D(Collider2D collision)
    //{
    //    if ((1 << collision.transform.gameObject.layer & crashMask)!= 0)
    //    {
    //        BattleData bd = collision.gameObject.GetComponent<BattleData>();
    //        if (bd != null) {
    //            targets.RemoveAt(0); 
    //        }
    //    }
    //}
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        targets = new List<BattleData>();
    }

    // Update is called once per frame
    void Update()
    {
        
        deltaAttack += Time.deltaTime;
        if (deltaAttack > 0 && targets.Count > 0)
        {
            animator.SetTrigger("IsAttack");
            deltaAttack = -data.attackSpeed;
            target = targets[0];
        }
        RaycastHit2D hit = Physics2D.Raycast(this.gameObject.transform.position, Vector2.right, 1.0f);
        if (hit.collider != null)
        {
            //콜라이더가 부딪히면 타겟을 add
            if ((1 << hit.collider.transform.gameObject.layer & crashMask) != 0)
            {
                BattleData bd = hit.collider.gameObject.GetComponentInParent<BattleData>();
                if (bd != null && !targets.Contains(bd))
                {
                    targets.Add(bd);
                }
            }
        }
        if (isMove && targets.Count < 1)
        {
            animator.SetBool("IsWalking", true);

            transform.position += (Vector3)(Vector2.right * data.moveSpeed * Time.deltaTime);
        }
        else animator.SetBool("IsWalking", false);

    }
    public void OnAttack()
    {
        isMove = false;

        if (target != null)
        {
            target.data.hp -= data.attackPower;
            if (target.data.hp <= 0)
            {
                target.data.hp = 0;
                target.GetComponent<Collider2D>().enabled = false;
                Destroy(target.gameObject);
                isMove = true;
                targets.RemoveAt(0);
            }
        }

    }



}
