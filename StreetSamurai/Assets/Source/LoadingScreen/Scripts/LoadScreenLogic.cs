using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using IJunior.TypedScenes;

public class LoadScreenLogic : MonoBehaviour, ISceneLoadHandler<LevelConfig>
{
    [SerializeField] private List<SceneAsset> _scenes;
    [SerializeField] private Slider _loadingBar;
    [SerializeField] private float _loadingSpeed;
    [SerializeField] private TextMeshProUGUI _finishMessage;
    
    private int _selectedSceneIndex;

    public void OnSceneLoaded(LevelConfig argument)
    {
        _selectedSceneIndex = argument.LevelIndex;
    }

    private void Start()
    {
        Debug.Log(_selectedSceneIndex);
        LoadSelectedScene();
    }

    private void LoadSelectedScene()
    {
        if (_selectedSceneIndex >= 0 && _selectedSceneIndex < _scenes.Count)
        {
            StartCoroutine(LoadSceneAsync());
        }
        else
        {
            Debug.LogError("Неверный индекс выбранной сцены.");
        }
    }

    private IEnumerator LoadSceneAsync()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(_scenes[_selectedSceneIndex].name);

        asyncLoad.allowSceneActivation = false;

        while (!asyncLoad.isDone)
        {
            float targetValue = asyncLoad.progress / 0.9f;
            _loadingBar.value = Mathf.MoveTowards(_loadingBar.value, targetValue, _loadingSpeed * Time.deltaTime);

            if (!asyncLoad.allowSceneActivation && asyncLoad.progress >= 0.9f)
            {
                _finishMessage.gameObject.SetActive(true);

                if (Input.anyKeyDown && _loadingBar.value >= _loadingBar.maxValue)
                    asyncLoad.allowSceneActivation = true;
            }

            yield return null;
        }
    }    
}