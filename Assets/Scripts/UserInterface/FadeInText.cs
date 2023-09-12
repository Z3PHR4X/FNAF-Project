using UnityEngine;
using UnityEngine.UI;

public class FadeInText : MonoBehaviour
{
    private Text text;
    private float  alpha = 0f;
    private Color color;

    [SerializeField] private bool checkNight;

    private void Start()
    {
        text = GetComponent<Text>();
        color = text.color;

        if (checkNight)
        {
            if(Singleton.Instance.selectedNight > 5)
            {
                gameObject.SetActive(false);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        alpha = Mathf.Lerp(alpha, 1, 0.3f*Time.deltaTime);
        color = new Color(color.r, color.g, color.b, alpha);
        text.color = color;

        if(alpha < 0.95f)
        {
            gameObject.SetActive(false);
        }
    }
}
