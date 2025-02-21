using UnityEngine;
using UnityEngine.Events;

public class animEvent : animProperty
{

    public UnityEvent attackAction;
    public UnityEvent deadAction;
    public void OnAttack()
    {
        attackAction?.Invoke();
    }
    public void OnDead()
    {
        deadAction?.Invoke();
    }
}
