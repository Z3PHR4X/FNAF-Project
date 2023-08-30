using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public GameObject ingamePrefab;
    public Sprite thumbnail;
    public string name;
    [TextAreaAttribute] 
    public string description;
    public List<AI.AIValues> aggressionProgression;
    public float actionInterval;
    public AudioClip soundEffect;
}
