using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    public float moveSpeed;

    public int scoreValue;

    public GameObject explosionEffect;
    public GameObject scoreImage;

    GameManager gm;

    AsteroidKeyPicker keyPicker;

    PlanetExplode planetExplode;

    private GameObject[] SFXAudioSources;
    private AudioSource audioSource;
    public AudioClip hitClip;
    public AudioClip incorrectClip;
    public AudioClip playerHitClip;

    private CameraShake cameraShake;
    public float cameraShakeDuration;
    public float cameraShakeMag;

    private void Awake()
    {
        gm = FindObjectOfType<GameManager>();
        keyPicker = GetComponent<AsteroidKeyPicker>();
        planetExplode = FindObjectOfType<PlanetExplode>();
        SFXAudioSources = GameObject.FindGameObjectsWithTag("SFXAudio");
        audioSource = SFXAudioSources[0].GetComponent<AudioSource>();
        cameraShake = FindObjectOfType<CameraShake>();
    }

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(0, 0, 0), moveSpeed * Time.deltaTime);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Letters"))
        {
            LetterBehavior lb = other.GetComponent<LetterBehavior>();
            if(keyPicker.keyCombo.Count == lb.keyCodes.Count)
            {
                int correct = 0;
                for (int i = 0; i < keyPicker.keyCombo.Count; i++)
                {
                    for (int j = 0; j < lb.keyCodes.Count; j++)
                    {
                        if (keyPicker.keyCombo[i] == lb.keyCodes[j])
                        {
                            correct++;
                        }
                    }
                }
                if(correct == keyPicker.keyCombo.Count)
                {
                    Collider[] col = GetComponentsInChildren<Collider>();
                    for (int i = 0; i < col.Length; i++)
                    {
                        col[i].enabled = false;
                    }
                    Debug.Log("scoreCalled");
                    Instantiate(explosionEffect, other.transform.position, explosionEffect.transform.rotation, null);
                    Instantiate(scoreImage, transform.position, scoreImage.transform.rotation, null);
                    audioSource.PlayOneShot(hitClip);
                    gm.AddScore(scoreValue);
                    Destroy(other.gameObject);
                    Destroy(gameObject);
                }
                else
                {
                    Destroy(other.gameObject);
                    audioSource.PlayOneShot(incorrectClip);
                }
            }
            else
            {
                Destroy(other.gameObject);
            }
            //Debug.Log("collide");
        }
        if (other.CompareTag("Player"))
        {
            StartCoroutine("HitPlayer");
        }
    }

    public IEnumerator HitPlayer()
    {
        StartCoroutine(cameraShake.Shake(cameraShakeDuration, cameraShakeMag));
        SpriteRenderer[] spriteRenderer = GetComponentsInChildren<SpriteRenderer>();
        Collider[] col = GetComponentsInChildren<Collider>();
        gm.smolDestructoWave.Play();
        for (int i = 0; i < spriteRenderer.Length; i++)
        {
            spriteRenderer[i].enabled = false;
        }
        for (int i = 0; i < col.Length; i++)
        {
            col[i].enabled = false;
        }
        planetExplode.hit = true;
        audioSource.PlayOneShot(playerHitClip);
        yield return new WaitForSeconds(1f);
        planetExplode.min -= .3f;
        planetExplode.hit = false;
        Destroy(gameObject);
    }

}
