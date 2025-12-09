using System;
using System.Web.UI.HtmlControls;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Windows;
using System.IO;
using System.Data;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.xml.simpleparser;
using iTextSharp.text.xml;
using iTextSharp.text.io;
using Bus_EReport;
using System.Net;
using DBase_EReport;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;


public partial class MasterFiles_rptDashboardSFE : System.Web.UI.Page
{
    DataSet dsDoctor = null;
    string sMode = string.Empty;
    string sf_code = string.Empty;
    string FMonth = string.Empty;
    string FYear = string.Empty;
    string TMonth = string.Empty;
    string TYear = string.Empty;
    string div_code = string.Empty;
    string sReturn = string.Empty;
    string Stok_code = string.Empty;
    string StName = string.Empty;
    double Value = 0.0;
    double Value_1 = 0.0;
    double Value_2 = 0.0;
    double Value_3 = 0.0;
    DataSet dsSalesForce = null;
    DataSet dsDoc = null;
    DataSet dsSal = new DataSet();
    string strFieledForceName = string.Empty;
    string sCurrentDate = string.Empty;
    DateTime dtCurrent;
    DataTable dtrowClr = new System.Data.DataTable();
    SalesForce dcrdoc = new SalesForce();

    string sHeading; int sReportType;
    string strCase = string.Empty;
    List<int> iLstVstCnt = new List<int>();
    List<int> iLstVstmnt = new List<int>();
    List<int> iLstVstyr = new List<int>();
    string tot = string.Empty;
    Double total;
    string mode = string.Empty;
    int Tomato2 = 0;
    int Yellow2 = 0;
    int LightGreen2 = 0;
    int LightPink2 = 0;
    int Aqua2 = 0;
    int Skyblue2 = 0;
    DataSet dsCat = null;
    DataSet dsClass = null;
    string strQry = string.Empty;
    DB_EReporting db_ER = new DB_EReporting();
    DataSet dsCatcount = null;
  

    protected void Page_Load(object sender, EventArgs e)
    {

        div_code = Session["div_code"].ToString();
        //sMode = Request.QueryString["cMode"].ToString();
        sf_code = Request.QueryString["sfcode"].ToString();
        FMonth = Request.QueryString["FMonth"].ToString();
        FYear = Request.QueryString["FYear"].ToString();

        strFieledForceName = Request.QueryString["sf_name"];
        mode = Request.QueryString["mode"];
        //Stok_code = Request.QueryString["Stok_code"].ToString();
        //StName = Request.QueryString["sk_Name"].ToString();
        SalesForce sf = new SalesForce();
        string strFrmMonth = sf.getMonthName(FMonth.Trim());

        if (mode == "1")
        {

            lblHead.Text = "SFE ( "+"<span style='color:red'>"+"Coverage"+"</span> )- Dash Board (at a Glance) for the Month of " + strFrmMonth + " " + FYear;
        }
        else if (mode == "2")
        {
            lblHead.Text = "SFE ( " + "<span style='color:red'>" + "Call Average " + "</span> ) - Dash Board (at a Glance) for the Month of " + strFrmMonth + " " + FYear;
        }

        else if (mode == "3")
        {
            lblHead.Text = "SFE ( " + "<span style='color:red'>" + "Missed Call  " + "</span> ) - Dash Board (at a Glance) for the Month of " + strFrmMonth + " " + FYear;
        }

        else if (mode == "4")
        {
            lblHead.Text = "SFE ( " + "<span style='color:red'>" + "No.of Days in Field  " + "</span> ) - Dash Board (at a Glance) for the Month of " + strFrmMonth + " " + FYear;
        }

        else if (mode == "5")
        {
            lblHead.Text = "SFE ( " + "<span style='color:red'>" + "Drs Visit - Categorywise " + "</span> )  - Dash Board (at a Glance) for the Month of " + strFrmMonth + " " + FYear;
        }

        else if (mode == "6")
        {
            lblHead.Text = "SFE ( " + "<span style='color:red'>" + "Drs Missed - Categorywise " + "</span> )  - Dash Board (at a Glance) for the Month of " + strFrmMonth + " " + FYear;
        }

        else if (mode == "7")
        {
            lblHead.Text = "SFE ( " + "<span style='color:red'>" + "Drs Visit - Classwise " + "</span> )  - Dash Board (at a Glance) for the Month of " + strFrmMonth + " " + FYear;
        }

        else if (mode == "8")
        {
            lblHead.Text = "SFE ( " + "<span style='color:red'>" + "Drs Visit - Frequencywise " + "</span> ) - Dash Board (at a Glance) for the Month of " + strFrmMonth + " " + FYear;
        }

        else if (mode == "9")
        {
            lblHead.Text = "SFE ( " + "<span style='color:red'>" + "Drs Missed - Frequencywise " + "</span> ) - Dash Board (at a Glance) for the Month of " + strFrmMonth + " " + FYear;
        }

        else if (mode == "10")
        {
            lblHead.Text = "SFE ( " + "<span style='color:red'>" + "Consolidated - Frequencywise " + "</span> ) - Dash Board (at a Glance) for the Month of " + strFrmMonth + " " + FYear;
        }


        LblForceName.Text = "Field Force Name : " + "<span style='color:black;'>" + strFieledForceName + "</span>";
        //lblstock.Text = "Stockist Name : " + "<span style='font-weight: bold;color:Red;'> " + StName + "</span>";

       

        if (mode == "5" || mode=="6" || mode =="8" || mode =="9")
        {

            strQry = "select Doc_Cat_Code, Doc_Cat_SName, Doc_Cat_SName + ' ( '+ cast(No_of_visit as varchar) + ' )' as  Name_visit from Mas_Doctor_Category where division_code='" + div_code + "' and Doc_Cat_Active_Flag=0";
            dsCat = db_ER.Exec_DataSet(strQry);

            strQry = "EXEC dash_SFE_CatCount '" + div_code + "','" + sf_code + "','" + FMonth + "','" + FYear + "','"+mode+"'";
            dsCatcount = db_ER.Exec_DataSet(strQry);
        }

        else if (mode == "7")
        {
            strQry = "select Doc_ClsCode as Doc_Cat_Code,Doc_ClsSName from Mas_Doc_Class where Division_Code='" + div_code + "' and Doc_Cls_ActiveFlag=0 ";
            dsCat = db_ER.Exec_DataSet(strQry);

            strQry = "EXEC dash_SFE_CatCount '" + div_code + "','" + sf_code + "','" + FMonth + "','" + FYear + "','" + mode + "'";
            dsCatcount = db_ER.Exec_DataSet(strQry);

        }

        else if (mode == "10")
        {
            tbl.Visible = false;
            pnlconsol.Visible = true;
            grdAppdashboard.Visible = false;
            strQry = "select Doc_Cat_Code, Doc_Cat_SName, Doc_Cat_SName + ' ( '+ cast(No_of_visit as varchar) + ' )' as  Name_visit from Mas_Doctor_Category where division_code='" + div_code + "' and Doc_Cat_Active_Flag=0";
            dsCat = db_ER.Exec_DataSet(strQry);

            strQry = "EXEC dash_SFE_CatCount '" + div_code + "','" + sf_code + "','" + FMonth + "','" + FYear + "','5'";
            dsCatcount = db_ER.Exec_DataSet(strQry);

            Consolidated(10);

            strQry = "EXEC dash_SFE_CatCount '" + div_code + "','" + sf_code + "','" + FMonth + "','" + FYear + "','6'";
            dsCatcount = db_ER.Exec_DataSet(strQry);

            Consolidated(11);

            strQry = "EXEC dash_SFE_CatCount '" + div_code + "','" + sf_code + "','" + FMonth + "','" + FYear + "','8'";
            dsCatcount = db_ER.Exec_DataSet(strQry);

            Consolidated(12);

            strQry = "EXEC dash_SFE_CatCount '" + div_code + "','" + sf_code + "','" + FMonth + "','" + FYear + "','9'";
            dsCatcount = db_ER.Exec_DataSet(strQry);

            Consolidated(13);

            strQry = "select Doc_ClsCode as Doc_Cat_Code,Doc_ClsSName from Mas_Doc_Class where Division_Code='" + div_code + "' and Doc_Cls_ActiveFlag=0 ";
            dsCat = db_ER.Exec_DataSet(strQry);

            strQry = "EXEC dash_SFE_CatCount '" + div_code + "','" + sf_code + "','" + FMonth + "','" + FYear + "','7'";
            dsCatcount = db_ER.Exec_DataSet(strQry);

            Consolidated(14);

            strQry = "EXEC dash_SFE_CatCount '" + div_code + "','" + sf_code + "','" + FMonth + "','" + FYear + "','15'";
            dsCatcount = db_ER.Exec_DataSet(strQry);

            Consolidated2(15);

            strQry = "EXEC dash_SFE_CatCount '" + div_code + "','" + sf_code + "','" + FMonth + "','" + FYear + "','16'";
            dsCatcount = db_ER.Exec_DataSet(strQry);

            Consolidated2(16);

            strQry = "EXEC dash_SFE_CatCount '" + div_code + "','" + sf_code + "','" + FMonth + "','" + FYear + "','17'";
            dsCatcount = db_ER.Exec_DataSet(strQry);

            Consolidated2(17);

            strQry = "EXEC dash_SFE_CatCount '" + div_code + "','" + sf_code + "','" + FMonth + "','" + FYear + "','18'";
            dsCatcount = db_ER.Exec_DataSet(strQry);

            Consolidated2(18);
        }


        FillReport();



        if (mode == "5" || mode =="6" || mode=="7" || mode =="8" || mode=="9")
        {

            Tomatocolor.Visible = false;
            Yellowcolor.Visible = false;
            LightGreencolor.Visible = false;
            LightPinkcolor.Visible = false;
            Aquacolor.Visible = false;
            SkyBluecolor.Visible = false;

            TextBox[] txt = new TextBox[dsCat.Tables[0].Rows.Count];
            Label[] lbl = new Label[dsCat.Tables[0].Rows.Count];

         

            //for (int m = 0; m < dsCat.Tables[0].Rows.Count; m++)
            //{
            //    lbl[m] = new Label();
            //    lbl[m].Text = dsCat.Tables[0].Rows[m]["Doc_Cat_SName"].ToString() + " Category";
            //    lbl[m].ForeColor = System.Drawing.Color.BlueViolet;

            //    LiteralControl tt = new LiteralControl();
            //    tt.Text = "&nbsp;";
            //    this.addtextbox.Controls.Add(lbl[m]);
            //    this.addtextbox.Controls.Add(tt);


            //    for (int k = 0; k < 4; k++)
            //    {
            //        txt[k] = new TextBox();
            //        LiteralControl tl3 = new LiteralControl();
            //        tl3.Text = "&nbsp;";

            //        if (k == 0)
            //        {
            //            txt[k].BackColor = System.Drawing.Color.Tomato;

            //            strQry = "EXEC dash_SFE_CatCount '" + div_code + "','" + dsCat.Tables[0].Rows[m]["Doc_Cat_Code"].ToString() + "','0','40','" + FMonth + "','" + FYear + "'";
            //            dsCatcount = db_ER.Exec_DataSet(strQry);
            //            txt[k].Text = dsCatcount.Tables[0].Rows[0]["Count"].ToString() + " ( 0 - 40 % )";
            //            txt[k].Style["text-align"] = "center";



            //        }
            //        else if (k == 1)
            //        {
            //            txt[k].BackColor = System.Drawing.Color.Yellow;
            //            strQry = "EXEC dash_SFE_CatCount '" + div_code + "','" + dsCat.Tables[0].Rows[m]["Doc_Cat_Code"].ToString() + "','41','60','" + FMonth + "','" + FYear + "'";
            //            dsCatcount = db_ER.Exec_DataSet(strQry);
            //            txt[k].Text = dsCatcount.Tables[0].Rows[0]["Count"].ToString() + " ( 41 - 60 % )";
            //            txt[k].Style["text-align"] = "center";

            //        }
            //        else if (k == 2)
            //        {
            //            txt[k].BackColor = System.Drawing.Color.LightGreen;
            //            strQry = "EXEC dash_SFE_CatCount '" + div_code + "','" + dsCat.Tables[0].Rows[m]["Doc_Cat_Code"].ToString() + "','61','80','" + FMonth + "','" + FYear + "'";
            //            dsCatcount = db_ER.Exec_DataSet(strQry);
            //            txt[k].Text = dsCatcount.Tables[0].Rows[0]["Count"].ToString() + " ( 61 - 80 % )";
            //            txt[k].Style["text-align"] = "center";

            //        }
            //        else if (k == 3)
            //        {
            //            txt[k].BackColor = System.Drawing.Color.LightPink;
            //            strQry = "EXEC dash_SFE_CatCount '" + div_code + "','" + dsCat.Tables[0].Rows[m]["Doc_Cat_Code"].ToString() + "','81','100','" + FMonth + "','" + FYear + "'";
            //            dsCatcount = db_ER.Exec_DataSet(strQry);
            //            txt[k].Text = dsCatcount.Tables[0].Rows[0]["Count"].ToString() + " ( 81 - 100 % )";
            //            txt[k].Style["text-align"] = "center";

            //        }

            //        txt[k].Font.Bold = true;
            //        txt[k].Width = 105;
            //        txt[k].Height = 25;
            //        txt[k].Enabled = false;
            //        this.addtextbox.Controls.Add(txt[k]);
            //        this.addtextbox.Controls.Add(tl3);
            //    }

            //    LiteralControl ty = new LiteralControl();
            //    ty.Text = "<br/>";
            //    LiteralControl tz = new LiteralControl();
            //    tz.Text = "<br/>";
            //    this.addtextbox.Controls.Add(ty);
            //    this.addtextbox.Controls.Add(tz);
            //}




            for (int i = 0, j = 0, k = 0, m = 0; i < dsCat.Tables[0].Rows.Count; i++, j++, k++, m++)
            {

                lbl[i] = new Label();
                lbl[i].Width = 150;
                if (mode == "5" || mode == "6" )
                {

                    lbl[i].Text = dsCat.Tables[0].Rows[m]["Doc_Cat_SName"].ToString() + " Category";
                }
                else if(mode == "7")
                {
                    lbl[i].Text = dsCat.Tables[0].Rows[m]["Doc_ClsSName"].ToString() + " Class";
                }

                else if (mode == "8" || mode=="9")
                {
                    lbl[i].Text = dsCat.Tables[0].Rows[m]["Name_visit"].ToString() + " Category";
                }

                lbl[i].ForeColor = System.Drawing.Color.Black;

                //LiteralControl ttt = new LiteralControl();
                //ttt.Text = "&nbsp;";
                //this.addtextbox.Controls.Add(lbl[m]);
                //this.addtextbox.Controls.Add(ttt);

               

                txt[i] = new TextBox();
                LiteralControl tl = new LiteralControl();
                tl.Text = "&nbsp;";
                LiteralControl tt = new LiteralControl();
                tt.Text = "&nbsp;";
                txt[i].BackColor = System.Drawing.Color.Tomato;
                txt[i].Width = 105;
                txt[i].Height = 25;
                txt[i].Font.Bold = true;
                txt[i].Enabled = false;

                //strQry = "EXEC dash_SFE_CatCount '" + div_code + "','" + dsCat.Tables[0].Rows[i]["Doc_Cat_Code"].ToString() + "','0','40','" + FMonth + "','" + FYear + "'";
                //dsCatcount = db_ER.Exec_DataSet(strQry);
                txt[j].Text = dsCatcount.Tables[0].Rows[i]["one"].ToString() + " ( 0 - 40 % )";
                txt[j].Style["text-align"] = "center";

                this.addtextbox.Controls.Add(lbl[i]);
                this.addtextbox.Controls.Add(tt);
                this.addtextbox.Controls.Add(txt[i]);
                this.addtextbox.Controls.Add(tl);

                txt[j] = new TextBox();
                LiteralControl tl2 = new LiteralControl();
                tl2.Text = "&nbsp;";
                txt[j].BackColor = System.Drawing.Color.Yellow;
                txt[j].Width = 105;
                txt[j].Height = 25;
                txt[j].Font.Bold = true;
                txt[j].Enabled = false;
                this.addtextbox.Controls.Add(txt[j]);
                this.addtextbox.Controls.Add(tl2);

                //strQry = "EXEC dash_SFE_CatCount '" + div_code + "','" + dsCat.Tables[0].Rows[j]["Doc_Cat_Code"].ToString() + "','41','60','" + FMonth + "','" + FYear + "'";
                //dsCatcount = db_ER.Exec_DataSet(strQry);
                txt[j].Text = dsCatcount.Tables[0].Rows[i]["two"].ToString() + " ( 41 - 60 % )";
                txt[j].Style["text-align"] = "center";


                txt[k] = new TextBox();
                LiteralControl tl3 = new LiteralControl();
                tl3.Text = "&nbsp;";
                txt[k].BackColor = System.Drawing.Color.LightGreen;
                txt[k].Width = 105;
                txt[k].Height = 25;
                txt[k].Font.Bold = true;
                txt[k].Enabled = false;
                this.addtextbox.Controls.Add(txt[k]);
                this.addtextbox.Controls.Add(tl3);

                //strQry = "EXEC dash_SFE_CatCount '" + div_code + "','" + dsCat.Tables[0].Rows[k]["Doc_Cat_Code"].ToString() + "','61','80','" + FMonth + "','" + FYear + "'";
                //dsCatcount = db_ER.Exec_DataSet(strQry);
                txt[k].Text = dsCatcount.Tables[0].Rows[i]["three"].ToString() + " ( 61 - 80 % )";
                txt[k].Style["text-align"] = "center";

                txt[m] = new TextBox();
                LiteralControl tl4 = new LiteralControl();
                tl4.Text = "&nbsp;";
                txt[m].BackColor = System.Drawing.Color.LightPink;
                txt[m].Width = 105;
                txt[m].Height = 25;
                txt[m].Font.Bold = true;
                txt[m].Enabled = false;
                this.addtextbox.Controls.Add(txt[m]);
                this.addtextbox.Controls.Add(tl4);

                //strQry = "EXEC dash_SFE_CatCount '" + div_code + "','" + dsCat.Tables[0].Rows[m]["Doc_Cat_Code"].ToString() + "','81','100','" + FMonth + "','" + FYear + "'";
                //dsCatcount = db_ER.Exec_DataSet(strQry);
                txt[m].Text = dsCatcount.Tables[0].Rows[i]["four"].ToString() + " ( 81 - 100 % )";
                txt[m].Style["text-align"] = "center";

                LiteralControl ty = new LiteralControl();
                ty.Text = "<br/>";
                LiteralControl tz = new LiteralControl();
                tz.Text = "<br/>";
                this.addtextbox.Controls.Add(ty);
                this.addtextbox.Controls.Add(tz);
            }

         
        }

    }

