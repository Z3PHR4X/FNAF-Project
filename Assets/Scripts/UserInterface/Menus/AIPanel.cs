using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Zephrax.FNAFGame.UserInterface.Menus { 
public class AIPanel : MonoBehaviour
{
    public Character character;
    public Image thumbnail;
    public TMP_Text charName, description, aggressionLevel;
    public Slider aggressionSlider;
    public Toggle characterToggle;
    public GameObject disabledOverlay;
    public AudioSource characterAudio, interfaceAudio;
    private bool canPlayAudio, prevCharacterToggleValue;

    // Start is called before the first frame update
    void Start()
    {
        //canPlayAudio = false;
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
                characterAudio.pitch = character.soundEffectPitch;
                //characterAudio.volume = Singleton.Instance.voiceVolume;
                aggressionLevel.text = aggro.ToString();
                aggressionSlider.value = aggro;
                aggressionSlider.interactable = false;
                characterToggle.gameObject.SetActive(false);
                if (Singleton.Instance.selectedMap.supportsCustomNight)
                {
                    disabledOverlay.gameObject.SetActive(!character.aggressionProgression[night].isEnabled);
                }
                else
                {
                    disabledOverlay.gameObject.SetActive(false);
                }
            }
            else
            {
                int aggro = character.aggressionProgression[7].activityValues[0];
                characterToggle.isOn = character.aggressionProgression[7].isEnabled;
                thumbnail.sprite = character.thumbnail;
                charName.text = character.characterName;
                description.text = character.description;
                characterAudio.clip = character.soundEffect;
                characterAudio.pitch = character.soundEffectPitch;
                //characterAudio.volume = Singleton.Instance.voiceVolume;
                aggressionLevel.text = aggro.ToString();
                aggressionSlider.value = aggro;
                aggressionSlider.interactable = true;
                characterToggle.gameObject.SetActive(true);
                disabledOverlay.gameObject.SetActive(!characterToggle.isOn);
            }
        }
        canPlayAudio = true;
        prevCharacterToggleValue = characterToggle.isOn;
        //print("amogus");
        //print("audio: "+canPlayAudio);
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
            //print(canPlayAudio);
            if (canPlayAudio)
            {
                //interfaceAudio.Play();
                if (characterToggle.isOn != prevCharacterToggleValue)
                {
                    if (characterToggle.isOn)
                    {
                        characterAudio.Play();
                    }
                    prevCharacterToggleValue = characterToggle.isOn;
                }
            }
        }
    }
}
}