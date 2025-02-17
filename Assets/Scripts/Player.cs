using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Player : BattleData
{
    public Animator animator;


    [SerializeField] private float CurFood, FoodRegen, MaxFood, CurMana, ManaRegen, MaxMana;

    [Header("플레이어 이동 범위")]
    [SerializeField] private float left;
    [SerializeField] private float right;

    [Header("카메라 이동 범위")]
    [SerializeField] private float leftC;
    [SerializeField] private float rightC;

    [Header("스킬 시전할 위치 Create Empty 해서 등록\n(바꾸고 싶으면 등록되어 있는 오브젝트 위치 수정)")]
    [SerializeField] private Transform skillposition;

    [Header("발동할 스킬 resources에서 찾아서 등록")]
    [SerializeField] private Skills[] skills;

    [Header("UI 만들고 Slider, Text 등록")]
    [SerializeField] Slider FoodSlider;
    [SerializeField] Slider ManaSlider;
    [SerializeField] TextMeshProUGUI FoodText;
    [SerializeField] TextMeshProUGUI ManaText;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float move = Input.GetAxis("Horizontal");
        transform.Translate(move * Vector3.right * Time.deltaTime * data.moveSpeed);
        animator.SetBool("IsWalk", !Mathf.Approximately(move, 0f));

        //식량, 마나 회복 및 ui에 값 표시
        if (CurFood < MaxFood) CurFood += FoodRegen * Time.deltaTime;
        else if (CurFood > MaxFood) CurFood = MaxFood;
        if (CurMana < MaxMana) CurMana += ManaRegen * Time.deltaTime;
        else if (CurMana > MaxMana) CurMana = MaxMana;
        FoodSlider.value = CurFood / MaxFood;
        ManaSlider.value = CurMana / MaxMana;
        FoodText.text = CurFood.ToString("F0") + " / " + MaxFood.ToString("F0");
        ManaText.text = CurMana.ToString("F0") + " / " + MaxMana.ToString("F0");

        transform.position = new Vector3(Mathf.Clamp(transform.position.x, left, right), transform.position.y, transform.position.z);

        //QWE로 스킬 발동.
        if (Input.GetKeyDown(KeyCode.Q) && skills[0].Mana < CurMana)
        {
            animator.SetTrigger("OnAttack");
            Instantiate(skills[0], skillposition.position, Quaternion.identity);
            CurMana -= skills[0].Mana;
        }
        if (Input.GetKeyDown(KeyCode.W) && skills[1].Mana < CurMana)
        {
            animator.SetTrigger("OnAttack");
            Instantiate(skills[1], skillposition.position, Quaternion.identity);
            CurMana -= skills[1].Mana;
        }
        //3번 스킬 아직 안 만듦.
        if (Input.GetKeyDown(KeyCode.E))
        {

        }
    }

    private void LateUpdate()
    {
        Camera.main.transform.position = new Vector3(Mathf.Clamp(transform.position.x, leftC, rightC),
            Camera.main.transform.position.y, Camera.main.transform.position.z);
    }
}