    private void FillReport()
    {
        DB_EReporting db_ER = new DB_EReporting();
        string strQry = string.Empty;

        DataSet dsAdm = null;

        if (mode == "5" || mode == "6" || mode =="7" || mode=="8" || mode=="9")
        {

            strQry = "EXEC Dashboard_SFE_Cat '" + div_code + "','" + sf_code + "','" + FMonth + "','" + FYear + "','" + mode + "' ";
            dsAdm = db_ER.Exec_DataSet(strQry);
        }
        else
        {

            strQry = "EXEC Dashboard_SFE '" + div_code + "','" + sf_code + "','" + FMonth + "','" + FYear + "','" + mode + "' ";
            dsAdm = db_ER.Exec_DataSet(strQry);
        }



        if (dsAdm.Tables[0].Rows.Count > 0)
        {

            grdAppdashboard.DataSource = dsAdm;
            grdAppdashboard.DataBind();
        }

        FillgridColor();
    }
    private void FillgridColor()
    {

        foreach (GridViewRow grid_row in grdAppdashboard.Rows)
        {

            Label lblBackColor = (Label)grid_row.FindControl("lblBackColor");
            string bcolor = "#" + lblBackColor.Text;
            grid_row.BackColor = System.Drawing.Color.FromName(bcolor);


        }
    }


    protected void grdAppdashboard_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            GridView objGridView = (GridView)sender;
            GridViewRow objgridviewrow = new GridViewRow(1, 0, DataControlRowType.Header, DataControlRowState.Insert);
            //Creating a table cell object
            TableCell objtablecell = new TableCell();

            GridViewRow objgridviewrow1 = new GridViewRow(2, 0, DataControlRowType.Header, DataControlRowState.Insert);
            //Creating a table cell object
            TableCell objtablecell1 = new TableCell();

            GridViewRow objgridviewrow2 = new GridViewRow(2, 0, DataControlRowType.Header, DataControlRowState.Insert);
            //Creating a table cell object
            TableCell objtablecell2 = new TableCell();


            AddMergedCells(objgridviewrow, objtablecell, 0, "#", "#F1F5F8", true);
            AddMergedCells(objgridviewrow, objtablecell, 0, "Field Force Name", "#F1F5F8", true);
            AddMergedCells(objgridviewrow, objtablecell, 0, "Designation", "#F1F5F8", true);
            AddMergedCells(objgridviewrow, objtablecell, 0, "HQ", "#F1F5F8", true);
            AddMergedCells(objgridviewrow, objtablecell, 0, "Date of Joining", "#F1F5F8", true);
            AddMergedCells(objgridviewrow, objtablecell, 0, "No.of Dr", "#F1F5F8", true);
            AddMergedCells(objgridviewrow, objtablecell, 0, "Drs Met", "#F1F5F8", true);
            AddMergedCells(objgridviewrow, objtablecell, 0, "Drs Seen", "#F1F5F8", true);
            AddMergedCells(objgridviewrow, objtablecell, 0, "No.of FW", "#F1F5F8", true);


            if (mode == "1")
            {
                AddMergedCells(objgridviewrow, objtablecell, 11, "Coverage (%) ", "#F1F5F8", true);

                AddMergedCells(objgridviewrow1, objtablecell1, 0, " 0 - 10	", "#D6E9C6", false);
                AddMergedCells(objgridviewrow1, objtablecell1, 0, " 11 - 20	", "#D6E9C6", false);
                AddMergedCells(objgridviewrow1, objtablecell1, 0, " 21 - 30 ", "#D6E9C6", false);
                AddMergedCells(objgridviewrow1, objtablecell1, 0, " 31 - 40	", "#D6E9C6", false);

                AddMergedCells(objgridviewrow1, objtablecell1, 0, "	41 - 50 ", "#D6E9C6", false);
                AddMergedCells(objgridviewrow1, objtablecell1, 0, " 51 - 60 ", "#D6E9C6", false);
                AddMergedCells(objgridviewrow1, objtablecell1, 0, " 61 - 70 ", "#D6E9C6", false);
                AddMergedCells(objgridviewrow1, objtablecell1, 0, " 71 - 80 ", "#D6E9C6", false);

                AddMergedCells(objgridviewrow1, objtablecell1, 0, " 81 - 90 ", "#D6E9C6", false);
                AddMergedCells(objgridviewrow1, objtablecell1, 0, " 91 - 95 ", "#D6E9C6", false);
                AddMergedCells(objgridviewrow1, objtablecell1, 0, " 96 - 100 ", "#D6E9C6", false);
            }
            else if (mode == "2")
            {
                AddMergedCells(objgridviewrow, objtablecell, 11, "Call Average ", "#F1F5F8", true);

                AddMergedCells(objgridviewrow1, objtablecell1, 0, " 0 - 5	", "#D6E9C6", false);
                AddMergedCells(objgridviewrow1, objtablecell1, 0, " 6 - 7	", "#D6E9C6", false);
                AddMergedCells(objgridviewrow1, objtablecell1, 0, " 8 - 9 ", "#D6E9C6", false);
                AddMergedCells(objgridviewrow1, objtablecell1, 0, " 10 - 11	", "#D6E9C6", false);

                AddMergedCells(objgridviewrow1, objtablecell1, 0, "	12 - 13 ", "#D6E9C6", false);
                AddMergedCells(objgridviewrow1, objtablecell1, 0, " 14 - 15 ", "#D6E9C6", false);
                AddMergedCells(objgridviewrow1, objtablecell1, 0, " > 15 ", "#D6E9C6", false);
               
            }
            else if (mode == "3")
            {
                AddMergedCells(objgridviewrow, objtablecell, 11, "Missed Call (%) ", "#F1F5F8", true);

              

                AddMergedCells(objgridviewrow1, objtablecell1, 0, " 0 - 10	", "#D6E9C6", false);
                AddMergedCells(objgridviewrow1, objtablecell1, 0, " 11 - 20	", "#D6E9C6", false);
                AddMergedCells(objgridviewrow1, objtablecell1, 0, " 21 - 30 ", "#D6E9C6", false);
                AddMergedCells(objgridviewrow1, objtablecell1, 0, " 31 - 40	", "#D6E9C6", false);

                AddMergedCells(objgridviewrow1, objtablecell1, 0, "	41 - 50 ", "#D6E9C6", false);
                AddMergedCells(objgridviewrow1, objtablecell1, 0, " > 50 ", "#D6E9C6", false);
            }

            else if (mode == "4")
            {
                AddMergedCells(objgridviewrow, objtablecell, 6, "Days in Field ", "#F1F5F8", true);



                AddMergedCells(objgridviewrow1, objtablecell1, 0, " 0 - 9	", "#D6E9C6", false);
                AddMergedCells(objgridviewrow1, objtablecell1, 0, " 10 - 15	", "#D6E9C6", false);
                AddMergedCells(objgridviewrow1, objtablecell1, 0, " 16 - 20 ", "#D6E9C6", false);
                AddMergedCells(objgridviewrow1, objtablecell1, 0, " 21 - 22	", "#D6E9C6", false);

                AddMergedCells(objgridviewrow1, objtablecell1, 0, "	23 - 24 ", "#D6E9C6", false);
                AddMergedCells(objgridviewrow1, objtablecell1, 0, " > 24 ", "#D6E9C6", false);
            }

