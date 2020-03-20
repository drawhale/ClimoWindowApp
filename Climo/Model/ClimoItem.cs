using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Climo.Model
{
    public class ClimoItem
    {
        public int ID { get; set; }
        public int Index { get; set; }
        public string Text { get; set; }
        public DateTime CreateDateTime { get; set; }

        public ClimoItem(int Index, string Text)
        {
            this.Index = Index;
            this.Text = Text;
            this.CreateDateTime = DateTime.Now;
        }

        public ClimoItem(int Index, string Text, DateTime CreateDateTime)
        {
            this.Index = Index;
            this.Text = Text;
            this.CreateDateTime = CreateDateTime;
        }

        public ClimoItem(int ID, int Index, string Text, DateTime CreateDateTime)
        {
            this.ID = ID;
            this.Index = Index;
            this.Text = Text;
            this.CreateDateTime = CreateDateTime;
        }
    }
}
