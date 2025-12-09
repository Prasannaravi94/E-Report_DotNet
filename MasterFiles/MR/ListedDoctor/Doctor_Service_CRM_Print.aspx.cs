using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Data;
using Bus_EReport;
using System.Configuration;
using System.Data.SqlClient;
using System.Text;
using System.Xml;
using System.IO;
using System.Web.Services;
using System.Web.Script.Services;
using System.Xml.Linq;
using System.Collections;
using System.Drawing.Text;

public partial class MasterFiles_MR_ListedDoctor_Doctor_Service_CRM_Print_1 : System.Web.UI.Page
{
    #region Variables
    string sMode = string.Empty;
    string sf_code = string.Empty;
    string FMonth = string.Empty;
    string FYear = string.Empty;
    string TMonth = string.Empty;
    string TYear = string.Empty;
    string div_code = string.Empty;
    string sReturn = string.Empty;
    string sDay = string.Empty;
    string sCnt = string.Empty;
    string vMode = string.Empty;
    string strmode = string.Empty;
    string strFieledForceName = string.Empty;
    string sCurrentDate = string.Empty;
    DataSet dsFF = new DataSet();
    DataSet dsdoc = new DataSet();
    DataTable dtrowClr = new System.Data.DataTable();
    DataTable dtrowdt = new System.Data.DataTable();
    SalesForce dcrdoc = new SalesForce();
    DataSet dsDoctor = new DataSet();
    string sHeading; int sReportType;
    string strCase = string.Empty;
    List<int> iLstVstmnt = new List<int>();
    List<int> iLstVstyr = new List<int>();
    List<DataTable> result = new List<System.Data.DataTable>();
    #endregion
    DataSet dsmgrsf = new DataSet();
    DataTable dtsf_code = new DataTable();
    DataSet dsts = new DataSet();

    string S_Code = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        string div_code = Session["div_code"].ToString();
        string sfCode = Session["sf_code"].ToString();

        if (Request.QueryString["S_Code"] != null && Request.QueryString["S_Code"] != "")
        {
            Session["sf_code"] = Request.QueryString["S_Code"];
        }

        S_Code = Session["SF_Code_N"].ToString();

        Session["S_Code"] = S_Code;

        if (Request.QueryString["ListedDrCode"] != null && Request.QueryString["ListedDrCode"] != "")
        {
            Session["doctorcode"] = Request.QueryString["ListedDrCode"];
        }

        div_code = Session["div_code"].ToString();

