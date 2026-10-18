using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] SFXPlayer[] SFXPlayers;
    [SerializeField] SFXPlayer[] bossSFXPlayers;

    [SerializeField] AudioSource playerSource;
    [SerializeField] PlayerWeaponController playerWeaponController;
    [SerializeField] PlayerAI[] playerAIs;

    [SerializeField] GameObject[] CellsLocation;

    int i = 0;
    int j = 0;

    public void Init()
    {
        foreach (SFXPlayer sfxPlayer in SFXPlayers)
            sfxPlayer.Init();
        foreach (SFXPlayer sfxPlayer in bossSFXPlayers)
            sfxPlayer.Init();
        i = 0;
    }

    public void SetVolume(float Volume)
    {
        foreach (SFXPlayer sfxPlayer in SFXPlayers)
            sfxPlayer.GetAudioSource().volume = Volume;

        foreach (SFXPlayer sfxPlayer in bossSFXPlayers)
            sfxPlayer.GetAudioSource().volume = Volume;

        foreach (GameObject cellGroup in CellsLocation)
        {
            AudioSource[] audioSources = cellGroup.GetComponentsInChildren<AudioSource>();
            foreach (AudioSource audioSource in audioSources)
                audioSource.volume = Volume;
        }

        foreach (PlayerAI playerAI in playerAIs)
            playerAI.GetAudioSource().volume = Volume;

        playerSource.volume = Volume;
        AudioSource[] weaponAudioSources = playerWeaponController.GetAudioSources();
        weaponAudioSources[0].volume = Volume;
        weaponAudioSources[1].volume = Volume;
    }
    public void PlaySFX(Transform other, AudioClip audioClip)
    {
        SFXPlayer sfxPlayer = SFXPlayers[i];
        int brk = 0;
        while (sfxPlayer.IsPlaying())
        {
            i += 1;
            brk += 1;
            if (i == SFXPlayers.Length) i = 0;
            if (brk == SFXPlayers.Length) return;
            sfxPlayer = SFXPlayers[i];
        }
        sfxPlayer.PlaySFX(other, audioClip);
    }

    public void BossSFXPlay(Transform other, AudioClip audioClip)
    {
        SFXPlayer bossSFXPlayer = bossSFXPlayers[j];
        int brk = 0;
        while (bossSFXPlayer.IsPlaying())
        {
            j += 1;
            brk += 1;
            if (j == bossSFXPlayers.Length) j = 0;
            if (brk == bossSFXPlayers.Length) return;
            bossSFXPlayer = bossSFXPlayers[j];
        }
        bossSFXPlayer.PlaySFX(other, audioClip);
    }
}
