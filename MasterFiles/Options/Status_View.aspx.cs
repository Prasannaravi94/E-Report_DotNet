using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;

public partial class MIS_Reports_doctorbusview : System.Web.UI.Page
{
    string div_code = string.Empty;
    DataSet dsSalesForce = null;
    DataSet dsDoc = null;
    DataSet dsDoc1 = null;
    DataSet dsDoc2 = null;
    string tot_dr = string.Empty;
    string total_doc = string.Empty;
    string tot_FWD = string.Empty;
    string tot_dcr_dr = string.Empty;
    string tot_dr1 = string.Empty;
    string total_doc1 = string.Empty;
    string tot_dr2 = string.Empty;
    string total_doc2 = string.Empty;
    string sCurrentDate = string.Empty;
    string sf_code = string.Empty;
    string sf_type = string.Empty;
    string sfcsts = string.Empty;
    string MultiSf_Code = string.Empty;
    int time = 0;
    string strSf_Code = string.Empty;
    DateTime ServerStartTime;
    DateTime ServerEndTime;

    protected void Page_Load(object sender, EventArgs e)
    {
        sf_code = Session["sf_code"].ToString();
        sf_type = Session["sf_type"].ToString();

        if (sf_type == "3")
        {
            div_code = Session["div_code"].ToString();
        }
        else
        {
            div_code = Session["div_code"].ToString();
        }
        if (!Page.IsPostBack)
        {
            ServerStartTime = DateTime.Now;
            base.OnPreInit(e);

            if (Session["sf_type"].ToString() == "2")
            {
                div_code = Session["div_code"].ToString();
                UserControl_MGR_Menu c1 =
               (UserControl_MGR_Menu)LoadControl("~/UserControl/MGR_Menu.ascx");
                Divid.Controls.Add(c1);
                c1.Title = Page.Title;
                // c1.FindControl("btnBack").Visible = false;
            }

            else if (Session["sf_type"].ToString() == "" || Session["sf_type"].ToString() == "3")
            {
                div_code = Session["div_code"].ToString();
                if (div_code.Contains(','))
                {
                    div_code = div_code.Remove(div_code.Length - 1);
                }
                UserControl_MenuUserControl c1 =
               (UserControl_MenuUserControl)LoadControl("~/UserControl/MenuUserControl.ascx");
                Divid.Controls.Add(c1);
                c1.Title = Page.Title;
                // c1.FindControl("btnBack").Visible = false;
            }
            else if (Session["sf_type"].ToString() == "1")
            {
                div_code = Session["div_code"].ToString();
                UserControl_MR_Menu c1 =
               (UserControl_MR_Menu)LoadControl("~/UserControl/MR_Menu.ascx");
                Divid.Controls.Add(c1);
                c1.Title = Page.Title;
                // c1.FindControl("btnBack").Visible = false;
            }
        }
        else
        {
            if (Session["sf_type"].ToString() == "2")
            {
                div_code = Session["div_code"].ToString();
                UserControl_MGR_Menu c1 =
               (UserControl_MGR_Menu)LoadControl("~/UserControl/MGR_Menu.ascx");
                Divid.Controls.Add(c1);
                c1.Title = Page.Title;
                // c1.FindControl("btnBack").Visible = false;

            }
            else if (Session["sf_type"].ToString() == "" || Session["sf_type"].ToString() == "3")
            {
                div_code = Session["div_code"].ToString();
                if (div_code.Contains(','))
                {
                    div_code = div_code.Remove(div_code.Length - 1);
                }
                UserControl_MenuUserControl c1 =
               (UserControl_MenuUserControl)LoadControl("~/UserControl/MenuUserControl.ascx");
                Divid.Controls.Add(c1);
                c1.Title = Page.Title;
                // c1.FindControl("btnBack").Visible = false;

            }
            else if (Session["sf_type"].ToString() == "1")
            {
                div_code = Session["div_code"].ToString();
                UserControl_MR_Menu c1 =
               (UserControl_MR_Menu)LoadControl("~/UserControl/MR_Menu.ascx");
                Divid.Controls.Add(c1);
                c1.Title = Page.Title;
                // c1.FindControl("btnBack").Visible = false;
            }
        }
    }
    protected override void OnLoadComplete(EventArgs e)
    {
        ServerEndTime = DateTime.Now;
        TrackPageTime();//It will give you page load time 
    }
    public void TrackPageTime()
    {
        TimeSpan serverTimeDiff = ServerEndTime.Subtract(ServerStartTime);
        time = serverTimeDiff.Minutes;
    }
    protected void ddlFieldForce_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void txtNew_TextChanged(object sender, EventArgs e)
    {
        ddlFieldForce_SelectedIndexChanged(sender, e);
    }
    protected void btnGo_Click(object sender, EventArgs e)
    {
        if (Session["sf_type"].ToString() == "1")
        {
            FillSalesForce();
        }
        else if (Session["sf_type"].ToString() == "2")
        {
            FillSalesForceMGR();
        }
    }

