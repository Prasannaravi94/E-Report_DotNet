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
using System.Text;
using Bus_EReport;
using System.Net;

public partial class MasterFiles_ActivityReports_rptLeave_Entitlement_view : System.Web.UI.Page
{
    string sfCode = string.Empty;
    string sfname = string.Empty;
    string divcode = string.Empty;
    int iMonth = -1;
    int iYear = -1;
    DataSet dsDoctor = null;
    
    string sMode = string.Empty;
    string FMonth = string.Empty;
    string FYear = string.Empty;
    string TMonth = string.Empty;
    string TYear = string.Empty;
    string Prod = string.Empty;
    string Prod_Name = string.Empty;
    string sReturn = string.Empty;
    string sDay = string.Empty;
    string sCnt = string.Empty;
    string vMode = string.Empty;
    DataSet dsSalesForce = new DataSet();
    DataSet dsmgrsf = new DataSet();
    DataSet dsDoc = null;
    DataSet dsLeave = null;
    string Monthsub = string.Empty;
    string tot_dr = string.Empty;
    int tot_drcl = 0;
   int tot_drpl = 0;
    string tot_drsl = string.Empty;
    string tot_drLOP = string.Empty;
    string monthtotal = string.Empty;
    string totutcl = string.Empty;
    string totutpl = string.Empty;
    string totutsl = string.Empty;
    string totutlop = string.Empty;
    string Days = string.Empty;
    string strSf_Code = string.Empty;
    string strmonth_Count = string.Empty;
    string sCurrentDate = string.Empty;
    string Detailed = string.Empty;
    int totuticount = 0;
    int totminus = 0;
    DateTime dtCurrent;
   
