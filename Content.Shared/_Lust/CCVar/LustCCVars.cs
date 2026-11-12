using Robust.Shared;
using Robust.Shared.Configuration;

namespace Content.Shared._Lust.CCVar;

[CVarDefs]
public sealed class LustCCVars : CVars
{
    /// <summary>
    /// Клиентская громкость объявлений из сети динамиков (консоли связи и прочие станционные оповещения).
    /// Влияет и на звуковой сигнал объявления, и на его TTS. 1 — громкость по умолчанию, 0 — без звука.
    /// </summary>
    public static readonly CVarDef<float> AnnouncementVolume =
        CVarDef.Create("lust.announcement_volume", 1f, CVar.CLIENTONLY | CVar.ARCHIVE);
}