        if (!Page.IsPostBack)
        {
            FillReport();
        }
    }

    private void FillReport()
    {

        SalesForce sf1 = new SalesForce();
        int iMn = 0, iYr = 0;

        DataTable dtMnYr = new DataTable();
        dtMnYr.Columns.Add("INX", typeof(int)).AutoIncrement = true;
        dtMnYr.Columns["INX"].AutoIncrementSeed = 1;
        dtMnYr.Columns["INX"].AutoIncrementStep = 1;
        dtMnYr.Columns.Add("MNTH", typeof(int));

        int MonthCnt = 0;

        string currentMonth = DateTime.Now.Month.ToString();

        int Month = Convert.ToInt32(currentMonth);

        while (MonthCnt < 3)
        {
            iMn = Month;
            dtMnYr.Rows.Add(null, iMn);
            // months--; cmonth++;
            MonthCnt += 1;
            Month = Convert.ToInt32(Month) - 1;
        }

        string sf_code = Session["sf_code"].ToString();



        //  string doctorcode = Request.QueryString["ListedDrCode"];

        string doctorcode = Session["doctorcode"].ToString();
        string div_code = Session["div_code"].ToString();

        int j = 0;
        DataTable SfCodes = sf1.GetCRM_MR_Detail(div_code, sf_code, 0);
        DataTable dtsf_code = new DataTable();
        dtsf_code.Columns.Add("INX", typeof(int)).AutoIncrement = true;
        dtsf_code.Columns["INX"].AutoIncrementSeed = 1;
        dtsf_code.Columns["INX"].AutoIncrementStep = 1;
        dtsf_code.Columns.Add("sf_code");
        for (int i = 0; i < SfCodes.Rows.Count; i++)
        {
            //j += 1;
            //dtsf_code.Rows.Add(j.ToString());

            dtsf_code.Rows.Add(null, SfCodes.Rows[i]["sf_code"]);
        }

        // DataSet dsmgrsf = new DataSet();
        dsmgrsf.Tables.Add(SfCodes);   //Listeddr_Period_MGR_Proc

        SalesForce sf = new SalesForce();
        DCR dcc = new DCR();
        // DB_EReporting db = new DB_EReporting();
        string strConn = System.Configuration.ConfigurationManager.ConnectionStrings["Ereportcon"].ToString();
        SqlConnection con = new SqlConnection(strConn);
        string sProc_Name = "";
        sProc_Name = "Listeddr_Period_MGR_Proc";

        SqlCommand cmd = new SqlCommand(sProc_Name, con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Div_Code", Convert.ToInt32(div_code));
        cmd.Parameters.AddWithValue("@Msf_code", sf_code);
        cmd.Parameters.AddWithValue("@Mnth_tbl", dtMnYr);
        cmd.Parameters.AddWithValue("@Mgr_Codes", dtsf_code);
        cmd.Parameters.AddWithValue("@ListDrCode", doctorcode);
        cmd.CommandTimeout = 800;
        SqlDataAdapter da = new SqlDataAdapter(cmd);

        da.Fill(dsts);
        dtrowClr = dsts.Tables[0].Copy();
        dtrowdt = dsts.Tables[0].Copy();

        GenerateTable();
    }

    private void GenerateTable()
    {
        TableRow tr_mth_header = new TableRow();
        TableRow tr_sub_header = new TableRow();

        string currentMonth = DateTime.Now.Month.ToString();

        int Month = Convert.ToInt32(currentMonth);
        int iColSpan = 0;

        for (int w = 0; w < dsmgrsf.Tables[0].Rows.Count; w++)
        {
            TableCell tc_sec_name = new TableCell();
            tc_sec_name.BorderStyle = BorderStyle.Solid;
            tc_sec_name.BorderWidth = 1;
            tc_sec_name.Width = 100;
            tc_sec_name.ColumnSpan = 3;
            Literal lit_sec_name = new Literal();
            string ColName = dsmgrsf.Tables[0].Rows[w]["sf_Designation_Short_Name"].ToString() + "(" + dsmgrsf.Tables[0].Rows[w]["sf_Name"].ToString() + ")";
            lit_sec_name.Text = ColName;
            tc_sec_name.Attributes.Add("Class", "Backcolor");
            tc_sec_name.Controls.Add(lit_sec_name);
            //tr_header.Cells.Add(tc_sec_name);
            tr_mth_header.Cells.Add(tc_sec_name);

            // tc_sec_name.BackColor = System.Drawing.ColorTranslator.FromHtml("#1f9bad");

            tc_sec_name.BackColor = System.Drawing.ColorTranslator.FromHtml("#fff");
            tc_sec_name.ForeColor = System.Drawing.Color.Black;
            tc_sec_name.Style.Add("font-weight", "bold");

            tr_sub_header.BackColor = System.Drawing.Color.White;
            tr_sub_header.Attributes.Add("Class", "rptCellBorder");

            //  tr_sub_header.BackColor = System.Drawing.ColorTranslator.FromHtml("#1f9bad");
            tr_sub_header.BackColor = System.Drawing.ColorTranslator.FromHtml("#fff");
            tr_sub_header.ForeColor = System.Drawing.Color.Black;
            tr_sub_header.Style.Add("font-weight", "bold");
            tr_sub_header.Style.Add("font-size", "11px");
            // tr_sub_header. = System.Drawing.Color.Black;

            int MonthCnt = 0;
            Month = Convert.ToInt32(currentMonth);

            while (MonthCnt < 3)
            {
                TableCell objtablecell3 = new TableCell();

                SalesForce sf = new SalesForce();
                string MonthName = sf.getMonthName(Month.ToString()).Substring(0, 3);

                TableCell tc_qty = new TableCell();
                tc_qty.BorderStyle = BorderStyle.Solid;
                tc_qty.BorderWidth = 1;
                tc_qty.Width = 50;
                Literal lit_qty = new Literal();
                lit_qty.Text = MonthName;
                tc_qty.Attributes.Add("Class", "Backcolor");
                tc_qty.Controls.Add(lit_qty);
                tr_sub_header.Cells.Add(tc_qty);

                // AddMergedCells(objgridviewrow3, objtablecell3, 0, MonthName, "#0097AC", false);

                MonthCnt += 1;
                Month = Convert.ToInt32(Month) - 1;
            }

        }

        tbl.Rows.Add(tr_mth_header);
        tbl.Rows.Add(tr_sub_header);

        List<string> objData = new List<string>();

        //List<string> objSfData = new List<string>();

        //for (int w = 0; w < dsmgrsf.Tables[0].Rows.Count; w++)
        //{
        //    string Sf_Code = dsmgrsf.Tables[0].Rows[w]["sf_code"].ToString();

        //    if (dsts.Tables[0].Rows.Contains(Sf_Code))
        //    {

        //    }

        //}

        foreach (DataRow dr in dsts.Tables[0].Rows)
        {
            string Data = dr["Sf_Code"].ToString() + "_" + dr["Month"].ToString() + "_" + dr["dt"].ToString();
            objData.Add(Data);
        }

        TableRow tr_det = new TableRow();
        tr_det.BackColor = System.Drawing.Color.White;
        tr_det.Attributes.Add("Class", "rptCellBorder");

        if (objData.Count > 0)
        {

            if (dsmgrsf.Tables[0].Rows.Count > 0)
            {

                for (int w = 0; w < dsmgrsf.Tables[0].Rows.Count; w++)
                {

                    string Sf_Code = dsmgrsf.Tables[0].Rows[w]["sf_code"].ToString();

                    int MonthCnt = 0;
                    Month = Convert.ToInt32(currentMonth);

                    // for (int i = 0; i < objData.Count; i++)
                    // {
                    while (MonthCnt < 3)
                    {
                        SalesForce sf = new SalesForce();
                        string MonthName = sf.getMonthName(Month.ToString()).Substring(0, 3);

                        TableCell tc_det_qty = new TableCell();
                        tc_det_qty.BorderStyle = BorderStyle.Solid;
                        tc_det_qty.BorderWidth = 1;
                        // tc_det_qty.HorizontalAlign = HorizontalAlign.Center;
                        tc_det_qty.Width = 50;
                        Literal lit_det_qty = new Literal();
                        lit_det_qty.Text = "";


                        for (int i = 0; i < objData.Count; i++)
                        {
                            if (objData[i].Contains(Sf_Code) && objData[i].Contains(MonthName))
                            {
                                string str = objData[i].ToString();
                                string[] ArrData = str.Split('_');

                                if (ArrData[1].Contains(MonthName))
                                {
                                    lit_det_qty.Text = ArrData[2];
                                }
                            }
                        }

                        tc_det_qty.Attributes.Add("Class", "Backcolor");
                        tc_det_qty.Controls.Add(lit_det_qty);
                        tr_det.Cells.Add(tc_det_qty);

                        MonthCnt += 1;
                        Month = Convert.ToInt32(Month) - 1;
                    }

                    //  }

                    tbl.Rows.Add(tr_det);

                }
            }
        }
        else
        {

            for (int w = 0; w < dsmgrsf.Tables[0].Rows.Count; w++)
            {

                int MonthCnt = 0;
                Month = Convert.ToInt32(currentMonth);


                while (MonthCnt < 3)
                {
                    TableCell tc_det_qty = new TableCell();
                    tc_det_qty.BorderStyle = BorderStyle.Solid;
                    tc_det_qty.BorderWidth = 1;
                    // tc_det_qty.HorizontalAlign = HorizontalAlign.Center;
                    tc_det_qty.Width = 50;
                    Literal lit_det_qty = new Literal();
                    if (MonthCnt == 0 && w == 0)
                    {
                        lit_det_qty.Text = "No Records Found";
                        tc_det_qty.ForeColor = System.Drawing.Color.Red;
                    }
                    else
                    {
                        lit_det_qty.Text = "";
                    }
                    tc_det_qty.Attributes.Add("Class", "Backcolor");
                    tc_det_qty.Controls.Add(lit_det_qty);
                    tr_det.Cells.Add(tc_det_qty);

                    MonthCnt += 1;
                }

                tbl.Rows.Add(tr_det);
            }
        }

    }

    [WebMethod(EnableSession = true)]
    [ScriptMethod]
    public static List<DrServiceDetail_Print> BindDoctorService(DrServiceDetail_Print objDrDetail)
    {
        string div_code = HttpContext.Current.Session["div_code"].ToString();
        string sf_code = HttpContext.Current.Session["sf_code"].ToString();
        string Sf_Type = HttpContext.Current.Session["sf_type"].ToString();

        List<DrServiceDetail_Print> objDrService = new List<DrServiceDetail_Print>();

        DrServiceDetail_Print objData = new DrServiceDetail_Print();
        objData.Sl_No = objDrDetail.Sl_No;
        objData.DoctorCode = objDrDetail.DoctorCode;

        SecSale ss = new SecSale();
        DataSet dsDr = ss.GetDr_ServiceCRM_Print(objData.Sl_No,div_code,objData.DoctorCode);

        foreach (DataRow dr in dsDr.Tables[0].Rows)
        {
            DrServiceDetail_Print objDr = new DrServiceDetail_Print();
            objDr.Sl_No = dr["Sl_No"].ToString();            
            objDr.DoctorCode = dr["ListedDrCode"].ToString();
            objDr.DoctorName = dr["ListedDr_Name"].ToString();
            objDr.Address = dr["ListedDr_Address1"].ToString();
            objDr.Category = dr["Doc_Cat_Name"].ToString();
            objDr.Speciality = dr["Doc_Special_Name"].ToString();
            objDr.Qualification = dr["Doc_QuaName"].ToString();
            objDr.Class = dr["Doc_ClsName"].ToString();
            objDr.Mobile = dr["ListedDr_Mobile"].ToString();
            objDr.Email = dr["ListedDr_Email"].ToString();
            objDr.TotalBusReturn_Amt = dr["Total_Business_Expect"].ToString();
            objDr.ServiceAmt_tillDate = dr["Sevice_Amt_till_Date"].ToString();
            objDr.ROI_Dur_Month = dr["ROI_Month"].ToString();
            objDr.Service_Req = dr["Service_Req"].ToString();
            objDr.Service_Amt = dr["Service_Amt"].ToString();
            objDr.Specific_Act = dr["Specific_Remark"].ToString();

            objDr.ddlChemist_1 = dr["Prescr_Chemist_1"].ToString();
            objDr.ddlChemist_2 = dr["Prescr_Chemist_2"].ToString();
            objDr.ddlChemist_3 = dr["Prescr_Chemist_3"].ToString();
            objDr.ddlStockist_1 = dr["Stockist_1"].ToString();
            objDr.ddlStockist_2 = dr["Stockist_2"].ToString();
            objDr.ddlStockist_3 = dr["Stockist_3"].ToString();

            objDr.Prd_Sl_No = dr["Prd_Sl_No"].ToString();
            objDr.Cur_Prod_Code_1 = dr["Cur_Prod_Code_1"].ToString();
            objDr.Cur_Prod_Price_1 = dr["Cur_Prod_Price_1"].ToString();
            objDr.Cur_Prod_Qty_1 = dr["Cur_Prod_Qty_1"].ToString();
            objDr.Cur_Prod_Value_1 = dr["Cur_Prod_Value_1"].ToString();
            objDr.Cur_Prod_Code_2 = dr["Cur_Prod_Code_2"].ToString();
            objDr.Cur_Prod_Price_2 = dr["Cur_Prod_Price_2"].ToString();
            objDr.Cur_Prod_Qty_2 = dr["Cur_Prod_Qty_2"].ToString();
            objDr.Cur_Prod_Value_2 = dr["Cur_Prod_Value_2"].ToString();
            objDr.Cur_Prod_Code_3 = dr["Cur_Prod_Code_3"].ToString();
            objDr.Cur_Prod_Price_3 = dr["Cur_Prod_Price_3"].ToString();
            objDr.Cur_Prod_Qty_3 = dr["Cur_Prod_Qty_3"].ToString();
            objDr.Cur_Prod_Value_3 = dr["Cur_Prod_Value_3"].ToString();
            objDr.Cur_Prod_Code_4 = dr["Cur_Prod_Code_4"].ToString();
            objDr.Cur_Prod_Price_4 = dr["Cur_Prod_Price_4"].ToString();
            objDr.Cur_Prod_Qty_4 = dr["Cur_Prod_Qty_4"].ToString();
            objDr.Cur_Prod_Value_4 = dr["Cur_Prod_Value_4"].ToString();
            objDr.Cur_Prod_Code_5 = dr["Cur_Prod_Code_5"].ToString();
            objDr.Cur_Prod_Price_5 = dr["Cur_Prod_Price_5"].ToString();
            objDr.Cur_Prod_Qty_5 = dr["Cur_Prod_Qty_5"].ToString();
            objDr.Cur_Prod_Value_5 = dr["Cur_Prod_Value_5"].ToString();
            objDr.Cur_Prod_Code_6 = dr["Cur_Prod_Code_6"].ToString();
            objDr.Cur_Prod_Price_6 = dr["Cur_Prod_Price_6"].ToString();
            objDr.Cur_Prod_Qty_6 = dr["Cur_Prod_Qty_6"].ToString();
            objDr.Cur_Prod_Value_6 = dr["Cur_Prod_Value_6"].ToString();
            objDr.Cur_Total = dr["Cur_Total"].ToString();

            objDr.Potl_Prod_Code_1 = dr["Potl_Prod_Code_1"].ToString();
            objDr.Potl_Prod_Price_1 = dr["Potl_Prod_Price_1"].ToString();
            objDr.Potl_Prod_Qty_1 = dr["Potl_Prod_Qty_1"].ToString();
            objDr.Potl_Prod_Value_1 = dr["Potl_Prod_Value_1"].ToString();
            objDr.Potl_Prod_Code_2 = dr["Potl_Prod_Code_2"].ToString();
            objDr.Potl_Prod_Price_2 = dr["Potl_Prod_Price_2"].ToString();
            objDr.Potl_Prod_Qty_2 = dr["Potl_Prod_Qty_2"].ToString();
            objDr.Potl_Prod_Value_2 = dr["Potl_Prod_Value_2"].ToString();
            objDr.Potl_Prod_Code_3 = dr["Potl_Prod_Code_3"].ToString();
            objDr.Potl_Prod_Price_3 = dr["Potl_Prod_Price_3"].ToString();
            objDr.Potl_Prod_Qty_3 = dr["Potl_Prod_Qty_3"].ToString();
            objDr.Potl_Prod_Value_3 = dr["Potl_Prod_Value_3"].ToString();
            objDr.Potl_Prod_Code_4 = dr["Potl_Prod_Code_4"].ToString();
            objDr.Potl_Prod_Price_4 = dr["Potl_Prod_Price_4"].ToString();
            objDr.Potl_Prod_Qty_4 = dr["Potl_Prod_Qty_4"].ToString();
            objDr.Potl_Prod_Value_4 = dr["Potl_Prod_Value_4"].ToString();
            objDr.Potl_Prod_Code_5 = dr["Potl_Prod_Code_5"].ToString();
            objDr.Potl_Prod_Price_5 = dr["Potl_Prod_Price_5"].ToString();
            objDr.Potl_Prod_Qty_5 = dr["Potl_Prod_Qty_5"].ToString();
            objDr.Potl_Prod_Value_5 = dr["Potl_Prod_Value_5"].ToString();
            objDr.Potl_Prod_Code_6 = dr["Potl_Prod_Code_6"].ToString();
            objDr.Potl_Prod_Price_6 = dr["Potl_Prod_Price_6"].ToString();
            objDr.Potl_Prod_Qty_6 = dr["Potl_Prod_Qty_6"].ToString();
            objDr.Potl_Prod_Value_6 = dr["Potl_Prod_Value_6"].ToString();
            objDr.Potl_Total = dr["Potential_Total"].ToString();
            //objDr.Service_Sl_No = dr["Service_Sl_No"].ToString();

            objDr.First_Lev_Name = dr["First_Lev_Name"].ToString();
            objDr.Second_Lev_Name = dr["Second_Lev_Name"].ToString();
            objDr.Third_Lev_Name = dr["Third_Lev_Name"].ToString();
            objDr.Four_Lev_Name = dr["Four_Lev_Name"].ToString();
            objDr.Fivth_Lev_Name = dr["Fivth_Lev_Name"].ToString();
            objDr.Six_Lev_Name = dr["Six_Lev_Name"].ToString();
            objDr.Seven_Lev_Name = dr["Seven_Lev_Name"].ToString();
            objDr.Applied_Date = dr["Applied_Date"].ToString();
            objDr.Approved_Date = dr["Approved_Date"].ToString();
            objDr.Confirmed_Date = dr["Confirmed_Date"].ToString();
            objDr.Applied_By = dr["Applied_By"].ToString();
            objDr.Approved_By = dr["Approved_By"].ToString();
            objDr.Confirm_By = dr["Confirm_By"].ToString();

            objDr.Sf_HQ = dr["Sf_HQ"].ToString();
            objDr.Sf_Desig = dr["Sf_Desig"].ToString();
            objDr.Division_Name = dr["Division_Name"].ToString();
            objDr.Sf_Type = Sf_Type;
            objDr.Status = dr["Ser_Type"].ToString();

            objDrService.Add(objDr);

        }

        return objDrService;
    }

    [WebMethod(EnableSession = true)]
    [ScriptMethod]
    public static List<ProductDetail_Print> GetProductDetail()
    {

        string div_code = HttpContext.Current.Session["div_code"].ToString();
        string sf_code = HttpContext.Current.Session["sf_code"].ToString();

        string state_code = "";
        string Prod_Grp = "";
        string sub_code = "";
        string Prd_grp = "";

        ProductDetail_Print objProd = new ProductDetail_Print();

        SecSale ss = new SecSale();

        UnListedDR LstDR = new UnListedDR();
        DataSet dsState = LstDR.getState(sf_code);
        if (dsState.Tables[0].Rows.Count > 0)
            state_code = dsState.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();

        //DataSet dsRate = ss.getAddionalRptSaleMaster(div_code);
        //if (dsRate != null)
        //{
        //    if (dsRate.Tables[0].Rows.Count > 0)

        //    objProd.Calc_Rate = dsRate.Tables[0].Rows[0].ItemArray.GetValue(2).ToString().Trim();

        //    Prod_Grp = dsRate.Tables[0].Rows[0].ItemArray.GetValue(3).ToString().Trim();

        //}

        objProd.Calc_Rate = "R";
        Prod_Grp = "0";

        SubDivision sb = new SubDivision();
        DataSet dsSub = sb.getSub_sf(sf_code);
        if (dsSub.Tables[0].Rows.Count > 0)
        {
            sub_code = dsSub.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
        }

        DataSet dsProd = ss.Get_MRwise_ProductDetail(div_code, state_code, Prod_Grp, sub_code);

        List<ProductDetail_Print> objProdDel = new List<ProductDetail_Print>();

        foreach (DataRow drow in dsProd.Tables[0].Rows)
        {
            ProductDetail_Print objPrd = new ProductDetail_Print();
            objPrd.Product_Detail_Code = drow["Product_Detail_Code"].ToString();
            objPrd.Product_Detail_Name = drow["Product_Detail_Name"].ToString();
            objProd.Calc_Rate = "R";
            if (objProd.Calc_Rate == "R")
            {
                objPrd.Rate = drow["Retailor_Price"].ToString();
            }
            else if (objProd.Calc_Rate == "M")
            {
                objPrd.Rate = drow["MRP_Price"].ToString();
            }
            else if (objProd.Calc_Rate == "D")
            {
                objPrd.Rate = drow["Distributor_Price"].ToString();
            }
            else if (objProd.Calc_Rate == "N")
            {
                objPrd.Rate = drow["NSR_Price"].ToString();
            }
            else if (objProd.Calc_Rate == "T")
            {
                objPrd.Rate = drow["Target_Price"].ToString();
            }

            objProdDel.Add(objPrd);
        }

        return objProdDel;
    }

    [WebMethod(EnableSession = true)]
    [ScriptMethod]
    public static List<Chemist_Pr> GetChemist(string DoctorCode)
    {

        string div_code = HttpContext.Current.Session["div_code"].ToString();
        string sf_code = HttpContext.Current.Session["sf_code"].ToString();

        ListedDR ch = new ListedDR();
        DataSet dsCh = ch.BingChemistDDL(DoctorCode, sf_code);

        List<Chemist_Pr> objChData = new List<Chemist_Pr>();

        foreach (DataRow dr in dsCh.Tables[0].Rows)
        {
            Chemist_Pr objch = new Chemist_Pr();
            objch.Chemists_Code = dr["Chemists_Code"].ToString();
            objch.Chemists_Name = dr["chemists_name"].ToString();
            objChData.Add(objch);
        }

        return objChData;
    }

    [WebMethod(EnableSession = true)]
    [ScriptMethod]
    public static List<Stockist_Pr> GetStockist()
    {

        string div_code = HttpContext.Current.Session["div_code"].ToString();
        string sf_code = HttpContext.Current.Session["sf_code"].ToString();

        DCR dc = new DCR();
        DataSet dsSale = dc.getStockiest(sf_code, div_code);

        List<Stockist_Pr> objStockData = new List<Stockist_Pr>();

        foreach (DataRow dr in dsSale.Tables[0].Rows)
        {
            Stockist_Pr objch = new Stockist_Pr();
            objch.Stockist_Code = dr["Stockist_Code"].ToString();
            objch.Stockist_Name = dr["Stockist_Name"].ToString();
            objStockData.Add(objch);
        }

        return objStockData;
    }
}

