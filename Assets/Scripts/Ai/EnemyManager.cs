using System.Collections.Generic;
using UnityEngine;


namespace AI
{
    public class EnemyManager : MonoBehaviour
    {
        private List<Character> characterList = new List<Character>();
        [SerializeField] private List<DynamicWaypoints> spawnWaypoints = new List<DynamicWaypoints>();


        // Start is called before the first frame update
        void Start()
        {
            //Spawn AI at the start of the game from characterlist in SelectedLevel
            characterList = Singleton.Instance.selectedMap.characters;

            //Make sure the amount of characters is within the levels limitations
            if(characterList.Count > spawnWaypoints.Count)
            {
                while(characterList.Count > spawnWaypoints.Count) { 
                characterList.RemoveAt(characterList.Count-1);
                }
            }

            for(int x = 0; x < characterList.Count; x++)
            {
                if (characterList[x].aggressionProgression[Singleton.Instance.selectedNight-1].isEnabled)
                {
                    GameObject character = characterList[x].ingamePrefab;
                    Transform spawnPoint = spawnWaypoints[x].GetComponent<Transform>();
                    GameObject spawnedChar = Instantiate(character, spawnPoint);
                    spawnedChar.transform.SetParent(this.gameObject.transform);
                    spawnedChar.GetComponent<DefaultEnemyAI>().homeWaypoint = spawnWaypoints[x].GetComponent<DynamicWaypoints>();
                }
            }
        }
    }
}