using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
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
    [SerializeField] GameObject caughtVfx;
    GameObject vfxInstance;
    bool bitten = false;
    Coroutine fishCoroutine;
    [SerializeField] GameObject poleModel;

    void Start()
    {
        castUICanvas.SetActive(false);
        maxFill = castFillTransform.rect.width;
        poleModel.SetActive(false);
        waitTime += bobber.GetCastTime();
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

        fish.gameObject.SetActive(false);
        if (fishCoroutine != null) StopCoroutine(fishCoroutine);
        fishCoroutine = StartCoroutine(WaitForFish(waitTime));
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

        FishAppeared();

        float addedTime = Random.Range(1, 4);
        yield return new WaitForSeconds(addedTime);

        FishBites();

        yield return null;
    }

    void FishAppeared()
    {
        Vector2 rand = Random.insideUnitCircle.normalized;
        fish.Spawn(rand, bobber.transform.position);
    }

    void FishBites()
    {
        bitten = true;
        vfxInstance = Instantiate(caughtVfx, bobber.transform.position, Quaternion.identity);
        Destroy(vfxInstance, 2f);
    }


    public bool PullCast()
    {
        if (bitten)
        {
            fish.transform.SetParent(bobber.transform);
            Destroy(vfxInstance);
            SetPoleModelActive(false, 2000);
            return true;
        }
        else
        {
            if (!fish.isActiveAndEnabled) return false;
            StopCoroutine(fishCoroutine);
            fishCoroutine = StartCoroutine(WaitForFish(waitTime));
            fish.Run();
            return false;
        }
    }

    public void PullFish()
    {
        bobber.Pull();
    }

    public void RestartBobberPosition()
    {
        bobber.transform.position = new Vector3(0, 0, 0);
    }

    async public void SetPoleModelActive(bool active, int delay = 0)
    {
        await Task.Delay(delay);
        poleModel.SetActive(active);
    }


}
