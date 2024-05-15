using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Labr3
{
    public class Region // создан объект регион со свойсвами - название и года, поле изменение численности
    {
        public string RegionName;
        public double N2009;
        public double N2010;
        public double N2011;
        public double N2012;
        public double N2013;
        public double N2014;
        public double N2015;
        public double N2016;
        public double N2017;
        public double N2018;
        public double N2019;
        public double N2020;
        public double N2021;
        public double N2022;
        public double N2023;

        public double Pizm;


        //public Region() { } 
    }

    public class ListRegion
    {
        public List<Region> regions;

        //public void LoadFromExel() 
        //{ 

        //}
        public List<Region> copy()
        {
            List<Region> copy = new List<Region>();
            List<Region> copy2 = new List<Region>();

            foreach (var LR in this.regions)// удаляем элементы списка, в котрых изменение численности положительное, то есть увеличение численности населения
            {
                //izm = LR.Pizm;
                if (LR.Pizm < 0)
                {
                    copy.Add(LR);
                }
            }
            // сортируем список в порядке возрастанияя
            // первый  и последний элементы списка являются нужными
            copy.Sort((x, y) => x.Pizm.CompareTo(y.Pizm));

            copy2.Add(copy[0]);
            copy2.Add(copy[copy.Count - 1]);

            return copy2;
        }

        public void LoadFromExel()
        {
            //string str; // название региоона
            //double doublestr; // значениечисленности в году
            int rCnt; // количество строк
                      //int cCnt;// количество колнок

            //List<Region> regions2 = new List<Region>();
            regions = new List<Region>();
            regions.Clear(); // очищение списка перед новым заполнением 

            OpenFileDialog opf = new OpenFileDialog();
            opf.Filter = "Excel (*.XLS)|*.XLS";
            opf.ShowDialog();
            //System.Data.DataTable tb = new System.Data.DataTable();
            string filename = opf.FileName;

            Microsoft.Office.Interop.Excel.Application ExcelApp = new Microsoft.Office.Interop.Excel.Application();
            Microsoft.Office.Interop.Excel._Workbook ExcelWorkBook;
            Microsoft.Office.Interop.Excel.Worksheet ExcelWorkSheet;
            Microsoft.Office.Interop.Excel.Range ExcelRange;

            ExcelWorkBook = ExcelApp.Workbooks.Open(filename, 0, true, 5, "", "", true, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "\t", false,
                false, 0, true, 1, 0);


            ExcelWorkSheet = (Microsoft.Office.Interop.Excel.Worksheet)ExcelWorkBook.Worksheets.get_Item(1);

            ExcelRange = ExcelWorkSheet.UsedRange; //

            for (rCnt = 1; rCnt <= ExcelRange.Rows.Count; rCnt++)

            {

                double Znkol2 = (ExcelRange.Cells[rCnt, 2] as Microsoft.Office.Interop.Excel.Range).Value2;

                double Znkol16 = (ExcelRange.Cells[rCnt, 16] as Microsoft.Office.Interop.Excel.Range).Value2;

                // изменение численности за 15 летв процентном соотношении.Последний год минус первый, делим на первый,умнож на100
                double izm = ((Znkol16 - Znkol2) / Znkol2) * 100;

                Region region = new Region();
                region.RegionName = (string)(ExcelRange.Cells[rCnt, 1] as Microsoft.Office.Interop.Excel.Range).Value2;

                region.N2009 = (ExcelRange.Cells[rCnt, 2] as Microsoft.Office.Interop.Excel.Range).Value2;
                region.N2010 = (ExcelRange.Cells[rCnt, 3] as Microsoft.Office.Interop.Excel.Range).Value2;
                region.N2011 = (ExcelRange.Cells[rCnt, 4] as Microsoft.Office.Interop.Excel.Range).Value2;
                region.N2012 = (ExcelRange.Cells[rCnt, 5] as Microsoft.Office.Interop.Excel.Range).Value2;
                region.N2013 = (ExcelRange.Cells[rCnt, 6] as Microsoft.Office.Interop.Excel.Range).Value2;
                region.N2014 = (ExcelRange.Cells[rCnt, 7] as Microsoft.Office.Interop.Excel.Range).Value2;
                region.N2015 = (ExcelRange.Cells[rCnt, 8] as Microsoft.Office.Interop.Excel.Range).Value2;
                region.N2016 = (ExcelRange.Cells[rCnt, 9] as Microsoft.Office.Interop.Excel.Range).Value2;
                region.N2017 = (ExcelRange.Cells[rCnt, 10] as Microsoft.Office.Interop.Excel.Range).Value2;
                region.N2018 = (ExcelRange.Cells[rCnt, 11] as Microsoft.Office.Interop.Excel.Range).Value2;
                region.N2019 = (ExcelRange.Cells[rCnt, 12] as Microsoft.Office.Interop.Excel.Range).Value2;
                region.N2020 = (ExcelRange.Cells[rCnt, 13] as Microsoft.Office.Interop.Excel.Range).Value2;
                region.N2021 = (ExcelRange.Cells[rCnt, 14] as Microsoft.Office.Interop.Excel.Range).Value2;
                region.N2022 = (ExcelRange.Cells[rCnt, 15] as Microsoft.Office.Interop.Excel.Range).Value2;
                region.N2023 = (ExcelRange.Cells[rCnt, 16] as Microsoft.Office.Interop.Excel.Range).Value2;

                region.Pizm = Math.Round(izm, 2);

                regions.Add(region);

            }

            //this.regions.Clear();
            //this.regions= regions;

            ExcelWorkBook.Close(true, null, null);
            ExcelApp.Quit();
        }

        public double extrapol(List<double> massEkstp, int Kol)
        {
            double y = 0;

            foreach (var LR in massEkstp)
            {
                y = y + LR;
            }

            y = y / Kol;

            return y;
        }
    }
}
