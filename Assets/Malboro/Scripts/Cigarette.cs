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
        public float rotateSpeed = 10;
        float rotateDirection = 0;

        [SerializeField] Transform player;
        Vector3 previousPos;

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

#if !UNITY_EDITOR
            InputSystem.EnableDevice(Gyroscope.current);
            InputSystem.EnableDevice(Accelerometer.current);
#else
            Input.gyro.enabled = true;
            Input.gyro.updateInterval = 0.0167f;
#endif
            isKinematic(true);
        }

        private void Update()
        {
            if (EventManager.Instance.isGameOver || EventManager.Instance.isUIOpen || rb.isKinematic)
            {
                return;
            }


#if !UNITY_EDITOR

            Vector3 angularVelocity = Accelerometer.current.acceleration.ReadValue();
            Vector3 movement = new Vector3(angularVelocity.x, 0.0f, angularVelocity.y);
            rb.AddForce(movement * speed * Time.deltaTime);

            if (previousPos.z > rb.position.z)
            {
                rotateDirection = -1;
                //rotateDirection = (angularVelocity.y < 0 ? angularVelocity.y : -angularVelocity.y);
            }
            else if (previousPos.z < rb.position.z)
            {
                rotateDirection = 1;
                //rotateDirection = (angularVelocity.y > 0 ? angularVelocity.y : -angularVelocity.y);
            }
            else
            {
                rotateDirection = 0;
            }

            Debug.Log(angularVelocity);
            player.Rotate(Vector3.right * rotateDirection * rotateSpeed, Space.World);
#else


            Vector3 movement = new Vector3(Input.gyro.gravity.x, 0.0f, Input.gyro.gravity.y);
            rb.AddForce(movement * speed * Time.deltaTime);

            if (previousPos.z > rb.position.z)
            {
                rotateDirection = -1;
                //rotateDirection = (movement.z < 0 ? movement.z : -movement.z);
            }
            else if (previousPos.z < rb.position.z)
            {
                rotateDirection = 1;
                //rotateDirection = (movement.z > 0 ? movement.z : -movement.z);
            }
            else
            {
                rotateDirection = 0;
            }
            

            player.Rotate(Vector3.right * rotateDirection * rotateSpeed, Space.World);

#endif

            
        }

        private void LateUpdate()
        {
            previousPos = rb.position;
        }

    }
}
