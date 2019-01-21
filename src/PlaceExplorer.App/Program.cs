using Microsoft.Office.Interop.Excel;
using PlaceExplorer.Client;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace PlaceExplorer.App
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== Google Places API Client ===\n");

            Console.WriteLine("API key : ");
            string apiKey = Console.ReadLine();

            Console.WriteLine("Search spot latitude : ");
            string latitude = Console.ReadLine();
            Console.WriteLine("Search spot longitude : ");
            string longitude = Console.ReadLine();
            Console.WriteLine("Search radius (meter) : ");
            string radius = Console.ReadLine();
            Console.WriteLine("Search type : ");
            string type = Console.ReadLine();
            Console.WriteLine("Name filter (optional) : ");
            string name = Console.ReadLine();
            Console.WriteLine("Search language (optional) : ");
            string language = Console.ReadLine();

            Console.WriteLine("Result file name : ");
            string filepath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"\" + Console.ReadLine() + ".xlsx";
            if (File.Exists(filepath))
            {
                Console.WriteLine("该文件已存在，请换名字");
                throw new Exception("该文件已存在，请换名字");
            }

            Console.WriteLine("\n=== ATTENTION : The API returns 20 results per call, Google allows only 45 calls per day===");
            Console.WriteLine("API calls number : ");
            int nb = Int32.Parse(Console.ReadLine());
            if (nb > 45)
            {
                Console.WriteLine("眼瞎了吗？");
                throw new Exception("眼瞎了吗？");
            }

            var places = new List<PlaceDetail>();

            var client = new GooglePlacesClient(apiKey, latitude, longitude, radius, type, name, language);

            Task.Run(async () =>
            {
                string token = null;
                while (nb > 0)
                {
                    try
                    {
                        var temp = await client.GetPlaceDetailList(token);
                        token = temp.Key;
                        places.AddRange(temp.Value);
                    }
                    catch (Exception e)
                    {
                        throw e;
                    }

                    nb--;
                }
            }).Wait();

            Application excel = new Application
            {
                DisplayAlerts = false
            };
            excel.Workbooks.Add();
            Worksheet worksheet = (Worksheet)excel.ActiveSheet;
            worksheet.Cells[1, "A"] = "Name";
            worksheet.Cells[1, "B"] = "Address";
            worksheet.Cells[1, "C"] = "Url";
            worksheet.Cells[1, "D"] = "Telephone";
            worksheet.Cells[1, "E"] = "Website";
            worksheet.Cells[1, "F"] = "Rating";
            worksheet.Cells[1, "G"] = "Location";
            worksheet.Cells[1, "H"] = "Types";

            Console.WriteLine("Copying the contents to Excel");
            int rowIndex = 3;
            foreach (var place in places)
            {
                worksheet.Cells[rowIndex, "A"] = place.Name;
                worksheet.Cells[rowIndex, "B"] = place.Vicinity;
                worksheet.Cells[rowIndex, "C"] = place.Url;
                worksheet.Cells[rowIndex, "D"] = place.International_phone_number;
                worksheet.Cells[rowIndex, "E"] = place.Website;
                worksheet.Cells[rowIndex, "F"] = place.Rating;
                worksheet.Cells[rowIndex, "G"] = place.Geometry.Location.Lat + "," + place.Geometry.Location.Lng;
                worksheet.Cells[rowIndex, "H"] = string.Join(",", place.Types);
                rowIndex++;
                //Console.Write("List Title: {0}", list.Title);
                //Console.WriteLine("\t"+"Item Count:"+list.ItemCount);
            }

            worksheet.Cells[rowIndex + 2, "A"] = "Web Request : " + client.Request;

            worksheet.SaveAs(filepath);
            Console.WriteLine("Export Completed : " + filepath);
            Console.ReadLine();
            excel.Quit();
            GC.Collect();
        }
    }
}
