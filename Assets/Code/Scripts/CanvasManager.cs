using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasManager : MonoBehaviour
{
    public static CanvasManager Instance;
    [SerializeField] private Text _notificationText;


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

    /*
     * Show a notification for x amount of time
     */
    public void sendNotification( string message, int time)
    {
        StartCoroutine(showNotification(message, time));
    }

    private IEnumerator showNotification(string text, int seconds)
    {
        _notificationText.text = text;
        yield return new WaitForSeconds(seconds);
        _notificationText.text = " ";

    }
}
