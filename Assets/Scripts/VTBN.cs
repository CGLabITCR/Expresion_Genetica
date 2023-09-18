using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;


public class VTBN : MonoBehaviour
{
    public GameObject cube;
    public VirtualButtonBehaviour VT_btn;
    // Start is called before the first frame update
    void Start()
    {
        VT_btn.RegisterOnButtonPressed(OnButtonPressed);

        VT_btn.RegisterOnButtonReleased(OnButtonReleased);

        cube.SetActive(false);
    }

    public void OnButtonPressed(VirtualButtonBehaviour vt_btn)
    {
        cube.SetActive(true);
    }

    public void OnButtonReleased(VirtualButtonBehaviour vt_btn)
    {
        cube.SetActive(false);
    }

}
