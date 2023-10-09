using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DisplayDeathMessage : MonoBehaviour
{
    public TMP_Text deathText;
    public string deathMessage;

    private void Awake()
    {
        deathText = GetComponent<TMP_Text>();
    }

    // Start is called before the first frame update
    void Start()
    {
        deathMessage = Singleton.Instance.deathReason;
        deathText.text = deathMessage;
    }

}
