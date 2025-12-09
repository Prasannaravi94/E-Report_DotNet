using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;
using System.Data.OleDb;
using System.Data.Sql;
using System.Data.SqlClient;
using System.IO;
using System.Configuration;
using System.Data.Common;
using System.Drawing.Imaging;
using DBase_EReport;
using System.Net;
using System.Web.UI.HtmlControls;
using System.Web.Services;
using System.Text;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.xml.simpleparser;
using iTextSharp.text.xml;
using iTextSharp.text.io;
using ClosedXML;

public partial class Exp_Cons : System.Web.UI.Page
{
    string div_code = string.Empty;
    DataSet dsSalesForce = new DataSet();
    DataSet dsProduct = null;
    DataSet dslstSpec = new DataSet();
    DataSet dslstCat = new DataSet();
    DataSet dslstCls = new DataSet();
    DataTable dt = new DataTable();
    
    DataSet dsDivision = null;
    string sfCode = string.Empty;
    SqlConnection conn = new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["Ereportcon"].ConnectionString);
    DataSet ds;
    DataTable Dt;
    protected void Page_Load(object sender, EventArgs e)
    {
        div_code = Session["div_code"].ToString();
        //btnSubmit.Focus();
        if (!Page.IsPostBack)
        {
            Filldiv();
            FillManagers();
            FillColor();
            //menu1.FindControl("btnBack").Visible = false;
            //menu1.Title = this.Page.Title;
            TourPlan tp = new TourPlan();
            DataSet dsTP = new DataSet();
            dsTP = tp.Get_TP_Edit_Year(div_code);
            if (dsTP.Tables[0].Rows.Count > 0)
            {
                for (int k = Convert.ToInt16(dsTP.Tables[0].Rows[0]["Year"]); k <= DateTime.Now.Year + 1; k++)
                {
                    ddlYear.Items.Add(k.ToString());
                    ddlYear.SelectedValue = DateTime.Now.Year.ToString();
                    ddlMonth.SelectedValue = DateTime.Now.Month.ToString();
                }
            }
        }
    }

    private void Filldiv()
    {
        Division dv = new Division();
        dsDivision = dv.getDivision();
        if (dsDivision.Tables[0].Rows.Count > 0)
        {
            ddlDivision.DataTextField = "Division_Name";
            ddlDivision.DataValueField = "Division_Code";
            ddlDivision.DataSource = dsDivision;
            ddlDivision.DataBind();
        }
    }

    protected void ddlFFType_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillManagers();
        FillColor();
    }

    private void FillManagers()
    {
        SalesForce sf = new SalesForce();

        if (ddlFFType.SelectedValue.ToString() == "1")
        {
            ddlAlpha.Visible = false;
            dsSalesForce = sf.UserListTP_Hierarchy(div_code, "admin");
        }
        else if (ddlFFType.SelectedValue.ToString() == "0")
        {
            FillSF_Alpha();
            ddlAlpha.Visible = true;
            dsSalesForce = sf.UserListTP_Alpha(div_code, "admin");
            //dsSalesForce = sf.UserList_Alpha(div_code, "admin");
        }
        else if (ddlFFType.SelectedValue.ToString() == "2")
        {
            dsSalesForce = sf.UserList_HQ(div_code, "admin");
        }

        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            ddlFieldForce.DataTextField = "sf_name";
            ddlFieldForce.DataValueField = "sf_code";
            ddlFieldForce.DataSource = dsSalesForce;
            ddlFieldForce.DataBind();

            ddlSF.DataTextField = "des_color";
            ddlSF.DataValueField = "sf_code";
            ddlSF.DataSource = dsSalesForce;
            ddlSF.DataBind();

        }
    }

    private void FillSF_Alpha()
    {
        SalesForce sf = new SalesForce();
        dsSalesForce = sf.getSalesForcelist_Alphabet(div_code);
        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            ddlAlpha.DataTextField = "sf_name";
            ddlAlpha.DataValueField = "val";
            ddlAlpha.DataSource = dsSalesForce;
            ddlAlpha.DataBind();
            ddlAlpha.SelectedIndex = 0;
        }
    }


    private void FillColor()
    {
        //int j = 0;

        //foreach (ListItem ColorItems in ddlSF.Items)
        //{
        //    string bcolor = "#" + ColorItems.Text;
        //    ddlFieldForce.Items[j].Attributes.Add("style", "background-color:" + bcolor);

        //    j = j + 1;

        //}
    }

    protected void ddlAlpha_SelectedIndexChanged(object sender, EventArgs e)
    {
        SalesForce sf = new SalesForce();
        if (ddlAlpha.SelectedItem.Text == "All")
        {
            dsSalesForce = sf.UserListTP_Alpha(div_code, "admin");
        }
        else
        {
            dsSalesForce = sf.UserList_Alphasearch(ddlDivision.SelectedValue.ToString(), "admin", ddlAlpha.SelectedValue);
        }
        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            ddlFieldForce.DataTextField = "sf_name";
            ddlFieldForce.DataValueField = "sf_code";
            ddlFieldForce.DataSource = dsSalesForce;
            ddlFieldForce.DataBind();

            ddlSF.DataTextField = "des_color";
            ddlSF.DataValueField = "sf_code";
            ddlSF.DataSource = dsSalesForce;
            ddlSF.DataBind();

        }
        FillColor();

    }


    protected void lnkDownload_Click(object sender, EventArgs e)
    {
        SalesForce sf = new SalesForce();
        DCR dcc = new DCR();
        DB_EReporting db = new DB_EReporting();
        string strConn = System.Configuration.ConfigurationManager.ConnectionStrings["Ereportcon"].ToString();
        SqlConnection con = new SqlConnection(strConn);
        con.Open();
        DataSet dsts = new DataSet();
        // SqlCommand cmd = null;
        if (div_code == "2")
        {
            SqlCommand cmd = new SqlCommand("sp_SalesForceGet_MM_expense_dump", con);

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@div_code", Convert.ToInt32(div_code));
            cmd.Parameters.AddWithValue("@sf_code", ddlFieldForce.SelectedValue);
            cmd.Parameters.AddWithValue("@mnth", ddlMonth.SelectedValue);
            cmd.Parameters.AddWithValue("@yr", ddlYear.SelectedValue);

            cmd.CommandTimeout = 5000;

            SqlDataAdapter da = new SqlDataAdapter(cmd);

            da.Fill(dsts);
            dsts.Tables[0].Columns.Remove("Sortid");
            dsts.Tables[0].Columns.Remove("Sf_Code");
            con.Close();
        }
        else if (div_code == "3")
        {
            SqlCommand cmd = new SqlCommand("sp_SalesForceGet_MM_expense_dump3", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@div_code", Convert.ToInt32(div_code));
            cmd.Parameters.AddWithValue("@sf_code", ddlFieldForce.SelectedValue);
            cmd.Parameters.AddWithValue("@mnth", ddlMonth.SelectedValue);
            cmd.Parameters.AddWithValue("@yr", ddlYear.SelectedValue);

            cmd.CommandTimeout = 5000;

            SqlDataAdapter da = new SqlDataAdapter(cmd);

            da.Fill(dsts);
            dsts.Tables[0].Columns.Remove("Sortid");
            dsts.Tables[0].Columns.Remove("Sf_Code");
            con.Close();
        }
        else if (div_code == "4")
        {
            SqlCommand cmd = new SqlCommand("sp_SalesForceGet_MM_expense_dump4", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@div_code", Convert.ToInt32(div_code));
            cmd.Parameters.AddWithValue("@sf_code", ddlFieldForce.SelectedValue);
            cmd.Parameters.AddWithValue("@mnth", ddlMonth.SelectedValue);
            cmd.Parameters.AddWithValue("@yr", ddlYear.SelectedValue);

            cmd.CommandTimeout = 5000;

            SqlDataAdapter da = new SqlDataAdapter(cmd);

            da.Fill(dsts);
            dsts.Tables[0].Columns.Remove("Sortid");
            dsts.Tables[0].Columns.Remove("Sf_Code");
            con.Close();
        }
        else if (div_code == "5")
        {
            SqlCommand cmd = new SqlCommand("sp_SalesForceGet_MM_expense_dump5", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@div_code", Convert.ToInt32(div_code));
            cmd.Parameters.AddWithValue("@sf_code", ddlFieldForce.SelectedValue);
            cmd.Parameters.AddWithValue("@mnth", ddlMonth.SelectedValue);
            cmd.Parameters.AddWithValue("@yr", ddlYear.SelectedValue);

            cmd.CommandTimeout = 5000;

            SqlDataAdapter da = new SqlDataAdapter(cmd);

            da.Fill(dsts);
            dsts.Tables[0].Columns.Remove("Sortid");
            dsts.Tables[0].Columns.Remove("Sf_Code");
            con.Close();
        }
        if (dsts.Tables.Count > 0)
        {
            dt = dsts.Tables[0];
            //dt = dsts.Tables[0];
            ClosedXML.Excel.XLWorkbook wbook = new ClosedXML.Excel.XLWorkbook();
            wbook.Worksheets.Add(dt, "Download_Expense");
            HttpResponse httpResponse = Response;
            httpResponse.Clear();
            httpResponse.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            httpResponse.AddHeader("content-disposition", "attachment;filename=\"Download_SFC.xlsx\"");

            using (MemoryStream memoryStream = new MemoryStream())
            {
                wbook.SaveAs(memoryStream);
                memoryStream.WriteTo(httpResponse.OutputStream);
                memoryStream.Close();
            }

            httpResponse.End();
        }
    }


}