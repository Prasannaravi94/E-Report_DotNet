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
using System.Text;
using System.Web.UI.HtmlControls;
using System.Net;
using System.Web.Services;
using Newtonsoft.Json;
using System.Data.SqlClient;

public partial class MIS_Reports_ColorSetting : System.Web.UI.Page
{
    #region "Declaration"
    string div_code = string.Empty;
    string sf_type = string.Empty;
    string sf_code = string.Empty;
    static string div_color = string.Empty;
    static string div_codeJSON = string.Empty;
    DataSet dsHoliday = null;
    DataSet dsdiv = null;
    DataSet dsDivision = null;
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
     
        sf_code = Session["sf_code"].ToString();
        sf_type = Session["sf_type"].ToString();
        heading.InnerText = this.Page.Title;
        if (sf_type == "3")
        {
            div_code = Session["division_code"].ToString();
            div_code = div_code.Substring(0, div_code.Length - 1);
        }
        else
        {
            div_code = Session["div_code"].ToString();
        }

        if (!Page.IsPostBack)
        {
            UserControl_pnlMenu c1 = (UserControl_pnlMenu)LoadControl("~/UserControl/pnlMenu.ascx");
            Divid.Controls.Add(c1);
            c1.Title = Page.Title;
            //c1.FindControl("btnBack").Visible = false;
        }
        if (!Page.IsPostBack)
        {
            Filldiv();
        }
    }

    [WebMethod(EnableSession = true)]
    public static string bg_colorSave(string objData)
    {
        string[] arr = objData.Split('^');
        //div_codeJSON = arr[0].TrimEnd(',');
        string[] div_codeJSON = arr[0].ToString().Split(',');
        div_color = arr[1];
        HttpContext.Current.Session["div_color"] = div_color;
        DataTable dt = new DataTable();

        using (SqlConnection con = new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["Ereportcon"].ConnectionString))
        {
            SqlCommand cmd = new SqlCommand("sp_ColorCodeDetail", con);

            cmd.CommandTimeout = 600;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Div_Code", div_codeJSON[0]);
            cmd.Parameters.AddWithValue("@Div_color", div_color);
            cmd.Parameters.AddWithValue("@Record", "update");
            con.Open();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.SelectCommand = cmd;
            da.Fill(dt);

            con.Close();
            dt.AcceptChanges();
            dt.AcceptChanges();
            string jsonResult = JsonConvert.SerializeObject(dt);
            return objData;
        }
    }

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
                    ddlDivisionCode.Items.Add(liTerr);
                }
            }
        }
        else
        {
            dsDivision = dv.getDivision_Name();
            if (dsDivision.Tables[0].Rows.Count > 0)
            {
                ddlDivisionCode.DataTextField = "Division_Name";
                ddlDivisionCode.DataValueField = "Division_Code";
                ddlDivisionCode.DataSource = dsDivision;
                ddlDivisionCode.DataBind();
            }
        }
    }

}

