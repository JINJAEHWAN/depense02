using UnityEngine;
using UnityEngine.EventSystems;
public class MoveButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [Header("이동 방향 -1 또는 1로 입력")]
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
        
    }

    // Update is called once per frame
    void Update()
    {
        if(IsPush && !(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow) 
            || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)))
        {
            Player.Instance.Move_(Dir);
        }
    }
}
