using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AIPanel : MonoBehaviour
{
    public Character character;
    public Image thumbnail;
    public Text name, description, aggressionLevel;
    public Slider aggressionSlider;

    // Start is called before the first frame update
    void Start()
    {

    }

    public void UpdateInterface()
    {
        if (character != null)
        {
            int night = Mathf.Clamp(Singleton.Instance.selectedNight - 1, 0, 6);
            int aggro = character.aggressionProgression[night].activityValues[0];
            thumbnail.sprite = character.thumbnail;
            name.text = character.characterName;
            description.text = character.description;
            aggressionLevel.text = aggro.ToString();
            aggressionSlider.value = aggro;
        }
    }
}
