using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Mechanical;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_2_WallCount
{
    [Transaction(TransactionMode.Manual)]
    public class Main : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Document doc = uidoc.Document;
            
            var levelList = new FilteredElementCollector(doc) //список уровней в проекте
                .OfClass(typeof(Level))
                .Cast<Level>()
                .ToList();

            var wallList = new FilteredElementCollector(doc) // список стен в проекте
                .OfClass(typeof(Wall))
                .Cast<Wall>()
                .ToList();

            int[] countByLevels = new int[levelList.Count()]; //Массив для хранения количества стен по этажам
            int i = 0;
            string info = ""; //для вывода информации

            foreach (Level l in levelList)
            {
                foreach(Wall w in wallList)
                {
                    if (l.Id==w.LevelId)
                    {
                        countByLevels[i]++;
                    }
                }
                info += $"{l.Name}: {countByLevels[i]} walls {Environment.NewLine}";
                i++;
            }
                TaskDialog.Show("Количество стен по этажам: ", info );
            return Result.Succeeded;

        }
    }
}
