using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

    public class MenuCharacters : MonoBehaviour
    {
    //TODO Major rewrite
    public bool isDeprecated = true;
    [SerializeField] private Light characterLight;
        [SerializeField] private float defaultLightBrightness = 0.5f;
        [SerializeField] private GameObject[] characters;
        [SerializeField] private bool GlitchEnabled;
        [SerializeField] private int switchChance;
        [SerializeField] private int glitchChance;
        [SerializeField] private int flickerChance;
        [SerializeField] private int darkChance;
        [SerializeField] private int minSwitchTime;
        [SerializeField] private int maxSwitchTime;
        [SerializeField] private int minSwitchLightTime;
        [SerializeField] private int maxSwitchLightTime;
        private float timer;
        private float cooldown;
        private float timerLight;
        private float cooldownLight;
        private int current;

        private int glitchAmount;
        private int glitchLimit;
        private int flickerAmount;
        private int flickerLimit;
        private int darkAmount;
        private int darkLimit;

        private bool isGlitching;
        private bool isSwitching;
        private bool isFlickering;
        private bool isDark;


        // Start is called before the first frame update
        void Start()
        {
            characterLight.intensity = defaultLightBrightness;
            for (int i = 0; i <= characters.Length - 1; i++)
            {
                characters[i].SetActive(false);
            }

            current = Random.Range(0, characters.Length - 1);
            characters[current].SetActive(true);
            ResetCharTimer();
            ResetLightTimer();
        }

        // Update is called once per frame
        void Update()
        {
            choose();
            if (isGlitching)
            {
                Glitch();
            }
            else if (isSwitching)
            {
                Switch();
            }

            if (isFlickering)
            {
                Flicker();
            }
            else if (isDark)
            {
                Dark();
            }
        }

        private void choose()
        {
            if (!isGlitching && !isSwitching)
            {
                if (timer + cooldown < Time.time)
                {
                    int roll = Random.Range(0, 21);
                    if (roll > glitchChance)
                    {
                        if (GlitchEnabled)
                        {
                            isGlitching = true;
                            glitchLimit = (Random.Range(1, 20) * 2);
                        }
                        else
                        {
                            //print("hello there");
                            ResetCharTimer();
                        }
                    }
                    else if (roll < switchChance)
                    {
                        isSwitching = true;
                    }
                }
            }

            if (!isDark && !isFlickering)
            {
                if (timerLight + cooldownLight < Time.time)
                {
                    int roll = Random.Range(0, 21);
                    if (roll == darkChance)
                    {
                        isDark = true;
                        darkLimit = (Random.Range(1, 400));
                    }
                    else if (roll == flickerChance)
                    {
                        isFlickering = true;
                        flickerLimit = (Random.Range(1, 200));
                    }
                }
            }
        }

        private void Glitch()
        {
            if (glitchAmount < glitchLimit)
            {
                characterLight.intensity = 0.5f;
                int glitch = characters.Length - 1;
                if (characters[glitch].active)
                {
                    characters[glitch].SetActive(false);
                    characters[current].SetActive(true);
                }
                else
                {
                    characters[current].SetActive(false);
                    characters[glitch].SetActive(true);
                }
                glitchAmount++;
            }
            else
            {
                glitchAmount = 0;
                characterLight.intensity = defaultLightBrightness;
                isGlitching = false;
                ResetCharTimer();
            }
        }

        private void Switch()
        {
            int next = Random.Range(0, characters.Length - 1);
            if (current != next)
            {
                characters[current].SetActive(false);
                current = next;
                characters[current].SetActive(true);
            }
            isSwitching = false;
            ResetCharTimer();
        }

        private void Flicker()
        {
            if (flickerAmount < flickerLimit)
            {
                characterLight.intensity = Random.Range(0.0f, 1.0f);
                flickerAmount++;
            }
            else
            {
                characterLight.intensity = 1;
                flickerAmount = 0;
                isFlickering = false;
                ResetLightTimer();
            }
        }

        private void Dark()
        {
            if (darkAmount < darkLimit)
            {
                characterLight.intensity = 0;
                darkAmount++;
            }
            else
            {
                characterLight.intensity = defaultLightBrightness;
                darkAmount = 0;
                isDark = false;
                ResetLightTimer();
            }

        }

        private void ResetCharTimer()
        {
            cooldown = Random.Range(minSwitchTime, maxSwitchTime);
            //print(cooldown);
            timer = Time.time;
        }
        private void ResetLightTimer()
        {
            cooldownLight = Random.Range(minSwitchLightTime, maxSwitchLightTime);
            //print(cooldownLight);
            timerLight = Time.time;
        }
    }
