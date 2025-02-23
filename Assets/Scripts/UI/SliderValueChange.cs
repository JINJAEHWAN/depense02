using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SliderValueChange : MonoBehaviour
{
    public static SliderValueChange Instance;
    public Player player;
    [SerializeField] Slider FoodSlider;
    [SerializeField] Slider ManaSlider;
    [SerializeField] TextMeshProUGUI FoodText;
    [SerializeField] TextMeshProUGUI ManaText;
    [SerializeField] Slider playerHPSliderTop, playerHPSliderField;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Instance = this;   
        player = FindFirstObjectByType<Player>();

        FoodSliderValueChange();
        ManaSliderValueChange();
        PlayerHPSliderValueChange();
    }

    // Update is called once per frame
    void Update()
    {
        playerHPSliderField.transform.position = Camera.main.WorldToScreenPoint(player.hpbarposition.position);
    }

    public void FoodSliderValueChange()
    {
        FoodSlider.value = player.CurFood / player.MaxFood;
        FoodText.text = player.CurFood.ToString("F0") + " / " + player.MaxFood.ToString("F0");

    }
    public void ManaSliderValueChange()
    {
        ManaSlider.value = player.CurMana / player.MaxMana;
        ManaText.text = player.CurMana.ToString("F0") + " / " + player.MaxMana.ToString("F0");
    }
    //플레이어 HP 변동할 때마다 갖다 쓰시오.
    public void PlayerHPSliderValueChange()
    {
        playerHPSliderField.value = playerHPSliderTop.value = (float)player.data.hp / player.data.MaxHp;
    }
}
