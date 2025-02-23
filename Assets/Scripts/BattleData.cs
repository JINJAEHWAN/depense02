using UnityEngine;
using UnityEngine.Events;
using static UnityEngine.GraphicsBuffer;

[System.Serializable]
public struct battleData
{
    public int attackPower;
    public int hp;
    public int MaxHp;
    public float attackSpeed;
    public int cost;
    public float moveSpeed;
}

public class BattleData : MonoBehaviour
{
    public event UnityAction deathAlarm;
    public event UnityAction HPBarChange;
    public battleData data;
   

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
    public void onHit(int dmg)
    {
        data.hp -= dmg;
        HPBarChange?.Invoke();
        if (data.hp <= 0)
        {
            StopAllCoroutines();
            deathAlarm?.Invoke();
            //Destroy(gameObject);
        }
    }
}
