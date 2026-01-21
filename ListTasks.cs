using System;
using System.Collections.Generic;

namespace CollectionsLab
{
    public static class ListTasks
    {
        public static List<T> RemoveAfterEachE<T>(List<T> list, T E)
        {
            List<T> result = new List<T>();
            for (int i = 0; i < list.Count; i++)
            {
                result.Add(list[i]);
                if (EqualityComparer<T>.Default.Equals(list[i], E))
                {
                    if (i + 1 < list.Count && !EqualityComparer<T>.Default.Equals(list[i + 1], E))
                    {
                        i++;
                    }
                }
            }
            return result;
        }
    }
}