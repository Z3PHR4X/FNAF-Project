using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameplaySettings : MonoBehaviour
{
    [SerializeField] private Toggle lockCursorToggle;

    // Start is called before the first frame update
    void Start()
    {
        lockCursorToggle.isOn = (PlayerPrefs.GetString("lockMouseToWindow") == "true");
    }

    public void SetLockCursor()
    {
        if(lockCursorToggle.isOn)
        {
            PlayerPrefs.SetString("lockMouseToWindow", "true");
            Cursor.lockState = CursorLockMode.Confined;
            print("Locking cursor to screen");
        }
        else
        {
            PlayerPrefs.SetString("lockMouseToWindow", "false");
            Cursor.lockState = CursorLockMode.None;
            print("Cursor is no longer locked to screen");
        }
    }
}
