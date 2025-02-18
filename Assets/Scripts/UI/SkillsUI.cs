using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkillsUI : MonoBehaviour
{

    private Button[] btns = new Button[3];
    [Header("��ư �ȿ� �ִ� Mana �ؽ�Ʈ ������� ����")]
    [SerializeField] private TextMeshProUGUI[] ManaText = new TextMeshProUGUI[3];
    [Header("��ư �ȿ� �ִ� Key �ؽ�Ʈ ������� ����")]
    [SerializeField] private TextMeshProUGUI[] KeyText = new TextMeshProUGUI[3];
    [Header("��ư �ȿ� �ִ� Icon ������� ����")]
    [SerializeField] private Image[] SkillIcon = new Image[3];
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        btns = GetComponentsInChildren<Button>();
        for(int i=0; i<btns.Length; i++)
        {
            int index = i;
            btns[index].onClick.AddListener(delegate { Player.Instance.UseNthSkill(index); });
        }
    }
    public void InitFunction()
    {
        for(int i=0;i<btns.Length; i++)
        {
            SkillIcon[i].sprite = Player.Instance.skills[i].skillspr;
            ManaText[i].text = Player.Instance.skills[i].Mana.ToString();
            KeyText[i].text = Player.Instance.Skillkeys[i].ToString();
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
