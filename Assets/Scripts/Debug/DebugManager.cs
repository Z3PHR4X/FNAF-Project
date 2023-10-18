using AI;
using System.Collections.Generic;
using UnityEngine;

namespace DebugCustom
{
    public class DebugManager : MonoBehaviour
    {
        public bool debugAvailable = false;
        public bool debugEnabled = false;
        //public EnemyManager enemyManager;
        public Transform content;
        public GameObject debugAiPanelPrefab;
        public List<DebugAIPanel> aiPanels = new List<DebugAIPanel>();
        private Canvas canvas;

        // Start is called before the first frame update
        void Start()
        {
            //enemyManager = FindObjectOfType<EnemyManager>().GetComponent<EnemyManager>();
            canvas = GetComponent<Canvas>();
            canvas.enabled = debugEnabled;
            content.gameObject.SetActive(false);
        }

        private void Update()
        {
            if (debugAvailable)
            {
                HandleInput();
            }
        }

        public void CreateDebugPanel(DefaultEnemyAI enemyAI)
        {
            DebugAIPanel panel;
            GameObject newObj = Instantiate(debugAiPanelPrefab);
            newObj.transform.SetParent(content);
            panel = newObj.GetComponent<DebugAIPanel>();
            panel.SetupPanel(enemyAI);
            aiPanels.Add(panel);
        }

        public void HandleInput()
        {
            if (Input.GetKeyDown(KeyCode.F1) && !GameManagerV2.Instance.isPaused && debugEnabled)
            {
                //Gamespeed to 1f
                Time.timeScale = 1f;
            }
            if (Input.GetKeyDown(KeyCode.F2) && !GameManagerV2.Instance.isPaused && debugEnabled)
            {
                //Gamespeed to 2f
                Time.timeScale = 2f;
            }
            if (Input.GetKeyDown(KeyCode.F3) && !GameManagerV2.Instance.isPaused && debugEnabled)
            {
                if (GameManagerV2.Instance.hour >= 1)
                {
                    GameManagerV2.Instance.RetroNight();
                }
            }
            if (Input.GetKeyDown(KeyCode.F4) && !GameManagerV2.Instance.isPaused && debugEnabled)
            {
                GameManagerV2.Instance.AdvanceNight();
            }
            if (Input.GetKeyDown(KeyCode.F5) && !GameManagerV2.Instance.isPaused && debugEnabled)
            {
                Player.Instance.powerManager.powerReserve -= 100;
            }
            if (Input.GetKeyDown(KeyCode.F6) && !GameManagerV2.Instance.isPaused && debugEnabled)
            {
                Player.Instance.powerManager.powerReserve += 100;
            }
            if (Input.GetKeyDown(KeyCode.F7) && !GameManagerV2.Instance.isPaused && debugEnabled)
            {
                content.gameObject.SetActive(!content.gameObject.activeInHierarchy);
            }
            if (Input.GetKeyDown(KeyCode.F8) && !GameManagerV2.Instance.isPaused && debugEnabled)
            {

            }
            if (Input.GetKeyDown(KeyCode.F9) && !GameManagerV2.Instance.isPaused && debugEnabled)
            {

            }
            if (Input.GetKeyDown(KeyCode.F10) && !GameManagerV2.Instance.isPaused && debugEnabled)
            {

            }
            if (Input.GetKeyDown(KeyCode.F11) && !GameManagerV2.Instance.isPaused)
            {
                debugEnabled = !debugEnabled;
                canvas.enabled = debugEnabled;
            }
        }
    }
}
