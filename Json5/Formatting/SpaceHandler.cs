namespace Json5
{
    public static class SpaceHandler
    {
        /// <summary>
        /// Add space string to the start of original string, if needed
        /// </summary>
        /// <example>if original string is "worm" and space is "  " then this returns "  worm"</example>
        /// <param name="originalString">Original string</param>
        /// <param name="space">Space string, can contain other chars than whitespace</param>
        /// <returns>Original or combined string</returns>
        public static string AddSpace(string originalString, string space)
        {
            if (string.IsNullOrEmpty(space))
            {
                return originalString;
            }

            return space + originalString;
        }
    }
}