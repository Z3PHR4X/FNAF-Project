using UnityEngine;
using UnityEngine.UI;

public class GameplaySettings : MonoBehaviour
{
    [SerializeField] private Toggle debugModeToggle, lockCursorToggle;
    [SerializeField] private Slider mouseSensitivitySlider;
    [SerializeField] private Text mouseSensitivityValueText;
    [SerializeField] private Vector2 mouseSensitivityRange = new Vector2(0.1f, 3f);

    float mouseSensitivity;

    // Start is called before the first frame update
    void Start()
    {
        LoadSettings();
    }

    public void SetDebugMode()
    {
        Singleton.Instance.debugMode = debugModeToggle.isOn;
        print($"Debug mode: {Singleton.Instance.debugMode}");
    }

    public void SetLockCursor()
    {
        if(lockCursorToggle.isOn)
        {
            PlayerPrefs.SetInt("lockMouseToWindow", 1);
            Cursor.lockState = CursorLockMode.Confined;
            //print("Locking cursor to screen");
        }
        else
        {
            PlayerPrefs.SetInt("lockMouseToWindow", 0);
            Cursor.lockState = CursorLockMode.None;
            //print("Cursor is no longer locked to screen");
        }
        PlayerPrefs.Save();
    }

    public void SetMouseSensitivity()
    {
        float newSensitivity = mouseSensitivitySlider.value;
        newSensitivity = Mathf.Round(newSensitivity * 100) / 100;
        mouseSensitivity = newSensitivity;
        mouseSensitivityValueText.text = newSensitivity.ToString();
    }

    public void SaveSettings()
    {
        Singleton.Instance.mouseSensitivity = mouseSensitivity;
        PlayerPrefs.SetFloat("mouseSensitivity", Singleton.Instance.mouseSensitivity);
        PlayerPrefs.Save();
    }

    public void LoadSettings()
    {
        if ((PlayerPrefs.GetInt("lockMouseToWindow") == 1))
        {
            lockCursorToggle.isOn = true;
        }
        else
        {
            lockCursorToggle.isOn = false;
        }
        
        debugModeToggle.isOn = Singleton.Instance.debugMode;

        if(GameManagerV2.Instance == null)
        {
            debugModeToggle.interactable = true;
        }
        else
        {
            debugModeToggle.interactable = false;
        }

        mouseSensitivitySlider.minValue = mouseSensitivityRange.x;
        mouseSensitivitySlider.maxValue = mouseSensitivityRange.y;
        Singleton.Instance.mouseSensitivity = PlayerPrefs.GetFloat("mouseSensitivity");
        mouseSensitivitySlider.value = Singleton.Instance.mouseSensitivity;
        mouseSensitivityValueText.text = Singleton.Instance.mouseSensitivity.ToString();
    }

    public void Cancel()
    {
        Singleton.Instance.mouseSensitivity = PlayerPrefs.GetFloat("mouseSensitivity");
    }
}
