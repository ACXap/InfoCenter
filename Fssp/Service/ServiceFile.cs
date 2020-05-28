using Fssp.Data;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Fssp.Service
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

        public static IEnumerable<string> ReadFile(string file)
        {
            return File.ReadLines(file);
        }
    }
}