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
using DBase_EReport;
using System.Data.SqlClient;

public partial class MIS_Reports_Sample_Input_Modify_MRWise_Rpt : System.Web.UI.Page
{
    int cmonth = -1;
    int cyear = -1;
    DataTable dsSalesForce = null;
    DataSet dsDCR = null;
    string sPending = string.Empty;
    string dcrdays = string.Empty;
    string Trans_Slno = string.Empty;
    int tot_days = -1;
    string div_code = string.Empty;
    DateTime ldcrdate;
    int count = 0;
    DataTable dtrowClr = null;
    string Dr_Code = string.Empty;
    string FYear = string.Empty;
    string TMonth = string.Empty;
    string TYear = string.Empty;
    string sf_name = string.Empty;

    string strQry = string.Empty;


    DB_EReporting db_ER = new DB_EReporting();

    DataSet dsSF = null;

    string sProdName = string.Empty;
    string sProdcode = string.Empty;
    string sQty = string.Empty;
    string FinalProducts = string.Empty;
    string FinalProductcodes = string.Empty;
    int iReturn = -1;

    protected void Page_Load(object sender, EventArgs e)
    {
        div_code = Session["div_code"].ToString();
        Trans_Slno = Request.QueryString["trans_SlNo"].ToString();
        Dr_Code = Request.QueryString["Trans_Detail_Info_Code"].ToString();
        //FYear = Request.QueryString["FYear"].ToString();
        //TMonth = Request.QueryString["TMonth"].ToString();
        //TYear = Request.QueryString["TYear"].ToString();
        //sf_name = Request.QueryString["sf_name"].ToString();

        if (Request.QueryString["Dashboard"] != null)//Dashboard
        {
            pnlbutton.Visible = false;
        }

        //lblname.Text = sf_name;


        if (!Page.IsPostBack)
        {
            FillDespatch_status();
            FillState2();
        }
    }


    protected DataSet FillState()
    {
        strQry = "select '-1' Product_Code_SlNo,'--Select--' Product_Detail_Name union select Product_Code_SlNo,Product_Detail_Name+'-'+Sample_Erp_Code as Product_Detail_Name from Mas_Product_Detail where division_Code='" + div_code + "' and Product_Active_Flag=0 and Product_Mode!='sale' order by Product_Detail_Name";
        DataSet dsproduct = db_ER.Exec_DataSet(strQry);
        return dsproduct;
    }
    private void FillState2()
    {
        strQry = "select '-1' Product_Code_SlNo,'--Select--' Product_Detail_Name union select Product_Code_SlNo,Product_Detail_Name+'-'+Sample_Erp_Code as Product_Detail_Name from Mas_Product_Detail where division_Code='" + div_code + "' and Product_Active_Flag=0 and Product_Mode!='sale' order by Product_Detail_Name";
        DataSet dsproduct = db_ER.Exec_DataSet(strQry);
        if (dsproduct.Tables[0].Rows.Count > 0)
        {
            ddlStateHdn.DataTextField = "Product_Detail_Name";
            ddlStateHdn.DataValueField = "Product_Code_SlNo";
            ddlStateHdn.DataSource = dsproduct;
            ddlStateHdn.DataBind();
        }
        //return dsproduct;
    }
    private void FillDespatch_status()
    {

        ///strQry = "EXEC Input_Des_New '" + div_code + "', '" + SF_code + "', " + FMonth + "," + FYear + "," + TMonth + "," + TYear + " ";
        //strQry = "EXEC Input_Des_New_Modify '" + div_code + "', '" + SF_code + "', " + FMonth + "," + FYear + "," + TMonth + "," + TYear + ", '" + "1" + "', '" + "0" + "', '" + "" + "' ";
           strQry = "SELECT Trans_Detail_Info_Code, sf_code,Trans_SlNo,Product_Code,LTRIM(RTRIM(m.n.value('.[1]', 'varchar(8000)'))) AS Prod_code,right((LTRIM(RTRIM(m.n.value('.[1]', 'varchar" +
           "(8000)')))), len((LTRIM(RTRIM(m.n.value('.[1]', 'varchar(8000)'))))) - charindex('~', (LTRIM(RTRIM(m.n.value('.[1]', 'varchar(8000)'))))))issue_Qty into #Drs_Prod_Bfr FROM " +
           "(SELECT c.Trans_SlNo, Trans_Detail_Info_Code, C.sf_code, c.Product_Code,CAST('<XMLRoot><RowData>' + REPLACE(c.Product_Code, '#', '#</RowData><RowData>') + '</RowData></XMLRoot>' AS XML)" +
           " AS x FROM DCRDetail_Lst_Trans C where c.Trans_SlNo = '" + Trans_Slno + "' and c.Trans_Detail_Info_Code = '" + Dr_Code + "' and (Product_Code <> '' and Product_Code like '%~%'))t CROSS APPLY x.nodes('/XMLRoot/RowData')m(n) select Trans_SlNo, Trans_Detail_Info_Code, sf_code, replace((replace((replace((replace((Prod_code), '#', '')), '$0', '')), '^0', '')), '$', '')Prod_code,issue_Qty Into #Drs_Prod_Bfr_Filtr from #Drs_Prod_Bfr where (Prod_code<>'' and Prod_code<>'0') select distinct Trans_SlNo,Trans_Detail_Info_Code,sf_code,LEFT(Prod_code + '~', CHARINDEX('~', Prod_code + '~') - 1)Product_Code_SlNo ,replace((LEFT(issue_Qty + '$', CHARINDEX('$', issue_Qty + '$') - 1)), '#', '')issue_befor into #DCR_Detail_Issue_TRANS2 from #Drs_Prod_Bfr_Filtr select Trans_SlNo,a.Product_Code_SlNo, Product_Detail_Name+'-'+Sample_Erp_Code as Product_Detail_Name, issue_befor from #DCR_Detail_Issue_TRANS2 a,Mas_Product_Detail b where a.Product_Code_SlNo=b.Product_Code_SlNo and b.Product_Active_Flag=0 union select '10000' as Trans_SlNo,'-1' Product_Code_SlNo,'--Select--' Product_Detail_Name,0 as issue_befor drop table #Drs_Prod_Bfr drop table #Drs_Prod_Bfr_Filtr drop table #DCR_Detail_Issue_TRANS2";

        dsSalesForce = db_ER.Exec_DataTable(strQry);


        if (dsSalesForce.Rows.Count > 0)
        {
            btntransfer.Visible = true;
            grdDespatch.DataSource = dsSalesForce;
            grdDespatch.DataBind();
            ViewState["CurrentTable"] = dsSalesForce;
        }
        else
        {
            grdDespatch.DataSource = dsSalesForce;
            grdDespatch.DataBind();
        }
    }

