using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using static System.Collections.Specialized.BitVector32;
namespace LKBIM.Functions
{
    class RetriveElementFromCollector
    {
        public List<Element> refc(Document doc, IList<Element> elem)
        {
            List<ElementId> ids = (from Element r in elem select r.Id).ToList();
            List<Element> list = new List<Element>();
            foreach (ElementId id in ids)
            {
                Element ele = doc.GetElement(id);
                list.Add(ele);
            }

            return list;
        }
        //Using code
        //RetriveElementFromCollector sc = new RetriveElementFromCollector();
        //List<Element> list = sc.refc(uidoc, doc, elem);


    }
}

