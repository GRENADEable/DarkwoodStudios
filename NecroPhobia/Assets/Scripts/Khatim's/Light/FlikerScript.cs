using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlikerScript : MonoBehaviour
{
    #region Public Variables
    public float minIntensity = 0f;
    public float maxIntensity = 1f;
    [Range(1, 50)] public int smoothing = 5;
    #endregion

    #region Private Variables
    private Light _flickerLight;
    private Queue<float> _smoothQueue;
    private float _lastSum = 0;
    #endregion

    void Start()
    {
        _smoothQueue = new Queue<float>(smoothing);

        // External or internal light?
        if (_flickerLight == null)
        {
            _flickerLight = GetComponent<Light>();
        }
    }

    void Reset()
    {
        _smoothQueue.Clear();
        _lastSum = 0;
    }

    void Update()
    {
        if (_flickerLight == null)
            return;

        // pop off an item if too big
        while (_smoothQueue.Count >= smoothing)
        {
            _lastSum -= _smoothQueue.Dequeue();
        }

        // Generate random new item, calculate new average
        float newVal = Random.Range(minIntensity, maxIntensity);
        _smoothQueue.Enqueue(newVal);
        _lastSum += newVal;

        // Calculate new smoothed average
        _flickerLight.intensity = _lastSum / (float)_smoothQueue.Count;
    }
}