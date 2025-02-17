using UnityEngine;

public class Skills : MonoBehaviour
{
    [Header("쓸 아이콘 연결")]
    public Sprite skillspr;
    [Header("소모할 마나 입력")]
    public float Mana;
    [Header("몇 초 후에 삭제할 건지 입력 ")]
    [SerializeField] private float DestroyTime;
    //충돌 처리는 나중에.
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
