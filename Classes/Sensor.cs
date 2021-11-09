using System;
using System.Drawing;
using System.Windows.Forms;

namespace Cane_Tracking.Classes
{
    class Sensor
    {
        CrossThreadingCheck ctcc = new CrossThreadingCheck();
        ConfigValues ci = new ConfigValues();

        private string IncomingData { get; set; }
        private bool Pause { get; set; }
        private bool Decrementing { get; set; }
        private TrackingList BatchNumberList { get; set; }

        public Sensor(string incomingData, bool pause, bool decrementing, TrackingList bnlist)
        {
            this.IncomingData = incomingData;
            this.Pause = pause;
            this.Decrementing = decrementing;
            this.BatchNumberList = bnlist;
        }

        public void GetSensorsActivity()
        {
            SensorIndicator();
            TipperOneSensor();
            TipperTwoSensor();
            DumpTruckSensor();
            StockPileSensor();
            MainCaneSensor();
            CaneKnivesSensor();
            ShreddedCaneSensor();
        }

        private void SensorIndicator()
        {
            for (int i = 0; i < BatchNumberList.sensorIndicators.Count; i++)
            {
                ctcc.ChangeColorTextBox(BatchNumberList.sensorIndicators[i].Item1, Color.CornflowerBlue);


                if (IncomingData.ToLower() == "side cane object")
                {
                    if (BatchNumberList.sensorIndicators[i].Item2 == "Side Cane")
                    {
                        ctcc.ChangeColorTextBox(BatchNumberList.sensorIndicators[i].Item1, Color.Orange);
                    }
                }

                if (IncomingData.ToLower() == "main cane object")
                {
                    if (BatchNumberList.sensorIndicators[i].Item2 == "Main Cane")
                    {
                        ctcc.ChangeColorTextBox(BatchNumberList.sensorIndicators[i].Item1, Color.Orange);
                    }
                }

                if (IncomingData.ToLower() == "cane knives object")
                {
                    if (BatchNumberList.sensorIndicators[i].Item2 == "Cane Knives")
                    {
                        ctcc.ChangeColorTextBox(BatchNumberList.sensorIndicators[i].Item1, Color.Orange);
                    }
                }

                if (IncomingData.ToLower() == "shredded cane object")
                {
                    if (BatchNumberList.sensorIndicators[i].Item2 == "Shredder")
                    {
                        ctcc.ChangeColorTextBox(BatchNumberList.sensorIndicators[i].Item1, Color.Orange);
                    }
                }
            }

        }

        private void TipperOneSensor()
        {
            int count;
            int tipperOneMaxCount = ci.TipperOneMaxCount;

            for (int i = 0; i < BatchNumberList.tipperOne.Count; i++)
            {
                if (IncomingData.ToLower() == "side cane object")
                {
                    count = int.Parse(ctcc.GetTextboxValue(BatchNumberList.tipperOne[i].Item2));

                    if (!Pause)
                    {
                        if (Decrementing)
                        {
                            count--;
                        }
                        else
                        {
                            count++;
                        }
                    }

                    ctcc.ChangeText(BatchNumberList.tipperOne[i].Item2, count.ToString());

                    if (count > tipperOneMaxCount)
                    {
                        BatchNumberList.dumpCanesHistory.Remove(new Tuple<RichTextBox, RichTextBox, string>(BatchNumberList.tipperOne[i].Item1, BatchNumberList.tipperOne[i].Item2, "Tipper One"));
                        BatchNumberList.mainCaneBatchNumbers.Add(ctcc.GetTextboxValue(BatchNumberList.tipperOne[i].Item1));

                        ctcc.ChangeText(BatchNumberList.tipperOne[i].Item1, "");
                        ctcc.ChangeText(BatchNumberList.tipperOne[i].Item2, "");
                        ctcc.ChangeColorTextBox(BatchNumberList.tipperOne[i].Item2, Color.CornflowerBlue);

                    }
                }
            }

            for (int i = BatchNumberList.tipperOne.Count - 1; i >= 0; i--)
            {
                if (ctcc.GetTextboxValue(BatchNumberList.tipperOne[i].Item1) == "")
                {
                    BatchNumberList.tipperOne.RemoveAt(i);
                }
            }
        }

