using System.Collections.Generic;
using TMPro;
using UnityEngine;

public sealed class SimpleFPSCounter : MonoBehaviour
{
    [SerializeField] private GameObject _rootObj;
    [SerializeField] private TMP_Text _label;
    [SerializeField] private float _labelUpdatePeriod = 1f;

    private readonly List<float> _framerateHistory = new();
    private float _time;

    private void Start()
    {
        _time = _labelUpdatePeriod;
    }

    public void Update()
    {
        var framerate = 1f / Time.unscaledDeltaTime;
        _framerateHistory.Add(framerate);
        _time += Time.unscaledDeltaTime;

        if (_time > _labelUpdatePeriod)
        {
            _time = 0f;
            var avgFramerate = Mathf.RoundToInt(CalculateAvgFramerate());
            _label.text = avgFramerate.ToString();
            _framerateHistory.Clear();
        }
    }

    private float CalculateAvgFramerate()
    {
        var total = 0f;

        foreach (var framerate in _framerateHistory)
        {
            total += framerate;
        }

        var avgFramerate = total / _framerateHistory.Count;

        return avgFramerate;
    }

    public void SetVisibility(bool isVisible)
    {
        _rootObj.SetActive(isVisible);
    }
}
