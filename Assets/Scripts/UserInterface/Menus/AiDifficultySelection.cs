using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UserInterface.Menus
{
    public class AiDifficultySelection : MonoBehaviour
    {
        public GameObject aiPanelPrefab;
        public List<AIPanel> aIPanels = new List<AIPanel>();
        public List<Character> characters = new List<Character>();
        private Transform myTransform;

        private void Awake()
        {
            if(Singleton.Instance.completedNight >= 6)
            {
                this.gameObject.SetActive(true);
            }
        }
        // Start is called before the first frame update
        void Start()
        {
            myTransform = this.gameObject.transform;
            UpdatePanels();
        }

        public void UpdatePanels()
        {
            characters = Singleton.Instance.selectedMap.characters;

            for(int x = aIPanels.Count-1; x > 0; x--)
            {
                AIPanel panel = aIPanels[x];
                aIPanels.Remove(panel);
                GameObject obj = panel.gameObject;
                print("destroying " + obj);
                Object.Destroy(obj);
            }


            foreach(Character character in characters)
            {
                GameObject instance = Instantiate(aiPanelPrefab, myTransform);
                instance.GetComponent<AIPanel>().character = character;
                aIPanels.Add(instance.GetComponent<AIPanel>());
                aIPanels[aIPanels.Count-1].UpdateInterface();
            }
        }
    }
}