    protected void Page_Load(object sender, EventArgs e)
    {
        divcode = Session["div_code"].ToString();
        sfCode = Request.QueryString["sfcode"].ToString();
        FMonth = Request.QueryString["FMonth"].ToString();
        FYear = Request.QueryString["FYear"].ToString();
        TMonth = Request.QueryString["TMonth"].ToString();
        TYear = Request.QueryString["TYear"].ToString();
        sfname = Request.QueryString["sf_name"].ToString();
        Session["cl"] = 0;
        lblRegionName.Text = sfname;
        System.Globalization.DateTimeFormatInfo mfi = new System.Globalization.DateTimeFormatInfo();
        string strFMonthName = mfi.GetMonthName(Convert.ToInt16(FMonth)).ToString().Substring(0, 3);
        string strTMonthName = mfi.GetMonthName(Convert.ToInt16(TMonth)).ToString().Substring(0, 3);

        lblHead.Text = "Leave Status View for the Month of " + strFMonthName + " " + FYear + " " + "To" + " " + strTMonthName + " " + TYear;

        lblIDMonth.Visible = false;
        lblIDYear.Visible = false;

        //if (Detailed == "1")
        //{
            FillSF_Leave_Type();
        //}
        //else
        //{
        //    FillSF();
        //}

    }
    private void FillSF_Leave_Type()
    {
        string sURL = string.Empty;
        tbl.Rows.Clear();

        SalesForce sf = new SalesForce();
        dsSalesForce = sf.UserList_get_SelfMail(divcode, sfCode);

        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            TableRow tr_header = new TableRow();
            tr_header.BorderStyle = BorderStyle.Solid;
            tr_header.BorderWidth = 1;
            tr_header.BackColor = System.Drawing.Color.FromName("#0097AC");
            tr_header.Style.Add("Color", "White");
            tr_header.BorderColor = System.Drawing.Color.Black;

            TableCell tc_SNo = new TableCell();
            tc_SNo.BorderStyle = BorderStyle.Solid;
            tc_SNo.BorderWidth = 1;
            tc_SNo.Width = 50;
            tc_SNo.RowSpan = 3;
            Literal lit_SNo =
                new Literal();
            lit_SNo.Text = "#";
            tc_SNo.BorderColor = System.Drawing.Color.Black;
            tc_SNo.Controls.Add(lit_SNo);
            tc_SNo.Attributes.Add("Class", "rptCellBorder");
            tr_header.Cells.Add(tc_SNo);

            TableCell tc_DR_Code = new TableCell();
            tc_DR_Code.BorderStyle = BorderStyle.Solid;
            tc_DR_Code.BorderWidth = 1;
            tc_DR_Code.Width = 400;
            tc_DR_Code.RowSpan = 3;
            Literal lit_DR_Code = new Literal();
            lit_DR_Code.Text = "<center>SF Code</center>";
            tc_DR_Code.Controls.Add(lit_DR_Code);
            tc_DR_Code.Attributes.Add("Class", "rptCellBorder");
            tc_DR_Code.BorderColor = System.Drawing.Color.Black;
            tc_DR_Code.Visible = false;
            tr_header.Cells.Add(tc_DR_Code);

            TableCell tc_DR_Emp = new TableCell();
            tc_DR_Emp.BorderStyle = BorderStyle.Solid;
            tc_DR_Emp.BorderWidth = 1;
            tc_DR_Emp.Width = 50;
            tc_DR_Emp.RowSpan = 3;
            Literal lit_DR_Emp = new Literal();
            lit_DR_Emp.Text = "<center>Employee Id</center>";
            tc_DR_Emp.BorderColor = System.Drawing.Color.Black;
            tc_DR_Emp.Attributes.Add("Class", "rptCellBorder");
            tc_DR_Emp.Controls.Add(lit_DR_Emp);
            tr_header.Cells.Add(tc_DR_Emp);

            TableCell tc_DR_Name = new TableCell();
            tc_DR_Name.BorderStyle = BorderStyle.Solid;
            tc_DR_Name.BorderWidth = 1;
            tc_DR_Name.Width = 200;
            tc_DR_Name.RowSpan = 3;
            Literal lit_DR_Name = new Literal();
            lit_DR_Name.Text = "<center>Fieldforce&nbspName / Month</center>";
            tc_DR_Name.BorderColor = System.Drawing.Color.Black;
            tc_DR_Name.Attributes.Add("Class", "rptCellBorder");
            tc_DR_Name.Controls.Add(lit_DR_Name);
            tr_header.Cells.Add(tc_DR_Name);

            TableCell tc_DR_HQ = new TableCell();
            tc_DR_HQ.BorderStyle = BorderStyle.Solid;
            tc_DR_HQ.BorderWidth = 1;
            tc_DR_HQ.Width = 100;
            tc_DR_HQ.RowSpan = 3;
            Literal lit_DR_HQ = new Literal();
            lit_DR_HQ.Text = "<center>HQ</center>";
            tc_DR_HQ.BorderColor = System.Drawing.Color.Black;
            tc_DR_HQ.Attributes.Add("Class", "rptCellBorder");
            tc_DR_HQ.Controls.Add(lit_DR_HQ);
            tr_header.Cells.Add(tc_DR_HQ);

            TableCell tc_DR_Des = new TableCell();
            tc_DR_Des.BorderStyle = BorderStyle.Solid;
            tc_DR_Des.BorderWidth = 1;
            tc_DR_Des.Width = 80;
            tc_DR_Des.RowSpan = 3;
            Literal lit_DR_Des = new Literal();
            lit_DR_Des.Text = "<center>Designation</center>";
            tc_DR_Des.BorderColor = System.Drawing.Color.Black;
            tc_DR_Des.Attributes.Add("Class", "rptCellBorder");
            tc_DR_Des.Controls.Add(lit_DR_Des);
            tr_header.Cells.Add(tc_DR_Des);

            Doctor dr = new Doctor();
            dsDoctor = dr.getDCR_Leave_Type(divcode);
            TableCell leave_utility = new TableCell();
            leave_utility.BorderStyle = BorderStyle.Solid;
            leave_utility.BorderWidth = 1;
            leave_utility.Width = 80;
            leave_utility.RowSpan = 2;

            leave_utility.ColumnSpan = dsDoctor.Tables[0].Rows.Count;
            //leave_utility.ColumnSpan = 2;
            Literal lit_DR_utility = new Literal();
            //lit_DR_utility.Text = dataRow["Leave_SName"].ToString();
            lit_DR_utility.Text = "<center>Leave Eligibilty</center>";
            leave_utility.BorderColor = System.Drawing.Color.Black;
            leave_utility.Attributes.Add("Class", "rptCellBorder");
            leave_utility.Controls.Add(lit_DR_utility);
            tr_header.Cells.Add(leave_utility);

            TableRow tr_catg = new TableRow();
            tr_catg.BackColor = System.Drawing.Color.FromName("#0097AC");
            tr_catg.Style.Add("Color", "White");
            //  tr_catg.BackColor = System.Drawing.Color.FromName("#A6A6D2");

           
                if (dsDoctor.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dataRow in dsDoctor.Tables[0].Rows)
                    {
                        TableCell tc_catg_namee = new TableCell();
                        tc_catg_namee.BorderStyle = BorderStyle.Solid;
                        tc_catg_namee.BorderWidth = 1;
                        //text-align: center;
                      

                        Literal lit_catg_namee = new Literal();
                        lit_catg_namee.Text = dataRow["Leave_SName"].ToString();

                        tc_catg_namee.Attributes.Add("Class", "rptCellBorder");
                        tc_catg_namee.HorizontalAlign = HorizontalAlign.Center;
                        tc_catg_namee.Controls.Add(lit_catg_namee);
                        tr_catg.Cells.Add(tc_catg_namee);
                    }

                    //tbl.Rows.Add(tr_catg);
                }
           


            //TableRow tr_catgG = new TableRow();
           
            //tbl.Rows.Add(tr_catgG);
            int months = (Convert.ToInt32(TYear) - Convert.ToInt32(FYear)) * 12 + Convert.ToInt32(TMonth) - Convert.ToInt32(FMonth); //(Date2.Year - Date1.Year) * 12 + Date2.Month - Date1.Month;
            int cmonth = Convert.ToInt32(FMonth);
            int cyear = Convert.ToInt32(FYear);
           
