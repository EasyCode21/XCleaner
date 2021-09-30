using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XCleaner
{
    public class RecursiveDel
    {
        public static void EnumerateFolder_DeliteRecur(string path)
        {
            try
            {
                foreach (string Folder in Directory.GetDirectories(path))
                {
                    EnumerateFolder_DeliteRecur(Folder);
                    try
                    {
                        Directory.Delete(Folder);
                    }
                    catch { }

                }
                foreach (string files in Directory.GetFiles(path))
                {
                    try
                    {
                        int y;
                        y = files.Length;
                        Cleaning.File_size = y + Cleaning.File_size;
                        File.Delete(files);
                        Console.WriteLine(files);
                        Cleaning.ALL_files = Cleaning.ALL_files + 1;
                    }
                    catch { }

                }
            }
            catch
            { return;}
        }

        public static void getFiles(string path)
        {
            foreach (string Folder in Directory.GetDirectories(path))
            {
                foreach (string files in Directory.GetFiles(Folder))
                {
                    Console.WriteLine(files);
                }
                getFiles(Folder);
            }
        }

        public static List<string> lst1 = new List<string> { "", ""};
    }
}
