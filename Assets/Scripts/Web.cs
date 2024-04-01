using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;
using SimpleJSON;
using Structs;

public class Web : MonoBehaviour
{
    [SerializeField] List<GameObject> Model;
    [SerializeField] itemStr[] items = new itemStr[3];
    [SerializeField] correctObjectStr[] objects = new correctObjectStr[3];
    [SerializeField] correctMaterialStr[] materials = new correctMaterialStr[3];

    void Start()
    {
        for (int j = 0; j < Model.Count; j++)
        {
            List<Vector3> positions = new List<Vector3>();
            List<Vector3> normals = new List<Vector3>();
            List<Vector2> uvs = new List<Vector2>();
            List<int> indices = new List<int>();
            Quaternion rotation;
            Vector3 position;
            Mesh modelMesh = Model[j].GetComponent<MeshFilter>().mesh;
            Transform transformMesh = Model[j].GetComponent<Transform>();
            position = transformMesh.position;
            rotation = transformMesh.rotation;

            for (int i = 0; i < modelMesh.vertices.Length; i++)
            {
                positions.Add(modelMesh.vertices[i]);
                normals.Add(modelMesh.normals[i]);
                uvs.Add(modelMesh.uv[i]);
            }
            indices.AddRange(modelMesh.GetIndices(0));
            
            itemStr baseItem = new itemStr
            {
                id = j + 1,
                icon = " ",
                name = this.Model[j].name
            };
            items[j] = baseItem;

            transformStr transformer = new transformStr
            {
                position = position,
                rotation = rotation
            };
            meshStr mesher = new meshStr
            {
                positions = positions.ToArray(),
                normals = normals.ToArray(),
                uvs = uvs.ToArray(),
                indices = indices.ToArray()
            };
            materialStr materialer = new materialStr
            {
                basecolor = " ",
                normal = " ",
                roughness = " "
            };

            objectStr objecter = new objectStr
            {
                transform = transformer,
                mesh = mesher,
                material = -j - 1
            };
            correctObjectStr baseObject = new correctObjectStr
            {
                id = j + 1,
                objects = objecter
            };
            objects[j] = baseObject;
            correctMaterialStr baseMaterial = new correctMaterialStr
            {
                guid = -j - 1,
                texture = materialer
            };
            materials[j] = baseMaterial;
        }
        dataBase data = new dataBase
        {
            items = this.items,
            objects = this.objects,
            materials = this.materials
        };

        string json = JsonUtility.ToJson(data, true);

        File.WriteAllText("C:\\WelcomeToHell\\dataBase.json", json);

    }

    void Update()
    {

    }
}
