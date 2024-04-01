using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Fish : MonoBehaviour
{

    [SerializeField] Material whiteMat;
    [SerializeField] Material pinkMat;

    Coroutine moveCoroutine;

    public void Spawn(Vector2 offset, Vector3 targetPosition)
    {
        Vector3 correctedTargetPosition = new Vector3(targetPosition.x, transform.position.y, targetPosition.z);
        transform.position = new Vector3(targetPosition.x + offset.x, transform.position.y, targetPosition.z + offset.y);

        transform.LookAt(correctedTargetPosition);
        transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);

        gameObject.SetActive(true);

        StartCoroutine(FadeIn());

        if (moveCoroutine != null) StopCoroutine(moveCoroutine);
        moveCoroutine = StartCoroutine(MoveTowards(correctedTargetPosition));
    }

    public void Run()
    {
        Vector3 runTarget = new Vector3(transform.position.x, transform.position.y, transform.position.z + 12f);
        transform.LookAt(runTarget);

        if (moveCoroutine != null) StopCoroutine(moveCoroutine);
        moveCoroutine = StartCoroutine(MoveTowards(runTarget, 10f));
    }

    IEnumerator FadeIn()
    {
        float opacity = 0;

        while (opacity < 1)
        {
            opacity += Time.deltaTime;

            whiteMat.color = SetMatOpacity(whiteMat, opacity);
            pinkMat.color = SetMatOpacity(pinkMat, opacity);

            yield return null;
        }

        yield return null;
    }

    IEnumerator MoveTowards(Vector3 targetPosition, float speed = 0.2f)
    {
        while (Vector3.Distance(transform.position, targetPosition) > 0.5f)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, Time.deltaTime * speed);
            yield return null;
        }
        yield return null;
    }

    Color SetMatOpacity(Material mat, float opacity)
    {
        Color color = mat.color;
        color.a = opacity;
        return color;
    }
}
