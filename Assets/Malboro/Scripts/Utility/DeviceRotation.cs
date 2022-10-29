using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utility
{
    public static class DeviceRotation
    {
        private static bool gyroInitialized = false;

        public static bool HasGyroscope { get { return SystemInfo.supportsGyroscope; } }

        public static Quaternion Get()
        {
            if (!gyroInitialized)
            {
                InitGyro();
            }

            return HasGyroscope ? ReadGyroScopeRotation() : Quaternion.identity;
        }

        private static void InitGyro()
        {
            if (HasGyroscope)
            {
                Input.gyro.enabled = true;
                Input.gyro.updateInterval = 0.0167f;
            }
            gyroInitialized = true;
        }

        private static Quaternion ReadGyroScopeRotation()
        {
            return new Quaternion(0.5f, 0.5f, -0.5f, 0.5f) * Input.gyro.attitude * new Quaternion(0, 0, 1, 0);
        }
    }
}
