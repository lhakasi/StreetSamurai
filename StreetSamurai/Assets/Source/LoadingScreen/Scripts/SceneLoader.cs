using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using IJunior.TypedScenes;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] private LevelConfig _levelConfig;
    [SerializeField] private List<SceneAsset> _scenes;
    [SerializeField] private int _selectedSceneIndex;

    public void LoadLoadingScreen()
    {
        SetLoadingLevel();
        LoadingScreen.Load(_levelConfig);
    }

    private void SetLoadingLevel()
    {
        _levelConfig.SetLevelIndex(_selectedSceneIndex);
    }
}
