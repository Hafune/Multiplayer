using System.Linq;
using UnityEngine;

namespace Lib
{
    public static class MyDebug
    {
        public static void Log(params object[] logs) =>
            Debug.Log(string.Join(", ", logs.Select(v => v.ToString())));
    }
}