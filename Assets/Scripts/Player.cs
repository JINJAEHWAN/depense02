using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Player : BattleData
{
    public static Player Instance;
    public Animator animator;
    public Rigidbody2D rigid;

    [SerializeField] private float CurFood, FoodRegen, MaxFood, CurMana, ManaRegen, MaxMana;

    [Header("�÷��̾� �̵� ����")]
    [SerializeField] private float left;
    [SerializeField] private float right;

    [Header("ī�޶� �̵� ����")]
    [SerializeField] private float leftC;
    [SerializeField] private float rightC;

    [Header("��ų ������ ��ġ Create Empty �ؼ� ���\n(�ٲٰ� ������ ��ϵǾ� �ִ� ������Ʈ ��ġ ����)")]
    [SerializeField] private Transform skillposition;

    [Header("�ߵ��� ��ų resources���� ã�Ƽ� ���")]
    public Skills[] skills;

    public KeyCode[] Skillkeys = new KeyCode[3];

    [Header("UI ����� Slider, Text ���")]
    [SerializeField] Slider FoodSlider;
    [SerializeField] Slider ManaSlider;
    [SerializeField] TextMeshProUGUI FoodText;
    [SerializeField] TextMeshProUGUI ManaText;

    [Header("UI ����� �̵� ��ư ���")]

    [SerializeField] private MoveButton[] Movebtn = new MoveButton[2];
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Instance = this;
        animator = GetComponentInChildren<Animator>();
        rigid = GetComponent<Rigidbody2D>();
        //��ų Ű ����.

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
        


        //�ķ�, ���� ȸ�� �� ui�� �� ǥ��
        if (CurFood < MaxFood) CurFood += FoodRegen * Time.deltaTime;
        else if (CurFood > MaxFood) CurFood = MaxFood;
        if (CurMana < MaxMana) CurMana += ManaRegen * Time.deltaTime;
        else if (CurMana > MaxMana) CurMana = MaxMana;
        FoodSlider.value = CurFood / MaxFood;
        ManaSlider.value = CurMana / MaxMana;
        FoodText.text = CurFood.ToString("F0") + " / " + MaxFood.ToString("F0");
        ManaText.text = CurMana.ToString("F0") + " / " + MaxMana.ToString("F0");

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
        }
    }

    private void LateUpdate()
    {
        Camera.main.transform.position = new Vector3(Mathf.Clamp(transform.position.x, leftC, rightC),
            Camera.main.transform.position.y, Camera.main.transform.position.z);
    }
}
