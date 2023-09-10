using UnityEngine;

public class FirstTimeLaunchScreen : MonoBehaviour
{

    private bool hasAcknowledged;

    // Start is called before the first frame update
    void Start()
    {
        hasAcknowledged = (PlayerPrefs.GetString("hasAcknowledgedFirstTimeSetupNotice") == "true");
        gameObject.SetActive(!hasAcknowledged);
    }

    public void Acknowledge()
    {
        PlayerPrefs.SetString("hasAcknowledgedFirstTimeSetupNotice", "true");
        gameObject.SetActive(false);
    }
}
