using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

public class Skills : MonoBehaviour
{
    [Header("쓸 아이콘 연결")]
    public Sprite skillspr;
    [Header("소모할 마나 입력")]
    public float Mana;
    [Header("몇 초 후에 삭제할 건지 입력")]
    public float DestroyTime;
    [Header("힐 스킬만 체크")]
    [SerializeField] private bool IsHeal;
    [Header("딜량/힐량 입력")]
    public int skillPower;

    public List<BattleData> targets;
    //충돌 처리는 나중에.
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!IsHeal)
        {
            if(collision.gameObject.layer == 11)
            {
                BattleData bd = collision.gameObject.GetComponent<BattleData>();
                if (bd != null)
                {
                    targets.Add(bd);
                }
            }
        }
        else
        {
            if (collision.gameObject.layer == 10)
            {
                BattleData bd = collision.gameObject.GetComponent<BattleData>();
                if (bd != null)
                {
                    targets.Add(bd);
                }
            }
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(Attack());
        Destroy(gameObject, DestroyTime);
    }
    IEnumerator Attack()
    {
        yield return new WaitForSeconds(0.1f);
        if (!IsHeal)
        {
            foreach (BattleData bd in targets)
            {
                bd.data.hp -= skillPower;
            }
        }
        else
        {

            foreach (BattleData bd in targets)
            {
                bd.data.hp += skillPower;
                if (bd.data.hp > bd.data.MaxHp)
                {
                    bd.data.hp = bd.data.MaxHp;
                }
                SliderValueChange.Instance.PlayerHPSliderValueChange();
            }
        }
        gameObject.GetComponent<Collider2D>().enabled = false;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
