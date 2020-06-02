using Common.Data;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Common.Service
{
    public class ServiceFile<T> where T : TypeData, new()
    {
        public string GetFile()
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

        public async Task<T> GetTypeData(string file)
        {
            if (string.IsNullOrEmpty(file)) return null;
            if (File.Exists(file) == false) return null;

            return await Task.Run(() =>
            {
                var t = new T();

                try
                {
                    var str = File.ReadLines(file).ElementAt(0);
                    t.Init(str);
                }
                catch (Exception ex)
                {
                    t.Init(ex.Message);
                }

                return t;
            }).ConfigureAwait(false);
        }

        public IEnumerable<string> ReadFile(string file)
        {
            return File.ReadAllLines(file);
        }
    }
}