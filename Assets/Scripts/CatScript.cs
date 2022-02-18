using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CatScript : MonoBehaviour
{
    public GameObject deathPlane;
    public AudioClip winSound;
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI resultText;
    private AudioSource source;
    private bool active = false;
    private void Start()
    {
        source = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!active)
        {
            deathPlane.active = false;
            source.PlayOneShot(winSound);
            resultText.text = timerText.text;
            active = true;
        }
    }
}
