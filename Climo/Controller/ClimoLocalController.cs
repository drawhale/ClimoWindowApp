using Climo.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Climo.Controller
{
    class ClimoLocalController : IClimoController
    {
        private string _DIR_PATH { get; set; }

        public ClimoLocalController()
        {
            _DIR_PATH = @"items";
        }

        public List<ClimoItem> GetList()
        {
            List<ClimoItem> climos = new List<ClimoItem>();

            try
            {
                if (System.IO.Directory.Exists(_DIR_PATH))
                {
                    System.IO.DirectoryInfo di = new System.IO.DirectoryInfo(_DIR_PATH);
                    foreach(var item in di.GetFiles())
                    {
                        int fileIndex = 0;
                        bool isNumber = int.TryParse(item.Name.Split('.')[0], out fileIndex);
                        if(isNumber && fileIndex >= 1 && fileIndex <= 5)
                        {
                            string text = System.IO.File.ReadAllText(String.Format(@"{0}\{1}", _DIR_PATH, item.Name));
                            climos.Add(new ClimoItem(fileIndex - 1, fileIndex - 1, text, item.CreationTime));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return climos;
        }

        public void AddItem(ClimoItem item)
        {
            try
            {
                if (!System.IO.Directory.Exists(_DIR_PATH))
                {
                    System.IO.Directory.CreateDirectory(_DIR_PATH);
                }

                string filePath = string.Format(@"{0}\{1}.txt", _DIR_PATH, item.Index + 1);
                System.IO.File.WriteAllText(filePath, item.Text, Encoding.UTF8);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void RemoveItem(int ID)
        {
            try
            {
                int fileIndex = ID + 1;
                string filePath = string.Format(@"{0}\{1}.txt", _DIR_PATH, fileIndex);
                if(System.IO.File.Exists(filePath))
                {
                    System.IO.File.Delete(filePath);
                    for(int i = fileIndex; i <= 5; i++)
                    {
                        string srcPath = string.Format(@"{0}\{1}.txt", _DIR_PATH, i + 1);
                        if (System.IO.File.Exists(srcPath))
                        {
                            string destPath = string.Format(@"{0}\{1}.txt", _DIR_PATH, i);
                            if (!System.IO.File.Exists(destPath))
                            {
                                System.IO.File.Move(srcPath, destPath);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
