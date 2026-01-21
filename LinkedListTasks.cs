using System;
using System.Collections.Generic;

namespace CollectionsLab
{
    public static class LinkedListTasks
    {
        public static bool HasEqualNeighbors<T>(LinkedList<T> list)
        {
            if (list.Count == 0) return false;
            LinkedListNode<T> node = list.First;
            while (node != null)
            {
                LinkedListNode<T> next = node.Next ?? list.First;
                if (EqualityComparer<T>.Default.Equals(node.Value, next.Value))
                    return true;
                node = node.Next;
            }
            return false;
        }
    }
}