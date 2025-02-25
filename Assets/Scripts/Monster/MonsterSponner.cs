using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSponner : MonoBehaviour
{
    [Header("스포너 위치에 따른 생성")]
    [SerializeField] Vector2[] Sponners;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(summonMonster());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator summonMonster()
    {
        while (true)
        {
            Vector2 rndPosition = Sponners[Random.Range(0, Sponners.Length)];
            //현재는 몬스터가 1개임.
            Monster m= Instantiate(Resources.Load<Monster>($"Monster/Monster 1"), 
                new Vector2(rndPosition.x, rndPosition.y), Quaternion.identity);
            yield return new WaitForSeconds(2.0f);
        }
    }
}
