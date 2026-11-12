using Content.Shared._Lust.Announcements;
using Content.Shared._Lust.CCVar;
using Robust.Client.Audio;
using Robust.Shared.Audio.Systems;
using Robust.Shared.Configuration;
using Robust.Shared.Player;

namespace Content.Client._Lust.Announcements;

/// <summary>
/// Проигрывает звуковой сигнал объявлений из динамиков с учётом клиентской громкости объявлений.
/// </summary>
public sealed partial class AnnouncementSoundSystem : EntitySystem
{
    [Dependency] private IConfigurationManager _cfg = default!;
    [Dependency] private AudioSystem _audio = default!;
    [Dependency] private SharedTransformSystem _xform = default!;

    private float _volume;

    public override void Initialize()
    {
        base.Initialize();

        Subs.CVar(_cfg, LustCCVars.AnnouncementVolume, v => _volume = v, true);
        SubscribeNetworkEvent<PlayAnnouncementSoundEvent>(OnPlayAnnouncementSound);
    }

    private void OnPlayAnnouncementSound(PlayAnnouncementSoundEvent ev)
    {
        if (_volume <= 0f)
            return;

        foreach (var speaker in ev.Speakers)
        {
            var gain = speaker.VolumeModifier * _volume;
            if (gain <= 0f)
                continue;

            var coordinates = _xform.ToCoordinates(speaker.Coordinates);
            if (!coordinates.IsValid(EntityManager))
                continue;

            var audioParams = ev.AudioParams
                .WithMaxDistance(speaker.MaxDistance)
                .AddVolume(SharedAudioSystem.GainToVolume(gain));

            _audio.PlayStatic(ev.Sound, Filter.Local(), coordinates, false, audioParams);
        }
    }
}
