using UnityEngine;
using UnityEngine.Events;

public class animEvent : animProperty
{

    public UnityEvent attackAction;
    public void OnAttack()
    {
        attackAction?.Invoke();
    }

}
