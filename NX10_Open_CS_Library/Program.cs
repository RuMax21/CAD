using System;
using System.Collections.Generic;
using System.Collections; 
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NXOpen;
using NXOpenUI;
using NXOpen.UF;
using NXOpen.Utilities;
using NX10_Open_CS_Library;


public class Program
{
    // class members
    private static Session theSession;
    private static UI theUI;
    private static UFSession theUFSession;
    public static Program theProgram;
    public static bool isDisposeCalled;

    //------------------------------------------------------------------------------
    // Constructor
    //------------------------------------------------------------------------------
    public Program()
    {
        try
        {
            theSession = Session.GetSession();
            theUI = UI.GetUI();
            theUFSession = UFSession.GetUFSession();
                        isDisposeCalled = false;
        }
        catch (NXOpen.NXException ex)
        {
            // ---- Enter your exception handling code here -----
            UI.GetUI().NXMessageBox.Show("Message", NXMessageBox.DialogType.Error, ex.Message);
        }
    }

    //------------------------------------------------------------------------------
    //  Explicit Activation
    //      This entry point is used to activate the application explicitly
    //------------------------------------------------------------------------------
    public static int Main(string[] args)
    {
        theProgram = new Program();
        Form1 form = new Form1();
        form.Show();
        return 0;
    }
    public static void drawBolt(int diameter)
    {
        double lenghtBolt = 14, threadPitch = 0.8, f = 2 * threadPitch;
        double headDiameter = 8.5, k = 3.3;
        string valueChamfer = "0.9";
        switch (diameter)
        {
            case 5:
                lenghtBolt = 18;
                threadPitch = 0.8;
                valueChamfer = "0.9";
                headDiameter = 8.5;
                k = 3.3;
                break;
            case 6:
                lenghtBolt = 20;
                threadPitch = 1;
                valueChamfer = "1.1";
                headDiameter = 10;
                k = 3.9;
                break;
            case 8:
                lenghtBolt = 30;
                threadPitch = 1.2;
                valueChamfer = "1.4";
                headDiameter = 13;
                k = 5;
                break;
            case 10:
                lenghtBolt = 40;
                threadPitch = 1.6;
                f = 3;
                valueChamfer = "1.6";
                headDiameter = 16;
                k = 6;
                break;
            case 12:
                lenghtBolt = 45;
                threadPitch = 2;
                f = 3.6;
                valueChamfer = "1.9";
                headDiameter = 18;
                k = 7;
                break;
        }
        double radius = Convert.ToDouble(diameter) / 2, radiusBolt = headDiameter / 2;

        Tag UFPart1;
        string name1 = "bolt";
        int units1 = 1;
        try
        {
            theUFSession.Part.New(name1, units1, out UFPart1);
        }
        catch (Exception ex)
        {
            UI.GetUI().NXMessageBox.Show("Error", NXMessageBox.DialogType.Error, ex.Message);
        }

        UFCurve.Line line1 = new UFCurve.Line();
        UFCurve.Line line1_1 = new UFCurve.Line();
        UFCurve.Line line1_2 = new UFCurve.Line();
        UFCurve.Line line1_3 = new UFCurve.Line();
        UFCurve.Line line1_4 = new UFCurve.Line();
        UFCurve.Line line2 = new UFCurve.Line();
        UFCurve.Line line3 = new UFCurve.Line();
        UFCurve.Line line4 = new UFCurve.Line();
        UFCurve.Line line5 = new UFCurve.Line();
        UFCurve.Line line6 = new UFCurve.Line();

        line1.start_point = new double[3] { 0, 0, 0 };
        line1.end_point = new double[3] { -radiusBolt, 0, 0 };

        line2.start_point = new double[3] { -radiusBolt, 0, 0 };
        line2.end_point = new double[3] { -radiusBolt, k, 0 };

        line3.start_point = new double[3] { -radiusBolt, k, 0 };
        line3.end_point = new double[3] { -(radiusBolt - (radiusBolt - radius)), k, 0 };

        line4.start_point = new double[3] { -(radiusBolt - (radiusBolt - radius)), k, 0 };
        line4.end_point = new double[] { -(radiusBolt - (radiusBolt - radius)), lenghtBolt, 0 };

        line5.start_point = new double[3] { -(radiusBolt - (radiusBolt - radius)), lenghtBolt, 0 };
        line5.end_point = new double[3] { 0, lenghtBolt, 0 };

        line6.start_point = new double[3] { 0, lenghtBolt, 0 };
        line6.end_point = new double[3] { 0, 0, 0 };

        line1_1.start_point = new double[3] { 0 - threadPitch / 2, 0, 0 };
        line1_1.end_point = new double[3] { 0 - threadPitch / 2, f, 0 };

        line1_2.start_point = new double[3] { 0 - threadPitch / 2, f, 0 };
        line1_2.end_point = new double[3] { 0 + threadPitch / 2, f, 0 };

        line1_3.start_point = new double[3] { 0 + threadPitch / 2, f, 0 };
        line1_3.end_point = new double[3] { 0 + threadPitch / 2, 0, 0 };

        line1_4.start_point = new double[3] { 0 + threadPitch / 2, 0, 0 };
        line1_4.end_point = new double[3] { 0 - threadPitch / 2, 0, 0 };

        Tag[] objarray1 = new Tag[6];
        Tag[] objarray2 = new Tag[4];
        theUFSession.Curve.CreateLine(ref line1, out objarray1[0]);
        theUFSession.Curve.CreateLine(ref line2, out objarray1[1]);
        theUFSession.Curve.CreateLine(ref line3, out objarray1[2]);
        theUFSession.Curve.CreateLine(ref line4, out objarray1[3]);
        theUFSession.Curve.CreateLine(ref line5, out objarray1[4]);
        theUFSession.Curve.CreateLine(ref line6, out objarray1[5]);
        theUFSession.Curve.CreateLine(ref line1_1, out objarray2[0]);
        theUFSession.Curve.CreateLine(ref line1_2, out objarray2[1]);
        theUFSession.Curve.CreateLine(ref line1_3, out objarray2[2]);
        theUFSession.Curve.CreateLine(ref line1_4, out objarray2[3]);

        double[] ref_pt1 = new double[3] { 0, 0, 0 };
        double[] direction1 = { 0.00, 1.00, 0.00 };
        string[] limit1 = { "0", "360" };
        Tag[] features1;

        theUFSession.Modl.CreateRevolved(objarray1, limit1, ref_pt1, direction1, FeatureSigns.Nullsign, out features1);

        Tag feat = features1[0];
        Tag cyl_tag, obj_id_camf, obj_id_blend;
        Tag[] Edge_array_cyl, list1, list2, obj_id_extr;
        int ecount;

        theUFSession.Modl.AskFeatBody(feat, out cyl_tag);
        theUFSession.Modl.AskBodyEdges(cyl_tag, out Edge_array_cyl);
        theUFSession.Modl.AskListCount(Edge_array_cyl, out ecount);

        ArrayList arr_list1 = new ArrayList();
        ArrayList arr_list2 = new ArrayList();

        for (int i = 0; i < ecount; i++)
        {
            Tag edge;
            theUFSession.Modl.AskListItem(Edge_array_cyl, i, out edge);
            if (i == 2)
                arr_list1.Add(edge);
            if (i == 1)
                arr_list2.Add(edge);
        }

        list1 = (Tag[])arr_list1.ToArray(typeof(Tag));
        list2 = (Tag[])arr_list2.ToArray(typeof(Tag));

        theUFSession.Modl.CreateChamfer(1, valueChamfer, "", "45", list1, out obj_id_camf);
        theUFSession.Modl.CreateBlend(valueChamfer, list2, 0, 0, 0, 0, out obj_id_blend);

        double[] ref_pt = new double[3] { 0, 0, 0 };
        int StD = 0 - diameter;
        string[] limit = { StD.ToString(), diameter.ToString() };
        double[] direction = { 0.0, 0.0, 1.0 };
        theUFSession.Modl.CreateExtruded(objarray2, "0.0", limit, ref_pt, direction, FeatureSigns.Negative, out obj_id_extr);
    }

    //------------------------------------------------------------------------------
    // Following method disposes all the class members
    //------------------------------------------------------------------------------
    public void Dispose()
    {
        try
        {
            if (isDisposeCalled == false)
            {
                //TODO: Add your application code here 
            }
            isDisposeCalled = true;
        }
        catch (NXOpen.NXException ex)
        {
            // ---- Enter your exception handling code here -----

        }
    }

    public static int GetUnloadOption(string arg)
    {
        //Unloads the image explicitly, via an unload dialog
        //return System.Convert.ToInt32(Session.LibraryUnloadOption.Explicitly);

        //Unloads the image immediately after execution within NX
        // return System.Convert.ToInt32(Session.LibraryUnloadOption.Immediately);

        //Unloads the image when the NX session terminates
        return System.Convert.ToInt32(Session.LibraryUnloadOption.AtTermination);
    }

}

