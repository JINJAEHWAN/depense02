using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class MovementUI : MonoBehaviour
{
    private MoveButton[] btn = new MoveButton[2];
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        btn = GetComponentsInChildren<MoveButton>();
        btn[0].Dir = -1f;
        btn[1].Dir = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
