using Common.Data;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
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
                    var str = ReadFirstLinFile(file);
                    t.Init(str);
                }
                catch (Exception ex)
                {
                    t.Init(ex.Message);
                }

                return t;
            }).ConfigureAwait(false);
        }


        public string ReadFirstLinFile(string file)
        {
            var enc = GetEncoding(file);
            return File.ReadLines(file, enc).ElementAt(0);
        }


        public IEnumerable<string> ReadFile(string file)
        {
            var enc = GetEncoding(file);
            return File.ReadAllLines(file, enc);
        }

        private Encoding GetEncoding(string path)
        {
            try
            {
                using (FileStream fs = File.OpenRead(path))
                {
                    Ude.CharsetDetector cdet = new Ude.CharsetDetector();
                    cdet.Feed(fs);
                    cdet.DataEnd();
                    if (cdet.Charset != null)
                    {
                        if (cdet.Charset == "ASCII")
                        {
                            return Encoding.Default;
                        }
                        else if(cdet.Charset == "UTF-8")
                        {
                            return Encoding.UTF8;
                        }
                        else
                        {
                            return Encoding.Default;
                        }
                    }
                    else
                    {
                        return Encoding.Default;
                    }
                }
            }
            catch
            {
                return Encoding.Default;
            }
        }
    }
}