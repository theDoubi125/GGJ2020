using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.anyKeyDown)
        {
            SoundManagerScript.instance.PlayOneShotSound(SoundManagerScript.AudioClips.MenuStart);
            StartCoroutine(StartMethod(0.5f));
        }
    }

    private IEnumerator StartMethod(float delay)
    {
        yield return new WaitForSeconds(delay);
        SoundManagerScript.instance.StartPlaySounds();
        SceneManager.LoadScene(1);

    }
}