public class DrServiceDetail_Print
{
    public string Sf_Type { get; set; }

    public string DoctorCode { get; set; }
    public string DoctorName { get; set; }
    public string Address { get; set; }
    public string Category { get; set; }
    public string Qualification { get; set; }
    public string Speciality { get; set; }
    public string Class { get; set; }
    public string Mobile { get; set; }
    public string Email { get; set; }
    public string ServiceAmt_tillDate { get; set; }
    public string Business_Date { get; set; }
    public string TotalBusReturn_Amt { get; set; }
    public string ROI_Dur_Month { get; set; }
    public string Service_Req { get; set; }
    public string Service_Amt { get; set; }
    public string Specific_Act { get; set; }
    public string ddlChemist_1 { get; set; }
    public string ddlChemist_2 { get; set; }
    public string ddlChemist_3 { get; set; }
    public string ddlStockist_1 { get; set; }
    public string ddlStockist_2 { get; set; }
    public string ddlStockist_3 { get; set; }

    public string FinancialYear { get; set; }
    public string TransMonth { get; set; }
    public string TransYear { get; set; }

    public string Service_Statement { get; set; }

    public string Sf_Name { get; set; }
    public string Sf_Mgr_1 { get; set; }
    public string Sf_Mgr_2 { get; set; }
    public string Sf_Mgr_3 { get; set; }
    public string Sf_Mgr_4 { get; set; }
    public string Sf_Mgr_5 { get; set; }

