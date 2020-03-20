using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Climo.Model
{
    public class ClimoCollection : ObservableCollection<ClimoItem>
    {
        public int ItemIndex { get; set; }
        public int MaxCount { get; set; }
        public int ItemPasteIndex { get; set; }

        public ClimoCollection(int MaxCount)
        {
            this.ItemIndex = 0;
            this.MaxCount = MaxCount;
            this.ItemPasteIndex = 0;
        }

        public ClimoCollection(int MaxCount, List<ClimoItem> ClimoList) : base(ClimoList)
        {
            this.MaxCount = MaxCount;
            this.ItemIndex = ClimoList.Count;
            this.ItemPasteIndex = 0;
        }

        public ClimoItem AddItem(string Text)
        {
            if (this.ItemIndex > this.MaxCount - 1)
            {
                this.ItemIndex = 0;
            }

            ClimoItem newItem = new ClimoItem(this.ItemIndex, Text);

            if (this.Count < this.MaxCount)
            {
                this.Add(newItem);
            }
            else
            {
                this[this.ItemIndex] = newItem;
            }

            this.ItemIndex++;

            return newItem;
        }

        public ClimoItem AddItem(int Index, string Text)
        {
            if(Index > this.MaxCount -1)
            {
                Index = 0;
            }

            ClimoItem newItem = new ClimoItem(Index, Text);

            if (this.Count <= Index)
            {
                this.Add(newItem);
            }
            else
            {
                this[Index] = newItem;
            }

            this.ItemIndex = Index + 1;

            return newItem;
        }

        public new ClimoItem RemoveItem(int Index)
        {
            ClimoItem removedItem = this[Index];

            this.RemoveAt(Index);
            this.ItemIndex--;

            for (int i = 0; i < this.Count; i++)
            {
                this[i].Index = i;
            }

            return removedItem;
        }

        public void IncrementPasteIndex()
        {
            this.ItemPasteIndex++;
            if (this.ItemPasteIndex > this.Count - 1)
            {
                this.ItemPasteIndex = 0;
            }
        }
    }
}
