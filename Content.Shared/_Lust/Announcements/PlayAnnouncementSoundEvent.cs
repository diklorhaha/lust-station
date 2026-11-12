using Content.Shared._Sunrise.AnnouncementSpeaker.Events;
using Robust.Shared.Audio;
using Robust.Shared.Serialization;

namespace Content.Shared._Lust.Announcements;

/// <summary>
/// Отправляется конкретному игроку, чтобы клиент сам проиграл звуковой сигнал объявления
/// из ближайших динамиков с учётом клиентской настройки громкости объявлений.
/// </summary>
[Serializable, NetSerializable]
public sealed class PlayAnnouncementSoundEvent(
    List<MultiSpeakerTtsSource> speakers,
    ResolvedSoundSpecifier sound,
    AudioParams audioParams) : EntityEventArgs
{
    public List<MultiSpeakerTtsSource> Speakers { get; } = speakers;
    public ResolvedSoundSpecifier Sound { get; } = sound;
    public AudioParams AudioParams { get; } = audioParams;
}
