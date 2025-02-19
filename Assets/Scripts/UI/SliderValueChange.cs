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
    public void Start_Func()
    {
        FoodSliderValueChange();
        ManaSliderValueChange();
        PlayerHPSliderValueChange();
    }

    // Update is called once per frame
    void Update()
    {
        playerHPSliderField.transform.position = Camera.main.WorldToScreenPoint(Player.Instance.hpbarposition.position);
    }

    public void FoodSliderValueChange()
    {
        FoodSlider.value = Player.Instance.CurFood / Player.Instance.MaxFood;
        FoodText.text = Player.Instance.CurFood.ToString("F0") + " / " + Player.Instance.MaxFood.ToString("F0");

    }
    public void ManaSliderValueChange()
    {
        ManaSlider.value = Player.Instance.CurMana / Player.Instance.MaxMana;
        ManaText.text = Player.Instance.CurMana.ToString("F0") + " / " + Player.Instance.MaxMana.ToString("F0");
    }
    //플레이어 HP 변동할 때마다 갖다 쓰시오.
    public void PlayerHPSliderValueChange()
    {
        playerHPSliderField.value = playerHPSliderTop.value = (float)Player.Instance.data.hp / Player.Instance.data.MaxHp;
    }
}
