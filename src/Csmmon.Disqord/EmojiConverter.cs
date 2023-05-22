using Csmmon.Discord;
using Disqord;

namespace Csmmon.Disqord
{
    public static class EmojiConverter
    {
        public static bool TryGetEmoji(string name, out LocalEmoji? emoji)
        {
            emoji = null;
            if (EmojiNameContainer.TryGetCode(name, out var value))
            {
                emoji = LocalEmoji.Unicode(value!);
                return true;
            }
            return false;
        }

        public static LocalEmoji GetEmoji(string name)
        {
            if (TryGetEmoji(name, out var emoji))
                return emoji!;
            throw new KeyNotFoundException($"Could not find emoji by name {name}");
        }
    }
}
