using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WarningMessage : MonoBehaviour
{
    [SerializeField] private float _delay;

    private Image _warningImage;
    private TextMeshProUGUI _warningText;
    private bool _isBlinking;

    private void Awake()
    {
        _warningImage = GetComponent<Image>();
        _warningText = GetComponentInChildren<TextMeshProUGUI>();
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

            Color imageColor = _warningImage.color;
            imageColor.a = alpha;
            _warningImage.color = imageColor;

            Color textColor = _warningText.color;
            textColor.a = alpha;
            _warningText.color = textColor;

            yield return null;
        }
    }
}
