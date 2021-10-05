using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Cane_Tracking.Classes
{
    class BatchNumberList
    {
        public List<Tuple<RichTextBox, RichTextBox, string>> dumpCanesHistory = new List<Tuple<RichTextBox, RichTextBox, string>>();
        public List<Tuple<RichTextBox, RichTextBox>> tipperOne = new List<Tuple<RichTextBox, RichTextBox>>();
        public List<Tuple<RichTextBox, RichTextBox>> tipperTwo = new List<Tuple<RichTextBox, RichTextBox>>();
        public List<Tuple<RichTextBox, RichTextBox>> dumpTruck = new List<Tuple<RichTextBox, RichTextBox>>();
        public List<Tuple<RichTextBox, RichTextBox>> stockPile = new List<Tuple<RichTextBox, RichTextBox>>();
        public List<Tuple<RichTextBox, RichTextBox>> mainCane = new List<Tuple<RichTextBox, RichTextBox>>();
        public List<Tuple<RichTextBox, RichTextBox>> caneKnives = new List<Tuple<RichTextBox, RichTextBox>>();
        public List<Tuple<RichTextBox, RichTextBox>> shreddedCane = new List<Tuple<RichTextBox, RichTextBox>>();
        
        public List<string> mainCaneBatchNumbers = new List<string>();
        public List<string> caneKnivesBatchNumbers = new List<string>();
        public List<string> shredderBatchNumbers = new List<string>();

    }
}
