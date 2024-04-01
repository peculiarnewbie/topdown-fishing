using System.Collections;
using UnityEngine;

public static class Helpers
{
    public static IEnumerator SetActiveWithDelay(GameObject obj, bool active, float delay)
    {
        yield return new WaitForSeconds(delay);

        obj.SetActive(active);
    }
}