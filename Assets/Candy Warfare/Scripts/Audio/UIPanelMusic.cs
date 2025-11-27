using UnityEngine;

public class UIPanelMusic : MonoBehaviour
{
    [Header("Cài đặt Nhạc")]
    [SerializeField] private AudioClip musicClip;

    [Range(0f, 1f)]
    [SerializeField] private float volume = 1f; // Kéo cái này để chỉnh to nhỏ cho từng màn

    private void OnEnable()
    {
        if (AudioManager.instance != null)
        {
            // Gọi hàm PlayMusic mới với tham số volume
            AudioManager.instance.PlayMusic(musicClip, volume);
        }
    }
}