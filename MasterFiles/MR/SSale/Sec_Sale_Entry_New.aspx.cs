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

public partial class MasterFiles_MR_SSale_Sec_Sale_Entry_New : System.Web.UI.Page
{
    string sf_code = string.Empty;
    string div_code = string.Empty;
    DataSet dsState = new DataSet();
    DataSet dsSale = new DataSet();
    DataSet dsRet = new DataSet();
    DataSet dsDet = new DataSet();
    string FMonth = string.Empty;
    string FYear = string.Empty;
    string SBillNo = string.Empty;
    string TBillNo = string.Empty;
    string RBillNo = string.Empty;
    string StkCode = string.Empty;
    string Stk_ERPCode = string.Empty;
    string StkName = string.Empty;
    DataSet dsts = new DataSet();
    double sumPrim = 0;
    double sumCB = 0;
    double sumOB = 0;
    double sumRecp = 0;
    double sumSale = 0;
    double sumSaleR = 0;
    double sumTrans = 0;
    double sumSale2 = 0;
    string Prev = string.Empty;
    DataSet dsPrev = new DataSet();
    DataSet dsHead = new DataSet();
    string Head_Slno = string.Empty;
    string sub = string.Empty;
    string sub_code = string.Empty;
    string sub_code_P = string.Empty;
    int ob_code;
    int Primary_Code;
    int Receipt_Code;
    int Sale_Ret_Code;
    int Sale_Code;
    int CB_Code;
    int Pur_Ret_Qty;
    int Transit_Code;
    int Free_Code;
    SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Ereportcon"].ConnectionString);
    protected void Page_Load(object sender, EventArgs e)
    {
        div_code = Session["div_code"].ToString();
        //sMode = Request.QueryString["cMode"].ToString();
        sf_code = Request.QueryString["sfcode"].ToString();
        FMonth = Request.QueryString["FMonth"].ToString();
        FYear = Request.QueryString["Fyear"].ToString();
        if (Request.QueryString["Status"].ToString() != "2")
        {
            SBillNo = Request.QueryString["Billno"].ToString();
            TBillNo = Request.QueryString["TBillno"].ToString();
            RBillNo = Request.QueryString["RBillno"].ToString();



        }
        Stk_ERPCode = Request.QueryString["st_ERP"].ToString();
        hdnStatus.Value = Request.QueryString["Status"].ToString();
        StkCode = Request.QueryString["stk_code"].ToString();
        StkName = Request.QueryString["stk_name"].ToString();

        SubDivision sb = new SubDivision();
        DataSet dsSub = sb.getSub_sf(sf_code);
        if (dsSub.Tables[0].Rows.Count > 0)
        {
            sub_code = dsSub.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
            sub_code_P = dsSub.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
            sub_code_P = sub_code_P.Remove(sub_code_P.Length - 1);
        }
        SecSale sale = new SecSale();
        dsPrev = sale.Get_Prev_St_Division(div_code, StkCode, sub_code_P);
        if (dsPrev.Tables[0].Rows.Count > 0)
        {
            hdnPrev.Value = "1";
        }
        else
        {
            hdnPrev.Value = "2";
        }
        //if ((FMonth.Trim() == "4" && FYear.Trim() == "2019" && Request.QueryString["Status"].ToString() != "2"))
        //{
        //    hdnPrev.Value = "3";
        //}

        
        if (!Page.IsPostBack) // Only on first time page load
        {
            if (Session["sf_type"].ToString() == "2")
            {
                div_code = Session["div_code"].ToString();
                UserControl_MGR_Menu c1 =
               (UserControl_MGR_Menu)LoadControl("~/UserControl/MGR_Menu.ascx");
                Divid.Controls.Add(c1);
                c1.Title = Page.Title;
                c1.FindControl("btnBack").Visible = false;

            }
            else if (Session["sf_type"].ToString() == "1")
            {
                div_code = Session["div_code"].ToString();

                UserControl_MR_Menu c1 =
               (UserControl_MR_Menu)LoadControl("~/UserControl/MR_Menu.ascx");
                Divid.Controls.Add(c1);
                c1.Title = Page.Title;
                c1.FindControl("btnBack").Visible = false;


            }
            SalesForce sf = new SalesForce();
            string strFrmMonth = sf.getMonthName(FMonth);
            lblHead.Text = "Secondary Sale Entry for " + strFrmMonth + " " + FYear;
            lblstk.Text = StkName;
            GetMrDet();
            GetStkDet();
            if (Request.QueryString["Status"].ToString() != "2")
            {
                btnSubmit.Visible = false;
                btnReject.Visible = false;
                FillReport();
            }
            else
            {
                btnBackBill.Visible = false;
                btnDraft.Visible = false;
                btnApprove.Visible = false;
                FillMGR_App();
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
                c1.FindControl("btnBack").Visible = false;

            }
            else if (Session["sf_type"].ToString() == "1")
            {
                div_code = Session["div_code"].ToString();
                UserControl_MR_Menu c1 =
               (UserControl_MR_Menu)LoadControl("~/UserControl/MR_Menu.ascx");
                Divid.Controls.Add(c1);
                c1.Title = Page.Title;
                c1.FindControl("btnBack").Visible = false;

            }
        }
    }


