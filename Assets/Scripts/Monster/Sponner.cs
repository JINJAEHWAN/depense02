using System.Collections;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;


public class Sponner : StageLevel
{
    
    [Header("������ ��ġ�� ���� ����")]
    [SerializeField] Vector2[] Sponners;

    [Header("��ȯ �� ����")]
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
