using System.Collections;
using UnityEngine;

namespace Framework
{
    public class PlayAudioTask : MonoBehaviour, ITask
    {
        public AudioSource audioSource;
        public AudioClip audioClip;
        public bool waitUntilFinished { get; set ; }

        public IEnumerator ExecuteTask()
        {
            if (audioSource == null || audioClip == null)
                yield break;

            audioSource.clip = audioClip;
            audioSource.Play();

            if (waitUntilFinished)
            {
                yield return new WaitForSeconds(audioClip.length);
            }
        }
    }
}