    public string Cur_Prod_Code_1 { get; set; }
    public string Cur_Prod_Price_1 { get; set; }
    public string Cur_Prod_Qty_1 { get; set; }
    public string Cur_Prod_Value_1 { get; set; }
    public string Cur_Prod_Code_2 { get; set; }
    public string Cur_Prod_Price_2 { get; set; }
    public string Cur_Prod_Qty_2 { get; set; }
    public string Cur_Prod_Value_2 { get; set; }
    public string Cur_Prod_Code_3 { get; set; }
    public string Cur_Prod_Price_3 { get; set; }
    public string Cur_Prod_Qty_3 { get; set; }
    public string Cur_Prod_Value_3 { get; set; }
    public string Cur_Prod_Code_4 { get; set; }
    public string Cur_Prod_Price_4 { get; set; }
    public string Cur_Prod_Qty_4 { get; set; }
    public string Cur_Prod_Value_4 { get; set; }
    public string Cur_Prod_Code_5 { get; set; }
    public string Cur_Prod_Price_5 { get; set; }
    public string Cur_Prod_Qty_5 { get; set; }
    public string Cur_Prod_Value_5 { get; set; }
    public string Cur_Prod_Code_6 { get; set; }
    public string Cur_Prod_Price_6 { get; set; }
    public string Cur_Prod_Qty_6 { get; set; }
    public string Cur_Prod_Value_6 { get; set; }
    public string Cur_Total { get; set; }

