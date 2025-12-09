using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Web.UI.HtmlControls;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Data.SqlClient;
using Newtonsoft.Json;
using System.Text;
using Bus_EReport;
using System.Configuration;
using System.Web.Script.Services;
using iTextSharp.tool.xml;
using System.Web.UI.DataVisualization.Charting;

public partial class Admin_Dashboard : System.Web.UI.Page
{
    DataSet dsAdmin = null;
    DataSet dsAdmin1 = null;
    string div_code = string.Empty;
    string sf_code = string.Empty;
    DateTime ServerStartTime;
    DateTime ServerEndTime;
    DataSet dsHoliday = new DataSet();
    DataSet dsSalesForce = new DataSet();
    DataSet dsTP = new DataSet();

    static DataTable dtcopy = new DataTable();
    string strday = string.Empty;
    string strWeek = string.Empty;
    string state_code = string.Empty;

    static string sSf_Code = string.Empty;
    static string sDivCode = string.Empty;
    static string smonth = string.Empty;
    static string syear = string.Empty;
    static string tmonth = string.Empty;
    static string tyear = string.Empty;
    static string mode = string.Empty;
    static string sSfName = string.Empty;
    static string FMName = string.Empty;
    int time;
    protected void Page_Load(object sender, EventArgs e)
    {
        menumas.FindControl("pnlHeader").Visible = false;
        
        div_code = Session["div_code"].ToString();
        sDivCode = Session["div_code"].ToString();
        sf_code = Session["sf_code"].ToString();

        if (!Page.IsPostBack)
        {
            FillYear();
            FillMRManagers();
        }
        //tbl5.Enabled = false;
        //menumas.Visible = false;
    }
    private void FillMRManagers()
    {
        SalesForce sf = new SalesForce();
        dsSalesForce = sf.SalesForceListMgrGet(div_code, sf_code);
        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            ddlFieldForce.DataTextField = "sf_name";
            ddlFieldForce.DataValueField = "sf_code";
            ddlFieldForce.DataSource = dsSalesForce;
            ddlFieldForce.DataBind();
        }
    }
    private void FillYear()
    {
        TourPlan tp = new TourPlan();
        dsTP = tp.Get_TP_Edit_Year(div_code);
        if (dsTP.Tables[0].Rows.Count > 0)
        {
            for (int k = Convert.ToInt16(dsTP.Tables[0].Rows[0]["Year"]); k <= DateTime.Now.Year + 1; k++)
            {
                ddlFYear.Items.Add(k.ToString());
                ddlFYear.SelectedValue = DateTime.Now.Year.ToString();
                //  ddlFYear.SelectedValue = "2018";
            }

        }
        ddlFMonth.SelectedValue = DateTime.Now.Month.ToString();
        //  ddlFMonth.SelectedValue ="4";

    }
    [WebMethod(EnableSession = true)]

    public static string SingleFW(string objData)
    {
        string div_code = HttpContext.Current.Session["div_code"].ToString();


        string[] arr = objData.Split('^');
        smonth = arr[0];
        syear = arr[1];
        sSf_Code = arr[2];
        sSfName = arr[3];
        FMName = arr[4];
        using (SqlConnection con = new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["Ereportcon"].ConnectionString))
        {
            DataTable dt = new DataTable();
            // dt.Columns.Add("Label");
            // dt.Columns.Add("Value");
            string sProc_Name = "";

            sProc_Name = "FW_Single_Graph_Des_Only";


            SqlCommand cmd = new SqlCommand(sProc_Name, con);


            cmd.CommandTimeout = 600;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Div_Code", div_code);
            cmd.Parameters.AddWithValue("@Msf_code", sSf_Code);
            cmd.Parameters.AddWithValue("@cMnth", smonth);
            cmd.Parameters.AddWithValue("@cYrs", syear);

            con.Open();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.SelectCommand = cmd;
            // DataTable dt = new DataTable();
            da.Fill(dt);
            //  dtcopy = dt.Copy();
            //  dt.Columns.RemoveAt(4);
            dt.Columns.RemoveAt(2);
            dt.Columns.RemoveAt(1);


            con.Close();
            dt.Columns["desg"].ColumnName = "Label";
            dt.AcceptChanges();
            dt.Columns["1_TTL"].ColumnName = "Value";

            dt.AcceptChanges();


            dt.AcceptChanges();

            //System.Data.DataColumn newColumn = new System.Data.DataColumn("link", typeof(System.String));
            //newColumn.DefaultValue = " window.open(Fw_Zoom.aspx?sf_code='" + sSf_Code + "'&FMonth='" + smonth + "'&Fyear='" + syear + "',width=400,height=300,toolbar=no,scrollbars=yes,resizable=yes";
            //dt.Columns.Add(newColumn);


            string jsonResult = JsonConvert.SerializeObject(dt);

            return jsonResult;

        }
    }
    [WebMethod(EnableSession = true)]

    public static string Visit_Days(string objData)
    {
        string div_code = HttpContext.Current.Session["div_code"].ToString();


        string[] arr = objData.Split('^');
        smonth = arr[0];
        syear = arr[1];
        sSf_Code = arr[2];
        sSfName = arr[3];
        FMName = arr[4];
        using (SqlConnection con = new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["Ereportcon"].ConnectionString))
        {

            // dt.Columns.Add("Label");
            // dt.Columns.Add("Value");
            string sProc_Name = "";

            sProc_Name = "Manager_Visit_Days";


            SqlCommand cmd = new SqlCommand(sProc_Name, con);


            cmd.CommandTimeout = 600;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Div_Code", div_code);
            cmd.Parameters.AddWithValue("@Msf_code", sSf_Code);
            cmd.Parameters.AddWithValue("@cMnth", smonth);
            cmd.Parameters.AddWithValue("@cYrs", syear);

            con.Open();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.SelectCommand = cmd;
            DataTable dtVD = new DataTable();
            da.Fill(dtVD);




            con.Close();
            dtVD.Columns["dys"].ColumnName = "Label";
            dtVD.AcceptChanges();
            dtVD.Columns["cnt"].ColumnName = "Value";

            dtVD.AcceptChanges();




            string jsonResult = JsonConvert.SerializeObject(dtVD);

            return jsonResult;

        }
    }
    [WebMethod(EnableSession = true)]

    public static string SingleCV(string objData)
    {


        string div_code = HttpContext.Current.Session["div_code"].ToString();


        string[] arr = objData.Split('^');
        smonth = arr[0];
        syear = arr[1];
        sSf_Code = arr[2];
        sSfName = arr[3];
        FMName = arr[4];
        using (SqlConnection con = new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["Ereportcon"].ConnectionString))
        {
            DataTable dtCV = new DataTable();
            // dt.Columns.Add("Label");
            // dt.Columns.Add("Value");
            string sProc_Name = "";

            sProc_Name = "FW_Single_Graph_Des";


            SqlCommand cmd = new SqlCommand(sProc_Name, con);


            cmd.CommandTimeout = 600;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Div_Code", div_code);
            cmd.Parameters.AddWithValue("@Msf_code", sSf_Code);
            cmd.Parameters.AddWithValue("@cMnth", smonth);
            cmd.Parameters.AddWithValue("@cYrs", syear);

            con.Open();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.SelectCommand = cmd;
            // DataTable dt = new DataTable();
            da.Fill(dtCV);

            dtCV.Columns.RemoveAt(3);
            dtCV.Columns.RemoveAt(2);
            dtCV.Columns.RemoveAt(1);

            con.Close();
            dtCV.Columns["desg"].ColumnName = "Label";
            dtCV.AcceptChanges();
            dtCV.Columns["1_UTL"].ColumnName = "Value";

            dtCV.AcceptChanges();




            string jsonResult = JsonConvert.SerializeObject(dtCV);

            return jsonResult;

        }


    }
    [WebMethod(EnableSession = true)]

    public static string MgrDet(string objData)
    {
        string div_code = HttpContext.Current.Session["div_code"].ToString();


        string[] arr = objData.Split('^');
        smonth = arr[0];
        syear = arr[1];
        sSf_Code = arr[2];
        sSfName = arr[3];
        FMName = arr[4];
        using (SqlConnection con = new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["Ereportcon"].ConnectionString))
        {
            DataTable dtM = new DataTable();
            // dt.Columns.Add("Label");
            // dt.Columns.Add("Value");
            string sProc_Name = "";

            sProc_Name = "Manager_Det_Graph";


            SqlCommand cmd = new SqlCommand(sProc_Name, con);


            cmd.CommandTimeout = 600;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Div_Code", div_code);
            cmd.Parameters.AddWithValue("@Msf_code", sSf_Code);
            cmd.Parameters.AddWithValue("@cMnth", smonth);
            cmd.Parameters.AddWithValue("@cYr", syear);

            con.Open();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.SelectCommand = cmd;
            // DataTable dt = new DataTable();
            da.Fill(dtM);
            //   dtcopy = dt.Copy();
            for (int i = 0; i < dtM.Rows.Count; i++)
            {
                if (dtM.Rows[i]["Sl_No"].ToString() == "2" || dtM.Rows[i]["Sl_No"].ToString() == "1" || dtM.Rows[i]["Sl_No"].ToString() == "8" || dtM.Rows[i]["Sl_No"].ToString() == "9" || dtM.Rows[i]["Sl_No"].ToString() == "10" || dtM.Rows[i]["Sl_No"].ToString() == "11" || dtM.Rows[i]["Sl_No"].ToString() == "12" || dtM.Rows[i]["Sl_No"].ToString() == "13" || dtM.Rows[i]["Sl_No"].ToString() == "14" || dtM.Rows[i]["Sl_No"].ToString() == "15")
                {
                    dtM.Rows[i].Delete();
                    dtM.AcceptChanges();
                }

            }

            dtM.Columns.RemoveAt(3);
            dtM.Columns.RemoveAt(2);
            dtM.Columns.RemoveAt(0);


            con.Close();
            dtM.Columns["para"].ColumnName = "Label";
            dtM.AcceptChanges();
            dtM.Columns["1"].ColumnName = "Value";

            dtM.AcceptChanges();




            string jsonResult = JsonConvert.SerializeObject(dtM);

            return jsonResult;

        }
    }
    [WebMethod(EnableSession = true)]

    public static string SingleCall(string objData)
    {
        string div_code = HttpContext.Current.Session["div_code"].ToString();


        string[] arr = objData.Split('^');
        smonth = arr[0];
        syear = arr[1];
        sSf_Code = arr[2];
        sSfName = arr[3];
        FMName = arr[4];
        using (SqlConnection con = new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["Ereportcon"].ConnectionString))
        {
            DataTable dtD = new DataTable();
            // dt.Columns.Add("Label");
            // dt.Columns.Add("Value");
            string sProc_Name = "";

            sProc_Name = "Manager_Det_Graph";


            SqlCommand cmd = new SqlCommand(sProc_Name, con);


            cmd.CommandTimeout = 600;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Div_Code", div_code);
            cmd.Parameters.AddWithValue("@Msf_code", sSf_Code);
            cmd.Parameters.AddWithValue("@cMnth", smonth);
            cmd.Parameters.AddWithValue("@cYr", syear);

            con.Open();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.SelectCommand = cmd;
            // DataTable dt = new DataTable();
            da.Fill(dtD);
            //  dtcopy = dtD.Copy();
            dtD.Columns.RemoveAt(3);
            dtD.Columns.RemoveAt(2);
            dtD.Columns.RemoveAt(0);

            con.Close();

            dtD.Columns["para"].ColumnName = "Label";
            dtD.AcceptChanges();
            dtD.Columns["1"].ColumnName = "Value";

            dtD.AcceptChanges();




            string jsonResult = JsonConvert.SerializeObject(dtD);

            return jsonResult;

        }
    }
    [WebMethod(EnableSession = true)]

    public static string Mgr_Det(string objData)
    {
        string div_code = HttpContext.Current.Session["div_code"].ToString();


        string[] arr = objData.Split('^');
        smonth = arr[0];
        syear = arr[1];
        sSf_Code = arr[2];
        sSfName = arr[3];
        FMName = arr[4];
        using (SqlConnection con = new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["Ereportcon"].ConnectionString))
        {
            DataTable dtD = new DataTable();
            // dt.Columns.Add("Label");
            // dt.Columns.Add("Value");
            string sProc_Name = "";

            sProc_Name = "Manager_CallDet_Graph";


            SqlCommand cmd = new SqlCommand(sProc_Name, con);


            cmd.CommandTimeout = 600;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Div_Code", div_code);
            cmd.Parameters.AddWithValue("@Msf_code", sSf_Code);
            cmd.Parameters.AddWithValue("@cMnth", smonth);
            cmd.Parameters.AddWithValue("@cYr", syear);

            con.Open();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.SelectCommand = cmd;
            // DataTable dt = new DataTable();
            da.Fill(dtD);
            //  dtcopy = dtD.Copy();
            dtD.Columns.RemoveAt(3);
            dtD.Columns.RemoveAt(2);
            dtD.Columns.RemoveAt(0);

            con.Close();

            dtD.Columns["para"].ColumnName = "Label";
            dtD.AcceptChanges();
            dtD.Columns["1"].ColumnName = "Value";

            dtD.AcceptChanges();




            string jsonResult = JsonConvert.SerializeObject(dtD);

            return jsonResult;

        }
    }
    [WebMethod(EnableSession = true)]

    public static string Visit(string objData)
    {
        string div_code = HttpContext.Current.Session["div_code"].ToString();


        string[] arr = objData.Split('^');
        smonth = arr[0];
        syear = arr[1];
        sSf_Code = arr[2];
        sSfName = arr[3];
        FMName = arr[4];
        using (SqlConnection con = new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["Ereportcon"].ConnectionString))
        {
            DataTable dtv = new DataTable();
            // dt.Columns.Add("Label");
            // dt.Columns.Add("Value");
            string sProc_Name = "";

            sProc_Name = "Manager_Visit_Cat";


            SqlCommand cmd = new SqlCommand(sProc_Name, con);


            cmd.CommandTimeout = 600;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Div_Code", div_code);
            cmd.Parameters.AddWithValue("@Msf_code", sSf_Code);
            cmd.Parameters.AddWithValue("@cMnth", smonth);
            cmd.Parameters.AddWithValue("@cYrs", syear);

            con.Open();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.SelectCommand = cmd;
            // DataTable dt = new DataTable();
            da.Fill(dtv);
            //  dtcopy = dtD.Copy();
            dtv.Columns.RemoveAt(4);
            dtv.Columns.RemoveAt(2);
            dtv.Columns.RemoveAt(1);

            con.Close();

            dtv.Columns["Doc_Cat_SName"].ColumnName = "Label";
            dtv.AcceptChanges();
            dtv.Columns["avg"].ColumnName = "Value";

            dtv.AcceptChanges();




            string jsonResult = JsonConvert.SerializeObject(dtv);

            return jsonResult;

        }
    }
    [WebMethod(EnableSession = true)]

    public static string meter(string objData)
    {
        string div_code = HttpContext.Current.Session["div_code"].ToString();


        string[] arr = objData.Split('^');
        smonth = arr[0];
        syear = arr[1];
        sSf_Code = arr[2];
        sSfName = arr[3];
        FMName = arr[4];
        using (SqlConnection con = new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["Ereportcon"].ConnectionString))
        {
            DataTable dtM = new DataTable();
            // dt.Columns.Add("Label");
            // dt.Columns.Add("Value");
            string sProc_Name = "";

            sProc_Name = "Manager_Call_avg";


            SqlCommand cmd = new SqlCommand(sProc_Name, con);


            cmd.CommandTimeout = 600;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Div_Code", div_code);
            cmd.Parameters.AddWithValue("@Msf_code", sSf_Code);
            cmd.Parameters.AddWithValue("@cMnth", smonth);
            cmd.Parameters.AddWithValue("@cYr", syear);

            con.Open();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.SelectCommand = cmd;
            // DataTable dt = new DataTable();
            da.Fill(dtM);
            //   dtcopy = dt.Copy();
            ////for (int i = 0; i < dtM.Rows.Count; i++)
            ////{
            ////    if (dtM.Rows[i]["Sl_No"].ToString() == "2" || dtM.Rows[i]["Sl_No"].ToString() == "1" || dtM.Rows[i]["Sl_No"].ToString() == "8" || dtM.Rows[i]["Sl_No"].ToString() == "9" || dtM.Rows[i]["Sl_No"].ToString() == "10" || dtM.Rows[i]["Sl_No"].ToString() == "11" || dtM.Rows[i]["Sl_No"].ToString() == "12" || dtM.Rows[i]["Sl_No"].ToString() == "13" || dtM.Rows[i]["Sl_No"].ToString() == "14" || dtM.Rows[i]["Sl_No"].ToString() == "15")
            ////    {
            ////        dtM.Rows[i].Delete();
            ////        dtM.AcceptChanges();
            ////    }

            ////}

            //dtM.Columns.RemoveAt(3);
            //dtM.Columns.RemoveAt(2);
            //dtM.Columns.RemoveAt(0);


            con.Close();

            dtM.Columns["count"].ColumnName = "Value";

            dtM.AcceptChanges();




            string jsonResult = JsonConvert.SerializeObject(dtM);

            return jsonResult;

        }
    }
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static List<values> getChartVal(List<string> lst_Input_Mn_Yr)
    {
        List<values> lstCoverage = new List<values>();

        using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["Ereportcon"].ConnectionString))
        {
            SqlCommand cmd = new SqlCommand();

            cmd.CommandText = "Graph_Team_Calls_Det";

            cmd.CommandType = CommandType.StoredProcedure;
            //cmd.Parameters.AddWithValue("@year", pData[0]);
            cmd.Parameters.AddWithValue("@Div_Code", Convert.ToInt32(sDivCode));
            cmd.Parameters.AddWithValue("@MSf_Code", Convert.ToString(lst_Input_Mn_Yr[2]));
            cmd.Parameters.AddWithValue("@cMnth", Convert.ToInt32(lst_Input_Mn_Yr[0]));
            cmd.Parameters.AddWithValue("@cYr", Convert.ToInt32(lst_Input_Mn_Yr[1]));
            //string cDate = "";
            //if (lst_Input_Mn_Yr[0].ToString() == "12")
            //    cDate = "01-01-" + (Convert.ToInt32(lst_Input_Mn_Yr[1].ToString()) + 1).ToString();
            //else
            //    cDate = lst_Input_Mn_Yr[0].ToString() + "-01-" + lst_Input_Mn_Yr[1].ToString();
            //cmd.Parameters.AddWithValue("@cDate", cDate);
            //   cmd.Parameters.AddWithValue("@cDate", Convert.ToInt32(lst_Input_Mn_Yr[3]));
            cmd.Connection = cn;
            cn.Open();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    values cpData = new values();
                    //  cpData.Sf_Code = dr["Sf_Code"].ToString();

                    cpData.totdrs = Convert.ToDecimal(dr["Total_Drs"].ToString());

                    cpData.Met = Convert.ToDecimal(dr["Drs_Met"].ToString());
                    cpData.Miss = Convert.ToDecimal(dr["Missed"].ToString());
                    //cpData.PSale = Convert.ToDecimal(dr["PSale"].ToString());
                    //cpData.Growth = Convert.ToDecimal(dr["Growth"].ToString());

                    lstCoverage.Add(cpData);
                }
            }
            dr.Close();
            cn.Close();
        }
        return lstCoverage;
    }
    #region Class Values
    public class values
    {
        // public string Sf_Code { get; set; }

        public decimal totdrs { get; set; }
        public decimal Met { get; set; }
        public decimal Miss { get; set; }
        //public decimal PSale { get; set; }
        //public decimal Growth { get; set; }
    }
    #endregion
    protected void btn_shrtcut_Click(object sender, EventArgs e)
    {
        // Response.Redirect("~/Default_MGR.aspx");
    }
    [WebMethod(EnableSession = true)]

    public static string Prod_Det(string objData)
    {
        string div_code = HttpContext.Current.Session["div_code"].ToString();


        string[] arr = objData.Split('^');
        smonth = arr[0];
        syear = arr[1];
        sSf_Code = arr[2];
        sSfName = arr[3];
        FMName = arr[4];
        using (SqlConnection con = new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["Ereportcon"].ConnectionString))
        {

            // dt.Columns.Add("Label");
            // dt.Columns.Add("Value");
            string sProc_Name = "";

            sProc_Name = "Product_Det_Graph";


            SqlCommand cmd = new SqlCommand(sProc_Name, con);


            cmd.CommandTimeout = 600;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Div_Code", div_code);
            cmd.Parameters.AddWithValue("@Msf_code", sSf_Code);
            cmd.Parameters.AddWithValue("@cMnth", smonth);
            cmd.Parameters.AddWithValue("@cYrs", syear);

            con.Open();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.SelectCommand = cmd;
            DataTable dtProd = new DataTable();
            da.Fill(dtProd);




            con.Close();
            dtProd.Columns["Prod_Name"].ColumnName = "Label";
            dtProd.AcceptChanges();
            dtProd.Columns["cnt"].ColumnName = "Value";

            dtProd.AcceptChanges();




            string jsonResult = JsonConvert.SerializeObject(dtProd);

            return jsonResult;

        }
    }

    //protected void btnDownload_Click(object sender, EventArgs e)
    //{
    //string strFileName = "MR_Dashboard";
    //Response.ContentType = "application/pdf";
    //Response.AddHeader("content-disposition", "attachment;filename=" + strFileName + ".pdf");
    //Response.Cache.SetCacheability(HttpCacheability.NoCache);
    //StringWriter sw = new StringWriter();
    //HtmlTextWriter hw = new HtmlTextWriter(sw);
    //HtmlForm frm = new HtmlForm();
    //pnlchart.Parent.Controls.Add(frm);
    //frm.Attributes["runat"] = "server";
    //frm.Controls.Add(pnlchart);
    //frm.RenderControl(hw);
    //StringReader sr = new StringReader(sw.ToString());
    //Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 10f, 4f);
    ////iTextSharp.text.html.simpleparser.HTMLWorker htmlparser = new iTextSharp.text.html.simpleparser.HTMLWorker(pdfDoc);
    //pdfDoc.Open();
    //var writer = PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
    //XMLWorkerHelper.GetInstance().ParseXHtml(writer, pdfDoc, sr);

    ////htmlparser.Parse(sr);
    //pdfDoc.Close();
    //Response.Write(pdfDoc);
    //Response.End();
    //}

  

    protected void ddlMode_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlMode.SelectedItem.Value == "1")
        {
            Response.Redirect("Admin_Sales_DashBoard.aspx");
        }
        if (ddlMode.SelectedItem.Value == "2")
        {
            Response.Redirect("Admin_Primary_Sales_DashBoard.aspx");
        }
    }
}