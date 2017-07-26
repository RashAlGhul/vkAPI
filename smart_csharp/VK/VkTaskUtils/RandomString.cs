using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WebdriverFramework.VK.VkTaskUtils
{
    internal static class RandomString
    {
        internal static string RandomName(int size, bool lowerCase)
        {
            StringBuilder builder = new StringBuilder();
            Random random = new Random();
            for (int i = 0; i < size; i++)
            {
                var ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
                builder.Append(ch);
            }
            if (lowerCase)
                return builder.ToString().ToLower();
            return builder.ToString();
        }
    }
}
