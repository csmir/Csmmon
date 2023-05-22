using Csmmon.Discord;
using NetCord;

namespace Csmmon.Disqord
{
    public static class EmojiConverter
    {
        public static bool TryGetEmoji(string name, out EmojiProperties? emoji)
        {
            emoji = null;
            if (EmojiNameContainer.TryGetCode(name, out var value))
            {
                emoji = new EmojiProperties(value!);
                return true;
            }
            return false;
        }

        public static EmojiProperties GetEmoji(string name)
        {
            if (TryGetEmoji(name, out var emoji))
                return emoji!;
            throw new KeyNotFoundException($"Could not find emoji by name {name}");
        }
    }
}
