using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private Slider _slider;
    [SerializeField] private Image _sliderFill;
    [SerializeField] private Gradient _fillGradient;
    [SerializeField] private float _step;
    [SerializeField] private float _delay;
    [SerializeField] private TMP_Text _healthPersentsRenderer;
    [SerializeField] private WarningMessage _warningMessage;
    [SerializeField] private float _healthLevelForWarningMessage;

    private Coroutine _coroutine;

    private void OnEnable()
    {
        _player.MaxHealthEstablished += SetMaxHealth;
        _player.HealthChanged += SetHealth;
    }

    private void OnDisable()
    {
        _player.MaxHealthEstablished -= SetMaxHealth;
        _player.HealthChanged -= SetHealth;
    }

    public void SetMaxHealth(int health)
    {
        _slider.maxValue = health;
        _slider.value = health;
        _healthPersentsRenderer.text = $"{health} %";

        _sliderFill.color = _fillGradient.Evaluate(1f);
        _healthPersentsRenderer.color = _fillGradient.Evaluate(1f);
    }

    public void SetHealth(int health)
    {
        if (_coroutine != null)
            StopCoroutine(_coroutine);

        if (health <= _healthLevelForWarningMessage)
            _warningMessage.gameObject.SetActive(true);
        else
            _warningMessage.gameObject.SetActive(false);

        _coroutine = StartCoroutine(ChangeHealth(health));
    }

    private IEnumerator ChangeHealth(int health)
    {
        WaitForSeconds delay = new WaitForSeconds(_delay);

        while (_slider.value != health)
        {
            _slider.value = Mathf.MoveTowards(_slider.value, health, _step);
            _sliderFill.color = _fillGradient.Evaluate(_slider.normalizedValue);

            _healthPersentsRenderer.text = $"{health} %";
            _healthPersentsRenderer.color = _fillGradient.Evaluate(_slider.normalizedValue);

            yield return delay;
        }
    }
}
