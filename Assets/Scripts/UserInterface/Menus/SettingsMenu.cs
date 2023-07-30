using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SettingsMenu : MonoBehaviour
{
    [SerializeField] private Slider sensitivitySlider;
    [SerializeField] private Text sensitivityText;

    private float mouseSensitivity;

    // Start is called before the first frame update
    void Start()
    {
        mouseSensitivity = PlayerPrefs.GetFloat("mouseSensitivity" );
        UpdateMenu();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateMenu();
    }
    
    void UpdateMenu()
    {
            sensitivityText.text = Convert.ToString(Math.Round(mouseSensitivity,2));
            sensitivitySlider.value = mouseSensitivity;
    }

    public void resetSettings()
    {
        mouseSensitivity = 1;
        PlayerPrefs.SetFloat("mouseSensitivity", mouseSensitivity);
        UpdateMenu();
    }
    
    public void HandleInput()
    {
        mouseSensitivity = sensitivitySlider.value;
        print("Mouse Sensitivity set to: " + mouseSensitivity);
        UpdateMenu();
        PlayerPrefs.SetFloat("mouseSensitivity", mouseSensitivity);
    }
}
