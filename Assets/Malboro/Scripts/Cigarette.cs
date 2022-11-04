using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Utility;
using Gyroscope = UnityEngine.InputSystem.Gyroscope;

namespace Malboro
{
    public class Cigarette : MonoBehaviour
    {
        [SerializeField] private float speed = 10f;

        public float speedAc = 10;

        [SerializeField] Transform player;

        private Rigidbody rb;

        public static Action<bool> IsKinematic;

        void isKinematic(bool value)
        {
            rb.isKinematic = value;
        }

        private void OnEnable()
        {
            IsKinematic += isKinematic;
        }
        private void OnDisable()
        {
            IsKinematic -= isKinematic;
        }

        private void Start()
        {
            rb = GetComponent<Rigidbody>();

            //Input.gyro.enabled = true;
            //Input.gyro.updateInterval = 0.0167f;

#if !UNITY_EDITOR
            InputSystem.EnableDevice(Gyroscope.current);
            InputSystem.EnableDevice(Accelerometer.current);
#else
            Input.gyro.enabled = true;
            Input.gyro.updateInterval = 0.0167f;
#endif
            //Debug.Log("Gyroscope.current.enabled : " + Gyroscope.current.enabled);

            isKinematic(true);
        }

        private void Update()
        {
            if (EventManager.Instance.isGameOver || EventManager.Instance.isUIOpen || rb.isKinematic)
            {
                //Debug.Log("isGameOver : " + EventManager.Instance.isGameOver);
                return;
            }

            //Vector3 acceleration = Accelerometer.current.acceleration.ReadValue();
            //Debug.Log(acceleration);

            //Quaternion deviceRotation = DeviceRotation.Get();
            //transform.rotation = Quaternion.Euler(deviceRotation.eulerAngles.x, 0, 0);

            //Vector3 movement = new Vector3(Input.acceleration.x, 0.0f, Input.acceleration.y);
            //Debug.Log("Input.acceleration : " + tilt);
            //tilt = (Quaternion.Euler(deviceRotation.eulerAngles.x, 0, 0) * tilt) * speed;


            //rb.AddForce(tilt, ForceMode.Force);
            //rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxSpeed);

#if !UNITY_EDITOR

            Vector3 angularVelocity = Accelerometer.current.acceleration.ReadValue();
            Vector3 movement = new Vector3(angularVelocity.x, 0.0f, angularVelocity.y);
            rb.AddForce(movement * speed * Time.deltaTime);

            player.Rotate(Vector3.right * angularVelocity.y * speed, Space.World);
#else


            Vector3 movement = new Vector3(Input.gyro.gravity.x, 0.0f, Input.gyro.gravity.y);
            rb.AddForce(movement * speed * Time.deltaTime);

            player.Rotate(Vector3.right * Input.acceleration.y * speed, Space.World);

#endif
            // Player movement in mobile devices
            // Building of force vector 
            //Vector3 movement = new Vector3(Input.gyro.gravity.x, 0.0f, Input.gyro.gravity.y);
            // Adding force to rigidbody
            //rb.AddForce(movement * speed * Time.deltaTime);


        }

    }
}
