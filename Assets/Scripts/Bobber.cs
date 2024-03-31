using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bobber : MonoBehaviour
{
    Animator animator;
    Vector2 targetPos;
    float lerpTime = 0;
    float castTime = 0.5f;
    [SerializeField] AnimationClip castClip;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        castTime = castClip.length;
        Debug.Log("Cast time: " + castTime);
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (lerpTime >= 1) return;

        lerpTime += Time.deltaTime;
        transform.localPosition = Vector3.Lerp(new Vector3(0, transform.localPosition.y, 0), new Vector3(targetPos.x, transform.localPosition.y, targetPos.y), lerpTime);
    }

    public void Cast(Vector2 target)
    {
        animator.SetTrigger("Cast");
        targetPos = target;
        lerpTime = 0;
    }

    public float GetCastTime()
    {
        return castTime;
    }

}