    private void FillSalesForce()
    {
        string sURL = string.Empty;

        tbl.Rows.Clear();
        //doctor_total = 0;

        SalesForce sf = new SalesForce();
        DataTable dt = new DataTable();
        DataSet ds = new DataSet();

        if (ddlmode.SelectedValue == "4")
        {
            dsSalesForce = null;
        }
        else
        {
            ds = sf.getReportingTo(sf_code);
            string repTo = ds.Tables[0].Rows[0][0].ToString();

            dsSalesForce = sf.sp_UserList_getMR_Doc_List(div_code, repTo);

            var rows = (from row in dsSalesForce.Tables[0].AsEnumerable()
                        where row.Field<string>("SF_Code").Trim() == sf_code
                        select row).DefaultIfEmpty(null);

            if (rows.ElementAtOrDefault(0) != null)
            {
                dt = rows.CopyToDataTable();
            }
        }

        if (dt.Rows.Count > 0)
        {
            TableRow tr_header = new TableRow();
            //tr_header.BorderStyle = BorderStyle.Solid;
            //tr_header.BorderWidth = 1;
            tr_header.Attributes.Add("Class", "tr_th");

            tr_header.BackColor = System.Drawing.Color.FromName("#F1F5F8");
            //tr_header.Style.Add("Color", "White");
            //tr_header.BorderColor = System.Drawing.Color.Black;

            TableCell tc_SNo = new TableCell();
            tc_SNo.BackColor = System.Drawing.Color.FromName("#414d55");
            tc_SNo.Style.Add("Color", "White");
            //tc_SNo.BorderStyle = BorderStyle.Solid;
            //tc_SNo.BorderWidth = 1;
            //tc_SNo.Width = 50;
            tc_SNo.RowSpan = 1;
            Literal lit_SNo = new Literal();
            lit_SNo.Text = "<center>#</center>";
            tc_SNo.Attributes.Add("Class", "tr_Sno");
            //tc_SNo.BorderColor = System.Drawing.Color.Black;
            tc_SNo.Controls.Add(lit_SNo);
            //tc_SNo.Style.Add("font-family", "Calibri");
            //tc_SNo.Style.Add("font-size", "10pt");
            //tc_SNo.Attributes.Add("Class", "rptCellBorder");
            tr_header.Cells.Add(tc_SNo);

            TableCell tc_DR_Code = new TableCell();
            //tc_DR_Code.BorderStyle = BorderStyle.Solid;
            //tc_DR_Code.BorderWidth = 1;
            //tc_DR_Code.Width = 400;
            tc_DR_Code.RowSpan = 1;
            Literal lit_DR_Code = new Literal();
            lit_DR_Code.Text = "<center>SF Code</center>";
            tc_DR_Code.Controls.Add(lit_DR_Code);
            //tc_DR_Code.Style.Add("font-family", "Calibri");
            //tc_DR_Code.Style.Add("font-size", "10pt");
            //tc_DR_Code.Attributes.Add("Class", "rptCellBorder");
            //tc_DR_Code.BorderColor = System.Drawing.Color.Black;
            tc_DR_Code.Visible = false;
            tr_header.Cells.Add(tc_DR_Code);

            TableCell tc_DR_Name = new TableCell();
            //tc_DR_Name.BorderStyle = BorderStyle.Solid;
            //tc_DR_Name.BorderWidth = 1;
            //tc_DR_Name.Width = 200;
            tc_DR_Name.RowSpan = 1;
            Literal lit_DR_Name = new Literal();
            lit_DR_Name.Text = "<center>Fieldforce&nbspName</center>";
            //tc_DR_Name.BorderColor = System.Drawing.Color.Black;
            //tc_DR_Name.Style.Add("font-family", "Calibri");
            //tc_DR_Name.Style.Add("font-size", "10pt");
            //tc_DR_Name.Attributes.Add("Class", "rptCellBorder");
            tc_DR_Name.Controls.Add(lit_DR_Name);
            tr_header.Cells.Add(tc_DR_Name);


            TableCell tc_DR_HQ = new TableCell();
            //tc_DR_HQ.BorderStyle = BorderStyle.Solid;
            //tc_DR_HQ.BorderWidth = 1;
            //tc_DR_HQ.Width = 100;
            tc_DR_HQ.RowSpan = 1;
            Literal lit_DR_HQ = new Literal();
            lit_DR_HQ.Text = "<center>HQ</center>";
            //tc_DR_HQ.BorderColor = System.Drawing.Color.Black;
            //tc_DR_HQ.Style.Add("font-family", "Calibri");
            //tc_DR_HQ.Style.Add("font-size", "10pt");
            //tc_DR_HQ.Attributes.Add("Class", "rptCellBorder");
            tc_DR_HQ.Controls.Add(lit_DR_HQ);
            tr_header.Cells.Add(tc_DR_HQ);


            TableCell tc_DR_Des = new TableCell();
            //tc_DR_Des.BorderStyle = BorderStyle.Solid;
            //tc_DR_Des.BorderWidth = 1;
            //tc_DR_Des.Width = 80;
            tc_DR_Des.RowSpan = 1;
            Literal lit_DR_Des = new Literal();
            lit_DR_Des.Text = "<center>Designation</center>";
            //tc_DR_Des.BorderColor = System.Drawing.Color.Black;
            //tc_DR_Des.Style.Add("font-family", "Calibri");
            //tc_DR_Des.Style.Add("font-size", "10pt");
            //tc_DR_Des.Attributes.Add("Class", "rptCellBorder");
            tc_DR_Des.Controls.Add(lit_DR_Des);
            tr_header.Cells.Add(tc_DR_Des);

            TableCell tc_status = new TableCell();
            //tc_status.BorderStyle = BorderStyle.Solid;
            //tc_status.BorderWidth = 1;
            //tc_status.Width = 80;
            tc_status.RowSpan = 1;
            Literal lit_status = new Literal();
            if (ddlmode.SelectedValue == "5")
            {
                lit_status.Text = "<center>No.of.Lst.drs</center>";
            }
            else
            {
                lit_status.Text = "<center>Status</center>";
            }
            //tc_status.BorderColor = System.Drawing.Color.Black;
            //tc_status.Attributes.Add("Class", "rptCellBorder");
            //tc_status.Style.Add("font-family", "Calibri");
            //tc_status.Style.Add("font-size", "10pt");
            tc_status.Controls.Add(lit_status);
            tr_header.Cells.Add(tc_status);
            if (ddlmode.SelectedValue == "5")
            {
                TableCell tc_status2 = new TableCell();
                //tc_status2.BorderStyle = BorderStyle.Solid;
                //tc_status2.BorderWidth = 1;
                //tc_status2.Width = 80;
                tc_status2.RowSpan = 1;
                Literal lit_status2 = new Literal();
                lit_status2.Text = "<center>Tagged drs</center>";
                //tc_status2.BorderColor = System.Drawing.Color.Black;
                //tc_status2.Attributes.Add("Class", "rptCellBorder");
                //tc_status2.Style.Add("font-family", "Calibri");
                //tc_status2.Style.Add("font-size", "10pt");
                tc_status2.Controls.Add(lit_status2);
                tr_header.Cells.Add(tc_status2);

                TableCell tc_status3 = new TableCell();
                //tc_status3.BorderStyle = BorderStyle.Solid;
                //tc_status3.BorderWidth = 1;
                //tc_status3.Width = 80;
                tc_status3.RowSpan = 1;
                Literal lit_status3 = new Literal();
                lit_status3.Text = "<center>UnTagged drs</center>";
                tc_status3.BorderColor = System.Drawing.Color.Black;
                //tc_status3.Attributes.Add("Class", "rptCellBorder");
                //tc_status3.Style.Add("font-family", "Calibri");
                //tc_status3.Style.Add("font-size", "10pt");
                tc_status3.Controls.Add(lit_status3);
                tr_header.Cells.Add(tc_status3);
            }

            tbl.Rows.Add(tr_header);

            if (Session["sf_type"].ToString() == "" || Session["sf_type"].ToString() == "3")
            {
                //tr_header.BackColor = System.Drawing.Color.FromName("#336277");
            }

            if (dt.Rows.Count > 0)
                ViewState["dsSalesForce"] = dsSalesForce;


            int iCount = 0;
            int iTotLstCount = 0;
            dsSalesForce = (DataSet)ViewState["dsSalesForce"];

            foreach (DataRow drFF in dt.Rows)
            {
                TableRow tr_det = new TableRow();
                iCount += 1;
                strSf_Code += "'" + drFF["sf_code"].ToString() + "'" + ",";


                //S.No
                TableCell tc_det_SNo = new TableCell();
                Literal lit_det_SNo = new Literal();
                lit_det_SNo.Text = "<center>" + iCount.ToString() + "</center>";
                //tc_det_SNo.BorderStyle = BorderStyle.Solid;
                //tc_det_SNo.BorderWidth = 1;
                //tc_det_SNo.Style.Add("font-family", "Calibri");
                //tc_det_SNo.Style.Add("font-size", "10pt");
                //tc_det_SNo.Style.Add("text-align", "left");
                tc_det_SNo.Attributes.Add("Class", "rptCellBorder");
                tc_det_SNo.Controls.Add(lit_det_SNo);
                tr_det.Cells.Add(tc_det_SNo);
                //tr_det.BackColor = System.Drawing.Color.White;

                //SF_code
                TableCell tc_det_usr = new TableCell();
                Literal lit_det_usr = new Literal();
                lit_det_usr.Text = "&nbsp;" + drFF["Sf_Code"].ToString();
                //tc_det_usr.BorderStyle = BorderStyle.Solid;
                //tc_det_usr.BorderWidth = 1;
                tc_det_usr.Visible = false;
                //tc_det_usr.Style.Add("font-family", "Calibri");
                //tc_det_usr.Style.Add("font-size", "10pt");
                //tc_det_usr.Style.Add("text-align", "left");
                tc_det_usr.Attributes.Add("Class", "rptCellBorder");
                tc_det_usr.Controls.Add(lit_det_usr);
                tr_det.Cells.Add(tc_det_usr);

                //SF Name
                TableCell tc_det_FF = new TableCell();
                Literal lit_det_FF = new Literal();
                lit_det_FF.Text = "&nbsp;" + drFF["sf_name"].ToString();
                //tc_det_FF.BorderStyle = BorderStyle.Solid;
                //tc_det_FF.BorderWidth = 1;
                //tc_det_FF.Style.Add("font-family", "Calibri");
                //tc_det_FF.Style.Add("font-size", "10pt");
                //tc_det_FF.Style.Add("text-align", "left");
                tc_det_FF.Attributes.Add("Class", "rptCellBorder");
                tc_det_FF.Controls.Add(lit_det_FF);
                tr_det.Cells.Add(tc_det_FF);

                //hq
                TableCell tc_det_hq = new TableCell();
                Literal lit_det_hq = new Literal();
                lit_det_hq.Text = "&nbsp;" + drFF["sf_hq"].ToString();
                //tc_det_hq.BorderStyle = BorderStyle.Solid;
                //tc_det_hq.BorderWidth = 1;
                //tc_det_hq.Style.Add("font-family", "Calibri");
                //tc_det_hq.Style.Add("font-size", "10pt");
                //tc_det_hq.Style.Add("text-align", "left");
                tc_det_hq.Attributes.Add("Class", "rptCellBorder");
                tc_det_hq.Controls.Add(lit_det_hq);
                tr_det.Cells.Add(tc_det_hq);

                //SF Designation Short Name
                TableCell tc_det_Designation = new TableCell();
                Literal lit_det_Designation = new Literal();
                if (ddlmode.SelectedValue == "4")
                {
                    lit_det_Designation.Text = "&nbsp;" + drFF["Designation_Short_Name"].ToString();
                }
                else
                {
                    lit_det_Designation.Text = "&nbsp;" + drFF["sf_Designation_Short_Name"].ToString();
                }
                //tc_det_Designation.BorderStyle = BorderStyle.Solid;
                //tc_det_Designation.BorderWidth = 1;
                //tc_det_Designation.Style.Add("font-family", "Calibri");
                //tc_det_Designation.Style.Add("font-size", "10pt");
                //tc_det_Designation.Style.Add("text-align", "left");
                tc_det_Designation.Controls.Add(lit_det_Designation);
                tc_det_Designation.Attributes.Add("Class", "rptCellBorder");
                tr_det.Cells.Add(tc_det_Designation);


                TableCell tc_tot_month = new TableCell();
                HyperLink hyp_month = new HyperLink();
                iTotLstCount = 0;


                //if (ddlmode.SelectedValue == "1")
                //{

                dsDoc = sf.getDrprdMap_Status(drFF["sf_code"].ToString(), div_code, ddlmode.SelectedValue);


                //}



                TableCell tc_lst_month = new TableCell();
                HyperLink hyp_lst_month = new HyperLink();
                if (ddlmode.SelectedValue == "3")
                {
                    if (dsDoc.Tables[0].Rows.Count > 0)
                        sfcsts = dsDoc.Tables[0].Rows[0].ItemArray.GetValue(1).ToString();
                    if (sfcsts == "*")
                    {
                        hyp_lst_month.ImageUrl = "../Images/correct.png";
                    }
                    else
                    {
                        hyp_lst_month.Text = "-";
                    }
                }
                else if (ddlmode.SelectedValue == "7")
                {
                    if (dsDoc.Tables[0].Rows.Count > 0)
                        tot_dr = dsDoc.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();

                    if (tot_dr != "0")
                    {

                        hyp_lst_month.Text = tot_dr;
                        sURL = "&sf_code=" + drFF["Sf_Code"] + "&sf_name=" + drFF["sf_name"] + "";

                        hyp_lst_month.Attributes.Add("href", "javascript:showModalPopUp2('" + drFF["sf_code"].ToString() + "','" + drFF["sf_name"] + "')");
                        hyp_lst_month.Attributes["onclick"] = "javascript:showModalPopUp2('" + sURL + "')";
                        hyp_lst_month.NavigateUrl = "#";
                        hyp_lst_month.BackColor = System.Drawing.Color.Yellow;
                        //hyp_lst_month.Width = 50;
                    }

                    else
                    {
                        hyp_lst_month.Text = "-";
                    }
                }
                else if (ddlmode.SelectedValue == "8")
                {
                    if (dsDoc.Tables[0].Rows.Count > 0)
                    {
                        hyp_lst_month.Text = "Yes";
                        //hyp_lst_month.BackColor = System.Drawing.Color.Yellow;
                    }
                    else
                    {
                        hyp_lst_month.Text = "-";
                    }
                }
                else if (ddlmode.SelectedValue == "9")
                {
                    if (dsDoc.Tables[0].Rows.Count > 0)
                    {
                        hyp_lst_month.Text = "Yes";
                        //hyp_lst_month.BackColor = System.Drawing.Color.Yellow;
                    }
                    else
                    {
                        hyp_lst_month.Text = "-";
                    }
                }
                else if (ddlmode.SelectedValue == "4")
                {
                    if (dsDoc.Tables[0].Rows.Count > 0)
                    {
                        tot_dr = dsDoc.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                    }
                    else
                    {
                        tot_dr = "0";
                    }


                    if (tot_dr != "0")
                    {

                        hyp_lst_month.Text = tot_dr;
                        sURL = "&sf_code=" + drFF["Sf_Code"] + "&sf_name=" + drFF["sf_name"] + "";

                        hyp_lst_month.Attributes.Add("href", "javascript:showModalPopUp1('" + drFF["sf_code"].ToString() + "','" + drFF["sf_name"] + "')");
                        hyp_lst_month.Attributes["onclick"] = "javascript:showModalPopUp1('" + sURL + "')";
                        hyp_lst_month.NavigateUrl = "#";
                        //hyp_lst_month.BackColor = System.Drawing.Color.Yellow;
                        //hyp_lst_month.Width = 50;


                    }

                    else
                    {
                        hyp_lst_month.Text = "-";
                    }
                }
                else
                {
                    if (ddlmode.SelectedValue == "5")
                    {
                        tot_dr = dsDoc.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();

                    }
                    else
                    {
                        tot_dr = dsDoc.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                        total_doc = dsDoc.Tables[0].Rows[0].ItemArray.GetValue(1).ToString();

                    }

                    if (tot_dr != "0")
                    {
                        //iTotLstCount += Convert.ToInt16(tot_dr);
                        if (ddlmode.SelectedValue == "5")
                        {
                            hyp_lst_month.Text = tot_dr;
                        }
                        else
                        {
                            hyp_lst_month.Text = tot_dr + "/" + total_doc;
                            if (ddlmode.SelectedValue == "1" || ddlmode.SelectedValue == "6" || ddlmode.SelectedValue == "2")
                            {
                                sURL = "&sf_code=" + drFF["Sf_Code"] + "&sf_name=" + drFF["sf_name"] + "&sf_short=" + drFF["sf_Designation_Short_Name"] + "&sf_hq=" + drFF["Sf_HQ"] + "";
                                if (ddlmode.SelectedValue == "1")
                                {
                                    hyp_lst_month.Attributes.Add("href", "javascript:showModalPopUp('" + drFF["sf_code"].ToString() + "', '" + drFF["sf_name"] + "','" + drFF["sf_Designation_Short_Name"] + "','" + drFF["Sf_HQ"] + "')");
                                    hyp_lst_month.Attributes["onclick"] = "javascript:showModalPopUp('" + sURL + "')";
                                }
                                else if (ddlmode.SelectedValue == "2")
                                {
                                    string URL = sURL;
                                    hyp_lst_month.Attributes.Add("href", "javascript:showModalPopUp22('" + drFF["sf_code"].ToString() + "', '" + drFF["sf_name"] + "','" + drFF["sf_Designation_Short_Name"] + "','" + drFF["Sf_HQ"] + "','" + div_code + "')");
                                    hyp_lst_month.Attributes["onclick"] = "javascript:showModalPopUp22('" + sURL + "')";
                                }
                                else
                                {
                                    hyp_lst_month.Attributes.Add("href", "javascript:showModalPopUp5('" + drFF["sf_code"].ToString() + "', '" + drFF["sf_name"] + "','" + drFF["sf_Designation_Short_Name"] + "','" + drFF["Sf_HQ"] + "')");
                                    hyp_lst_month.Attributes["onclick"] = "javascript:showModalPopUp5('" + sURL + "')";
                                }
                                hyp_lst_month.NavigateUrl = "#";
                                //hyp_lst_month.BackColor = System.Drawing.Color.Yellow;
                            }
                        }


                        if (ddlmode.SelectedValue == "5")
                        {
                            sURL = "&sf_code=" + drFF["Sf_Code"] + "&sf_name=" + drFF["sf_name"] + "&sf_short=" + drFF["sf_Designation_Short_Name"] + "&sf_hq=" + drFF["Sf_HQ"] + "";

                            hyp_lst_month.Attributes.Add("href", "javascript:showModalPopUp3('" + drFF["sf_code"].ToString() + "', '" + drFF["sf_name"] + "','" + drFF["sf_Designation_Short_Name"] + "','" + drFF["Sf_HQ"] + "')");
                            hyp_lst_month.Attributes["onclick"] = "javascript:showModalPopUp3('" + sURL + "')";
                            hyp_lst_month.NavigateUrl = "#";
                            //hyp_lst_month.BackColor = System.Drawing.Color.White;
                        }
                        else
                        {
                            //hyp_lst_month.BackColor = System.Drawing.Color.Yellow;
                        }

                    }

                    else
                    {
                        hyp_lst_month.Text = "-";
                    }
                }

                //tc_lst_month.BorderStyle = BorderStyle.Solid;
                //tc_lst_month.BorderWidth = 1;
                //tc_lst_month.BackColor = System.Drawing.Color.White;
                //tc_lst_month.Width = 200;
                //tc_lst_month.HorizontalAlign = HorizontalAlign.Center;
                //tc_lst_month.VerticalAlign = VerticalAlign.Middle;
                tc_lst_month.Controls.Add(hyp_lst_month);
                //tc_lst_month.Style.Add("font-family", "Calibri");
                //tc_lst_month.Style.Add("font-size", "10pt");
                //tc_lst_month.Style.Add("text-align", "center");
                //tc_lst_month.Attributes.Add("Class", "rptCellBorder");
                tr_det.Cells.Add(tc_lst_month);
                if (ddlmode.SelectedValue == "5")
                {

                    dsDoc1 = sf.getDrgeoMap_Status(drFF["sf_code"].ToString(), div_code, ddlmode.SelectedValue);
                    TableCell tc_lst_month1 = new TableCell();
                    HyperLink hyp_lst_month1 = new HyperLink();
                    if (dsDoc1.Tables[0].Rows.Count > 0)


                        tot_dr1 = dsDoc1.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();






                    if (tot_dr1 != "0")
                    {
                        //iTotLstCount += Convert.ToInt16(tot_dr);
                        hyp_lst_month1.Text = tot_dr1;
                        sURL = "&sf_code=" + drFF["Sf_Code"] + "";
                        sURL = "Geo_ShowMap.aspx?sfcode=" + drFF["sf_code"].ToString() + " ";
                        hyp_lst_month1.Attributes["onclick"] = "javascript:window.open('" + sURL + "','_blank','PopUp',0,'resizable=yes,toolbar=no,menubar=no,status=no,width=1000,height=600,left=0,top=0');";
                        hyp_lst_month1.NavigateUrl = "#";




                    }

                    else
                    {
                        hyp_lst_month1.Text = "-";
                    }


                    //tc_lst_month1.BorderStyle = BorderStyle.Solid;
                    //tc_lst_month1.BorderWidth = 1;
                    //tc_lst_month1.BackColor = System.Drawing.Color.White;
                    //tc_lst_month1.Width = 200;
                    //tc_lst_month1.HorizontalAlign = HorizontalAlign.Center;
                    //tc_lst_month1.VerticalAlign = VerticalAlign.Middle;
                    tc_lst_month1.Controls.Add(hyp_lst_month1);
                    //tc_lst_month1.Style.Add("font-family", "Calibri");
                    //tc_lst_month1.Style.Add("font-size", "10pt");
                    //tc_lst_month1.Style.Add("text-align", "center");
                    //tc_lst_month1.Attributes.Add("Class", "rptCellBorder");
                    tr_det.Cells.Add(tc_lst_month1);

                    dsDoc2 = sf.getUnDrgeoMap_Status(drFF["sf_code"].ToString(), div_code, ddlmode.SelectedValue);
                    TableCell tc_lst_month2 = new TableCell();
                    HyperLink hyp_lst_month2 = new HyperLink();
                    if (dsDoc2.Tables[0].Rows.Count > 0)


                        tot_dr2 = dsDoc2.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();






                    if (tot_dr2 != "0")
                    {
                        //iTotLstCount += Convert.ToInt16(tot_dr);
                        hyp_lst_month2.Text = tot_dr2;
                        sURL = "&sf_code=" + drFF["Sf_Code"] + "";
                        //hyp_lst_month1.Attributes.Add("href", "javascript:showModalPopUp4('" + drFF["sf_code"].ToString() + "')");
                        //hyp_lst_month1.Attributes["onclick"] = "javascript:showModalPopUp4('" + sURL + "')";
                        //hyp_lst_month1.NavigateUrl = "#";
                        sURL = "GeoUnList_ShowMap.aspx?sfcode=" + drFF["sf_code"].ToString() + " ";
                        hyp_lst_month2.Attributes["onclick"] = "javascript:window.open('" + sURL + "','_blank','PopUp',0,'resizable=yes,toolbar=no,menubar=no,status=no,width=1000,height=600,left=0,top=0');";
                        hyp_lst_month2.NavigateUrl = "#";




                    }

                    else
                    {
                        hyp_lst_month2.Text = "-";
                    }


                    //tc_lst_month2.BorderStyle = BorderStyle.Solid;
                    //tc_lst_month2.BorderWidth = 1;
                    //tc_lst_month2.BackColor = System.Drawing.Color.White;
                    //tc_lst_month2.Width = 200;
                    //tc_lst_month2.HorizontalAlign = HorizontalAlign.Center;
                    //tc_lst_month2.VerticalAlign = VerticalAlign.Middle;
                    tc_lst_month2.Controls.Add(hyp_lst_month2);
                    //tc_lst_month2.Style.Add("font-family", "Calibri");
                    //tc_lst_month2.Style.Add("font-size", "10pt");
                    //tc_lst_month2.Style.Add("text-align", "center");
                    //tc_lst_month2.Attributes.Add("Class", "rptCellBorder");
                    tr_det.Cells.Add(tc_lst_month2);
                }
                tbl.Rows.Add(tr_det);
            }

        }
        else
        {
            TableRow tr_det_sno = new TableRow();
            TableCell tc_det_SNo = new TableCell();
            Literal lit_det_SNo = new Literal();
            lit_det_SNo.Text = "No Record Found";
            //tc_det_SNo.BorderStyle = BorderStyle.Solid;
            tc_det_SNo.Attributes.Add("Class", "NoRecord");

            ////tc_det_SNo.HorizontalAlign = HorizontalAlign.Center;
            ////tc_det_SNo.BorderWidth = 1;
            ////tc_det_SNo.BorderStyle = BorderStyle.None;
            tc_det_SNo.Controls.Add(lit_det_SNo);
            tr_det_sno.Cells.Add(tc_det_SNo);

            tbl.Rows.Add(tr_det_sno);
        }
    }