    public string Potl_Prod_Code_1 { get; set; }
    public string Potl_Prod_Price_1 { get; set; }
    public string Potl_Prod_Qty_1 { get; set; }
    public string Potl_Prod_Value_1 { get; set; }
    public string Potl_Prod_Code_2 { get; set; }
    public string Potl_Prod_Price_2 { get; set; }
    public string Potl_Prod_Qty_2 { get; set; }
    public string Potl_Prod_Value_2 { get; set; }
    public string Potl_Prod_Code_3 { get; set; }
    public string Potl_Prod_Price_3 { get; set; }
    public string Potl_Prod_Qty_3 { get; set; }
    public string Potl_Prod_Value_3 { get; set; }
    public string Potl_Prod_Code_4 { get; set; }
    public string Potl_Prod_Price_4 { get; set; }
    public string Potl_Prod_Qty_4 { get; set; }
    public string Potl_Prod_Value_4 { get; set; }
    public string Potl_Prod_Code_5 { get; set; }
    public string Potl_Prod_Price_5 { get; set; }
    public string Potl_Prod_Qty_5 { get; set; }
    public string Potl_Prod_Value_5 { get; set; }
    public string Potl_Prod_Code_6 { get; set; }
    public string Potl_Prod_Price_6 { get; set; }
    public string Potl_Prod_Qty_6 { get; set; }
    public string Potl_Prod_Value_6 { get; set; }
    public string Potl_Total { get; set; }

