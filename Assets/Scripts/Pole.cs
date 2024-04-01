using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pole : MonoBehaviour
{
    [SerializeField] GameObject leftHand;

    private void Start()
    {
        transform.SetParent(leftHand.transform);
    }
}
