using UnityEditor.Networking.PlayerConnection;
using UnityEngine;

public class UnitSponner : MonoBehaviour
{
    private Player player;
    [Header("이곳에 유닛 x축 위치 정하기.")]
    [SerializeField] float onX =0;

    //랜덤용
    float cpu;
    Vector3 OnPos;
    KeyCode[] alphas = new KeyCode[8] {
        KeyCode.Alpha1, KeyCode.Alpha2, 
        KeyCode.Alpha3, KeyCode.Alpha4, 
        KeyCode.Alpha5, KeyCode.Alpha6, 
        KeyCode.Alpha7, KeyCode.Alpha8
    };

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = FindFirstObjectByType<Player>();

    }

    // Update is called once per frame
    void Update()
    {
        for(int i=0; i<8; i++)
        {
            if (Input.GetKeyDown(alphas[i]))
            {
                caseCpu();
                if (Resources.Load<Unit>($"Unit/Unit {i + 1}").data.cost <= player.CurFood)
                {
                    Unit u = Instantiate(Resources.Load<Unit>($"Unit/Unit {i + 1}"), OnPos, Quaternion.identity);
                    player.CurFood -= u.data.cost;
                    SliderValueChange.Instance.FoodSliderValueChange();
                }
            } 
        }
    }

    public void clickUnit(int a)
    {
        caseCpu();
        if(Resources.Load<Unit>($"Unit/Unit {a}").data.cost <= player.CurFood)
        {
            Unit u = Instantiate(Resources.Load<Unit>($"Unit/Unit {a}"), OnPos, Quaternion.identity);
            player.CurFood -= u.data.cost;
            SliderValueChange.Instance.FoodSliderValueChange();
        }
    }




    void caseCpu()
    {
        //-1.0에서 1.0까지 랜덤한 값
        cpu = Random.Range(-1.0f, 1.0f);

        OnPos = new Vector3(onX, cpu, 0);

    }
}
