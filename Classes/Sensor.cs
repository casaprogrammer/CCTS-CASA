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

        private string incomingData { get; set; }
        private bool pause { get; set; }
        private bool decrementing { get; set; }
        private TrackingList bnlist { get; set; }

        public Sensor(string incomingData, bool pause, bool decrementing, TrackingList bnlist)
        {
            this.incomingData = incomingData;
            this.pause = pause;
            this.decrementing = decrementing;
            this.bnlist = bnlist;
        }

        public void SensorActions()
        {
            SensorIndicator();
            TipperOne();
            TipperTwo();
            DumpTruck();
            StockPile();
            MainCane();
            CaneKnives();
            ShreddedCane();
        }

        private void SensorIndicator()
        {
            for(int i = 0; i < bnlist.sensorIndicators.Count; i++)
            {
                ctcc.ChangeColorTextBox(bnlist.sensorIndicators[i].Item1, Color.CornflowerBlue);


                if (incomingData.ToLower() == "side cane object")
                {
                    if (bnlist.sensorIndicators[i].Item2 == "Side Cane")
                    {
                        ctcc.ChangeColorTextBox(bnlist.sensorIndicators[i].Item1, Color.Orange);
                    }
                }

                if (incomingData.ToLower() == "main cane object")
                {
                    if (bnlist.sensorIndicators[i].Item2 == "Main Cane")
                    {
                        ctcc.ChangeColorTextBox(bnlist.sensorIndicators[i].Item1, Color.Orange);
                    }
                }

                if (incomingData.ToLower() == "cane knives object")
                {
                    if (bnlist.sensorIndicators[i].Item2 == "Cane Knives")
                    {
                        ctcc.ChangeColorTextBox(bnlist.sensorIndicators[i].Item1, Color.Orange);
                    }
                }

                if (incomingData.ToLower() == "shredded cane object")
                {
                    if (bnlist.sensorIndicators[i].Item2 == "Shredder")
                    {
                        ctcc.ChangeColorTextBox(bnlist.sensorIndicators[i].Item1, Color.Orange);
                    }
                }
            }

        }

        private void TipperOne()
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

        private void TipperTwo()
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

        private void DumpTruck()
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

        private void StockPile()
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

        private void MainCane()
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

            for (int i = 0; i <= bnlist.mainCaneBatchNumbers.Count -1; i++)
            {
                for (int y = 0; y < bnlist.lTbox.Count; y++)
                {
                    if (ctcc.GetTextboxValue(bnlist.lTbox[y].Item1) == "" && bnlist.lTbox[y].Item3 == "MainCane")
                    {
                        ctcc.ChangeText(bnlist.lTbox[y].Item1, bnlist.mainCaneBatchNumbers[i]);
                        ctcc.ChangeText(bnlist.lTbox[y].Item2, "0");
                        ctcc.ChangeColorTextBox(bnlist.lTbox[y].Item2, Color.Maroon);
                        ctcc.ChangeForeColorTextBox(bnlist.lTbox[y].Item2, Color.White);

                        bnlist.mainCane.Add(new Tuple<RichTextBox, RichTextBox>(bnlist.lTbox[y].Item1, bnlist.lTbox[y].Item2));


                        break;

                    }
                }
                bnlist.mainCaneBatchNumbers.RemoveAt(i);
            }

            /*
             * Loop to remove batch numbers from list after 
             * filling empty boxes
             */

           /* for (int i = bnlist.mainCaneBatchNumbers.Count - 1; i >= 0; i--)
            {
                bnlist.mainCaneBatchNumbers.RemoveAt(i);
            }*/

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

        private void CaneKnives()
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

            for (int i = 0; i <= bnlist.caneKnivesBatchNumbers.Count - 1; i++)
            {
                for (int y = 0; y < bnlist.lTbox.Count; y++)
                {
                    if (ctcc.GetTextboxValue(bnlist.lTbox[y].Item1) == "" && bnlist.lTbox[y].Item3 == "CaneKnives")
                    {
                        ctcc.ChangeText(bnlist.lTbox[y].Item1, bnlist.caneKnivesBatchNumbers[i]);
                        ctcc.ChangeText(bnlist.lTbox[y].Item2, "0");
                        ctcc.ChangeColorTextBox(bnlist.lTbox[y].Item2, Color.Maroon);
                        ctcc.ChangeForeColorTextBox(bnlist.lTbox[y].Item2, Color.White);

                        bnlist.caneKnives.Add(new Tuple<RichTextBox, RichTextBox>(bnlist.lTbox[y].Item1, bnlist.lTbox[y].Item2));


                        break;

                    }
                }
                bnlist.caneKnivesBatchNumbers.RemoveAt(i);
            }

            /*
             * Loop to remove batch numbers from list after 
             * filling empty boxes
             */

            /*for (int i = bnlist.caneKnivesBatchNumbers.Count - 1; i >= 0; i--)
            {
                bnlist.caneKnivesBatchNumbers.RemoveAt(i);
            }*/

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

        private void ShreddedCane()
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

            for (int i = 0; i <= bnlist.shredderBatchNumbers.Count - 1; i++)
            {
                for (int y = 0; y < bnlist.lTbox.Count; y++)
                {
                    if (ctcc.GetTextboxValue(bnlist.lTbox[y].Item1) == "" && bnlist.lTbox[y].Item3 == "Shredder")
                    {
                        ctcc.ChangeText(bnlist.lTbox[y].Item1, bnlist.shredderBatchNumbers[i]);
                        ctcc.ChangeText(bnlist.lTbox[y].Item2, "0");
                        ctcc.ChangeColorTextBox(bnlist.lTbox[y].Item2, Color.Maroon);
                        ctcc.ChangeForeColorTextBox(bnlist.lTbox[y].Item2, Color.White);

                        bnlist.shreddedCane.Add(new Tuple<RichTextBox, RichTextBox>(bnlist.lTbox[y].Item1, bnlist.lTbox[y].Item2));


                        break;

                    }
                }
                bnlist.shredderBatchNumbers.RemoveAt(i);
            }

            /*
             * Loop to remove batch numbers from list after 
             * filling empty boxes
             */

            /*for (int i = bnlist.shredderBatchNumbers.Count - 1; i >= 0; i--)
            {
                bnlist.shredderBatchNumbers.RemoveAt(i);
            }*/

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
                    for (int y = 0; y < bnlist.lTbox.Count; y++)
                    {
                        if (bnlist.lTbox[y].Item3 == "Nir")
                        {
                            ctcc.ChangeText(bnlist.lTbox[y].Item1, bnShred.ToString());
                            ctcc.ChangeText(bnlist.lTbox[y].Item2, "0");
                            ctcc.ChangeColorTextBox(bnlist.lTbox[y].Item2, Color.Maroon);
                            ctcc.ChangeForeColorTextBox(bnlist.lTbox[y].Item2, Color.White);

                            break;
                        }
                    }
                    bnlist.shreddedCane.RemoveAt(i);
                }
            }
        }
    }
}
