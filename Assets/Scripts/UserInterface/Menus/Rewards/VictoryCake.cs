using UnityEngine;

public class VictoryCake : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (Singleton.Instance.completedNight >= 7)
        {
            gameObject.SetActive(true);
        }
        else
        {
            gameObject.SetActive(false);
        };
    }
}