    public string Sl_No { get; set; }
    public string Prd_Sl_No { get; set; }
    public string Service_Sl_No { get; set; }
    public string ButtonVal { get; set; }
    public string Status { get; set; }

    public string First_Lev_Name { get; set; }
    public string Second_Lev_Name { get; set; }
    public string Third_Lev_Name { get; set; }
    public string Four_Lev_Name { get; set; }
    public string Fivth_Lev_Name { get; set; }
    public string Six_Lev_Name { get; set; }
    public string Seven_Lev_Name { get; set; }
    public string Applied_Date { get; set; }
    public string Approved_Date { get; set; }
    public string Confirmed_Date { get; set; }
    public string Applied_By { get; set; }
    public string Approved_By { get; set; }
    public string Confirm_By { get; set; }
    public string Sf_HQ { get; set; }
    public string Sf_Desig { get; set; }
    public string Division_Name { get; set; }

}

public class ProductDetail_Print
{
    public string Calc_Rate { get; set; }
    public string Product_Detail_Code { get; set; }
    public string Product_Detail_Name { get; set; }
    public string Rate { get; set; }
}

public class Chemist_Pr
{
    public string Chemists_Code { get; set; }
    public string Chemists_Name { get; set; }
}
public class Stockist_Pr
{
    public string Stockist_Code { get; set; }
    public string Stockist_Name { get; set; }
}