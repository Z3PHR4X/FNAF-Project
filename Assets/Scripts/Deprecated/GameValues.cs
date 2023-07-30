using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameValues : MonoBehaviour
{
    //TODO Major rewrite

    private int _numberOfLevels = 2;
    private int _numberOfEnemies = 4;
    private string[] _enemyList = {"Tom Nook", "Isebella", "Zipper", "Timmy and Tommy", "Syntax Error", "Runtime Error", "Null Reference", "Logic Error"};
    
    public string[][] enemyNames;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < _numberOfLevels; i++)
        {
            for (int j = 0; j < _numberOfEnemies; j++)
            {
                enemyNames[i][j] = _enemyList[_numberOfEnemies*i + j];
                print(enemyNames[i][j]);
            }
        }
    }
}
