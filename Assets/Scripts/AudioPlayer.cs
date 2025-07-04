using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    [Header("Shooting")]
    [SerializeField] AudioClip shootingClip; // An AudioClip to be played when shooting
    [SerializeField][Range(0f, 1f)] float shootingVolume = 1f; // A float value to control the volume of the shooting sound, restricted to a range between 0 and 1

    [Header("Damage")]
    [SerializeField] AudioClip damageClip; // An AudioClip to be played when taking damage
    [SerializeField][Range(0f, 1f)] float damageVolume = 1f; // A float value to control the volume of the damage sound, restricted to a range between 0 and 1

    static AudioPlayer instance; // This line declares a static variable instance of type AudioPlayer to implement the singleton pattern

    void Awake()
    {
        ManageSingleton(); // This Awake method calls ManageSingleton to ensure that only one instance of AudioPlayer exists
    }

    void ManageSingleton()
    {
        if (instance != null) // If an instance of AudioPlayer already exists (instance != null), it deactivates and destroys the current GameObject
        {
            gameObject.SetActive(false);        // before set active to false to be sure
            Destroy(gameObject);
        }
        else // If no instance exists, it sets the instance to this instance and marks the GameObject to not be destroyed when loading a new scene with DontDestroyOnLoad
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void PlayShootingClip() // This method plays the shooting sound clip at the specified volume by calling the PlayClip method with shootingClip and shootingVolume as parameters
    {
        PlayClip(shootingClip, shootingVolume);
    }

    public void PlayDamageClip() // This method plays the damage sound clip at the specified volume by calling the PlayClip method with damageClip and damageVolume as parameters
    {
        PlayClip(damageClip, damageVolume);
    }

    void PlayClip(AudioClip clip, float volume) // This method handles the actual playing of an audio clip
    {
        if (clip != null) // checks if the clip parameter is not null
        {
            Vector3 cameraPos = Camera.main.transform.position; // If the clip is valid, it gets the position of the main camera (cameraPos)
            AudioSource.PlayClipAtPoint(clip, cameraPos, volume); // It uses AudioSource.PlayClipAtPoint to play the clip at the camera's position with the specified volume.
                                                                  // This ensures the sound is played in a 3D space, centered on the camera, making it audible regardless
                                                                  // of the listener's position in the game world
        }
    }
}
