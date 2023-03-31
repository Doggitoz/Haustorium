using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroAudio : MonoBehaviour
{
    [SerializeField] float delay = 3f;
    //[SerializeField] AudioClip clip;

    AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        StartCoroutine(playIntroAudio());
    }

    private void Update()
    {
        transform.position = PlayerManager.Instance.Controller.gameObject.transform.position;
    }

    IEnumerator playIntroAudio()
    {
        yield return new WaitForSecondsRealtime(delay);
        audioSource.Play();
        yield return new WaitForSecondsRealtime(audioSource.clip.length);
        //Destroy(gameObject);
    }
}