    private void GetMrDet()
    {
        SalesForce sf = new SalesForce();
        dsDet = sf.getSfCode_Verify(sf_code, div_code);
        if (dsDet.Tables[0].Rows.Count > 0)
        {
            LblForceName.Text = " " + dsDet.Tables[0].Rows[0].ItemArray.GetValue(1).ToString();

            lblDesig.Text = " " + dsDet.Tables[0].Rows[0].ItemArray.GetValue(4).ToString();
            lblHQ.Text = " " + dsDet.Tables[0].Rows[0].ItemArray.GetValue(2).ToString();

            lblSt.Text = dsDet.Tables[0].Rows[0]["State_Code"].ToString();
            lblsub.Text = dsDet.Tables[0].Rows[0]["subdivision_code"].ToString();
            //lblsub.Text = lblsub.Text.Remove(lblsub.Text.Length - 1);
        }
    }
    private void GetStkDet()
    {
        Stockist st = new Stockist();
        dsDet = st.getStockistCreate_StockistName(div_code, StkCode);
        if (dsDet.Tables[0].Rows.Count > 0)
        {

            lblState.Text = dsDet.Tables[0].Rows[0]["State"].ToString();
            //lblStkHq.Text = dsDet.Tables[0].Rows[0]["Territory"].ToString();

        }
    }
    protected void grdProd_RowDataBound(object sender, GridViewRowEventArgs e)
    {


        double primary = 0.0;
        double CB_Q = 0.0;
        double OB_Q = 0.0;

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //int indx = e.Row.RowIndex;
            //int k = e.Row.Cells.Count - 5;
            //int l = 7;
            //    int primary = Convert.ToInt32((e.Row.Cells[l].Text == "") || (e.Row.Cells[l].Text == "&nbsp;") || (e.Row.Cells[l].Text == "-") ? "0" : e.Row.Cells[l].Text);
            //    int Transit = Convert.ToInt32((e.Row.Cells[l + 4].Text == "") || (e.Row.Cells[l + 4].Text == "&nbsp;") || (e.Row.Cells[l + 4].Text == "-") ? "0" : e.Row.Cells[l + 4].Text);

            //    int iRec = primary - Transit;
            //    e.Row.Cells[l + 1].Text = iRec.ToString();

            Label lblRate = (Label)e.Row.FindControl("lblRate");

            TextBox txtPrim = (TextBox)e.Row.FindControl("txtPrimary");
            TextBox trans = (TextBox)e.Row.FindControl("txtTran");

            TextBox Receipt = (TextBox)e.Row.FindControl("txtRec");

            TextBox S_Ret = (TextBox)e.Row.FindControl("txtSRet");

            TextBox Sale = (TextBox)e.Row.FindControl("txtSale");

            TextBox Sale2 = (TextBox)e.Row.FindControl("txtSale2");

            TextBox CB = (TextBox)e.Row.FindControl("txtCB");

            TextBox OB = (TextBox)e.Row.FindControl("txtOB");
            TextBox trans2 = (TextBox)e.Row.FindControl("txttrans2");

            Label lblsal = (Label)e.Row.FindControl("lblsal");
            Label lblCb = (Label)e.Row.FindControl("lblCb");


            if (Request.QueryString["Status"].ToString() != "2")
            {
                OB.Enabled = true;
            }
            else
            {
                OB.Enabled = false;
            }
            if (hdnPrev.Value == "1")
            {
                OB.Enabled = false;
            }
            else if (hdnPrev.Value == "2")
            {
                OB.Enabled = true;
            }
            //else if (hdnPrev.Value == "3")
            //{
            //    OB.Enabled = true;
            //}
            //if (FMonth == "4" && FYear == "2019")
            //{
            //    OB.Enabled = true;
            //}
            primary = Convert.ToDouble((txtPrim.Text == "") || (txtPrim.Text == "&nbsp;") || (txtPrim.Text == "-") ? "0" : txtPrim.Text);

            double Transit = Convert.ToDouble((trans.Text == "") || (trans.Text == "&nbsp;") || (trans.Text == "-") ? "0" : trans.Text);
            sumTrans += Transit * (Convert.ToDouble(lblRate.Text.Trim())); ;
            double SRet = Convert.ToDouble((S_Ret.Text == "") || (S_Ret.Text == "&nbsp;") || (S_Ret.Text == "-") ? "0" : S_Ret.Text);
            sumSaleR += SRet * (Convert.ToDouble(lblRate.Text.Trim()));
            OB_Q = Convert.ToDouble((OB.Text == "") || (OB.Text == "&nbsp;") || (OB.Text == "-") ? "0" : OB.Text);

            double Transit2 = Convert.ToDouble((trans2.Text == "") || (trans2.Text == "&nbsp;") || (trans2.Text == "-") ? "0" : trans2.Text);
            if (Request.QueryString["Status"].ToString() != "2")
            {

                primary = (primary + Transit);
                txtPrim.Text = primary.ToString();

                double receipt = (primary - Transit) + Transit2;
                Receipt.Text = Math.Abs(receipt).ToString();
            }
            else
            {
                double receipt = Convert.ToDouble((Receipt.Text == "") || (Receipt.Text == "&nbsp;") || (Receipt.Text == "-") ? "0" : Receipt.Text);
                Receipt.Text = Math.Abs(receipt).ToString();
            }
            sumPrim += primary * (Convert.ToDouble(lblRate.Text.Trim()));
            sumRecp += Convert.ToDouble(Receipt.Text) * (Convert.ToDouble(lblRate.Text.Trim()));

            int Ob_Qt = Convert.ToInt32((OB.Text == "") || (OB.Text == "&nbsp;") || (OB.Text == "-") ? "0" : OB.Text);
            //if ((txtPrim.Text == "0" || txtPrim.Text == "") && (Receipt.Text == "0" || Receipt.Text == "") && (S_Ret.Text == "" || S_Ret.Text == "0") && (trans.Text == "0" || trans.Text == "") && OB.Text != "0")
            //{


            //  //  CB.Text = Ob_Qt.ToString();

            //    Sale.Text = Ob_Qt.ToString();
            //    //if (Request.QueryString["Status"].ToString() == "2")
            //    //{
            //    //    CB.Enabled = false;
            //    //}
            //    if (Request.QueryString["Status"].ToString() != "2")
            //    {
            //        double PSale = (Convert.ToDouble(Receipt.Text) - SRet);
            //        Sale.Text = Math.Abs(PSale).ToString();
            //        Sale2.Text = Math.Abs(PSale).ToString();
            //    }
            //    else
            //    {
            //        double PSale = Convert.ToDouble((Sale.Text == "") || (Sale.Text == "&nbsp;") || (Sale.Text == "-") ? "0" : Sale.Text);
            //        Sale.Text = Math.Abs(PSale).ToString();
            //        Sale2.Text = Math.Abs(PSale).ToString();
            //    }
            //}
            //else
            //{
            if (Request.QueryString["Status"].ToString() != "2")
            {
                double PSale = (Convert.ToDouble(Receipt.Text) - SRet) + Convert.ToDouble(OB_Q);
                Sale.Text = PSale.ToString();
                Sale2.Text = PSale.ToString();
            }
            else
            {
                double PSale = Convert.ToDouble((Sale.Text == "") || (Sale.Text == "&nbsp;") || (Sale.Text == "-") ? "0" : Sale.Text);
                Sale.Text = PSale.ToString();
                Sale2.Text = PSale.ToString();
            }
            // }
            //if ((txtPrim.Text == "0" || txtPrim.Text == "") && (Receipt.Text == "0" || Receipt.Text == "") && (S_Ret.Text != "0") && (CB.Text == "0" || CB.Text == "") && (trans.Text == "0" || trans.Text == "") && OB.Text != "0")
            //{
            //    int Ob_Qt2 = Ob_Qt - Convert.ToInt32(SRet);

            //    CB.Text = Math.Abs(Ob_Qt2).ToString();

            //    Sale.Text = "0";
            //}

            sumSale += Convert.ToDouble(Sale.Text) * (Convert.ToDouble(lblRate.Text.Trim()));


            sumSale2 += Convert.ToDouble(Sale2.Text) * (Convert.ToDouble(lblRate.Text.Trim()));


            CB_Q = Convert.ToDouble((CB.Text == "") || (CB.Text == "&nbsp;") || (CB.Text == "-") ? "0" : CB.Text);
            sumCB += CB_Q * (Convert.ToDouble(lblRate.Text.Trim()));


            sumOB += OB_Q * (Convert.ToDouble(lblRate.Text.Trim()));

            // CB.Attributes.Add("onkeyup", "javascript:get(" + Sale.ClientID + "," + CB.ClientID + ")");
            if (OB.Text == "0")
            {
                OB.Text = "";
            }
            if (Sale.Text == "0")
            {
                Sale.Text = "";
            }
            if (Receipt.Text == "0")
            {
                Receipt.Text = "";
            }
            if (txtPrim.Text == "0")
            {
                txtPrim.Text = "";
            }
            if (trans.Text == "0")
            {
                trans.Text = "";
            }
            if (S_Ret.Text == "0")
            {
                S_Ret.Text = "";
            }
            if (CB.Text == "0")
            {
                CB.Text = "";
            }
            //if (Sale2.Text == "0")
            //{
            //    Sale2.Text = "";
            //}
            //if (Receipt.Text.Trim() != "")
            //{
            //    Sale.Text = "0";
            //}
        }

