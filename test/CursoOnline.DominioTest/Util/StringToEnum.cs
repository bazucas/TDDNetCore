using System;

namespace CursoOnline.DominioTest.Util
{
    public static class StringToEnum<T>
    {
        public static T ParseEnum(string value)
        {
            return (T)Enum.Parse(typeof(T), value, true);
        }
    }
}
