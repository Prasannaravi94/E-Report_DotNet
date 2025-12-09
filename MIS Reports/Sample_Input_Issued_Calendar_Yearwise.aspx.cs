using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;
using System.Drawing.Imaging;
using System.IO;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;


public partial class MIS_Reports_Sample_Input_Issued_Calendar_Yearwise : System.Web.UI.Page
{
    string div_code = string.Empty;
    DataSet dsSalesForce = null;
    DataSet dschm = null;
    DataSet dsstk = null;
    DataSet dsDoc5 = null;
    DataSet dsDoc6 = null;
    string tot_chm6 = string.Empty;
    DataSet dsstk1 = null;
    DataSet dsstk2 = null;
    string tot_chm = string.Empty;
    string tot_stk = string.Empty;
    string tot_stk1 = string.Empty;
    string tot_stk2 = string.Empty;
    string tot_chm1 = string.Empty;
    string total_doc_reg = string.Empty;
    string tot_dr_draft = string.Empty;
    DataTable dt = new DataTable();
    DataSet dsDoc = null;
    DataSet dsDoc1 = null;
    DataSet dsDoc2 = null;
    DataSet dsDivision = null;
    DataSet dsdiv = null;
    string tot_draft = string.Empty;
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
    int mode = -1;
    string strSf_Code = string.Empty;
    DateTime ServerStartTime;
    DateTime ServerEndTime;
    DataTable dtrowClr = new DataTable();
    string strConn = System.Configuration.ConfigurationManager.ConnectionStrings["Ereportcon"].ToString();
    protected void Page_Load(object sender, EventArgs e)
    {
        sf_code = Session["sf_code"].ToString();
        sf_type = Session["sf_type"].ToString();

        if (sf_type == "3")
        {
            div_code = Session["division_code"].ToString();
        }
        else
        {
            div_code = Session["div_code"].ToString();
        }

        if (div_code.Contains(','))
        {
            div_code = div_code.Remove(div_code.Length - 1);
        }
        if (!Page.IsPostBack)
        {
            Filldiv();
            FillYear();
            //menu1.Title = this.Page.Title;
            ServerStartTime = DateTime.Now;
            base.OnPreInit(e);
            ////// menu1.FindControl("btnBack").Visible = false;
           // FillManagers();
        }
        // FillColor();
    }


    //protected void ddlDivision_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    FillManagers();
    //    FillColor();
    //}

    private void Filldiv()
    {
        Division dv = new Division();
        if (sf_type == "3")
        {
            string[] strDivSplit = div_code.Split(',');
            foreach (string strdiv in strDivSplit)
            {
                if (strdiv != "")
                {
                    dsdiv = dv.getDivisionHO(strdiv);
                    ListItem liTerr = new ListItem();
                    liTerr.Value = dsdiv.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                    liTerr.Text = dsdiv.Tables[0].Rows[0].ItemArray.GetValue(1).ToString();
                    ddlDivision.Items.Add(liTerr);
                }
            }
        }

    }
    //private void FillManagers()
    //{
    //    SalesForce sf = new SalesForce();

    //    if (ddlFFType.SelectedValue.ToString() == "1")
    //    {
    //        ddlAlpha.Visible = false;
    //        dsSalesForce = sf.UserList_Hierarchy(ddlDivision.SelectedValue.ToString(), "admin");
    //    }
    //    else if (ddlFFType.SelectedValue.ToString() == "0")
    //    {
    //        FillSF_Alpha();
    //        ddlAlpha.Visible = true;
    //        dsSalesForce = sf.UserList_Alpha(ddlDivision.SelectedValue.ToString(), "admin");
    //    }

    //    if (dsSalesForce.Tables[0].Rows.Count > 0)
    //    {
    //        ddlFieldForce.DataTextField = "sf_name";
    //        ddlFieldForce.DataValueField = "sf_code";
    //        ddlFieldForce.DataSource = dsSalesForce;
    //        ddlFieldForce.DataBind();

