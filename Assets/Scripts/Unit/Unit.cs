using UnityEngine;
using System.Collections;
using UnityEngine.InputSystem.Controls;
using NUnit.Framework;
using System.Collections.Generic;
public class Unit : BattleData
{
    public List<BattleData> target;
    bool isMove = true;
    float deltaAttack = 0f;
    Animator animator;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 11)
        {
            BattleData bd = collision.gameObject.GetComponent<BattleData>();
            if (bd != null) { target.Add(bd); }
            
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 11)
        {
            BattleData bd = collision.gameObject.GetComponent<BattleData>();
            if (bd != null) { target.Remove(bd); }
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //StartCoroutine(move());
        animator = GetComponentInChildren<Animator>();
        target = new List<BattleData>();
    }

    // Update is called once per frame
    void Update()
    {
        if (target.Count<1)
        {
            animator.SetBool("IsWalking", true);

            transform.position += (Vector3)(Vector2.right * data.moveSpeed * Time.deltaTime);
        }
        else animator.SetBool("IsWalking", false);

        deltaAttack += Time.deltaTime * data.attackSpeed;
        if (deltaAttack > 0 && target.Count > 0) 
        {
            animator.SetTrigger("IsAttack");
            deltaAttack = -1f;
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