        private void TipperTwoSensor()
        {
            int count;
            int tipperTwoMaxCount = ci.TipperTwoMaxCount;

            for (int i = 0; i < BatchNumberList.tipperTwo.Count; i++)
            {
                if (IncomingData.ToLower() == "side cane object")
                {
                    count = int.Parse(ctcc.GetTextboxValue(BatchNumberList.tipperTwo[i].Item2));

                    if (!Pause)
                    {
                        if (Decrementing)
                        {
                            count--;
                        }
                        else
                        {
                            count++;
                        }
                    }

                    ctcc.ChangeText(BatchNumberList.tipperTwo[i].Item2, count.ToString());

                    if (count > tipperTwoMaxCount)
                    {
                        BatchNumberList.dumpCanesHistory.Remove(new Tuple<RichTextBox, RichTextBox, string>(BatchNumberList.tipperTwo[i].Item1, BatchNumberList.tipperTwo[i].Item2, "Tipper Two"));
                        BatchNumberList.mainCaneBatchNumbers.Add(ctcc.GetTextboxValue(BatchNumberList.tipperTwo[i].Item1));

                        ctcc.ChangeText(BatchNumberList.tipperTwo[i].Item1, "");
                        ctcc.ChangeText(BatchNumberList.tipperTwo[i].Item2, "");
                        ctcc.ChangeColorTextBox(BatchNumberList.tipperTwo[i].Item2, Color.CornflowerBlue);
                    }
                }
            }

            for (int i = BatchNumberList.tipperTwo.Count - 1; i >= 0; i--)
            {
                if (ctcc.GetTextboxValue(BatchNumberList.tipperTwo[i].Item1) == "")
                {
                    BatchNumberList.tipperTwo.RemoveAt(i);
                }
            }
        }

        private void DumpTruckSensor()
        {
            int count;
            int dumpAndPileMaxCount = ci.DumpAndPileMaxCount;

            for (int i = 0; i < BatchNumberList.dumpTruck.Count; i++)
            {
                if (IncomingData.ToLower() == "side cane object")
                {
                    count = int.Parse(ctcc.GetTextboxValue(BatchNumberList.dumpTruck[i].Item2));

                    if (!Pause)
                    {
                        if (Decrementing)
                        {
                            count--;
                        }
                        else
                        {
                            count++;
                        }
                    }

                    ctcc.ChangeText(BatchNumberList.dumpTruck[i].Item2, count.ToString());

                    if (count > dumpAndPileMaxCount)
                    {
                        BatchNumberList.dumpCanesHistory.Remove(new Tuple<RichTextBox, RichTextBox, string>(BatchNumberList.dumpTruck[i].Item1, BatchNumberList.dumpTruck[i].Item2, "Dump Truck"));
                        BatchNumberList.mainCaneBatchNumbers.Add(ctcc.GetTextboxValue(BatchNumberList.dumpTruck[i].Item1));

                        ctcc.ChangeText(BatchNumberList.dumpTruck[i].Item1, "");
                        ctcc.ChangeText(BatchNumberList.dumpTruck[i].Item2, "");
                        ctcc.ChangeColorTextBox(BatchNumberList.dumpTruck[i].Item2, Color.CornflowerBlue);
                    }
                }
            }

            for (int i = BatchNumberList.dumpTruck.Count - 1; i >= 0; i--)
            {
                if (ctcc.GetTextboxValue(BatchNumberList.dumpTruck[i].Item1) == "")
                {
                    BatchNumberList.dumpTruck.RemoveAt(i);
                }
            }
        }

        private void StockPileSensor()
        {
            int count;
            int dumpAndPileMaxCount = ci.DumpAndPileMaxCount;

            for (int i = 0; i < BatchNumberList.stockPile.Count; i++)
            {
                if (IncomingData.ToLower() == "side cane object")
                {
                    count = int.Parse(ctcc.GetTextboxValue(BatchNumberList.stockPile[i].Item2));

                    if (!Pause)
                    {
                        if (Decrementing)
                        {
                            count--;
                        }
                        else
                        {
                            count++;
                        }
                    }

                    ctcc.ChangeText(BatchNumberList.stockPile[i].Item2, count.ToString());

                    if (count > dumpAndPileMaxCount)
                    {
                        BatchNumberList.dumpCanesHistory.Remove(new Tuple<RichTextBox, RichTextBox, string>(BatchNumberList.stockPile[i].Item1, BatchNumberList.stockPile[i].Item2, "Stock Pile"));
                        BatchNumberList.mainCaneBatchNumbers.Add(ctcc.GetTextboxValue(BatchNumberList.stockPile[i].Item1));

                        ctcc.ChangeText(BatchNumberList.stockPile[i].Item1, "");
                        ctcc.ChangeText(BatchNumberList.stockPile[i].Item2, "");
                        ctcc.ChangeColorTextBox(BatchNumberList.stockPile[i].Item2, Color.CornflowerBlue);
                    }
                }
            }

            for (int i = BatchNumberList.stockPile.Count - 1; i >= 0; i--)
            {
                if (ctcc.GetTextboxValue(BatchNumberList.stockPile[i].Item1) == "")
                {
                    BatchNumberList.stockPile.RemoveAt(i);
                }
            }
        }

