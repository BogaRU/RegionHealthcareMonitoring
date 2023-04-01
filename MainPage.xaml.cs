using OfficeOpenXml;
using System.Linq;

namespace AlmazovaProject;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
        population.Text = "2000000";
        Dictionary<string, RegionInfo> regions;
        DbInitialize();
        XlsxParse();
        population.Text = GetLastValue(regions["Stv"].Population) + "чел.,";

        void XlsxParse()
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            FileInfo dbfile = new FileInfo("A:/C#/Projects/AlmazovaProject/AlmazovaProject/Resources/Database/db1.xlsx");
            using (ExcelPackage package = new ExcelPackage(dbfile))
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
                int colCount = worksheet.Dimension.End.Column;
                int rowCount = worksheet.Dimension.End.Row;
                List<List<string>> db = new List<List<string>>();
                for (int row = 1; row <= rowCount; row++)
                {
                    db.Add(new List<string>());
                    for (int col = 1; col <= colCount; col++)
                    {
                        db[row - 1].Add(worksheet.Cells[row, col].Value?.ToString().Trim());
                    }
                }
                DbFill(db);
            }
        }

        void DbInitialize()
        {
            var regionsArr = new RegionInfo[]{
                new RegionInfo { Name = "Российская Федерация", Id = "RF" },
                new RegionInfo { Name = "Северо-Западный федеральный округ", Id = "Szf" },
                new RegionInfo { Name = "Республика Карелия", Id = "Krl" },
                new RegionInfo { Name = "Республика Коми", Id = "Kom" },
                new RegionInfo { Name = "Архангельская область", Id = "Arh" },
                new RegionInfo { Name = "Ненецкий автономный округ", Id = "Nen"},
                new RegionInfo { Name = "Вологодская область", Id = "Vol" },
                new RegionInfo { Name = "Калининградская область", Id = "Kgd" },
                new RegionInfo { Name = "Ленинградская область", Id = "Len" },
                new RegionInfo { Name = "Мурманская область", Id = "Mur" },
                new RegionInfo { Name = "Новгородская область", Id = "Ngd" },
                new RegionInfo { Name = "Псковская область", Id = "Psk" },
                new RegionInfo { Name = "Санкт-Петербург", Id = "Spb" },
                new RegionInfo { Name = "Южный федеральный округ", Id = "Ufo" },
                new RegionInfo { Name = "Республика Адыгея", Id = "Adg" },
                new RegionInfo { Name = "Республика Калмыкия", Id = "Kmk" },
                new RegionInfo { Name = "Республика Крым", Id = "Crm" },
                new RegionInfo { Name = "Краснодарский край", Id = "Knd" },
                new RegionInfo { Name = "Астраханская область", Id = "Ast" },
                new RegionInfo { Name = "Волгоградская область", Id = "Vgr" },
                new RegionInfo { Name = "Ростовская область", Id = "Rst" },
                new RegionInfo { Name = "Севастополь", Id = "Svs" },
                new RegionInfo { Name = "Северо-Кавказский федеральный округ", Id = "Skf" },
                new RegionInfo { Name = "Республика Дагестан", Id = "Dag" },
                new RegionInfo { Name = "Республика Ингушетия", Id = "Ing" },
                new RegionInfo { Name = "Кабардино-Балкарская Республика", Id = "Kbr" },
                new RegionInfo { Name = "Карачаево-Черкесская Республика", Id = "Kcr" },
                new RegionInfo { Name = "Республика Северная Осетия-Алания", Id = "Ost" },
                new RegionInfo { Name = "Чеченская Республика", Id = "Chn" },
                new RegionInfo { Name = "Ставропольский край", Id = "Stv" },
                new RegionInfo { Name = "Приволжский федеральный округ", Id = "Pvf" },
                new RegionInfo { Name = "Республика Башкортостан", Id = "Bkn" },
                new RegionInfo { Name = "Республика Марий Эл", Id = "Mrl" },
                new RegionInfo { Name = "Республика Мордовия", Id = "Mrd" },
                new RegionInfo { Name = "Республика Татарстан", Id = "Ttn" },
                new RegionInfo { Name = "Удмуртская Республика", Id = "Udm" },
                new RegionInfo { Name = "Чувашская Республика", Id = "Chv" },
                new RegionInfo { Name = "Пермский край", Id = "Pms" },
                new RegionInfo { Name = "Кировская область", Id = "Krv" },
                new RegionInfo { Name = "Нижегородская область", Id = "Ngr" },
                new RegionInfo { Name = "Оренбургская область", Id = "Orb" },
                new RegionInfo { Name = "Пензенская область", Id = "Pnz" },
                new RegionInfo { Name = "Самарская область", Id = "Smr" },
                new RegionInfo { Name = "Саратовская область", Id = "Srt" },
                new RegionInfo { Name = "Ульяновская область", Id = "Uln" }
            };

            regions = new Dictionary<string, RegionInfo>();
            for (int i = 0; i < regionsArr.Length; i++)
            {
                regions.Add(regionsArr[i].Id, regionsArr[i]);
                RegionInfo.RegionIds[i] = regionsArr[i].Id;
            }
        }

        void DbFill(List<List<string>> db)
        {
            for (int i = 0; i < regions.Count; i++)
            {
                var parsedDb = db.Where(l => l.Contains(regions[RegionInfo.RegionIds[i]].Name))
                                .Select(l => l
                                .Select(s =>
                                    {
                                        if (s == "#N/A" || s == "#DIV/0!" || s == "#Н/Д" || s == "#ДЕЛ/0!") s = null;
                                        return s;
                                    })
                                .ToList())
                                .ToList();
                for (int j = 0; j < parsedDb.Count; j++)
                {
                    regions[RegionInfo.RegionIds[i]].Date.Add(new DateTime(Convert.ToInt32(parsedDb[j][1]), Convert.ToInt32(parsedDb[j][2]), 1));
                    regions[RegionInfo.RegionIds[i]].Population.Add(Convert.ToInt64(parsedDb[j][3]));
                    regions[RegionInfo.RegionIds[i]].TltPrehospital.Add(Convert.ToInt32(parsedDb[j][4]));
                    regions[RegionInfo.RegionIds[i]].SteAcs.Add(Convert.ToInt32(parsedDb[j][5]));
                    regions[RegionInfo.RegionIds[i]].NsteAcs.Add(Convert.ToInt32(parsedDb[j][6]));
                    regions[RegionInfo.RegionIds[i]].PciAcs.Add(Convert.ToDouble(parsedDb[j][7]));
                    regions[RegionInfo.RegionIds[i]].PciSteAcs.Add(Convert.ToDouble(parsedDb[j][8]));
                    regions[RegionInfo.RegionIds[i]].PciNsteAcs.Add(Convert.ToDouble(parsedDb[j][9]));
                    regions[RegionInfo.RegionIds[i]].PciNsteAcsHighRisk.Add(Convert.ToDouble(parsedDb[j][10]));
                    regions[RegionInfo.RegionIds[i]].PciSum.Add(Convert.ToInt32(parsedDb[j][11]));
                    regions[RegionInfo.RegionIds[i]].ElectivePci.Add(Convert.ToInt32(parsedDb[j][12]));
                    regions[RegionInfo.RegionIds[i]].FatalityRsc.Add(Convert.ToDouble(parsedDb[j][13]));
                    regions[RegionInfo.RegionIds[i]].FatalityPso.Add(Convert.ToDouble(parsedDb[j][14]));
                    regions[RegionInfo.RegionIds[i]].FatalityNonProper.Add(Convert.ToDouble(parsedDb[j][15]));
                    regions[RegionInfo.RegionIds[i]].TltPharmProp.Add(Convert.ToDouble(parsedDb[j][16]));
                    regions[RegionInfo.RegionIds[i]].TwoHours.Add(Convert.ToDouble(parsedDb[j][17]));
                    regions[RegionInfo.RegionIds[i]].TwelveHours.Add(Convert.ToDouble(parsedDb[j][18]));
                    regions[RegionInfo.RegionIds[i]].AtHome.Add(Convert.ToInt32(parsedDb[j][19]));
                    regions[RegionInfo.RegionIds[i]].FatalityFirstDay.Add(Convert.ToDouble(parsedDb[j][20]));
                }
            }
        }

        string GetLastValue<T>(List<T> l)
        {
            for (int i = l.Count - 1; i >= 0; i--)
            {
                if (l[i] != null && l[i].ToString() != "0") return l[i].ToString();
            }
            return "Empty value";
        }
    }
}