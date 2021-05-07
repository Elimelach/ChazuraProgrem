namespace ChazuraProgram.Models
{
    public static class IntExt
    {
        public static int ToInt(this string s)
        {
            int.TryParse(s, out int id);
            return id;
        }
    }
}
