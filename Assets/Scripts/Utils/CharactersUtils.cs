using System.Collections.Generic;
using System.Linq;

public static class CharactersUtils
{
    public static string GetFormattedNames<T>(List<T> characters, bool onlyDistinctNames = true) where T : Character
    {
        List<string> names = characters.Select(x => x.Name).ToList();

        if (onlyDistinctNames)
        {
            names = names.Distinct().ToList();
        }

        string res = names[0];
        int lastElementIndex = names.Count - 1;

        for (int i = 1; i <= lastElementIndex; i++)
        {
            res += i < lastElementIndex ? ", " : " and ";
            res += names[i];
        }

        return res;
    }
}
