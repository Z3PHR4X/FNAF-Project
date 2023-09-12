using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ProBuilder;
using UnityEngine.UI;

public class GameplaySettings : MonoBehaviour
{
    [SerializeField] private Toggle lockCursorToggle;
    [SerializeField] private Slider mouseSensitivitySlider;
    [SerializeField] private Text mouseSensitivityValueText;
    [SerializeField] private Vector2 mouseSensitivityRange = new Vector2(0.01f, 3f);

    float mouseSensitivity;

    // Start is called before the first frame update
    void Start()
    {
        if((PlayerPrefs.GetInt("lockMouseToWindow") == 1))
        { 
            lockCursorToggle.isOn = true;
        }
        else
        {
            lockCursorToggle.isOn = false;
        }
        
        mouseSensitivitySlider.minValue = mouseSensitivityRange.x;
        mouseSensitivitySlider.maxValue = mouseSensitivityRange.y;
        mouseSensitivitySlider.value = Singleton.Instance.mouseSensitivity;
        mouseSensitivityValueText.text = Singleton.Instance.mouseSensitivity.ToString();
    }

    public void SetLockCursor()
    {
        if(lockCursorToggle.isOn)
        {
            PlayerPrefs.SetInt("lockMouseToWindow", 1);
            Cursor.lockState = CursorLockMode.Confined;
            print("Locking cursor to screen");
        }
        else
        {
            PlayerPrefs.SetInt("lockMouseToWindow", 0);
            Cursor.lockState = CursorLockMode.None;
            print("Cursor is no longer locked to screen");
        }
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
    }

    public void Cancel()
    {
        Singleton.Instance.mouseSensitivity = PlayerPrefs.GetFloat("mouseSensitivity");
    }
}
