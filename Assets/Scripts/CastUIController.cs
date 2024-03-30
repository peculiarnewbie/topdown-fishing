using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastUIController : MonoBehaviour
{
    [SerializeField] GameObject castUICanvas;
    [SerializeField] RectTransform castFillTransform;
    float castFillSize = 0;
    float maxFill = 200f;

    [SerializeField] float castPower = 100;

    void Start()
    {
        castUICanvas.SetActive(false);
        maxFill = castFillTransform.rect.width;
    }

    public void StartCasting()
    {
        castUICanvas.SetActive(true);
        castFillSize = 0;
        castFillTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, castFillSize);
    }

    public void StopCasting()
    {
        castUICanvas.SetActive(false);
    }

    public void PowerUp()
    {

        if (castFillSize == maxFill) return;

        castFillSize += Time.deltaTime * castPower;
        if (castFillSize > maxFill) castFillSize = maxFill;
        castFillTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, castFillSize);
    }
}
