using Microsoft.Win32;
using Rosreestr.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Rosreestr.Service
{
    public static class ServiceFile
    {
        public static string GetFile()
        {
            OpenFileDialog fd = new OpenFileDialog()
            {
                Multiselect = false
            };

            if (fd.ShowDialog() == true)
            {
                return fd.FileName;
            }

            return null;
        }
        public static IEnumerable<string> ReadFile(string file)
        {
            return File.ReadAllLines(file);
        }
        public async static Task<TypeData> GetTypeData(string file)
        {
            if (string.IsNullOrEmpty(file)) return null;
            if (File.Exists(file) == false) return null;

            return await Task.Run(() =>
            {
                try
                {
                    var str = File.ReadLines(file).ElementAt(0);
                    return new TypeData(str);
                }
                catch (Exception ex)
                {
                    return new TypeData(ex.Message);
                }
            }).ConfigureAwait(false);
        }

        public static string GetOnlyFileName(string file)
        {
            return Path.GetFileNameWithoutExtension(file);
        }
    }
}