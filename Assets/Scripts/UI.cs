using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    public GameObject mainMenuPrefab;
    public GameObject gameplayUIPrefab;
    public GameObject pauseMenuPrefab;
    public GameObject gameOverPrefab;

    private GameObject mainMenu;
    private GameObject gameplayUI;
    private GameObject pauseMenu;
    private GameObject gameOverUI;

    private GameObject pauseButtonObject;
    private Button pauseButton;

    private GameObject continueButtonObject;
    private Button continueButton;

    private LiftButtonUI liftButton;

    private GameObject startGameButtonObject;
    private Button startGameButton;

    private GameObject quitButtonObject;
    private Button quitButton;

    private GameObject mainMenuButtonObject;
    private Button mainMenuButton;

    private GameObject gameStateObject;
    private GameState gameState;

    private GameObject scoreGameObject;
    private Score score;

    private GameObject gameOverButtonObject;
    private Button gameOverButton;

    private void Start()
    {
        InstantiateUI();

        FindObjects();
        GetButtons();
        GetScripts();
        SetupButtonsEvent();

        ClosePauseMenu();
        CloseGameOverUI();

        InitializeLiftButton();

        InitializeScoreTextUI();
    }

    private void InitializeScoreTextUI()
    {
        score.GetScoreUI();
    }

    private void InitializeLiftButton()
    {
        liftButton.InitializeLiftButton();
    }

    private void InstantiateUI()
    {
        mainMenu = Instantiate(mainMenuPrefab, Vector3.zero, Quaternion.identity);
        gameplayUI = Instantiate(gameplayUIPrefab, Vector3.zero, Quaternion.identity);
        pauseMenu = Instantiate(pauseMenuPrefab, Vector3.zero, Quaternion.identity);
        gameOverUI = Instantiate(gameOverPrefab, Vector3.zero, Quaternion.identity);

        mainMenu.transform.SetParent(transform);
        gameplayUI.transform.SetParent(transform);
        pauseMenu.transform.SetParent(transform);
        gameOverUI.transform.SetParent(transform);
    }

    private void FindObjects()
    {
        pauseButtonObject = GameObject.FindGameObjectWithTag("PauseButton");
        continueButtonObject = GameObject.FindGameObjectWithTag("ContinueButton");
        startGameButtonObject = GameObject.FindGameObjectWithTag("StartGameButton");
        quitButtonObject = GameObject.FindGameObjectWithTag("QuitButton");
        mainMenuButtonObject = GameObject.FindGameObjectWithTag("MainMenuButton");
        gameOverButtonObject = GameObject.FindGameObjectWithTag("GameOverButton");

        gameStateObject = GameObject.FindGameObjectWithTag("GameState");

        scoreGameObject = GameObject.FindGameObjectWithTag("Score");
    }

    private void GetButtons()
    {
        pauseButton = pauseButtonObject.GetComponent<Button>();
        continueButton = continueButtonObject.GetComponent<Button>();
        startGameButton = startGameButtonObject.GetComponent<Button>();
        quitButton = quitButtonObject.GetComponent<Button>();
        mainMenuButton = mainMenuButtonObject.GetComponent<Button>();
        gameOverButton = gameOverButtonObject.GetComponent<Button>();
    }

    private void GetScripts()
    {
        gameState = gameStateObject.GetComponent<GameState>();
        liftButton = GetComponent<LiftButtonUI>();

        score = scoreGameObject.GetComponent<Score>();
    }

    private void SetupButtonsEvent()
    {
        pauseButton.onClick.AddListener(OpenPauseMenu);
        continueButton.onClick.AddListener(ClosePauseMenu);
        startGameButton.onClick.AddListener(StartGame);
        quitButton.onClick.AddListener(QuitGame);
        mainMenuButton.onClick.AddListener(OpenMainMenu);
        gameOverButton.onClick.AddListener(OpenMainMenu);
    }

    private void OpenPauseMenu()
    {
        gameState.PauseGame();
        pauseMenu.SetActive(true);
    }

    private void ClosePauseMenu()
    {
        pauseMenu.SetActive(false);
        gameState.ContinueGame();
    }

    private void OpenMainMenu()
    {
        mainMenu.SetActive(true);
        gameState.StopGame();
    }

    private void CloseMainMenu()
    {
        mainMenu.SetActive(false);
    }

    public void OpenGameOverUI()
    {
        gameOverUI.SetActive(true);
        gameplayUI.SetActive(false);
    }

    public void CloseGameOverUI()
    {
        gameOverUI.SetActive(false);
        gameplayUI.SetActive(true);
    }

    private void StartGame()
    {
        ClosePauseMenu();
        CloseMainMenu();

        gameState.StartGame();
    }

    private void QuitGame()
    {
        gameState.StopGame();
        Application.Quit();
    }
}
