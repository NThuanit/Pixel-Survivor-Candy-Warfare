using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using NaughtyAttributes;
using UnityEngine.SceneManagement;
public class WaveTransitionManager : MonoBehaviour, IGameStateListener
{
    [Header("Elements")]
    [SerializeField] private PlayerStatsManager playerStatsManager;
    [SerializeField] private UpgradeContainer[] upgradeContainers;
 

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GameStateChangedCallback(GameState gameState)
    {
        switch (gameState)
        {
            case GameState.WAVETRANSITION:
                ConfigureUpgradeContainers();
                break;
        }
    }


    [Button]
    private void ConfigureUpgradeContainers()
    {
        for (int i = 0; i < upgradeContainers.Length; i++)
        {
            int randomIndex = UnityEngine.Random.Range(0, Enum.GetValues(typeof(Stat)).Length);

            Stat stat = (Stat)Enum.GetValues(typeof (Stat)).GetValue(randomIndex);

            string randomStatString = Enums.FormatStatName(stat);  

            string buttonString;
            Action action = GetActionToPerform(stat, out buttonString);

            upgradeContainers[i].Configure(null, randomStatString, buttonString);

            upgradeContainers[i].Button.onClick.RemoveAllListeners();
            upgradeContainers[i].Button.onClick.AddListener(() => action?.Invoke());
            upgradeContainers[i].Button.onClick.AddListener(() => BonusSelectedCallback());
        }
    }

    private void BonusSelectedCallback()
    {
        GameManager.instance.WaveCompletedCallback();
    }

    private Action GetActionToPerform(Stat stat, out string buttonString)
    {
        buttonString = "";
        float value;

        value = UnityEngine.Random.Range(1, 10);

        switch (stat)
        {
            case Stat.Attack:
                value = UnityEngine.Random.Range(1, 10);
                buttonString = "+" + value.ToString() + "%";

                break;

            case Stat.AttackSpeed:
                value = UnityEngine.Random.Range(1, 10);
                buttonString = "+" + value.ToString() + "%";

                break;

            case Stat.CriticalChance:
                value = UnityEngine.Random.Range(1, 10);
                buttonString = "+" + value.ToString() + "%";

                break;

            case Stat.CriticalPercent:
                value = UnityEngine.Random.Range(1f, 2f);
                buttonString = "+" + value.ToString("F2") + "x";
                break;

            case Stat.MoveSpeed:
                value = UnityEngine.Random.Range(1, 10);
                buttonString = "+" + value.ToString() + "%";

                break;


            case Stat.MaxHealth:
                value = UnityEngine.Random.Range(1, 10);
                buttonString = "+" + value.ToString() + "%";

                //SceneManager.LoadScene(0);
                break;

            case Stat.Range:
                value = UnityEngine.Random.Range(1f, 5f);
                buttonString = "+" + value.ToString();

                break;

            case Stat.HealthRecoverySpeed:
                value = UnityEngine.Random.Range(1, 10);
                buttonString = "+" + value.ToString() + "%";

                break;

            case Stat.Armor:
                value = UnityEngine.Random.Range(1, 10);
                buttonString = "+" + value.ToString() + "%";

                break;

            case Stat.Luck:
                value = UnityEngine.Random.Range(1, 10);
                buttonString = "+" + value.ToString() + "%";

                break;

            case Stat.Dodge:
                value = UnityEngine.Random.Range(1, 10);
                buttonString = "+" + value.ToString() + "%";

                break;

            case Stat.LifeSteal:
                value = UnityEngine.Random.Range(1, 10);
                buttonString = "+" + value.ToString() + "%";

                break;

            default:
                
                return () => Debug.Log("Invalid stat");
        }

        return () => playerStatsManager.AddPlayerStat(stat, value);
    }
}
