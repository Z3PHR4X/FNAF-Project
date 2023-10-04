using UnityEngine;

public class Night5Completed : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (Singleton.Instance.completedNight >= 5)
        {
            gameObject.SetActive(true);
        }
        else
        {
            gameObject.SetActive(false);
        };
    }
}
