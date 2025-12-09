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

public partial class MIS_Reports_Input_Modify_MRWise_Rpt : System.Web.UI.Page
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

    //Newly added
    string sInputName = string.Empty;
    string sInputcode = string.Empty;
    string sQty = string.Empty;
    string FinalInputDtls = string.Empty;
    string FinalInputCodes = string.Empty;
    int iReturn = -1;

    string InputQty = string.Empty;
    string InputCode = string.Empty;
    string InputName = string.Empty;
    DB_EReporting db_ER = new DB_EReporting();

    DataSet dsSF = null;
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
        strQry = "select '-1' Gift_Code,'--Select--' Gift_Name union select Gift_Code,Gift_Name+'-'+Gift_SName as Gift_Name from Mas_Gift where division_Code='" + div_code + "' order by Gift_Name";
        DataSet dsproduct = db_ER.Exec_DataSet(strQry);
        return dsproduct;
    }
    private void FillState2()
    {
        strQry = "select '-1' Gift_Code,'--Select--' Gift_Name union select Gift_Code,Gift_Name+'-'+Gift_SName as Gift_Name from Mas_Gift where division_Code='" + div_code + "' order by Gift_Name";
        DataSet dsproduct = db_ER.Exec_DataSet(strQry);
        if (dsproduct.Tables[0].Rows.Count > 0)
        {
            ddlStateHdn.DataTextField = "Gift_Name";
            ddlStateHdn.DataValueField = "Gift_Code";
            ddlStateHdn.DataSource = dsproduct;
            ddlStateHdn.DataBind();
        }
        //return dsproduct;
    }
    private void FillDespatch_status()
    {

        ///strQry = "EXEC Input_Des_New '" + div_code + "', '" + SF_code + "', " + FMonth + "," + FYear + "," + TMonth + "," + TYear + " ";
        //strQry = "EXEC Input_Des_New_Modify '" + div_code + "', '" + SF_code + "', " + FMonth + "," + FYear + "," + TMonth + "," + TYear + ", '" + "1" + "', '" + "0" + "', '" + "" + "' ";
        strQry = "select Gift_Name,Gift_SName,Trans_SlNo,Gift_Code,Sum(isnull(cast(SUBSTRING(valuee,CHARINDEX('~',valuee)+1,LEN(valuee)) as float),0))  as issue_befor into #DCR_Detail_Issue_TRANS from " +
            "(select Gift_Name,Gift_SName, Trans_SlNo, Gift_Code, case when code like '%#%' then  SUBSTRING(code, CHARINDEX('~', code) + 1, CHARINDEX('#',code, CHARINDEX('~', code) + 1) - CHARINDEX('~', code) - 1) else code end as valuee from(select Gift_Name,Gift_SName, Trans_SlNo, Gift_Code,case when Additional_Gift_Code != '' then SUBSTRING(code, charindex('#' + cast(Gift_Code as varchar) + '~', code) + 2, len(code) - CHARINDEX('#', reverse(code))- charindex('#', code)) else   cast(gift_Qty as varchar) end as code from(select Gift_Name,Gift_SName, Trans_SlNo, Additional_Gift_Code, gift_Qty, LEFT(code, LEN(code) - PATINDEX('#', RIGHT(code, 1))) as code, Gift_Code from (select replace('#' + c.Additional_Gift_Code + c.Gift_Code + '~' + cast(gift_Qty as varchar) + '#', ' ', '') as code, a.Gift_Code, Additional_Gift_Code, gift_Qty, a.Gift_Name,a.Gift_SName, Trans_SlNo from dcrdetail_lst_trans c , Mas_Gift a  where charindex('#' + cast(a.Gift_Code as varchar) + '~', '#' + replace(c.Gift_Code + '~' + cast(gift_Qty as varchar) + '#' + Additional_Gift_Code, ' ', '')) > 0 and c.Trans_SlNo = '" + Trans_Slno+"' and Trans_Detail_Info_Code = '"+Dr_Code+ "') tt)ttt) ttt) tttt group by Gift_Code,Gift_SName, Gift_Name, Trans_SlNo select Gift_Name+'-'+Gift_SName as Gift_Name,Trans_SlNo,Gift_Code,issue_befor from #DCR_Detail_Issue_TRANS union select '--Select--' Gift_Name,'10000' Trans_SlNo,'-1' Gift_Code,0 as issue_befor drop table #DCR_Detail_Issue_TRANS";

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
        //strQry = "select Product_Code_SlNo,Product_Detail_Name from Mas_Product_Detail where division_Code='" + div_code + "' and product_Active_Flag=0";
        //DataSet dsproduct = db_ER.Exec_DataSet(strQry);
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DropDownList ddlStateCode = (DropDownList)e.Row.FindControl("ddlState");
            if (ddlStateCode != null)
            {
                DataRowView row = (DataRowView)e.Row.DataItem;
                ddlStateCode.SelectedIndex = ddlStateCode.Items.IndexOf(ddlStateCode.Items.FindByText(row["Gift_Name"].ToString()));
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

                    if (lblissued.Text == "")
                        lblissued.Text = "0";
                    // Update the DataRow with the DDL Selected Items
                    dtCurrentTable.Rows[i]["Gift_Name"] = ddlState.SelectedItem.Text;
                    dtCurrentTable.Rows[i]["Trans_SlNo"] = lblSNo.Text;
                    dtCurrentTable.Rows[i]["Gift_Code"] = ddlState.SelectedValue.Trim();
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
                        ddlState.Items.FindByText(dt.Rows[i]["Gift_Name"].ToString()).Selected = true;
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
                        if (dt.Rows[i]["Gift_Code"].ToString() != "-1")
                        {
                            if ((dt.Rows[j]["Gift_Code"].ToString() == dt.Rows[i]["Gift_Code"].ToString()))
                            {
                                error = 1;
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", " alert('Duplicate Inpuy');", true);
                                break;
                            }
                        }
                    }
                }
            }
        }

        if (error == 0)
        {
            int i = 0;

            foreach (GridViewRow gridRow in grdDespatch.Rows)
            {
                DropDownList ddlInput = (DropDownList)gridRow.Cells[1].FindControl("ddlState");
                TextBox lblInputQty = (TextBox)gridRow.Cells[2].FindControl("lblissued");


                if (lblInputQty.Text == "")
                    lblInputQty.Text = "0";

                sInputName = ddlInput.SelectedItem.Text.ToString();
                sInputcode = ddlInput.SelectedValue.Trim();
                sQty = lblInputQty.Text.ToString();

                if (ddlInput.SelectedIndex != 0)
                {
                    if (sInputName.Trim().Length > 0)
                    {
                        if (i == 0)
                        {
                            InputQty = sQty;
                            InputCode = sInputcode;
                            InputName = sInputName;
                        }
                        else
                        {
                            FinalInputDtls = FinalInputDtls + sInputName + "~" + sQty + "#";
                            FinalInputCodes = FinalInputCodes + sInputcode + "~" + sQty + "#";
                        }
                        i++;
                    }
                }
            }

            DCR dc = new DCR();
            iReturn = dc.RecordUpdate_DCRInput(Trans_Slno, Dr_Code, InputCode, InputName, InputQty, FinalInputCodes, FinalInputDtls);

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

                    if (txtProdQty.Text == "")
                        txtProdQty.Text = "0";

                    drCurrentRow["Trans_SLNO"] = i + 1;
                    dtCurrentTable.Rows[i - 1]["Gift_Code"] = ddlProductAdd.SelectedValue;
                    dtCurrentTable.Rows[i - 1]["Gift_Name"] = ddlProductAdd.SelectedItem.Text.ToString();
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