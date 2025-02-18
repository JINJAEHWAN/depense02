using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Player : BattleData
{
    public static Player Instance;
    [HideInInspector] public Animator animator;

    [SerializeField] float CurFood, FoodRegen, MaxFood, CurMana, ManaRegen, MaxMana;

    [Header("플레이어 이동 범위")]
    [SerializeField] float left;
    [SerializeField] float right;

    [Header("카메라 이동 범위")]
    [SerializeField] float leftC;
    [SerializeField] float rightC;

    [Header("스킬 시전할 위치 Create Empty 해서 등록\n(바꾸고 싶으면 등록되어 있는 오브젝트 위치 수정)")]
    [SerializeField] Transform skillposition;

    [Header("HP바 표시할 위치 Create Empty 해서 등록\n(바꾸고 싶으면 등록되어 있는 오브젝트 위치 수정)")]
    [SerializeField] Transform hpbarposition;

    [Header("발동할 스킬 resources에서 찾아서 등록")]
    public Skills[] skills;

    [HideInInspector] public KeyCode[] Skillkeys = new KeyCode[3];

    [Header("UI 만들고 Slider, Text 등록")]
    [SerializeField] Slider FoodSlider;
    [SerializeField] Slider ManaSlider;
    [SerializeField] TextMeshProUGUI FoodText;
    [SerializeField] TextMeshProUGUI ManaText;
    [SerializeField] Slider playerHPSliderTop, playerHPSliderField;

    [Header("UI 만들고 이동 버튼 등록")]
    [SerializeField] MoveButton[] Movebtn = new MoveButton[2];
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Instance = this;
        animator = GetComponentInChildren<Animator>();
        data.hp = data.MaxHp;
        //스킬 키 설정. 수정하고 싶으면 여기서 수정.
        Skillkeys[0] = KeyCode.Q;
        Skillkeys[1] = KeyCode.W;
        Skillkeys[2] = KeyCode.E;

        SkillsUI sUI = FindFirstObjectByType<SkillsUI>();
        for(int i=0; i<3; i++)
        {
            sUI.InitFunction();
        }

    }

    // Update is called once per frame
    void Update()
    {
        float moveDir = 0;
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            moveDir -= 1;
        }
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            moveDir += 1;
        }
        if (!Mathf.Approximately(moveDir, 0f))
        {
            Move_(moveDir);
        }
        else if (!Movebtn[0].IsPush && !Movebtn[1].IsPush)
        {
            animator.SetBool("IsWalk", false);
        }
        


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

        //설정한 키로 스킬 발동.
        //원본 게임도 쿨타임이 없으므로 여기도 없음.
        for(int i=0; i<3; i++)
        {
            if (Input.GetKeyDown(Skillkeys[i]))
            {
                UseNthSkill(i);
            }
        }
        playerHPSliderField.value = playerHPSliderTop.value = (float)data.hp / data.MaxHp;
    }
    public void Move_(float dir)
    {
        transform.Translate(dir * Vector3.right * data.moveSpeed * Time.deltaTime);
        animator.SetBool("IsWalk", true);
    }
    public void UseNthSkill(int n)
    {
        if (skills[n].Mana < CurMana)
        {
            animator.SetTrigger("OnAttack");
            Instantiate(skills[n], skillposition.position, Quaternion.identity);
            CurMana -= skills[n].Mana;
        }
    }

    private void LateUpdate()
    {
        playerHPSliderField.transform.position = Camera.main.WorldToScreenPoint(hpbarposition.position);
        Camera.main.transform.position = new Vector3(Mathf.Clamp(transform.position.x, leftC, rightC),
            Camera.main.transform.position.y, Camera.main.transform.position.z);
    }
}
