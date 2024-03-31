using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastController : MonoBehaviour
{
    [SerializeField] GameObject castUICanvas;
    [SerializeField] RectTransform castFillTransform;
    float castFillSize = 0;
    float maxFill = 200f;

    [SerializeField] float maxRange = 10f;
    [SerializeField] float minRange = 2f;
    [SerializeField] float castSpeed = 100;
    [SerializeField] Bobber bobber;
    [SerializeField] Transform fishTransform;
    [SerializeField] float waitTime = 1;

    [SerializeField] Fish fish;
    bool bitten = false;

    void Start()
    {
        castUICanvas.SetActive(false);
        maxFill = castFillTransform.rect.width;
    }

    public void StartPowerUp()
    {
        castUICanvas.SetActive(true);
        castFillSize = 0;
        castFillTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, castFillSize);
    }

    public void StartCasting()
    {
        castUICanvas.SetActive(false);
        bobber.Cast(new Vector2(0, minRange + ((maxRange - minRange) * castFillSize / maxFill)));

        waitTime += bobber.GetCastTime();
        StartCoroutine(WaitForFish(waitTime));
    }

    public void PowerUp()
    {

        if (castFillSize == maxFill) return;

        castFillSize += Time.deltaTime * castSpeed;
        if (castFillSize > maxFill) castFillSize = maxFill;
        castFillTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, castFillSize);
    }

    IEnumerator WaitForFish(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);


        Vector2 rand = Random.insideUnitCircle.normalized;
        fish.Spawn(rand, bobber.transform.position);

        float addedTime = Random.Range(1, 4);
        yield return new WaitForSeconds(addedTime);

        FishBites();


        yield return null;
    }

    void FishAppeared(int degree)
    {
    }

    void FishBites()
    {
        bitten = true;
    }


    public bool PullCast()
    {
        if (bitten) return true;
        else return false;
    }
}
