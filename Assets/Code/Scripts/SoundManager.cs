using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Este Manager se encarga de los sonidos de la aplicacion
 */
public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;
    [SerializeField] private AudioSource _successSoundEffectSource, _failureSoundEffectSource;


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

    public void PlaySuccessSound()
    {
        _successSoundEffectSource.Play();

    }

    public void PlayFailureSound()
    {
        _failureSoundEffectSource.Play();
    }


}
