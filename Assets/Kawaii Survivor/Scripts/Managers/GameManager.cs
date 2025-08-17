using System.Collections;
using UnityEngine;
using System.Linq;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private void Awake()
    {
        SetGameState(GameState.MENU);
        if (instance == null)
            instance = this;
        else 
            Destroy(gameObject); 
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Application.targetFrameRate = 60;
        //SetGameState(GameState.MENU);
    }

    public void StartGame()             => SetGameState(GameState.GAME);
    public void StartWeaponSelection()  => SetGameState(GameState.WEAPONSELECTION);
    public void StartShop()             => SetGameState(GameState.SHOP);

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetGameState(GameState gameState)
    {
        IEnumerable<IGameStateListener> gameStateListeners = FindObjectsByType<MonoBehaviour>(FindObjectsSortMode.None).OfType<IGameStateListener>();
    
        foreach(IGameStateListener gameStateListener in gameStateListeners)
        {
            gameStateListener.GameStateChangedCallback(gameState);  
        }

    }

    public void ManagerGameover()
    {
        SceneManager.LoadScene(0);
    }

    public void WaveCompletedCallback()
    {
        if (Player.instance.HasLeveledUp())
        {
            SetGameState(GameState.WAVETRANSITION); 

        }
        else
        {
            SetGameState(GameState.SHOP);
        }
    }

    public void GameStateChangedCallback(GameState gameState)
    {
        throw new System.NotImplementedException();
    }
}

public interface IGameStateListener
{
    void GameStateChangedCallback(GameState gameState);
}

