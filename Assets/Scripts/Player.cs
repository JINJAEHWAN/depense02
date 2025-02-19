using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Player : BattleData
{
    //�ٸ� ��ũ��Ʈ���� �ʿ��ϸ� Player.Instance�� �����Ͻÿ�.
    public static Player Instance;
    [HideInInspector] public Animator animator;

    public float CurFood, FoodRegen, MaxFood, CurMana, ManaRegen, MaxMana;
    private float delta = 0f;
    [Header("�÷��̾� �̵� ����")]
    [SerializeField] float left;
    [SerializeField] float right;

    [Header("ī�޶� �̵� ����")]
    [SerializeField] float leftC;
    [SerializeField] float rightC;

    [Header("��ų ������ ��ġ Create Empty �ؼ� ���\n(�ٲٰ� ������ ��ϵǾ� �ִ� ������Ʈ ��ġ ����)")]
    [SerializeField] Transform skillposition;

    [Header("HP�� ǥ���� ��ġ Create Empty �ؼ� ���\n(�ٲٰ� ������ ��ϵǾ� �ִ� ������Ʈ ��ġ ����)")]
    public Transform hpbarposition;

    [Header("�ߵ��� ��ų resources���� ã�Ƽ� ���")]
    public Skills[] skills;

    [HideInInspector]
    public KeyCode[] Skillkeys;

    [Header("UI ����� �̵� ��ư ���")]
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
        //�ķ�, ���� ȸ�� �� ui�� �� ǥ��


        transform.position = new Vector3(Mathf.Clamp(transform.position.x, left, right), transform.position.y, transform.position.z);
        //������ Ű�� ��ų �ߵ�.
        //���� ���ӵ� ��Ÿ���� �����Ƿ� ���⵵ ����.
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