        private void MainCaneSensor()
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

            for (int i = 0; i <= BatchNumberList.mainCaneBatchNumbers.Count - 1; i++)
            {
                for (int y = 0; y < BatchNumberList.lTbox.Count; y++)
                {
                    if (ctcc.GetTextboxValue(BatchNumberList.lTbox[y].Item1) == "" && BatchNumberList.lTbox[y].Item3 == "MainCane")
                    {
                        ctcc.ChangeText(BatchNumberList.lTbox[y].Item1, BatchNumberList.mainCaneBatchNumbers[i]);
                        ctcc.ChangeText(BatchNumberList.lTbox[y].Item2, "0");
                        ctcc.ChangeColorTextBox(BatchNumberList.lTbox[y].Item2, Color.Maroon);
                        ctcc.ChangeForeColorTextBox(BatchNumberList.lTbox[y].Item2, Color.White);

                        BatchNumberList.mainCane.Add(new Tuple<RichTextBox, RichTextBox>(BatchNumberList.lTbox[y].Item1, BatchNumberList.lTbox[y].Item2));


                        break;

                    }
                }
                BatchNumberList.mainCaneBatchNumbers.RemoveAt(i);
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

            for (int i = 0; i < BatchNumberList.mainCane.Count; i++)
            {
                if (IncomingData.ToLower() == "main cane object")
                {
                    count = int.Parse(ctcc.GetTextboxValue(BatchNumberList.mainCane[i].Item2));

                    if (!Pause)
                    {
                        count++;
                    }

                    ctcc.ChangeText(BatchNumberList.mainCane[i].Item2, count.ToString());

                    if (count > mainCaneMaxCount)
                    {
                        BatchNumberList.caneKnivesBatchNumbers.Add(ctcc.GetTextboxValue(BatchNumberList.mainCane[i].Item1));

                        ctcc.ChangeText(BatchNumberList.mainCane[i].Item1, "");
                        ctcc.ChangeText(BatchNumberList.mainCane[i].Item2, "");
                        ctcc.ChangeColorTextBox(BatchNumberList.mainCane[i].Item2, Color.CornflowerBlue);
                    }
                }
            }

            /*
             * Removing batch numbers from list after counting is finished.
             */

            for (int i = BatchNumberList.mainCane.Count - 1; i >= 0; i--)
            {
                if (ctcc.GetTextboxValue(BatchNumberList.mainCane[i].Item1) == "")
                {
                    BatchNumberList.mainCane.RemoveAt(i);
                }
            }
        }

        private void CaneKnivesSensor()
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

            for (int i = 0; i <= BatchNumberList.caneKnivesBatchNumbers.Count - 1; i++)
            {
                for (int y = 0; y < BatchNumberList.lTbox.Count; y++)
                {
                    if (ctcc.GetTextboxValue(BatchNumberList.lTbox[y].Item1) == "" && BatchNumberList.lTbox[y].Item3 == "CaneKnives")
                    {
                        ctcc.ChangeText(BatchNumberList.lTbox[y].Item1, BatchNumberList.caneKnivesBatchNumbers[i]);
                        ctcc.ChangeText(BatchNumberList.lTbox[y].Item2, "0");
                        ctcc.ChangeColorTextBox(BatchNumberList.lTbox[y].Item2, Color.Maroon);
                        ctcc.ChangeForeColorTextBox(BatchNumberList.lTbox[y].Item2, Color.White);

                        BatchNumberList.caneKnives.Add(new Tuple<RichTextBox, RichTextBox>(BatchNumberList.lTbox[y].Item1, BatchNumberList.lTbox[y].Item2));


                        break;

                    }
                }
                BatchNumberList.caneKnivesBatchNumbers.RemoveAt(i);
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

            for (int i = 0; i < BatchNumberList.caneKnives.Count; i++)
            {
                if (IncomingData.ToLower() == "cane knives object")
                {
                    count = int.Parse(ctcc.GetTextboxValue(BatchNumberList.caneKnives[i].Item2));

                    if (!Pause)
                    {
                        count++;
                    }

                    ctcc.ChangeText(BatchNumberList.caneKnives[i].Item2, count.ToString());

                    if (count > knivesAndShredderMaxCount)
                    {
                        BatchNumberList.shredderBatchNumbers.Add(ctcc.GetTextboxValue(BatchNumberList.caneKnives[i].Item1));

                        ctcc.ChangeText(BatchNumberList.caneKnives[i].Item1, "");
                        ctcc.ChangeText(BatchNumberList.caneKnives[i].Item2, "");
                        ctcc.ChangeColorTextBox(BatchNumberList.caneKnives[i].Item2, Color.CornflowerBlue);
                    }
                }
            }

            /*
             * Removing batch numbers from list after counting is finished.
             */

            for (int i = BatchNumberList.caneKnives.Count - 1; i >= 0; i--)
            {
                if (ctcc.GetTextboxValue(BatchNumberList.caneKnives[i].Item1) == "")
                {
                    BatchNumberList.caneKnives.RemoveAt(i);
                }
            }
        }

