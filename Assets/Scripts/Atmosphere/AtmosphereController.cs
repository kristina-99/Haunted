using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using static GameConstants;

/// <summary>
/// Drives the game's mood via two global post-processing Volumes:
///   * Night  - the dark night mood, shown to non-Haunted players during the night phase.
///   * Ghost  - the ghostly view; for the Haunted player it REPLACES the night look
///              during the night phase (the night volume is suppressed for them).
///
/// Day phase = both volumes off, so the scene's default atmosphere shows through for
/// everyone (there is no dedicated day volume).
///
/// The Volumes live in the scene and are assigned in the Inspector, so their profiles
/// can be tuned in edit mode. At runtime this controller only animates their weights.
///
/// Scene setup:
///   * Create 2 Global Volume GameObjects, assign the Night / Ghost profiles.
///   * Night at a low Priority (e.g. 1); Ghost higher (e.g. 10).
///   * Drag the two Volume components into the slots below.
/// </summary>
public class AtmosphereController : MonoBehaviour
{
    private enum Phase { Day, Night }

    [Header("Scene Volumes (assign Global Volumes from the scene)")]
    [SerializeField] private Volume nightVolume;
    [SerializeField] private Volume ghostVolume;

    [Header("Fade Durations (seconds)")]
    [Tooltip("How long the night fade in/out takes.")]
    [SerializeField] private float dayNightFadeDuration = 1.5f;
    [Tooltip("How long the ghost overlay fades in/out.")]
    [SerializeField] private float ghostFadeDuration = 1f;

    [Header("Debug")]
    [Tooltip("Force the ghost overlay on regardless of role or phase, for demos/previewing.")]
    [SerializeField] private bool debugForceGhost = false;

    private Phase currentPhase = Phase.Day;
    private bool isHaunted;
    private bool prevDebugForceGhost;

    private Coroutine nightFade;
    private Coroutine ghostFade;

    private void Start()
    {
        // Night is the default mood from the very start (e.g. during role selection,
        // before the phase loop begins). The Haunted -> ghost swap only kicks in once
        // the actual night phase starts.
        currentPhase = Phase.Night;
        prevDebugForceGhost = debugForceGhost;

        bool ghostAtStart = debugForceGhost;
        SetWeight(ghostVolume, ghostAtStart ? 1f : 0f);
        SetWeight(nightVolume, ghostAtStart ? 0f : 1f);
    }

    private void Update()
    {
        // Live toggle for previewing/demoing the ghost overlay.
        if (debugForceGhost != prevDebugForceGhost)
        {
            prevDebugForceGhost = debugForceGhost;
            ApplyAtmosphere();
        }
    }

    private void OnEnable()
    {
        GameEvents.OnNightStarted += HandleNightStarted;
        GameEvents.OnTransitionStarted += HandleNightStarted;
        GameEvents.OnDayStarted += HandleDayStarted;
        GameEvents.OnArcadeMapLoaded += HandleArcadeMapLoaded;
    }

    private void OnDisable()
    {
        GameEvents.OnNightStarted -= HandleNightStarted;
        GameEvents.OnTransitionStarted -= HandleNightStarted;
        GameEvents.OnDayStarted -= HandleDayStarted;
        GameEvents.OnArcadeMapLoaded -= HandleArcadeMapLoaded;
    }

    private void HandleNightStarted(int round) => HandleNightStarted();

    private void HandleNightStarted()
    {
        currentPhase = Phase.Night;
        ApplyAtmosphere();
    }

    private void HandleDayStarted()
    {
        currentPhase = Phase.Day;
        ApplyAtmosphere();
    }

    private void HandleArcadeMapLoaded()
    {
        // Roles are assigned by other OnArcadeMapLoaded subscribers in the same frame;
        // defer one frame so the check is independent of subscription order.
        StartCoroutine(DetermineRole());
    }

    private IEnumerator DetermineRole()
    {
        yield return null;

        PlayerController player = FindFirstObjectByType<PlayerController>();
        isHaunted = player != null && player.Role == CharacterRole.Haunted;
        // Don't re-apply here: role selection stays plain night for everyone.
        // The Haunted -> ghost swap happens on the first NightStarted.
    }

    /// <summary>
    /// Computes the target weights from the current phase, role, and debug flag,
    /// then fades each volume toward its target.
    /// </summary>
    private void ApplyAtmosphere()
    {
        bool ghostActive = debugForceGhost || (currentPhase == Phase.Night && isHaunted);
        // Ghost replaces the night look for the Haunted, so night is suppressed when ghost is active.
        bool nightActive = currentPhase == Phase.Night && !ghostActive;

        Fade(ref nightFade, nightVolume, nightActive ? 1f : 0f, dayNightFadeDuration);
        Fade(ref ghostFade, ghostVolume, ghostActive ? 1f : 0f, ghostFadeDuration);
    }

    private void Fade(ref Coroutine handle, Volume volume, float targetWeight, float duration)
    {
        if (volume == null)
        {
            Debug.LogWarning("[AtmosphereController] A Volume slot is not assigned.");
            return;
        }
        if (handle != null) StopCoroutine(handle);
        handle = StartCoroutine(FadeWeight(volume, targetWeight, duration));
    }

    private IEnumerator FadeWeight(Volume volume, float targetWeight, float duration)
    {
        float startWeight = volume.weight;

        if (duration <= 0f)
        {
            volume.weight = targetWeight;
            yield break;
        }

        float elapsed = 0f;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.SmoothStep(0f, 1f, elapsed / duration);
            volume.weight = Mathf.Lerp(startWeight, targetWeight, t);
            yield return null;
        }
        volume.weight = targetWeight;
    }

    private static void SetWeight(Volume volume, float weight)
    {
        if (volume != null) volume.weight = weight;
    }
}
