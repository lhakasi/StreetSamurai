using System.Collections;
using TMPro;
using UnityEngine;

public class TextBlinkRenderer : MonoBehaviour
{
    [SerializeField] private float _delay;

    private TextMeshProUGUI _text;
    private bool _isBlinking;

    private void Awake()
    {
        _text = GetComponent<TextMeshProUGUI>();
        _isBlinking = true;
    }

    private void OnEnable()
    {
        StartCoroutine(Blink());
    }

    private void OnDisable()
    {
        StopCoroutine(Blink());
    }

    private IEnumerator Blink()
    {
        while (_isBlinking)
        {
            float alpha = Mathf.Sin(Time.time / _delay);

            Color textColor = _text.color;
            textColor.a = alpha;
            _text.color = textColor;

            yield return null;
        }
    }
}