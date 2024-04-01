using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class PlayerAnimationEvents : MonoBehaviour
{
    [SerializeField] UnityEvent onCast;
    [SerializeField] UnityEvent onPull;
    public void OnCast()
    {
        onCast.Invoke();
    }

    public void OnPull()
    {
        onPull.Invoke();
    }
}