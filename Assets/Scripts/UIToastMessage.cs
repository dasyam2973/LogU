using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements.Experimental;

public class UIToastMessage : UIBehaviour
{
    [SerializeField] private Image _image;
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private float _duration;
    private float _currentDuration;

    private void Update()
    {
        _currentDuration = Mathf.Max(0f, _currentDuration - Time.deltaTime);
        float alpha = Easing.OutBack(_currentDuration / _duration);
        if (_image)
            _image.color = _image.color.WithAlpha(alpha);
        _text.color = _text.color.WithAlpha(alpha);
    }

    public void Show()
    {
        _currentDuration = _duration;
    }

    public void Hide()
    {
        _currentDuration = 0f;
    }
}