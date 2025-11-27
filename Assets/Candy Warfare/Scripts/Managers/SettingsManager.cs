using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Networking;
using Tabsil.Sijil;

public class SettingsManager : MonoBehaviour, IWantToBeSaved
{
    [Header("Elements")]
    [SerializeField] private Button sfxButton;
    [SerializeField] private Button musicButton;
    [SerializeField] private Button privacyPolicyButton;
    [SerializeField] private Button askButton;
    [SerializeField] private Button creditsButton;
    [SerializeField] private GameObject creditsPanel;

    [Header("Settings")]
    [SerializeField] private Color onColor;
    [SerializeField] private Color offColor;

    [Header("Data")]
    private bool sfxState;
    private bool musicState;

    [Header("Actions")]
    public static Action<bool> onSFXStateChanged;
    public static Action<bool> onMusicStateChanged;

    private void Awake()
    {
        sfxButton.onClick.RemoveAllListeners();
        sfxButton.onClick.AddListener(SFXButtonCallback);

        musicButton.onClick.RemoveAllListeners();
        musicButton.onClick.AddListener(MusicButtonCallback);

        privacyPolicyButton.onClick.RemoveAllListeners();
        privacyPolicyButton.onClick.AddListener(PrivacyPolicyButtonCallback);

        askButton.onClick.RemoveAllListeners();
        askButton.onClick.AddListener(AskButtonCallback);

        creditsButton.onClick.RemoveAllListeners();
        creditsButton.onClick.AddListener(CreditsButtonCallback);
    }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        HideCreditsPanel();

        onSFXStateChanged?.Invoke(sfxState);
        onMusicStateChanged?.Invoke(musicState);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SFXButtonCallback()
    {
        sfxState  = !sfxState;

        UpdateSFXVisuals();

        Save();

        onSFXStateChanged?.Invoke(sfxState);    
    }

    private void UpdateSFXVisuals()
    {
        if (sfxState)
        {
            sfxButton.image.color = onColor;
            sfxButton.GetComponentInChildren<TextMeshProUGUI>().text = "ON";
        }
        else
        {
            sfxButton.image.color = offColor;
            sfxButton.GetComponentInChildren<TextMeshProUGUI>().text = "OFF";

        }
    }

    private void MusicButtonCallback()
    {
        musicState = !musicState;

        UpdateMusicVisuals();


        Save();
        onMusicStateChanged?.Invoke(musicState);
    }

    private void UpdateMusicVisuals()
    {
        if (musicState)
        {
            musicButton.image.color = onColor;
            musicButton.GetComponentInChildren<TextMeshProUGUI>().text = "ON";
        }
        else
        {
            musicButton.image.color = offColor;
            musicButton.GetComponentInChildren<TextMeshProUGUI>().text = "OFF";

        }
    }

    private void PrivacyPolicyButtonCallback()
    {
        Application.OpenURL("");
    }

    private void AskButtonCallback()
    {
        string email = "nbthuan.icloud@gmail.com";
        string subject = "Help";
        string body = "Hey ! I Need help with this....";

        Application.OpenURL("mailto: " + email + "?subject=" + subject + "&body=" + body);
    }

    private string MyEscapeURL(string s)
    {
        return UnityWebRequest.EscapeURL(s).Replace("+", "%20");
    }

    public void Load()
    {
        sfxState = true;
        musicState = true;

        if (Sijil.TryLoad(this, "sfx", out object sfxStateObject))
            sfxState = (bool)sfxStateObject;

        if (Sijil.TryLoad(this, "music", out object musicStateObject))
            musicState = (bool)musicStateObject;

        UpdateSFXVisuals();
        UpdateMusicVisuals();
    }

    public void Save()
    {
        Sijil.Save(this, "sfx", sfxState);
        Sijil.Save(this, "music", musicState);
    }


    private void CreditsButtonCallback()
    {
        creditsPanel.SetActive(true);
    }

    public void HideCreditsPanel()
    {
        creditsPanel.SetActive(false);
    }
}
