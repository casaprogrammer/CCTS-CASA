using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Cane_Tracking.Classes
{
    class Sensor
    {
        CrossThreadingCheck ctcc = new CrossThreadingCheck();
        CountInterval ci = new CountInterval();

        public void SensorIndicator(string incomingData,
                                    RichTextBox rtSideCaneCheck, RichTextBox rtMainCaneCheck, RichTextBox rtCaneKnivesCheck, RichTextBox rtShredderCheck)
        {
            ctcc.ChangeColorTextBox(rtSideCaneCheck, Color.CornflowerBlue);
            ctcc.ChangeColorTextBox(rtMainCaneCheck, Color.CornflowerBlue);
            ctcc.ChangeColorTextBox(rtCaneKnivesCheck, Color.CornflowerBlue);
            ctcc.ChangeColorTextBox(rtShredderCheck, Color.CornflowerBlue);

            if (incomingData.ToLower() == "side cane object")
            {
                ctcc.ChangeColorTextBox(rtSideCaneCheck, Color.Orange);
            }

            if (incomingData.ToLower() == "main cane object")
            {
                ctcc.ChangeColorTextBox(rtMainCaneCheck, Color.Orange);
            }

            if (incomingData.ToLower() == "cane knives object")
            {
                ctcc.ChangeColorTextBox(rtCaneKnivesCheck, Color.Orange);
            }

            if (incomingData.ToLower() == "shredded cane object")
            {
                ctcc.ChangeColorTextBox(rtShredderCheck, Color.Orange);
            }
        }

        public void TipperOne(string incomingData, bool pause, bool decrementing, BatchNumberList bnlist)
        {
            int count;
            int tipperOneMaxCount = ci.TipperOneMaxCount;

            for (int i = 0; i < bnlist.tipperOne.Count; i++)
            {
                if (incomingData.ToLower() == "side cane object")
                {
                    count = int.Parse(ctcc.GetTextboxValue(bnlist.tipperOne[i].Item2));

                    if (!pause)
                    {
                        if (decrementing)
                        {
                            count--;
                        }
                        else
                        {
                            count++;
                        }
                    }

                    ctcc.ChangeText(bnlist.tipperOne[i].Item2, count.ToString());

                    if (count > tipperOneMaxCount)
                    {
                        bnlist.dumpCanesHistory.Remove(new Tuple<RichTextBox, RichTextBox, string>(bnlist.tipperOne[i].Item1, bnlist.tipperOne[i].Item2, "Tipper One"));
                        bnlist.mainCaneBatchNumbers.Add(ctcc.GetTextboxValue(bnlist.tipperOne[i].Item1));

                        ctcc.ChangeText(bnlist.tipperOne[i].Item1, "");
                        ctcc.ChangeText(bnlist.tipperOne[i].Item2, "");
                        ctcc.ChangeColorTextBox(bnlist.tipperOne[i].Item2, Color.CornflowerBlue);

                    }
                }
            }

            for (int i = bnlist.tipperOne.Count - 1; i >= 0; i--)
            {
                if (ctcc.GetTextboxValue(bnlist.tipperOne[i].Item1) == "")
                {
                    bnlist.tipperOne.RemoveAt(i);
                }
            }
        }

        public void TipperTwo(string incomingData, bool pause, bool decrementing, BatchNumberList bnlist)
        {
            int count;
            int tipperTwoMaxCount = ci.TipperTwoMaxCount;

            for (int i = 0; i < bnlist.tipperTwo.Count; i++)
            {
                if (incomingData.ToLower() == "side cane object")
                {
                    count = int.Parse(ctcc.GetTextboxValue(bnlist.tipperTwo[i].Item2));

                    if (!pause)
                    {
                        if (decrementing)
                        {
                            count--;
                        }
                        else
                        {
                            count++;
                        }
                    }

                    ctcc.ChangeText(bnlist.tipperTwo[i].Item2, count.ToString());

                    if (count > tipperTwoMaxCount)
                    {
                        bnlist.dumpCanesHistory.Remove(new Tuple<RichTextBox, RichTextBox, string>(bnlist.tipperTwo[i].Item1, bnlist.tipperTwo[i].Item2, "Tipper Two"));
                        bnlist.mainCaneBatchNumbers.Add(ctcc.GetTextboxValue(bnlist.tipperTwo[i].Item1));

                        ctcc.ChangeText(bnlist.tipperTwo[i].Item1, "");
                        ctcc.ChangeText(bnlist.tipperTwo[i].Item2, "");
                        ctcc.ChangeColorTextBox(bnlist.tipperTwo[i].Item2, Color.CornflowerBlue);
                    }
                }
            }

            for (int i = bnlist.tipperTwo.Count - 1; i >= 0; i--)
            {
                if (ctcc.GetTextboxValue(bnlist.tipperTwo[i].Item1) == "")
                {
                    bnlist.tipperTwo.RemoveAt(i);
                }
            }
        }

        public void DumpTruck(string incomingData, bool pause, bool decrementing, BatchNumberList bnlist)
        {
            int count;
            int dumpAndPileMaxCount = ci.DumpAndPileMaxCount;

            for (int i = 0; i < bnlist.dumpTruck.Count; i++)
            {
                if (incomingData.ToLower() == "side cane object")
                {
                    count = int.Parse(ctcc.GetTextboxValue(bnlist.dumpTruck[i].Item2));

                    if (!pause)
                    {
                        if (decrementing)
                        {
                            count--;
                        }
                        else
                        {
                            count++;
                        }
                    }

                    ctcc.ChangeText(bnlist.dumpTruck[i].Item2, count.ToString());

                    if (count > dumpAndPileMaxCount)
                    {
                        bnlist.dumpCanesHistory.Remove(new Tuple<RichTextBox, RichTextBox, string>(bnlist.dumpTruck[i].Item1, bnlist.dumpTruck[i].Item2, "Dump Truck"));
                        bnlist.mainCaneBatchNumbers.Add(ctcc.GetTextboxValue(bnlist.dumpTruck[i].Item1));

                        ctcc.ChangeText(bnlist.dumpTruck[i].Item1, "");
                        ctcc.ChangeText(bnlist.dumpTruck[i].Item2, "");
                        ctcc.ChangeColorTextBox(bnlist.dumpTruck[i].Item2, Color.CornflowerBlue);
                    }
                }
            }

            for (int i = bnlist.dumpTruck.Count - 1; i >= 0; i--)
            {
                if (ctcc.GetTextboxValue(bnlist.dumpTruck[i].Item1) == "")
                {
                    bnlist.dumpTruck.RemoveAt(i);
                }
            }
        }

        public void StockPile(string incomingData, bool pause, bool decrementing, BatchNumberList bnlist)
        {
            int count;
            int dumpAndPileMaxCount = ci.DumpAndPileMaxCount;

            for (int i = 0; i < bnlist.stockPile.Count; i++)
            {
                if (incomingData.ToLower() == "side cane object")
                {
                    count = int.Parse(ctcc.GetTextboxValue(bnlist.stockPile[i].Item2));

                    if (!pause)
                    {
                        if (decrementing)
                        {
                            count--;
                        }
                        else
                        {
                            count++;
                        }
                    }

                    ctcc.ChangeText(bnlist.stockPile[i].Item2, count.ToString());

                    if (count > dumpAndPileMaxCount)
                    {
                        bnlist.dumpCanesHistory.Remove(new Tuple<RichTextBox, RichTextBox, string>(bnlist.stockPile[i].Item1, bnlist.stockPile[i].Item2, "Stock Pile"));
                        bnlist.mainCaneBatchNumbers.Add(ctcc.GetTextboxValue(bnlist.stockPile[i].Item1));

                        ctcc.ChangeText(bnlist.stockPile[i].Item1, "");
                        ctcc.ChangeText(bnlist.stockPile[i].Item2, "");
                        ctcc.ChangeColorTextBox(bnlist.stockPile[i].Item2, Color.CornflowerBlue);
                    }
                }
            }

            for (int i = bnlist.stockPile.Count - 1; i >= 0; i--)
            {
                if (ctcc.GetTextboxValue(bnlist.stockPile[i].Item1) == "")
                {
                    bnlist.stockPile.RemoveAt(i);
                }
            }
        }

        public void MainCane(string incomingData, bool pause, BatchNumberList bnlist,
                             RichTextBox rtMainBn1, RichTextBox rtMainBn2, RichTextBox rtMainBn3, RichTextBox rtMainBn4,
                             RichTextBox rtMainBx1, RichTextBox rtMainBx2, RichTextBox rtMainBx3, RichTextBox rtMainBx4)
        {
            int count;
            int mainCaneMaxCount = ci.MainCaneMaxCount;

            /*
             * Looping to the batch numbers stored in list coming from side canes.
             * Loop from list to fill in empty boxes
             * Loop from list to prevent overlapping of 
             * batch numbers with the same countings.
             * 
             * Batch numbers are also being added to another list
             * responsible for tracking the countings
             */

            for (int i = 0; i < bnlist.mainCaneBatchNumbers.Count; i++)
            {
                if (ctcc.GetTextboxValue(rtMainBn1) == "")
                {
                    ctcc.ChangeText(rtMainBn1, bnlist.mainCaneBatchNumbers[i]);
                    ctcc.ChangeText(rtMainBx1, "0");
                    ctcc.ChangeColorTextBox(rtMainBx1, Color.Maroon);
                    ctcc.ChangeForeColorTextBox(rtMainBx1, Color.White);
                    bnlist.mainCane.Add(new Tuple<RichTextBox, RichTextBox>(rtMainBn1, rtMainBx1));
                }
                else if (ctcc.GetTextboxValue(rtMainBn1) != "" && ctcc.GetTextboxValue(rtMainBn2) == "")
                {
                    ctcc.ChangeText(rtMainBn2, bnlist.mainCaneBatchNumbers[i]);
                    ctcc.ChangeText(rtMainBx2, "0");
                    ctcc.ChangeColorTextBox(rtMainBx2, Color.Maroon);
                    ctcc.ChangeForeColorTextBox(rtMainBx2, Color.White);
                    bnlist.mainCane.Add(new Tuple<RichTextBox, RichTextBox>(rtMainBn2, rtMainBx2));
                }
                else if (ctcc.GetTextboxValue(rtMainBn1) != "" && ctcc.GetTextboxValue(rtMainBn2) != "" && ctcc.GetTextboxValue(rtMainBn3) == "")
                {
                    ctcc.ChangeText(rtMainBn3, bnlist.mainCaneBatchNumbers[i]);
                    ctcc.ChangeText(rtMainBx3, "0");
                    ctcc.ChangeColorTextBox(rtMainBx3, Color.Maroon);
                    ctcc.ChangeForeColorTextBox(rtMainBx3, Color.White);
                    bnlist.mainCane.Add(new Tuple<RichTextBox, RichTextBox>(rtMainBn3, rtMainBx3));
                }
                else if (ctcc.GetTextboxValue(rtMainBn1) != "" && ctcc.GetTextboxValue(rtMainBn2) != "" && ctcc.GetTextboxValue(rtMainBn3) != "" && ctcc.GetTextboxValue(rtMainBn4) == "")
                {
                    ctcc.ChangeText(rtMainBn4, bnlist.mainCaneBatchNumbers[i]);
                    ctcc.ChangeText(rtMainBx4, "0");
                    ctcc.ChangeColorTextBox(rtMainBx4, Color.Maroon);
                    ctcc.ChangeForeColorTextBox(rtMainBx4, Color.White);
                    bnlist.mainCane.Add(new Tuple<RichTextBox, RichTextBox>(rtMainBn4, rtMainBx4));
                }
            }

            /*
             * Loop to remove batch numbers from list after 
             * filling empty boxes
             */
            for (int i = bnlist.mainCaneBatchNumbers.Count - 1; i >= 0; i--)
            {
                bnlist.mainCaneBatchNumbers.RemoveAt(i);
            }

            /*
             * Looping on the stored batch numbers in list 
             * for counting
             */

            for (int i = 0; i < bnlist.mainCane.Count; i++)
            {
                if (incomingData.ToLower() == "main cane object")
                {
                    count = int.Parse(ctcc.GetTextboxValue(bnlist.mainCane[i].Item2));

                    if (!pause)
                    {
                        count++;
                    }

                    ctcc.ChangeText(bnlist.mainCane[i].Item2, count.ToString());

                    if (count > mainCaneMaxCount)
                    {
                        bnlist.caneKnivesBatchNumbers.Add(ctcc.GetTextboxValue(bnlist.mainCane[i].Item1));

                        ctcc.ChangeText(bnlist.mainCane[i].Item1, "");
                        ctcc.ChangeText(bnlist.mainCane[i].Item2, "");
                        ctcc.ChangeColorTextBox(bnlist.mainCane[i].Item2, Color.CornflowerBlue);
                    }
                }
            }

            /*
             * Removing batch numbers from list after counting is finished.
             */

            for (int i = bnlist.mainCane.Count - 1; i >= 0; i--)
            {
                if (ctcc.GetTextboxValue(bnlist.mainCane[i].Item1) == "")
                {
                    bnlist.mainCane.RemoveAt(i);
                }
            }
        }

        public void CaneKnives(string incomingData, bool pause, BatchNumberList bnlist,
                            RichTextBox rtKnivesBn1, RichTextBox rtKnivesBn2,
                            RichTextBox rtKnivesBx1, RichTextBox rtKnivesBx2)
        {
            int count;
            int knivesAndShredderMaxCount = ci.KnivesAndShredderMaxCount;

            /*
             * Looping to the batch numbers stored in list coming from side canes.
             * Loop from list to fill in empty boxes
             * Loop from list to prevent overlapping of 
             * batch numbers with the same countings.
             * 
             * Batch numbers are also being added to another list
             * responsible for tracking the countings
             */

            for (int i = 0; i < bnlist.caneKnivesBatchNumbers.Count; i++)
            {
                if (ctcc.GetTextboxValue(rtKnivesBn1) == "")
                {
                    ctcc.ChangeText(rtKnivesBn1, bnlist.caneKnivesBatchNumbers[i]);
                    ctcc.ChangeText(rtKnivesBx1, "0");
                    ctcc.ChangeColorTextBox(rtKnivesBx1, Color.Maroon);
                    ctcc.ChangeForeColorTextBox(rtKnivesBx1, Color.White);
                    bnlist.caneKnives.Add(new Tuple<RichTextBox, RichTextBox>(rtKnivesBn1, rtKnivesBx1));
                }
                else if (ctcc.GetTextboxValue(rtKnivesBn1) != "" && ctcc.GetTextboxValue(rtKnivesBn2) == "")
                {
                    ctcc.ChangeText(rtKnivesBn2, bnlist.caneKnivesBatchNumbers[i]);
                    ctcc.ChangeText(rtKnivesBx2, "0");
                    ctcc.ChangeColorTextBox(rtKnivesBx2, Color.Maroon);
                    ctcc.ChangeForeColorTextBox(rtKnivesBx2, Color.White);
                    bnlist.caneKnives.Add(new Tuple<RichTextBox, RichTextBox>(rtKnivesBn2, rtKnivesBx2));
                }
            }

            /*
             * Loop to remove batch numbers from list after 
             * filling empty boxes
             */

            for (int i = bnlist.caneKnivesBatchNumbers.Count - 1; i >= 0; i--)
            {
                bnlist.caneKnivesBatchNumbers.RemoveAt(i);
            }

            /*
             * Looping on the stored batch numbers in list 
             * for counting
             */

            for (int i = 0; i < bnlist.caneKnives.Count; i++)
            {
                if (incomingData.ToLower() == "cane knives object")
                {
                    count = int.Parse(ctcc.GetTextboxValue(bnlist.caneKnives[i].Item2));

                    if (!pause)
                    {
                        count++;
                    }

                    ctcc.ChangeText(bnlist.caneKnives[i].Item2, count.ToString());

                    if (count > knivesAndShredderMaxCount)
                    {
                        bnlist.shredderBatchNumbers.Add(ctcc.GetTextboxValue(bnlist.caneKnives[i].Item1));

                        ctcc.ChangeText(bnlist.caneKnives[i].Item1, "");
                        ctcc.ChangeText(bnlist.caneKnives[i].Item2, "");
                        ctcc.ChangeColorTextBox(bnlist.caneKnives[i].Item2, Color.CornflowerBlue);
                    }
                }
            }

            /*
             * Removing batch numbers from list after counting is finished.
             */

            for (int i = bnlist.caneKnives.Count - 1; i >= 0; i--)
            {
                if (ctcc.GetTextboxValue(bnlist.caneKnives[i].Item1) == "")
                {
                    bnlist.caneKnives.RemoveAt(i);
                }
            }
        }

        public void ShreddedCane(string incomingData, bool pause, BatchNumberList bnlist,
                            RichTextBox rtShredBn1, RichTextBox rtShredBn2,
                            RichTextBox rtShredBx1, RichTextBox rtShredBx2,
                            RichTextBox rtNirWashing, RichTextBox rtWashingCount)
        {
            int count;
            int knivesAndShredderMaxCount = ci.KnivesAndShredderMaxCount;
            int bnShred = 0;

            /*
             * Looping to the batch numbers stored in list coming from side canes.
             * Loop from list to fill in empty boxes
             * Loop from list to prevent overlapping of 
             * batch numbers with the same countings.
             * 
             * Batch numbers are also being added to another list
             * responsible for tracking the countings
             */

            for (int i = 0; i < bnlist.shredderBatchNumbers.Count; i++)
            {
                if (ctcc.GetTextboxValue(rtShredBn1) == "")
                {
                    ctcc.ChangeText(rtShredBn1, bnlist.shredderBatchNumbers[i]);
                    ctcc.ChangeText(rtShredBx1, "0");
                    ctcc.ChangeColorTextBox(rtShredBx1, Color.Maroon);
                    ctcc.ChangeForeColorTextBox(rtShredBx1, Color.White);
                    bnlist.shreddedCane.Add(new Tuple<RichTextBox, RichTextBox>(rtShredBn1, rtShredBx1));
                }
                else if (ctcc.GetTextboxValue(rtShredBn1) != "" && ctcc.GetTextboxValue(rtShredBn2) == "")
                {
                    ctcc.ChangeText(rtShredBn2, bnlist.shredderBatchNumbers[i]);
                    ctcc.ChangeText(rtShredBx2, "0");
                    ctcc.ChangeColorTextBox(rtShredBx2, Color.Maroon);
                    ctcc.ChangeForeColorTextBox(rtShredBx2, Color.White);
                    bnlist.shreddedCane.Add(new Tuple<RichTextBox, RichTextBox>(rtShredBn2, rtShredBx2));
                }
            }

            /*
             * Loop to remove batch numbers from list after 
             * filling empty boxes
             */

            for (int i = bnlist.shredderBatchNumbers.Count - 1; i >= 0; i--)
            {
                bnlist.shredderBatchNumbers.RemoveAt(i);
            }

            /*
            * Looping on the stored batch numbers in list 
            * for counting
            */

            for (int i = 0; i < bnlist.shreddedCane.Count; i++)
            {
                if (incomingData.ToLower() == "shredded cane object")
                {
                    count = int.Parse(ctcc.GetTextboxValue(bnlist.shreddedCane[i].Item2));

                    if (!pause)
                    {
                        count++;
                    }

                    ctcc.ChangeText(bnlist.shreddedCane[i].Item2, count.ToString());

                    if (count > knivesAndShredderMaxCount)
                    {
                        bnShred = int.Parse(ctcc.GetTextboxValue(bnlist.shreddedCane[i].Item1));
                        ctcc.ChangeText(bnlist.shreddedCane[i].Item1, "");
                        ctcc.ChangeText(bnlist.shreddedCane[i].Item2, "");
                        ctcc.ChangeColorTextBox(bnlist.shreddedCane[i].Item2, Color.CornflowerBlue);
                    }
                }
            }

            /*
             * Loop to transfer finished batch number count 
             * to rtNirWashing. 
             * No need to store in list since the last 
             * part only accepts one batch number per 
             * cycle
             */

            for (int i = bnlist.shreddedCane.Count - 1; i >= 0; i--)
            {
                if (ctcc.GetTextboxValue(bnlist.shreddedCane[i].Item1) == "")
                {
                    ctcc.ChangeText(rtNirWashing, bnShred.ToString());
                    ctcc.ChangeText(rtWashingCount, "0");
                    ctcc.ChangeColorTextBox(rtWashingCount, Color.Maroon);
                    ctcc.ChangeForeColorTextBox(rtWashingCount, Color.White);

                    bnlist.shreddedCane.RemoveAt(i);
                }
            }
        }
    }
}
