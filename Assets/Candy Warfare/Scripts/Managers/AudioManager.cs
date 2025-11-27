using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [Header("Sources (Tự động tạo)")]
    private AudioSource musicSource; // Loa Nhạc nền
    private AudioSource sfxSource;   // Loa Hiệu ứng

    // Biến lưu trạng thái
    public bool IsSFXOn { get; private set; } = true;
    public bool IsMusicOn { get; private set; } = true;

    // Biến volume để tính toán
    private float masterMusicVolume = 1f;
    private float currentClipVolume = 1f;

    private void Awake()
    {
        // 1. Setup Singleton
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            InitializeAudioSources();
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        // --- 2. ĐĂNG KÝ SỰ KIỆN TỪ SETTINGS MANAGER (QUAN TRỌNG) ---
        // Đây là phần bị thiếu lúc nãy khiến nút bấm không ăn
        SettingsManager.onSFXStateChanged += SFXStateChangedCallback;
        SettingsManager.onMusicStateChanged += MusicStateChangedCallback;
    }

    private void OnDestroy()
    {
        if (instance == this)
        {
            SettingsManager.onSFXStateChanged -= SFXStateChangedCallback;
            SettingsManager.onMusicStateChanged -= MusicStateChangedCallback;
        }
    }

    // --- 3. HÀM XỬ LÝ KHI BẤM NÚT ON/OFF ---
    private void MusicStateChangedCallback(bool isOn)
    {
        IsMusicOn = isOn;
        if (musicSource != null)
        {
            musicSource.mute = !isOn; // Nếu Off -> Mute loa nhạc
        }
    }

    private void SFXStateChangedCallback(bool isOn)
    {
        IsSFXOn = isOn;
        if (sfxSource != null)
        {
            sfxSource.mute = !isOn; // Nếu Off -> Mute loa SFX
        }
    }

    // --- 4. KHỞI TẠO LOA ---
    private void InitializeAudioSources()
    {
        GameObject musicObj = new GameObject("MusicSource");
        musicObj.transform.SetParent(transform);
        musicSource = musicObj.AddComponent<AudioSource>();
        musicSource.loop = true;

        GameObject sfxObj = new GameObject("SFXSource");
        sfxObj.transform.SetParent(transform);
        sfxSource = sfxObj.AddComponent<AudioSource>();
    }

    // --- 5. CÁC HÀM PHÁT NHẠC (GIỮ NGUYÊN) ---
    public void PlayMusic(AudioClip clip, float volume = 1f)
    {
        if (musicSource == null || clip == null) return;

        currentClipVolume = volume;

        // Nếu bài mới trùng bài cũ -> chỉ update volume, không phát lại
        if (musicSource.clip == clip && musicSource.isPlaying)
        {
            UpdateMusicSourceVolume();
            return;
        }

        musicSource.clip = clip;
        UpdateMusicSourceVolume();
        musicSource.Play();

        // Cập nhật lại trạng thái Mute theo biến IsMusicOn hiện tại
        musicSource.mute = !IsMusicOn;
    }

    public void PlaySFX(AudioClip clip, float volume = 1f)
    {
        // Chỉ phát nếu có loa và có clip
        if (sfxSource == null || clip == null) return;

        // Cập nhật trạng thái mute trước khi phát để chắc chắn
        sfxSource.mute = !IsSFXOn;
        sfxSource.PlayOneShot(clip, volume);
    }

    public void PauseMusic()
    {
        if (musicSource != null) musicSource.Pause();
    }

    public void ResumeMusic()
    {
        if (musicSource != null) musicSource.UnPause();
    }

    // --- LOGIC VOLUME ---
    private void UpdateMusicSourceVolume()
    {
        if (musicSource != null)
        {
            musicSource.volume = masterMusicVolume * currentClipVolume;
        }
    }
}