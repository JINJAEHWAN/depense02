using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SliderValueChange : MonoBehaviour
{
    [SerializeField] Slider FoodSlider;
    [SerializeField] Slider ManaSlider;
    [SerializeField] TextMeshProUGUI FoodText;
    [SerializeField] TextMeshProUGUI ManaText;
    [SerializeField] Slider playerHPSliderTop, playerHPSliderField;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        FoodSlider.value = Player.Instance.CurFood / Player.Instance.MaxFood;
        ManaSlider.value = Player.Instance.CurMana / Player.Instance.MaxMana;
        FoodText.text = Player.Instance.CurFood.ToString("F0") + " / " + Player.Instance.MaxFood.ToString("F0");
        ManaText.text = Player.Instance.CurMana.ToString("F0") + " / " + Player.Instance.MaxMana.ToString("F0");
        playerHPSliderField.value = playerHPSliderTop.value = (float)Player.Instance.data.hp / Player.Instance.data.MaxHp;
        playerHPSliderField.transform.position = Camera.main.WorldToScreenPoint(Player.Instance.hpbarposition.position);

    }
}