    private void FillSalesForceMGR()
    {
        string sURL = string.Empty;

        tbl.Rows.Clear();
        //doctor_total = 0;

        SalesForce sf = new SalesForce();
        if (ddlmode.SelectedValue == "4")
        {
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            ds = sf.SalesForceListMgrGet_MgrOnly(div_code, sf_code);

            var rows = (from row in ds.Tables[0].AsEnumerable()
                        where row.Field<string>("Sf_Code").Trim() == sf_code
                        select row).DefaultIfEmpty(null);

            if (rows.ElementAtOrDefault(0) != null)
            {
                dt = rows.CopyToDataTable();

                if (dt.Rows.Count > 0)
                {
                    dsSalesForce = new DataSet();
                    dsSalesForce.Tables.Add(dt.Copy());
                }
            }
        }
        else
        {
            dsSalesForce = sf.SalesForceList_New_GetMr(div_code, sf_code);
        }

        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            TableRow tr_header = new TableRow();
            //tr_header.Attributes.Add("border-collapse", "collapse");
            //tr_header.BorderStyle = BorderStyle.Solid;
            //tr_header.BorderWidth = 1;
            tr_header.Attributes.Add("Class", "tr_th");

            tr_header.BackColor = System.Drawing.Color.FromName("#f1f5f8");
            //tr_header.Style.Add("Color", "White");
            //tr_header.BorderColor = System.Drawing.Color.Black;

            TableCell tc_SNo = new TableCell();
            //tc_SNo.BorderStyle = BorderStyle.Solid;
            //tc_SNo.BorderWidth = 1;
            tc_SNo.BackColor = System.Drawing.Color.FromName("#414d55");
            tc_SNo.Width = 50;
            tc_SNo.RowSpan = 1;
            Literal lit_SNo =
                new Literal();
            lit_SNo.Text = "<center>#</center>";
            tc_SNo.ForeColor = System.Drawing.Color.White;
            tc_SNo.Controls.Add(lit_SNo);
            tc_SNo.Attributes.Add("Class", "tr_Sno");
            //tc_SNo.Style.Add("font-family", "Calibri");
            //tc_SNo.Style.Add("font-size", "10pt");
            //tc_SNo.Attributes.Add("Class", "rptCellBorder");
            tr_header.Cells.Add(tc_SNo);

            TableCell tc_DR_Code = new TableCell();
            //tc_DR_Code.BorderStyle = BorderStyle.Solid;
            //tc_DR_Code.BorderWidth = 1;
            //tc_DR_Code.Width = 400;
            tc_DR_Code.RowSpan = 1;
            Literal lit_DR_Code = new Literal();
            lit_DR_Code.Text = "<center>SF Code</center>";
            tc_DR_Code.Controls.Add(lit_DR_Code);
            //tc_DR_Code.Style.Add("font-family", "Calibri");
            //tc_DR_Code.Style.Add("font-size", "10pt");
            //tc_DR_Code.Attributes.Add("Class", "rptCellBorder");
            //tc_DR_Code.BorderColor = System.Drawing.Color.Black;
            tc_DR_Code.Visible = false;
            tr_header.Cells.Add(tc_DR_Code);

            TableCell tc_DR_Name = new TableCell();
            //tc_DR_Name.BorderStyle = BorderStyle.Solid;
            //tc_DR_Name.BorderWidth = 1;
            //tc_DR_Name.Width = 200;
            tc_DR_Name.RowSpan = 1;
            Literal lit_DR_Name = new Literal();
            lit_DR_Name.Text = "<center>Fieldforce&nbspName</center>";
            //tc_DR_Name.BorderColor = System.Drawing.Color.Black;
            //tc_DR_Name.Style.Add("font-family", "Calibri");
            //tc_DR_Name.Style.Add("font-size", "10pt");
            tc_DR_Name.Attributes.Add("Class", "rptCellBorder");
            tc_DR_Name.Controls.Add(lit_DR_Name);
            tr_header.Cells.Add(tc_DR_Name);


            TableCell tc_DR_HQ = new TableCell();
            //tc_DR_HQ.BorderStyle = BorderStyle.Solid;
            //tc_DR_HQ.BorderWidth = 1;
            //tc_DR_HQ.Width = 100;
            tc_DR_HQ.RowSpan = 1;
            Literal lit_DR_HQ = new Literal();
            lit_DR_HQ.Text = "<center>HQ</center>";
            //tc_DR_HQ.BorderColor = System.Drawing.Color.Black;
            //tc_DR_HQ.Style.Add("font-family", "Calibri");
            //tc_DR_HQ.Style.Add("font-size", "10pt");
            //tc_DR_HQ.Attributes.Add("Class", "rptCellBorder");
            tc_DR_HQ.Controls.Add(lit_DR_HQ);
            tr_header.Cells.Add(tc_DR_HQ);


            TableCell tc_DR_Des = new TableCell();
            //tc_DR_Des.BorderStyle = BorderStyle.Solid;
            //tc_DR_Des.BorderWidth = 1;
            //tc_DR_Des.Width = 80;
            tc_DR_Des.RowSpan = 1;
            Literal lit_DR_Des = new Literal();
            lit_DR_Des.Text = "<center>Designation</center>";
            //tc_DR_Des.BorderColor = System.Drawing.Color.Black;
            //tc_DR_Des.Style.Add("font-family", "Calibri");
            //tc_DR_Des.Style.Add("font-size", "10pt");
            //tc_DR_Des.Attributes.Add("Class", "rptCellBorder");
            tc_DR_Des.Controls.Add(lit_DR_Des);
            tr_header.Cells.Add(tc_DR_Des);

            TableCell tc_status = new TableCell();
            //tc_status.BorderStyle = BorderStyle.Solid;
            //tc_status.BorderWidth = 1;
            //tc_status.Width = 80;
            tc_status.RowSpan = 1;
            Literal lit_status = new Literal();
            if (ddlmode.SelectedValue == "5")
            {
                lit_status.Text = "<center>No.of.Lst.drs</center>";
            }
            else
            {
                lit_status.Text = "<center>Status</center>";
            }
            //tc_status.BorderColor = System.Drawing.Color.Black;
            //tc_status.Attributes.Add("Class", "rptCellBorder");
            //tc_status.Style.Add("font-family", "Calibri");
            //tc_status.Style.Add("font-size", "10pt");
            tc_status.Controls.Add(lit_status);
            tr_header.Cells.Add(tc_status);
            if (ddlmode.SelectedValue == "5")
            {
                TableCell tc_status2 = new TableCell();
                //tc_status2.BorderStyle = BorderStyle.Solid;
                //tc_status2.BorderWidth = 1;
                //tc_status2.Width = 80;
                tc_status2.RowSpan = 1;
                Literal lit_status2 = new Literal();
                lit_status2.Text = "<center>Tagged drs</center>";
                //tc_status2.BorderColor = System.Drawing.Color.Black;
                //tc_status2.Attributes.Add("Class", "rptCellBorder");
                //tc_status2.Style.Add("font-family", "Calibri");
                //tc_status2.Style.Add("font-size", "10pt");
                tc_status2.Controls.Add(lit_status2);
                tr_header.Cells.Add(tc_status2);

                TableCell tc_status3 = new TableCell();
                //tc_status3.BorderStyle = BorderStyle.Solid;
                //tc_status3.BorderWidth = 1;
                //tc_status3.Width = 80;
                tc_status3.RowSpan = 1;
                Literal lit_status3 = new Literal();
                lit_status3.Text = "<center>UnTagged drs</center>";
                //tc_status3.BorderColor = System.Drawing.Color.Black;
                //tc_status3.Attributes.Add("Class", "rptCellBorder");
                //tc_status3.Style.Add("font-family", "Calibri");
                //tc_status3.Style.Add("font-size", "10pt");
                tc_status3.Controls.Add(lit_status3);
                tr_header.Cells.Add(tc_status3);
            }

            tbl.Rows.Add(tr_header);

            if (Session["sf_type"].ToString() == "" || Session["sf_type"].ToString() == "3")
            {
                //tr_header.BackColor = System.Drawing.Color.FromName("#336277");
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
                lit_det_SNo.Text = "<center>" + iCount.ToString() + "</center>";
                //tc_det_SNo.BorderStyle = BorderStyle.Solid;
                //tc_det_SNo.BorderWidth = 1;
                //tc_det_SNo.Style.Add("font-family", "Calibri");
                //tc_det_SNo.Style.Add("font-size", "10pt");
                //tc_det_SNo.Style.Add("text-align", "left");
                tc_det_SNo.Attributes.Add("Class", "rptCellBorder");
                tc_det_SNo.Controls.Add(lit_det_SNo);
                tr_det.Cells.Add(tc_det_SNo);
                //tr_det.BackColor = System.Drawing.Color.White;

                //SF_code
                TableCell tc_det_usr = new TableCell();
                Literal lit_det_usr = new Literal();
                lit_det_usr.Text = "&nbsp;" + drFF["Sf_Code"].ToString();
                //tc_det_usr.BorderStyle = BorderStyle.Solid;
                //tc_det_usr.BorderWidth = 1;
                tc_det_usr.Visible = false;
                //tc_det_usr.Style.Add("font-family", "Calibri");
                //tc_det_usr.Style.Add("font-size", "10pt");
                //tc_det_usr.Style.Add("text-align", "left");
                tc_det_usr.Attributes.Add("Class", "rptCellBorder");
                tc_det_usr.Controls.Add(lit_det_usr);
                tr_det.Cells.Add(tc_det_usr);

                //SF Name
                TableCell tc_det_FF = new TableCell();
                Literal lit_det_FF = new Literal();
                lit_det_FF.Text = "&nbsp;" + drFF["sf_name"].ToString();
                //tc_det_FF.BorderStyle = BorderStyle.Solid;
                //tc_det_FF.BorderWidth = 1;
                //tc_det_FF.Style.Add("font-family", "Calibri");
                //tc_det_FF.Style.Add("font-size", "10pt");
                //tc_det_FF.Style.Add("text-align", "left");
                tc_det_FF.Attributes.Add("Class", "rptCellBorder");
                tc_det_FF.Controls.Add(lit_det_FF);
                tr_det.Cells.Add(tc_det_FF);

                //hq
                TableCell tc_det_hq = new TableCell();
                Literal lit_det_hq = new Literal();
                lit_det_hq.Text = "&nbsp;" + drFF["sf_hq"].ToString();
                //tc_det_hq.BorderStyle = BorderStyle.Solid;
                //tc_det_hq.BorderWidth = 1;
                //tc_det_hq.Style.Add("font-family", "Calibri");
                //tc_det_hq.Style.Add("font-size", "10pt");
                //tc_det_hq.Style.Add("text-align", "left");
                tc_det_hq.Attributes.Add("Class", "rptCellBorder");
                tc_det_hq.Controls.Add(lit_det_hq);
                tr_det.Cells.Add(tc_det_hq);

                //SF Designation Short Name
                TableCell tc_det_Designation = new TableCell();
                Literal lit_det_Designation = new Literal();
                if (ddlmode.SelectedValue == "4")
                {
                    lit_det_Designation.Text = "&nbsp;" + drFF["Designation_Short_Name"].ToString();
                }
                else
                {
                    lit_det_Designation.Text = "&nbsp;" + drFF["sf_Designation_Short_Name"].ToString();
                }
                //tc_det_Designation.BorderStyle = BorderStyle.Solid;
                //tc_det_Designation.BorderWidth = 1;
                //tc_det_Designation.Style.Add("font-family", "Calibri");
                //tc_det_Designation.Style.Add("font-size", "10pt");
                //tc_det_Designation.Style.Add("text-align", "left");
                tc_det_Designation.Controls.Add(lit_det_Designation);
                tc_det_Designation.Attributes.Add("Class", "rptCellBorder");
                tr_det.Cells.Add(tc_det_Designation);


                TableCell tc_tot_month = new TableCell();
                HyperLink hyp_month = new HyperLink();
                iTotLstCount = 0;


                //if (ddlmode.SelectedValue == "1")
                //{

                dsDoc = sf.getDrprdMap_Status(drFF["sf_code"].ToString(), div_code, ddlmode.SelectedValue);


                //}



                TableCell tc_lst_month = new TableCell();
                HyperLink hyp_lst_month = new HyperLink();
                if (ddlmode.SelectedValue == "3")
                {
                    if (dsDoc.Tables[0].Rows.Count > 0)
                        sfcsts = dsDoc.Tables[0].Rows[0].ItemArray.GetValue(1).ToString();
                    if (sfcsts == "*")
                    {
                        hyp_lst_month.ImageUrl = "../Images/correct.png";
                    }
                    else
                    {
                        hyp_lst_month.Text = "-";
                    }
                }
                else if (ddlmode.SelectedValue == "7")
                {
                    if (dsDoc.Tables[0].Rows.Count > 0)
                        tot_dr = dsDoc.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();

                    if (tot_dr != "0")
                    {

                        hyp_lst_month.Text = tot_dr;
                        sURL = "&sf_code=" + drFF["Sf_Code"] + "&sf_name=" + drFF["sf_name"] + "";

                        hyp_lst_month.Attributes.Add("href", "javascript:showModalPopUp2('" + drFF["sf_code"].ToString() + "','" + drFF["sf_name"] + "')");
                        hyp_lst_month.Attributes["onclick"] = "javascript:showModalPopUp2('" + sURL + "')";
                        hyp_lst_month.NavigateUrl = "#";
                        hyp_lst_month.BackColor = System.Drawing.Color.Yellow;
                        //hyp_lst_month.Width = 50;
                    }

                    else
                    {
                        hyp_lst_month.Text = "-";
                    }
                }
                else if (ddlmode.SelectedValue == "8")
                {
                    if (dsDoc.Tables[0].Rows.Count > 0)
                    {
                        hyp_lst_month.Text = "Yes";
                        hyp_lst_month.BackColor = System.Drawing.Color.Yellow;
                    }
                    else
                    {
                        hyp_lst_month.Text = "-";
                    }
                }
                else if (ddlmode.SelectedValue == "9")
                {
                    if (dsDoc.Tables[0].Rows.Count > 0)
                    {
                        hyp_lst_month.Text = "Yes";
                        hyp_lst_month.BackColor = System.Drawing.Color.Yellow;
                    }
                    else
                    {
                        hyp_lst_month.Text = "-";
                    }
                }
                else if (ddlmode.SelectedValue == "4")
                {
                    if (dsDoc.Tables[0].Rows.Count > 0)
                    {
                        tot_dr = dsDoc.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                    }
                    else
                    {
                        tot_dr = "0";
                    }


                    if (tot_dr != "0")
                    {

                        hyp_lst_month.Text = tot_dr;
                        sURL = "&sf_code=" + drFF["Sf_Code"] + "&sf_name=" + drFF["sf_name"] + "";

                        hyp_lst_month.Attributes.Add("href", "javascript:showModalPopUp1('" + drFF["sf_code"].ToString() + "','" + drFF["sf_name"] + "')");
                        hyp_lst_month.Attributes["onclick"] = "javascript:showModalPopUp1('" + sURL + "')";
                        hyp_lst_month.NavigateUrl = "#";
                        hyp_lst_month.BackColor = System.Drawing.Color.Yellow;
                        //hyp_lst_month.Width = 50;


                    }

                    else
                    {
                        hyp_lst_month.Text = "-";
                    }
                }
                else
                {
                    if (ddlmode.SelectedValue == "5")
                    {
                        tot_dr = dsDoc.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();

                    }
                    else
                    {
                        tot_dr = dsDoc.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                        total_doc = dsDoc.Tables[0].Rows[0].ItemArray.GetValue(1).ToString();

                    }

                    if (tot_dr != "0")
                    {
                        //iTotLstCount += Convert.ToInt16(tot_dr);
                        if (ddlmode.SelectedValue == "5")
                        {
                            hyp_lst_month.Text = tot_dr;
                        }
                        else
                        {
                            hyp_lst_month.Text = tot_dr + "/" + total_doc;
                            if (ddlmode.SelectedValue == "1" || ddlmode.SelectedValue == "6" || ddlmode.SelectedValue == "2")
                            {
                                sURL = "&sf_code=" + drFF["Sf_Code"] + "&sf_name=" + drFF["sf_name"] + "&sf_short=" + drFF["sf_Designation_Short_Name"] + "&sf_hq=" + drFF["Sf_HQ"] + "";
                                if (ddlmode.SelectedValue == "1")
                                {
                                    hyp_lst_month.Attributes.Add("href", "javascript:showModalPopUp('" + drFF["sf_code"].ToString() + "', '" + drFF["sf_name"] + "','" + drFF["sf_Designation_Short_Name"] + "','" + drFF["Sf_HQ"] + "')");
                                    hyp_lst_month.Attributes["onclick"] = "javascript:showModalPopUp('" + sURL + "')";
                                }
                                else if (ddlmode.SelectedValue == "2")
                                {
                                    string URL = sURL;
                                    hyp_lst_month.Attributes.Add("href", "javascript:showModalPopUp22('" + drFF["sf_code"].ToString() + "', '" + drFF["sf_name"] + "','" + drFF["sf_Designation_Short_Name"] + "','" + drFF["Sf_HQ"] + "','" + div_code + "')");
                                    hyp_lst_month.Attributes["onclick"] = "javascript:showModalPopUp22('" + sURL + "')";
                                }
                                else
                                {
                                    hyp_lst_month.Attributes.Add("href", "javascript:showModalPopUp5('" + drFF["sf_code"].ToString() + "', '" + drFF["sf_name"] + "','" + drFF["sf_Designation_Short_Name"] + "','" + drFF["Sf_HQ"] + "')");
                                    hyp_lst_month.Attributes["onclick"] = "javascript:showModalPopUp5('" + sURL + "')";
                                }
                                hyp_lst_month.NavigateUrl = "#";
                                hyp_lst_month.BackColor = System.Drawing.Color.Yellow;
                            }
                        }


                        if (ddlmode.SelectedValue == "5")
                        {
                            sURL = "&sf_code=" + drFF["Sf_Code"] + "&sf_name=" + drFF["sf_name"] + "&sf_short=" + drFF["sf_Designation_Short_Name"] + "&sf_hq=" + drFF["Sf_HQ"] + "";

                            hyp_lst_month.Attributes.Add("href", "javascript:showModalPopUp3('" + drFF["sf_code"].ToString() + "', '" + drFF["sf_name"] + "','" + drFF["sf_Designation_Short_Name"] + "','" + drFF["Sf_HQ"] + "')");
                            hyp_lst_month.Attributes["onclick"] = "javascript:showModalPopUp3('" + sURL + "')";
                            hyp_lst_month.NavigateUrl = "#";
                            hyp_lst_month.BackColor = System.Drawing.Color.White;
                        }
                        else
                        {
                            hyp_lst_month.BackColor = System.Drawing.Color.Yellow;
                        }

                    }

                    else
                    {
                        hyp_lst_month.Text = "-";
                    }
                }

                //tc_lst_month.BorderStyle = BorderStyle.Solid;
                //tc_lst_month.BorderWidth = 1;
                //tc_lst_month.BackColor = System.Drawing.Color.White;
                //tc_lst_month.Width = 200;
                tc_lst_month.HorizontalAlign = HorizontalAlign.Center;
                tc_lst_month.VerticalAlign = VerticalAlign.Middle;
                tc_lst_month.Controls.Add(hyp_lst_month);
                //tc_lst_month.Style.Add("font-family", "Calibri");
                //tc_lst_month.Style.Add("font-size", "10pt");
                //tc_lst_month.Style.Add("text-align", "center");
                //tc_lst_month.Attributes.Add("Class", "rptCellBorder");
                tr_det.Cells.Add(tc_lst_month);
                if (ddlmode.SelectedValue == "5")
                {

                    dsDoc1 = sf.getDrgeoMap_Status(drFF["sf_code"].ToString(), div_code, ddlmode.SelectedValue);
                    TableCell tc_lst_month1 = new TableCell();
                    HyperLink hyp_lst_month1 = new HyperLink();
                    if (dsDoc1.Tables[0].Rows.Count > 0)


                        tot_dr1 = dsDoc1.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();






                    if (tot_dr1 != "0")
                    {
                        //iTotLstCount += Convert.ToInt16(tot_dr);
                        hyp_lst_month1.Text = tot_dr1;
                        sURL = "&sf_code=" + drFF["Sf_Code"] + "";
                        sURL = "Geo_ShowMap.aspx?sfcode=" + drFF["sf_code"].ToString() + " ";
                        hyp_lst_month1.Attributes["onclick"] = "javascript:window.open('" + sURL + "','_blank','PopUp',0,'resizable=yes,toolbar=no,menubar=no,status=no,width=1000,height=600,left=0,top=0');";
                        hyp_lst_month1.NavigateUrl = "#";




                    }

                    else
                    {
                        hyp_lst_month1.Text = "-";
                    }


                    //tc_lst_month1.BorderStyle = BorderStyle.Solid;
                    //tc_lst_month1.BorderWidth = 1;
                    //tc_lst_month1.BackColor = System.Drawing.Color.White;
                    //tc_lst_month1.Width = 200;
                    tc_lst_month1.HorizontalAlign = HorizontalAlign.Center;
                    tc_lst_month1.VerticalAlign = VerticalAlign.Middle;
                    tc_lst_month1.Controls.Add(hyp_lst_month1);
                    //tc_lst_month1.Style.Add("font-family", "Calibri");
                    //tc_lst_month1.Style.Add("font-size", "10pt");
                    //tc_lst_month1.Style.Add("text-align", "center");
                    //tc_lst_month1.Attributes.Add("Class", "rptCellBorder");
                    tr_det.Cells.Add(tc_lst_month1);

                    dsDoc2 = sf.getUnDrgeoMap_Status(drFF["sf_code"].ToString(), div_code, ddlmode.SelectedValue);
                    TableCell tc_lst_month2 = new TableCell();
                    HyperLink hyp_lst_month2 = new HyperLink();
                    if (dsDoc2.Tables[0].Rows.Count > 0)


                        tot_dr2 = dsDoc2.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();






                    if (tot_dr2 != "0")
                    {
                        //iTotLstCount += Convert.ToInt16(tot_dr);
                        hyp_lst_month2.Text = tot_dr2;
                        sURL = "&sf_code=" + drFF["Sf_Code"] + "";
                        //hyp_lst_month1.Attributes.Add("href", "javascript:showModalPopUp4('" + drFF["sf_code"].ToString() + "')");
                        //hyp_lst_month1.Attributes["onclick"] = "javascript:showModalPopUp4('" + sURL + "')";
                        //hyp_lst_month1.NavigateUrl = "#";
                        sURL = "GeoUnList_ShowMap.aspx?sfcode=" + drFF["sf_code"].ToString() + " ";
                        hyp_lst_month2.Attributes["onclick"] = "javascript:window.open('" + sURL + "','_blank','PopUp',0,'resizable=yes,toolbar=no,menubar=no,status=no,width=1000,height=600,left=0,top=0');";
                        hyp_lst_month2.NavigateUrl = "#";




                    }

                    else
                    {
                        hyp_lst_month2.Text = "-";
                    }


                    //tc_lst_month2.BorderStyle = BorderStyle.Solid;
                    //tc_lst_month2.BorderWidth = 1;
                    //tc_lst_month2.BackColor = System.Drawing.Color.White;
                    //tc_lst_month2.Width = 200;
                    tc_lst_month2.HorizontalAlign = HorizontalAlign.Center;
                    tc_lst_month2.VerticalAlign = VerticalAlign.Middle;
                    tc_lst_month2.Controls.Add(hyp_lst_month2);
                    //tc_lst_month2.Style.Add("font-family", "Calibri");
                    //tc_lst_month2.Style.Add("font-size", "10pt");
                    //tc_lst_month2.Style.Add("text-align", "center");
                    //tc_lst_month2.Attributes.Add("Class", "rptCellBorder");
                    tr_det.Cells.Add(tc_lst_month2);
                }
                tbl.Rows.Add(tr_det);
            }

        }
        else
        {
            TableRow tr_det_sno = new TableRow();
            TableCell tc_det_SNo = new TableCell();
            Literal lit_det_SNo = new Literal();
            lit_det_SNo.Text = "No Record Found";
            tc_det_SNo.BorderStyle = BorderStyle.Solid;
            tc_det_SNo.Attributes.Add("Class", "NoRecord");

            tc_det_SNo.HorizontalAlign = HorizontalAlign.Center;
            //tc_det_SNo.BorderWidth = 1;
            //tc_det_SNo.BorderStyle = BorderStyle.None;
            tc_det_SNo.Controls.Add(lit_det_SNo);
            tr_det_sno.Cells.Add(tc_det_SNo);

            tbl.Rows.Add(tr_det_sno);
        }
    }
}