            ViewState["months"] = months;
            ViewState["cmonth"] = cmonth;
            ViewState["cyear"] = cyear;

            //Doctor dr = new Doctor();
            dsDoctor = dr.getDCR_Leave_Type(divcode);

            if (months >= 0)
            {
                for (int j = 1; j <= months + 1; j++)
                {
                    TableCell tc_month = new TableCell();
                    tc_month.ColumnSpan = dsDoctor.Tables[0].Rows.Count * 1;
                    //tc_month.ColumnSpan = 1;
                    Literal lit_month = new Literal();
                    Monthsub = sf.getMonthName(cmonth.ToString()) + "-" + cyear;
                    lit_month.Text = Monthsub.Substring(0, 3) + "-" + cyear;
                    tc_month.Attributes.Add("Class", "rptCellBorder");
                    tc_month.BorderStyle = BorderStyle.Solid;
                    tc_month.BorderWidth = 1;
                    tc_month.HorizontalAlign = HorizontalAlign.Center;
                    //tc_month.Width = 200;
                    tc_month.Controls.Add(lit_month);
                    tr_header.Cells.Add(tc_month);
                    strmonth_Count += "'" + cmonth + "'" + ",";
                    cmonth = cmonth + 1;
                    if (cmonth == 13)
                    {
                        cmonth = 1;
                        cyear = cyear + 1;
                    }
                    //string cc=cmonth 
                    //strmonth_Count += "'"+ cmonth+"'" + ",";
                  
                   
                   
                }
            }

            tbl.Rows.Add(tr_header);

            //Sub Header
            months = Convert.ToInt16(ViewState["months"].ToString());
            cmonth = Convert.ToInt16(ViewState["cmonth"].ToString());
            cyear = Convert.ToInt16(ViewState["cyear"].ToString());

            if (months >= 0)
            {
                TableRow tr_lst_det = new TableRow();
                for (int j = 1; j <= months + 1; j++)
                {
                    TableCell tc_lst_month = new TableCell();
                    HyperLink lit_lst_month = new HyperLink();
                    lit_lst_month.Text = "Leave Count";
                    tc_lst_month.BorderStyle = BorderStyle.Solid;
                    tc_lst_month.HorizontalAlign = HorizontalAlign.Center;
                    tc_lst_month.BorderWidth = 1;
                    tc_lst_month.Attributes.Add("Class", "rptCellBorder");
                    tc_lst_month.ColumnSpan = dsDoctor.Tables[0].Rows.Count;
                    tc_lst_month.Controls.Add(lit_lst_month);
                    tr_lst_det.Cells.Add(tc_lst_month);


                    cmonth = cmonth + 1;
                    if (cmonth == 13)
                    {
                        cmonth = 1;
                        cyear = cyear + 1;
                    }
                }
                tr_lst_det.BackColor = System.Drawing.Color.FromName("#0097AC");
                tr_lst_det.Style.Add("Color", "White");

                tr_lst_det.Attributes.Add("Class", "Backcolor");

                tbl.Rows.Add(tr_lst_det);
            }

            if (months >= 0)
            {
               
                tr_catg.BackColor = System.Drawing.Color.FromName("#0097AC");
                tr_catg.Style.Add("Color", "White");
                //  tr_catg.BackColor = System.Drawing.Color.FromName("#A6A6D2");

                for (int j = 1; j <= (months + 1) * 1; j++)
                {
                    if (dsDoctor.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow dataRow in dsDoctor.Tables[0].Rows)
                        {
                            TableCell tc_catg_name = new TableCell();
                            tc_catg_name.BorderStyle = BorderStyle.Solid;
                            tc_catg_name.BorderWidth = 1;
                            //text-align: center;
                            if ((j % 2) == 1)
                            {
                                //tc_catg_name.BackColor = System.Drawing.Color.LavenderBlush;
                            }
                            else
                            {
                                //tc_catg_name.BackColor = System.Drawing.Color.PapayaWhip;
                            }
                            // tc_catg_name.Width = 30;

                            Literal lit_catg_name = new Literal();
                            lit_catg_name.Text = dataRow["Leave_SName"].ToString();
                         
                            tc_catg_name.Attributes.Add("Class", "rptCellBorder");
                            tc_catg_name.HorizontalAlign = HorizontalAlign.Center;
                            tc_catg_name.Controls.Add(lit_catg_name);
                            tr_catg.Cells.Add(tc_catg_name);
                        }

                       
                    }
                }
               

           
                dsDoctor = dr.getDCR_Leave_Type(divcode);

                TableCell leave_eligiblity = new TableCell();
                leave_eligiblity.BorderStyle = BorderStyle.Solid;
                leave_eligiblity.BorderWidth = 1;
                leave_eligiblity.Width = 80;
                leave_eligiblity.RowSpan = 2;
                leave_eligiblity.ColumnSpan = dsDoctor.Tables[0].Rows.Count;
                //leave_eligiblity.ColumnSpan = 3;
                Literal lit_DR_Dess = new Literal();
              