    //        ddlSF.DataTextField = "des_color";
    //        ddlSF.DataValueField = "sf_code";
    //        ddlSF.DataSource = dsSalesForce;
    //        ddlSF.DataBind();

    //    }
    //}
    //private void FillSF_Alpha()
    //{
    //    SalesForce sf = new SalesForce();
    //    dsSalesForce = sf.getSalesForcelist_Alphabet(ddlDivision.SelectedValue.ToString());
    //    if (dsSalesForce.Tables[0].Rows.Count > 0)
    //    {
    //        ddlAlpha.DataTextField = "sf_name";
    //        ddlAlpha.DataValueField = "val";
    //        ddlAlpha.DataSource = dsSalesForce;
    //        ddlAlpha.DataBind();
    //        ddlAlpha.SelectedIndex = 0;
    //    }
    //}

    //protected void ddlAlpha_SelectedIndexChanged(object sender, EventArgs e)
    //{

    //    SalesForce sf = new SalesForce();
    //    dsSalesForce = sf.UserList_Alphasearch(ddlDivision.SelectedValue.ToString(), "admin", ddlAlpha.SelectedValue);
    //    if (dsSalesForce.Tables[0].Rows.Count > 0)
    //    {
    //        ddlFieldForce.DataTextField = "sf_name";
    //        ddlFieldForce.DataValueField = "sf_code";
    //        ddlFieldForce.DataSource = dsSalesForce;
    //        ddlFieldForce.DataBind();

    //        ddlSF.DataTextField = "des_color";
    //        ddlSF.DataValueField = "sf_code";
    //        ddlSF.DataSource = dsSalesForce;
    //        ddlSF.DataBind();

    //    }
    //    FillColor();

    //}

    //protected void ddlFFType_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    lblFF.Text = "Field Force";
    //    FillManagers();
    //    FillColor();
    //}

    //private void FillColor()
    //{
    //    int j = 0;

    //    foreach (ListItem ColorItems in ddlSF.Items)
    //    {
    //        //ddlFieldForce.Items[j].Selected = true;

    //        string bcolor = "#" + ColorItems.Text;
    //        ddlFieldForce.Items[j].Attributes.Add("style", "background-color:" + bcolor);

    //        j = j + 1;

    //    }
    //}
    private void FillYear()
    {
        try
        {
            TourPlan tp = new TourPlan();
            DataSet dsYear = tp.Get_TP_Edit_Year("2"); // Get the Year for the Division

            if (dsYear.Tables[0].Rows.Count > 0)
            {
                for (int k = Convert.ToInt16(dsYear.Tables[0].Rows[0]["Year"]); k <= DateTime.Now.Year + 1; k++)
                {
                    int Year = k + 1;
                    string F_Year = k + "-" + Year;
                    ddlFinancial.Items.Add(F_Year);
                }
            }

            string CurYear = DateTime.Now.Year.ToString() + "-" + (Convert.ToInt32(DateTime.Now.Year.ToString()) + 1);
            ddlFinancial.Items.FindByText(CurYear).Selected = true;

            //ddlFinancial.Items.Contains();
            //ddlFinancial.SelectedIndex = 0;

        }
        catch (Exception ex)
        {
            //ErrorLog err = new ErrorLog();
            // iErrReturn = err.LogError(Convert.ToInt32(div_code), ex.Message.ToString().Trim(), "Sec Sales Entry", "FillYear()");
            //ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Unknown error exists. Please contact customer care for support');</script>");
        }
    }