            else if (mode == "5")
            {

                AddMergedCells(objgridviewrow, objtablecell, dsCat.Tables[0].Rows.Count*4, "Category wise Visit (%) ", "#F1F5F8", true);

                if (dsCat.Tables[0].Rows.Count > 0)
                {

                    for (int i = 0; i < dsCat.Tables[0].Rows.Count; i++)
                    {

                        AddMergedCells(objgridviewrow1, objtablecell1, 4, dsCat.Tables[0].Rows[i]["Doc_Cat_SName"].ToString(), "#F1F5F8", true);

                        AddMergedCells(objgridviewrow2, objtablecell2, 0, " 0 - 40 ", "#D6E9C6", false);
                        AddMergedCells(objgridviewrow2, objtablecell2, 0, " 41 - 60	", "#D6E9C6", false);
                        AddMergedCells(objgridviewrow2, objtablecell2, 0, " 61 - 80 ", "#D6E9C6", false);
                        AddMergedCells(objgridviewrow2, objtablecell2, 0, " 81 - 100 ", "#D6E9C6", false);
                    }
                }
            }

            else if (mode == "6")
            {

                AddMergedCells(objgridviewrow, objtablecell, dsCat.Tables[0].Rows.Count * 4, "Category wise Missed (%) ", "#F1F5F8", true);

                if (dsCat.Tables[0].Rows.Count > 0)
                {

                    for (int i = 0; i < dsCat.Tables[0].Rows.Count; i++)
                    {

                        AddMergedCells(objgridviewrow1, objtablecell1, 4, dsCat.Tables[0].Rows[i]["Doc_Cat_SName"].ToString(), "#F1F5F8", true);

                        AddMergedCells(objgridviewrow2, objtablecell2, 0, " 0 - 40 ", "#D6E9C6", false);
                        AddMergedCells(objgridviewrow2, objtablecell2, 0, " 41 - 60	", "#D6E9C6", false);
                        AddMergedCells(objgridviewrow2, objtablecell2, 0, " 61 - 80 ", "#D6E9C6", false);
                        AddMergedCells(objgridviewrow2, objtablecell2, 0, " 81 - 100 ", "#D6E9C6", false);
                    }
                }
            }

            else if (mode == "7")
            {

                AddMergedCells(objgridviewrow, objtablecell, dsCat.Tables[0].Rows.Count * 4, "Class wise Visit (%) ", "#F1F5F8", true);

                if (dsCat.Tables[0].Rows.Count > 0)
                {

                    for (int i = 0; i < dsCat.Tables[0].Rows.Count; i++)
                    {

                        AddMergedCells(objgridviewrow1, objtablecell1, 4, dsCat.Tables[0].Rows[i]["Doc_ClsSName"].ToString(), "#F1F5F8", true);

                        AddMergedCells(objgridviewrow2, objtablecell2, 0, " 0 - 40 ", "#D6E9C6", false);
                        AddMergedCells(objgridviewrow2, objtablecell2, 0, " 41 - 60	", "#D6E9C6", false);
                        AddMergedCells(objgridviewrow2, objtablecell2, 0, " 61 - 80 ", "#D6E9C6", false);
                        AddMergedCells(objgridviewrow2, objtablecell2, 0, " 81 - 100 ", "#D6E9C6", false);
                    }
                }
            }

            else if (mode == "8")
            {

                AddMergedCells(objgridviewrow, objtablecell, dsCat.Tables[0].Rows.Count * 4, "Frequency wise Visit (%) ", "#F1F5F8", true);

                if (dsCat.Tables[0].Rows.Count > 0)
                {

                    for (int i = 0; i < dsCat.Tables[0].Rows.Count; i++)
                    {

                        AddMergedCells(objgridviewrow1, objtablecell1, 4, dsCat.Tables[0].Rows[i]["Name_visit"].ToString(), "#F1F5F8", true);

                        AddMergedCells(objgridviewrow2, objtablecell2, 0, " 0 - 40 ", "#D6E9C6", false);
                        AddMergedCells(objgridviewrow2, objtablecell2, 0, " 41 - 60	", "#D6E9C6", false);
                        AddMergedCells(objgridviewrow2, objtablecell2, 0, " 61 - 80 ", "#D6E9C6", false);
                        AddMergedCells(objgridviewrow2, objtablecell2, 0, " 81 - 100 ", "#D6E9C6", false);
                    }
                }
            }




            else if (mode == "9")
            {

                AddMergedCells(objgridviewrow, objtablecell, dsCat.Tables[0].Rows.Count * 4, "Frequency wise Missed (%) ", "#F1F5F8", true);

                if (dsCat.Tables[0].Rows.Count > 0)
                {

                    for (int i = 0; i < dsCat.Tables[0].Rows.Count; i++)
                    {

                        AddMergedCells(objgridviewrow1, objtablecell1, 4, dsCat.Tables[0].Rows[i]["Name_visit"].ToString(), "#F1F5F8", true);

                        AddMergedCells(objgridviewrow2, objtablecell2, 0, " 0 - 40 ", "#D6E9C6", false);
                        AddMergedCells(objgridviewrow2, objtablecell2, 0, " 41 - 60	", "#D6E9C6", false);
                        AddMergedCells(objgridviewrow2, objtablecell2, 0, " 61 - 80 ", "#D6E9C6", false);
                        AddMergedCells(objgridviewrow2, objtablecell2, 0, " 81 - 100 ", "#D6E9C6", false);
                    }
                }
            }
          
         


