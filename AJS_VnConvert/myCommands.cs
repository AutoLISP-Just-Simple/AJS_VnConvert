// (C) Copyright 2024 by
//
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.Runtime;
using System.Linq;

// This line is not mandatory, but improves loading performances
[assembly: CommandClass(typeof(AJS_VnConvert.MyCommands))]

namespace AJS_VnConvert
{
    // This class is instantiated by AutoCAD for each document when
    // a command is called by the user the first time in the context
    // of a given document. In other words, non static data in this class
    // is implicitly per-document!
    public class MyCommands
    {
        // Modal Command with localized name
        [CommandMethod("AJSVnConvert", CommandFlags.Modal)]
        public void MyCommand() // This method can have any name
        {
            // Put your command code here
            Document doc = Application.DocumentManager.MdiActiveDocument;
            Editor ed;
            if (doc != null)
            {
                ed = doc.Editor;
                ed.WriteMessage("AJS-VNCONVERT from www.lisp.vn");

                foreach (var id in CharsetIdMap.CharsetIdMapArray)
                    ed.WriteMessage("\n" + id.Name + " " + id.Id);

                PromptIntegerOptions pio = new PromptIntegerOptions("\nChọn bảng mã nguồn");
                pio.DefaultValue = 20;
                pio.LowerLimit = CharsetIdMap.CharsetIdMapArray.Min(x => x.Id);
                pio.UpperLimit = CharsetIdMap.CharsetIdMapArray.Max(x => x.Id);
                PromptIntegerResult spir = ed.GetInteger(pio);
                if (spir.Status != PromptStatus.OK || !CharsetIdMap.CharsetIdMapArray.Any(x => x.Id == spir.Value)) return;

                pio = new PromptIntegerOptions("\nChọn bảng mã đích");
                pio.DefaultValue = 1;
                pio.LowerLimit = CharsetIdMap.CharsetIdMapArray.Min(x => x.Id);
                pio.UpperLimit = CharsetIdMap.CharsetIdMapArray.Max(x => x.Id);
                PromptIntegerResult dpir = ed.GetInteger(pio);
                if (dpir.Status != PromptStatus.OK || !CharsetIdMap.CharsetIdMapArray.Any(x => x.Id == dpir.Value)) return;

                if (spir.Value == dpir.Value) return;

                SelectionFilter sf = new SelectionFilter(new TypedValue[] { new TypedValue(0, "*TEXT") });
                PromptSelectionResult psr = ed.GetSelection(sf);
                if (psr.Status != PromptStatus.OK) return;

                using (Transaction tr = doc.Database.TransactionManager.StartTransaction())
                {
                    foreach (SelectedObject so in psr.Value)
                    {
                        DBObject d = tr.GetObject(so.ObjectId, OpenMode.ForWrite);
                        if (d == null) continue;

                        if (d is DBText txt)
                        {
                            string content = txt.TextString;
                            string converted = content.ConvertString(spir.Value, dpir.Value);
                            txt.TextString = converted;
                        }
                        else if (d is MText mtxt)
                        {
                            string content = mtxt.Contents;
                            string converted = content.ConvertString(spir.Value, dpir.Value);
                            mtxt.Contents = converted;
                        }
                    }

                    tr.Commit();
                }

                ed.WriteMessage("AJS-VNCONVERT from www.lisp.vn");
            }
        }
    }
}