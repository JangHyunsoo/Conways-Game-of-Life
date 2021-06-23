using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class BuildData : ScriptableObject
{
    public string dataName;
    public List<Vector2> dataValue;
    public Sprite dataSprite;
}