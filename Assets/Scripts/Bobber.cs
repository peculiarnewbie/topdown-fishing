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
    bool pulling = false;
    [SerializeField] GameObject bobberModel;

    void Start()
    {
        animator = GetComponent<Animator>();
        castTime = castClip.length;
        bobberModel.SetActive(false);
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (!pulling && lerpTime <= 1)
        {
            lerpTime += Time.deltaTime;
        }
        else if (pulling && lerpTime > 0)
        {
            lerpTime -= Time.deltaTime;
        }
        else return;

        transform.localPosition = Vector3.Lerp(new Vector3(0, transform.localPosition.y, 0), new Vector3(targetPos.x, transform.localPosition.y, targetPos.y), lerpTime);
    }

    public void Cast(Vector2 target)
    {
        bobberModel.SetActive(true);
        animator.SetTrigger("Cast");
        targetPos = target;
        lerpTime = 0;
        pulling = false;
    }

    public void Pull()
    {
        bobberModel.SetActive(false);
        animator.SetTrigger("Pull");
        pulling = true;
        lerpTime = 1;
    }

    public float GetCastTime()
    {
        return castTime;
    }


}
