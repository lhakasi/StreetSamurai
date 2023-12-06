using UnityEngine;

[CreateAssetMenu(fileName = "LevelConfig", menuName = "Create Level Config")]
public class LevelConfig : ScriptableObject
{  
    public int LevelIndex {get; set;}

    public void SetLevelIndex(int index)
    {
        LevelIndex = index;
    }
}