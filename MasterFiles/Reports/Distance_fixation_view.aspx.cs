using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using Bus_EReport;
using System.Data.SqlClient;
using DBase_EReport;

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
    DataTable dtrowClr = new System.Data.DataTable();
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
            //c1.FindControl("btnBack").Visible = false;
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
            //c1.FindControl("btnBack").Visible = false;
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
           // mainDiv.Visible = false;
            Divid.Visible = false;
            divHqId.Visible = true;
            pnlprint.Visible = false;
            lblSubdiv.Visible = false;
            ddlSubdiv.Visible = false;
            btnSF.Visible = false;
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
                FillFieldForcediv(divcode);
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
        FillReport();
        


    }



    #region Salesforce 
    private void FillReport()
    {
        // int months = (Convert.ToInt32(TYear) - Convert.ToInt32(FYear)) * 12 + Convert.ToInt32(TMonth) - Convert.ToInt32(FMonth); //(Date2.Year - Date1.Year) * 12 + Date2.Month - Date1.Month;


        SalesForce sf = new SalesForce();
        DCR dcc = new DCR();
        DB_EReporting db = new DB_EReporting();
        string strConn = System.Configuration.ConfigurationManager.ConnectionStrings["Ereportcon"].ToString();
        SqlConnection con = new SqlConnection(strConn);
        con.Open();
        string sProc_Name = "";
        Distance_calculation_001 dv = new Distance_calculation_001();
        
        if ((ddlSubdiv.SelectedValue.ToString()).Contains("MGR"))
        {
            sProc_Name = "sfcviewnew_MGR_CatDr";
            //dsSubDivision = dv.sfcviewnew_MGR(sf_code);
            dswrktyp = dv.sfcwrktyp(ddlSubdiv.SelectedValue.ToString());
            Gridwrktyp.Visible = true;
        }
        else
        {
            sProc_Name = "sfcviewnew_CatDr_BKP";//sfcviewnew_CatDr
            //dsSubDivision = dv.sfcviewnew(sf_code);
            Gridwrktyp.Visible = false;
        }
        //string sqry = "EXEC visit_fixation_Cat " + Convert.ToInt32(div_code) + ",'" + ddlFieldForce.SelectedValue.ToString() + "',"+dtMnYr+"";
        SqlCommand cmd = new SqlCommand(sProc_Name, con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Div_Code", Convert.ToInt32(divcode));
        if (Session["sf_type"].ToString() == "1")
        {
            cmd.Parameters.AddWithValue("@sf_code",sfcode);
        }
        else
        {
            cmd.Parameters.AddWithValue("@sf_code", ddlSubdiv.SelectedValue.ToString());
        }

        cmd.CommandTimeout = 600;
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet dsts = new DataSet();
        da.Fill(dsts);
        con.Close();
        DataSet dsExpFare = new DataSet();
        
        dsExpFare = dv.Expense_Fixed_MR(ddlSubdiv.SelectedValue.ToString());
        if (dsts.Tables[0].Rows.Count > 0)
        {
            dtrowClr = dsts.Tables[0].Copy();
            dsts.Tables[0].Columns.Remove("Sf_Code");
            dsts.Tables[0].Columns.Remove("Territory_code");
            
            grdSalesForce1.DataSource = dsts;
            grdSalesForce1.DataBind();

            pnlprint.Visible = true;
            //grdSalesForce.Visible = true;
            //grdSalesForce.DataSource = dsSubDivision;
            //grdSalesForce.DataBind();

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
            grdSalesForce1.DataSource = dsts;
            grdSalesForce1.DataBind();
        }

        if ((ddlSubdiv.SelectedValue.ToString()).Contains("MGR"))
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
        DataTable expParamsAmnt = Exp.getExpParamAmt(ddlSubdiv.SelectedValue.ToString(), divcode);
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
                customExpTable.Rows[customExpTable.Rows.Count - 1]["amount"] = expParamsAmnt.Rows[i][colName].ToString()== "" ? "0" : expParamsAmnt.Rows[i][colName];

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

    protected void grdSalesForce1_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            int indx = e.Row.RowIndex;
            int k = e.Row.Cells.Count - 5;
            //
            #region Calculations
            e.Row.Font.Size = 8;
            e.Row.Style.Add("border", "1px solid #99ADA5");
            for (int i = 9, j = 0; i < e.Row.Cells.Count; i++)
            {
                if (e.Row.Cells[i].Text != "0" && e.Row.Cells[i].Text != "-")
                {
                }
                else if (e.Row.Cells[i].Text == "0")
                {
                    if (e.Row.Cells[i].Text == "0")
                    {
                        e.Row.Cells[i].Text = "-";
                        e.Row.Cells[i].Attributes.Add("style", "color:black;font-weight:normal;");
                    }
                }
                e.Row.Cells[i].Attributes.Add("align", "center");
            }
            try
            {
                int j = Convert.ToInt32(e.Row.Cells[0].Text) - 1;
                string backcolor = Convert.ToString(dtrowClr.Rows[j][7].ToString()) == "DEFFBE" ? "A9FFCA" : Convert.ToString(dtrowClr.Rows[j][7].ToString());

                e.Row.Attributes.Add("style", "background-color:" + "#" + backcolor);
            }
            catch
            {
                e.Row.Attributes.Add("style", "background-color:" + "#FFFFFF");
            }

            #endregion
            //
            //e.Row.Cells[1].Wrap = false;
            //e.Row.Cells[2].Wrap = false;
            //e.Row.Cells[3].Wrap = false;
            //e.Row.Cells[4].Wrap = false;
            //e.Row.Cells[5].Wrap = false;
            //e.Row.Cells[6].Wrap = false;
            //e.Row.Cells[7].Wrap = false;
            //e.Row.Cells[11].BackColor = System.Drawing.Color.Yellow;
            //e.Row.Cells[17].BackColor = System.Drawing.Color.Yellow;
            //e.Row.Cells[18].BackColor = System.Drawing.Color.Yellow;x
            //e.Row.Cells[19].BackColor = System.Drawing.Color.Yellow;
            //e.Row.Cells[25].BackColor = System.Drawing.Color.Yellow;
            //e.Row.Cells[26].BackColor = System.Drawing.Color.Yellow;
            //e.Row.Cells[27].BackColor = System.Drawing.Color.Yellow;
            //e.Row.Cells[33].BackColor = System.Drawing.Color.Yellow;
            //e.Row.Cells[34].BackColor = System.Drawing.Color.Yellow;
        }
    }
    protected void grdSalesForce1_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            //Creating a gridview object            
            GridView objGridView = (GridView)sender;

            //Creating a gridview row object
            GridViewRow objgridviewrow = new GridViewRow(1, 0, DataControlRowType.Header, DataControlRowState.Insert);

            //Creating a table cell object
            TableCell objtablecell = new TableCell();
            GridViewRow objgridviewrow2 = new GridViewRow(2, 0, DataControlRowType.Header, DataControlRowState.Insert);
            TableCell objtablecell2 = new TableCell();
            #region Merge cells

            DB_EReporting db = new DB_EReporting();
            string strQry = "";
            string strQry1 = "";

            strQry = " select distinct Doc_Cat_Code,Doc_Cat_SName,Doc_Cat_Name,Doc_Cat_sl_no from Mas_Doctor_Category where Doc_Cat_Active_Flag=0 and division_code='" + divcode + "' order by Doc_Cat_Code";
            DataSet dsDoctor = db.Exec_DataSet(strQry);


            AddMergedCells(objgridviewrow, objtablecell, 2, 0, "#", "#0097AC", true);
            AddMergedCells(objgridviewrow, objtablecell, 2, 0, "Emp.Code", "#0097AC", true);
            AddMergedCells(objgridviewrow, objtablecell, 2, 0, "FieldForce Name", "#0097AC", true);
            AddMergedCells(objgridviewrow, objtablecell, 2, 0, "HQ", "#0097AC", true);
            AddMergedCells(objgridviewrow, objtablecell, 2, 0, "Desig", "#0097AC", true);
            //AddMergedCells(objgridviewrow, objtablecell, 3, 0, "First Level Manager", "#0097AC", true);
            //AddMergedCells(objgridviewrow, objtablecell, 3, 0, "Second Level Manager", "#0097AC", true);

            AddMergedCells(objgridviewrow, objtablecell, 2, 0, "From", "#0097AC", true);
            AddMergedCells(objgridviewrow, objtablecell, 2, 0, "From<br/>Category", "#0097AC", true);
            AddMergedCells(objgridviewrow, objtablecell, 2, 0, "To", "#0097AC", true);
            AddMergedCells(objgridviewrow, objtablecell, 2, 0, "To<br/>Category", "#0097AC", true);
            AddMergedCells(objgridviewrow, objtablecell, 2, 0, "Distance", "#0097AC", true);
            AddMergedCells(objgridviewrow, objtablecell, 2, 0, "Visit", "#0097AC", true);

            AddMergedCells(objgridviewrow, objtablecell, 0, (dsDoctor.Tables[0].Rows.Count), "Category", "#0097AC", true);
            foreach (DataRow dtRow in dsDoctor.Tables[0].Rows)
            {
                AddMergedCells(objgridviewrow2, objtablecell2, 0, 0, dtRow["Doc_Cat_SName"].ToString(), "#0097AC", false);
            }

            objGridView.Controls[0].Controls.AddAt(0, objgridviewrow);
            objGridView.Controls[0].Controls.AddAt(1, objgridviewrow2);
            #endregion
        }
    }
    protected void AddMergedCells(GridViewRow objgridviewrow, TableCell objtablecell, int rowSpan, int colspan, string celltext, string backcolor, bool bRowspan)
    {
        objtablecell = new TableCell();
        objtablecell.Text = celltext;
        objtablecell.ColumnSpan = colspan;
        objtablecell.RowSpan = rowSpan;
        objtablecell.Font.Size = 8;
        objtablecell.Style.Add("background-color", "white");
        //objtablecell.Style.Add("Fore-color", "white");
        objtablecell.Style.Add("color", "#636d73");
        //objtablecell.Style.Add("border-color", "black");
        //objtablecell.Style.Add("border", "1px solid #99ADA5");
        objtablecell.HorizontalAlign = HorizontalAlign.Center;
        objtablecell.VerticalAlign = VerticalAlign.Middle;
        objtablecell.Wrap = true;
        objgridviewrow.Cells.Add(objtablecell);
    }
    #endregion

    //protected void linkcheck_Click(object sender, EventArgs e)
    //   {


    //           FillFieldForcediv(divcode);

    //       ddlSubdiv.Visible = true;
    //       linkcheck.Visible = false;
    //       txtNew.Visible = true;
    //       btnSF.Visible = true;
    //       FillColor();
    //   }

}