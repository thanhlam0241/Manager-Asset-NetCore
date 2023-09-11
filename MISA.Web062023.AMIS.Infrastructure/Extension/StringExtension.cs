namespace MISA.Web062023.AMIS.Infrastructure
{

    /// <summary>
    /// The string extension.
    /// </summary>
    /// Created by: NTLam (14/8/2023)
    public static class StringExtension
    {

        /// <summary>
        /// The pascal case to underscore case.
        /// </summary>
        /// <param name="str">The input string</param>
        /// <returns>The result.</returns>
        /// Created by: NTLam (14/8/2023)
        public static string PascalCaseToUnderscoreCase(this string str)
        {
            return string.Concat(str.Select((x, i) => i > 0 && char.IsUpper(x) ? "_" + x.ToString() : x.ToString())).ToLower();
        }

        /// <summary>
        /// The underscore case to pascal case.
        /// </summary>
        /// <param name="str">The str.</param>
        /// <returns>The result.</returns>
        /// Created by: NTLam (14/8/2023)
        public static string UnderscoreCaseToPascalCase(this string str)
        {
            return string.Concat(str.Split('_').Select(x => x.First().ToString().ToUpper() + x.Substring(1)));
        }
    }
}
