using System.Collections;
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
            
        public static Bounds OrthographicBounds(Camera camera)
        {
            float screenAspect = (float)Screen.width / (float)Screen.height;
            float cameraHeight = camera.orthographicSize * 2;
            Bounds bounds = new Bounds(
                camera.transform.position,
                new Vector3(cameraHeight * screenAspect, cameraHeight, 0));
            return bounds;
        }
    }

}

