using System.Collections;
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

            for(int x = 0; x < characterList.Count; x++)
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