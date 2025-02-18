using UnityEngine;
using System.Collections;
using UnityEngine.InputSystem.Controls;
public class Unit : BattleData
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
        while (true)
        {
            transform.position += (Vector3)(Vector2.right * data.moveSpeed * Time.deltaTime);
            yield return null;
        }
    }

}
