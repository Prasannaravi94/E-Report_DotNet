using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Data;
using System.Configuration;
using Bus_EReport;

public partial class MasterFiles_Options_Expense_Setup : System.Web.UI.Page
{

    string div_code = string.Empty;
    string startfrom_periodically = string.Empty;
    string Fieldforce_HQ_Ex_Max = string.Empty;
    string txtmini = string.Empty;
    DataSet dsadmin = null;

    protected void Page_Load(object sender, EventArgs e)
    {
        div_code = Session["div_code"].ToString();

        if (!Page.IsPostBack)
        {
            //menu1.Title = this.Page.Title;
            //menu1.FindControl("btnBack").Visible = false;
            Distance_calculation Exp = new Distance_calculation();
            DataTable otherExpTable = Exp.getDesigExp(div_code);
            //generateOtherExpListData(otherExpTable);
            generateOtherExpControls(Exp);
            AdminSetup adm = new AdminSetup();
            dsadmin = adm.getSetup_Expense(div_code);

            if (dsadmin.Tables[0].Rows.Count > 0)
            {
                if (dsadmin.Tables[0].Rows[0].ItemArray.GetValue(0).ToString() == "1")
                {
                    rdoMgrRemarks.SelectedValue = "1";
                    rdoMgrRemarks.SelectedItem.Attributes.Add("style", "color:red;font-weight:bold");
                }
                else if (dsadmin.Tables[0].Rows[0].ItemArray.GetValue(0).ToString() == "0")
                {
                    rdoMgrRemarks.SelectedValue = "0";
                    rdoMgrRemarks.SelectedItem.Attributes.Add("style", "color:red;font-weight:bold");
                }


                if (dsadmin.Tables[0].Rows[0].ItemArray.GetValue(1).ToString() == "1")
                {
                    rdoMgrRow.SelectedValue = "1";
                    rdoMgrRow.SelectedItem.Attributes.Add("style", "color:red;font-weight:bold");
                }
                else if (dsadmin.Tables[0].Rows[0].ItemArray.GetValue(1).ToString() == "0")
                {
                    rdoMgrRow.SelectedValue = "0";
                    rdoMgrRow.SelectedItem.Attributes.Add("style", "color:red;font-weight:bold");
                }

                if (dsadmin.Tables[0].Rows[0].ItemArray.GetValue(2).ToString() == "M")
                {
                    rdoEx_Subm.SelectedValue = "M";
                    rdoEx_Subm.SelectedItem.Attributes.Add("style", "color:red;font-weight:bold");
                    lblstarfrom.Visible = false;
                    ddlperiodRange.Visible = false;
                }
                else if (dsadmin.Tables[0].Rows[0].ItemArray.GetValue(2).ToString() == "F")
                {
                    rdoEx_Subm.SelectedValue = "F";
                    rdoEx_Subm.SelectedItem.Attributes.Add("style", "color:red;font-weight:bold");
                    lblstarfrom.Visible = false;
                    ddlperiodRange.Visible = false;
                }

                else if (dsadmin.Tables[0].Rows[0].ItemArray.GetValue(2).ToString() == "P")
                {
                    rdoEx_Subm.SelectedValue = "P";
                    rdoEx_Subm.SelectedItem.Attributes.Add("style", "color:red;font-weight:bold");
                    lblstarfrom.Visible = true;
                    ddlperiodRange.Visible = true;
                    ddlperiodRange.SelectedValue = dsadmin.Tables[0].Rows[0].ItemArray.GetValue(3).ToString();
                }


                if (dsadmin.Tables[0].Rows[0].ItemArray.GetValue(4).ToString() == "OS")
                {
                    rdoLast_OS.SelectedValue = "OS";
                    rdoLast_OS.SelectedItem.Attributes.Add("style", "color:red;font-weight:bold");
                }
                else if (dsadmin.Tables[0].Rows[0].ItemArray.GetValue(4).ToString() == "EX")
                {
                    rdoLast_OS.SelectedValue = "EX";
                    rdoLast_OS.SelectedItem.Attributes.Add("style", "color:red;font-weight:bold");
                }
                if (dsadmin.Tables[0].Rows[0].ItemArray.GetValue(10).ToString() == "P")
                {
                    RdoPackage.SelectedValue = "P";
                    RdoPackage.SelectedItem.Attributes.Add("style", "color:red;font-weight:bold");
                }
                else if (dsadmin.Tables[0].Rows[0].ItemArray.GetValue(10).ToString() == "R")
                {
                    RdoPackage.SelectedValue = "R";
                    RdoPackage.SelectedItem.Attributes.Add("style", "color:red;font-weight:bold");
                }
                ddlStart_date.SelectedValue = dsadmin.Tables[0].Rows[0].ItemArray.GetValue(5).ToString();
                ddlEnd_date.SelectedValue = dsadmin.Tables[0].Rows[0].ItemArray.GetValue(6).ToString();

                if (dsadmin.Tables[0].Rows[0].ItemArray.GetValue(7).ToString() == "HQ")
                {
                    rdofieldforceHQ.Font.Bold = true;
                    rdofieldforceHQ.Checked = true;
                    rdofieldforceHQ.ForeColor = System.Drawing.Color.Red;
                }
                else if (dsadmin.Tables[0].Rows[0].ItemArray.GetValue(7).ToString() == "EX")
                {
                    rdofieldforceEX.Font.Bold = true;
                    rdofieldforceEX.Checked = true;
                    rdofieldforceEX.ForeColor = System.Drawing.Color.Red;
                }

                else if (dsadmin.Tables[0].Rows[0].ItemArray.GetValue(7).ToString() == "MHQ")
                {
                    rdoMax_callsMHQ.Font.Bold = true;
                    rdoMax_callsMHQ.Checked = true;
                    rdoMax_callsMHQ.ForeColor = System.Drawing.Color.Red;
                }
                else if (dsadmin.Tables[0].Rows[0].ItemArray.GetValue(7).ToString() == "MHQF")
                {
                    rdoMax_callsMHQF.Font.Bold = true;
                    rdoMax_callsMHQF.Checked = true;
                    rdoMax_callsMHQF.ForeColor = System.Drawing.Color.Red;
                }

                else if (dsadmin.Tables[0].Rows[0].ItemArray.GetValue(7).ToString() == "MEXF")
                {
                    rdoMax_callsMEXF.Font.Bold = true;
                    rdoMax_callsMEXF.Checked = true;
                    rdoMax_callsMEXF.ForeColor = System.Drawing.Color.Red;
                }
                else
                {
                    rdomaxcalls.Font.Bold = true;
                    rdomaxcalls.ForeColor = System.Drawing.Color.Red;
                    rdomaxcalls.Checked = true;
                    txtminimum.Visible = true;
                    txtminimum.Text = dsadmin.Tables[0].Rows[0].ItemArray.GetValue(8).ToString();
                }

                if (dsadmin.Tables[0].Rows[0].ItemArray.GetValue(9).ToString() == "1")
                {
                    rdoadmin.SelectedValue = "1";
                }
                else if (dsadmin.Tables[0].Rows[0].ItemArray.GetValue(9).ToString() == "0")
                {
                    rdoadmin.SelectedValue = "0";
                    rdoadmin.SelectedItem.Attributes.Add("style", "color:red;font-weight:bold");
                }
                if (dsadmin.Tables[0].Rows[0].ItemArray.GetValue(11).ToString() == "Y")
                {
                    rdoRow_wise.SelectedValue = "Y";
                    rdoRow_wise.SelectedItem.Attributes.Add("style", "color:red;font-weight:bold");
                }
                else
                {
                    rdoRow_wise.SelectedValue = "N";
                    rdoRow_wise.SelectedItem.Attributes.Add("style", "color:red;font-weight:bold");
                }
                if (dsadmin.Tables[0].Rows[0].ItemArray.GetValue(12).ToString() == "OS")
                {
                    rdoSingle_OS.SelectedValue = "OS";
                    rdoSingle_OS.SelectedItem.Attributes.Add("style", "color:red;font-weight:bold");
                }
                else if (dsadmin.Tables[0].Rows[0].ItemArray.GetValue(12).ToString() == "EX")
                {
                    rdoSingle_OS.SelectedValue = "EX";
                    rdoSingle_OS.SelectedItem.Attributes.Add("style", "color:red;font-weight:bold");
                }
            }
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        int iReturn = -1;
        AdminSetup dv = new AdminSetup();
        Distance_calculation dist = new Distance_calculation();
        if (rdoEx_Subm.SelectedValue != "P")
        {
            startfrom_periodically = "";
        }
        else
        {
            startfrom_periodically = ddlperiodRange.SelectedValue;
        }

        if (rdofieldforceHQ.Checked)
        {
            Fieldforce_HQ_Ex_Max = "HQ";
        }
        else if (rdofieldforceEX.Checked)
        {
            Fieldforce_HQ_Ex_Max = "EX";
        }
        else if (rdoMax_callsMHQ.Checked)
        {
            Fieldforce_HQ_Ex_Max = "MHQ";
        }
        else if (rdoMax_callsMHQF.Checked)
        {
            Fieldforce_HQ_Ex_Max = "MHQF";
        }
        else if (rdoMax_callsMEXF.Checked)
        {
            Fieldforce_HQ_Ex_Max = "MEXF";
        }

        if (rdomaxcalls.Checked)
        {
            txtmini = txtminimum.Text;
        }
        else
        {
            txtmini = "";
        }

        iReturn = dv.Expense_Setup(div_code, rdoMgrRemarks.SelectedValue, rdoMgrRow.SelectedValue, rdoEx_Subm.SelectedValue, startfrom_periodically, rdoLast_OS.SelectedValue, ddlStart_date.SelectedValue, ddlEnd_date.SelectedValue, Fieldforce_HQ_Ex_Max, txtmini, rdoadmin.SelectedValue, RdoPackage.SelectedValue, rdoRow_wise.SelectedValue, rdoSingle_OS.SelectedValue);
        div_code = Session["div_code"].ToString();

        HiddenField otherExpValues = (HiddenField)FindControl("otherExpValues");
        iReturn = dist.deleteOthMgrExpSetupRecord(div_code);
        string[] splitVal = otherExpValues.Value.Split('~');


        string[] desig = splitVal[0].Split(',');
        string[] dropMode = splitVal[1].Split(',');
        for (int p = 0; p < desig.Length; p++)
        {

            string[] e1 = desig[p].Split('=');
            string[] e2 = dropMode[p].Split('=');
            iReturn = dist.addOthMgrExpSetupRecord(e2[0], e2[1], e1[0], e1[1], div_code);
        }

        if (iReturn > 0)
        {
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>createCustomAlert('Setup has been updated Successfully');</script>");

        }
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {

    }
    protected void rdoEx_Subm_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rdoEx_Subm.SelectedValue == "P")
        {
            ddlperiodRange.Visible = true;
            lblstarfrom.Visible = true;
        }
        else
        {
            ddlperiodRange.Visible = false;
            lblstarfrom.Visible = false;
        }
    }
    protected void rdomaxcalls_CheckedChanged(object sender, EventArgs e)
    {
        if (rdomaxcalls.Checked)
        {
            txtminimum.Visible = true;
        }
        else
        {
            txtminimum.Visible = false;
        }
    }
    private void generateOtherExpListData(DataTable otherExpTable)
    {
        if (otherExpTable.Rows.Count > 0)
        {
            foreach (DataRow row in otherExpTable.Rows)
            {
                ListItem list = new ListItem();
                list.Text = row["Designation_Short_Name"].ToString();
                list.Value = row["designation_code"].ToString();
                desig.Items.Add(list);
            }
            //otherExp.Items.Insert(0, new ListItem("  --Select--  ", "0"));
            //otherExp.DataTextField = "Expense_Parameter_Name";
        }
    }

