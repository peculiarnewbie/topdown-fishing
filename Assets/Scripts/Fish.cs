using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Fish : MonoBehaviour
{

    [SerializeField] Material whiteMat;
    [SerializeField] Material pinkMat;

    public void Spawn(Vector2 offset, Vector3 targetPosition)
    {
        transform.position = new Vector3(targetPosition.x + offset.x, transform.position.y, targetPosition.z + offset.y);

        transform.LookAt(targetPosition);
        transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);

        gameObject.SetActive(true);

        StartCoroutine(FadeIn());
    }

    IEnumerator FadeIn()
    {
        float opacity = 0;

        while (opacity < 1)
        {
            opacity += Time.deltaTime;

            whiteMat.color = setMatOpacity(whiteMat, opacity);
            pinkMat.color = setMatOpacity(pinkMat, opacity);

            yield return null;
        }

        yield return null;
    }

    Color setMatOpacity(Material mat, float opacity)
    {
        Color color = mat.color;
        color.a = opacity;
        return color;
    }
}
