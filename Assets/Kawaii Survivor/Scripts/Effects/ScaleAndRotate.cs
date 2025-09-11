using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(RectTransform))]
public class ScaleAndRotate : MonoBehaviour, IPointerEnterHandler
{
    [Header("Elements")]
    private RectTransform rt;

    private void Awake() => rt = GetComponent<RectTransform>();

    
    public void OnPointerEnter(PointerEventData eventData)
    {
        LeanTween.cancel(gameObject);

        rt.localScale = Vector2.one;
        LeanTween.scale(rt, Vector2.one * 1.1f, 1)
            .setEase(LeanTweenType.punch)
            .setIgnoreTimeScale(true);

        rt.rotation = Quaternion.identity;
        int sign = (int)Mathf.Sign(Random.Range(-1f, 1f));
        LeanTween.rotateAround(rt, Vector3.forward, 15 * sign, 1)
            .setEase(LeanTweenType.punch)
            .setIgnoreTimeScale(true);
    }
}
