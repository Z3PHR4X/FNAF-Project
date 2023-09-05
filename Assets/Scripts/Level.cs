using System.Collections.Generic;
using UnityEngine;
using Eflatun.SceneReference;

public class Level : MonoBehaviour
{
    public SceneReference levelScene;
    public string levelName, levelDescription;
    public Sprite levelThumbnail, levelLoadingBackground;
    public AudioClip selectionSound, selectionMusic;
    public List<Character> characters = new List<Character>();
    public int numberOfNights = 7;
}
