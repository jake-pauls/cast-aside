using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class PlayAudioInRange : MonoBehaviour
{
    private AudioSource audioSource;

    void Start() => audioSource = gameObject.GetComponent<AudioSource>();

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player")
            audioSource.Play();
    }

    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "Player")
            audioSource.Stop();
    }
}
