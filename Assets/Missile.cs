using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour
{

    [SerializeField]
    [Range(0, 5000)]
    private float _thrust = 1000f;

    [SerializeField]
    private float _maxAngularVelocity = 20;

    [SerializeField]
    private GameObject _target = null;

    private PID _xAxisPIDController;
    private Rigidbody _rb;

    [SerializeField]
    [Range(-10, 10)]
    private float _xAxisP, _xAxisI, _xAxisD;

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _rb.maxAngularVelocity = _maxAngularVelocity;
        _xAxisPIDController = new PID(_xAxisP, _xAxisI, _xAxisD);
    }

    // Update is called once per frame
    void Update()
    {
        _xAxisPIDController.Kp = _xAxisP;
        _xAxisPIDController.Ki = _xAxisI;
        _xAxisPIDController.Kd = _xAxisD;
        // Vector3 targetDirection = transform.position - _target.transform.position;
        // Vector3 rotationDirection = Vector3.RotateTowards(transform.forward, targetDirection, 360, 0.00f);
        // Quaternion targetRotation = Quaternion.LookRotation(rotationDirection);

        // transform.rotation = targetRotation;

    }

    private void FixedUpdate(){
        Vector3 targetDirection = transform.position - _target.transform.position;
        Vector3 rotationDirection = Vector3.RotateTowards(transform.forward, targetDirection, 360, 0.00f);
        Quaternion targetRotation = Quaternion.LookRotation(rotationDirection);

        float xAngleError = Mathf.DeltaAngle(transform.rotation.eulerAngles.x, targetRotation.eulerAngles.x);
        float xTorqueCorrection = _xAxisPIDController.GetOutput(xAngleError, Time.fixedDeltaTime);

        _rb.AddRelativeTorque(xTorqueCorrection * Vector3.right);
        _rb.AddRelativeForce((-Vector3.forward) * _thrust * Time.fixedDeltaTime);

        transform.rotation = targetRotation;

    }
}
