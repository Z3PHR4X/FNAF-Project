using System.Collections.Generic;
using UnityEngine;

public class ActivateRandomObject : MonoBehaviour
{
    public List<GameObject> gameObjects = new List<GameObject>();
    [RangeAttribute(0, 20)] public int chance;
    public float interval;
    private float timer;
    private GameObject curObj;

    // Start is called before the first frame update
    void Start()
    {
        curObj = gameObjects[0];
        timer = Time.time + interval;
    }

    // Update is called once per frame
    void Update()
    {
        if (timer + interval < Time.time)
        {
            int randomObj = Random.Range(0, gameObjects.Count);
            if (Tools.DiceRollGenerator.hasSuccessfulRoll(chance))
            {
                curObj.SetActive(false);
                curObj = gameObjects[randomObj];
                curObj.SetActive(true);
            }

            timer = Time.time;
        }
    }



}
