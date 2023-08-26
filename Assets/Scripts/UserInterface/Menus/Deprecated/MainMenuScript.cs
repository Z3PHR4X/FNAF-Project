using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    //TODO Major rewrite

    [SerializeField] private Dropdown levelSelection;
    [SerializeField] private Dropdown nightSelection;
    [SerializeField] private Slider[] aiSliders;
    [SerializeField] private Image levelImage;
    [SerializeField] private Text levelText;
    [SerializeField] private Image[] enemyImages;
    [SerializeField] private Text[] enemyTexts;
    [SerializeField] private Text[] enemyLevelTexts;
    [SerializeField] private Sprite[] enemyThumbnails;
    [SerializeField] private Sprite[] levelThumbnails;
    [TextArea] public string[] levelDescriptions;

    private int _sceneToLoad;
    private int _chosenNight;
    private int[] _enemyLevels = {0, 0, 0, 0};
    private string[] enemyNames = {"Tom Nook", "Isabelle", "Zipper", "Timmy & Tommy"};
    private int[] freddyLevels = {0, 0, 1, 2, 3, 4, 20};
    private int[] bonnyLevels = {0, 3, 0, 2, 5, 10, 20};
    private int[] chicaLevels = {0, 1, 5, 4, 7, 12, 20};
    private int[] foxyLevels = {0, 1, 2, 6, 5, 16, 20};

    // Start is called before the first frame update
    void Start()
    {
        //_sceneToLoad = Singleton.Instance.selectedMap - 2;
        _chosenNight = Singleton.Instance.selectedNight;
        Time.timeScale = 1.0f;
        levelSelection.value = _sceneToLoad;
        nightSelection.value = _chosenNight;
        CheckSettings();
        UpdateMenu();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateMenu();
    }

    public void EnterLoadingScreen()
    {
        SceneManager.LoadScene(1);
    }

    void UpdateMenu()
    {
        //Update Enemy names
        for (int i = 0; i < enemyTexts.Length; i++)
        {
            enemyTexts[i].text = enemyNames[i];
        }

        //Update Enemy thumbnails
        for (int i = 0; i < enemyImages.Length; i++)
        {
            enemyImages[i].sprite = enemyThumbnails[i];
        }

        //Update Enemy ai-levels
        UpdateAILevel();
        for (int i = 0; i < enemyLevelTexts.Length; i++)
        {
            enemyLevelTexts[i].text = Convert.ToString(_enemyLevels[i]);
            aiSliders[i].value = _enemyLevels[i];
        }

        //Update Level thumbnail
        levelImage.sprite = levelThumbnails[_sceneToLoad];

        //Update Level description
        levelText.text = levelDescriptions[_sceneToLoad];
    }

    void UpdateAILevel()
    {
        _enemyLevels[0] = freddyLevels[_chosenNight];
        _enemyLevels[1] = bonnyLevels[_chosenNight];
        _enemyLevels[2] = chicaLevels[_chosenNight];
        _enemyLevels[3] = foxyLevels[_chosenNight];
    }

    void CheckSettings()
    {
        if (PlayerPrefs.GetFloat("mouseSensitivity") == null || PlayerPrefs.GetFloat("mouseSensitivity") >= 0)
        {
            PlayerPrefs.SetFloat("mouseSensitivity", 1f);
        }
    }
    
    public void HandleInputLevel()
    {
        _sceneToLoad = levelSelection.value;
        print("Level chosen: " + _sceneToLoad);
        UpdateMenu();
        PlayerPrefs.SetInt("selectedLevel", (_sceneToLoad+2));
    }

    public void HandleInputNight()
    {
        _chosenNight = nightSelection.value;
        print("Night chosen: " + _chosenNight);
        PlayerPrefs.SetInt("selectedNight", _chosenNight);
        UpdateMenu();
    }

    public void ExitApplication()
    {
        Application.Quit();
    }

}
