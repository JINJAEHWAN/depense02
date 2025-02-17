using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Player : BattleData
{
    public Animator animator;


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
    [SerializeField] private Skills[] skills;

    [Header("UI ����� Slider, Text ���")]
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

        //QWE�� ��ų �ߵ�.
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
        //3�� ��ų ���� �� ����.
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
