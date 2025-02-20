using UnityEngine;

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
        if (data.hp <= 0)
        {
            StopAllCoroutines();
            Destroy(gameObject);
        }
    }
}