    private void generateOtherExpControls(Distance_calculation dist)
    {
        div_code = Session["div_code"].ToString();
        DataTable t2 = dist.getMgrExpsetupDesignation(div_code);
        HtmlTable htmlTable = (HtmlTable)FindControl("tableId");
        DataTable otherExp1 = dist.getDesigExp(div_code);

        for (int p = htmlTable.Rows.Count - 1; p > 0; p--)
        {
            if (t2.Rows.Count > 0)
                htmlTable.Rows.RemoveAt(p);
            else
            {
                generateOtherExpListData(otherExp1);
            }

        }
        for (int i = 0; i < t2.Rows.Count; i++)
        {

            HtmlTableRow r = new HtmlTableRow();
            DropDownList d = new DropDownList();
            d.ID = "desig_" + i;
            d.CssClass = "desig";
            if (otherExp1.Rows.Count > 0)
            {
                foreach (DataRow row in otherExp1.Rows)
                {
                    ListItem list = new ListItem();
                    list.Text = row["Designation_Short_Name"].ToString();
                    list.Value = row["Designation_Code"].ToString();
                    d.Items.Add(list);


                }
                d.Items.Insert(0, new ListItem("--Select--", "0"));


            }
            d.Text = t2.Rows[i]["Designation_Code"].ToString();
            d.Items.FindByText(t2.Rows[i]["Designation_Short_Name"].ToString()).Selected = true;
            HtmlTableCell cell1 = new HtmlTableCell();
            cell1.Controls.Add(d);
            r.Cells.Add(cell1);
            DropDownList m = new DropDownList();
            m.ID = "dropMode_" + i;
            m.CssClass = "dropMode";
            m.Items.Insert(0, new System.Web.UI.WebControls.ListItem(" --Select-- ", "0"));
            m.Items.Insert(1, new System.Web.UI.WebControls.ListItem("Automatic", "A"));
            m.Items.Insert(1, new System.Web.UI.WebControls.ListItem("Semi-Automatic", "SA"));
            m.Items.Insert(2, new System.Web.UI.WebControls.ListItem("Manual", "M"));
            m.Text = t2.Rows[i]["Designation_Mode"].ToString();
            m.Items.FindByText(t2.Rows[i]["Designation_Name"].ToString()).Selected = true;
            HtmlTableCell cell2 = new HtmlTableCell();
            cell2.Controls.Add(m);
            r.Cells.Add(cell2);
            HtmlTableCell cell4 = new HtmlTableCell();
            Button b1 = new Button();
            Literal lit = new Literal();
            lit.Text = @"<input type='button' id='btnadd' value=' + ' class='btnSave' onclick='_AdRowByCurrElem(this)' />";
            HtmlTable table = new HtmlTable();
            System.Text.StringBuilder sb = new System.Text.StringBuilder("");
            System.IO.StringWriter tw = new System.IO.StringWriter(sb);
            HtmlTextWriter hw = new HtmlTextWriter(tw);
            sb = new System.Text.StringBuilder("");
            tw = new System.IO.StringWriter(sb);
            hw = new HtmlTextWriter(tw);
            b1.RenderControl(hw);
            cell4.Controls.Add(lit);
            r.Cells.Add(cell4);
            HtmlTableCell cell5 = new HtmlTableCell();
            Button b2 = new Button();
            lit = new Literal();
            lit.Text = @"<input type='button' id='btndel' value=' - ' class='btnSave' onclick='DRForOthExp(this,this.parentNode.parentNode,1)' />";
            sb = new System.Text.StringBuilder("");
            tw = new System.IO.StringWriter(sb);
            hw = new HtmlTextWriter(tw);
            b2.RenderControl(hw);
            cell5.Controls.Add(lit);
            r.Cells.Add(cell5);
            htmlTable.Rows.Add(r);
        }
    }
}