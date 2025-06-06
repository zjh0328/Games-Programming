using System.Collections;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [SerializeField] public AudioSource[] sfx;
    [SerializeField] private AudioSource[] bgm;
    [SerializeField] private float MinHearDistance;

    public bool playingBGM;
    private int bgmIndex;
    private bool canPlaySFX = false;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        Invoke("StartSFX", 0.2f);
    }

    private void Update()
    {
        if (!playingBGM)
        {
            StopBGM();
        }
        else
        {
            if (!bgm[bgmIndex].isPlaying)
            {
                PlayBGM(bgmIndex);
            }
        }
    }

    public void PlaySFX(int sfxIndex, Transform sfxSourceTransform)
    {
        if (!canPlaySFX || sfxIndex >= sfx.Length) return;

        if (sfxSourceTransform != null &&
            Vector2.Distance(PlayerManager.instance.player.transform.position, sfxSourceTransform.position) > MinHearDistance)
        {
            return;
        }

        sfx[sfxIndex].Play();
    }

    public void StopSFX(int sfxIndex)
    {
        if (sfxIndex < sfx.Length)
        {
            sfx[sfxIndex].Stop();
        }
    }

    public void PlayBGM(int bgmIndexToPlay)
    {
        StopBGM();

        if (bgmIndexToPlay < bgm.Length)
        {
            bgmIndex = bgmIndexToPlay;
            bgm[bgmIndex].Play();
        }
        else
        {
            Debug.Log("BGM Index out of range");
        }
    }

    public void StopBGM()
    {
        for (int i = 0; i < bgm.Length; i++)
        {
            bgm[i].Stop();
        }
    }

    public IEnumerator DecreaseVolume(AudioSource audio)
    {
        float defaultVolume = audio.volume;

        while (audio.volume > 0.1f)
        {
            audio.volume -= audio.volume * 0.2f;
            yield return new WaitForSeconds(0.2f);

            if (audio.volume <= 0.1f)
            {
                audio.Stop();
                audio.volume = defaultVolume;
                break;
            }
        }
    }

    public void StartSFX()
    {
        canPlaySFX = true;
    }

    public void PlayAreaLoopSFX(int index)
    {
        if (index < sfx.Length && !sfx[index].isPlaying)
        {
            sfx[index].volume = 1;
            sfx[index].Play();
        }
    }

}
