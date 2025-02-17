using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Sponner : MonoBehaviour
{
    [Header("스포너 위치에 따른 생성")]
    public Transform[] Sponners;

    [Header("소환 할 유닛")]
    public GameObject[] Monster;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameObject summon = Instantiate(Monster[Random.Range(0,2)],Sponners[Random.Range(0, 4)].transform.position, Quaternion.identity);
        Debug.Log(Monster[Random.Range(0, 2)]);
        Debug.Log(Sponners[Random.Range(0, 4)]);
    }

    // Update is called once per frame
    void Update()
    {
    }
}
