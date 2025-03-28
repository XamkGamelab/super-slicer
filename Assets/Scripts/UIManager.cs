using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] Slider dashSlider;
    [SerializeField] Slider healthSlider;
    [SerializeField] Combo combo;
    [SerializeField] TextMeshProUGUI scoreTextField;
    //[SerializeField] Button playButton;
    //[SerializeField] Button settingsButton;
    //[SerializeField] Button quitButton;
    [SerializeField] GameObject mainMenu;
    [SerializeField] GameObject pauseMenu;
    [SerializeField] GameObject gameOverMenu;
    [SerializeField] GameObject HUD;

    private GameManager gameManager;
    int score = 0;

    public enum Menus
    {
        MainMenu,
        PauseMenu,
        GameOverMenu,
        HUD
    }

    private void Start()
    {
        gameManager = GameManager.Instance;
        dashSlider.maxValue = 20;

        // testing
        dashSlider.value = 20;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && HUD.activeSelf)
        {
            EnableMenu(Menus.PauseMenu);
        }
    }

    public Slider DashSlider
    {
        get => dashSlider;
        //set => dashSlider = value;
    }

    public Slider HealthSlider 
    { 
        get => healthSlider;
        //set => healthSlider = value; 
    }

    public Combo Combo
    {
        get => combo;
        //set => combo = value;
    }

    public void IncreaseScore(int amount)
    {
        score += amount * combo.comboMult;
        scoreTextField.text = $"Score: {score}";
    }

    public void Play()
    {
        EnableMenu(Menus.HUD);
        gameManager.Pause(false);
        // TODO: use previous timescale instead if paused at TS 0
        //Time.timeScale = 1.0f;
    }

    public void QuitGame()
    {
        gameManager.QuitGame();
    }

    public void Resume()
    {
        EnableMenu(Menus.HUD);
        gameManager.Pause(false);
    }

    public void MainMenu()
    {
        EnableMenu(Menus.MainMenu);

        // TODO: use previous timescale instead if paused at TS 0
        Time.timeScale = 1.0f;
    }

    public void Pause()
    {
        EnableMenu(Menus.PauseMenu);
        gameManager.Pause(true);
    }

    public void Settings()
    {
        // TODO: Settings
    }

    public void EnableMenu(Menus menu)
    {
        mainMenu.SetActive(menu == Menus.MainMenu);
        pauseMenu.SetActive(menu == Menus.PauseMenu);
        gameOverMenu.SetActive(menu == Menus.GameOverMenu);
        HUD.SetActive(menu == Menus.HUD);
    }
}
