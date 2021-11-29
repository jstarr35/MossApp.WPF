using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;


namespace MossApp.Utilities.Extensions
{
    public static class CollectionExtensions
    {
        public static bool IsEmptyArray(this object[] array)
        {
            bool rtn = !(array.Length > 0);
            return rtn;
        }

        public static Dictionary<string, List<string>> ParseTokens(this StringCollection collection)
        {
            Dictionary<string, List<string>> rtn = new();
            foreach (string language in collection)
            {
                string[] tokens = language?.Split(',');
                if (tokens?.Length > 0)
                {
                    rtn.Add(tokens[0], new List<string>());
                }

                for (int index = 1; index < tokens?.Length; index++)
                {
                    rtn[tokens[0]].Add(tokens[index]);
                }


            }
            return rtn;

        }

        public static string ToExtensionString(this List<string> extensions)
        {
            StringBuilder sb = new();
            extensions.ForEach(e => sb.Append(e).Append(", "));
            char[] trims = new char[2] { ',', ' ' };
            return sb.ToString().Trim(trims);
        }
    }
}
