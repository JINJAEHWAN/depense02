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
    //�浹 ó���� ���߿�.
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Destroy(gameObject, DestroyTime);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
