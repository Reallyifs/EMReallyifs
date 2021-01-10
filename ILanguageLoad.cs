using System.Collections.Generic;

namespace EMReallyifs
{
    interface ILanguageLoad
    {
        void LanguageAdd(List<(string, string[])> array);
    }
}