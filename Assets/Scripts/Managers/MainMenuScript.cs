using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
using TMPro;

public class MainMenuScript : MonoBehaviour
{
    public TextMeshProUGUI title;

    // Start is called before the first frame update
    void Start()
    {
        DOTween.Init();
        StartCoroutine(fading());
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
        SceneManager.LoadScene("Scenes/GameScene");

    }

    IEnumerator fading()
    {

        var tween1 = title.DOFade(1, 1);

        yield return tween1.WaitForCompletion();

        var tween = title.DOFade(0, 1);

        yield return tween.WaitForCompletion();
        StartCoroutine(fading());
    }
}
