using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PostProcessing;
using UnityStandardAssets.Characters.FirstPerson;
using System;

public class start_up : MonoBehaviour {

    public PostProcessingProfile ppProfile;
    public GameObject canvas;
    public GameObject player;

    private float velocity = 2.0f;
    private VignetteModel.Settings vignetteSettings;
    Dictionary<bool, Func<float, float, float>> operations = new Dictionary<bool, Func<float, float, float>>()
    {
        {false, (x, y) => { return x+y; } },
        {true, (x, y) => { return x-y; }}

    };

    // 1, 0.42, 0.82, 0.23, 0.44, 0

    // Use this for initialization
    void Start () {
        vignetteSettings = ppProfile.vignette.settings;
        StartCoroutine(vignetteAnimation());
	}


    IEnumerator vignetteAnimation()
    {
        player.GetComponent<FirstPersonController>().enabled = false;

        ppProfile.vignette.enabled = true;
        vignetteSettings.intensity = 1;
        ppProfile.vignette.settings = vignetteSettings;

        StartCoroutine(changeVignette(0.42f));
        yield return new WaitForSeconds(1f);
        StartCoroutine(changeVignette(0.82f));
        yield return new WaitForSeconds(1f);
        StartCoroutine(changeVignette(0.23f));
        yield return new WaitForSeconds(1f);
        StartCoroutine(changeVignette(0.44f));
        yield return new WaitForSeconds(1f);
        StartCoroutine(changeVignette(0.05f));
        yield return new WaitForSeconds(1f);
        vignetteSettings.intensity = 0;
        ppProfile.vignette.settings = vignetteSettings;
        ppProfile.vignette.enabled = false;

        // enable ui
        canvas.SetActive(true);
        player.GetComponent<FirstPersonController>().enabled = true;


    }


    private IEnumerator changeVignette(float value)
    {
        Func<float, float, float> operation = operations[vignetteSettings.intensity > value];
        if (vignetteSettings.intensity < value)
        {
            while(vignetteSettings.intensity < value)
            {
                vignetteSettings.intensity = operation(vignetteSettings.intensity, 0.05f);
                yield return new WaitForSeconds(0.05f);
                ppProfile.vignette.settings = vignetteSettings;
                Debug.Log(vignetteSettings.intensity);

            }
        } else if (vignetteSettings.intensity > value)
        {
            while (vignetteSettings.intensity > value)
            {
                vignetteSettings.intensity = operation(vignetteSettings.intensity, 0.05f);
                yield return new WaitForSeconds(0.05f);
                ppProfile.vignette.settings = vignetteSettings;
                Debug.Log(vignetteSettings.intensity);

            }
        }




    }

}
