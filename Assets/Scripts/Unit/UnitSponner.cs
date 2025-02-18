using UnityEngine;

public class UnitSponner : MonoBehaviour
{

    [Header("�̰��� ���� x�� ��ġ ���ϱ�.")]
    [SerializeField] float onX =0;

    string objName = "clone";

    //������
    int cpu;
    //y�� ��ġ�� ���� 3����
    //random �Ἥ y ��ġ 3���� �� �� ������ ����
    //-2 0 2
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
