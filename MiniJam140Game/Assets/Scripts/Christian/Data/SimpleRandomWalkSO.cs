using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SimpleRandomWalkParameters_", menuName = "ScriptableObjects/SimpleRandomWalkSO", order = 1)]
public class SimpleRandomWalkSO : ScriptableObject
{
    public int iterations = 10; 
    public int walkLenght = 10;
    public bool startRandomlyEachIteration = true;
}
