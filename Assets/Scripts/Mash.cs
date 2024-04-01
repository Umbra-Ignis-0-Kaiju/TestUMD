using UnityEngine;

namespace Structs
{
    [System.Serializable]
    public struct transformStr
    {
        public Vector3 position;
        public Quaternion rotation;
    }

    [System.Serializable]
    public struct meshStr
    {
        public Vector3[] positions;
        public  Vector3[] normals;
        public Vector2[] uvs;
        public int[] indices;
    }

    [System.Serializable]
    public struct objectStr
    {
        public transformStr transform;
        public meshStr mesh;
        public int material;
    }

    [System.Serializable]
    public struct materialStr
    {
        public string basecolor;
        public string normal;
        public string roughness;
    }


    [System.Serializable]
    public struct itemStr
    {
        public int id;
        public string icon;
        public string name;
    }

    [System.Serializable]
    public struct correctObjectStr
    {
        public int id;
        public objectStr objects;
    }

    [System.Serializable]
    public struct correctMaterialStr
    {
        public int guid;
        public materialStr texture;
    }

    [System.Serializable]
    public struct dataBase
    {
        public itemStr[] items;
        public correctObjectStr[] objects;
        public correctMaterialStr[] materials;
    }
}
