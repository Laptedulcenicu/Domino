using UnityEngine;

[CreateAssetMenu(fileName = "New Skin", menuName = "Create Skin Data")]
public class SkinData : ScriptableObject
{
    [Header("Shop Info")]
    public int cost;
    public Sprite icon;
    public bool isPreinlocked = false;

    [Header("Skin Properties")]
    public Material prefab;

}
