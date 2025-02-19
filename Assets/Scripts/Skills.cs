using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

public class Skills : MonoBehaviour
{
    [Header("�� ������ ����")]
    public Sprite skillspr;
    [Header("�Ҹ��� ���� �Է�")]
    public float Mana;
    [Header("�� �� �Ŀ� ������ ���� �Է�")]
    public float DestroyTime;
    [Header("�� ��ų�� üũ")]
    [SerializeField] private bool IsHeal;
    [Header("����/���� �Է�")]
    public int skillPower;

    public List<BattleData> targets;
    //�浹 ó���� ���߿�.
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
