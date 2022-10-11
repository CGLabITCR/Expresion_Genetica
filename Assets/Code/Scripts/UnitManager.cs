using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitManager : MonoBehaviour
{
    public static UnitManager Instance;

    private List<GameObject> _boardElements = new List<GameObject>();


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void inserElement(GameObject nuevoElemento)
    {
        _boardElements.Add(nuevoElemento);
    }

    public void showLenghtNotification()
    {
        string listLen = "Hay " +_boardElements.Count.ToString() + " elementos en la lista.";
        CanvasManager.Instance.sendNotification(listLen, 3);
    }

}
