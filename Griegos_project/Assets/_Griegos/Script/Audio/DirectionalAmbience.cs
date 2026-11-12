
using UnityEngine;

public class DirectionalAmbience : MonoBehaviour
{
    [System.Serializable]
    public class SoundPoint
    {
        public string soundName;
        public AudioSource audioSource;
        public Transform soundPosition;

        [Range(0, 180)]
        public float hearingAngle = 90f;

        [Range(0, 1)]
        public float maxVolume = 1f;
    }

    public Transform playerCamera;
    public SoundPoint[] sounds;

    public float fadeSpeed = 2f;

    void Update()
    {
        foreach (SoundPoint sound in sounds)
        {
            if (sound.audioSource == null ||
                sound.soundPosition == null)
                continue;

            Vector3 direction =
                sound.soundPosition.position -
                playerCamera.position;

            float angle = Vector3.Angle(
                playerCamera.forward,
                direction
            );

            float volumeMultiplier =
                Mathf.Clamp01(
                    1f - angle / sound.hearingAngle
                );

            float targetVolume =
                volumeMultiplier * sound.maxVolume;

            sound.audioSource.volume = Mathf.Lerp(
                sound.audioSource.volume,
                targetVolume,
                Time.deltaTime * fadeSpeed
            );
        }
    }
}
