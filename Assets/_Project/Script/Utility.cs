using UnityEngine;

namespace _Project.Script.Persistent
{
    public static class Utility
    {
        public static void FaceToCamera(this Transform transform)
        {
            transform.LookAt(transform.position + Camera.main.transform.rotation * Vector3.back, Camera.main.transform.rotation * Vector3.up);
        }

        public static Vector3 SetValues(this Vector3 vector, float? x = null, float? y = null, float? z = null)
        {
            vector.x = x ?? vector.x;
            vector.y = y ?? vector.y;
            vector.z = z ?? vector.z;
            
            return vector;
        }
    }
}