using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using Bus_EReport;
using DBase_EReport;
using System.Web.Script.Serialization;

public partial class MasterFiles_AllowanceFixation_NEW : System.Web.UI.Page
{

    string div_code = string.Empty;
    string sfCode = string.Empty;
    SalesForce sf = new SalesForce();
    DataSet dsExp = new DataSet();
    DataSet dsSFMR = new DataSet();
    TextBox TextBox1 = new TextBox();
    static DataTable Sdt = new DataTable();
    static DataTable dt = new DataTable();
    static DataTable wrkType_dt = new DataTable();

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            div_code = Session["div_code"].ToString();
            sfCode = Session["sf_code"].ToString();
            //btnSave.Visible = false; 
            string str = txtTPStartDate.Text;
            if (!Page.IsPostBack)
            {
                menu1.Title = Page.Title;
                //menu1.FindControl("btnBack").Visible = false;
                FillReporting();
                filldesg();
                //BindSelectedValue();
                //txtFix_Date.Text = DateTime.Now.Date.ToShortDateString();
                //btnSave.Visible = false; 


            }

            //BindDate();
        }
        catch (Exception ex)
        {

        }
    }

    private void FillReporting()
    {
        Territory sf = new Territory();
        DataSet dsSalesForce = new DataSet();
        dsSalesForce = sf.getExp_Managers(div_code);
        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            //dsSalesForce.Tables[0].Rows[0].Delete();
            //dsSalesForce.Tables[0].Rows[1].Delete();
            ddlRegion.DataTextField = "Sf_Name";
            ddlRegion.DataValueField = "Sf_Code";
            ddlRegion.DataSource = dsSalesForce;
            ddlRegion.DataBind();

        }

    }
    protected void ddlRegion_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            BindSelectedValue();
        }
        catch (Exception ex)
        {

        }
    }

    private void BindSelectedValue()
    {

        Territory terr = new Territory();
        grdWTAllowance.DataSource = null;
        grdWTAllowance.DataBind();

        dsSFMR = sf.GetExp_AllwanceFixation(div_code, ddlRegion.SelectedValue);

        if (dsSFMR.Tables[0].Rows.Count > 0)
        {
            //grdWTAllowance.DataSource = dsSFMR;
            //grdWTAllowance.DataBind();
        }
        else
        {
            dsSFMR = sf.SalesForceListMgrGet(div_code, ddlRegion.SelectedValue);

            if (dsSFMR.Tables[0].Rows.Count > 0)
            {
                //grdWTAllowance.DataSource = dsSFMR;
                //grdWTAllowance.DataBind();
                btnSave.Visible = true;
                txtTPStartDate.Text = DateTime.Now.Date.ToShortDateString();



            }
        }
        dsExp = terr.getWorkType_allow_type(div_code);
        if (dsExp.Tables[0].Rows.Count > 0)
        {
            wrkType_dt = dsExp.Tables[0];
            dsSFMR.Tables[0].Merge(wrkType_dt);

            for (int i = 0; i < wrkType_dt.Columns.Count; i++)
            {

                grdWTAllowance.Columns[22 + i].Visible = true;
                grdWTAllowance.Columns[22 + i].HeaderText = wrkType_dt.Columns[i].ColumnName;
            }
            /*if (wrkType_dt.Columns.Count == 1)
            {
                grdWTAllowance.Columns[18].Visible = true;
                grdWTAllowance.Columns[18].HeaderText = wrkType_dt.Columns[0].ColumnName;
            }
            else if (wrkType_dt.Columns.Count == 2)
            {
                grdWTAllowance.Columns[18].Visible = true;
                grdWTAllowance.Columns[19].Visible = true;
                grdWTAllowance.Columns[18].HeaderText = wrkType_dt.Columns[0].ColumnName;
                grdWTAllowance.Columns[19].HeaderText = wrkType_dt.Columns[1].ColumnName;

            }
            else if (wrkType_dt.Columns.Count == 3)
            {
                grdWTAllowance.Columns[18].Visible = true;
                grdWTAllowance.Columns[19].Visible = true;
                grdWTAllowance.Columns[20].Visible = true;
                grdWTAllowance.Columns[18].HeaderText = wrkType_dt.Columns[0].ColumnName;
                grdWTAllowance.Columns[19].HeaderText = wrkType_dt.Columns[1].ColumnName;
                grdWTAllowance.Columns[20].HeaderText = wrkType_dt.Columns[2].ColumnName;
            }

            else if (wrkType_dt.Columns.Count == 4)
            {
                grdWTAllowance.Columns[18].Visible = true;
                grdWTAllowance.Columns[19].Visible = true;
                grdWTAllowance.Columns[20].Visible = true;
                grdWTAllowance.Columns[21].Visible = true;
                grdWTAllowance.Columns[18].HeaderText = wrkType_dt.Columns[0].ColumnName;
                grdWTAllowance.Columns[19].HeaderText = wrkType_dt.Columns[1].ColumnName;
                grdWTAllowance.Columns[20].HeaderText = wrkType_dt.Columns[2].ColumnName;
                grdWTAllowance.Columns[21].HeaderText = wrkType_dt.Columns[3].ColumnName;
            }

            else if (wrkType_dt.Columns.Count == 5)
            {
                grdWTAllowance.Columns[18].Visible = true;
                grdWTAllowance.Columns[19].Visible = true;
                grdWTAllowance.Columns[20].Visible = true;
                grdWTAllowance.Columns[21].Visible = true;
                grdWTAllowance.Columns[22].Visible = true;
                grdWTAllowance.Columns[18].HeaderText = wrkType_dt.Columns[0].ColumnName;
                grdWTAllowance.Columns[19].HeaderText = wrkType_dt.Columns[1].ColumnName;
                grdWTAllowance.Columns[20].HeaderText = wrkType_dt.Columns[2].ColumnName;
                grdWTAllowance.Columns[21].HeaderText = wrkType_dt.Columns[3].ColumnName;
                grdWTAllowance.Columns[22].HeaderText = wrkType_dt.Columns[4].ColumnName;
            }*/

        }

        dsExp = terr.getExp_FixedType1(div_code);
        string strTextbox = string.Empty;
        DataRow dr = null;
        if (dsExp.Tables[0].Rows.Count > 0)
        {
            dt = dsExp.Tables[0];
            dsSFMR.Tables[0].Merge(dt);
            for (int i = 0, j = 0; i < dt.Columns.Count; i++)
            {
                if (j == 0)
                {
                    Label14.Visible = true;
                    TextBoxx1.Visible = true;
                    Label14.Text = dt.Columns[i].ColumnName;
                }
                else if (j == 1)
                {
                    Label15.Visible = true;
                    TextBoxx2.Visible = true;
                    Label15.Text = dt.Columns[i].ColumnName;
                }
                else if (j == 2)
                {
                    Label16.Visible = true;
                    TextBoxx3.Visible = true;
                    Label16.Text = dt.Columns[i].ColumnName;
                }
                else if (j == 3)
                {
                    Label17.Visible = true;
                    TextBoxx4.Visible = true;
                    Label17.Text = dt.Columns[i].ColumnName;
                }
                else if (j == 4)
                {
                    Label18.Visible = true;
                    TextBoxx5.Visible = true;
                    Label18.Text = dt.Columns[i].ColumnName;
                }
                else if (j == 5)
                {
                    Label19.Visible = true;
                    TextBoxx6.Visible = true;
                    Label19.Text = dt.Columns[i].ColumnName;
                }
                else if (j == 6)
                {
                    Label120.Visible = true;
                    TextBoxx7.Visible = true;
                    Label120.Text = dt.Columns[i].ColumnName;
                }

                j++;


                grdWTAllowance.Columns[19 + i].Visible = true;
                grdWTAllowance.Columns[19 + i].HeaderText = dt.Columns[i].ColumnName;
            }
            /* if (dt.Columns.Count == 1)
             {
                 grdWTAllowance.Columns[13].Visible = true;
                 grdWTAllowance.Columns[13].HeaderText = dt.Columns[0].ColumnName;
             }
             else if (dt.Columns.Count == 2)
             {
                 grdWTAllowance.Columns[13].Visible = true;
                 grdWTAllowance.Columns[14].Visible = true;
                 grdWTAllowance.Columns[13].HeaderText = dt.Columns[0].ColumnName;
                 grdWTAllowance.Columns[14].HeaderText = dt.Columns[1].ColumnName;

             }
             else if (dt.Columns.Count == 3)
             {
                 grdWTAllowance.Columns[13].Visible = true;
                 grdWTAllowance.Columns[14].Visible = true;
                 grdWTAllowance.Columns[15].Visible = true;
                 grdWTAllowance.Columns[13].HeaderText = dt.Columns[0].ColumnName;
                 grdWTAllowance.Columns[14].HeaderText = dt.Columns[1].ColumnName;
                 grdWTAllowance.Columns[15].HeaderText = dt.Columns[2].ColumnName;
             }

             else if (dt.Columns.Count == 4)
             {
                 grdWTAllowance.Columns[13].Visible = true;
                 grdWTAllowance.Columns[14].Visible = true;
                 grdWTAllowance.Columns[15].Visible = true;
                 grdWTAllowance.Columns[16].Visible = true;
                 grdWTAllowance.Columns[13].HeaderText = dt.Columns[0].ColumnName;
                 grdWTAllowance.Columns[14].HeaderText = dt.Columns[1].ColumnName;
                 grdWTAllowance.Columns[15].HeaderText = dt.Columns[2].ColumnName;
                 grdWTAllowance.Columns[16].HeaderText = dt.Columns[3].ColumnName;
             }

             else if (dt.Columns.Count == 5)
             {
                 grdWTAllowance.Columns[13].Visible = true;
                 grdWTAllowance.Columns[14].Visible = true;
                 grdWTAllowance.Columns[15].Visible = true;
                 grdWTAllowance.Columns[16].Visible = true;
                 grdWTAllowance.Columns[17].Visible = true;
                 grdWTAllowance.Columns[13].HeaderText = dt.Columns[0].ColumnName;
                 grdWTAllowance.Columns[14].HeaderText = dt.Columns[1].ColumnName;
                 grdWTAllowance.Columns[15].HeaderText = dt.Columns[2].ColumnName;
                 grdWTAllowance.Columns[16].HeaderText = dt.Columns[3].ColumnName;
                 grdWTAllowance.Columns[17].HeaderText = dt.Columns[4].ColumnName;
             }*/

        }
        //int iCount = dsSFMR.Tables[0].Rows.Count - 1;
        //dsSFMR.Tables[0].Rows.RemoveAt(iCount);


        grdWTAllowance.DataSource = dsSFMR;
        grdWTAllowance.DataBind();
        DataTable rGdT = new DataTable();
        rGdT = sf.GetRange(div_code);

        GridViewRow row = grdWTAllowance.HeaderRow;

        if (rGdT.Rows.Count > 0)
        {
            ((TextBox)row.FindControl("txtRange")).Text = rGdT.Rows[0]["Range1_KMS"].ToString();
            ((RadioButtonList)row.FindControl("rbtLstRating")).SelectedValue = rGdT.Rows[0]["Range1_status"].ToString();

            ((TextBox)row.FindControl("txtRange2")).Text = rGdT.Rows[0]["Range2_KMS"].ToString();
            ((RadioButtonList)row.FindControl("rbtLstRating2")).SelectedValue = rGdT.Rows[0]["Range2_status"].ToString();
            ((TextBox)row.FindControl("txtRange3")).Text = rGdT.Rows[0]["Range3_KMS"].ToString();
            ((RadioButtonList)row.FindControl("rbtLstRating3")).SelectedValue = rGdT.Rows[0]["Range3_status"].ToString();
        }
        else
        {
            ((TextBox)row.FindControl("txtRange")).Text = "0";
            ((RadioButtonList)row.FindControl("rbtLstRating")).SelectedValue = "Consolidate";

            ((TextBox)row.FindControl("txtRange2")).Text = "0";
            ((RadioButtonList)row.FindControl("rbtLstRating2")).SelectedValue = "Consolidate";
            ((TextBox)row.FindControl("txtRange3")).Text = "0";
            ((RadioButtonList)row.FindControl("rbtLstRating3")).SelectedValue = "Consolidate";

        }

        foreach (GridViewRow gridRow in grdWTAllowance.Rows)
        {
            TextBox lblEffective = (TextBox)gridRow.FindControl("txtEffective");
            if (lblEffective.Text == "")
            {
                lblEffective.Text = txtTPStartDate.Text;
            }
        }
        // Sdt= dsSFMR.Tables[0];
        //CreateDynamicTable(dsSFMR);
    }

    //protected void linkcheck_Click(object sender, EventArgs e)
    //{


    //    FillReporting();
    //    ddlRegion.Visible = true;
    //    linkcheck.Visible = false;
    //    txtNew.Visible = true;
    //    btnGo.Visible = true;
    //    //FillColor();
    //}
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            string strParametrValue = string.Empty;
            string strColumn = string.Empty;
            string strWtypeParametrValue = string.Empty;
            string strWtypeColumn = string.Empty;
            int iReturn = -1;

            //for (int i = 1; i < grdWTAllowance.Columns.Count; i++)
            //{
            //    foreach (GridViewRow gridrow in grdWTAllowance.Rows)
            //    {
            //        string str1 = grdWTAllowance.HeaderRow.Cells[13].Text;                     
            //    }
            //    for (int j = 0; j < grdWTAllowance.Rows.Count; j++)
            //    {
            //        string str = grdWTAllowance.Rows[j].Cells[i].Text;
            //    }
            //}

            //TextBox lnkView = (sender as TextBox);
            //GridViewRow row = (lnkView.NamingContainer as GridViewRow);
            //string id = lnkView.Text;
            //string name = row.Cells[13].Text;
            //string country = (row.FindControl("myTextBox13") as TextBox).Text;
            //ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Id: " + id + " Name: " + name + " Country: " + country + "')", true);

            foreach (GridViewRow gridRow in grdWTAllowance.Rows)
            {

                Label lblsfcode = (Label)gridRow.FindControl("lblsfcode");
                Label lbldesigcode = (Label)gridRow.FindControl("lbldesigcode");
                Label lblDesignation = (Label)gridRow.FindControl("lblDesignation");
                TextBox txtHq = (TextBox)gridRow.FindControl("txtHq");
                TextBox txtEXHQ = (TextBox)gridRow.FindControl("txtEXHQ");
                TextBox txtOS = (TextBox)gridRow.FindControl("txtOS");
                TextBox txtMetro = (TextBox)gridRow.FindControl("txtMetro");
                TextBox txtnonMetro = (TextBox)gridRow.FindControl("txtnonMetro");
                TextBox txtHill = (TextBox)gridRow.FindControl("txtHill");
                TextBox txtFareKm = (TextBox)gridRow.FindControl("txtFareKm");
                TextBox txtlclcon = (TextBox)gridRow.FindControl("txtlclcon");
                TextBox txtmeetallw = (TextBox)gridRow.FindControl("txtmeetallw");
                TextBox txtRangeofFare1 = (TextBox)gridRow.FindControl("txtRangeofFare1");
                TextBox txtRangeofFare2 = (TextBox)gridRow.FindControl("txtRangeofFare2");
                TextBox txtRangeofFare3 = (TextBox)gridRow.FindControl("txtRangeofFare3");
                TextBox txtEffective = (TextBox)gridRow.FindControl("txtEffective");

                /*if (dt.Columns.Count == 1)
                {
                    TextBox txttxtHill_Station_Allowance = (TextBox)gridRow.FindControl("TextBox13"); 
                    strParametrValue="'"+txttxtHill_Station_Allowance.Text+"'";
                    strColumn = "Fixed_Column1";
                }
                else if (dt.Columns.Count == 2)
                {
                    TextBox txttxtHill_Station_Allowance = (TextBox)gridRow.FindControl("TextBox13");
                    TextBox txtMetro_Allowances = (TextBox)gridRow.FindControl("TextBox14"); 
                    strParametrValue="'"+txttxtHill_Station_Allowance.Text+"','"+txtMetro_Allowances.Text+"'";
                    strColumn = "Fixed_Column1,Fixed_Column2";
                }
                else if (dt.Columns.Count == 3)
                {
                    TextBox txttxtHill_Station_Allowance = (TextBox)gridRow.FindControl("TextBox13");
                    TextBox txtMetro_Allowances = (TextBox)gridRow.FindControl("TextBox14");
                    TextBox txtMiscellaneous_Expenses = (TextBox)gridRow.FindControl("TextBox15");
                    strParametrValue = "'" + txttxtHill_Station_Allowance.Text + "','" + txtMetro_Allowances.Text + "','"+ txtMiscellaneous_Expenses.Text+"'";
                    strColumn = "Fixed_Column1,Fixed_Column2,Fixed_Column3";
                }
                else if (dt.Columns.Count == 4)
                {
                    TextBox txttxtHill_Station_Allowance = (TextBox)gridRow.FindControl("TextBox13");
                    TextBox txtMetro_Allowances = (TextBox)gridRow.FindControl("TextBox14");
                    TextBox txtMiscellaneous_Expenses = (TextBox)gridRow.FindControl("TextBox15");
                    TextBox TextBox16 = (TextBox)gridRow.FindControl("TextBox16");
                    strParametrValue = "'" + txttxtHill_Station_Allowance.Text + "','" + txtMetro_Allowances.Text + "','" + txtMiscellaneous_Expenses.Text + "','"+ TextBox16.Text+"'";
                    strColumn = "Fixed_Column1,Fixed_Column2,Fixed_Column3,Fixed_Column4";
                }
                else if (dt.Columns.Count == 5)
                {
                    TextBox txttxtHill_Station_Allowance = (TextBox)gridRow.FindControl("TextBox13");
                    TextBox txtMetro_Allowances = (TextBox)gridRow.FindControl("TextBox14");
                    TextBox txtMiscellaneous_Expenses = (TextBox)gridRow.FindControl("TextBox15");
                    TextBox TextBox16 = (TextBox)gridRow.FindControl("TextBox16");
                    TextBox TextBox17 = (TextBox)gridRow.FindControl("TextBox17");

                    strParametrValue = "'" + txttxtHill_Station_Allowance.Text + "','" + txtMetro_Allowances.Text + "','" + txtMiscellaneous_Expenses.Text + "','" + TextBox16.Text + "','"+ TextBox17.Text+"'";
                    strColumn = "Fixed_Column1,Fixed_Column2,Fixed_Column3,Fixed_Column4,Fixed_Column5";
                }*/

                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    string txtValue = "TextBox" + (13 + j);
                    string colValue = "Fixed_Column" + (j + 1);
                    TextBox txttxtHill_Station_Allowance = (TextBox)gridRow.FindControl(txtValue);
                    if (j == 0)
                    {
                        strParametrValue = "'" + txttxtHill_Station_Allowance.Text + "'";
                        strColumn = colValue;
                    }
                    else
                    {
                        strParametrValue = strParametrValue + ",'" + txttxtHill_Station_Allowance.Text + "'";
                        strColumn = strColumn + "," + colValue;
                    }
                }
                for (int j = 0; j < wrkType_dt.Columns.Count; j++)
                {
                    string txtValue = "TextBox" + (23 + j);
                    string colValue = "Wtype_Fixed_Column" + (j + 1);
                    TextBox txttxtHill_Station_Allowance = (TextBox)gridRow.FindControl(txtValue);
                    if (j == 0)
                    {
                        strWtypeParametrValue = "'" + txttxtHill_Station_Allowance.Text + "'";
                        strWtypeColumn = colValue;
                    }
                    else
                    {
                        strWtypeParametrValue = strWtypeParametrValue + ",'" + txttxtHill_Station_Allowance.Text + "'";
                        strWtypeColumn = strWtypeColumn + "," + colValue;
                    }
                }
                if (txtEffective.Text != "")
                {
                    //iReturn = sf.ExpSalesforce_RecordAdd(lblsfcode.Text, txtHq.Text, txtEXHQ.Text, txtOS.Text, txtHill.Text, txtFareKm.Text, txtRangeofFare1.Text, txtRangeofFare2.Text, txtEffective.Text, strParametrValue, strColumn, lbldesigcode.Text, lblDesignation.Text, div_code);
                    iReturn = sf.ExpSalesforce_RecordAdd(lblsfcode.Text, txtHq.Text, txtEXHQ.Text, txtOS.Text, txtHill.Text, txtFareKm.Text, txtRangeofFare1.Text, txtRangeofFare2.Text, txtEffective.Text, strParametrValue, strColumn, lbldesigcode.Text, lblDesignation.Text, div_code, strWtypeParametrValue, strWtypeColumn, txtRangeofFare3.Text, txtMetro.Text, txtnonMetro.Text, txtlclcon.Text, txtmeetallw.Text);
                }
            }
            GridViewRow row = grdWTAllowance.HeaderRow;
            string txtRange = ((TextBox)row.FindControl("txtRange")).Text;
            string rbtLstRating = ((RadioButtonList)row.FindControl("rbtLstRating")).SelectedValue;
            string txtRange2 = ((TextBox)row.FindControl("txtRange2")).Text;
            string rbtLstRating2 = ((RadioButtonList)row.FindControl("rbtLstRating2")).SelectedValue;
            string txtRange3 = ((TextBox)row.FindControl("txtRange3")).Text;
            string rbtLstRating3 = ((RadioButtonList)row.FindControl("rbtLstRating3")).SelectedValue;
            iReturn = sf.SetRange(txtRange, rbtLstRating, txtRange2, rbtLstRating2, div_code, txtRange3, rbtLstRating3);

            if (iReturn > 0)
            {
                //   menu1.Status = "Division Created Successfully ";
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Created Successfully');</script>");
                //Server.Transfer("FVExpense_Parameter.aspx");
                // Resetall();
            }

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }


    private void BindDate()
    {


        foreach (GridViewRow gridRow in grdWTAllowance.Rows)
        {
            TextBox lblEffective = (TextBox)gridRow.FindControl("txtEffective");
            lblEffective.Text = txtTPStartDate.Text;
        }
    }

    protected void txtTPStartDate_TextChanged(object sender, EventArgs e)
    {
        BindDate();
        btnSave.Visible = true;
    }
    protected void Page_PreInit(object sender, EventArgs e)
    {
        //grdWTAllowance.DataSource = Sdt;
        //grdWTAllowance.DataBind();
    }
    protected void grdWTAllowance_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        DataTable dtCurrentTable = new DataTable();
        DataRow drCurrentRow = null;

        //if (e.Row.RowType == DataControlRowType.DataRow)
        //{
        //    for (int i = 13; i < grdWTAllowance.Columns.Count; i++)
        //    {
        //        // string str = dsSFMR.Tables[0].Rows[i][23].ToString();
        //        for (int j = 0; j < grdWTAllowance.Rows.Count; j++)
        //        {
        //            TextBox TextBox1 = new TextBox();
        //            grdWTAllowance.HeaderRow.Cells[i].Text = dsSFMR.Tables[0].Columns[i].ToString();

        //        }
        //    }
        //for (int i = 23; i < dsSFMR.Tables[0].Columns.Count; i++)
        //{
        //    //TextBox TextBox1 = new TextBox();
        //    //TextBox1.ID = "myTextBox" + (i).ToString();
        //    ////TextBox1.Text = "myTextBox" + (i).ToString();
        //    //TextBox1.Attributes.Add("runat", "server");
        //    //e.Row.Cells[i].FindControl("myTextBox" + (i));
        //    grdWTAllowance.HeaderRow.Cells[i].Text = dsSFMR.Tables[0].Columns[i].ToString();
        //    //dsSFMR.Tables[0].Rows[i][str] = TextBox1.Text;
        //    //e.Row.Cells[i].Controls.Add(TextBox1);

        //}
        //}

        //        TextBox txtCountry = new TextBox();
        //        txtCountry.ID = "myTextBox" + (i).ToString();

        //            string str = grdWTAllowance.HeaderRow.Cells[i].Text;
        //            txtCountry.Text = (e.Row.DataItem as DataRowView).Row[str].ToString();
        //            e.Row.Cells[i].Controls.Add(txtCountry);

        //    }


        //}

        //for (int i = 13; i < grdWTAllowance.Columns.Count; i++)
        //{
        //    // string str = dsSFMR.Tables[0].Rows[i][23].ToString();
        //    //for (int j = 0; j < grdWTAllowance.Rows.Count; j++)
        //    //{
        //    //    TextBox TextBox1 = new TextBox();
        //    //    TextBox1.ID = newserialno + (Convert.ToInt32(grdWTAllowance.Rows[.RowIndex + 1)).ToString();
        //    //    grdWTAllowance.Rows[j].Cells[i].Controls.Add(TextBox1);
        //    //}

        //    foreach (GridViewRow gridRow in grdWTAllowance.Rows)
        //    {
        //        TextBox TextBox1 = new TextBox();
        //        TextBox1.ID = "myTextBox" + (i).ToString();
        //        //TextBox1.Text = "myTextBox" + (i).ToString();
        //        TextBox1.Attributes.Add("runat", "server");
        //        grdWTAllowance.Rows[gridRow.RowIndex].Cells[i].Controls.Add(TextBox1);
        //    }
        //}

        //dsExp = terr.getExp_FixedType();
        //dt = dsExp.Tables[0];
        //Repeater rt = (Repeater)e.Row.FindControl("rptCont");
        //Repeater rt1 = (Repeater)e.Row.FindControl("rptCont1");
        //if (e.Row.RowType == DataControlRowType.DataRow)
        //{
        //    rt.DataSource = dsExp;
        //    rt.DataBind();

        //    //rt1.DataSource = dsExp;
        //    //rt1.DataBind();

        //    for (int i = 0; i < dt.Columns.Count; i++)
        //    {
        //        BoundField boundfield = new BoundField();
        //        boundfield.DataField = dt.Rows[i].ToString();
        //        boundfield.HeaderText = dt.Rows[i].Field<string>(0);
        //        grdWTAllowance.Columns.Add(boundfield);
        //    }
        //}
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        BindSelectedValue();
        btnSave.Visible = true;
        lblSelect.Visible = false;
    }
    protected void btndes_Click(object sender, EventArgs e)
    {
        fillgrid_desig();
    }

    private void fillgrid_desig()
    {
        foreach (GridViewRow grd in grdWTAllowance.Rows)
        {
            Label lblDesignation = (Label)grd.FindControl("lblDesignation");
            Label lblCat = (Label)grd.FindControl("lblCat");
            Label lblSts = (Label)grd.FindControl("lblSts");

            TextBox txtHq = (TextBox)grd.FindControl("txtHq");
            TextBox txtEXHQ = (TextBox)grd.FindControl("txtEXHQ");
            TextBox txtOS = (TextBox)grd.FindControl("txtOS");
            TextBox txtMetro = (TextBox)grd.FindControl("txtMetro");
            TextBox txtnonMetro = (TextBox)grd.FindControl("txtnonMetro");
            TextBox txtHill = (TextBox)grd.FindControl("txtHill");
            TextBox txtFareKm = (TextBox)grd.FindControl("txtFareKm");

            //TextBox txtspecHQ = (TextBox)grd.FindControl("txtspecHQ");
            //TextBox txtspecEX = (TextBox)grd.FindControl("txtspecEX");
            //TextBox txtspecOS = (TextBox)grd.FindControl("txtspecOS");
            //TextBox txtspecMEET = (TextBox)grd.FindControl("txtspecMEET");
            //TextBox txtspecTRA = (TextBox)grd.FindControl("txtspecTRA");

            TextBox txtRangeofFare1 = (TextBox)grd.FindControl("txtRangeofFare1");
            TextBox txtRangeofFare2 = (TextBox)grd.FindControl("txtRangeofFare2");
            TextBox txtRangeofFare3 = (TextBox)grd.FindControl("txtRangeofFare3");

            TextBox TextBox13 = (TextBox)grd.FindControl("TextBox13");

            TextBox TextBox14 = (TextBox)grd.FindControl("TextBox14");

            TextBox TextBox15 = (TextBox)grd.FindControl("TextBox15");

            TextBox TextBox16 = (TextBox)grd.FindControl("TextBox16");
            TextBox TextBox17 = (TextBox)grd.FindControl("TextBox17");
            TextBox TextBox18 = (TextBox)grd.FindControl("TextBox18");
            TextBox TextBox19 = (TextBox)grd.FindControl("TextBox19");
            TextBox TextBox20 = (TextBox)grd.FindControl("TextBox20");
            TextBox TextBox21 = (TextBox)grd.FindControl("TextBox21");
            TextBox TextBox22 = (TextBox)grd.FindControl("TextBox22");



            if (ddldesig.SelectedItem.Text != null && dd2cat.SelectedItem.Text != null && dd3sts.SelectedItem.Text != null)
            {

                if (lblDesignation.Text == ddldesig.SelectedItem.Text && lblCat.Text == dd2cat.SelectedItem.Text && lblSts.Text == dd3sts.SelectedItem.Text)
                {
                    //if (txtHq.Text == "")
                    //{
                    txtHq.Text = txthqdes.Text;
                    // }

                    //if (txtEXHQ.Text == "")
                    //{
                    txtEXHQ.Text = txtEXdes.Text;
                    //}
                    //if (txtOS.Text == "")
                    //{
                    txtOS.Text = txtosdes.Text;
                    // }
                    txtMetro.Text = txtossmdes.Text;
                    txtnonMetro.Text = txtosnmdes.Text;
                    //if (txtHill.Text == "")
                    //{
                    txtHill.Text = txthilldes.Text;
                    //}
                    //if (txtFareKm.Text == "")
                    //{
                    txtFareKm.Text = txtfaredes.Text;
                    //}
                    //if (txtspecHQ.Text == "")
                    //{
                    // txtspecHQ.Text = txtspehqdes.Text;
                    //}
                    //if (txtspecEX.Text == "")
                    //{
                    // txtspecEX.Text = txtspecexdes.Text;
                    //}
                    //if (txtspecOS.Text == "")
                    //{
                    // txtspecOS.Text = txtspecosdes.Text;
                    //}
                    //if (txtspecMEET.Text == "")
                    //{
                    // txtspecMEET.Text = txtspecmeetdes.Text;
                    //}
                    //if (txtspecTRA.Text == "")
                    //{
                    //txtspecTRA.Text = txtspectrandes.Text;
                    //}
                    //if (txtRangeofFare1.Text == "")
                    //{
                    txtRangeofFare1.Text = txtrange1des.Text;
                    //}
                    //if (txtRangeofFare2.Text == "")
                    //{
                    txtRangeofFare2.Text = txtrange2des.Text;
                    //}
                    //if (TextBox13.Text == "")
                    //{
                    TextBox13.Text = TextBoxx1.Text;
                    //}
                    //if (TextBox14.Text == "")
                    //{
                    TextBox14.Text = TextBoxx2.Text;
                    //}
                    //if (TextBox15.Text == "")
                    //{
                    TextBox15.Text = TextBoxx3.Text;
                    //}
                    //if (TextBox16.Text == "")
                    //{
                    TextBox16.Text = TextBoxx4.Text;
                    // }
                    //if (TextBox17.Text == "")
                    //{
                    TextBox17.Text = TextBoxx5.Text;
                    TextBox18.Text = TextBoxx6.Text;
                    TextBox19.Text = TextBoxx7.Text;
                    TextBox20.Text = TextBoxx8.Text;
                    TextBox21.Text = TextBoxx9.Text;
                    TextBox22.Text = TextBoxx10.Text;
                    txtRangeofFare3.Text = txtrange3des.Text;
                    //}

                    //}
                }
            }
        }
    }
    private void filldesg()
    {
        DataSet dsDes = null;
        Designation des = new Designation();
        //  dsDes = des.getDesig_code_withname(div_code);
        string strQry = string.Empty;

        DB_EReporting db_ER = new DB_EReporting();

        DataSet dsDesignation = null;
        strQry = "SELECT Designation_Code,Designation_Short_Name " +
                 " FROM Mas_SF_Designation where Division_code='" + div_code + "' and Designation_Active_Flag=0 ";

        dsDesignation = db_ER.Exec_DataSet(strQry);

        if (dsDesignation.Tables[0].Rows.Count > 0)
        {
            ddldesig.DataSource = dsDesignation;
            ddldesig.DataTextField = "Designation_Short_Name";
            ddldesig.DataValueField = "Designation_Code";
            ddldesig.DataBind();
        }
    }

}