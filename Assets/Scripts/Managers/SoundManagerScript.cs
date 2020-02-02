using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManagerScript : MonoBehaviour
{
    public enum AudioClips
    {
        DoorsClose,
        DoorsOpen,
        ForkliftMoving,
        GameLose,
        GameWin,
        MenuSelect,
        MenuStart,
        ObjectFallsHeavy, // Multiple
        ObjectFallsLight, // Multiple
        ObjectPickUp,
        //ObjectPickUpAlternative,
        ObjectPutColor,
        ObjectPutWorkbench,
        ObjectRepair,
        //ObjectRepairAlternative,
        ObjectThrow, // Multiple
        PlayerRolling,
        //PlayerRollingLoopOriginal,
        ShipArriving,
        ShipConnectPiece,
        ShipCrashDoor,
        ShipLeaving,
        ShipDriveBy,
        ShipWarning, // Multiple
        ToolBlowtorch,
        ToolGet
    };

    public static SoundManagerScript instance = null;

    public AudioSource musicSource;
    public AudioSource sfxSource;

    public AudioSource player1Source;
    public AudioSource player2Source;

    public Rigidbody player1RigidBody;
    public Rigidbody player2RigidBody;

    bool muteSFX = false;

    [Header("Doors")]
    public AudioClip doorsOpen;
    public AudioClip doorsClose;

    [Header("Forklift")]
    public AudioClip forkliftMoving;

    [Header("Game")]
    public AudioClip gameLose;
    public AudioClip gameWin;

    [Header("Menu")]
    public AudioClip menuSelect;
    public AudioClip menuStart;

    [Header("Objects")]
    public List<AudioClip> objectFallsHeavy;
    public List<AudioClip> objectFallsLight;

    public AudioClip objectPickUp;

    public AudioClip objectPutColor;
    public AudioClip objectPutWorkbench;

    public AudioClip objectRepair;

    public List<AudioClip> objectThrow;

    [Header("Player")]
    public AudioClip playerRolling;

    [Header("Ship")]
    public AudioClip shipArriving;
    public AudioClip shipConnectPiece;
    public AudioClip shipLeaving;
    public AudioClip shipCrashDoor;
    public List<AudioClip> shipWarning;
    public List<AudioClip> shipDriveBy;

    [Header("Tool")]
    public AudioClip toolBlowtorch;
    public AudioClip toolGet;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);

        if (instance == null)
        {
            //if not, set instance to this
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        musicSource.loop = true;
        StartCoroutine(DelayPlayMusic());

        player1Source.loop = true;
        player1Source.clip = playerRolling;
        player1Source.Play();

        player2Source.loop = true;
        player2Source.clip = playerRolling;
        player2Source.Play();
    }

    void Update()
    {
        player1Source.volume = Mathf.Max(0.1f, 0.02f * player1RigidBody.velocity.magnitude);
        player2Source.volume = Mathf.Max(0.1f, 0.02f * player1RigidBody.velocity.magnitude);
    }

    IEnumerator DelayPlayMusic()
    {
        yield return new WaitForSeconds(1.0f);
        PlayMusic();
    }

    public void PlayOneShotSound(AudioClips clipType)
    {
        AudioClip clip = null;
        switch (clipType)
        {
            case AudioClips.DoorsOpen:
                clip = doorsOpen;
                break;
            case AudioClips.DoorsClose:
                clip = doorsClose;
                break;
            case AudioClips.ForkliftMoving:
                clip = forkliftMoving;
                break;
            case AudioClips.GameLose:
                clip = gameLose;
                break;
            case AudioClips.GameWin:
                clip = gameWin;
                break;
            case AudioClips.MenuSelect:
                clip = menuSelect;
                break;
            case AudioClips.MenuStart:
                clip = menuStart;
                break;
            case AudioClips.ObjectFallsHeavy:
                clip = objectFallsHeavy[Random.Range(0, objectFallsHeavy.Count)];
                break;
            case AudioClips.ObjectFallsLight:
                clip = objectFallsLight[Random.Range(0, objectFallsLight.Count)];
                break;
            case AudioClips.ObjectPickUp:
                clip = objectPickUp;
                break;
            case AudioClips.ObjectPutColor:
                clip = objectPutColor;
                break;
            case AudioClips.ObjectPutWorkbench:
                clip = objectPutWorkbench;
                break;
            case AudioClips.ObjectRepair:
                clip = objectRepair;
                break;
            case AudioClips.ObjectThrow:
                clip = objectThrow[Random.Range(0, objectThrow.Count)];
                break;
            case AudioClips.PlayerRolling:
                // TODO: loop ?
                clip = playerRolling;
                break;
            case AudioClips.ShipArriving:
                clip = shipArriving;
                break;
            case AudioClips.ShipConnectPiece:
                clip = shipConnectPiece;
                break;
            case AudioClips.ShipCrashDoor:
                clip = shipCrashDoor;
                break;
            case AudioClips.ShipLeaving:
                clip = shipLeaving;
                break;
            case AudioClips.ShipWarning:
                clip = shipWarning[Random.Range(0, shipWarning.Count)];
                break;
            case AudioClips.ShipDriveBy:
                clip = shipDriveBy[Random.Range(0, shipDriveBy.Count)];
                break;
            case AudioClips.ToolBlowtorch:
                clip = toolBlowtorch;
                break;
        }
        if (clip != null)
        {
            sfxSource.PlayOneShot(clip);
        }
    }

    public void PlayMusic()
    {
        musicSource.Play();
    }


    public void MuteSound()
    {
        musicSource.mute = !musicSource.mute;
    }

    public void MuteSFX()
    {
        sfxSource.mute = !sfxSource.mute;
    }

}