        private void ShreddedCaneSensor()
        {
            int count;
            int knivesAndShredderMaxCount = ci.KnivesAndShredderMaxCount;
            string bnShred = "";

            /*
             * Looping to the batch numbers stored in list coming from side canes.
             * Loop from list to fill in empty boxes
             * Loop from list to prevent overlapping of 
             * batch numbers with the same countings.
             * 
             * Batch numbers are also being added to another list
             * responsible for tracking the countings
             */

            for (int i = 0; i <= BatchNumberList.shredderBatchNumbers.Count - 1; i++)
            {
                for (int y = 0; y < BatchNumberList.lTbox.Count; y++)
                {
                    if (ctcc.GetTextboxValue(BatchNumberList.lTbox[y].Item1) == "" && BatchNumberList.lTbox[y].Item3 == "Shredder")
                    {
                        ctcc.ChangeText(BatchNumberList.lTbox[y].Item1, BatchNumberList.shredderBatchNumbers[i]);
                        ctcc.ChangeText(BatchNumberList.lTbox[y].Item2, "0");
                        ctcc.ChangeColorTextBox(BatchNumberList.lTbox[y].Item2, Color.Maroon);
                        ctcc.ChangeForeColorTextBox(BatchNumberList.lTbox[y].Item2, Color.White);

                        BatchNumberList.shreddedCane.Add(new Tuple<RichTextBox, RichTextBox>(BatchNumberList.lTbox[y].Item1, BatchNumberList.lTbox[y].Item2));


                        break;

                    }
                }
                BatchNumberList.shredderBatchNumbers.RemoveAt(i);
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

            for (int i = 0; i < BatchNumberList.shreddedCane.Count; i++)
            {
                if (IncomingData.ToLower() == "shredded cane object")
                {
                    count = int.Parse(ctcc.GetTextboxValue(BatchNumberList.shreddedCane[i].Item2));

                    if (!Pause)
                    {
                        count++;
                    }

                    ctcc.ChangeText(BatchNumberList.shreddedCane[i].Item2, count.ToString());

                    if (count > knivesAndShredderMaxCount)
                    {
                        bnShred = ctcc.GetTextboxValue(BatchNumberList.shreddedCane[i].Item1);
                        ctcc.ChangeText(BatchNumberList.shreddedCane[i].Item1, "");
                        ctcc.ChangeText(BatchNumberList.shreddedCane[i].Item2, "");
                        ctcc.ChangeColorTextBox(BatchNumberList.shreddedCane[i].Item2, Color.CornflowerBlue);
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

            for (int i = BatchNumberList.shreddedCane.Count - 1; i >= 0; i--)
            {
                if (ctcc.GetTextboxValue(BatchNumberList.shreddedCane[i].Item1) == "")
                {
                    for (int y = 0; y < BatchNumberList.lTbox.Count; y++)
                    {
                        if (BatchNumberList.lTbox[y].Item3 == "NirWashing")
                        {
                            ctcc.ChangeText(BatchNumberList.lTbox[y].Item1, bnShred);
                            ctcc.ChangeText(BatchNumberList.lTbox[y].Item2, "0");
                            ctcc.ChangeColorTextBox(BatchNumberList.lTbox[y].Item2, Color.Maroon);
                            ctcc.ChangeForeColorTextBox(BatchNumberList.lTbox[y].Item2, Color.White);

                            break;
                        }
                    }
                    BatchNumberList.shreddedCane.RemoveAt(i);
                }
            }
        }
    }
}