                lit_DR_Dess.Text =  "Leave Utility";
                leave_eligiblity.HorizontalAlign = HorizontalAlign.Center;
                leave_eligiblity.BorderColor = System.Drawing.Color.Black;
                leave_eligiblity.Attributes.Add("Class", "rptCellBorder");
                leave_eligiblity.Controls.Add(lit_DR_Dess);
                tr_header.Cells.Add(leave_eligiblity);
                //TableRow tr_catgq = new TableRow();
                if (dsDoctor.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dataRow in dsDoctor.Tables[0].Rows)
                    {
                TableCell tc_catg_namew = new TableCell();
                tc_catg_namew.BorderStyle = BorderStyle.Solid;
                tc_catg_namew.BorderWidth = 1;

                Literal lit_catg_namew = new Literal();
                lit_catg_namew.Text =  dataRow["Leave_SName"].ToString();
                tc_catg_namew.Attributes.Add("Class", "rptCellBorder");
                tc_catg_namew.HorizontalAlign = HorizontalAlign.Center;
                tc_catg_namew.Controls.Add(lit_catg_namew);
                tr_catg.Cells.Add(tc_catg_namew);
                    }
                    tbl.Rows.Add(tr_catg);
               
                
                    }

                    //tbl.Rows.Add(tr_catg);
                

              
                       
           

             
               


               



              
               
            }


         
            


           
            if (dsSalesForce.Tables[0].Rows.Count > 0)
                ViewState["dsSalesForce"] = dsSalesForce;


            int iCount = 0;
            int iTotLstCount = 0;
            dsSalesForce = (DataSet)ViewState["dsSalesForce"];
            foreach (DataRow drFF in dsSalesForce.Tables[0].Rows)
            {
                TableRow tr_det = new TableRow();
                iCount += 1;
                strSf_Code += "'" + drFF["sf_code"].ToString() + "'" + ",";


                //S.No
                TableCell tc_det_SNo = new TableCell();
                Literal lit_det_SNo = new Literal();
                lit_det_SNo.Text = iCount.ToString();
                tc_det_SNo.BorderStyle = BorderStyle.Solid;
                tc_det_SNo.BorderWidth = 1;
                tc_det_SNo.Attributes.Add("Class", "rptCellBorder");
                tc_det_SNo.Controls.Add(lit_det_SNo);
                tr_det.Cells.Add(tc_det_SNo);
                tr_det.BackColor = System.Drawing.Color.White;

                //Emp
                TableCell tc_det_Emp = new TableCell();
                Literal lit_det_Emp = new Literal();
                lit_det_Emp.Text = drFF["sf_emp_id"].ToString();
                tc_det_Emp.BorderStyle = BorderStyle.Solid;
                tc_det_Emp.BorderWidth = 1;
                tc_det_Emp.Attributes.Add("Class", "rptCellBorder");
                tc_det_Emp.Controls.Add(lit_det_Emp);
                tr_det.Cells.Add(tc_det_Emp);

                //SF_code
                TableCell tc_det_usr = new TableCell();
                Literal lit_det_usr = new Literal();
                lit_det_usr.Text = drFF["Sf_Code"].ToString();
                tc_det_usr.BorderStyle = BorderStyle.Solid;
                tc_det_usr.BorderWidth = 1;
                tc_det_usr.Visible = false;
                tc_det_usr.Attributes.Add("Class", "rptCellBorder");
                tc_det_usr.Controls.Add(lit_det_usr);
                tr_det.Cells.Add(tc_det_usr);

                //SF Name
                TableCell tc_det_FF = new TableCell();
                Literal lit_det_FF = new Literal();
                lit_det_FF.Text = drFF["sf_name"].ToString();
                tc_det_FF.BorderStyle = BorderStyle.Solid;
                tc_det_FF.BorderWidth = 1;
                tc_det_FF.Attributes.Add("Class", "rptCellBorder");
                tc_det_FF.Controls.Add(lit_det_FF);
                tr_det.Cells.Add(tc_det_FF);

                //hq
                TableCell tc_det_hq = new TableCell();
                Literal lit_det_hq = new Literal();
                lit_det_hq.Text = drFF["sf_hq"].ToString();
                tc_det_hq.BorderStyle = BorderStyle.Solid;
                tc_det_hq.BorderWidth = 1;
                tc_det_hq.Attributes.Add("Class", "rptCellBorder");
                tc_det_hq.Controls.Add(lit_det_hq);
                tr_det.Cells.Add(tc_det_hq);

                //SF Designation Short Name
                TableCell tc_det_Designation = new TableCell();
                Literal lit_det_Designation = new Literal();
                lit_det_Designation.Text = drFF["Designation_Short_Name"].ToString();
                tc_det_Designation.BorderStyle = BorderStyle.Solid;
                tc_det_Designation.BorderWidth = 1;
                tc_det_Designation.Controls.Add(lit_det_Designation);
                tc_det_Designation.Attributes.Add("Class", "rptCellBorder");
                tr_det.Cells.Add(tc_det_Designation);

                months = Convert.ToInt16(ViewState["months"].ToString());
                cmonth = Convert.ToInt16(ViewState["cmonth"].ToString());
                cyear = Convert.ToInt16(ViewState["cyear"].ToString());

                if (dsDoctor.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dataRow in dsDoctor.Tables[0].Rows)
                    {
                        dsDoc = sf.get_leave_days(drFF["Sf_Code"].ToString(), divcode, dataRow["Leave_SName"].ToString(), cyear);
                        if (dsDoc.Tables[0].Rows.Count > 0)
                            tot_drpl = Convert.ToInt32(dsDoc.Tables[0].Rows[0].ItemArray.GetValue(0));
                        TableCell tc_tot_monthqEah = new TableCell();

                        HyperLink hyp_monthqEah = new HyperLink();
                        hyp_monthqEah.Text = tot_drpl.ToString();
                        tc_tot_monthqEah.BorderStyle = BorderStyle.Solid;
                        tc_tot_monthqEah.BorderWidth = 1;
                        tc_tot_monthqEah.BackColor = System.Drawing.Color.White;
                        tc_tot_monthqEah.Width = 200;
                        tc_tot_monthqEah.Style.Add("font-family", "Calibri");
                        tc_tot_monthqEah.Style.Add("font-size", "10pt");
                        tc_tot_monthqEah.HorizontalAlign = HorizontalAlign.Center;
                        tc_tot_monthqEah.VerticalAlign = VerticalAlign.Middle;
                        tc_tot_monthqEah.Controls.Add(hyp_monthqEah);
                        tc_tot_monthqEah.Attributes.Add("style", "font-weight:bold;");
                        tc_tot_monthqEah.Attributes.Add("Class", "rptCellBorder");
                        tr_det.Cells.Add(tc_tot_monthqEah);
                        tot_drpl = 0;
                    }
                }
                if (months >= 0)
                {

                    for (int j = 1; j <= months + 1; j++)
                    {

                        if (cmonth == 12)
                        {
                            sCurrentDate = "01-01-" + (cyear + 1);
                        }
                        else
                        {
                            sCurrentDate = (cmonth + 1) + "-01-" + cyear;
                        }

                        dtCurrent = Convert.ToDateTime(sCurrentDate);

                        if (dsDoctor.Tables[0].Rows.Count > 0)
                        {
                            foreach (DataRow dataRow in dsDoctor.Tables[0].Rows)
                            {
                                   
                                dsDoc = sf.getDCR_Leave_Count_Type(drFF["sf_code"].ToString(), divcode, cmonth, cyear, Convert.ToInt32(dataRow["Leave_code"].ToString()));


                                if (dsDoc.Tables[0].Rows.Count > 0)
                                    tot_dr = dsDoc.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                               // int clcode = Convert.ToInt32(dataRow["Leave_code"].ToString());
                             
                               // int ff=clcode;
                               //  if(ff==Convert.ToInt32(Session["cl"].ToString()))
                               //  {
                               //      totutcl += tot_dr;
                               //      //Response.Write(totutcl);
                               //  }
                               //Session["cl"]=Convert.ToInt32(dataRow["Leave_code"].ToString());
                                                         
                             dsLeave = sf.getDCR_Leave_Count_ToolTip_Type(drFF["sf_code"].ToString(), divcode, cmonth, cyear, Convert.ToInt32(dataRow["Leave_code"].ToString()));

                                if (dsLeave.Tables[0].Rows.Count > 0)
                                    Days = dsLeave.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();


                                TableCell tc_lst_month = new TableCell();
                                HyperLink hyp_lst_month = new HyperLink();

                                if (tot_dr != "0")
                                {
                                    //iTotLstCount += Convert.ToInt16(tot_dr);
                                    hyp_lst_month.Text = tot_dr;
                                    hyp_lst_month.ToolTip = Days;

                                    //sURL = "&sf_code=" + drFF["Sf_Code"] + "&sf_name=" + drFF["sf_name"] + "&Year=" + cyear + "&Month=" + cmonth + "&Prod_Name=" + Prod_Name + "&Prod=" + Prod + "&sCurrentDate=" + sCurrentDate + "";

                                    //hyp_lst_month.Attributes.Add("href", "javascript:showModalPopUp('" + drFF["sf_code"].ToString() + "', '" + drFF["sf_name"] + "', '" + cyear + "', '" + cmonth + "', '" + Prod_Name + "', '" + Prod + "','" + sCurrentDate + "')");
                                    ////hyp_lst_month.Attributes["onclick"] = "javascript:showModalPopUp('" + sURL + "')";
                                    //hyp_lst_month.NavigateUrl = "#";

                                }

                                else
                                {
                                    hyp_lst_month.Text = "";
                                }


                                tc_lst_month.BorderStyle = BorderStyle.Solid;
                                tc_lst_month.BorderWidth = 1;
                                tc_lst_month.BackColor = System.Drawing.Color.White;
                                tc_lst_month.Width = 100;
                                tc_lst_month.HorizontalAlign = HorizontalAlign.Center;
                                tc_lst_month.VerticalAlign = VerticalAlign.Middle;
                                tc_lst_month.Controls.Add(hyp_lst_month);
                                tc_lst_month.Attributes.Add("Class", "rptCellBorder");
                                tr_det.Cells.Add(tc_lst_month);
                                Label1.Text = totutcl;
                                tot_dr = "";
                                Days = "";
                            }

                        }
                        cmonth = cmonth + 1;
                        if (cmonth == 13)
                        {
                            cmonth = 1;
                            cyear = cyear + 1;
                        }

                    }
                    //

                }
                
                        //dsDoc = sf.get_cl_days(drFF["sf_code"].ToString(), divcode);


                        //if (dsDoc.Tables[0].Rows.Count > 0)
                        //    tot_drcl = dsDoc.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                        //dsDoc = sf.get_pl_days(drFF["sf_code"].ToString(), divcode);


                        //if (dsDoc.Tables[0].Rows.Count > 0)
                        //    tot_drpl = dsDoc.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                       
                    
                        //strmonth_Count=strmonth_Count.Replace(" ","");
                        //strmonth_Count = strmonth_Count.Replace("""", "");
                       //strmonth_Count= strmonth_Count.Remove("""", String.Empty);
                        //strmonth_Count=strmonth_Count.Replace('"', ' ').Trim();
                        //strmonth_Count = strmonth_Count.Trim('"');
                        //if (strmonth_Count.Contains(','))
                        //{
                        //    strmonth_Count = strmonth_Count.Remove(strmonth_Count.Length - 1);
                        //}
                        //strmonth_Count = strmonth_Count + "'0'";
                        //strmonth_Count = Convert.ToInt32(strmonth_Count)
                        //int x = Int32.Parse(strmonth_Count);
                        //int x = int.Parse(strmonth_Count);

                        //for(int i=0;i<)
                        //{

                      
                    //}
                    

                     
                  
               
                //TableRow tr_total = new TableRow();
                
                //TableCell tc_tot_monthqE = new TableCell();
                //HyperLink hyp_monthqE = new HyperLink();
                //hyp_monthqE.Text = tot_drpl;
                //tc_tot_monthqE.BorderStyle = BorderStyle.Solid;
                //tc_tot_monthqE.BorderWidth = 1;
                //tc_tot_monthqE.BackColor = System.Drawing.Color.White;
                //tc_tot_monthqE.Width = 200;
                //tc_tot_monthqE.Style.Add("font-family", "Calibri");
                //tc_tot_monthqE.Style.Add("font-size", "10pt");
                //tc_tot_monthqE.HorizontalAlign = HorizontalAlign.Center;
                //tc_tot_monthqE.VerticalAlign = VerticalAlign.Middle;
                //tc_tot_monthqE.Controls.Add(hyp_monthqE);
                //tc_tot_monthqE.Attributes.Add("style", "font-weight:bold;");
                //tc_tot_monthqE.Attributes.Add("Class", "rptCellBorder");
                //tr_det.Cells.Add(tc_tot_monthqE);
                //tot_drpl = "";
                //TableCell tc_tot_monthqB = new TableCell();
                //HyperLink hyp_monthqB = new HyperLink();
                //hyp_monthqB.Text = tot_drsl;
                //tc_tot_monthqB.BorderStyle = BorderStyle.Solid;
                //tc_tot_monthqB.BorderWidth = 1;
                //tc_tot_monthqB.BackColor = System.Drawing.Color.White;
                //tc_tot_monthqB.Width = 200;
                //tc_tot_monthqB.Style.Add("font-family", "Calibri");
                //tc_tot_monthqB.Style.Add("font-size", "10pt");
                //tc_tot_monthqB.HorizontalAlign = HorizontalAlign.Center;
                //tc_tot_monthqB.VerticalAlign = VerticalAlign.Middle;
                //tc_tot_monthqB.Controls.Add(hyp_monthqB);
                //tc_tot_monthqB.Attributes.Add("style", "font-weight:bold;");
                //tc_tot_monthqB.Attributes.Add("Class", "rptCellBorder");
                //tr_det.Cells.Add(tc_tot_monthqB);
                //tot_drsl = "";

                //TableCell tc_tot_monthqBF = new TableCell();
                //HyperLink hyp_monthqBF = new HyperLink();
                //hyp_monthqBF.Text = tot_drLOP;
                //tc_tot_monthqBF.BorderStyle = BorderStyle.Solid;
                //tc_tot_monthqBF.BorderWidth = 1;
                //tc_tot_monthqBF.BackColor = System.Drawing.Color.White;
                //tc_tot_monthqBF.Width = 200;
                //tc_tot_monthqBF.Style.Add("font-family", "Calibri");
                //tc_tot_monthqBF.Style.Add("font-size", "10pt");
                //tc_tot_monthqBF.HorizontalAlign = HorizontalAlign.Center;
                //tc_tot_monthqBF.VerticalAlign = VerticalAlign.Middle;
                //tc_tot_monthqBF.Controls.Add(hyp_monthqBF);
                //tc_tot_monthqBF.Attributes.Add("style", "font-weight:bold;");
                //tc_tot_monthqBF.Attributes.Add("Class", "rptCellBorder");
                //tr_det.Cells.Add(tc_tot_monthqBF);
                //tot_drLOP = "";
                //UTILITY
                dsDoctor = dr.getDCR_Leave_Type(divcode);
                if (dsDoctor.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dataRow in dsDoctor.Tables[0].Rows)
                    {
                        dsDoc = sf.get_leave_days(drFF["Sf_Code"].ToString(), divcode, dataRow["Leave_SName"].ToString(), cyear);
                        if (dsDoc.Tables[0].Rows.Count > 0)
                            tot_drcl = Convert.ToInt32(dsDoc.Tables[0].Rows[0].ItemArray.GetValue(0));
                        TableCell tc_tot_monthqEa = new TableCell();
                        tc_tot_monthqEa.Visible = false;
                         HyperLink hyp_monthqEa = new HyperLink();
                        hyp_monthqEa.Text = tot_drcl.ToString();
                        tc_tot_monthqEa.BorderStyle = BorderStyle.Solid;
                        tc_tot_monthqEa.BorderWidth = 1;
                        tc_tot_monthqEa.BackColor = System.Drawing.Color.White;
                        tc_tot_monthqEa.Width = 200;
                        tc_tot_monthqEa.Style.Add("font-family", "Calibri");
                        tc_tot_monthqEa.Style.Add("font-size", "10pt");
                        tc_tot_monthqEa.HorizontalAlign = HorizontalAlign.Center;
                        tc_tot_monthqEa.VerticalAlign = VerticalAlign.Middle;
                        tc_tot_monthqEa.Controls.Add(hyp_monthqEa);
                        tc_tot_monthqEa.Attributes.Add("style", "font-weight:bold;");
                        tc_tot_monthqEa.Attributes.Add("Class", "rptCellBorder");
                        tr_det.Cells.Add(tc_tot_monthqEa);
                       
                    //}}
                dsDoctor = dr.getDCR_Leave_Type(divcode);
                //if (dsDoctor.Tables[0].Rows.Count > 0)
                //{
                //    foreach (DataRow dataRow in dsDoctor.Tables[0].Rows)
                //    {

                        dsDoc = sf.getDCR_Leave_utilitycount(drFF["sf_code"].ToString(), divcode, strmonth_Count, cyear, Convert.ToInt32(dataRow["Leave_code"].ToString()));
                        if (dsDoc.Tables[0].Rows.Count > 0)
                            totuticount = Convert.ToInt32(dsDoc.Tables[0].Rows[0].ItemArray.GetValue(0));
                        TableCell tc_tot_monthqUT = new TableCell();
                        tc_tot_monthqUT.Visible = false;
                        HyperLink hyp_monthqUT = new HyperLink();
                        hyp_monthqUT.Text = totuticount.ToString();
                        tc_tot_monthqUT.BorderStyle = BorderStyle.Solid;
                        tc_tot_monthqUT.BorderWidth = 1;
                        tc_tot_monthqUT.BackColor = System.Drawing.Color.White;
                        tc_tot_monthqUT.Width = 200;
                        tc_tot_monthqUT.Style.Add("font-family", "Calibri");
                        tc_tot_monthqUT.Style.Add("font-size", "10pt");
                        tc_tot_monthqUT.HorizontalAlign = HorizontalAlign.Center;
                        tc_tot_monthqUT.VerticalAlign = VerticalAlign.Middle;
                        tc_tot_monthqUT.Controls.Add(hyp_monthqUT);
                        tc_tot_monthqUT.Attributes.Add("style", "font-weight:bold;");
                        tc_tot_monthqUT.Attributes.Add("Class", "rptCellBorder");
                        tr_det.Cells.Add(tc_tot_monthqUT);
                        //double totminus = Convert.ToDouble(tot_drcl - totuticount);
                        totminus = tot_drcl - totuticount;
                        //totminus += tot_drcl;
                        totuticount = 0;
                        tot_drcl = 0;
                        TableCell tc_tot_monthqEUTI = new TableCell();
                        HyperLink hyp_monthqEUTI = new HyperLink();
                        hyp_monthqEUTI.Text = totminus.ToString();
                        tc_tot_monthqEUTI.BorderStyle = BorderStyle.Solid;
                        tc_tot_monthqEUTI.BorderWidth = 1;
                        tc_tot_monthqEUTI.BackColor = System.Drawing.Color.White;
                        tc_tot_monthqEUTI.Width = 200;
                        tc_tot_monthqEUTI.Style.Add("font-family", "Calibri");
                        tc_tot_monthqEUTI.Style.Add("font-size", "10pt");
                        tc_tot_monthqEUTI.HorizontalAlign = HorizontalAlign.Center;
                        tc_tot_monthqEUTI.VerticalAlign = VerticalAlign.Middle;
                        tc_tot_monthqEUTI.Controls.Add(hyp_monthqEUTI);
                        tc_tot_monthqEUTI.Attributes.Add("style", "font-weight:bold;");
                        tc_tot_monthqEUTI.Attributes.Add("Class", "rptCellBorder");
                        tr_det.Cells.Add(tc_tot_monthqEUTI);
                        totminus = 0;

                    }
                }


                
                //TableCell tc_tot_monthqBUTI = new TableCell();
                //HyperLink hyp_monthqBUTI = new HyperLink();
                //hyp_monthqBUTI.Text = tot_drsl;
                //tc_tot_monthqBUTI.BorderStyle = BorderStyle.Solid;
                //tc_tot_monthqBUTI.BorderWidth = 1;
                //tc_tot_monthqBUTI.BackColor = System.Drawing.Color.White;
                //tc_tot_monthqBUTI.Width = 200;
                //tc_tot_monthqBUTI.Style.Add("font-family", "Calibri");
                //tc_tot_monthqBUTI.Style.Add("font-size", "10pt");
                //tc_tot_monthqBUTI.HorizontalAlign = HorizontalAlign.Center;
                //tc_tot_monthqBUTI.VerticalAlign = VerticalAlign.Middle;
                //tc_tot_monthqBUTI.Controls.Add(hyp_monthqBUTI);
                //tc_tot_monthqBUTI.Attributes.Add("style", "font-weight:bold;");
                //tc_tot_monthqBUTI.Attributes.Add("Class", "rptCellBorder");
                //tr_det.Cells.Add(tc_tot_monthqBUTI);
                //tot_drsl = "";
                //TableCell tc_tot_UTI = new TableCell();
                //HyperLink hyp_uti = new HyperLink();
                //hyp_uti.Text = tot_drsl;
                //tc_tot_UTI.BorderStyle = BorderStyle.Solid;
                //tc_tot_UTI.BorderWidth = 1;
                //tc_tot_UTI.BackColor = System.Drawing.Color.White;
                //tc_tot_UTI.Width = 200;
                //tc_tot_UTI.Style.Add("font-family", "Calibri");
                //tc_tot_UTI.Style.Add("font-size", "10pt");
                //tc_tot_UTI.HorizontalAlign = HorizontalAlign.Center;
                //tc_tot_UTI.VerticalAlign = VerticalAlign.Middle;
                //tc_tot_UTI.Controls.Add(hyp_uti);
                //tc_tot_UTI.Attributes.Add("style", "font-weight:bold;");
                //tc_tot_UTI.Attributes.Add("Class", "rptCellBorder");
                //tr_det.Cells.Add(tc_tot_UTI);
                //tot_drsl = "";


                //tbl.Rows.Add(tr_total);
                tbl.Rows.Add(tr_det);

                
            }
          

        }
    }

    

    protected void btnPrint_Click(object sender, EventArgs e)
    {
        Session["ctrl"] = pnlContents;
        Control ctrl = (Control)Session["ctrl"];
        PrintWebControl(ctrl);
    }
    public static void PrintWebControl(Control ControlToPrint)
    {
        StringWriter stringWrite = new StringWriter();
        System.Web.UI.HtmlTextWriter htmlWrite = new System.Web.UI.HtmlTextWriter(stringWrite);
        if (ControlToPrint is WebControl)
        {
            Unit w = new Unit(100, UnitType.Percentage);
            ((WebControl)ControlToPrint).Width = w;
        }
        Page pg = new Page();
        pg.EnableEventValidation = false;
        HtmlForm frm = new HtmlForm();
        pg.Controls.Add(frm);
        frm.Attributes.Add("runat", "server");
        frm.Controls.Add(ControlToPrint);
        pg.DesignerInitialize();
        pg.RenderControl(htmlWrite);
        string strHTML = stringWrite.ToString();
        HttpContext.Current.Response.Clear();
        HttpContext.Current.Response.Write(strHTML);
        HttpContext.Current.Response.Write("<script>window.print();</script>");
        HttpContext.Current.Response.End();

    }

    protected void btnExcel_Click(object sender, EventArgs e)
    {
        string attachment = "attachment; filename=DCRView.xls";
        Response.ClearContent();
        Response.AddHeader("content-disposition", attachment);
        Response.ContentType = "application/ms-excel";
        StringWriter sw = new StringWriter();
        HtmlTextWriter htw = new HtmlTextWriter(sw);
        HtmlForm frm = new HtmlForm();
        form1.Parent.Controls.Add(frm);
        frm.Attributes["runat"] = "server";
        frm.Controls.Add(pnlContents);
        frm.RenderControl(htw);
        Response.Write(sw.ToString());
        Response.End();
    }

    protected void btnClose_Click(object sender, EventArgs e)
    {

    }
}