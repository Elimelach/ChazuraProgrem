using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;

namespace ChazuraProgram.EOReadFiles
{
    public static class GetFile
    {
        public static string Read(string path)
        {
            bool exist = File.Exists(path);
            if (!exist)
            {
                throw new FileNotFoundException("File does not exists!");
            }
            using StreamReader reader = new StreamReader(new FileStream(path, FileMode.Open, FileAccess.Read));
            string text = reader.ReadToEnd();
            reader.Close();
            return text;

        }
    }
}
