using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class BumpyButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerExitHandler
{
    [Header("Elements")]
    private Button button;

    [Header("Audio")]
    [SerializeField] private AudioClip clickSound; // Kéo file âm thanh nút bấm vào đây

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        button = GetComponent<Button>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (!button.interactable)
            return;

        // --- GỌI ÂM THANH ---
        if (AudioManager.instance != null)
        {
            AudioManager.instance.PlaySFX(clickSound);
        }
        // --------------------

        LeanTween.cancel(button.gameObject);

        LeanTween.scale(gameObject, new Vector2(1.1f, 0.9f), 0.6f)
            .setEase(LeanTweenType.easeOutElastic)
            .setIgnoreTimeScale(true);

    }

    // ... (Các hàm OnPointerUp, OnPointerExit giữ nguyên như cũ) ...
    public void OnPointerUp(PointerEventData eventData)
    {
        if (!button.interactable) return;
        LeanTween.cancel(button.gameObject);
        LeanTween.scale(gameObject, Vector2.one, 0.6f).setEase(LeanTweenType.easeOutElastic).setIgnoreTimeScale(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (!button.interactable) return;
        LeanTween.cancel(button.gameObject);
        LeanTween.scale(gameObject, Vector2.one, 0.6f).setEase(LeanTweenType.easeOutElastic).setIgnoreTimeScale(true);
    }
}