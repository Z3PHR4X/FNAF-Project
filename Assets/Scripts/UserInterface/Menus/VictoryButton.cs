using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictoryButton : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ReturnToMenu()
    {
        if (Singleton.Instance.selectedNight == 7)
        {
            Singleton.Instance.ChangeScene("GameWin");
        }
        else
        {
            Singleton.Instance.ChangeScene("MainMenu");
        }
    }
}
