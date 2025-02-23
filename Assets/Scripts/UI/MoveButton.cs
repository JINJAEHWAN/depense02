using UnityEngine;
using UnityEngine.EventSystems;
public class MoveButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public Player player;
    [Header("�̵� ���� -1 �Ǵ� 1�� �Է�")]
    public float Dir;
    [HideInInspector] public bool IsPush = false;
    public void OnPointerDown(PointerEventData eventData)
    {
        IsPush = true;
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        IsPush = false;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = FindFirstObjectByType<Player>();
    }
    // Update is called once per frame
    void Update()
    {
        if(IsPush && !(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow) 
            || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)))
        {
            player.Move_(Dir);
        }
    }
}
