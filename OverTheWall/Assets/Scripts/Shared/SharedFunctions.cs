﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OverTheWall.SharedFunctions
{
    public class SharedFunctions : MonoBehaviour
    {

        public static Vector2 CalculateAcceleration(Vector2 start, Vector2 end, float time)
        {
            Vector2 acceleration = Vector2.zero;

            Vector2 distance = start - end;

            acceleration = distance * time;

            return acceleration;
        }

        public static float CalculateSpeed(Vector2 start, Vector2 end, float time)
        {
            return ((end - start) / time).magnitude;
        }
    }

}
