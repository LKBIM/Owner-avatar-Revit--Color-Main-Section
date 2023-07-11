using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using LKBIM.Functions;

namespace LKBIM
{
    [TransactionAttribute(TransactionMode.Manual)]
    public class _603ColorMainSection : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIDocument uidoc = commandData.Application.ActiveUIDocument;
            Document doc = uidoc.Document;
            //Get active view
            View view = doc.ActiveView;
            //Clear Overrides method
            OverrideGraphicSettings a = new OverrideGraphicSettings();
            //Set Override Graphics of MainViews
            OverrideGraphicSettings b = new OverrideGraphicSettings();
            b.SetProjectionLineColor(new Color(255, 0, 0));
            //Get all sections in view
            IList<Element> sections = new FilteredElementCollector(doc, view.Id).OfCategory(BuiltInCategory.OST_Viewers).WhereElementIsNotElementType().ToElements();
            RetriveElementFromCollector sc = new RetriveElementFromCollector();
            List<Element> list = sc.refc(doc, sections);
            //Get Main View
            List<Element> MainViews = new List<Element>();
            foreach (Element el in list)
            {
                string e2 = el.OwnerViewId.ToString();
                if (e2 == "-1")
                {
                    MainViews.Add(el);
                }
            }
            //Reset Overrides and Set Overrides
            try
            {
                using (Transaction trans = new Transaction(doc, "Override"))
                {
                    trans.Start();
                    foreach (var i in sections)
                    {
                        view.SetElementOverrides(i.Id, a);
                    }
                    foreach (var i1 in MainViews)
                    {
                        view.SetElementOverrides(i1.Id, b);
                    }
                    trans.Commit();
                }
                return Result.Succeeded;
            }
            catch (Exception e)
            {
                message = e.Message;
                return Result.Failed;

            }
        }
    }
}
