using System;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;
using SimpleJSON;
using Structs;


public class FromWeb : MonoBehaviour
{
    [SerializeField] Material mat;
    [SerializeField] GameObject ItemPref;
    [SerializeField] GameObject Loyaut;


    public static int number = -1;
    private int id;
    private int guid;

    public static dataBase fromServer;

    void Start()
    {
        if (number < 0)
            StartCoroutine(getData());
        else
            findIds();
    }

    IEnumerator getData()
    {

        if (number == -1)
        {
            WWW _www = new WWW("http://variant-unity-test-server.loc/dataBase.json");
            yield return _www;
            number = 0;
            if (_www != null)
            {
                fromServer = JsonUtility.FromJson<dataBase>(_www.text);
                Debug.Log("Connected");
                for (int i = 0; i < fromServer.items.Length; i++)
                {
                    WWW _image = new WWW(fromServer.items[i].icon);
                    yield return _image;
                    CreateButton(fromServer.items[i].id, fromServer.items[i].name, _image.texture);
                }
            }
            else
            {
                Debug.Log("Auch");
            }
        }
        else
        {
            for (int i = 0; i < fromServer.items.Length; i++)
            {
                WWW _image = new WWW(fromServer.items[i].icon);
                yield return _image;
                CreateButton(fromServer.items[i].id, fromServer.items[i].name, _image.texture);
            }
        }
    }

    private void CreateButton(int id, string name, Texture2D texture)
    {
        GameObject button = Instantiate(ItemPref, Loyaut.transform);
        Sprite image = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.zero);
        button.GetComponent<CreateNewObject>().ChangeName(name);
        button.GetComponent<CreateNewObject>().ChangeImage(image);
        button.GetComponent<CreateNewObject>().ID = id;
    }

    public void WatchID(GameObject button)
    {
        number = button.GetComponent<CreateNewObject>().ID;
    }

    void findIds()
    {
        //int id = 0;
        for (int i = 0; i < fromServer.items.Length; i++)
        {
            if (number == fromServer.items[i].id)
            {
                id = fromServer.items[i].id;
            }
        }

        //int guid = 0;
        for (int i = 0; i < fromServer.objects.Length; i++)
        {
            if (id == fromServer.objects[i].id)
            {
                id = i;
                guid = fromServer.objects[i].objects.material;
            }
        }

        for (int i = 0; i < fromServer.materials.Length; i++)
        {
            if (guid == fromServer.materials[i].guid)
            {
                guid = i;
            }
        }

        StartCoroutine(createModel(id, guid));
    }

    IEnumerator createModel(int id, int guid)
    {
        Debug.Log("Connected for texturing");
        WWW _nat = new WWW(fromServer.materials[guid].texture.basecolor);
        WWW _nmt = new WWW(fromServer.materials[guid].texture.roughness);
        WWW _nnt = new WWW(fromServer.materials[guid].texture.normal);
        yield return _nat;
        yield return _nmt;
        yield return _nnt;

        Mesh modelMesh = new Mesh();

        modelMesh.vertices = fromServer.objects[id].objects.mesh.positions;
        modelMesh.normals = fromServer.objects[id].objects.mesh.normals;
        modelMesh.uv = fromServer.objects[id].objects.mesh.uvs;
        modelMesh.triangles = fromServer.objects[id].objects.mesh.indices;

        mat.SetTexture("_MainTex", _nat.texture);
        mat.SetTexture("_MetallicGlossMap", _nmt.texture);
        mat.SetTexture("_BumpMap", _nnt.texture); 

        GameObject dat = new GameObject("Dat");
        MeshFilter datFilter = dat.AddComponent<MeshFilter>();
        datFilter.mesh = modelMesh;

        MeshRenderer datRender = dat.AddComponent<MeshRenderer>();
        datRender.material = mat;
    }

    public void ComeBack()
    {
        number = -2;
    }

    void Update()
    {
      
    }

}
