using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarCtrl : MonoBehaviour
{
    // 碰撞器
    public WheelCollider Wheel1;
    public WheelCollider Wheel2;
    public WheelCollider Wheel3;
    public WheelCollider Wheel4;

    // 变换矩阵
    public Transform Wheel1Transformation;
    public Transform Wheel2Transformation;
    public Transform Wheel3Transformation;
    public Transform Wheel4Transformation;

    // 自身的刚体
    private Rigidbody carRigidbody;

    // 参数
    public float GRAVITY = 9.8f;
    public Vector3 CenterOfGravity;
    private bool isBrake = false;
    private float maxBrakeTorque = 300;
    private float maxTorque = 300;

    // Start is called before the first frame update
    void Start()
    {
        carRigidbody = GetComponent<Rigidbody>();
        carRigidbody.centerOfMass = CenterOfGravity;   
    }
    
    // Update is called once per frame
    void Update() {
        

        Wheel1.steerAngle = 30 * Input.GetAxis("Horizontal");
        Wheel2.steerAngle = 30 * Input.GetAxis("Horizontal");
        Wheel3.motorTorque = maxTorque * Input.GetAxis("Vertical");
        Wheel4.motorTorque = maxTorque * Input.GetAxis("Vertical");

        if(!isBrake) {
            Wheel1.brakeTorque = Wheel2.brakeTorque = Wheel3.brakeTorque = Wheel4.brakeTorque = 0;
        }

        Brake();

        Wheel1Transformation.Rotate(Wheel1.rpm * 360 / 60 * Time.deltaTime, 0, 0);
        Wheel2Transformation.Rotate(Wheel2.rpm * 360 / 60 * Time.deltaTime, 0, 0);
        Wheel3Transformation.Rotate(Wheel3.rpm * 360 / 60 * Time.deltaTime, 0, 0);
        Wheel4Transformation.Rotate(Wheel4.rpm * 360 / 60 * Time.deltaTime, 0, 0);
        
        // 车轮方向
        Vector3 angle1 = Wheel1Transformation.localEulerAngles;
        Vector3 angle2 = Wheel2Transformation.localEulerAngles;

        angle1.y = Wheel1.steerAngle;
        Wheel1Transformation.localEulerAngles = angle1;
        angle2.y = Wheel2.steerAngle;
        Wheel2Transformation.localEulerAngles = angle2;
    }

    // 检测刹车并控制刹车动力
    void Brake()
    {
        if(Input.GetKey(KeyCode.Space))
            isBrake = true;
        else
            isBrake = false;

        if(isBrake){
            Wheel3.brakeTorque = maxBrakeTorque * 50;
            Wheel4.brakeTorque = maxBrakeTorque * 50;
            Wheel3.motorTorque = 0;
            Wheel4.motorTorque = 0;
        }
    }
}
