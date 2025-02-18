using UnityEngine;

public class UnitSponner : MonoBehaviour
{

    [Header("이곳에 유닛 x축 위치 정하기.")]
    [SerializeField] float onX =0;

    //랜덤용
    //y축 위치는 랜덤 3군데
    //random 써서 y 위치 3군데 중 한 군데에 놓기
    //-2 0 2
    int cpu;
    Vector3 OnPos;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        caseCpu();   
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Debug.Log(OnPos);
            Instantiate(Resources.Load("Unit/Unit 1"), OnPos, Quaternion.identity);
        }
    }

    void caseCpu()
    {
        cpu = Random.Range(0, 3);

        switch (cpu)
        {
            case 0:
                OnPos = new Vector3(onX, -2, 0);
                break;
            case 1:
                OnPos = new Vector3(onX, 0, 0);
                break;
            case 2:
                OnPos = new Vector3(onX, 2, 0);
                break;
        }
    }
}