            objGridView.Controls[0].Controls.AddAt(0, objgridviewrow);
            objGridView.Controls[0].Controls.AddAt(1, objgridviewrow1);
            objGridView.Controls[0].Controls.AddAt(2, objgridviewrow2);
        }
    }

    protected void AddMergedCells(GridViewRow objgridviewrow, TableCell objtablecell, int colspan, string celltext, string backcolor, bool bRowspan)
    {
        objtablecell = new TableCell();
        objtablecell.Text = celltext;
        objtablecell.ColumnSpan = colspan;
        if ((colspan == 0) && bRowspan)
        {
            objtablecell.RowSpan = 3;
        }
        objtablecell.Style.Add("background-color", backcolor);
        objtablecell.Style.Add("color", "black");
        objtablecell.Style.Add("font-weight", "bold");
        //objtablecell.Style.Add("BorderWidth", "1px");
        // objtablecell.Style.Add("BorderStyle", "solid");
        // objtablecell.Style.Add("BorderColor", "Black");

        objtablecell.HorizontalAlign = HorizontalAlign.Center;
        objtablecell.Wrap = false;
        objgridviewrow.Cells.Add(objtablecell);
    }

    protected void grdAppdashboard_Rowdatabound(object sender, GridViewRowEventArgs e)
    {

        if(mode =="1")
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //hLink.Text = e.Row.Cells[l].Text;
                //hLink.Text = Math.Round(double.Parse(e.Row.Cells[l].Text), 0).ToString();

                Label lblvalue = (Label)e.Row.FindControl("lblvalue");
                Label lbl1 = (Label)e.Row.FindControl("lbl1");
                Label lbl2 = (Label)e.Row.FindControl("lbl2");
                Label lbl3 = (Label)e.Row.FindControl("lbl3");
                Label lbl4 = (Label)e.Row.FindControl("lbl4");
                Label lbl5 = (Label)e.Row.FindControl("lbl5");
                Label lbl6 = (Label)e.Row.FindControl("lbl6");
                Label lbl7 = (Label)e.Row.FindControl("lbl7");
                Label lbl8 = (Label)e.Row.FindControl("lbl8");
                Label lbl9 = (Label)e.Row.FindControl("lbl9");
                Label lbl10 = (Label)e.Row.FindControl("lbl10");
                Label lbl11 = (Label)e.Row.FindControl("lbl11");


                e.Row.Cells[10].BackColor = System.Drawing.Color.White;
                e.Row.Cells[11].BackColor = System.Drawing.Color.White;
                e.Row.Cells[12].BackColor = System.Drawing.Color.White;
                e.Row.Cells[13].BackColor = System.Drawing.Color.White;
                e.Row.Cells[14].BackColor = System.Drawing.Color.White;
                e.Row.Cells[15].BackColor = System.Drawing.Color.White;
                e.Row.Cells[16].BackColor = System.Drawing.Color.White;
                e.Row.Cells[17].BackColor = System.Drawing.Color.White;
                e.Row.Cells[18].BackColor = System.Drawing.Color.White;
                e.Row.Cells[19].BackColor = System.Drawing.Color.White;
                e.Row.Cells[20].BackColor = System.Drawing.Color.White;


                if (lblvalue.Text != "")
                {

                    lblvalue.Text = Math.Round(double.Parse(lblvalue.Text), 0).ToString();

                    if (Convert.ToInt16(lblvalue.Text) >= 0 && Convert.ToInt16(lblvalue.Text) <= 10)
                    {
                        lbl1.Text = lblvalue.Text;

                        e.Row.Cells[10].BackColor = System.Drawing.Color.Tomato;

                        Tomato2 += 1;
                    }

                    if (Convert.ToInt16(lblvalue.Text) >= 11 && Convert.ToInt16(lblvalue.Text) <= 20)
                    {
                        lbl2.Text = lblvalue.Text;
                        e.Row.Cells[11].BackColor = System.Drawing.Color.Tomato;
                        Tomato2 += 1;
                    }

                    if (Convert.ToInt16(lblvalue.Text) >= 21 && Convert.ToInt16(lblvalue.Text) <= 30)
                    {
                        lbl3.Text = lblvalue.Text;
                        e.Row.Cells[12].BackColor = System.Drawing.Color.Tomato;
                        Tomato2 += 1;
                    }

                    if (Convert.ToInt16(lblvalue.Text) >= 31 && Convert.ToInt16(lblvalue.Text) <= 40)
                    {
                        lbl4.Text = lblvalue.Text;
                        e.Row.Cells[13].BackColor = System.Drawing.Color.Tomato;
                        Tomato2 += 1;
                    }

                    if (Convert.ToInt16(lblvalue.Text) >= 41 && Convert.ToInt16(lblvalue.Text) <= 50)
                    {
                        lbl5.Text = lblvalue.Text;
                        e.Row.Cells[14].BackColor = System.Drawing.Color.Tomato;
                        Tomato2 += 1;
                    }

                    if (Convert.ToInt16(lblvalue.Text) >= 51 && Convert.ToInt16(lblvalue.Text) <= 60)
                    {
                        lbl6.Text = lblvalue.Text;
                        e.Row.Cells[15].BackColor = System.Drawing.Color.Yellow;
                        Yellow2 += 1;
                    }

                    if (Convert.ToInt16(lblvalue.Text) >= 61 && Convert.ToInt16(lblvalue.Text) <= 70)
                    {
                        lbl7.Text = lblvalue.Text;
                        e.Row.Cells[16].BackColor = System.Drawing.Color.Yellow;
                        Yellow2 += 1;
                    }

                    if (Convert.ToInt16(lblvalue.Text) >= 71 && Convert.ToInt16(lblvalue.Text) <= 80)
                    {
                        lbl8.Text = lblvalue.Text;
                        e.Row.Cells[17].BackColor = System.Drawing.Color.LightGreen;
                        LightGreen2 += 1;
                    }

                    if (Convert.ToInt16(lblvalue.Text) >= 81 && Convert.ToInt16(lblvalue.Text) <= 90)
                    {
                        lbl9.Text = lblvalue.Text;
                        e.Row.Cells[18].BackColor = System.Drawing.Color.LightPink;
                        LightPink2 += 1;
                    }

                    if (Convert.ToInt16(lblvalue.Text) >= 91 && Convert.ToInt16(lblvalue.Text) <= 95)
                    {
                        lbl10.Text = lblvalue.Text;
                        e.Row.Cells[19].BackColor = System.Drawing.Color.Aqua;
                        Aqua2 += 1;
                    }

                    if (Convert.ToInt16(lblvalue.Text) >= 96 && Convert.ToInt16(lblvalue.Text) <= 100)
                    {
                        lbl11.Text = lblvalue.Text;
                        e.Row.Cells[20].BackColor = System.Drawing.Color.SkyBlue;
                        Skyblue2 += 1;
                    }

                    Tomato.Text = Tomato2.ToString() + " ( 0 - 50 % )";
                    Yellow.Text = Yellow2.ToString() + " ( 51 - 70 % )";
                    LightGreen.Text = LightGreen2.ToString() + " ( 71 - 80 % )";
                    LightPink.Text = LightPink2.ToString() + " ( 81 - 90 % )";
                    Aqua.Text = Aqua2.ToString() + " ( 91 - 95 % )";
                    SkyBlue.Text = Skyblue2.ToString() + " ( 96 - 100 % )";

                }
            }


        }

        else if (mode == "2")
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //hLink.Text = e.Row.Cells[l].Text;
                //hLink.Text = Math.Round(double.Parse(e.Row.Cells[l].Text), 0).ToString();

                Label lblvalue = (Label)e.Row.FindControl("lblvalue");
                Label lbl1 = (Label)e.Row.FindControl("lbl1");
                Label lbl2 = (Label)e.Row.FindControl("lbl2");
                Label lbl3 = (Label)e.Row.FindControl("lbl3");
                Label lbl4 = (Label)e.Row.FindControl("lbl4");
                Label lbl5 = (Label)e.Row.FindControl("lbl5");
                Label lbl6 = (Label)e.Row.FindControl("lbl6");
                Label lbl7 = (Label)e.Row.FindControl("lbl7");
                Label lbl8 = (Label)e.Row.FindControl("lbl8");
                Label lbl9 = (Label)e.Row.FindControl("lbl9");
                Label lbl10 = (Label)e.Row.FindControl("lbl10");
                Label lbl11 = (Label)e.Row.FindControl("lbl11");


                e.Row.Cells[10].BackColor = System.Drawing.Color.White;
                e.Row.Cells[11].BackColor = System.Drawing.Color.White;
                e.Row.Cells[12].BackColor = System.Drawing.Color.White;
                e.Row.Cells[13].BackColor = System.Drawing.Color.White;
                e.Row.Cells[14].BackColor = System.Drawing.Color.White;
                e.Row.Cells[15].BackColor = System.Drawing.Color.White;
                e.Row.Cells[16].BackColor = System.Drawing.Color.White;
                e.Row.Cells[17].Visible = false;
                e.Row.Cells[18].Visible = false;
                e.Row.Cells[19].Visible = false;
                e.Row.Cells[20].Visible = false;



                if (lblvalue.Text != "")
                {

                    lblvalue.Text = Math.Round(double.Parse(lblvalue.Text), 0).ToString();

                    if (Convert.ToInt16(lblvalue.Text) >= 0 && Convert.ToInt16(lblvalue.Text) <= 5)
                    {
                        lbl1.Text = lblvalue.Text;

                        e.Row.Cells[10].BackColor = System.Drawing.Color.Tomato;

                        Tomato2 += 1;
                    }

                    if (Convert.ToInt16(lblvalue.Text) >= 6 && Convert.ToInt16(lblvalue.Text) <= 7)
                    {
                        lbl2.Text = lblvalue.Text;
                        e.Row.Cells[11].BackColor = System.Drawing.Color.Tomato;
                        Tomato2 += 1;
                    }

                    if (Convert.ToInt16(lblvalue.Text) >= 8 && Convert.ToInt16(lblvalue.Text) <= 9)
                    {
                        lbl3.Text = lblvalue.Text;
                        e.Row.Cells[12].BackColor = System.Drawing.Color.Yellow;
                        Yellow2 += 1;
                    }

                    if (Convert.ToInt16(lblvalue.Text) >= 10 && Convert.ToInt16(lblvalue.Text) <= 11)
                    {
                        lbl4.Text = lblvalue.Text;
                        e.Row.Cells[13].BackColor = System.Drawing.Color.LightGreen;
                        LightGreen2 += 1;
                    }

                    if (Convert.ToInt16(lblvalue.Text) >= 12 && Convert.ToInt16(lblvalue.Text) <= 13)
                    {
                        lbl5.Text = lblvalue.Text;
                        e.Row.Cells[14].BackColor = System.Drawing.Color.LightPink;
                        LightPink2 += 1;
                    }

                    if (Convert.ToInt16(lblvalue.Text) >= 14 && Convert.ToInt16(lblvalue.Text) <= 15)
                    {
                        lbl6.Text = lblvalue.Text;
                        e.Row.Cells[15].BackColor = System.Drawing.Color.Aqua;
                        Aqua2 += 1;
                    }

                    if (Convert.ToInt16(lblvalue.Text) > 15)
                    {
                        lbl7.Text = lblvalue.Text;
                        e.Row.Cells[16].BackColor = System.Drawing.Color.SkyBlue; ;
                        Skyblue2 += 1;
                    }



                    Tomato.Text = Tomato2.ToString() + " ( 0 - 7 )";
                    Yellow.Text = Yellow2.ToString() + " ( 8 - 9 )";
                    LightGreen.Text = LightGreen2.ToString() + " ( 10 - 11 )";
                    LightPink.Text = LightPink2.ToString() + " ( 12 - 13 )";
                    Aqua.Text = Aqua2.ToString() + " ( 14 - 15 )";
                    SkyBlue.Text = Skyblue2.ToString() + " ( Above 15 )";

                }


            }
        }

        else if (mode == "3")
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //hLink.Text = e.Row.Cells[l].Text;
                //hLink.Text = Math.Round(double.Parse(e.Row.Cells[l].Text), 0).ToString();

                Label lblvalue = (Label)e.Row.FindControl("lblvalue");
                Label lbl1 = (Label)e.Row.FindControl("lbl1");
                Label lbl2 = (Label)e.Row.FindControl("lbl2");
                Label lbl3 = (Label)e.Row.FindControl("lbl3");
                Label lbl4 = (Label)e.Row.FindControl("lbl4");
                Label lbl5 = (Label)e.Row.FindControl("lbl5");
                Label lbl6 = (Label)e.Row.FindControl("lbl6");
                Label lbl7 = (Label)e.Row.FindControl("lbl7");
                Label lbl8 = (Label)e.Row.FindControl("lbl8");
                Label lbl9 = (Label)e.Row.FindControl("lbl9");
                Label lbl10 = (Label)e.Row.FindControl("lbl10");
                Label lbl11 = (Label)e.Row.FindControl("lbl11");


                e.Row.Cells[10].BackColor = System.Drawing.Color.White;
                e.Row.Cells[11].BackColor = System.Drawing.Color.White;
                e.Row.Cells[12].BackColor = System.Drawing.Color.White;
                e.Row.Cells[13].BackColor = System.Drawing.Color.White;
                e.Row.Cells[14].BackColor = System.Drawing.Color.White;
                e.Row.Cells[15].BackColor = System.Drawing.Color.White;
                e.Row.Cells[16].Visible = false;
                e.Row.Cells[17].Visible = false;
                e.Row.Cells[18].Visible = false;
                e.Row.Cells[19].Visible = false;
                e.Row.Cells[20].Visible = false;



                if (lblvalue.Text != "")
                {

                    lblvalue.Text = Math.Round(double.Parse(lblvalue.Text), 0).ToString();

                    if (Convert.ToInt16(lblvalue.Text) >= 0 && Convert.ToInt16(lblvalue.Text) <= 10)
                    {
                        lbl1.Text = lblvalue.Text;

                        e.Row.Cells[10].BackColor = System.Drawing.Color.SkyBlue;

                        Skyblue2 += 1;
                    }

                    if (Convert.ToInt16(lblvalue.Text) >= 11 && Convert.ToInt16(lblvalue.Text) <= 20)
                    {
                        lbl2.Text = lblvalue.Text;
                        e.Row.Cells[11].BackColor = System.Drawing.Color.Aqua;
                        Aqua2 += 1;
                    }

                    if (Convert.ToInt16(lblvalue.Text) >= 21 && Convert.ToInt16(lblvalue.Text) <= 30)
                    {
                        lbl3.Text = lblvalue.Text;
                        e.Row.Cells[12].BackColor = System.Drawing.Color.LightPink;
                        LightPink2 += 1;
                    }

                    if (Convert.ToInt16(lblvalue.Text) >= 31 && Convert.ToInt16(lblvalue.Text) <= 40)
                    {
                        lbl4.Text = lblvalue.Text;
                        e.Row.Cells[13].BackColor = System.Drawing.Color.LightGreen;
                        LightGreen2 += 1;
                    }

                    if (Convert.ToInt16(lblvalue.Text) >= 41 && Convert.ToInt16(lblvalue.Text) <= 50)
                    {
                        lbl5.Text = lblvalue.Text;
                        e.Row.Cells[14].BackColor = System.Drawing.Color.Yellow;
                        Yellow2 += 1;
                    }

                    if (Convert.ToInt16(lblvalue.Text) > 50)
                    {
                        lbl6.Text = lblvalue.Text;
                        e.Row.Cells[15].BackColor = System.Drawing.Color.Tomato;
                        Tomato2 += 1;
                    }



                    SkyBlue.Text = Skyblue2.ToString() + " ( 0 - 10 % )";
                    Aqua.Text = Aqua2.ToString() + " ( 11 - 20 % )";
                    LightPink.Text = LightPink2.ToString() + " ( 21 - 30 % )";
                    LightGreen.Text = LightGreen2.ToString() + " ( 31 - 40 % )";
                    Yellow.Text = Yellow2.ToString() + " ( 41 - 50 % )";
                    Tomato.Text = Tomato2.ToString() + " ( Above 50 % )";

                }


            }
        }

        else if (mode == "4")
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //hLink.Text = e.Row.Cells[l].Text;
                //hLink.Text = Math.Round(double.Parse(e.Row.Cells[l].Text), 0).ToString();

                Label lblvalue = (Label)e.Row.FindControl("lblvalue");
                Label lbl1 = (Label)e.Row.FindControl("lbl1");
                Label lbl2 = (Label)e.Row.FindControl("lbl2");
                Label lbl3 = (Label)e.Row.FindControl("lbl3");
                Label lbl4 = (Label)e.Row.FindControl("lbl4");
                Label lbl5 = (Label)e.Row.FindControl("lbl5");
                Label lbl6 = (Label)e.Row.FindControl("lbl6");
                Label lbl7 = (Label)e.Row.FindControl("lbl7");
                Label lbl8 = (Label)e.Row.FindControl("lbl8");
                Label lbl9 = (Label)e.Row.FindControl("lbl9");
                Label lbl10 = (Label)e.Row.FindControl("lbl10");
                Label lbl11 = (Label)e.Row.FindControl("lbl11");


                e.Row.Cells[10].BackColor = System.Drawing.Color.White;
                e.Row.Cells[11].BackColor = System.Drawing.Color.White;
                e.Row.Cells[12].BackColor = System.Drawing.Color.White;
                e.Row.Cells[13].BackColor = System.Drawing.Color.White;
                e.Row.Cells[14].BackColor = System.Drawing.Color.White;
                e.Row.Cells[15].BackColor = System.Drawing.Color.White;
                e.Row.Cells[16].Visible = false;
                e.Row.Cells[17].Visible = false;
                e.Row.Cells[18].Visible = false;
                e.Row.Cells[19].Visible = false;
                e.Row.Cells[20].Visible = false;


                if (lblvalue.Text != "")
                {

                    lblvalue.Text = Math.Round(double.Parse(lblvalue.Text), 0).ToString();

                    if (Convert.ToInt16(lblvalue.Text) >= 0 && Convert.ToInt16(lblvalue.Text) <= 9)
                    {
                        lbl1.Text = lblvalue.Text;

                        e.Row.Cells[10].BackColor = System.Drawing.Color.Tomato;

                        Tomato2 += 1;
                    }

                    if (Convert.ToInt16(lblvalue.Text) >= 10 && Convert.ToInt16(lblvalue.Text) <= 15)
                    {
                        lbl2.Text = lblvalue.Text;
                        e.Row.Cells[11].BackColor = System.Drawing.Color.Yellow;
                        Yellow2 += 1;
                    }

                    if (Convert.ToInt16(lblvalue.Text) >= 16 && Convert.ToInt16(lblvalue.Text) <= 20)
                    {
                        lbl3.Text = lblvalue.Text;
                        e.Row.Cells[12].BackColor = System.Drawing.Color.LightGreen;
                        LightGreen2 += 1;
                    }

                    if (Convert.ToInt16(lblvalue.Text) >= 21 && Convert.ToInt16(lblvalue.Text) <= 22)
                    {
                        lbl4.Text = lblvalue.Text;
                        e.Row.Cells[13].BackColor = System.Drawing.Color.LightPink;
                        LightPink2 += 1;
                    }

                    if (Convert.ToInt16(lblvalue.Text) >= 23 && Convert.ToInt16(lblvalue.Text) <= 24)
                    {
                        lbl5.Text = lblvalue.Text;
                        e.Row.Cells[14].BackColor = System.Drawing.Color.Aqua;
                        Aqua2 += 1;
                    }


                    if (Convert.ToInt16(lblvalue.Text) > 24)
                    {
                        lbl6.Text = lblvalue.Text;
                        e.Row.Cells[15].BackColor = System.Drawing.Color.SkyBlue;
                        Skyblue2 += 1;
                    }



                    Tomato.Text = Tomato2.ToString() + " ( 0 - 9 )";
                    Yellow.Text = Yellow2.ToString() + " ( 10 - 15 )";
                    LightGreen.Text = LightGreen2.ToString() + " ( 16 - 20 )";
                    LightPink.Text = LightPink2.ToString() + " ( 21 - 22 )";
                    Aqua.Text = Aqua2.ToString() + " ( 23 - 24 )";
                    SkyBlue.Text = Skyblue2.ToString() + " ( Above 24 )";

                }


            }
        }

        else if (mode =="5")
        {
            int count = 0;
            int cnt = 23;

            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                int rowcount = e.Row.RowIndex;

                Label lblsf_code = (Label)e.Row.FindControl("lblsf_code");

                Label lblvalue = (Label)e.Row.FindControl("lblvalue");

                e.Row.Cells[10].Visible = false;
                e.Row.Cells[11].Visible = false;
                e.Row.Cells[12].Visible = false;
                e.Row.Cells[13].Visible = false;
                e.Row.Cells[14].Visible = false;
                e.Row.Cells[15].Visible = false;
                e.Row.Cells[16].Visible = false;
                e.Row.Cells[17].Visible = false;
                e.Row.Cells[18].Visible = false;
                e.Row.Cells[19].Visible = false;
                e.Row.Cells[20].Visible = false;

                for (int i = 0; i < dsCat.Tables[0].Rows.Count; i++)
                {

                    for (int j = 0; j < 4; j++)
                    {
                       // e.Row.Cells[cnt].BackColor = System.Drawing.Color.White;
                        TableCell tbldynamic = new TableCell();
                        e.Row.Cells.Add(tbldynamic);
                        e.Row.Cells[cnt].BackColor = System.Drawing.Color.White;
                        if (lblvalue.Text != "")
                        {


                            string[] val = lblvalue.Text.Split(',');

                            int test = val.Count();

                            //if (test > i)
                            //{

                               

                                //string input = lblvalue.Text;
                                //string[] result = input.Split(new string[] { "," }, StringSplitOptions.None);
                                foreach (string s in val)
                                {
                                    string dd = s;

                                    string[] delt = dd.Split('~');

                                    if (delt[0] == dsCat.Tables[0].Rows[i]["Doc_Cat_Code"].ToString())
                                    {

                                     

                                //string code = val[i];

                                //string[] val2 = code.Split('~');


                                        if (delt[1] != "")
                                        {


                                            if (Convert.ToInt16(delt[1]) >= 0 && Convert.ToInt16(delt[1]) <= 40 && j == 0)
                                            {
                                                //lbl1.Text = lblvalue.Text;

                                                e.Row.Cells[cnt].BackColor = System.Drawing.Color.Tomato;

                                                Tomato2 += 1;
                                                tbldynamic.Text = delt[1];
                                            }

                                            if (Convert.ToInt16(delt[1]) >= 41 && Convert.ToInt16(delt[1]) <= 60 && j == 1)
                                            {
                                                // lbl2.Text = lblvalue.Text;
                                                e.Row.Cells[cnt].BackColor = System.Drawing.Color.Yellow;
                                                Yellow2 += 1;
                                                tbldynamic.Text = delt[1];
                                            }

                                            if (Convert.ToInt16(delt[1]) >= 61 && Convert.ToInt16(delt[1]) <= 80 && j == 2)
                                            {
                                                //lbl3.Text = lblvalue.Text;
                                                e.Row.Cells[cnt].BackColor = System.Drawing.Color.LightGreen;
                                                LightGreen2 += 1;
                                                tbldynamic.Text = delt[1];
                                            }

                                            if (Convert.ToInt16(delt[1]) >= 81 && Convert.ToInt16(delt[1]) <= 100 && j == 3)
                                            {
                                                //lbl4.Text = lblvalue.Text;
                                                e.Row.Cells[cnt].BackColor = System.Drawing.Color.LightPink;
                                                LightPink2 += 1;
                                                tbldynamic.Text = delt[1];
                                            }
                                        }

                                    }
                                }
                                //}

                           // }


                        }


                       // e.Row.Cells.Add(tbldynamic);
                        //if (rowcount > 19)
                        //{
                           // e.Row.Cells[19 + count].BackColor = System.Drawing.Color.White;
                            count++;
                        //}

                            cnt++;
                    }
                }
            }

            
        }

        else if (mode == "6")
        {
            int count = 0;
            int cnt = 23;

            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                int rowcount = e.Row.RowIndex;

                Label lblsf_code = (Label)e.Row.FindControl("lblsf_code");

                Label lblvalue = (Label)e.Row.FindControl("lblvalue");

                e.Row.Cells[10].Visible = false;
                e.Row.Cells[11].Visible = false;
                e.Row.Cells[12].Visible = false;
                e.Row.Cells[13].Visible = false;
                e.Row.Cells[14].Visible = false;
                e.Row.Cells[15].Visible = false;
                e.Row.Cells[16].Visible = false;
                e.Row.Cells[17].Visible = false;
                e.Row.Cells[18].Visible = false;
                e.Row.Cells[19].Visible = false;
                e.Row.Cells[20].Visible = false;


                for (int i = 0; i < dsCat.Tables[0].Rows.Count; i++)
                {

                    for (int j = 0; j < 4; j++)
                    {
                        // e.Row.Cells[cnt].BackColor = System.Drawing.Color.White;
                        TableCell tbldynamic = new TableCell();
                        e.Row.Cells.Add(tbldynamic);
                        e.Row.Cells[cnt].BackColor = System.Drawing.Color.White;
                        if (lblvalue.Text != "")
                        {


                            string[] val = lblvalue.Text.Split(',');

                            int test = val.Count();

                            //if (test > i)
                            //{



                            //string input = lblvalue.Text;
                            //string[] result = input.Split(new string[] { "," }, StringSplitOptions.None);
                            foreach (string s in val)
                            {
                                string dd = s;

                                string[] delt = dd.Split('~');

                                if (delt[0] == dsCat.Tables[0].Rows[i]["Doc_Cat_Code"].ToString())
                                {



                                    //string code = val[i];

                                    //string[] val2 = code.Split('~');


                                    if (delt[1] != "")
                                    {


                                        if (Convert.ToInt16(delt[1]) >= 0 && Convert.ToInt16(delt[1]) <= 40 && j == 0)
                                        {
                                            //lbl1.Text = lblvalue.Text;

                                            e.Row.Cells[cnt].BackColor = System.Drawing.Color.Tomato;

                                            Tomato2 += 1;
                                            tbldynamic.Text = delt[1];
                                        }

                                        if (Convert.ToInt16(delt[1]) >= 41 && Convert.ToInt16(delt[1]) <= 60 && j == 1)
                                        {
                                            // lbl2.Text = lblvalue.Text;
                                            e.Row.Cells[cnt].BackColor = System.Drawing.Color.Yellow;
                                            Yellow2 += 1;
                                            tbldynamic.Text = delt[1];
                                        }

                                        if (Convert.ToInt16(delt[1]) >= 61 && Convert.ToInt16(delt[1]) <= 80 && j == 2)
                                        {
                                            //lbl3.Text = lblvalue.Text;
                                            e.Row.Cells[cnt].BackColor = System.Drawing.Color.LightGreen;
                                            LightGreen2 += 1;
                                            tbldynamic.Text = delt[1];
                                        }

                                        if (Convert.ToInt16(delt[1]) >= 81 && Convert.ToInt16(delt[1]) <= 100 && j == 3)
                                        {
                                            //lbl4.Text = lblvalue.Text;
                                            e.Row.Cells[cnt].BackColor = System.Drawing.Color.LightPink;
                                            LightPink2 += 1;
                                            tbldynamic.Text = delt[1];
                                        }
                                    }

                                }
                            }
                            //}

                            // }


                        }


                        // e.Row.Cells.Add(tbldynamic);
                        //if (rowcount > 19)
                        //{
                        // e.Row.Cells[19 + count].BackColor = System.Drawing.Color.White;
                        count++;
                        //}

                        cnt++;
                    }
                }
            }


        }

        else if (mode == "7")
        {
            int count = 0;
            int cnt = 23;

            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                int rowcount = e.Row.RowIndex;

                Label lblsf_code = (Label)e.Row.FindControl("lblsf_code");

                Label lblvalue = (Label)e.Row.FindControl("lblvalue");

                e.Row.Cells[10].Visible = false;
                e.Row.Cells[11].Visible = false;
                e.Row.Cells[12].Visible = false;
                e.Row.Cells[13].Visible = false;
                e.Row.Cells[14].Visible = false;
                e.Row.Cells[15].Visible = false;
                e.Row.Cells[16].Visible = false;
                e.Row.Cells[17].Visible = false;
                e.Row.Cells[18].Visible = false;
                e.Row.Cells[19].Visible = false;
                e.Row.Cells[20].Visible = false;

                for (int i = 0; i < dsCat.Tables[0].Rows.Count; i++)
                {

                    for (int j = 0; j < 4; j++)
                    {
                        // e.Row.Cells[cnt].BackColor = System.Drawing.Color.White;
                        TableCell tbldynamic = new TableCell();
                        e.Row.Cells.Add(tbldynamic);
                        e.Row.Cells[cnt].BackColor = System.Drawing.Color.White;
                        if (lblvalue.Text != "")
                        {


                            string[] val = lblvalue.Text.Split(',');

                            int test = val.Count();

                            //if (test > i)
                            //{



                            //string input = lblvalue.Text;
                            //string[] result = input.Split(new string[] { "," }, StringSplitOptions.None);
                            foreach (string s in val)
                            {
                                string dd = s;

                                string[] delt = dd.Split('~');

                                if (delt[0] == dsCat.Tables[0].Rows[i]["Doc_Cat_Code"].ToString())
                                {



                                    //string code = val[i];

                                    //string[] val2 = code.Split('~');


                                    if (delt[1] != "")
                                    {


                                        if (Convert.ToInt16(delt[1]) >= 0 && Convert.ToInt16(delt[1]) <= 40 && j == 0)
                                        {
                                            //lbl1.Text = lblvalue.Text;

                                            e.Row.Cells[cnt].BackColor = System.Drawing.Color.Tomato;

                                            Tomato2 += 1;
                                            tbldynamic.Text = delt[1];
                                        }

                                        if (Convert.ToInt16(delt[1]) >= 41 && Convert.ToInt16(delt[1]) <= 60 && j == 1)
                                        {
                                            // lbl2.Text = lblvalue.Text;
                                            e.Row.Cells[cnt].BackColor = System.Drawing.Color.Yellow;
                                            Yellow2 += 1;
                                            tbldynamic.Text = delt[1];
                                        }

                                        if (Convert.ToInt16(delt[1]) >= 61 && Convert.ToInt16(delt[1]) <= 80 && j == 2)
                                        {
                                            //lbl3.Text = lblvalue.Text;
                                            e.Row.Cells[cnt].BackColor = System.Drawing.Color.LightGreen;
                                            LightGreen2 += 1;
                                            tbldynamic.Text = delt[1];
                                        }

                                        if (Convert.ToInt16(delt[1]) >= 81 && Convert.ToInt16(delt[1]) <= 100 && j == 3)
                                        {
                                            //lbl4.Text = lblvalue.Text;
                                            e.Row.Cells[cnt].BackColor = System.Drawing.Color.LightPink;
                                            LightPink2 += 1;
                                            tbldynamic.Text = delt[1];
                                        }
                                    }

                                }
                            }
                            //}

                            // }


                        }


                        // e.Row.Cells.Add(tbldynamic);
                        //if (rowcount > 19)
                        //{
                        // e.Row.Cells[19 + count].BackColor = System.Drawing.Color.White;
                        count++;
                        //}

                        cnt++;
                    }
                }
            }


        }

        else if (mode == "8")
        {
            int count = 0;
            int cnt = 23;

            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                int rowcount = e.Row.RowIndex;

                Label lblsf_code = (Label)e.Row.FindControl("lblsf_code");

                Label lblvalue = (Label)e.Row.FindControl("lblvalue");

                e.Row.Cells[10].Visible = false;
                e.Row.Cells[11].Visible = false;
                e.Row.Cells[12].Visible = false;
                e.Row.Cells[13].Visible = false;
                e.Row.Cells[14].Visible = false;
                e.Row.Cells[15].Visible = false;
                e.Row.Cells[16].Visible = false;
                e.Row.Cells[17].Visible = false;
                e.Row.Cells[18].Visible = false;
                e.Row.Cells[19].Visible = false;
                e.Row.Cells[20].Visible = false;

                for (int i = 0; i < dsCat.Tables[0].Rows.Count; i++)
                {

                    for (int j = 0; j < 4; j++)
                    {
                        // e.Row.Cells[cnt].BackColor = System.Drawing.Color.White;
                        TableCell tbldynamic = new TableCell();
                        e.Row.Cells.Add(tbldynamic);
                        e.Row.Cells[cnt].BackColor = System.Drawing.Color.White;
                        if (lblvalue.Text != "")
                        {


                            string[] val = lblvalue.Text.Split(',');

                            int test = val.Count();

                            //if (test > i)
                            //{



                            //string input = lblvalue.Text;
                            //string[] result = input.Split(new string[] { "," }, StringSplitOptions.None);
                            foreach (string s in val)
                            {
                                string dd = s;

                                string[] delt = dd.Split('~');

                                if (delt[0] == dsCat.Tables[0].Rows[i]["Doc_Cat_Code"].ToString())
                                {



                                    //string code = val[i];

                                    //string[] val2 = code.Split('~');


                                    if (delt[1] != "")
                                    {


                                        if (Convert.ToInt16(delt[1]) >= 0 && Convert.ToInt16(delt[1]) <= 40 && j == 0)
                                        {
                                            //lbl1.Text = lblvalue.Text;

                                            e.Row.Cells[cnt].BackColor = System.Drawing.Color.Tomato;

                                            Tomato2 += 1;
                                            tbldynamic.Text = delt[1];
                                        }

                                        if (Convert.ToInt16(delt[1]) >= 41 && Convert.ToInt16(delt[1]) <= 60 && j == 1)
                                        {
                                            // lbl2.Text = lblvalue.Text;
                                            e.Row.Cells[cnt].BackColor = System.Drawing.Color.Yellow;
                                            Yellow2 += 1;
                                            tbldynamic.Text = delt[1];
                                        }

                                        if (Convert.ToInt16(delt[1]) >= 61 && Convert.ToInt16(delt[1]) <= 80 && j == 2)
                                        {
                                            //lbl3.Text = lblvalue.Text;
                                            e.Row.Cells[cnt].BackColor = System.Drawing.Color.LightGreen;
                                            LightGreen2 += 1;
                                            tbldynamic.Text = delt[1];
                                        }

                                        if (Convert.ToInt16(delt[1]) >= 81 && Convert.ToInt16(delt[1]) <= 100 && j == 3)
                                        {
                                            //lbl4.Text = lblvalue.Text;
                                            e.Row.Cells[cnt].BackColor = System.Drawing.Color.LightPink;
                                            LightPink2 += 1;
                                            tbldynamic.Text = delt[1];
                                        }
                                    }

                                }
                            }
                            //}

                            // }


                        }


                        // e.Row.Cells.Add(tbldynamic);
                        //if (rowcount > 19)
                        //{
                        // e.Row.Cells[19 + count].BackColor = System.Drawing.Color.White;
                        count++;
                        //}

                        cnt++;
                    }
                }
            }


        }

        else if (mode == "9")
        {
            int count = 0;
            int cnt = 23;

            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                int rowcount = e.Row.RowIndex;

                Label lblsf_code = (Label)e.Row.FindControl("lblsf_code");

                Label lblvalue = (Label)e.Row.FindControl("lblvalue");

                e.Row.Cells[10].Visible = false;
                e.Row.Cells[11].Visible = false;
                e.Row.Cells[12].Visible = false;
                e.Row.Cells[13].Visible = false;
                e.Row.Cells[14].Visible = false;
                e.Row.Cells[15].Visible = false;
                e.Row.Cells[16].Visible = false;
                e.Row.Cells[17].Visible = false;
                e.Row.Cells[18].Visible = false;
                e.Row.Cells[19].Visible = false;
                e.Row.Cells[20].Visible = false;

                for (int i = 0; i < dsCat.Tables[0].Rows.Count; i++)
                {

                    for (int j = 0; j < 4; j++)
                    {
                        // e.Row.Cells[cnt].BackColor = System.Drawing.Color.White;
                        TableCell tbldynamic = new TableCell();
                        e.Row.Cells.Add(tbldynamic);
                        e.Row.Cells[cnt].BackColor = System.Drawing.Color.White;
                        if (lblvalue.Text != "")
                        {


                            string[] val = lblvalue.Text.Split(',');

                            int test = val.Count();

                            //if (test > i)
                            //{



                            //string input = lblvalue.Text;
                            //string[] result = input.Split(new string[] { "," }, StringSplitOptions.None);
                            foreach (string s in val)
                            {
                                string dd = s;

                                string[] delt = dd.Split('~');

                                if (delt[0] == dsCat.Tables[0].Rows[i]["Doc_Cat_Code"].ToString())
                                {



                                    //string code = val[i];

                                    //string[] val2 = code.Split('~');


                                    if (delt[1] != "")
                                    {


                                        if (Convert.ToInt16(delt[1]) >= 0 && Convert.ToInt16(delt[1]) <= 40 && j == 0)
                                        {
                                            //lbl1.Text = lblvalue.Text;

                                            e.Row.Cells[cnt].BackColor = System.Drawing.Color.Tomato;

                                            Tomato2 += 1;
                                            tbldynamic.Text = delt[1];
                                        }

                                        if (Convert.ToInt16(delt[1]) >= 41 && Convert.ToInt16(delt[1]) <= 60 && j == 1)
                                        {
                                            // lbl2.Text = lblvalue.Text;
                                            e.Row.Cells[cnt].BackColor = System.Drawing.Color.Yellow;
                                            Yellow2 += 1;
                                            tbldynamic.Text = delt[1];
                                        }

                                        if (Convert.ToInt16(delt[1]) >= 61 && Convert.ToInt16(delt[1]) <= 80 && j == 2)
                                        {
                                            //lbl3.Text = lblvalue.Text;
                                            e.Row.Cells[cnt].BackColor = System.Drawing.Color.LightGreen;
                                            LightGreen2 += 1;
                                            tbldynamic.Text = delt[1];
                                        }

                                        if (Convert.ToInt16(delt[1]) >= 81 && Convert.ToInt16(delt[1]) <= 100 && j == 3)
                                        {
                                            //lbl4.Text = lblvalue.Text;
                                            e.Row.Cells[cnt].BackColor = System.Drawing.Color.LightPink;
                                            LightPink2 += 1;
                                            tbldynamic.Text = delt[1];
                                        }
                                    }

                                }
                            }
                            //}

                            // }


                        }


                        // e.Row.Cells.Add(tbldynamic);
                        //if (rowcount > 19)
                        //{
                        // e.Row.Cells[19 + count].BackColor = System.Drawing.Color.White;
                        count++;
                        //}

                        cnt++;
                    }
                }
            }


        }
    }

    private void column_count()
    {
        foreach (GridViewRow row in grdAppdashboard.Rows)
        {
            var clncount = grdAppdashboard.Columns.Count;
        }   
    
    }


    private void Consolidated(int consol)
    {

        if (mode == "10")
        {

            Tomatocolor.Visible = false;
            Yellowcolor.Visible = false;
            LightGreencolor.Visible = false;
            LightPinkcolor.Visible = false;
            Aquacolor.Visible = false;
            SkyBluecolor.Visible = false;

            TextBox[] txt = new TextBox[dsCat.Tables[0].Rows.Count];
            Label[] lbl = new Label[dsCat.Tables[0].Rows.Count];

           


            for (int i = 0, j = 0, k = 0, m = 0; i < dsCat.Tables[0].Rows.Count; i++, j++, k++, m++)
            {

                lbl[i] = new Label();
                if (consol == 10 || consol ==11)
                {

                    lbl[i].Text = dsCat.Tables[0].Rows[m]["Doc_Cat_SName"].ToString().Replace(" ","") + " Category";
                }
                else if (consol == 14)
                {
                    lbl[i].Text = dsCat.Tables[0].Rows[m]["Doc_ClsSName"].ToString().Replace(" ", "") + " Class";
                }

                else if (consol == 12 || consol == 13)
                {
                    lbl[i].Text = dsCat.Tables[0].Rows[m]["Name_visit"].ToString().Replace(" ", "") + " Category";
                }

                lbl[i].ForeColor = System.Drawing.Color.Black;
                lbl[i].Width = 150;
               

                //LiteralControl ttt = new LiteralControl();
                //ttt.Text = "&nbsp;";
                //this.addtextbox.Controls.Add(lbl[m]);
                //this.addtextbox.Controls.Add(ttt);



                txt[i] = new TextBox();
                LiteralControl tl = new LiteralControl();
                tl.Text = "&nbsp;";
                LiteralControl tt = new LiteralControl();
                tt.Text = "&nbsp;";
              
                txt[i].Width = 90;
                txt[i].Height = 30;
                txt[i].Font.Bold = false;
                txt[i].Enabled = false;
                txt[i].Font.Size = 9;

                //strQry = "EXEC dash_SFE_CatCount '" + div_code + "','" + dsCat.Tables[0].Rows[i]["Doc_Cat_Code"].ToString() + "','0','40','" + FMonth + "','" + FYear + "'";
                //dsCatcount = db_ER.Exec_DataSet(strQry);
                txt[j].Text = dsCatcount.Tables[0].Rows[i]["one"].ToString() + " (0 - 40 %)";
                txt[j].Style["text-align"] = "center";
                txt[j].Attributes["class"] = "txtbox";

                if (consol == 10)
                {
                    txt[i].BackColor = System.Drawing.Color.Wheat;

                    this.addtex_Consol1.Controls.Add(lbl[i]);
                    this.addtex_Consol1.Controls.Add(tt);
                    this.addtex_Consol1.Controls.Add(txt[i]);
                    this.addtex_Consol1.Controls.Add(tl);
                }
                else if (consol == 11)
                {
                    txt[i].BackColor = System.Drawing.Color.Wheat;
                    this.addtex_Consol2.Controls.Add(lbl[i]);
                    this.addtex_Consol2.Controls.Add(tt);
                    this.addtex_Consol2.Controls.Add(txt[i]);
                    this.addtex_Consol2.Controls.Add(tl);
                }

                else if (consol == 12)
                {
                    txt[i].BackColor = System.Drawing.Color.LavenderBlush;
                    this.addtex_Consol3.Controls.Add(lbl[i]);
                    this.addtex_Consol3.Controls.Add(tt);
                    this.addtex_Consol3.Controls.Add(txt[i]);
                    this.addtex_Consol3.Controls.Add(tl);
                }

                else if (consol == 13)
                {
                    txt[i].BackColor = System.Drawing.Color.LavenderBlush;
                    this.addtex_Consol4.Controls.Add(lbl[i]);
                    this.addtex_Consol4.Controls.Add(tt);
                    this.addtex_Consol4.Controls.Add(txt[i]);
                    this.addtex_Consol4.Controls.Add(tl);
                }

                else if (consol == 14)
                {

                    txt[i].BackColor = System.Drawing.Color.Moccasin;
                    this.addtex_Consol5.Controls.Add(lbl[i]);
                    this.addtex_Consol5.Controls.Add(tt);
                    this.addtex_Consol5.Controls.Add(txt[i]);
                    this.addtex_Consol5.Controls.Add(tl);
                }

                txt[j] = new TextBox();
                LiteralControl tl2 = new LiteralControl();
                tl2.Text = "&nbsp;";
               
                txt[j].Width = 90;
                txt[j].Height = 30;
                txt[j].Font.Bold = false;
                txt[j].Enabled = false;
                txt[j].Font.Size = 9;

                if (consol == 10)
                {
                    txt[j].BackColor = System.Drawing.Color.Turquoise;
                    this.addtex_Consol1.Controls.Add(txt[j]);
                    this.addtex_Consol1.Controls.Add(tl2);
                }

                else if (consol == 11)
                {
                    txt[j].BackColor = System.Drawing.Color.Turquoise;
                    this.addtex_Consol2.Controls.Add(txt[j]);
                    this.addtex_Consol2.Controls.Add(tl2);
                }

                else if (consol == 12)
                {
                    txt[j].BackColor = System.Drawing.Color.PaleGoldenrod;
                    this.addtex_Consol3.Controls.Add(txt[j]);
                    this.addtex_Consol3.Controls.Add(tl2);
                }

                else if (consol == 13)
                {
                    txt[j].BackColor = System.Drawing.Color.PaleGoldenrod;
                    this.addtex_Consol4.Controls.Add(txt[j]);
                    this.addtex_Consol4.Controls.Add(tl2);
                }

                else if (consol == 14)
                {
                    txt[j].BackColor = System.Drawing.Color.Aquamarine;
                    this.addtex_Consol5.Controls.Add(txt[j]);
                    this.addtex_Consol5.Controls.Add(tl2);
                }

                //strQry = "EXEC dash_SFE_CatCount '" + div_code + "','" + dsCat.Tables[0].Rows[j]["Doc_Cat_Code"].ToString() + "','41','60','" + FMonth + "','" + FYear + "'";
                //dsCatcount = db_ER.Exec_DataSet(strQry);
                txt[j].Text = dsCatcount.Tables[0].Rows[i]["two"].ToString() + " (41 - 60 %)";
                txt[j].Style["text-align"] = "center";
                txt[j].Attributes["class"] = "txtbox";


                txt[k] = new TextBox();
                LiteralControl tl3 = new LiteralControl();
                tl3.Text = "&nbsp;";
            
                txt[k].Width = 90;
                txt[k].Height = 30;
                txt[k].Font.Bold = false;
                txt[k].Enabled = false;
                txt[k].Font.Size = 9;


                if (consol == 10)
                {
                    txt[k].BackColor = System.Drawing.Color.Honeydew;
                    this.addtex_Consol1.Controls.Add(txt[k]);
                    this.addtex_Consol1.Controls.Add(tl3);

                }
                else if (consol == 11)
                {
                    txt[k].BackColor = System.Drawing.Color.Honeydew;
                    this.addtex_Consol2.Controls.Add(txt[k]);
                    this.addtex_Consol2.Controls.Add(tl3);
                }

                else if (consol == 12)
                {
                    txt[k].BackColor = System.Drawing.Color.Lavender;
                    this.addtex_Consol3.Controls.Add(txt[k]);
                    this.addtex_Consol3.Controls.Add(tl3);
                }

                else if (consol == 13)
                {
                    txt[k].BackColor = System.Drawing.Color.Lavender;
                    this.addtex_Consol4.Controls.Add(txt[k]);
                    this.addtex_Consol4.Controls.Add(tl3);
                }

                else if (consol == 14)
                {
                    txt[k].BackColor = System.Drawing.Color.Lavender;
                    this.addtex_Consol5.Controls.Add(txt[k]);
                    this.addtex_Consol5.Controls.Add(tl3);
                }


                //strQry = "EXEC dash_SFE_CatCount '" + div_code + "','" + dsCat.Tables[0].Rows[k]["Doc_Cat_Code"].ToString() + "','61','80','" + FMonth + "','" + FYear + "'";
                //dsCatcount = db_ER.Exec_DataSet(strQry);
                txt[k].Text = dsCatcount.Tables[0].Rows[i]["three"].ToString() + " (61 - 80 %)";
                txt[k].Style["text-align"] = "center";
                txt[k].Attributes["class"] = "txtbox";

                txt[m] = new TextBox();
                LiteralControl tl4 = new LiteralControl();
                tl4.Text = "&nbsp;";
              
                txt[m].Width = 90;
                txt[m].Height = 30;
                txt[m].Font.Bold = false;
                txt[m].Enabled = false;
                txt[m].Font.Size = 9;

                if (consol == 10)
                {
                    txt[m].BackColor = System.Drawing.Color.MistyRose;
                    this.addtex_Consol1.Controls.Add(txt[m]);
                    this.addtex_Consol1.Controls.Add(tl4);
                }

                else if (consol == 11)
                {
                    txt[m].BackColor = System.Drawing.Color.MistyRose;
                    this.addtex_Consol2.Controls.Add(txt[m]);
                    this.addtex_Consol2.Controls.Add(tl4);
                }

                else if (consol == 12)
                {
                    this.addtex_Consol3.Controls.Add(txt[m]);
                    this.addtex_Consol3.Controls.Add(tl4);
                }

                else if (consol == 13)
                {
                    this.addtex_Consol4.Controls.Add(txt[m]);
                    this.addtex_Consol4.Controls.Add(tl4);
                }

                else if (consol == 14)
                {
                    this.addtex_Consol5.Controls.Add(txt[m]);
                    this.addtex_Consol5.Controls.Add(tl4);
                }

                //strQry = "EXEC dash_SFE_CatCount '" + div_code + "','" + dsCat.Tables[0].Rows[m]["Doc_Cat_Code"].ToString() + "','81','100','" + FMonth + "','" + FYear + "'";
                //dsCatcount = db_ER.Exec_DataSet(strQry);
                txt[m].Text = dsCatcount.Tables[0].Rows[i]["four"].ToString() + " (81 - 100 %)";
                txt[m].Style["text-align"] = "center";
                txt[m].Attributes["class"] = "txtbox";

                LiteralControl ty = new LiteralControl();
                ty.Text = "<br/>";
                LiteralControl tz = new LiteralControl();
                tz.Text = "<br/>";

                if (consol == 10)
                {

                    this.addtex_Consol1.Controls.Add(ty);
                    this.addtex_Consol1.Controls.Add(tz);
                }

                else if (consol == 11)
                {
                    this.addtex_Consol2.Controls.Add(ty);
                    this.addtex_Consol2.Controls.Add(tz);
                }

                else if (consol == 12)
                {
                    this.addtex_Consol3.Controls.Add(ty);
                    this.addtex_Consol3.Controls.Add(tz);
                }

                else if (consol == 13)
                {
                    this.addtex_Consol4.Controls.Add(ty);
                    this.addtex_Consol4.Controls.Add(tz);
                }

                else if (consol == 14)
                {
                    this.addtex_Consol5.Controls.Add(ty);
                    this.addtex_Consol5.Controls.Add(tz);
                }
            }


        }

    }

    private void Consolidated2(int consol)
    {
        TextBox[] txt = new TextBox[6];
        //Label[] lbl = new Label[6];

        LiteralControl[] tl3 = new LiteralControl[6];
        
       

  


            //for (int k = 1; k <= 6; k++)
            //{
                txt[0] = new TextBox();
                txt[1] = new TextBox();
                txt[2] = new TextBox();
                txt[3] = new TextBox();
                txt[4] = new TextBox();
                txt[5] = new TextBox();

                tl3[0] = new LiteralControl();
                tl3[1] = new LiteralControl();
                tl3[2] = new LiteralControl();
                tl3[3] = new LiteralControl();
                tl3[4] = new LiteralControl();
                tl3[5] = new LiteralControl();
               
                tl3[0].Text = "&nbsp;";
                tl3[1].Text = "&nbsp;";
                tl3[2].Text = "&nbsp;";
                tl3[3].Text = "&nbsp;";
                tl3[4].Text = "&nbsp;";
                tl3[5].Text = "&nbsp;";

                txt[0].Attributes["class"] = "txtbox";
                txt[1].Attributes["class"] = "txtbox";
                txt[2].Attributes["class"] = "txtbox";
                txt[3].Attributes["class"] = "txtbox";
                txt[4].Attributes["class"] = "txtbox";
                txt[5].Attributes["class"] = "txtbox";

                txt[0].Font.Size = 9;
                txt[1].Font.Size = 9;
                txt[2].Font.Size = 9;
                txt[3].Font.Size = 9;
                txt[4].Font.Size = 9;
                txt[5].Font.Size = 9;


                    txt[0].Style["text-align"] = "center";
                    txt[1].Style["text-align"] = "center";
                    txt[2].Style["text-align"] = "center";
                    txt[3].Style["text-align"] = "center";
                    txt[4].Style["text-align"] = "center";
                    txt[5].Style["text-align"] = "center";

         
                txt[0].Font.Bold = true;
                txt[0].Width = 100;
                txt[0].Height = 30;
                txt[0].Enabled = false;

                if (consol == 15)
                {

                    txt[0].BackColor = System.Drawing.Color.MintCream;
                    txt[0].Text = dsCatcount.Tables[0].Rows[0]["one"].ToString() + " (0 - 50 %)";

                    this.addtex_Consol6.Controls.Add(txt[0]);
                    this.addtex_Consol6.Controls.Add(tl3[0]);
                }

                else if (consol == 16)
                {
                    txt[0].BackColor = System.Drawing.Color.PaleGoldenrod;
                    txt[0].Text = dsCatcount.Tables[0].Rows[0]["one"].ToString() + " (0 - 7)";

                    this.addtex_Consol7.Controls.Add(txt[0]);
                    this.addtex_Consol7.Controls.Add(tl3[0]);
                }

                else if (consol == 17)
                {
                    txt[0].BackColor = System.Drawing.Color.LemonChiffon;
                    txt[0].Text = dsCatcount.Tables[0].Rows[0]["one"].ToString() + " (0 - 10 %)";

                    this.addtex_Consol8.Controls.Add(txt[0]);
                    this.addtex_Consol8.Controls.Add(tl3[0]);
                }

                else if (consol == 18)
                {
                    txt[0].BackColor = System.Drawing.Color.LightCyan;
                    txt[0].Text = dsCatcount.Tables[0].Rows[0]["one"].ToString() + " (0 - 9)";

                    this.addtex_Consol9.Controls.Add(txt[0]);
                    this.addtex_Consol9.Controls.Add(tl3[0]);
                }

                txt[1].Font.Bold = true;
                txt[1].Width = 100;
                txt[1].Height = 30;
                txt[1].Enabled = false;
                if (consol == 15)
                {
                    txt[1].BackColor = System.Drawing.Color.Yellow;
                    txt[1].Text = dsCatcount.Tables[0].Rows[0]["two"].ToString() + " ( 51 - 70 % )";

                    this.addtex_Consol6.Controls.Add(txt[1]);
                    this.addtex_Consol6.Controls.Add(tl3[1]);
                }
                else if (consol == 16)
                {
                    txt[1].BackColor = System.Drawing.Color.LavenderBlush;
                    txt[1].Text = dsCatcount.Tables[0].Rows[0]["two"].ToString() + " ( 8 - 9 )";

                    this.addtex_Consol7.Controls.Add(txt[1]);
                    this.addtex_Consol7.Controls.Add(tl3[1]);
                }

                else if (consol == 17)
                {
                    txt[1].BackColor = System.Drawing.Color.PowderBlue;
                    txt[1].Text = dsCatcount.Tables[0].Rows[0]["two"].ToString() + " ( 11 - 20 % )";

                    this.addtex_Consol8.Controls.Add(txt[1]);
                    this.addtex_Consol8.Controls.Add(tl3[1]);
                }
                else if (consol == 18)
                {
                    txt[1].BackColor = System.Drawing.Color.LightSalmon;
                    txt[1].Text = dsCatcount.Tables[0].Rows[0]["two"].ToString() + " ( 10 - 15 )";

                    this.addtex_Consol9.Controls.Add(txt[1]);
                    this.addtex_Consol9.Controls.Add(tl3[1]);
                }
        

                txt[2].Font.Bold = true;
                txt[2].Width = 100;
                txt[2].Height = 30;
                txt[2].Enabled = false;
                if (consol == 15)
                {
                    txt[2].BackColor = System.Drawing.Color.LightGreen;
                    txt[2].Text = dsCatcount.Tables[0].Rows[0]["three"].ToString() + " ( 71 - 80 % )";

                    this.addtex_Consol6.Controls.Add(txt[2]);
                    this.addtex_Consol6.Controls.Add(tl3[2]);
                }

                else if (consol == 16)
                {
                    txt[2].BackColor = System.Drawing.Color.Moccasin;
                    txt[2].Text = dsCatcount.Tables[0].Rows[0]["three"].ToString() + " ( 10 - 11 )";

                    this.addtex_Consol7.Controls.Add(txt[2]);
                    this.addtex_Consol7.Controls.Add(tl3[2]);
                }

                else if (consol == 17)
                {
                    txt[2].BackColor = System.Drawing.Color.MistyRose;
                   
                    txt[2].Text = dsCatcount.Tables[0].Rows[0]["three"].ToString() + " ( 21 - 30 % )";

                    this.addtex_Consol8.Controls.Add(txt[2]);
                    this.addtex_Consol8.Controls.Add(tl3[2]);
                }

                else if (consol == 18)
                {
                    txt[2].BackColor = System.Drawing.Color.PapayaWhip;
                    txt[2].Text = dsCatcount.Tables[0].Rows[0]["three"].ToString() + " ( 16 - 20 )";

                    this.addtex_Consol9.Controls.Add(txt[2]);
                    this.addtex_Consol9.Controls.Add(tl3[2]);
                }

                txt[3].Font.Bold = true;
                txt[3].Width = 100;
                txt[3].Height = 30;
                txt[3].Enabled = false;
                if (consol == 15)
                {
                    txt[3].BackColor = System.Drawing.Color.LightPink;
                    txt[3].Text = dsCatcount.Tables[0].Rows[0]["four"].ToString() + " ( 81 - 90 % )";

                    this.addtex_Consol6.Controls.Add(txt[3]);
                    this.addtex_Consol6.Controls.Add(tl3[3]);
                }

                else if (consol == 16)
                {
                    txt[3].BackColor = System.Drawing.Color.Aquamarine;
                    txt[3].Text = dsCatcount.Tables[0].Rows[0]["four"].ToString() + " ( 12 - 13 )";

                    this.addtex_Consol7.Controls.Add(txt[3]);
                    this.addtex_Consol7.Controls.Add(tl3[3]);
                }

                else if (consol == 17)
                {
                    txt[3].BackColor = System.Drawing.Color.Honeydew;
                    txt[3].Text = dsCatcount.Tables[0].Rows[0]["four"].ToString() + " ( 31 - 40 % )";

                    this.addtex_Consol8.Controls.Add(txt[3]);
                    this.addtex_Consol8.Controls.Add(tl3[3]);
                }

                else if (consol == 18)
                {
                    txt[3].BackColor = System.Drawing.Color.Thistle;
                    txt[3].Text = dsCatcount.Tables[0].Rows[0]["four"].ToString() + " ( 21 - 22 )";

                    this.addtex_Consol9.Controls.Add(txt[3]);
                    this.addtex_Consol9.Controls.Add(tl3[3]);
                }



                txt[4].Font.Bold = true;
                txt[4].Width = 100;
                txt[4].Height = 30;
                txt[4].Enabled = false;
                if (consol == 15)
                {
                    txt[4].BackColor = System.Drawing.Color.Aqua;
                    txt[4].Text = dsCatcount.Tables[0].Rows[0]["five"].ToString() + " ( 91 - 95 % )";

                    this.addtex_Consol6.Controls.Add(txt[4]);
                    this.addtex_Consol6.Controls.Add(tl3[4]);
                }

                else if (consol == 16)
                {
                    txt[4].BackColor = System.Drawing.Color.Lavender;
                    txt[4].Text = dsCatcount.Tables[0].Rows[0]["five"].ToString() + " ( 14 - 15 )";

                    this.addtex_Consol7.Controls.Add(txt[4]);
                    this.addtex_Consol7.Controls.Add(tl3[4]);
                }

                else if (consol == 17)
                {
                    txt[4].BackColor = System.Drawing.Color.BurlyWood;
                    txt[4].Text = dsCatcount.Tables[0].Rows[0]["five"].ToString() + " ( 41 - 50 % )";

                    this.addtex_Consol8.Controls.Add(txt[4]);
                    this.addtex_Consol8.Controls.Add(tl3[4]);
                }

                else if (consol == 18)
                {
                    txt[4].BackColor = System.Drawing.Color.Khaki;
                    txt[4].Text = dsCatcount.Tables[0].Rows[0]["five"].ToString() + " ( 23 - 24 )";

                    this.addtex_Consol9.Controls.Add(txt[4]);
                    this.addtex_Consol9.Controls.Add(tl3[4]);
                }



                txt[5].Font.Bold = true;
                txt[5].Width = 100;
                txt[5].Height = 30;
                txt[5].Enabled = false;
                if (consol == 15)
                {
                    txt[5].BackColor = System.Drawing.Color.SkyBlue;
                    txt[5].Text = dsCatcount.Tables[0].Rows[0]["six"].ToString() + " ( 96 - 100 % )";

                    this.addtex_Consol6.Controls.Add(txt[5]);
                    this.addtex_Consol6.Controls.Add(tl3[5]);
                }

                else if (consol == 16)
                {

                    txt[5].BackColor = System.Drawing.Color.PaleGreen;
                    txt[5].Text = dsCatcount.Tables[0].Rows[0]["six"].ToString() + " ( Above 15 )";

                    this.addtex_Consol7.Controls.Add(txt[5]);
                    this.addtex_Consol7.Controls.Add(tl3[5]);
                }

                else if (consol == 17)
                {
                    txt[5].Text = dsCatcount.Tables[0].Rows[0]["six"].ToString() + " ( Above 50 % )";

                    this.addtex_Consol8.Controls.Add(txt[5]);
                    this.addtex_Consol8.Controls.Add(tl3[5]);
                }

                else if (consol == 18)
                {
                    txt[5].Text = dsCatcount.Tables[0].Rows[0]["six"].ToString() + " ( Above 24 )";

                    this.addtex_Consol9.Controls.Add(txt[5]);
                    this.addtex_Consol9.Controls.Add(tl3[5]);
                }
            }


    protected void Tomato_Click(object sender, EventArgs e)
    {
        int count = 0;
        foreach (GridViewRow rw in grdAppdashboard.Rows)
        {
            Label lblvalue = (Label)rw.FindControl("lblvalue");
            Label lblSNo = (Label)rw.FindControl("lblSNo");

            if (lblvalue.Text == "")
            {
                rw.Visible = false;
            }
            else
            {
                if (mode == "1")
                {

                    if (Convert.ToInt16(lblvalue.Text) >= 0 && Convert.ToInt16(lblvalue.Text) <= 50)
                    {
                        count++;
                        lblSNo.Text = count.ToString();
                    }
                    else
                    {
                        rw.Visible = false;
                    }
                }
                else if (mode == "2")
                {
                    if (Convert.ToInt16(lblvalue.Text) >= 0 && Convert.ToInt16(lblvalue.Text) <= 7)
                    {
                        count++;
                        lblSNo.Text = count.ToString();
                    }
                    else
                    {
                        rw.Visible = false;
                    }
                }
                else if (mode == "3")
                {
                    if (Convert.ToInt16(lblvalue.Text) >50)
                    {
                        count++;
                        lblSNo.Text = count.ToString();
                    }
                    else
                    {
                        rw.Visible = false;
                    }
                }

                else if (mode == "4")
                {
                    if (Convert.ToInt16(lblvalue.Text) >= 0 && Convert.ToInt16(lblvalue.Text) <= 9)
                    {
                        count++;
                        lblSNo.Text = count.ToString();
                    }
                    else
                    {
                        rw.Visible = false;
                    }
                }

              
            }
            
        }
    }
    protected void Yellow_Click(object sender, EventArgs e)
    {
        int count = 0;
        foreach (GridViewRow rw in grdAppdashboard.Rows)
        {
            Label lblvalue = (Label)rw.FindControl("lblvalue");
            Label lblSNo = (Label)rw.FindControl("lblSNo");

            if (lblvalue.Text == "")
            {
                rw.Visible = false;
            }
            else
            {

                if (mode == "1")
                {

                    if (Convert.ToInt16(lblvalue.Text) >= 51 && Convert.ToInt16(lblvalue.Text) <= 70)
                    {
                        count++;
                        lblSNo.Text = count.ToString();
                    }
                    else
                    {
                        rw.Visible = false;
                    }
                }


                else if (mode == "2")
                {
                    if (Convert.ToInt16(lblvalue.Text) >= 8 && Convert.ToInt16(lblvalue.Text) <= 9)
                    {
                        count++;
                        lblSNo.Text = count.ToString();
                    }
                    else
                    {
                        rw.Visible = false;
                    }
                }

                else if (mode == "3")
                {
                    if (Convert.ToInt16(lblvalue.Text) >= 41 && Convert.ToInt16(lblvalue.Text) <= 50)
                    {
                        count++;
                        lblSNo.Text = count.ToString();
                    }
                    else
                    {
                        rw.Visible = false;
                    }
                }

                else if (mode == "4")
                {
                    if (Convert.ToInt16(lblvalue.Text) >= 10 && Convert.ToInt16(lblvalue.Text) <= 15)
                    {
                        count++;
                        lblSNo.Text = count.ToString();
                    }
                    else
                    {
                        rw.Visible = false;
                    }
                }
            }
        }
    }
    protected void LightGreen_Click(object sender, EventArgs e)
    {
        int count = 0;
        foreach (GridViewRow rw in grdAppdashboard.Rows)
        {
            Label lblvalue = (Label)rw.FindControl("lblvalue");
            Label lblSNo = (Label)rw.FindControl("lblSNo");

            if (lblvalue.Text == "")
            {
                rw.Visible = false;
            }
            else
            {
                if (mode == "1")
                {

                    if (Convert.ToInt16(lblvalue.Text) >= 71 && Convert.ToInt16(lblvalue.Text) <= 80)
                    {
                        count++;
                        lblSNo.Text = count.ToString();
                    }
                    else
                    {
                        rw.Visible = false;
                    }
                }

                else if (mode == "2")
                {
                    if (Convert.ToInt16(lblvalue.Text) >= 10 && Convert.ToInt16(lblvalue.Text) <= 11)
                    {
                        count++;
                        lblSNo.Text = count.ToString();
                    }
                    else
                    {
                        rw.Visible = false;
                    }
                }

                else if (mode == "3")
                {
                    if (Convert.ToInt16(lblvalue.Text) >= 31 && Convert.ToInt16(lblvalue.Text) <= 40)
                    {
                        count++;
                        lblSNo.Text = count.ToString();
                    }
                    else
                    {
                        rw.Visible = false;
                    }
                }

                else if (mode == "4")
                {
                    if (Convert.ToInt16(lblvalue.Text) >= 16 && Convert.ToInt16(lblvalue.Text) <= 20)
                    {
                        count++;
                        lblSNo.Text = count.ToString();
                    }
                    else
                    {
                        rw.Visible = false;
                    }
                }
            }
        }
    }

   
    protected void LightPink_Click(object sender, EventArgs e)
    {
        int count = 0;
        foreach (GridViewRow rw in grdAppdashboard.Rows)
        {
            Label lblvalue = (Label)rw.FindControl("lblvalue");
            Label lblSNo = (Label)rw.FindControl("lblSNo");

            if (lblvalue.Text == "")
            {
                rw.Visible = false;
            }
            else
            {

                if (mode == "1")
                {
                    if (Convert.ToInt16(lblvalue.Text) >= 81 && Convert.ToInt16(lblvalue.Text) <= 90)
                    {
                        count++;
                        lblSNo.Text = count.ToString();
                    }
                    else
                    {
                        rw.Visible = false;
                    }
                }

                else if (mode == "2")
                {
                    if (Convert.ToInt16(lblvalue.Text) >= 12 && Convert.ToInt16(lblvalue.Text) <= 13)
                    {
                        count++;
                        lblSNo.Text = count.ToString();
                    }
                    else
                    {
                        rw.Visible = false;
                    }
                }

                else if (mode == "3")
                {
                    if (Convert.ToInt16(lblvalue.Text) >= 21 && Convert.ToInt16(lblvalue.Text) <= 30)
                    {
                        count++;
                        lblSNo.Text = count.ToString();
                    }
                    else
                    {
                        rw.Visible = false;
                    }
                }

                else if (mode == "4")
                {
                    if (Convert.ToInt16(lblvalue.Text) >= 21 && Convert.ToInt16(lblvalue.Text) <= 22)
                    {
                        count++;
                        lblSNo.Text = count.ToString();
                    }
                    else
                    {
                        rw.Visible = false;
                    }
                }
            }
        }
    }
    protected void Aqua_Click(object sender, EventArgs e)
    {
        int count = 0;
        foreach (GridViewRow rw in grdAppdashboard.Rows)
        {
            Label lblvalue = (Label)rw.FindControl("lblvalue");
            Label lblSNo = (Label)rw.FindControl("lblSNo");

            if (lblvalue.Text == "")
            {
                rw.Visible = false;
            }
            else
            {
                if (mode == "1")
                {
                    if (Convert.ToInt16(lblvalue.Text) >= 91 && Convert.ToInt16(lblvalue.Text) <= 95)
                    {
                        count++;
                        lblSNo.Text = count.ToString();
                    }
                    else
                    {
                        rw.Visible = false;
                    }
                }
                else if (mode == "2")
                {
                    if (Convert.ToInt16(lblvalue.Text) >= 14 && Convert.ToInt16(lblvalue.Text) <= 15)
                    {
                        count++;
                        lblSNo.Text = count.ToString();
                    }
                    else
                    {
                        rw.Visible = false;
                    }
                }

                else if (mode == "3")
                {
                    if (Convert.ToInt16(lblvalue.Text) >= 11 && Convert.ToInt16(lblvalue.Text) <= 20)
                    {
                        count++;
                        lblSNo.Text = count.ToString();
                    }
                    else
                    {
                        rw.Visible = false;
                    }
                }

                else if (mode == "4")
                {
                    if (Convert.ToInt16(lblvalue.Text) >= 23 && Convert.ToInt16(lblvalue.Text) <= 24)
                    {
                        count++;
                        lblSNo.Text = count.ToString();
                    }
                    else
                    {
                        rw.Visible = false;
                    }
                }

            }
        }
    }
    protected void SkyBlue_Click(object sender, EventArgs e)
    {
        int count = 0;
        foreach (GridViewRow rw in grdAppdashboard.Rows)
        {
            Label lblvalue = (Label)rw.FindControl("lblvalue");
            Label lblSNo = (Label)rw.FindControl("lblSNo");

            if (lblvalue.Text == "")
            {
                rw.Visible = false;
            }
            else
            {
                if (mode == "1")
                {
                    if (Convert.ToInt16(lblvalue.Text) >= 96 && Convert.ToInt16(lblvalue.Text) <= 100)
                    {
                        count++;
                        lblSNo.Text = count.ToString();
                    }
                    else
                    {
                        rw.Visible = false;
                    }
                }

                else if (mode == "2")
                {
                    if (Convert.ToInt16(lblvalue.Text) >15)
                    {
                        count++;
                        lblSNo.Text = count.ToString();
                    }
                    else
                    {
                        rw.Visible = false;
                    }
                }

                else if (mode == "3")
                {
                    if (Convert.ToInt16(lblvalue.Text) >= 0 && Convert.ToInt16(lblvalue.Text) <= 10)
                    {
                        count++;
                        lblSNo.Text = count.ToString();
                    }
                    else
                    {
                        rw.Visible = false;
                    }
                }

                else if (mode == "4")
                {
                    if (Convert.ToInt16(lblvalue.Text) > 24)
                    {
                        count++;
                        lblSNo.Text = count.ToString();
                    }
                    else
                    {
                        rw.Visible = false;
                    }
                }
            }
        }
    }
   
}