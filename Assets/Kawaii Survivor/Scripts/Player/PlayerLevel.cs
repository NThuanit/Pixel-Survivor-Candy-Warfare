using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerLevel : MonoBehaviour
{
    [Header("Settings")]
    private int requireXp;
    private int currentXp;
    private int level;


    [Header("Visuals")]
    [SerializeField] private Slider xpBar;
    [SerializeField] private TextMeshProUGUI levelText;

    private void Awake()
    {
        Candy.onCollected += CandyCollectedCallback;
    }

    private void OnDestroy()
    {
        Candy.onCollected -= CandyCollectedCallback;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        UpdateRequiredXp();
        UpdateVisuals();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void UpdateRequiredXp()
    {
        requireXp = (level + 1) * 5;
    }

    private void UpdateVisuals()
    {
        xpBar.value = (float)currentXp / requireXp;
        levelText.text = "level " + (level + 1).ToString();
    }

    private void CandyCollectedCallback(Candy candy)
    {
        currentXp = currentXp + 1;
        if (currentXp >= requireXp) LevelUp();

        UpdateVisuals();
    }

    private void LevelUp()
    {
        level = level + 1;
        currentXp = 0;
        UpdateRequiredXp();
    }
}
