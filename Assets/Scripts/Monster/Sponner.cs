using System.Collections;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;


public class Sponner : StageLevel
{
    
    [Header("스포너 위치에 따른 생성")]
    [SerializeField] Vector2[] Sponners;

    [Header("소환 할 유닛")]
    [SerializeField] Monster[] Monster;

    [SerializeField] LayerMask layerMask;

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
        while(true)
        {
            Vector2 rndPosition = Sponners[Random.Range(0, Sponners.Length)];
            Monster un = Instantiate(Monster[Random.Range(0,Monster.Length)], new Vector2(rndPosition.x,rndPosition.y), Quaternion.identity);
            un.CrashMask = layerMask;
            //un.stageLevel.Level = Level;
            yield return new WaitForSeconds(2.0f);
        }
    }
}