    protected void btnGo_Click(object sender, EventArgs e)
    {
        string[] authorsList = ddlFinancial.SelectedValue.Split('-');

        string From_year = authorsList[0].ToString();
        string To_year = authorsList[1].ToString();
        string From_Month = "4";
        string To_Month = "3";

        int months = (Convert.ToInt32(To_year) - Convert.ToInt32(From_year)) * 12 + Convert.ToInt32(To_Month) - Convert.ToInt32(From_Month); //(Date2.Year - Date1.Year) * 12 + Date2.Month - Date1.Month;
        int cmonth = Convert.ToInt32(From_Month);
        int cyear = Convert.ToInt32(From_year);

        int iMn = 0, iYr = 0;
        DataTable dtMnYr = new DataTable();
        dtMnYr.Columns.Add("INX", typeof(int)).AutoIncrement = true;
        dtMnYr.Columns["INX"].AutoIncrementSeed = 1;
        dtMnYr.Columns["INX"].AutoIncrementStep = 1;
        dtMnYr.Columns.Add("MNTH", typeof(int));
        dtMnYr.Columns.Add("YR", typeof(int));
        //
        while (months >= 0)
        {
            if (cmonth == 13)
            {
                cmonth = 01; iMn = cmonth; cyear = cyear + 1; iYr = cyear;
            }
            else
            {
                iMn = cmonth; iYr = cyear;
            }
            dtMnYr.Rows.Add(null, iMn, iYr);
            months--; cmonth++;
        }


        string sprocname = string.Empty;
        if (ddlmode.SelectedValue == "0")
        {
            //sprocname = "Sample_Issued_CalendarYearwise";
            sprocname = "Sample_Issued_CalendarYearwise_HQ";
        }
        else
        {
            //sprocname = "Input_Issued_CalendarYearwise";
            sprocname = "Input_Issued_CalendarYearwise_Hq";
        }
        string strConn = System.Configuration.ConfigurationManager.ConnectionStrings["Ereportcon"].ToString();
        SqlConnection con = new SqlConnection(strConn);
        con.Open();
        SqlCommand cmd = new SqlCommand(sprocname, con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Div_Code", ddlDivision.SelectedValue);
        cmd.Parameters.AddWithValue("@MnthTbl", dtMnYr);

        cmd.CommandTimeout = 8000;
        SqlDataAdapter da = new SqlDataAdapter(cmd);

        DataSet ds = new DataSet();
        SalesForce sf = new SalesForce();
        da.Fill(ds);
        if (ddlmode.SelectedValue == "0")
        {
            ds.Tables[0].Columns.Remove("ListedDrCode");
            ds.Tables[0].Columns.Remove("Sf_Code");
            ds.Tables[0].Columns.Remove("Prod_Code");
        }
        else
        {
            ds.Tables[0].Columns.Remove("ListedDrCode");
            ds.Tables[0].Columns.Remove("Sf_Code");
            ds.Tables[0].Columns.Remove("Gift_Code");
        }

        dt = ds.Tables[0];
        con.Close();

        DataTable dtClone = dt;

        Response.Clear();
        Response.Buffer = true;
        Response.AddHeader("content-disposition", "attachment;filename=Sample/Input.csv");
        Response.Charset = "";
        string csv = string.Empty;

        foreach (DataColumn column in dt.Columns)
        {
            //Add the Header row for CSV file.
            //csv += column.ColumnName + ',';
            Response.Write(column.ColumnName + ',');
        }

        //Add new line.
        //csv += "\r\n";
        Response.Write("\r\n");
        int rowClone = -1;
        foreach (DataRow row in dt.Rows)
        {
            foreach (DataColumn column in dt.Columns)
            {
               
                Response.Write(row[column.ColumnName].ToString().Replace('"',';').Replace(",", ";").Replace(".",";") + ',');
            
            }
            //rowClone++;
            //Add new line.
            //csv += "\r\n";
            Response.Write("\r\n");
        }

        //string style = @"<style> .textmode { mso-number-format:\@; } </style>";
        //Response.Write(style);
        Response.ContentType = "application/text";
        //Response.Output.Write(csv);
        Response.Flush();
        Response.End();


        // Its working Fine IF Excel Download Needed

        //int countRow = dt.Rows.Count;
        //int countCol = dt.Columns.Count;

        //for (int iCol = 0; iCol < countCol; iCol++)
        //{
        //    DataColumn col = dt.Columns[iCol];
        //    if (col.ColumnName != "Sf_Name" && col.ColumnName != "sf_emp_id" && col.ColumnName != "ListedDr_Name" && col.ColumnName != "Doc_Spec_ShortName" && col.ColumnName != "Doc_Cat_ShortName" && col.ColumnName != "Doc_Class_ShortName NO" && col.ColumnName != "ListedDr_Mobile" && col.ColumnName != "Product_Erp_Code" && col.ColumnName != "Prod_Name")
        //    {
        //        if (col.ColumnName == "A_Month")
        //        {
        //            dt.Columns[iCol].ColumnName = "January";
        //            dt.AcceptChanges();
        //        }
        //        else if (col.ColumnName == "B_Month")
        //        {
        //            dt.Columns[iCol].ColumnName = "February";
        //            dt.AcceptChanges();
        //        }
        //        else if (col.ColumnName == "C_Month")
        //        {
        //            dt.Columns[iCol].ColumnName = "March";
        //            dt.AcceptChanges();
        //        }
        //        else if (col.ColumnName == "D_Month")
        //        {
        //            dt.Columns[iCol].ColumnName = "April";
        //            dt.AcceptChanges();
        //        }
        //        else if (col.ColumnName == "E_Month")
        //        {
        //            dt.Columns[iCol].ColumnName = "May";
        //            dt.AcceptChanges();
        //        }
        //        else if (col.ColumnName == "F_Month")
        //        {
        //            dt.Columns[iCol].ColumnName = "June";
        //            dt.AcceptChanges();
        //        }
        //        else if (col.ColumnName == "G_Month")
        //        {
        //            dt.Columns[iCol].ColumnName = "July";
        //            dt.AcceptChanges();
        //        }
        //        else if (col.ColumnName == "H_Month")
        //        {
        //            dt.Columns[iCol].ColumnName = "August";
        //            dt.AcceptChanges();
        //        }
        //        else if (col.ColumnName == "I_Month")
        //        {
        //            dt.Columns[iCol].ColumnName = "September";
        //            dt.AcceptChanges();
        //        }
        //        else if (col.ColumnName == "J_Month")
        //        {
        //            dt.Columns[iCol].ColumnName = "October";
        //            dt.AcceptChanges();
        //        }
        //        else if (col.ColumnName == "K_Month")
        //        {
        //            dt.Columns[iCol].ColumnName = "November";
        //            dt.AcceptChanges();
        //        }
        //        else if (col.ColumnName == "L_Month")
        //        {
        //            dt.Columns[iCol].ColumnName = "December";
        //            dt.AcceptChanges();
        //        }
        //    }
        //}



        //string attachment = "attachment; filename=commonDr_Sample_Issued_Detail.xls";
        //Response.Charset = "";
        //Response.Clear();
        //Response.ClearContent();
        //Response.AddHeader("content-disposition", attachment);
        ////Response.ContentType = "application/vnd.ms-excel";
        //Response.ContentType = "application/text";
        //string tab = "";
        //Response.Write("<table width = '100%' border='1'>");
        //Response.Write("<tr>");
        //foreach (DataColumn dc in dt.Columns)
        //{
        //    Response.Write("<th style='background-color:lightblue'>");
        //    Response.Write(tab + dc.ColumnName);
        //    Response.Write("</th>");
        //    // tab = "\t";
        //}
        ////Response.Write("\n");
        //Response.Write("</tr>");
        //int i;
        //foreach (DataRow dr in dt.Rows)
        //{
        //    tab = "";
        //    Response.Write("<tr>");
        //    for (i = 0; i < dt.Columns.Count; i++)
        //    {
        //        Response.Write("<td>");
        //        Response.Write(tab + dr[i].ToString());
        //        Response.Write("</td>");
        //        //tab = "\t";
        //    }
        //    Response.Write("</tr>");
        //}

        //Response.Buffer = true;
        ////Response.Flush();
        //Response.End();
    }
}