    protected void grdDr_RowDataBoud(object sender, GridViewRowEventArgs e)
    {
        strQry = "select Product_Code_SlNo,Product_Detail_Name from Mas_Product_Detail where division_Code='" + div_code+"'";
       DataSet dsproduct = db_ER.Exec_DataSet(strQry);
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DropDownList ddlStateCode = (DropDownList)e.Row.FindControl("ddlState");
            if (ddlStateCode != null)
            {
                DataRowView row = (DataRowView)e.Row.DataItem;
                ddlStateCode.SelectedIndex = ddlStateCode.Items.IndexOf(ddlStateCode.Items.FindByText(row["Product_Detail_Name"].ToString()));
            }

            //if (lblGift_Name.Text == "Total")
            //{
            //    lblGift_Name.Font.Bold = true;
            //    lblGift_Name.ForeColor = System.Drawing.Color.Red;
            //    lblSNo.Text = "";
            //    lblopening.Font.Bold = true;
            //    lblDes.Font.Bold = true;
            //    lblissued.Font.Bold = true;
            //    lblclosing.Font.Bold = true;
            //    lblGift_Name.Attributes.Add("align", "right");

            //}
        }

    }
    protected void Gridview1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        if (ViewState["CurrentTable"] != null)
        {
            DataTable dt = (DataTable)ViewState["CurrentTable"];
            DataRow drCurrentRow = null;
            int rowIndex = Convert.ToInt32(e.RowIndex);
            if (dt.Rows.Count > 1)
            {
                dt.Rows.Remove(dt.Rows[rowIndex]);
                drCurrentRow = dt.NewRow();
                ViewState["CurrentTable"] = dt;
                grdDespatch.DataSource = dt;
                grdDespatch.DataBind();

                for (int i = 0; i < grdDespatch.Rows.Count - 1; i++)
                {
                    grdDespatch.Rows[i].Cells[0].Text = Convert.ToString(i + 1);
                }
                SetPreviousData();
            }

        }
    }
    private void AddNewRowToGrid()
    {

        if (ViewState["CurrentTable"] != null)
        {
            DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable"];
            DataRow drCurrentRow = null;

            if (dtCurrentTable.Rows.Count > 0)
            {
                drCurrentRow = dtCurrentTable.NewRow();
                drCurrentRow["Trans_SlNo"] = dtCurrentTable.Rows.Count + 1;
                //add new row to DataTable
                dtCurrentTable.Rows.Add(drCurrentRow);
                //Store the current data to ViewState
                ViewState["CurrentTable"] = dtCurrentTable;

                for (int i = 0; i < dtCurrentTable.Rows.Count - 1; i++)
                {
                    //extract the DropDownList Selected Items
                    Label lblSNo = (Label)grdDespatch.Rows[i].Cells[1].FindControl("lblSNo");
                    DropDownList ddlState = (DropDownList)grdDespatch.Rows[i].Cells[2].FindControl("ddlState");
                    TextBox lblissued = (TextBox)grdDespatch.Rows[i].Cells[3].FindControl("lblissued");

                    ddlState.Attributes.Add("onchange", "selectionChange(this)");
                    //   ddl2.Attributes.Add("onchange", "selectionChange(this)");

                    // Update the DataRow with the DDL Selected Items
                    dtCurrentTable.Rows[i]["Trans_SlNo"] = lblSNo.Text;
                    dtCurrentTable.Rows[i]["Product_Detail_Name"] = ddlState.SelectedItem.Text;
                    dtCurrentTable.Rows[i]["issue_befor"] = lblissued.Text =="" ? "0" : lblissued.Text;

                }

                //Rebind the Grid with the current data
                grdDespatch.DataSource = dtCurrentTable;
                grdDespatch.DataBind();
            }
        }
        else
        {
            Response.Write("ViewState is null");
        }

        //Set Previous Data on Postbacks
        SetPreviousData();
    }

    private void SetPreviousData()
    {
        int rowIndex = 0;
        if (ViewState["CurrentTable"] != null)
        {
            DataTable dt = (DataTable)ViewState["CurrentTable"];
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {

                    Label lblSNo = (Label)grdDespatch.Rows[i].Cells[1].FindControl("lblSNo");
                    DropDownList ddlState = (DropDownList)grdDespatch.Rows[i].Cells[2].FindControl("ddlState");
                    TextBox lblissued = (TextBox)grdDespatch.Rows[i].Cells[3].FindControl("lblissued");

                    //Fill the DropDownList with Data
                    //   FillDropDownList(ddl1);


                    ddlState.Attributes.Add("onchange", "selectionChange(this)");

                    if (i < dt.Rows.Count - 1)
                    {
                        ddlState.ClearSelection();
                    //    lblSNo.Text = dt.Rows[i]["Trans_SlNo"].ToString();
                        ddlState.Items.FindByText(dt.Rows[i]["Product_Detail_Name"].ToString()).Selected = true;
                        lblissued.Text = dt.Rows[i]["issue_befor"].ToString();

                    }
                   // lblSNo.Focus();
                    rowIndex++;
                }
            }
        }
    }
    protected void ButtonAdd_Click(object sender, EventArgs e)
    {
        AddNewRowToGrid();
    }
    protected void BtnUpdate_Click(object sender, EventArgs e)
    {

        SetRowData();
        int error = 0;
        DataTable dt = (DataTable)ViewState["CurrentTable"];
        if (dt != null)
        {
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    for (int j = i + 1; j < dt.Rows.Count; j++)
                    {
                        if (dt.Rows[i]["Product_Code_SlNO"].ToString() != "-1")
                        {
                            if ((dt.Rows[j]["Product_Code_SlNO"].ToString() == dt.Rows[i]["Product_Code_SlNO"].ToString()))
                            {
                                error = 1;
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", " alert('Duplicate Product');", true);
                                break;
                            }
                        }
                    }
                }
            }
        }

        if (error == 0)
        {

            foreach (GridViewRow gridRow in grdDespatch.Rows)
            {
                DropDownList ddlProduct = (DropDownList)gridRow.Cells[1].FindControl("ddlState");
                TextBox lblSampleQty = (TextBox)gridRow.Cells[2].FindControl("lblissued");


                if (lblSampleQty.Text == "")
                    lblSampleQty.Text = "0";

                sProdName = ddlProduct.SelectedItem.Text.ToString();
                sProdcode = ddlProduct.SelectedValue.Trim();
                sQty = lblSampleQty.Text.ToString();

                if (ddlProduct.SelectedIndex != 0)
                {
                    if (sProdName.Trim().Length > 0)
                    {
                        FinalProducts = FinalProducts + sProdName + "~" + sQty + "$0" + "#";
                        FinalProductcodes = FinalProductcodes + sProdcode + "~" + sQty + "$0$" + "#";
                    }
                }
            }

            DCR dc = new DCR();
            iReturn = dc.RecordUpdate_DCRProduct(Trans_Slno, Dr_Code, FinalProducts, FinalProductcodes);
            if (iReturn > 0)
            {
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Saved Successfully');</script>");

            }
            else

            {
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Error Exists!');</script>");
            }

        }


    }


    private void SetRowData()
    {
        int rowIndex = 0;

        if (ViewState["CurrentTable"] != null)
        {
            DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable"];
            DataRow drCurrentRow = null;
            if (dtCurrentTable.Rows.Count > 0)
            {
                for (int i = 1; i <= dtCurrentTable.Rows.Count; i++)
                {
                    DropDownList ddlProductAdd =
                      (DropDownList)grdDespatch.Rows[rowIndex].Cells[1].FindControl("ddlState");
                    TextBox txtProdQty =
                      (TextBox)grdDespatch.Rows[rowIndex].Cells[2].FindControl("lblissued");
                    drCurrentRow = dtCurrentTable.NewRow();
                    drCurrentRow["Trans_SLNO"] = i + 1;
                    dtCurrentTable.Rows[i - 1]["Product_Code_SlNO"] = ddlProductAdd.SelectedValue;
                    dtCurrentTable.Rows[i - 1]["Product_Detail_Name"] = ddlProductAdd.SelectedItem.Text.ToString();
                    dtCurrentTable.Rows[i - 1]["issue_befor"] = txtProdQty.Text;
                    rowIndex++;
                }
                ViewState["CurrentTable"] = dtCurrentTable;
            }
        }
        else
        {
            Response.Write("ViewState is null");
        }
    }
}