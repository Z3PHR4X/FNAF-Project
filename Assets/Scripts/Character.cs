using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AI;

public class Character : MonoBehaviour
{
    public GameObject ingamePrefab;
    public Sprite thumbnail;
    public string characterName;
    [TextAreaAttribute] 
    public string description;
    public List<AIValues> aggressionProgression;
    public float actionInterval;
    public AudioClip soundEffect;
}
