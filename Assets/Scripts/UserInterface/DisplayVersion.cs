using UnityEngine;
using UnityEngine.UI;

public class DisplayVersion : MonoBehaviour
{
    [SerializeField] private Text versionText;
    [SerializeField] private Text developerText;
    
    // Start is called before the first frame update
    void Start()
    {
        if(versionText != null){
        versionText.text = "Version: " + Application.version;
        }
        if(developerText != null){
            developerText.text = "Developed by: " + Application.companyName;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
