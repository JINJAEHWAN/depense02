using UnityEngine;

public class UnitSponner : MonoBehaviour
{

    [Header("�̰��� ���� x�� ��ġ ���ϱ�.")]
    [SerializeField] float onX =0;

    //������
    //y�� ��ġ�� ���� 3����
    //random �Ἥ y ��ġ 3���� �� �� ������ ����
    //-2 0 2
    int cpu;
    Vector3 OnPos;
    KeyCode[] alphas = new KeyCode[8] {
        KeyCode.Alpha1, KeyCode.Alpha2, 
        KeyCode.Alpha3, KeyCode.Alpha4, 
        KeyCode.Alpha5, KeyCode.Alpha6, 
        KeyCode.Alpha7, KeyCode.Alpha8
    };

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
           
    }

    // Update is called once per frame
    void Update()
    {
        for(int i=0; i<8; i++)
        {
            if (Input.GetKeyDown(alphas[i]))
            {
                caseCpu();
                Debug.Log(OnPos);
                Instantiate(Resources.Load($"Unit/Unit {i+1}"), OnPos, Quaternion.identity);


            } 

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
