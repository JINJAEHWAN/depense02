using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Player : BattleData
{
    //다른 스크립트에서 필요하면 Player.Instance로 접근하시오.
    public static Player Instance;
    [HideInInspector] public Animator animator;

    public float CurFood, FoodRegen, MaxFood, CurMana, ManaRegen, MaxMana;
    private float delta = 0f;
    [Header("플레이어 이동 범위")]
    [SerializeField] float left;
    [SerializeField] float right;

    [Header("카메라 이동 범위")]
    [SerializeField] float leftC;
    [SerializeField] float rightC;

    [Header("스킬 시전할 위치 Create Empty 해서 등록\n(바꾸고 싶으면 등록되어 있는 오브젝트 위치 수정)")]
    [SerializeField] Transform skillposition;

    [Header("HP바 표시할 위치 Create Empty 해서 등록\n(바꾸고 싶으면 등록되어 있는 오브젝트 위치 수정)")]
    public Transform hpbarposition;

    [Header("발동할 스킬 resources에서 찾아서 등록")]
    public Skills[] skills;

    [HideInInspector]
    public KeyCode[] Skillkeys;

    [Header("UI 만들고 이동 버튼 등록")]
    [SerializeField] MoveButton[] Movebtn = new MoveButton[2];

    private SliderValueChange sValueChange;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Instance = this;
        animator = GetComponentInChildren<Animator>();

        Skillkeys = new KeyCode[3] {
        KeyCode.Q, KeyCode.W, KeyCode.E
        };

        SkillsUI sUI = FindFirstObjectByType<SkillsUI>();
        for(int i=0; i<3; i++)
        {
            sUI.InitFunction();
        }
        sValueChange = FindFirstObjectByType<SliderValueChange>();
        sValueChange.Start_Func();
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
        else 
        {
            bool isMove = false;
            MoveButton[] m =  FindObjectsByType<MoveButton>(FindObjectsSortMode.None);
            foreach (MoveButton b in m)
            {
                if (b.IsPush) isMove = true;
            }
            if (!isMove) animator.SetBool("IsWalk", false);

        }


        delta += Time.deltaTime;
        if(delta > 0.25f)
        {
            delta -= 0.25f;
            if (CurFood < MaxFood) CurFood += FoodRegen * 0.25f;
            if (CurFood > MaxFood) CurFood = MaxFood;
            if (CurMana < MaxMana) CurMana += ManaRegen * 0.25f;
            if (CurMana > MaxMana) CurMana = MaxMana;
            sValueChange.FoodSliderValueChange();
            sValueChange.ManaSliderValueChange();
        }
        //식량, 마나 회복 및 ui에 값 표시


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
            sValueChange.ManaSliderValueChange();
        }
    }

    private void LateUpdate()
    {
        Camera.main.transform.position = new Vector3(Mathf.Clamp(transform.position.x, leftC, rightC),
            Camera.main.transform.position.y, Camera.main.transform.position.z);
    }
}
