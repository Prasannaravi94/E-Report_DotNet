using Bus_EReport;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Http;
using System.Globalization;

public partial class MasterFiles_DashBoard_RCPA_Analysis : System.Web.UI.Page
{
    #region Declaration
    DataTable dtrowClr = new System.Data.DataTable();
    DataSet dsTP = null;
    DataSet dsSalesForce = null;
    string div_code = string.Empty;
    string div_Name = string.Empty;
    string sf_code = string.Empty;
    string sf_type = string.Empty;
    string cFMnth = string.Empty;
    string cFYear = string.Empty;
    string cTMnth = string.Empty;
    string cTYear = string.Empty;
    string sCurrentDate = string.Empty;
    int cfmonth;
    int cfyear;
    int ctmonth;
    int ctyear;
    List<int> iLstVstCnt = new List<int>();
    List<int> iLstVstmnt = new List<int>();
    List<int> iLstVstyr = new List<int>();
    string parameter = string.Empty;
    DataSet dsts = new DataSet();
    DataTable dtrowdt = new System.Data.DataTable();

    DataSet dsmgrsf = new DataSet();
    DataTable dtsf_code = new DataTable();
    DataSet dsprod = new DataSet();
    DataSet dschem = new DataSet();
    DataSet dsbus = new DataSet();
    string strConn = System.Configuration.ConfigurationManager.ConnectionStrings["Ereportcon"].ToString();
    string drcode = string.Empty;
    string terrcode = string.Empty;
    string rSF = string.Empty;
    string IsDocView = string.Empty;
    static string smonth = string.Empty;
    static string syear = string.Empty;
    static string sprodcode = string.Empty;

