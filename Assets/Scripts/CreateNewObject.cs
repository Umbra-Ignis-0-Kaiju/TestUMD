using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CreateNewObject : MonoBehaviour
{
    public int ID;
    [SerializeField] GameObject Image;
    [SerializeField] GameObject Text;

    public void ChangeName(string name)
    {
        Text.GetComponent<TextMeshProUGUI>().text = name;
    }
    
    public void ChangeImage(Sprite image)
    {
        Image.GetComponent<Image>().sprite = image;
    }

}
