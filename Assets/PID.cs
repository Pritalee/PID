using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PID
{
    private float _kP, _kI, _kD;

    public float Kp{
        get
        {
            return _kP;
        }
        set
        {
            _kP = value;
        }
    }

    public float Ki{
        get
        {
            return _kI;
        }
        set
        {
            _kI = value;
        }
    }

    public float Kd{
        get
        {
            return _kD;
        }
        set
        {
            _kD = value;
        }
    }

    private float _p, _i, _d;
    private float _previousError;

    
   public PID(float p, float i, float d){
    _kP = p;
    _kI = i;
    _kD = d;

   }

   public float GetOutput(float currentError, float deltaTime){
    _p = currentError;
    _i = _p * deltaTime;
    _d = (_p - _previousError)/ deltaTime;
    _previousError = currentError;

    return _p*_kP + _i *_kI + _d*_kD;

   }
}
