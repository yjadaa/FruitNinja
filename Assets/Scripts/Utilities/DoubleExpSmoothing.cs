using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;


class DoubleExpSmoothing
{
    public float _alpha = 1f;
    private bool _isFirst = true;

    private float _forcastValue = 0.0f;
    private float _st1 = 0.0f;
    private float _st2 = 0.0f;

    private float _st = 0.0f;
    private float _trend_t = 0.0f;

    public DoubleExpSmoothing(float alpha)
    {
        this._alpha = alpha;
    }
    public float GetForcast(float inputData)
    {
        if (this._isFirst)
        {
            this._st1 = inputData;
            this._st2 = inputData;
            this._isFirst = false;
        }
        else
        {
            this._st1 = this._alpha * inputData + (1 - this._alpha) * this._st1;
            this._st2 = this._alpha * this._st1 + (1 - this._alpha) * this._st2;
        }

        this._st = 2 * this._st1 - this._st2;
        this._trend_t = (this._alpha / (1 - this._alpha)) * (this._st1 - this._st2);
        this._forcastValue = this._st + this._trend_t;

        return this._forcastValue;
    }
}

public class Smoothing
{
    private DoubleExpSmoothing xSmooth;
    private DoubleExpSmoothing ySmooth;
    private DoubleExpSmoothing zSmooth;

    public Smoothing(float alpha)
    {
        xSmooth = new DoubleExpSmoothing(alpha);
        ySmooth = new DoubleExpSmoothing(alpha);
        zSmooth = new DoubleExpSmoothing(alpha);
    }

    public Vector3 Forcast(Vector3 point)
    {
        return new Vector3(xSmooth.GetForcast(point.x), ySmooth.GetForcast(point.y), zSmooth.GetForcast(point.z));
    }
}

