using UnityEngine;
using System.Collections;
using UnityEngine.InputSystem.Controls;
using NUnit.Framework;
using System.Collections.Generic;
public class Unit : BattleData
{
    public List<BattleData> targets;
    BattleData target;
    bool isMove = true;
    float deltaAttack = 0f;
    Animator animator;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 11)
        {
            BattleData bd = collision.gameObject.GetComponentInParent<BattleData>();
            if (bd != null && !targets.Contains(bd)) {
                targets.Add(bd); 
            }
            
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 11)
        {
            BattleData bd = collision.gameObject.GetComponent<BattleData>();
            if (bd != null) {
                targets.Remove(bd); 
            }
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //StartCoroutine(move());
        animator = GetComponentInChildren<Animator>();
        targets = new List<BattleData>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isMove)
        {
            animator.SetBool("IsWalking", true);

            transform.position += (Vector3)(Vector2.right * data.moveSpeed * Time.deltaTime);
        }
        else animator.SetBool("IsWalking", false);

        deltaAttack += Time.deltaTime * data.attackSpeed;
        if (deltaAttack > 0 && targets.Count > 0) 
        {
            animator.SetTrigger("IsAttack");
            deltaAttack = -1f;
            target = targets[0];
            StartCoroutine(Attack());
        }
    }
    IEnumerator Attack()
    {
        isMove = false;
        yield return new WaitForSeconds(0.15f);
        if (target != null)
        {
            target.data.hp -= data.attackPower;
            if (target.data.hp <= 0)
            {
                target.data.hp = 0;
                targets.Remove(target);
                target.GetComponent<Collider2D>().enabled = false;
                Destroy(target.gameObject, 0.5f);
            }
        }
        else
        {
            targets.RemoveAt(0);
        }
        if (targets.Count < 1)
        {
            isMove = true;
        }
    }
    
    IEnumerator move()
    {
        while (isMove)
        {
            transform.position += (Vector3)(Vector2.right * data.moveSpeed * Time.deltaTime);
            yield return null;
        }
    }

}
