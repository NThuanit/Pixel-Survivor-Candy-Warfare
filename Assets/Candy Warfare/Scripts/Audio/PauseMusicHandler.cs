using UnityEngine;

public class PauseMusicHandler : MonoBehaviour
{
    // Khi Panel Pause hiện lên -> Tạm dừng nhạc
    private void OnEnable()
    {
        if (AudioManager.instance != null)
        {
            AudioManager.instance.PauseMusic();
        }
    }

    // Khi Panel Pause tắt đi (Resume game) -> Nhạc hát tiếp
    private void OnDisable()
    {
        if (AudioManager.instance != null)
        {
            AudioManager.instance.ResumeMusic();
        }
    }
}