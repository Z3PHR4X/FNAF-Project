using UnityEngine;
using UnityEngine.UI;
using AI;

namespace DebugCustom
{
    public class DebugAIPanel : MonoBehaviour
    {
        public DefaultEnemyAI enemyAI;
        [SerializeField] private Image iconImage;
        [SerializeField] private Text nameText, activityText, lastActionText, stateText, waypointText, flowWeightText, doorText;
        private Sprite charSprite;
        private bool isReady = false;

        // Update is called once per frame
        void Update()
        {
            if (isReady)
            {
                SetCharacterValues();
            }
        }

        public void SetCharacterValues()
        {
            string[] values = enemyAI.GetDebugValues();
            nameText.text = $"{values[0]}";
            activityText.text = $"Activity: {values[1]}";
            lastActionText.text = $"Last Action: {values[2]}s";
            stateText.text = $"State: {values[3]}";
            waypointText.text = $"{values[4]} <{values[5]}>";
            flowWeightText.text = $"FlowWeight: {values[6]}";
            doorText.text = $"Door: {values[7]}";
        }

        public void SetupPanel(DefaultEnemyAI newEnemyAI)
        {
            enemyAI = newEnemyAI;
            charSprite = enemyAI.GetCharacterSprite();
            iconImage.sprite = charSprite;
            isReady = true;
        }
    }
}