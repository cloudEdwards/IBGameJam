using UnityEngine;

namespace _Project.Script.Persistent
{
    public static class Utility
    {
        public static void FaceToCamera(this Transform transform)
        {
            transform.LookAt(transform.position + Camera.main.transform.rotation * Vector3.back, Camera.main.transform.rotation * Vector3.up);
        }
    }
}