        if (e.Row.RowType == DataControlRowType.Footer)
        {



            Label lblOB = (Label)e.Row.FindControl("lblOBqty");
            lblOB.Text = sumOB.ToString();

            Label lblPrim = (Label)e.Row.FindControl("lblPrimqty");
            lblPrim.Text = sumPrim.ToString();

            Label lblReceipt = (Label)e.Row.FindControl("lblRecqty");
            lblReceipt.Text = sumRecp.ToString();

            Label lblsale = (Label)e.Row.FindControl("lblSaleqty");
            lblsale.Text = sumSale.ToString();

            Label lblsaleR = (Label)e.Row.FindControl("lblSalRetqty");
            lblsaleR.Text = sumSaleR.ToString();

            Label lblCB = (Label)e.Row.FindControl("lblCBqty");
            lblCB.Text = sumCB.ToString();

            Label lbltrans = (Label)e.Row.FindControl("lblTransqty");
            lbltrans.Text = sumTrans.ToString();


            Label lblsale2 = (Label)e.Row.FindControl("lblSaleqty2");
            lblsale2.Text = sumSale2.ToString();

            //   Label lblSaleval = (Label)e.Row.FindControl("lblSaleval");
            //lblSaleval.Text = sumSale2.ToString();

            //Label lblCbVal = (Label)e.Row.FindControl("lblCbVal");
            //lblCbVal.Text = sumCB.ToString();


        }
    }
    private void FillMGR_App()
    {
        int cmonth = Convert.ToInt32(FMonth);
        int cyear = Convert.ToInt32(FYear);
        string sProcName = "";
        sub = lblsub.Text.Remove(lblsub.Text.Length - 1);
        sProcName = "Secondary_Sale_Entry_App_Sub";


        if (sProcName != "")
        {
            string strConn = System.Configuration.ConfigurationManager.ConnectionStrings["Ereportcon"].ToString();
            SqlConnection con = new SqlConnection(strConn);
            con.Open();
            SqlCommand cmd = new SqlCommand(sProcName, con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Div_Code", div_code);
            cmd.Parameters.AddWithValue("@Msf_code", sf_code);
            cmd.Parameters.AddWithValue("@subdiv", sub);
            cmd.Parameters.AddWithValue("@cMnth", cmonth);
            cmd.Parameters.AddWithValue("@cYrs", cyear);
            cmd.Parameters.AddWithValue("@stk_Code", StkCode.Trim());
            cmd.Parameters.AddWithValue("@st_code", lblSt.Text.Trim());

            cmd.CommandTimeout = 600;

            SqlDataAdapter da = new SqlDataAdapter(cmd);

            da.Fill(dsts);
            con.Close();

            //

            if (dsts.Tables[0].Rows.Count > 0)
            {
                grdProd.DataSource = dsts;
                grdProd.DataBind();
            }
            else
            {
                grdProd.DataSource = dsts;
                grdProd.DataBind();
                btnSubmit.Visible = false;
                btnApprove.Visible = false;
                btnReject.Visible = false;
            }
        }
    }
    private void FillReport()
    {

        int cmonth = Convert.ToInt32(FMonth);
        int cyear = Convert.ToInt32(FYear);

        DataTable dtSpclty = new DataTable();
        dtSpclty.Columns.Add("INX", typeof(int)).AutoIncrement = true;
        dtSpclty.Columns["INX"].AutoIncrementSeed = 1;
        dtSpclty.Columns["INX"].AutoIncrementStep = 1;
        dtSpclty.Columns.Add("CODE", typeof(DateTime));

        string spclty = Request.QueryString["Billno"].ToString();
        if (spclty != "")
        {
            spclty = spclty.Remove(spclty.LastIndexOf('~'));
            string[] ttlSpc = spclty.Split('~');

            foreach (string sSpclty in ttlSpc)
            {
                if (sSpclty != "")
                    dtSpclty.Rows.Add(null, Convert.ToDateTime(sSpclty));
            }
        }
        DataTable dtTransit = new DataTable();
        dtTransit.Columns.Add("INX", typeof(int)).AutoIncrement = true;
        dtTransit.Columns["INX"].AutoIncrementSeed = 1;
        dtTransit.Columns["INX"].AutoIncrementStep = 1;
        dtTransit.Columns.Add("Recep", typeof(DateTime));

        string Trans = Request.QueryString["TBillno"].ToString();
        if (Trans != "")
        {
            Trans = Trans.Remove(Trans.LastIndexOf('~'));
            string[] ttlTrans = Trans.Split('~');

            foreach (string sTrans in ttlTrans)
            {
                if (sTrans != "")
                    dtTransit.Rows.Add(null, Convert.ToDateTime(sTrans));
            }
        }
        DataTable dtRet = new DataTable();
        dtRet.Columns.Add("INX", typeof(int)).AutoIncrement = true;
        dtRet.Columns["INX"].AutoIncrementSeed = 1;
        dtRet.Columns["INX"].AutoIncrementStep = 1;
        dtRet.Columns.Add("Return", typeof(DateTime));

        string Ret = Request.QueryString["RBillno"].ToString();
        if (Ret != "")
        {
            Ret = Ret.Remove(Ret.LastIndexOf('~'));
            string[] ttlRet = Ret.Split('~');

            foreach (string sRet in ttlRet)
            {
                if (sRet != "")
                    dtRet.Rows.Add(null, Convert.ToDateTime(sRet));
            }
        }

        string sProcName = "";

        sProcName = "Secondary_Sale_Entry_Primary";
        sub = lblsub.Text.Remove(lblsub.Text.Length - 1);

        if (sProcName != "")
        {
            string strConn = System.Configuration.ConfigurationManager.ConnectionStrings["Ereportcon"].ToString();
            SqlConnection con = new SqlConnection(strConn);
            con.Open();
            SqlCommand cmd = new SqlCommand(sProcName, con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Div_Code", div_code);
            cmd.Parameters.AddWithValue("@Msf_code", sf_code);
            cmd.Parameters.AddWithValue("@subdiv", sub);
            cmd.Parameters.AddWithValue("@cMnth", cmonth);
            cmd.Parameters.AddWithValue("@cYrs", cyear);
            cmd.Parameters.AddWithValue("@stk_Code", StkCode.Trim());
            cmd.Parameters.AddWithValue("@st_code", lblSt.Text.Trim());
            cmd.Parameters.AddWithValue("@BillTbl", dtSpclty);
            cmd.Parameters.AddWithValue("@TransTbl", dtTransit);
            cmd.Parameters.AddWithValue("@RetTbl", dtRet);
            cmd.CommandTimeout = 600;

            SqlDataAdapter da = new SqlDataAdapter(cmd);

            da.Fill(dsts);
            con.Close();
            //dtrowClr = dsts.Tables[0].Copy();
            //dsts.Tables[0].Columns.RemoveAt(6);
            //dsts.Tables[0].Columns.RemoveAt(5);
            //dsts.Tables[0].Columns.RemoveAt(1);
            //
            if (dsts.Tables[0].Rows.Count > 0)
            {
                grdProd.DataSource = dsts;
                grdProd.DataBind();
            }
            else
            {
                grdProd.DataSource = dsts;
                grdProd.DataBind();
                btnSubmit.Visible = false;
                btnApprove.Visible = false;
            }
        }
    }

    protected void btnApprove_Click(object sender, EventArgs e)
    {
        int cmonth = Convert.ToInt32(FMonth);
        int cyear = Convert.ToInt32(FYear);
        StringBuilder sb = new StringBuilder();
        int iReturn = -1;
        //Loop through each row of gridview
        sb.Append("<root>");
        string SaleQty = string.Empty;
        
        SqlDataAdapter da;

        SqlConnection con = new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["Ereportcon"].ConnectionString);

        con.Open();
        SqlCommand cmd2 = new SqlCommand("select Sec_Sale_Code,Sec_Sale_Name,Sec_Sale_Short_Name,Pri_Sec_SName from Mas_Sec_Sale_Param where division_code='" + div_code + "'  ", con);
        DataSet dss = new DataSet();
        da = new SqlDataAdapter(cmd2);
        da.Fill(dss);
        if (dss.Tables[0].Rows.Count > 0)
        {

            for (int i = 0; i < dss.Tables[0].Rows.Count; i++)
            {
                if (dss.Tables[0].Rows[i]["Pri_Sec_SName"].ToString().Trim() == "OB")
                {
                    ob_code = Convert.ToInt32(dss.Tables[0].Rows[i]["Sec_Sale_Code"].ToString().Trim());
                }
                else if (dss.Tables[0].Rows[i]["Pri_Sec_SName"].ToString().Trim() == "PS")
                {
                    Primary_Code = Convert.ToInt32(dss.Tables[0].Rows[i]["Sec_Sale_Code"].ToString().Trim());
                }
                else if (dss.Tables[0].Rows[i]["Pri_Sec_SName"].ToString().Trim() == "RE")
                {
                    Receipt_Code = Convert.ToInt32(dss.Tables[0].Rows[i]["Sec_Sale_Code"].ToString().Trim());
                }
                else if (dss.Tables[0].Rows[i]["Pri_Sec_SName"].ToString().Trim() == "TR")
                {
                    Transit_Code = Convert.ToInt32(dss.Tables[0].Rows[i]["Sec_Sale_Code"].ToString().Trim());
                }
                else if (dss.Tables[0].Rows[i]["Pri_Sec_SName"].ToString().Trim() == "SR")
                {
                    Sale_Ret_Code = Convert.ToInt32(dss.Tables[0].Rows[i]["Sec_Sale_Code"].ToString().Trim());
                }
                else if (dss.Tables[0].Rows[i]["Pri_Sec_SName"].ToString().Trim() == "SA")
                {
                    Sale_Code = Convert.ToInt32(dss.Tables[0].Rows[i]["Sec_Sale_Code"].ToString().Trim());
                }
                else if (dss.Tables[0].Rows[i]["Pri_Sec_SName"].ToString().Trim() == "CB")
                {
                    CB_Code = Convert.ToInt32(dss.Tables[0].Rows[i]["Sec_Sale_Code"].ToString().Trim());
                }
            }
        }

        foreach (GridViewRow row in grdProd.Rows)
        {
            Label ProdCode = (Label)row.Cells[2].FindControl("lblProd_Code");

            Label ProdName = (Label)row.Cells[2].FindControl("lblProdName");

            Label pack = (Label)row.Cells[2].FindControl("lblSaleUnit");

            Label Rate = (Label)row.Cells[2].FindControl("lblRate");

            TextBox OB_Qty = (TextBox)row.Cells[2].FindControl("txtOB");

            TextBox Primary_Qty = (TextBox)row.Cells[2].FindControl("txtPrimary");

            TextBox Recp_Qty = (TextBox)row.Cells[2].FindControl("txtRec");

            TextBox Sale_Qty = (TextBox)row.Cells[2].FindControl("txtSale");

            TextBox Sale_Qty2 = (TextBox)row.Cells[2].FindControl("txtSale2");

            TextBox SRet_Qty = (TextBox)row.Cells[2].FindControl("txtSRet");

            TextBox CB_Qty = (TextBox)row.Cells[2].FindControl("txtCB");

            TextBox Trans_Qty = (TextBox)row.Cells[2].FindControl("txtTran");

            HiddenField hdCB = (HiddenField)row.Cells[2].FindControl("hdnCb");

            HiddenField hdnOb = (HiddenField)row.Cells[2].FindControl("hdnOb");
            if (OB_Qty.Text.Trim() == "")
            {
                OB_Qty.Text = "0";
            }
            if (Sale_Qty.Text.Trim() == "")
            {
                Sale_Qty.Text = "0";
            }
            if (Recp_Qty.Text.Trim() == "")
            {
                Recp_Qty.Text = "0";
            }
            if (Primary_Qty.Text.Trim() == "")
            {
                Primary_Qty.Text = "0";
            }
            if (Trans_Qty.Text.Trim() == "")
            {
                Trans_Qty.Text = "0";
            }
            if (SRet_Qty.Text.Trim() == "")
            {
                SRet_Qty.Text = "0";
            }
            if (CB_Qty.Text.Trim() == "")
            {
                CB_Qty.Text = "0";
            }
            if (Sale_Qty2.Text.Trim() == "")
            {
                Sale_Qty2.Text = "0";
            }
            if (hdCB.Value == "")
            {
                hdCB.Value = "0";
            }
            if (hdnOb.Value == "")
            {
                hdnOb.Value = "0";
            }
            //  Int32 ProdCode = Convert.ToInt32(row.Cells[5].Text);
            //if (Sale_Qty.Text != "0")
            //{
            if (hdnPrev.Value == "2")
            {
                int sale = (Convert.ToInt32(Sale_Qty2.Text) - Convert.ToInt32(CB_Qty.Text)) + Convert.ToInt32(OB_Qty.Text);

                SaleQty = sale.ToString();
            }
            else if (hdnPrev.Value == "1")
            {
                int sale = (Convert.ToInt32(Sale_Qty2.Text) - Convert.ToInt32(CB_Qty.Text)) + Convert.ToInt32(hdCB.Value);

                SaleQty = sale.ToString();
            }
            //else if (hdnPrev.Value == "3")
            //{
            //    int sale = ((Convert.ToInt32(Sale_Qty2.Text) - Convert.ToInt32(CB_Qty.Text)) + Convert.ToInt32(OB_Qty.Text)) - Convert.ToInt32(hdnOb.Value);

            //    SaleQty = sale.ToString();
            //}
            //}
            //else
            //{
            //     SaleQty = "0";
            //}

            sb.Append("<Product Det_sl_no='" + ProdCode.Text + "' ProdName ='" + ProdName.Text + "' Rate='" + Rate.Text + "'  OB_Code='" + ob_code + "' " +
                       " OB_Qty ='" + OB_Qty.Text + "' Primary_Code ='" + Primary_Code.ToString() + "' Primary_Qty ='" + Primary_Qty.Text + "' Receipt_Code ='" + Receipt_Code.ToString() + "' Recp_Qty ='" + Recp_Qty.Text + "' Sale_Code ='" + Sale_Code.ToString() + "'   Sale_Qty ='" + SaleQty + "' " +
                       " Sale_Ret_Code ='" + Sale_Ret_Code.ToString() + "' SRet_Qty ='" + SRet_Qty.Text + "' CB_Code ='" + CB_Code.ToString() + "'  CB_Qty ='" + CB_Qty.Text + "' Transit_Code ='" + Transit_Code.ToString() + "'  Trans_Qty ='" + Trans_Qty.Text + "' Div_Code='" + div_code + "'/>");

        }
        sb.Append("</root>");

       // SqlCommand cmd = new SqlCommand("SP_BulkInsert_TransDel_TransVal", conn);
        DataSet dsHead = new DataSet();
        SecSale sa = new SecSale();
        //dsHead = sa.GetSLNO_SS(div_code, StkCode, cmonth.ToString(), cyear.ToString(), lblsub.Text.Trim());
        //if (dsHead.Tables[0].Rows.Count > 0)
        //{
        //    ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Already Exists');</script>");
        //}
        //else
        //{
            conn.Open();
            SqlCommand cmd = new SqlCommand("SS_Entry_Primary_Insert", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Div_Code", div_code);
            cmd.Parameters.AddWithValue("@Msf_code", sf_code);
            cmd.Parameters.AddWithValue("@subdiv", lblsub.Text.Trim());

            cmd.Parameters.AddWithValue("@cMnth", cmonth);
            cmd.Parameters.AddWithValue("@cYrs", cyear);
            cmd.Parameters.AddWithValue("@stk_Code", StkCode);
            cmd.Parameters.AddWithValue("@st_code", lblSt.Text.Trim());
            cmd.Parameters.AddWithValue("@st_ERPcode", Stk_ERPCode);
            cmd.Parameters.AddWithValue("@XMLProduct", sb.ToString());
            cmd.Parameters.Add("@retValue", SqlDbType.Int);
            cmd.Parameters["@retValue"].Direction = ParameterDirection.Output;
            cmd.ExecuteNonQuery();
            iReturn = Convert.ToInt32(cmd.Parameters["@retValue"].Value.ToString());
            conn.Close();
            if (iReturn > 0)
            {
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Submitted Successfully');window.location='SS_Entry_Primary.aspx'</script>");
            }
            else
            {
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Error Exists');</script>");
            }
        //}
    }
    protected void btnBackBill_Click(object sender, EventArgs e)
    {
        Response.Redirect("SS_Entry_Primary.aspx");
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        int cmonth = Convert.ToInt32(FMonth);
        int cyear = Convert.ToInt32(FYear);
        int iReturn = -1;
        StringBuilder Ub = new StringBuilder();
        //Loop through each row of gridview
        Ub.Append("<Update>");
        string SaleQty = string.Empty;
        foreach (GridViewRow row in grdProd.Rows)
        {
            Label ProdCode = (Label)row.Cells[2].FindControl("lblProd_Code");

            Label ProdName = (Label)row.Cells[2].FindControl("lblProdName");

            Label pack = (Label)row.Cells[2].FindControl("lblSaleUnit");

            Label Rate = (Label)row.Cells[2].FindControl("lblRate");

            TextBox OB_Qty = (TextBox)row.Cells[2].FindControl("txtOB");

            TextBox Primary_Qty = (TextBox)row.Cells[2].FindControl("txtPrimary");

            TextBox Recp_Qty = (TextBox)row.Cells[2].FindControl("txtRec");

            TextBox Sale_Qty = (TextBox)row.Cells[2].FindControl("txtSale");

            TextBox SRet_Qty = (TextBox)row.Cells[2].FindControl("txtSRet");

            TextBox CB_Qty = (TextBox)row.Cells[2].FindControl("txtCB");

            TextBox Trans_Qty = (TextBox)row.Cells[2].FindControl("txtTran");
            TextBox Sale_Qty2 = (TextBox)row.Cells[2].FindControl("txtSale2");


            HiddenField hdCB = (HiddenField)row.Cells[2].FindControl("hdnCb");

            TextBox Trans_Qty2 = (TextBox)row.Cells[2].FindControl("txttrans2");

            //  Int32 ProdCode = Convert.ToInt32(row.Cells[5].Text);
            if (Sale_Qty2.Text.Trim() == "")
            {
                Sale_Qty2.Text = "0";
            }
            if (CB_Qty.Text.Trim() == "")
            {
                CB_Qty.Text = "0";
            }
            if (OB_Qty.Text.Trim() == "")
            {
                OB_Qty.Text = "0";
            }
            //if ((Primary_Qty.Text == "0" || Primary_Qty.Text == "") && (Recp_Qty.Text == "0" || Recp_Qty.Text == "") && (SRet_Qty.Text != "0") && (Trans_Qty.Text == "0" || Trans_Qty.Text == "") && CB_Qty.Text != "0")
            //{
            //    OB_Qty.Text = CB_Qty.Text.Trim();
            //}
            //if (Sale_Qty2.Text != "0")
            //{
            if (hdCB.Value == "")
            {
                hdCB.Value = "0";
            }
            int sale = (Convert.ToInt32(Sale_Qty2.Text) - Convert.ToInt32(CB_Qty.Text)) + Convert.ToInt32(hdCB.Value);

            SaleQty = sale.ToString();
            //}
            //else
            //{
            //    SaleQty = "0";
            //}
            Ub.Append("<row Det_sl_no='" + ProdCode.Text + "'  " +
                       " CB_Qty ='" + CB_Qty.Text.Trim() + "' Sale_Qty ='" + SaleQty + "' OB_Qty ='" + OB_Qty.Text.Trim() + "' Div_Code='" + div_code + "'/>");

        }
        Ub.Append("</Update>");
        conn.Open();
        // SqlCommand cmd = new SqlCommand("SP_BulkInsert_TransDel_TransVal", conn);
        SqlCommand cmd = new SqlCommand("SSEntry_Update_Sub", conn);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Div_Code", div_code);
        cmd.Parameters.AddWithValue("@Msf_code", sf_code);
        cmd.Parameters.AddWithValue("@subdiv", lblsub.Text.Trim());

        cmd.Parameters.AddWithValue("@cMnth", cmonth);
        cmd.Parameters.AddWithValue("@cYrs", cyear);
        cmd.Parameters.AddWithValue("@stk_Code", StkCode.Trim());
        cmd.Parameters.AddWithValue("@st_code", lblSt.Text.Trim());
        cmd.Parameters.AddWithValue("@st_ERPcode", Stk_ERPCode);
        cmd.Parameters.AddWithValue("@XMLUpdate", Ub.ToString());
        cmd.Parameters.Add("@retValue", SqlDbType.Int);
        cmd.Parameters["@retValue"].Direction = ParameterDirection.Output;
        cmd.ExecuteNonQuery();
        iReturn = Convert.ToInt32(cmd.Parameters["@retValue"].Value.ToString());
        if (iReturn > 0)
        {
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Approved Successfully');window.location='../../../MasterFiles/MGR/MGR_Index.aspx'</script>");
        }
        else
        {
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Error Exists');</script>");
        }
        conn.Close();
    }
    protected void btnReject_Click(object sender, EventArgs e)
    {
        btnSubmit.Visible = false;
        btnCReject.Visible = true;
        txtreject.Visible = true;
        txtreject.Focus();


    }
    protected void btnCReject_Click(object sender, EventArgs e)
    {
        SecSale sa = new SecSale();
        int cmonth = Convert.ToInt32(FMonth);
        int cyear = Convert.ToInt32(FYear);
        dsHead = sa.GetSLNO_Reject_Sub(div_code, StkCode, FMonth, FYear, lblsub.Text.Trim());
        if (dsHead.Tables[0].Rows.Count > 0)
        {
            Head_Slno = dsHead.Tables[0].Rows[0]["Sl_No"].ToString();
            using (SqlConnection connection = new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["Ereportcon"].ConnectionString))
            {
                connection.Open();

                SqlCommand command = connection.CreateCommand();
                SqlTransaction transaction;

                // Start a local transaction.
                transaction = connection.BeginTransaction("SampleTransaction");

                // Must assign both transaction object and connection
                // to Command object for a pending local transaction
                command.Connection = connection;
                command.Transaction = transaction;

                try
                {
                    command.CommandText =

               " INSERT INTO Trans_SSEntry_Reject(Stockist_Code,Stockist_Erp_Code,Trans_Month,Trans_Year,Division_Code,Reject_Reason,Reject_Date,Subdivision_code) " +
               " VALUES ('" + StkCode + "', '" + Stk_ERPCode + "','" + cmonth + "','" + cyear + "','" + div_code + "','" + txtreject.Text + "',getdate(),'" + lblsub.Text.Trim() + "')";

                    command.ExecuteNonQuery();


                    command.CommandText =

                      " delete from Trans_Secondary_Entry_Detail  where sl_no='" + Head_Slno + "' and Division_code='" + div_code + "' ";

                    command.ExecuteNonQuery();

                    command.CommandText =

                    " delete from Trans_Secondary_Entry_Head  where sl_no='" + Head_Slno + "' and Division_code='" + div_code + "' and Stockist_Code='" + StkCode + "'  ";

                    command.ExecuteNonQuery();



                    transaction.Commit();
                    connection.Close();
                    ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Rejected Successfully');window.location='../../../MasterFiles/MGR/MGR_Index.aspx';</script>");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Commit Exception Type: {0}", ex.GetType());
                    Console.WriteLine("  Message: {0}", ex.Message);

                    // Attempt to roll back the transaction.
                    try
                    {
                        transaction.Rollback();
                    }
                    catch (Exception ex2)
                    {
                        // This catch block will handle any errors that may have occurred
                        // on the server that would cause the rollback to fail, such as
                        // a closed connection.
                        Console.WriteLine("Rollback Exception Type: {0}", ex2.GetType());
                        Console.WriteLine("  Message: {0}", ex2.Message);
                    }
                    ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Error Exists!');</script>");
                }
            }
        }

    }
}