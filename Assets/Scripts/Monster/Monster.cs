using System.Collections;
using UnityEngine;

public class Monster : BattleData
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(move());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator move()
    {
        float delta = Time.deltaTime * 2.0f;
        transform.parent = null;
        while (true)
        {
            transform.position *= delta * Vector2.left ;
            yield return null;
        }
    }
}
