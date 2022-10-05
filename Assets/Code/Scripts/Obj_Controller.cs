using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class Obj_Controller : MonoBehaviour
{
    Material original_trigger_material;
    public Text notificationText;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        original_trigger_material = other.GetComponent<Renderer>().material;

        if (other.gameObject.CompareTag(this.gameObject.tag))
        {
            original_trigger_material.color =  new Color(0f, 1f, 0f, 0.5f);

            StartCoroutine(sendNotification("hola", 3));
            
        }
        else {
            original_trigger_material.color = new Color(1f, 0f, 0f, 0.5f);
        }
        
    }

    private void OnTriggerExit(Collider other)
    {
        original_trigger_material.color = new Color(0f, 0f, 1f, 0.5f);
    }


    IEnumerator sendNotification(string text, int time)
    {
        notificationText.text = text;
        yield return new WaitForSeconds(time);
        notificationText.text = " ";

    }
}