    #endregion
    #region Page_Events
    protected void Page_Load(object sender, EventArgs e)
    {
       // div_Name = Session["div_Name"].ToString();
        Page.Header.DataBind();

       // sf_code = "MR10586";
      //  div_code = "191";
        try
        {
            sf_code = Request.QueryString["SF"].ToString();
            div_code = Request.QueryString["div_code"].ToString();
           
        }
        catch
        {
            div_code = Request.QueryString["div_code"].ToString();
            sf_code = Request.QueryString["sfcode"].ToString();
            cFMnth = Request.QueryString["cMnth"].ToString();
            cFYear = Request.QueryString["cYr"].ToString();
         
        }
        if (sf_code.Contains("MR"))
        {
            sf_type = "1";
        }
        else if (sf_code.Contains("MGR"))
        {
            sf_type = "2";
        }
        else
        {
            sf_type = "3";
        }
        if (IsPostBack)
        {
            string eventTarget = Request["__EVENTTARGET"];
            string eventArgument = Request["__EVENTARGUMENT"];
            string eventArgument2 = Request["__EVENTARGUMENT"];

            if (eventTarget == "potentialLink")
            {
                string[] args = eventArgument.Split('|');
                string code = args[0];
                string name = args.Length > 1 ? args[1] : "";
                string strConn = System.Configuration.ConfigurationManager.ConnectionStrings["Ereportcon"].ToString();
                using (SqlConnection con = new SqlConnection(strConn))
                {
                    string sProc_Name = "RCPA_Analysis_Detail_PD_Multiple";
                    SqlCommand cmd = new SqlCommand(sProc_Name, con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Div_Code", div_code);
                    cmd.Parameters.AddWithValue("@Msf_code", ddlFieldForce.SelectedValue);
                    cmd.Parameters.AddWithValue("@cMnth", ddlMonth.SelectedValue);
                    cmd.Parameters.AddWithValue("@cYr", ddlYear.SelectedValue);
                    cmd.Parameters.AddWithValue("@map_code", code);
                    cmd.CommandTimeout = 800;

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    if (dt.Rows.Count > 0)
                    {
                        SalesForce sf = new SalesForce();
                        string strFrmMonth = sf.getMonthName(ddlMonth.SelectedValue);
                        string input = strFrmMonth + " " + ddlYear.SelectedValue;
                        //  DateTime dt2 = DateTime.ParseExact(input, "MMM yyyy", System.Globalization.CultureInfo.InvariantCulture);
                        DateTime dt2 = DateTime.ParseExact(input, "MMMM yyyy", System.Globalization.CultureInfo.InvariantCulture);

                        DateTime prev1 = dt2.AddMonths(-1); 
                        DateTime prev2 = dt2.AddMonths(-2);  

                        string prev1Str = prev1.ToString("MMM yyyy", System.Globalization.CultureInfo.InvariantCulture);
                        string prev2Str = prev2.ToString("MMM yyyy", System.Globalization.CultureInfo.InvariantCulture);

                        StringBuilder sb = new StringBuilder();
                        sb.Append("<style>");
                        sb.Append(".hidden-col { display: none; }");
                        sb.Append("@media screen and (max-width: 600px) {");
                        sb.Append("    th { writing-mode: vertical-rl; transform: rotate(180deg); white-space: nowrap; text-align: center; padding: 10px; }");
                        sb.Append("}");
                        sb.Append("</style>");
                        sb.Append("<h4 style='margin-top:10px;'>Product : <span style='color:blue;'>" + name + "</span></h4>");
                        sb.Append("<h4 style='margin-top:10px;'>Range   : <span style='color:blue;'>" + input + " - " + prev2Str + " </span></h4>");

                        sb.Append("<table border='1' cellpadding='5' cellspacing='0' style='border-collapse:collapse; width:100%;'>");

                        // Add table header
                        sb.Append("<tr style='background-color:#f2f2f2;'>");
                        foreach (DataColumn column in dt.Columns)
                        {
                            string className = (column.ColumnName == "Doctor Code" || column.ColumnName == "SortOrder" || column.ColumnName == "Sno") ? " class='hidden-col'" : "";
                            sb.Append("<th style='padding:8px;'").Append(className).Append("><div>").Append(column.ColumnName).Append("</div></th>");
                        }
                        sb.Append("</tr>");

                        // Track previous row values to handle rowspan
                        // Group the rows by Doctor Code
                        var grouped = dt.AsEnumerable()
                            .GroupBy(r => r["Doctor Code"].ToString());

                        foreach (var group in grouped)
                        {
                            int rowspan = group.Count();
                            bool isFirstRow = true;

                            foreach (DataRow row in group)
                            {
                                string val = row["Doctor Name"].ToString().Trim().ToLower();
                                bool isTotalRow = val.Contains("total");
                                string trStyle = isTotalRow ? " style='background-color:#f2f2f2; font-weight:bold;'" : "";
                                sb.Append("<tr").Append(trStyle).Append(">");

                                foreach (DataColumn column in dt.Columns)
                                {
                                    string colName = column.ColumnName;
                                    string className = (colName == "Doctor Code" || colName == "SortOrder" || colName == "Sno") ? " class='hidden-col'" : "";
                                    string cellValue = row[column].ToString();

                                    //if (colName == "Sno" && isFirstRow)
                                    //{
                                    //    sb.Append("<td style='padding:8px;' rowspan='").Append(rowspan).Append("'>")
                                    //        .Append(cellValue).Append("</td>");
                                    //}
                                    //else 
                                    if (colName == "Doctor Name" && isFirstRow)
                                    {
                                        sb.Append("<td style='padding:8px;' rowspan='").Append(rowspan).Append("'>")
                                            .Append(cellValue).Append("</td>");
                                    }
                                    else if (colName == "Category" && isFirstRow)
                                    {
                                        sb.Append("<td style='padding:8px;' rowspan='").Append(rowspan).Append("'>")
                                            .Append(cellValue).Append("</td>");
                                    }
                                    else if (colName == "Doctor Name" || colName == "Category")
                                    {
                                        
                                        continue;
                                    }
                                    else if (colName == "Yield")
                                    {
                                        string code2 = row["Doctor Code"].ToString().Replace("'", "\\'");
                                        string name2 = row["Doctor Name"].ToString().Replace("'", "\\'");
                                        string mn = row["Month"].ToString().Replace("'", "\\'");

                                        string eventArg = code2 + "|" + name2 + "|" + code + "|" + mn;

                                       
                                            //.Append("<a href=\"javascript:__doPostBack('yieldLink','")
                                            //.Append(eventArg)
                                            //.Append("')\" style='color:blue; text-decoration:underline;'>")
                                            //.Append(cellValue)
                                            //.Append("</a></td>");

                                        if (cellValue != "0")
                                        {
                                            sb.Append("<td style='padding:8px;'>")
                                            .Append("<a href=\"javascript:__doPostBack('yieldLink','")
                                              .Append(eventArg)
                                              .Append("')\" style='color:blue; text-decoration:underline;'>")
                                              .Append(cellValue)
                                              .Append("</a>");
                                        }
                                        else
                                        {
                                            sb.Append("<td style='padding:8px;'>").Append(cellValue); // Just plain text without link
                                        }
                                    }
                                    else
                                    {
                                        sb.Append("<td style='padding:8px;'").Append(className).Append(">")
                                            .Append(cellValue).Append("</td>");
                                    }
                                }

                                sb.Append("</tr>");
                                isFirstRow = false;
                            }
                        


                        sb.Append("</tr>");
                        }
                        sb.Append("</table>");
                        ltdet.Text = sb.ToString();
                    }
                    else
                    {
                        ltdet.Text = "<p>No data found.</p>";
                    }
                }
                ScriptManager.RegisterStartupScript(this, GetType(), "showDetailsDiv", @"
                document.getElementById('detailsView').style.display = 'block';
            ", true);
                
            }
            else if (eventTarget == "yieldLink")
            {
                string[] args = eventArgument.Split('|');
                string code = args[0];
                string name = args.Length > 1 ? args[1] : "";
                string pcode = args[2];
                string pmnth = args[3];

                string[] arr = pmnth.Split('-');
                // int mnth = Convert.ToInt32(arr[0]);


                //      int mnth = Convert.ToInt32(arr[0]); 

                string mnth = arr[0].Trim();

                int smonth = DateTime.ParseExact(mnth, "MMM", System.Globalization.CultureInfo.InvariantCulture).Month;


                string strConn = System.Configuration.ConfigurationManager.ConnectionStrings["Ereportcon"].ToString();
                using (SqlConnection con = new SqlConnection(strConn))
                {
                    string sProc_Name = "RCPA_Analysis_Detail_Chemist";
                    SqlCommand cmd = new SqlCommand(sProc_Name, con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Div_Code", div_code);
                    cmd.Parameters.AddWithValue("@Msf_code", ddlFieldForce.SelectedValue);
                    cmd.Parameters.AddWithValue("@cMnth", smonth);
                    cmd.Parameters.AddWithValue("@cYr", arr[1].Trim());
                    cmd.Parameters.AddWithValue("@map_code", pcode);
                    cmd.Parameters.AddWithValue("@Lst_code", code);
                    cmd.CommandTimeout = 800;

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    if (dt.Rows.Count > 0)
                    {
                        StringBuilder sb = new StringBuilder();
                        sb.Append("<style>");
                        sb.Append(".hidden-col { display: none; }");
                        sb.Append("@media screen and (max-width: 600px) {");
                        sb.Append("    th { writing-mode: vertical-rl; transform: rotate(180deg); white-space: nowrap; text-align: center; padding: 10px; }");
                        sb.Append("}");
                        sb.Append("</style>");
                        sb.Append("<h4 style='margin-top:10px;'>Doctor Name : <span style='color:blue;'>" + name + " (  "+ pmnth + " )</span></h4>");

                        sb.Append("<table border='1' cellpadding='5' cellspacing='0' style='border-collapse:collapse; width:100%;'>");

                        // Add table header
                        sb.Append("<tr style='background-color:#f2f2f2;'>");
                        foreach (DataColumn column in dt.Columns)
                        {
                            string className = column.ColumnName == "Doctor Code" ? " class='hidden-col'" : "";
                            sb.Append("<th style='padding:8px;'").Append(className).Append("><div>").Append(column.ColumnName).Append("</div></th>");
                        }
                        sb.Append("</tr>");

                        // Add table rows
                        foreach (DataRow row in dt.Rows)
                        {
                            // sb.Append("<tr>");
                            string val = row["Supportive Chemist"].ToString().Trim().ToLower();


                            bool isTotalRow = val.Contains("total");

                            // Highlight full row if it's a total row
                            string trStyle = isTotalRow ? " style='background-color:#f2f2f2; font-weight:bold;'" : "";
                            sb.Append("<tr").Append(trStyle).Append(">");
                            foreach (DataColumn column in dt.Columns)
                            {

                                string className = column.ColumnName == "Doctor Code" ? " class='hidden-col'" : "";
                                //string codeValue = row["Code"].ToString().Replace("'", "\\'");
                                if (column.ColumnName == "Sno" && row[column].ToString() == "0")
                                {
                                    sb.Append("<td style='padding:8px;'").Append(className).Append("></td>"); // Empty cell
                                }
                               
                                else
                                {
                                    sb.Append("<td style='padding:8px;'").Append(className).Append(">").Append(row[column].ToString()).Append("</td>");
                                    // sb.Append("<td style='padding:8px;'>").Append(row[column].ToString()).Append("</td>");
                                }

                            }
                            sb.Append("</tr>");
                        }
                        sb.Append("</table>");
                        ltchem.Text = sb.ToString();
                    }
                    else
                    {
                        ltchem.Text = "<p>No data found.</p>";
                    }
                }
                ScriptManager.RegisterStartupScript(this, GetType(), "showDetailsDiv", @"
                document.getElementById('detailsView').style.display = 'block';var targetoffset=$('#detailsView').offset().top;$('html, body').animate({scrollTop:$(document).height()-targetoffset},600)
            ", true);

            }
            else if (eventTarget == "goBack")
            {
                ltchem.Text = "";
                if (rdorcpa.SelectedValue == "0")
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "loadChart", "Product();GetDropdown();", true);
                }
                else if (rdorcpa.SelectedValue == "1")
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "loadChart", "DoctorGraph();", true);
                }
                else if (rdorcpa.SelectedValue == "2")
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "loadChart", "Trend();", true);
                }
                ScriptManager.RegisterStartupScript(this, GetType(), "toggleViews", @"
    document.getElementById('detailsView').style.display = 'none'; 
", true);
            }

        }
        if (!Page.IsPostBack)
        {
            FillProd();

            TourPlan tp2 = new TourPlan();
            dsTP = tp2.Get_TP_Edit_Year(div_code);
            if (dsTP.Tables[0].Rows.Count > 0)
            {
                for (int k = Convert.ToInt16(dsTP.Tables[0].Rows[0]["Year"]); k <= DateTime.Now.Year + 1; k++)
                {
                    ddlYear.Items.Add(k.ToString());
                    ddlYear.SelectedValue = DateTime.Now.Year.ToString();

                }
            }
            ddlMonth.SelectedValue = DateTime.Now.Month.ToString();
            if (sf_type == "1" || sf_type == "MR")
            {

                FillManagers();

                ddlFieldForce.SelectedValue = sf_code;
                ddlFieldForce.Enabled = false;

            }
            else if (sf_type == "2" || sf_type == "MGR")
            {
                FillManagers();
                //  ddlFieldForce.SelectedValue = sf_code;

                //ddlFieldForce.SelectedIndex = 2;
            }
            else
            {
                if (div_code.Contains(','))
                {
                    div_code = div_code.Remove(div_code.Length - 1);
                }
                FillManagers();
                ddlFieldForce.SelectedIndex = 2;
            }

            TourPlan tp = new TourPlan();
            dsTP = tp.Get_TP_Edit_Year(div_code);

        }
        else
        {
            if (sf_type == "1" || sf_type == "MR")
            {

            }
            else if (sf_type == "2" || sf_type == "MGR")
            {

            }
            else
            {
                if (div_code.Contains(','))
                {
                    div_code = div_code.Remove(div_code.Length - 1);
                }
            }
        }
    }
    #endregion

    //protected void ddlprod_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    main.Visible = false;
    //}
    protected void ddlFieldForce_SelectedIndexChanged(object sender, EventArgs e)
    {
        main.Visible = false;
        if (ddlFieldForce.SelectedValue != "0")
        {

        }
    }
    private void FillProd()
    {
        Product prod = new Product();
        DataSet dsprod = new DataSet();
        dsprod = prod.getProd(div_code);
        if (dsprod.Tables[0].Rows.Count > 0)
        {
            lstProd.DataValueField = "Product_Code_SlNo";
            lstProd.DataTextField = "Product_Detail_Name";
            lstProd.DataSource = dsprod;
            lstProd.DataBind();
        }
    }
    #region FillManagers
    private void FillManagers()
    {
        SalesForce sf = new SalesForce();
        //dsSalesForce = sf.Hierarchy_Team(div_code, ddlFieldForce.SelectedValue);
        if (sf_type == "1" || sf_type == "MR")
        {
            dsSalesForce = sf.SalesForceListMgrGet(div_code, sf_code);
        }
        else
        {
            dsSalesForce = sf.SalesForceListMgrGet_MRonly(div_code, sf_code);
        }

        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            for (int i = dsSalesForce.Tables[0].Rows.Count - 1; i >= 0; i--)
            {
                DataRow dr = dsSalesForce.Tables[0].Rows[i];
                if (dr["sf_code"].ToString() == "admin")
                    dr.Delete();
            }

            dsSalesForce.AcceptChanges();

            ddlFieldForce.DataTextField = "sf_name";
            ddlFieldForce.DataValueField = "sf_code";
            ddlFieldForce.DataSource = dsSalesForce;
            ddlFieldForce.DataBind();
            ddlFieldForce.Items.Insert(0, new ListItem("---Select---", "-1"));
            if (Request.QueryString["rSF"] != "-1")
            {
                //btnback.Visible = false;
                ddlFieldForce.SelectedValue = Request.QueryString["rSF"];
                ddlFieldForce.Enabled = false;
                // Proceed with docId
            }
        }
    }
    #endregion

    //#region btnGo_Click
    //protected void btnGo_Click(object sender, EventArgs e)
    //{
    //    lstProd.Disabled = true;
    //    ddlMonth.Enabled = false;
    //    ddlYear.Enabled = false;
    //    rdorcpa.Enabled = false;

    //    if (rdorcpa.SelectedIndex == 0)
    //    {
    //        main.Visible = true;
    //        divdoctor.Visible = false;
    //        divtrend.Visible = false;
    //        GetRCPA();
    //        ScriptManager.RegisterStartupScript(this, GetType(), "loadChart", "Product();GetDropdown();", true);
    //    }
    //    else if (rdorcpa.SelectedIndex == 1)
    //    {
    //        main.Visible = false;
    //        divtrend.Visible = false;
    //        divdoctor.Visible = true;
    //        GetRCPADr();
    //        ScriptManager.RegisterStartupScript(this, GetType(), "loadChart2", "DoctorGraph();", true);
    //    }
    //    else if (rdorcpa.SelectedIndex == 2)
    //    {
    //        main.Visible = false;
    //        divdoctor.Visible = false;
    //        divtrend.Visible = true;
    //        GetRCPATrend();
    //        GetRCPAlOWTrend();
    //        ScriptManager.RegisterStartupScript(this, GetType(), "loadChart3", "Trend();", true);
    //    }
    //    // FillVisit();
    //}
    //#endregion


    #region btnGo_Click
    protected void btnGo_Click(object sender, EventArgs e)
    {

       // string divName = Session["div_Name"].ToString();
        string sfName = ddlFieldForce.SelectedItem.Text;
        DateTime visitDate = DateTime.Now;

        SalesForce sf = new SalesForce();


        int insertResult = sf.rcpaadd(sf_code, div_code, "", sfName, visitDate);

        lstProd.Disabled = true;
        ddlMonth.Enabled = false;
        ddlYear.Enabled = false;
        rdorcpa.Enabled = false;

        if (rdorcpa.SelectedIndex == 0)
        {
            main.Visible = true;
            divdoctor.Visible = false;
            divtrend.Visible = false;
            GetRCPA();
            ScriptManager.RegisterStartupScript(this, GetType(), "loadChart", "Product();GetDropdown();", true);
        }
        else if (rdorcpa.SelectedIndex == 1)
        {
            main.Visible = false;
            divtrend.Visible = false;
            divdoctor.Visible = true;
            GetRCPADr();
            ScriptManager.RegisterStartupScript(this, GetType(), "loadChart2", "DoctorGraph();", true);
        }
        else if (rdorcpa.SelectedIndex == 2)
        {
            main.Visible = false;
            divdoctor.Visible = false;
            divtrend.Visible = true;
            GetRCPATrend();
            GetRCPAlOWTrend();
            ScriptManager.RegisterStartupScript(this, GetType(), "loadChart3", "Trend();", true);
        }
    }


    #endregion

    private void GetRCPA()
    {

        SalesForce sf = new SalesForce();
        DCR dcc = new DCR();
        // DB_EReporting db = new DB_EReporting();
        string map_prod = string.Empty;
        foreach (System.Web.UI.WebControls.ListItem item in lstProd.Items)
        {
            if (item.Selected)
            {
                map_prod += item.Value + ',';
            }
        }
        if (map_prod != "")
        {
            map_prod = map_prod.Remove(map_prod.Length - 1);
        }
        string strConn = System.Configuration.ConfigurationManager.ConnectionStrings["Ereportcon"].ToString();
        using (SqlConnection con = new SqlConnection(strConn))
        {
            string sProc_Name = "RCPA_Analysis_PD";
            SqlCommand cmd = new SqlCommand(sProc_Name, con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Div_Code", div_code);
            cmd.Parameters.AddWithValue("@Msf_code", ddlFieldForce.SelectedValue);
            cmd.Parameters.AddWithValue("@cMnth", ddlMonth.SelectedValue);
            cmd.Parameters.AddWithValue("@cYr", ddlYear.SelectedValue);
            cmd.Parameters.AddWithValue("@map_code", map_prod);
            //   cmd.Parameters.AddWithValue("@ListDrCode", "");
            cmd.CommandTimeout = 800;

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            if (dt.Rows.Count > 0)
            {

                StringBuilder sb = new StringBuilder();
                sb.Append("<style>");
                sb.Append(".hidden-col { display: none; }"); // Hide any th/td with this class

                sb.Append("@media screen and (max-width: 600px) {"); // Apply only on small screens
                sb.Append("    th { writing-mode: vertical-rl; transform: rotate(180deg); white-space: nowrap; text-align: center; padding: 10px; }");
                sb.Append("}");
                sb.Append("</style>");

                sb.Append("<table border='1' cellpadding='5' cellspacing='0' style='border-collapse:collapse; width:100%;'>");

                sb.Append("<tr style='background-color:#f2f2f2;'>");
                foreach (DataColumn column in dt.Columns)
                {
                    string className = column.ColumnName == "Code" ? " class='hidden-col'" : "";
                    sb.Append("<th style='padding:8px;'").Append(className).Append("><div>").Append(column.ColumnName).Append("</div></th>");

                }
                sb.Append("</tr>");

                
                foreach (DataRow row in dt.Rows)
                {
                    string productValue2 = row["Product"].ToString().Trim().ToLower();

                   
                    bool isTotalRow = productValue2.Contains("total"); 

                    // Highlight full row if it's a total row
                    string trStyle = isTotalRow ? " style='background-color:#f2f2f2; font-weight:bold;'" : "";
                    sb.Append("<tr").Append(trStyle).Append(">");
                    foreach (DataColumn column in dt.Columns)
                    {
                        string className = column.ColumnName == "Code" ? " class='hidden-col'" : "";
                        //string codeValue = row["Code"].ToString().Replace("'", "\\'");
                        if (column.ColumnName == "Sno" && row[column].ToString() == "0")
                        {
                            sb.Append("<td style='padding:8px;'").Append(className).Append("></td>"); // Empty cell
                        }
                        else if (column.ColumnName == "Potential")
                        {
                            string productValue = row["Product"].ToString();

                            if (productValue.Contains("Total") )
                            {
                                sb.Append("<td style='padding:8px;'").Append(className).Append(">").Append(row[column].ToString()).Append("</td>");

                            }
                            else
                            {
                                string potentialValue = row[column].ToString();

                                string code = row["Code"].ToString().Replace("'", "\\'");
                                string name = row["Product"].ToString().Replace("'", "\\'");

                                string eventArgument = code + "|" + name;

                                sb.Append("<td style='padding:8px;'>")
                                  .Append("<a href=\"javascript:__doPostBack('potentialLink','")
                                  .Append(eventArgument)
                                  .Append("')\" style='color:blue; text-decoration:underline;'>")
                                  .Append(potentialValue)
                                  .Append("</a></td>");
                            }


                        }
                        else
                        {
                            sb.Append("<td style='padding:8px;'").Append(className).Append(">").Append(row[column].ToString()).Append("</td>");
                            // sb.Append("<td style='padding:8px;'>").Append(row[column].ToString()).Append("</td>");
                        }

                    }

                    sb.Append("</tr>");
                }
                sb.Append("</table>");

                // Assign the generated HTML to the Literal control
                ltrcpa.Text = sb.ToString();
            }
            else
            {
                ltrcpa.Text = "<p>No records found.</p>";
            }

        }
    }
    private void GetRCPADr()
    {

        SalesForce sf = new SalesForce();
        DCR dcc = new DCR();
        // DB_EReporting db = new DB_EReporting();
        string map_prod = string.Empty;
        foreach (System.Web.UI.WebControls.ListItem item in lstProd.Items)
        {
            if (item.Selected)
            {
                map_prod += item.Value + ',';
            }
        }
        if (map_prod != "")
        {
            map_prod = map_prod.Remove(map_prod.Length - 1);
        }
        string strConn = System.Configuration.ConfigurationManager.ConnectionStrings["Ereportcon"].ToString();
        using (SqlConnection con = new SqlConnection(strConn))
        {
            string sProc_Name = "RCPA_Analysis_Doctorwise";
            SqlCommand cmd = new SqlCommand(sProc_Name, con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Div_Code", div_code);
            cmd.Parameters.AddWithValue("@Msf_code", ddlFieldForce.SelectedValue);
            cmd.Parameters.AddWithValue("@cMnth", ddlMonth.SelectedValue);
            cmd.Parameters.AddWithValue("@cYr", ddlYear.SelectedValue);
            cmd.Parameters.AddWithValue("@map_code", map_prod);
            //   cmd.Parameters.AddWithValue("@ListDrCode", "");
            cmd.CommandTimeout = 800;

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            if (dt.Rows.Count > 0)
            {

                StringBuilder sb = new StringBuilder();
                sb.Append("<style>");
                sb.Append(".hidden-col { display: none; }"); // Hide any th/td with this class

                sb.Append("@media screen and (max-width: 600px) {"); // Apply only on small screens
                sb.Append("    th { writing-mode: vertical-rl; transform: rotate(180deg); white-space: nowrap; text-align: center; padding: 10px; }");
                sb.Append("}");
                sb.Append("</style>");

                sb.Append("<table border='1' cellpadding='5' cellspacing='0' style='border-collapse:collapse; width:100%;'>");

                // Add table header
                sb.Append("<tr style='background-color:#f2f2f2;'>");
                foreach (DataColumn column in dt.Columns)
                {
                    string className = column.ColumnName == "Code" ? " class='hidden-col'" : "";
                    sb.Append("<th style='padding:8px;'").Append(className).Append("><div>").Append(column.ColumnName).Append("</div></th>");

                    // sb.Append("<th style='padding:8px;'><div>").Append(column.ColumnName).Append("</div></th>");
                }
                sb.Append("</tr>");

                // Add table rows
                foreach (DataRow row in dt.Rows)
                {
                    //  sb.Append("<tr>");
                    string productValue2 = row["Product"].ToString().Trim().ToLower();


                    bool isTotalRow = productValue2.Contains("total");

                    // Highlight full row if it's a total row
                    string trStyle = isTotalRow ? " style='background-color:#f2f2f2; font-weight:bold;'" : "";
                    sb.Append("<tr").Append(trStyle).Append(">");
                    foreach (DataColumn column in dt.Columns)
                    {
                        string className = column.ColumnName == "Code" ? " class='hidden-col'" : "";

                        if (column.ColumnName == "Sno" && row[column].ToString() == "0")
                        {
                            sb.Append("<td style='padding:8px;'").Append(className).Append("></td>"); // Empty cell
                        }
                        else if (column.ColumnName == "Contribution Doctors") // Check if it's the Potential column
                        {
                            // string potentialValue = row[column].ToString();
                            //string linkUrl = "https://yourwebsite.com/details?value=" + potentialValue; // Change to your desired URL

                            //sb.Append("<td style='padding:8px;'><a href='" + linkUrl + "' target='_blank' style='color:blue; text-decoration:underline;'>")
                            //  .Append(potentialValue)
                            //  .Append("</a></td>");
                            string productValue = row["Product"].ToString();

                            if (productValue.Contains("Total"))
                            {
                                sb.Append("<td style='padding:8px;'").Append(className).Append(">").Append(row[column].ToString()).Append("</td>");

                            }
                            else
                            {
                                string potentialValue = row[column].ToString();

                                string code = row["Code"].ToString().Replace("'", "\\'");
                                string name = row["Product"].ToString().Replace("'", "\\'");

                                string eventArgument = code + "|" + name; // Combine with a delimiter

                                sb.Append("<td style='padding:8px;'>")
                                  .Append("<a href=\"javascript:__doPostBack('potentialLink','")
                                  .Append(eventArgument)
                                  .Append("')\" style='color:blue; text-decoration:underline;'>")
                                  .Append(potentialValue)
                                  .Append("</a></td>");
                            }
                        }
                        else
                        {
                            sb.Append("<td style='padding:8px;'").Append(className).Append(">").Append(row[column].ToString()).Append("</td>");
                            // sb.Append("<td style='padding:8px;'>").Append(row[column].ToString()).Append("</td>");
                        }

                    }

                    sb.Append("</tr>");
                }
                sb.Append("</table>");

                // Assign the generated HTML to the Literal control
                ltdrrcpa.Text = sb.ToString();
            }
            else
            {
                ltdrrcpa.Text = "<p>No records found.</p>";
            }

        }
    }
    private void GetRCPATrend()
    {

        SalesForce sf = new SalesForce();
        DCR dcc = new DCR();
        // DB_EReporting db = new DB_EReporting();
        string map_prod = string.Empty;
        foreach (System.Web.UI.WebControls.ListItem item in lstProd.Items)
        {
            if (item.Selected)
            {
                map_prod += item.Value + ',';
            }
        }
        if (map_prod != "")
        {
            map_prod = map_prod.Remove(map_prod.Length - 1);
        }
        string strConn = System.Configuration.ConfigurationManager.ConnectionStrings["Ereportcon"].ToString();
        using (SqlConnection con = new SqlConnection(strConn))
        {
            string sProc_Name = "RCPA_Analysis_Doctorwise_H_L";
            SqlCommand cmd = new SqlCommand(sProc_Name, con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Div_Code", div_code);
            cmd.Parameters.AddWithValue("@Msf_code", ddlFieldForce.SelectedValue);
            cmd.Parameters.AddWithValue("@cMnth", ddlMonth.SelectedValue);
            cmd.Parameters.AddWithValue("@cYr", ddlYear.SelectedValue);
            cmd.Parameters.AddWithValue("@map_code", map_prod);
            //   cmd.Parameters.AddWithValue("@ListDrCode", "");
            cmd.CommandTimeout = 800;

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            if (dt.Rows.Count > 0)
            {

                StringBuilder sb = new StringBuilder();
                sb.Append("<style>");
                sb.Append(".hidden-col { display: none; }"); // Hide any th/td with this class

                sb.Append("@media screen and (max-width: 600px) {"); // Apply only on small screens
                sb.Append("    th { writing-mode: vertical-rl; transform: rotate(180deg); white-space: nowrap; text-align: center; padding: 10px; }");
                sb.Append("}");
                sb.Append("</style>");

                sb.Append("<table border='1' cellpadding='5' cellspacing='0' style='border-collapse:collapse; width:100%;'>");

                // Add table header
                sb.Append("<tr style='background-color:#f2f2f2;'>");
                foreach (DataColumn column in dt.Columns)
                {
                    string className = column.ColumnName == "Code" ? " class='hidden-col'" : "";
                    sb.Append("<th style='padding:8px;'").Append(className).Append("><div>").Append(column.ColumnName).Append("</div></th>");

                    // sb.Append("<th style='padding:8px;'><div>").Append(column.ColumnName).Append("</div></th>");
                }
                sb.Append("</tr>");

                // Add table rows
                foreach (DataRow row in dt.Rows)
                {
                    //sb.Append("<tr>");
                    string productValue2 = row["Product"].ToString().Trim().ToLower();


                    bool isTotalRow = productValue2.Contains("Total");

                    // Highlight full row if it's a total row
                    string trStyle = isTotalRow ? " style='background-color:#f2f2f2; font-weight:bold;'" : "";
                    sb.Append("<tr").Append(trStyle).Append(">");
                    foreach (DataColumn column in dt.Columns)
                    {
                        string className = column.ColumnName == "Code" ? " class='hidden-col'" : "";

                        //if (column.ColumnName == "Sno" && row[column].ToString() == "0")
                        //{
                        //    sb.Append("<td style='padding:8px;'").Append(className).Append("></td>"); // Empty cell
                        //}
                        if (column.ColumnName == "Contribution Doctors") // Check if it's the Potential column
                        {
                            string potentialValue = row[column].ToString();

                            string code = row["Code"].ToString().Replace("'", "\\'");
                            string name = row["Product"].ToString().Replace("'", "\\'");

                            string eventArgument = code + "|" + name; // Combine with a delimiter

                            sb.Append("<td style='padding:8px;'>")
                              .Append("<a href=\"javascript:__doPostBack('potentialLink','")
                              .Append(eventArgument)
                              .Append("')\" style='color:blue; text-decoration:underline;'>")
                              .Append(potentialValue)
                              .Append("</a></td>");

                        }
                        else
                        {
                            sb.Append("<td style='padding:8px;'").Append(className).Append(">").Append(row[column].ToString()).Append("</td>");
                            // sb.Append("<td style='padding:8px;'>").Append(row[column].ToString()).Append("</td>");
                        }

                    }

                    sb.Append("</tr>");
                }
                sb.Append("</table>");

                // Assign the generated HTML to the Literal control
                lthightrend.Text = sb.ToString();
            }
            else
            {
                lthightrend.Text = "<p>No records found.</p>";
            }

        }
    }
    private void GetRCPAlOWTrend()
    {

        SalesForce sf = new SalesForce();
        DCR dcc = new DCR();
        // DB_EReporting db = new DB_EReporting();
        string map_prod = string.Empty;
        foreach (System.Web.UI.WebControls.ListItem item in lstProd.Items)
        {
            if (item.Selected)
            {
                map_prod += item.Value + ',';
            }
        }
        if (map_prod != "")
        {
            map_prod = map_prod.Remove(map_prod.Length - 1);
        }
        string strConn = System.Configuration.ConfigurationManager.ConnectionStrings["Ereportcon"].ToString();
        using (SqlConnection con = new SqlConnection(strConn))
        {
            string sProc_Name = "RCPA_Analysis_Doctorwise_L_H";
            SqlCommand cmd = new SqlCommand(sProc_Name, con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Div_Code", div_code);
            cmd.Parameters.AddWithValue("@Msf_code", ddlFieldForce.SelectedValue);
            cmd.Parameters.AddWithValue("@cMnth", ddlMonth.SelectedValue);
            cmd.Parameters.AddWithValue("@cYr", ddlYear.SelectedValue);
            cmd.Parameters.AddWithValue("@map_code", map_prod);
            //   cmd.Parameters.AddWithValue("@ListDrCode", "");
            cmd.CommandTimeout = 800;

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            if (dt.Rows.Count > 0)
            {

                StringBuilder sb = new StringBuilder();
                sb.Append("<style>");
                sb.Append(".hidden-col { display: none; }"); // Hide any th/td with this class

                sb.Append("@media screen and (max-width: 600px) {"); // Apply only on small screens
                sb.Append("    th { writing-mode: vertical-rl; transform: rotate(180deg); white-space: nowrap; text-align: center; padding: 10px; }");
                sb.Append("}");
                sb.Append("</style>");

                sb.Append("<table border='1' cellpadding='5' cellspacing='0' style='border-collapse:collapse; width:100%;'>");

                // Add table header
                sb.Append("<tr style='background-color:#f2f2f2;'>");
                foreach (DataColumn column in dt.Columns)
                {
                    string className = column.ColumnName == "Code" ? " class='hidden-col'" : "";
                    sb.Append("<th style='padding:8px;'").Append(className).Append("><div>").Append(column.ColumnName).Append("</div></th>");

                    // sb.Append("<th style='padding:8px;'><div>").Append(column.ColumnName).Append("</div></th>");
                }
                sb.Append("</tr>");

                // Add table rows
                foreach (DataRow row in dt.Rows)
                {
                    sb.Append("<tr>");
                    foreach (DataColumn column in dt.Columns)
                    {

                        string columnName = column.ColumnName;
                        string cellValue = row[column].ToString().Replace("'", "\\'");
                        string codeValue = row["Code"].ToString().Replace("'", "\\'"); // Get the Code value
                        string className = columnName == "Code" ? " class='hidden-col'" : "";

                        if (columnName == "Contribution Doctors")
                        {
                            string potentialValue = row[column].ToString();

                            string code = row["Code"].ToString().Replace("'", "\\'");
                            string name = row["Product"].ToString().Replace("'", "\\'");

                            string eventArgument = code + "|" + name; // Combine with a delimiter

                            sb.Append("<td style='padding:8px;'>")
                              .Append("<a href=\"javascript:__doPostBack('potentialLink','")
                              .Append(eventArgument)
                              .Append("')\" style='color:blue; text-decoration:underline;'>")
                              .Append(cellValue)
                              .Append("</a></td>");
                        }

                        else
                        {
                            sb.Append("<td style='padding:8px;'").Append(className).Append(">")
                              .Append(cellValue)
                              .Append("</td>");
                        }

                    }

                    sb.Append("</tr>");
                }
                sb.Append("</table>");

                // Assign the generated HTML to the Literal control
                ltlowtrend.Text = sb.ToString();
            }
            else
            {
                ltlowtrend.Text = "<p>No records found.</p>";
            }

        }
    }
    [WebMethod]
    public static string RCPA(string objData)
    {
        // string div_code = HttpContext.Current.Session["div_code"].ToString();
     

        string msf_code = string.Empty;
       
        string[] arr = objData.Split('^');
        smonth = arr[0];
        syear = arr[1];
        sprodcode = arr[2];
        msf_code = arr[3];
        string divcode = arr[4];

        using (SqlConnection con = new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["Ereportcon"].ConnectionString))
        {
            DataTable dt = new DataTable();
            // dt.Columns.Add("Label");
            // dt.Columns.Add("Value");
            string sProc_Name = "";

            sProc_Name = "RCPA_Analysis_PD_Graph";

            SqlCommand cmd = new SqlCommand(sProc_Name, con);


            cmd.CommandTimeout = 600;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Div_Code", divcode);
            cmd.Parameters.AddWithValue("@Msf_code", msf_code);
            cmd.Parameters.AddWithValue("@cMnth", smonth);
            cmd.Parameters.AddWithValue("@cYr", syear);
            cmd.Parameters.AddWithValue("@map_code", sprodcode);
            con.Open();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.SelectCommand = cmd;
            // DataTable dt = new DataTable();
            da.Fill(dt);

            con.Close();

            int rowCount = dt.Rows.Count;
            int columnCount = dt.Columns.Count;


            object[,] arrData = new object[rowCount, columnCount];


            for (int i = 0; i < rowCount; i++)
            {
                for (int j = 0; j < columnCount; j++)
                {
                    arrData[i, j] = dt.Rows[i][j];

                    if (string.IsNullOrEmpty(dt.Rows[i][j].ToString()))
                    {
                        // Write your Custom Code
                        dt.Rows[i][j] = 0;
                        arrData[i, j] = dt.Rows[i][j];
                    }

                }
            }

            StringBuilder jsonData = new StringBuilder();
            StringBuilder Product = new StringBuilder();
            StringBuilder Yield = new StringBuilder();
            StringBuilder Comp = new StringBuilder();
            StringBuilder CompPer = new StringBuilder();

            jsonData.Append("{" +
                //Initialize the chart object with the chart-level attributes..
                "'chart': {" +
                    "'caption': ''," +
                    "'bgcolor': 'FFFFFF'," +
                    "'showHoverEffect': '1'," +
                        "'plotgradientcolor': ''," +
                        "'plotBorderDashed':'0'," +

        "'showalternatehgridcolor': '0'," +
        "'showplotborder': '1'," +
        "'divlinecolor': 'CCCCCC'," +
        "'showvalues': '1'," +
        "'xaxisname': 'Product'," +
        "'pyaxisname': 'Yield & Competitor Qty'," +
        "'syaxisname': 'Contribution(%)'," +
           "'palettecolors': '#EDCBD2,#80C4B7,#d62965,#b256d1,#cfc80e,#B8860B,#BC8F8F,#C48E48,#CC00FF,#CCFFCC,#CD7054,#D0FAEE,#E7C6A5,#EE6A50'," +
           "'placeValuesInside': '0',"+
        "'slantlabels': '0'," +
        "'canvasborderalpha': '0'," +
        "'legendshadow': '1'," +
        "'legendborderalpha': '0'," +
              "'labelDisplay': 'rotate'," +


                     "'sYAxisMaxValue' : '30'," +
                     "'sYAxisMinValue' : '0'," +
                            //"'exportEnabled': '1'," +
                            // "'exportAtClient': '1'," +
                            // "'exportHandler': 'http://export.api3.fusioncharts.com'," +
                            // "'html5ExportHandler': 'http://export.api3.fusioncharts.com'," +
                            //  "'exportFileName' : 'DashBoard'," +
                            "'formatNumber': '0'," +
                        "'formatNumberScale': '0'," +
                        "'useRoundEdges': '1'," +
                            "'placeValuesInside': '0'," +
                             "'rotateValues':'1', " +
        // "'showCanvasBorder':'1',  " +
        //  "'canvasBorderThickness':'2'," +
        "'showborder': '0' " +


                "},");

            Product.Append("'categories': [" +
                "{" +
                    "'category': [");

            Yield.Append("{" +
                        // dataset level attributes

                        "'seriesName': 'Yield'," +
                        "'data': [");

            Comp.Append("{" +
                         // dataset level attributes
                         "'seriesName': 'Competitor'," +
                         "'data': [");

            CompPer.Append("{" +
                        // dataset level attributes
                        "'seriesName': 'Percentage(%)'," +
                           "'renderAs': 'line'," +
                        "'anchorRadius': '4', " +
                           "'showalternatehgridcolor': '0', " +
        "'divlinecolor': 'CCCCCC'," +
           "'showvalues': '0'," +

        "'showcanvasborder': '0'," +
        "'canvasborderalpha': '0'," +
        "'canvasbordercolor': 'CCCCCC'," +
        "'canvasborderthickness': '1'," +
        "'yaxismaxvalue': '30000'," +
        "'captionpadding': '30'," +
        "'linethickness': '3'," +
        "'yaxisvaluespadding': '15'," +
        "'legendshadow': '0'," +
        "'legendborderalpha': '0'," +
        "'palettecolors': '#f8bd19,#008ee4,#33bdda,#e44a00,#6baa01,#583e78',  " +
        "'showborder': '0'," +
         "'stepSkipped': 'false', " +
                    "'appliedSmartLabel': 'true'," +

                            "'parentYAxis': 'S',   " +


                        "'data': [");




            for (int i = 0; i < arrData.GetLength(0); i++)
            {
                if (i > 0)
                {
                    Product.Append(",");
                    Yield.Append(",");
                    Comp.Append(",");
                    CompPer.Append(",");


                }


                Product.AppendFormat("{{" +
                        // category level attributes
                        "'label': '{0}'" +
                    "}}", arrData[i, 0]);

                Yield.AppendFormat("{{" +
                        // data level attributes
                        "'value': '{0}'" +
                    "}}", arrData[i, 1]);

                Comp.AppendFormat("{{" +
                      // data level attributes
                      "'value': '{0}'" +
                  "}}", arrData[i, 2]);

                CompPer.AppendFormat("{{" +
                          // data level attributes


                          "'value': '{0}'" +
                      "}}", arrData[i, 3]);



            }

            Product.Append("]" +
                    "}" +
                "],");

            Yield.Append("]" +
                    "},");

            Comp.Append("]" +
                   "},");
            CompPer.Append("]" +
                    "},");

            jsonData.Append(Product.ToString());
            jsonData.Append("'dataset': [");
            jsonData.Append(Yield.ToString());
            jsonData.Append(Comp.ToString());
            jsonData.Append(CompPer.ToString());

            jsonData.Append("]" +
                    "}");

            return jsonData.ToString();
        }
    }
    [WebMethod]
    public static string RCPA_Doctorwise(string objData)
    {
        // string div_code = HttpContext.Current.Session["div_code"].ToString();
        string msf_code = string.Empty;

        string[] arr = objData.Split('^');
        smonth = arr[0];
        syear = arr[1];
        sprodcode = arr[2];
        msf_code = arr[3];
        string divcode = arr[4];



        using (SqlConnection con = new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["Ereportcon"].ConnectionString))
        {
            DataTable dt = new DataTable();
            // dt.Columns.Add("Label");
            // dt.Columns.Add("Value");
            string sProc_Name = "";

            sProc_Name = "RCPA_Analysis_Doctorwise_Graph";

            SqlCommand cmd = new SqlCommand(sProc_Name, con);


            cmd.CommandTimeout = 600;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Div_Code", divcode);
            cmd.Parameters.AddWithValue("@Msf_code", msf_code);
            cmd.Parameters.AddWithValue("@cMnth", smonth);
            cmd.Parameters.AddWithValue("@cYr", syear);
            cmd.Parameters.AddWithValue("@map_code", sprodcode);
            con.Open();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.SelectCommand = cmd;
            // DataTable dt = new DataTable();
            da.Fill(dt);
            dt.Columns.RemoveAt(0);

            con.Close();

            int rowCount = dt.Rows.Count;
            int columnCount = dt.Columns.Count;


            object[,] arrData = new object[rowCount, columnCount];


            for (int i = 0; i < rowCount; i++)
            {
                for (int j = 0; j < columnCount; j++)
                {
                    arrData[i, j] = dt.Rows[i][j];

                    if (string.IsNullOrEmpty(dt.Rows[i][j].ToString()))
                    {
                        // Write your Custom Code
                        dt.Rows[i][j] = 0;
                        arrData[i, j] = dt.Rows[i][j];
                    }

                }
            }

            StringBuilder jsonData = new StringBuilder();
            StringBuilder Product = new StringBuilder();
            StringBuilder Yield = new StringBuilder();
            StringBuilder Comp = new StringBuilder();
            StringBuilder CompPer = new StringBuilder();

            jsonData.Append("{" +
                //Initialize the chart object with the chart-level attributes..
                "'chart': {" +
                    "'caption': ''," +
                    "'bgcolor': 'FFFFFF'," +
                    "'showHoverEffect': '1'," +
                        "'plotgradientcolor': ''," +
                        "'plotBorderDashed':'0'," +

        "'showalternatehgridcolor': '0'," +
        "'showplotborder': '1'," +
        "'divlinecolor': 'CCCCCC'," +
        "'showvalues': '1'," +

        "'xaxisname': 'Product'," +
        "'pyaxisname': 'Doctor Potential & Yield Qty'," +
        "'syaxisname': 'Contribution(%)'," +
           "'palettecolors': '#EDCBD2,#80C4B7,#d62965,#b256d1,#cfc80e,#B8860B,#BC8F8F,#C48E48,#CC00FF,#CCFFCC,#CD7054,#D0FAEE,#E7C6A5,#EE6A50'," +

        "'slantlabels': '0'," +
        "'canvasborderalpha': '0'," +
        "'legendshadow': '1'," +
        "'legendborderalpha': '0'," +
              "'labelDisplay': 'rotate'," +


                     "'sYAxisMaxValue' : '30'," +
                     "'sYAxisMinValue' : '0'," +
                            //"'exportEnabled': '1'," +
                            // "'exportAtClient': '1'," +
                            // "'exportHandler': 'http://export.api3.fusioncharts.com'," +
                            // "'html5ExportHandler': 'http://export.api3.fusioncharts.com'," +
                            //  "'exportFileName' : 'DashBoard'," +
                            "'formatNumber': '0'," +
                        "'formatNumberScale': '0'," +
                        "'useRoundEdges': '1'," +
                             "'placeValuesInside': '0'," +
                             "'rotateValues':'1', " +
        // "'showCanvasBorder':'1',  " +
        //  "'canvasBorderThickness':'2'," +
        "'showborder': '0' " +


                "},");

            Product.Append("'categories': [" +
                "{" +
                    "'category': [");

            Yield.Append("{" +
                        // dataset level attributes

                        "'seriesName': 'Our Yield'," +
                        "'data': [");

            Comp.Append("{" +
                         // dataset level attributes
                         "'seriesName': 'Doctor Potential'," +
                         "'data': [");

            CompPer.Append("{" +
                        // dataset level attributes
                        "'seriesName': 'Doctor Contribution(%)'," +
                           "'renderAs': 'line'," +
                        "'anchorRadius': '4', " +
                           "'showalternatehgridcolor': '0', " +
        "'divlinecolor': 'CCCCCC'," +
           "'showvalues': '0'," +

        "'showcanvasborder': '0'," +
        "'canvasborderalpha': '0'," +
        "'canvasbordercolor': 'CCCCCC'," +
        "'canvasborderthickness': '1'," +
        "'yaxismaxvalue': '30000'," +
        "'captionpadding': '30'," +
        "'linethickness': '3'," +
        "'yaxisvaluespadding': '15'," +
        "'legendshadow': '0'," +
        "'legendborderalpha': '0'," +
        "'palettecolors': '#f8bd19,#008ee4,#33bdda,#e44a00,#6baa01,#583e78',  " +
        "'showborder': '0'," +
         "'stepSkipped': 'false', " +
                    "'appliedSmartLabel': 'true'," +

                            "'parentYAxis': 'S',   " +


                        "'data': [");




            for (int i = 0; i < arrData.GetLength(0); i++)
            {
                if (i > 0)
                {
                    Product.Append(",");
                    Yield.Append(",");
                    Comp.Append(",");
                    CompPer.Append(",");


                }


                Product.AppendFormat("{{" +
                        // category level attributes
                        "'label': '{0}'" +
                    "}}", arrData[i, 0]);

                Yield.AppendFormat("{{" +
                        // data level attributes
                        "'value': '{0}'" +
                    "}}", arrData[i, 1]);

                Comp.AppendFormat("{{" +
                      // data level attributes
                      "'value': '{0}'" +
                  "}}", arrData[i, 2]);

                CompPer.AppendFormat("{{" +
                          // data level attributes


                          "'value': '{0}'" +
                      "}}", arrData[i, 3]);



            }

            Product.Append("]" +
                    "}" +
                "],");

            Yield.Append("]" +
                    "},");

            Comp.Append("]" +
                   "},");
            CompPer.Append("]" +
                    "},");

            jsonData.Append(Product.ToString());
            jsonData.Append("'dataset': [");
            jsonData.Append(Yield.ToString());
            jsonData.Append(Comp.ToString());
            jsonData.Append(CompPer.ToString());

            jsonData.Append("]" +
                    "}");

            return jsonData.ToString();
        }
    }
    [WebMethod]
    public static List<MyItem> GetDropdownProd(string objData)
    {
     
      
       // string divcode = HttpContext.Current.Request.QueryString["div_Code"];

        string[] arr = objData.Split('^');
        string smonth = arr[0];
        string syear = arr[1];
        string sprodcode = arr[2];
        string msf_code = arr[3];
        string divcode = arr[4];

        List<MyItem> items = new List<MyItem>();

        string strConn = System.Configuration.ConfigurationManager.ConnectionStrings["Ereportcon"].ConnectionString;

        using (SqlConnection con = new SqlConnection(strConn))
        {
            SqlCommand cmd = new SqlCommand("RCPA_Analysis_PD", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Div_Code", divcode);
            cmd.Parameters.AddWithValue("@Msf_code", msf_code);
            cmd.Parameters.AddWithValue("@cMnth", smonth);
            cmd.Parameters.AddWithValue("@cYr", syear);
            cmd.Parameters.AddWithValue("@map_code", sprodcode);
            cmd.CommandTimeout = 800;

            con.Open();
            SqlDataReader rdr = cmd.ExecuteReader();

            while (rdr.Read())
            {
                string name = rdr["Product"].ToString();
                string code = rdr["Code"].ToString();

                if (!name.Equals("Total", StringComparison.OrdinalIgnoreCase))
                {
                    items.Add(new MyItem { Name = name, Code = code });
                }
            }
        }

        return items; // ASP.NET will auto-serialize to JSON
    }

    public class MyItem
    {
        public string Name { get; set; }
        public string Code { get; set; }
    }
    [WebMethod(EnableSession = true)]

    public static string Competitor(string objData)
    {
       
        //string divcode = HttpContext.Current.Request.QueryString["div_Code"];
        string[] arr = objData.Split('^');
        smonth = arr[0];
        syear = arr[1];
        sprodcode = arr[2];
        string msf_code = arr[3];
        string divcode = arr[4];

        using (SqlConnection con = new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["Ereportcon"].ConnectionString))
        {
            DataTable dt = new DataTable();
            // dt.Columns.Add("Label");
            // dt.Columns.Add("Value");
            string sProc_Name = "";
            sProc_Name = "RCPA_Analysis_Competitor_Graph";



            SqlCommand cmd = new SqlCommand(sProc_Name, con);


            cmd.CommandTimeout = 600;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Div_Code", divcode);
            cmd.Parameters.AddWithValue("@Msf_code", msf_code);
            cmd.Parameters.AddWithValue("@cMnth", smonth);
            cmd.Parameters.AddWithValue("@cYr", syear);
            cmd.Parameters.AddWithValue("@our_code", sprodcode);
            con.Open();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.SelectCommand = cmd;
            // DataTable dt = new DataTable();
            da.Fill(dt);

            dt.Columns.RemoveAt(0);

            con.Close();
            dt.Columns["Product"].ColumnName = "Label";
            dt.AcceptChanges();
            dt.Columns["Qty"].ColumnName = "Value";

            dt.AcceptChanges();

            string jsonResult = JsonConvert.SerializeObject(dt);

            return jsonResult;

        }
    }
    #region btnback_Click
    protected void btnback_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/MasterFiles/Dashboard_Menu.aspx?sfcode=" + sf_code + "&cMnth=" + DateTime.Now.Month.ToString().Trim() + "&cYr=" + DateTime.Now.Year.ToString().Trim() + "&div_code=" + div_code + "&sf_type=" + sf_type + "");
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        lstProd.SelectedIndex = -1;
        rdorcpa.SelectedIndex = 0;
        lstProd.Disabled = false;
        ddlMonth.Enabled = true;
        ddlYear.Enabled = true;
        rdorcpa.Enabled = true;
        main.Visible = false;
        divdoctor.Visible = false;
        divtrend.Visible = false;
    }
    #endregion

    private void FillReportMonth()
    {

    }


}