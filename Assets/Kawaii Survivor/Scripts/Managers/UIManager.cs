using UnityEngine;

public class UIManager : MonoBehaviour, IGameStateListener
{
    [Header("Panels")]
    [SerializeField] private GameObject menuPanel;
    [SerializeField] private GameObject gamePanel;
    [SerializeField] private GameObject waveTransitionPanel;
    [SerializeField] private GameObject shopPanel;


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
            case GameState.MENU:
                menuPanel.SetActive(true);
                gamePanel.SetActive(false);
                waveTransitionPanel.SetActive(false);
                shopPanel.SetActive(false);
                break;

            case GameState.GAME:
                menuPanel.SetActive(false);
                shopPanel.SetActive(false);
                gamePanel.SetActive(true); 
                break;

            case GameState.WAVETRANSITION:
                gamePanel.SetActive(false);
                waveTransitionPanel.SetActive(true);
                break;

            case GameState.SHOP:
                gamePanel.SetActive(false);
                waveTransitionPanel.SetActive(false);
                shopPanel.SetActive(true);
                break;
        }
    }
}
