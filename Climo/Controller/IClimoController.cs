using Climo.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Climo.Controller
{
    interface IClimoController
    {
        List<ClimoItem> GetList();
        void AddItem(ClimoItem item);
        void RemoveItem(int ID);
    }
}
