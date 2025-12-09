using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using Bus_EReport;
public partial class MasterFiles_Subdiv_Salesforcewise : System.Web.UI.Page
{
    #region "Declaration"
    DataSet dsSubDivision = null;
    DataSet dswrktyp = null;
    int subdivcode = 0;
    int subdivision_code = 0;
    string divcode = string.Empty;
    string sfcode = string.Empty;
    string subdiv_sname = string.Empty;
    string subdiv_name = string.Empty;
    DateTime ServerStartTime;
    DateTime ServerEndTime;
    int time;
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["sf_type"].ToString() == "1")
        {
            sfcode = Session["sf_code"].ToString();
            UserControl_MR_Menu Usc_MR =
            (UserControl_MR_Menu)LoadControl("~/UserControl/MR_Menu.ascx");
            Divid.Controls.Add(Usc_MR);
            Usc_MR.Title = this.Page.Title;
            Usc_MR.FindControl("btnBack").Visible = false;
            //lblTerrritory.Text = "<span style='font-weight: bold;color:Blue;font-names:verdana'>HQ :</span>" +
            //                   "<span style='font-weight: bold;color:Red'>  " + Session["sf_HQ"] + "</span>";
            //btnBack.Visible = true;



        }
        else if (Session["sf_type"].ToString() == "2")
        {
            sfcode = Session["sf_code"].ToString();
            UserControl_MGR_Menu c1 =
             (UserControl_MGR_Menu)LoadControl("~/UserControl/MGR_Menu.ascx");
            Divid.Controls.Add(c1);
            c1.FindControl("btnBack").Visible = false;
            //btnBack.Visible = true;
            c1.Title = this.Page.Title;
            //   Session["backurl"] = "LstDoctorList.aspx";
            //lblTerrritory.Text = "<span style='font-weight: bold;color:Blue;font-names:verdana'>HQ :</span>" +
            //                   "<span style='font-weight: bold;color:Red'>  " + Session["sf_HQ"] + "</span>";

        }
        else
        {
            UserControl_MenuUserControl c1 =
            (UserControl_MenuUserControl)LoadControl("~/UserControl/MenuUserControl.ascx");
            Divid.Controls.Add(c1);
            c1.FindControl("btnBack").Visible = false;
            c1.Title = this.Page.Title;


        }
        if (Request.QueryString["sfCode"] != null)
        {
            Distance_calculation_001 Exp = new Distance_calculation_001();
            sfcode = Request.QueryString["sfCode"].ToString();
            divcode = Request.QueryString["divCode"].ToString();
            DataTable ds = Exp.getFieldForce(divcode, sfcode);
            hqId.InnerText = ds.Rows[0]["sf_hq"].ToString();

            populateGriddata(false);
            mainDiv.Visible = false;
            divHqId.Visible = true;
        }
        else
        {
            divcode = Convert.ToString(Session["div_code"]);
            sfcode = Convert.ToString(Session["Sf_Code"]);
            if (!Page.IsPostBack)
            {
                ServerStartTime = DateTime.Now;
                base.OnPreInit(e);
                //Divid.Title = this.Page.Title;
                //Divid.FindControl("btnBack").Visible = false;
                //FillFieldForcediv(divcode);
                ddlSubdiv.Focus();
            }

        }
        FillColor();
    }


    private void FillFieldForcediv(string divcode)
    {
        SalesForce dv = new SalesForce();
        if (Session["sf_type"].ToString() == "1")
        {
            dsSubDivision = dv.sp_UserMRLogin_With_Vacant_SFC(divcode, sfcode);
        }

        else if (Session["sf_type"].ToString() == "2")
        {
            dsSubDivision = dv.AllFieldforce_withVacant_SFC(divcode, sfcode);
        }
        else
        {
            //dsSubDivision = dv.SalesForceListMgrGet_Mail(divcode, sfcode);
            dsSubDivision = dv.AllFieldforce_withVacant_SFC(divcode, sfcode);
        }
        if (dsSubDivision.Tables[0].Rows.Count > 0)
        {
            if (Session["sf_type"].ToString() == "3")
            {
                dsSubDivision.Tables[0].Rows[0].Delete();
                //dsSubDivision.Tables[0].Rows[1].Delete();
            }

            ddlSubdiv.DataTextField = "sf_name_hq";
            ddlSubdiv.DataValueField = "sf_code";
            ddlSubdiv.DataSource = dsSubDivision;
            ddlSubdiv.DataBind();

            ddlSF.DataTextField = "des_Color";
            ddlSF.DataValueField = "sf_code";
            ddlSF.DataSource = dsSubDivision;
            ddlSF.DataBind();
        }
    }

    private void FillColor()
    {
        int j = 0;


        foreach (ListItem ColorItems in ddlSF.Items)
        {
            string bcolor = "#" + ColorItems.Text;
            ddlSubdiv.Items[j].Attributes.Add("style", "background-color:" + bcolor);
            j = j + 1;

        }
    }
    protected void btnSF_Click(object sender, EventArgs e)
    {
        populateGriddata(true);
    }

    private void populateGriddata(bool flag)
    {
        string sf_code = "";
        if (flag)
        {
            sf_code = ddlSubdiv.SelectedValue.ToString();
        }
        else
        {
            sf_code = sfcode;
        }
        Distance_calculation_001 dv = new Distance_calculation_001();
        if (sf_code.Contains("MGR"))
        {
            dsSubDivision = dv.sfcviewnew_MGR(sf_code);
            //  dsSubDivision = dv.sfcviewnew(sf_code);
            dswrktyp = dv.sfcwrktyp(sf_code);
            Gridwrktyp.Visible = true;
        }
        else
        {
            dsSubDivision = dv.sfcviewnew(sf_code);
        }
        DataSet dsExpFare = new DataSet();

        dsExpFare = dv.Expense_Fixed_MR(sf_code);
        if (dsSubDivision.Tables[0].Rows.Count > 0)
        {
            pnlprint.Visible = true;
            grdSalesForce.Visible = true;
            grdSalesForce.DataSource = dsSubDivision;
            grdSalesForce.DataBind();


            if (dsExpFare.Tables[0].Rows.Count > 0)
            {
                lblHQ.Visible = true;
                lblEX.Visible = true;
                lblEXOS.Visible = true;
                lblFareKM.Visible = true;
                lblHQ.Text = "HQ Allowance :" + "<span style='color:Red;'>" + dsExpFare.Tables[0].Rows[0]["HQ_Allowance"].ToString() + "</span>";
                lblEX.Text = "EX Allowance :" + "<span style='color:Red;'>" + dsExpFare.Tables[0].Rows[0]["EX_HQ_Allowance"].ToString() + "</span>";
                lblEXOS.Text = "OS-EX Allowance :" + "<span style='color:Red;'>" + dsExpFare.Tables[0].Rows[0]["OS_Allowance"].ToString() + "</span>";
                lblFareKM.Text = "Fare/KM Allowance :" + "<span style='color:Red;'>" + dsExpFare.Tables[0].Rows[0]["FareKm_Allowance"].ToString() + "</span>";
            }

        }
        else
        {
            grdSalesForce.DataSource = dsSubDivision;
            grdSalesForce.DataBind();
        }
        if (sf_code.Contains("MGR"))
        {

            if (dswrktyp.Tables[0].Rows.Count > 0)
            {

                Gridwrktyp.DataSource = dswrktyp;
                Gridwrktyp.DataBind();



            }

        }
        DataTable customExpTable = new DataTable();
        customExpTable.Columns.Add("Expense_Parameter_Name");
        customExpTable.Columns.Add("amount");
        customExpTable.Columns.Add("Expense_Parameter_Code");
        Distance_calculation Exp = new Distance_calculation();
        DataTable expParamsAmnt = Exp.getExpParamAmt(sf_code, divcode);
        double otherExAmnt = 0;
        if (expParamsAmnt.Rows.Count > 0)
        {
            for (int i = 0; i < expParamsAmnt.Rows.Count; i++)
            {
                string colName = "Fixed_Column" + (i + 1);
                if (expParamsAmnt.Rows[i][colName].ToString() != "")
                {
                    otherExAmnt = otherExAmnt + Convert.ToDouble(expParamsAmnt.Rows[i][colName].ToString());
                }
                customExpTable.Rows.Add();

                customExpTable.Rows[customExpTable.Rows.Count - 1]["Expense_Parameter_Name"] = expParamsAmnt.Rows[i]["Expense_Parameter_Name"];
                customExpTable.Rows[customExpTable.Rows.Count - 1]["Expense_Parameter_Code"] = expParamsAmnt.Rows[i]["Expense_Parameter_Code"];
                customExpTable.Rows[customExpTable.Rows.Count - 1]["amount"] = expParamsAmnt.Rows[i][colName] == "" ? "0" : expParamsAmnt.Rows[i][colName];

            }
            //lblFixedExp.Visible = true;
            gvExpense.DataSource = customExpTable;
            gvExpense.DataBind();
        }


        dsExpFare = dv.Expense_Fixed_Variable(divcode);
        if (dsExpFare.Tables[0].Rows.Count > 0)
        {
            //lblFixedExp.Visible = true;
            //gvExpense.DataSource = dsExpFare;
            //gvExpense.DataBind();
        }
    }

    protected void linkcheck_Click(object sender, EventArgs e)
    {


        FillFieldForcediv(divcode);

        ddlSubdiv.Visible = true;
        linkcheck.Visible = false;
        txtNew.Visible = true;
        btnSF.Visible = true;
        FillColor();
    }

}