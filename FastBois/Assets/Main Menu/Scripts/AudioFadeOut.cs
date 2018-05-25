using UnityEngine;
using System.Collections;

public static class AudioFadeOut
{

    public static IEnumerator FadeOut(AudioSource audioSource, float FadeTime)
    {

        while (audioSource.volume > 0)
        {

            yield return null;
        }

        audioSource.Stop();
    }

}