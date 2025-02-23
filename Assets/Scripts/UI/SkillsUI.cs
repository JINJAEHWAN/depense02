using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkillsUI : MonoBehaviour
{
    private Player player;
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
            btns[index].onClick.AddListener(delegate { player.UseNthSkill(index); });
        }
    }
    public void InitFunction()
    {
        player = FindFirstObjectByType<Player>();
        for (int i=0;i<btns.Length; i++)
        {
            SkillIcon[i].sprite =  player.skills[i].skillspr;
            ManaText[i].text = player.skills[i].Mana.ToString();
            KeyText[i].text = player.Skillkeys[i].ToString();
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
