using UnityEngine;
using UnityEngine.UI;

public class AIPanel : MonoBehaviour
{
    public Character character;
    public Image thumbnail;
    public Text charName, description, aggressionLevel;
    public Slider aggressionSlider;
    public Toggle characterToggle;
    public GameObject disabledOverlay;
    public AudioSource characterAudio;

    // Start is called before the first frame update
    void Start()
    {

    }

    public void UpdateInterface()
    {
        if (character != null)
        {
            if (Singleton.Instance.selectedNight < 8)
            {
                int night = Mathf.Clamp(Singleton.Instance.selectedNight - 1, 0, 6);
                int aggro = character.aggressionProgression[night].activityValues[0];
                thumbnail.sprite = character.thumbnail;
                charName.text = character.characterName;
                description.text = character.description;
                characterAudio.clip = character.soundEffect;
                aggressionLevel.text = aggro.ToString();
                aggressionSlider.value = aggro;
                aggressionSlider.interactable = false;
                characterToggle.gameObject.SetActive(false);
                disabledOverlay.gameObject.SetActive(!character.aggressionProgression[night].isEnabled);
            }
            else
            {
                int aggro = character.aggressionProgression[7].activityValues[0];
                characterToggle.isOn = character.aggressionProgression[7].isEnabled;
                thumbnail.sprite = character.thumbnail;
                charName.text = character.characterName;
                description.text = character.description;
                characterAudio.clip = character.soundEffect;
                aggressionLevel.text = aggro.ToString();
                aggressionSlider.value = aggro;
                aggressionSlider.interactable = true;
                characterToggle.gameObject.SetActive(true);
                disabledOverlay.gameObject.SetActive(!characterToggle.isOn);
            }
        }
    }

    public void SetCharacterValues()
    {
        if (Singleton.Instance.selectedNight == 8)
        {
            int newAggressionValue = (int)aggressionSlider.value;
            aggressionLevel.text = newAggressionValue.ToString();
            character.aggressionProgression[7].activityValues[0] = newAggressionValue;
            character.aggressionProgression[7].isEnabled = characterToggle.isOn;
            disabledOverlay.gameObject.SetActive(!characterToggle.isOn);
            //if(characterToggle.isOn) characterAudio.Play();
        }
    }
}
