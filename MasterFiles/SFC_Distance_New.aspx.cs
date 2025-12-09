using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;
using System.Data.SqlClient;
using DBase_EReport;
using System.Web.UI.HtmlControls;

public partial class MasterFiles_SFC_Distance_New : System.Web.UI.Page
{
    string div_code = string.Empty;
    DataSet dsSalesForce = new DataSet();
    string sfCode = string.Empty;
    string EX_Distance = string.Empty;
    string OS_Distance = string.Empty;
    string OSEX_Distance = string.Empty;
    string terrName_From = string.Empty;
    string terrName_To = string.Empty;
    DataSet dsTerritory = null;
    string terr_Name = string.Empty;
    DataSet dsTerr1 = null;
    DataSet dsTerr2 = null;
    string Fromterr_Sf_Code = string.Empty;
    string Toterr_Sf_Code = string.Empty;
    DataSet dsDCR = null;

    protected void Page_Load(object sender, EventArgs e)
    {

        div_code = Session["div_code"].ToString();
        sfCode = Session["sf_code"].ToString();
        //if (Session["sf_code"] != null && Session["sf_code"].ToString() != "")
        //{
        //    sfCode = Session["sf_code"].ToString();
        //}
        if (Session["sf_type"].ToString() == "1")
        {
            sfCode = Session["sf_code"].ToString();
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
            sfCode = Session["sf_code"].ToString();
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
            //c1.FindControl("btnBack").Visible = false;
            c1.Title = this.Page.Title;


        }

        if (!Page.IsPostBack)
        {
            //btnclear.Visible = false;
            FillSalesForce();
            ddlFieldForce.Visible = true;
            btnGo.Visible = true;

        }
        FillColor();

    }
    private void FillSalesForce()
    {
        //SalesForce sf = new SalesForce();
        //sfCode = Session["sf_code"].ToString();
        //if (Session["sf_type"].ToString() == "1")
        //{
        //    dsSalesForce = sf.getSfName_HQ(sfCode);
        //}
        //else
        //{
        //    dsSalesForce = sf.getFieldForce_Distance(div_code);
        //}
        ////dsSalesForce = sf.getFieldForce_Distance(div_code);
        //if (dsSalesForce.Tables[0].Rows.Count > 0)
        //{
        //    ddlFieldForce.DataTextField = "sf_name";
        //    ddlFieldForce.DataValueField = "sf_code";
        //    ddlFieldForce.DataSource = dsSalesForce;
        //    ddlFieldForce.DataBind();
        //}

        SalesForce sf = new SalesForce();
        dsSalesForce = sf.SalesForceListMgrGet(div_code, sfCode);
        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {

            if (Session["sf_type"].ToString() == "3")
            {
                dsSalesForce.Tables[0].Rows[0].Delete();

            }
            ddlFieldForce.DataValueField = "SF_Code";
            ddlFieldForce.DataTextField = "Sf_Name";
            ddlFieldForce.DataSource = dsSalesForce;
            ddlFieldForce.DataBind();

            ddlSF.DataTextField = "Desig_Color";
            ddlSF.DataValueField = "sf_code";
            ddlSF.DataSource = dsSalesForce;
            ddlSF.DataBind();

        }
        FillColor();
    }

    private void FillColor()
    {
        int j = 0;

        foreach (ListItem ColorItems in ddlSF.Items)
        {
            //ddlFieldForce.Items[j].Selected = true;
            string bcolor = "#" + ColorItems.Text;
            ddlFieldForce.Items[j].Attributes.Add("style", "background-color:" + bcolor);

            j = j + 1;

        }
    }

    protected void btnClear_Click(object sender, EventArgs e)
    {
        ddlFieldForce.Enabled = true;
        btnclear.Visible = false;
        btnGo.Visible = true;
        pnl.Visible = false;

    }
    //protected void linkcheck_Click(object sender, EventArgs e)
    //{
    //    FillSalesForce();
    //    ddlFieldForce.Visible = true;
    //    linkcheck.Visible = false;
    //    txtNew.Visible = true;
    //    btnGo.Visible = true;
    //    // btnclear.Visible = true;
    //    //FillColor();
    //}


    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        SalesForce sf = new SalesForce();
        dsDCR = sf.SalesForceListMgrGet(div_code, ddlFieldForce.SelectedValue);
        if (dsDCR.Tables[0].Rows.Count > 0)
        {
            //dsDCR.Tables[0].Rows[0].Delete();

            foreach (DataRow drFF in dsDCR.Tables[0].Rows)
            {
                string code = drFF["sf_code"].ToString();
                if (!code.Contains("MGR"))
                {
                    Fromterr_Sf_Code += "'" + drFF["sf_code"].ToString() + "'" + ",";
                    Toterr_Sf_Code += "'" + drFF["sf_code"].ToString() + "'" + ",";
                }
            }

            if (Fromterr_Sf_Code != "")
            {
                Fromterr_Sf_Code = Fromterr_Sf_Code.Substring(0, Fromterr_Sf_Code.Length - 1);
                Toterr_Sf_Code = Toterr_Sf_Code.Substring(0, Toterr_Sf_Code.Length - 1);
            }


            //ViewState["Fromterr_Sf_Code"] = "'" + ddlFieldForce.SelectedValue + "'" + "," + Fromterr_Sf_Code;
            //ViewState["Toterr_Sf_Code"] = Toterr_Sf_Code;
        }
        string vwsess = "'" + ddlFieldForce.SelectedValue + "'" + "," + Fromterr_Sf_Code;

        ViewState["Fromterr_Sf_Code"] = vwsess.TrimEnd(',');
        string vwsess1 = "'" + ddlFieldForce.SelectedValue + "'" + "," + Toterr_Sf_Code;

        ViewState["Toterr_Sf_Code"] = vwsess1.TrimEnd(',');
        //ViewState["Toterr_Sf_Code"] = Toterr_Sf_Code;



        generateOtherExpControls();
        pnl.Visible = true;
        ddlFieldForce.Enabled = false;
        btnGo.Visible = false;
        btnclear.Visible = true;
        fillterr1();
        fillterr2();
        txtdistance.Text = "";

    }

    private void fillterr1()
    {

        string strQry = string.Empty;

        DB_EReporting db_ER = new DB_EReporting();

        if (ddlFieldForce.SelectedValue.Contains("MGR"))
        {
            strQry = "select '0' as territory_code,'--Select--' as Territory_Name,'A' sf_name,'0' ord,'FF99FF' colors union all " +
                   " select sf_code as territory_code,sf_hq +' (' + sf_name +')' as Territory_Name,sf_name,'1' ord,'FF99FF' colors from mas_Salesforce where sf_code in (" + ViewState["Fromterr_Sf_Code"].ToString() + ") and sf_code like '%MGR%' " +
                     " union all " +
                     " select sf_code as territory_code,sf_hq +' (' + sf_name +')' as Territory_Name,sf_name,'2' ord,'FF99FF' colors from mas_Salesforce where sf_code in (" + ViewState["Fromterr_Sf_Code"].ToString() + ") and sf_code like '%MR%' " +
                     " union all " +
                      " select cast(territory_code as varchar) as territory_code, Territory_Name +'('+Alias_Name +' )' +'(' +case when territory_cat=1 then 'HQ' when territory_cat=2 then 'EX' when " +
                      " territory_cat='3' then 'OS' when territory_cat=4 then 'OS-EX' end + ')'+' ('+ sf_name +')' as Territory_Name,sf_name,'3' ord,'FF99FF' colors from mas_territory_creation T inner join mas_Salesforce S on T.sf_code=S.sf_code" +
                      " where Territory_Active_Flag=0 and S.sf_code in (" + ViewState["Fromterr_Sf_Code"].ToString() + ")  and territory_cat !=1 "+
                       " union all " +
                     " select cast(territory_code as varchar) as territory_code, Territory_Name +'('+Alias_Name +' )' +'(' + mgr_territory_cat " +
                     " + ')'+' ('+ sf_name +')' as Territory_Name,sf_name,'4' ord,'FF99FF' colors from mas_territory_creation T inner join mas_Salesforce S on T.sf_code=S.sf_code inner join Mgr_Allowance_Setup M on T.sf_code=M.sf_code and type_code='CA' and mgr_code in('" + ddlFieldForce.SelectedValue + "') " +
                     " where S.sf_code in (" + ViewState["Fromterr_Sf_Code"].ToString() + ")  and mgr_territory_cat in('EX','OS')  order by ord,sf_name";
            //order by ord,sf_name";


            dsTerr1 = db_ER.Exec_DataSet(strQry);
        }
        else
        {

            strQry = "select '0' as territory_code,'--Select--' as Territory_Name union all " +
                     " select sf_code as territory_code,sf_hq as Territory_Name from mas_Salesforce where sf_code='" + ddlFieldForce.SelectedValue + "' " +
                      " union all " +
                      " select cast(territory_code as varchar) as territory_code, Territory_Name +'('+Alias_Name +' )' +'(' +case when territory_cat=1 then 'HQ' when territory_cat=2 then 'EX' when " +
                      " territory_cat='3' then 'OS' when territory_cat=4 then 'OS-EX' end + ')' as Territory_Name from mas_territory_creation " +
                      " where Territory_Active_Flag=0 and sf_code='" + ddlFieldForce.SelectedValue + "' and territory_cat !=1  ";

            dsTerr1 = db_ER.Exec_DataSet(strQry);
        }

        if (dsTerr1.Tables[0].Rows.Count > 0)
        {
            desig.DataSource = dsTerr1;
            desig.DataTextField = "Territory_Name";
            desig.DataValueField = "territory_code";
            desig.DataBind();
        }


    }
    private void fillterr2()
    {

        string strQry = string.Empty;

        DB_EReporting db_ER = new DB_EReporting();

        if (ddlFieldForce.SelectedValue.Contains("MGR"))
        {

            strQry = "select '0' as territory_code,'--Select--' as Territory_Name,'A' sf_name,'0' ord,'FF99FF' colors union all " +
                    " select sf_code+'$' as territory_code,sf_hq +' (' + sf_name +')' as Territory_Name,sf_name,'1' ord,'FF99FF' colors from mas_Salesforce where sf_code in (" + ViewState["Toterr_Sf_Code"].ToString() + ") and sf_code like '%MGR%' " +
                     " Union All " +
                     " select sf_code+'$' as territory_code,sf_hq +' (' + sf_name +')' as Territory_Name,sf_name,'2' ord,'FF99FF' colors from mas_Salesforce where sf_code in (" + ViewState["Toterr_Sf_Code"].ToString() + ") and sf_code like '%MR%' " +
                     " Union All " +
                      " select cast(territory_code as varchar) +'$' +territory_cat as territory_code, Territory_Name +'('+Alias_Name +' )' +'(' +case when territory_cat=1 then 'HQ' when territory_cat=2 then 'EX' when " +
                       " territory_cat='3' then 'OS' when territory_cat=4 then 'OS-EX' end + ')'+ '('+ sf_name +')' as Territory_Name,sf_name,'3' ord,'FF99FF' colors from mas_territory_creation T inner join mas_Salesforce S on T.sf_code=s.sf_Code " +
                       " where Territory_Active_Flag=0 and S.sf_code in (" + ViewState["Toterr_Sf_Code"].ToString() + ")   and territory_cat !=1 "+
                        " Union All " +
                      " select cast(territory_code as varchar) +'$' + case when mgr_territory_cat='HQ' then '1' when mgr_territory_cat='EX' then '2' when mgr_territory_cat='OS' then '3' when mgr_territory_cat='OS-EX' then '4' END as territory_code, Territory_Name +'('+Alias_Name +' )' +'(' +mgr_territory_cat " +
                       " + ')'+ '('+ sf_name +')' as Territory_Name,sf_name,'4' ord,'FF99FF' colors from mas_territory_creation T inner join mas_Salesforce S on T.sf_code=s.sf_Code inner join Mgr_Allowance_Setup M on T.sf_code=M.sf_code and type_code='CA' and mgr_code in('" + ddlFieldForce.SelectedValue + "')" +
                       " where  S.sf_code in (" + ViewState["Toterr_Sf_Code"].ToString() + ")   and mgr_territory_cat in('EX','OS') order by ord,sf_name ";

            //order by ord,sf_name ";

            dsTerr2 = db_ER.Exec_DataSet(strQry);
        }
        else
        {

            strQry = " select '0' as territory_code,'--Select--' as Territory_Name union all " +
                     " select cast(territory_code as varchar) +'$' +territory_cat as territory_code, Territory_Name +'('+Alias_Name +' )' +'(' +case when territory_cat=1 then 'HQ' when territory_cat=2 then 'EX' when " +
                      " territory_cat='3' then 'OS' when territory_cat=4 then 'OS-EX' end + ')' as Territory_Name from mas_territory_creation " +
                      " where territory_Active_flag=0 and " +
            "sf_code='" + ddlFieldForce.SelectedValue + "'  and territory_cat !=1  ";

            dsTerr2 = db_ER.Exec_DataSet(strQry);
        }

        if (dsTerr2.Tables[0].Rows.Count > 0)
        {
            dropMode.DataSource = dsTerr2;
            dropMode.DataTextField = "Territory_Name";
            dropMode.DataValueField = "territory_code";
            dropMode.DataBind();
        }


    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        int iReturn = -1;
        string strQry = string.Empty;
        DB_EReporting db = new DB_EReporting();

        strQry = "delete from mas_distance_fixation where sf_code='" + ddlFieldForce.SelectedValue + "'";
        iReturn = db.ExecQry(strQry);

        HiddenField otherExpValues = (HiddenField)FindControl("otherExpValues");
        //  iReturn = dist.deleteOthMgrExpSetupRecord(div_code);
        string[] splitVal = otherExpValues.Value.Split('~');


        string[] desig = splitVal[0].Split(',');
        string[] dropMode = splitVal[1].Split(',');
        string[] txtdistance = splitVal[2].Split(',');
        for (int p = 0; p < desig.Length; p++)
        {

            string[] e1 = desig[p].Split('=');
            string[] e2 = dropMode[p].Split('=');
            string[] e3 = txtdistance[p].Split('=');

            if (e1[0] != "0" || e2[0] != "0")
            {

                //if (ddlFieldForce.SelectedValue.Contains("MGR"))
                //{

                //    strQry = "select count(sf_code) from mas_distance_fixation where (from_code='" + e1[0] + "' and to_code='" + e2[0] + "' or " +
                //            " from_code='" + e2[0] + "' and to_code='" + e1[0] + "') and division_code='" + div_code + "' and sf_code='" + ddlFieldForce.SelectedValue + "'";
                //    iReturn = db.Exec_Scalar(strQry);

                //    if (iReturn > 0)
                //    {
                //        ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Already Submitted for " + e1[1] + " and " + e2[1] + " ');</script>");
                //        //break;
                //    }
                //    else
                //    {
                //        strQry = "insert into mas_distance_fixation (SF_Code,From_Code,To_Code,Distance_In_Kms,Division_code,Created_Date,Flag,From_Code_Name,To_Code_Name,From_Code_Code,To_Code_Code) VALUES" +
                //                " ('" + ddlFieldForce.SelectedValue + "','" + e1[0] + "','" + e2[0] + "','" + e3[0] + "','" + div_code + "',getdate(),0,'" + e1[1] + "','" + e2[1] + "','" + e1[0] + "','" + e2[0] + "') ";
                //        iReturn = db.ExecQry(strQry);
                //    }
                //}
                //else
                //{
                if (e2[0] != "0")
                {
                    string[] terrcode2 = e2[0].Split('$');
                string[] terrname = e2[1].Split('$');
                //iReturn = dist.addOthMgrExpSetupRecord(e2[0], e2[1], e1[0], e1[1], div_code);

                strQry = "select count(sf_code) from mas_distance_fixation where (from_code='" + e1[0] + "' and to_code='" + terrcode2[0] + "' or " +
                       " from_code='" + terrcode2[0] + "' and to_code='" + e1[0] + "') and division_code='" + div_code + "' and sf_code='" + ddlFieldForce.SelectedValue + "'";
                iReturn = db.Exec_Scalar(strQry);

                if (iReturn > 0)
                {
                    ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Already Submitted for " + e1[1] + " and " + terrname[0] + " ');</script>");
                    // break;
                }
                else
                {
                    strQry = "insert into mas_distance_fixation (SF_Code,From_Code,To_Code,Town_Cat,Distance_In_Kms,Division_code,Created_Date,Flag,From_Code_Name,To_Code_Name,From_Code_Code,To_Code_Code) VALUES" +
                             " ('" + ddlFieldForce.SelectedValue + "','" + e1[0] + "','" + terrcode2[0] + "','" + terrcode2[1] + "','" + e3[0] + "','" + div_code + "',getdate(),0,'" + e1[1] + "','" + terrname[0] + "','" + e1[0] + "','" + e2[0] + "') ";
                    iReturn = db.ExecQry(strQry);
                }
                }
            }

            if (iReturn > 0)
            {
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Saved Successfully');</script>");

            }
        }
        btnclear.Visible = false;
        btnGo.Visible = true;
        ddlFieldForce.Enabled = true;
        pnl.Visible = false;


    }

    private void generateOtherExpControls()
    {
        string strQry = string.Empty;
        DB_EReporting db = new DB_EReporting();
        div_code = Session["div_code"].ToString();
        // DataTable t2 = dist.getMgrExpsetupDesignation(div_code);

        //if (ddlFieldForce.SelectedValue.Contains("MGR"))
        //{
        //    strQry = "EXEC sfc_auto '" + div_code + "', '" + ddlFieldForce.SelectedValue + "' ";
        //    int iReturn = db.ExecQry(strQry);
        //}

        //strQry = "select From_Code,From_Code_Name,To_Code,To_Code_Name,Distance_In_Kms,From_Code_Code,To_Code_Code from mas_distance_fixation where sf_code='" + ddlFieldForce.SelectedValue + "'and division_code='" + div_code + "' and Flag=0 " +
        // " and  To_Code not in " +
        //" ( select cast(Territory_Code as varchar) from mas_territory_creation where sf_code='" + ddlFieldForce.SelectedValue + "' and Territory_Active_Flag=1) " +
        //" and From_Code not in ( select cast(Territory_Code as varchar) " +
        //" from mas_territory_creation where sf_code='" + ddlFieldForce.SelectedValue + "' and Territory_Active_Flag=1) ";


        //if (ddlFieldForce.SelectedValue.Contains("MGR"))
        //{
        //    //strQry = " select From_Code,From_Code_Name,To_Code,To_Code_Name,Distance_In_Kms,From_Code_Code,To_Code_Code from mas_distance_fixation " +
        //    //         " where sf_code='" + ddlFieldForce.SelectedValue + "' and " +
        //    //        " division_code='" + div_code + "'  and Flag=0  and to_code!='T' and " +
        //    //       " (to_code in (" + ViewState["Fromterr_Sf_Code"].ToString() + ") or To_Code in ( select cast(Territory_Code as varchar) " +
        //    //       " from mas_territory_creation where sf_code in (" + ViewState["Fromterr_Sf_Code"].ToString() + ") " +
        //    //       " and Territory_Active_Flag=0) ) and (from_code in (" + ViewState["Fromterr_Sf_Code"].ToString() + ") or  From_Code " +
        //    //      " in  ( select cast(Territory_Code as varchar) from mas_territory_creation where sf_code in (" + ViewState["Fromterr_Sf_Code"].ToString() + ") " +
        //    //      " and Territory_Active_Flag=0)) ";
        //    strQry = " select From_Code,From_Code_Name,To_Code,To_Code_Name,Distance_In_Kms,From_Code_Code," +
        //      " case when To_Code_Code like 'M%' then To_Code_Code else to_code+'$'+(select Territory_Cat from Mas_Territory_Creation where cast(territory_code as varchar)=To_Code)  end To_Code_Code from mas_distance_fixation " +
        //            " where sf_code='" + ddlFieldForce.SelectedValue + "' and " +
        //           " division_code='" + div_code + "'  and Flag=0 and to_code!='T'  and " +
        //          " (to_code in (" + ViewState["Fromterr_Sf_Code"].ToString() + ") or To_Code in ( select cast(Territory_Code as varchar) " +
        //          " from mas_territory_creation where territory_cat!=1 and sf_code in (" + ViewState["Fromterr_Sf_Code"].ToString() + ") " +
        //          " and Territory_Active_Flag=0) ) and (from_code in (" + ViewState["Fromterr_Sf_Code"].ToString() + ") or  From_Code " +
        //         " in  ( select cast(Territory_Code as varchar) from mas_territory_creation where territory_cat!=1 and sf_code in (" + ViewState["Fromterr_Sf_Code"].ToString() + ") " +
        //         " and Territory_Active_Flag=0)) ";

        //}

        //else
        //{
        //    //strQry = " select From_Code,From_Code_Name,To_Code,To_Code_Name,Distance_In_Kms,From_Code_Code,To_Code_Code from mas_distance_fixation where sf_code='" + ddlFieldForce.SelectedValue + "' and " +
        //    //    " division_code='" + div_code + "' and Flag=0   and  To_Code not in ( select cast(Territory_Code as varchar) from mas_territory_creation " +
        //    //    " where cast(Territory_Code as varchar) in (select to_code from mas_distance_fixation where sf_code='" + ddlFieldForce.SelectedValue + "') and Territory_Active_Flag=1) " +
        //    //    " and From_Code not in ( select cast(Territory_Code as varchar) from mas_territory_creation where cast(Territory_Code as varchar) " +
        //    //    " in (select From_Code from mas_distance_fixation where sf_code='" + ddlFieldForce.SelectedValue + "') and Territory_Active_Flag=1) ";


        //    strQry = " select From_Code,From_Code_Name,To_Code,To_Code_Name,Distance_In_Kms,From_Code_Code,"+
        //        " case when To_Code_Code like 'M%' then To_Code_Code else to_code+'$'+(select Territory_Cat from Mas_Territory_Creation where cast(territory_code as varchar)=To_Code)  end To_Code_Code from mas_distance_fixation " +
        //              " where sf_code='" + ddlFieldForce.SelectedValue + "' and " +
        //             " division_code='" + div_code + "'  and Flag=0 and to_code!='T'   and " +
        //            " (to_code in (" + ViewState["Fromterr_Sf_Code"].ToString() + ") or To_Code in ( select cast(Territory_Code as varchar) " +
        //            " from mas_territory_creation where territory_cat!=1 and sf_code in (" + ViewState["Fromterr_Sf_Code"].ToString() + ") " +
        //            " and Territory_Active_Flag=0) ) and (from_code in (" + ViewState["Fromterr_Sf_Code"].ToString() + ") or  From_Code " +
        //           " in  ( select cast(Territory_Code as varchar) from mas_territory_creation where territory_cat!=1 and sf_code in (" + ViewState["Fromterr_Sf_Code"].ToString() + ") " +
        //           " and Territory_Active_Flag=0)) ";
        //  //  DataTable t2 = db.Exec_DataTable(strQry);

        //}



        strQry = " select From_Code,To_Code,Distance_In_Kms,From_Code_Code,To_Code_Code from mas_distance_fixation " +
                   " where sf_code='" + ddlFieldForce.SelectedValue + "' and " +
                  " division_code='" + div_code + "'  and Flag=0 and to_code!='T' and " +
                 " (to_code in (" + ViewState["Fromterr_Sf_Code"].ToString() + ") or To_Code in ( select cast(Territory_Code as varchar) " +
                 " from mas_territory_creation where sf_code in (" + ViewState["Fromterr_Sf_Code"].ToString() + ") " +
                 " and Territory_Active_Flag=0) ) and (from_code in (" + ViewState["Fromterr_Sf_Code"].ToString() + ") or  From_Code " +
                " in  ( select cast(Territory_Code as varchar) from mas_territory_creation where sf_code in (" + ViewState["Fromterr_Sf_Code"].ToString() + ") " +
                " and Territory_Active_Flag=0)) ";

        DataTable t2 = db.Exec_DataTable(strQry);



        HtmlTable htmlTable = (HtmlTable)FindControl("tableId");
        //DataTable otherExp1 = dist.getDesigExp(div_code);

        for (int p = htmlTable.Rows.Count - 1; p > 0; p--)
        {
            if (t2.Rows.Count > 0)
                htmlTable.Rows.RemoveAt(p);
            else
            {
                fillterr1();
            }

        }
        for (int i = 0; i < t2.Rows.Count; i++)
        {

            HtmlTableRow r = new HtmlTableRow();
            DropDownList d = new DropDownList();
            d.ID = "desig_" + i;
            d.CssClass = "desig";
            fillterr1();
            if (dsTerr1.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow row in dsTerr1.Tables[0].Rows)
                {
                    ListItem list = new ListItem();
                    list.Text = row["Territory_Name"].ToString();
                    list.Value = row["territory_code"].ToString();
                    d.Items.Add(list);


                }
                //  d.Items.Insert(0, new ListItem("--Select--", "0"));


            }
            d.Text = t2.Rows[i]["From_Code"].ToString();
            //  d.Items.FindByText(t2.Rows[i]["From_Code_Name"].ToString()).Selected = true;
            d.Items.FindByValue(t2.Rows[i]["From_Code_Code"].ToString()).Selected = true;
            HtmlTableCell cell1 = new HtmlTableCell();
            cell1.Controls.Add(d);
            r.Cells.Add(cell1);
            DropDownList m = new DropDownList();
            m.ID = "dropMode_" + i;
            m.CssClass = "dropMode";
            fillterr2();
            if (dsTerr2.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow row in dsTerr2.Tables[0].Rows)
                {
                    ListItem list = new ListItem();
                    list.Text = row["Territory_Name"].ToString();
                    list.Value = row["territory_code"].ToString();
                    m.Items.Add(list);


                }
                //  d.Items.Insert(0, new ListItem("--Select--", "0"));


            }
            m.Text = t2.Rows[i]["To_Code"].ToString();
            //m.Items.FindByText(t2.Rows[i]["To_Code_Name"].ToString()).Selected = true;
            if (t2.Rows[i]["To_Code_Code"].ToString() != "" && m.Items.FindByValue(t2.Rows[i]["To_Code_Code"].ToString()) != null)
                m.Items.FindByValue(t2.Rows[i]["To_Code_Code"].ToString()).Selected = true;
            HtmlTableCell cell2 = new HtmlTableCell();
            cell2.Controls.Add(m);
            r.Cells.Add(cell2);

            // string amnt = t2.Rows[i]["Distance_In_Kms"].ToString();



            string dts_kms = t2.Rows[i]["Distance_In_Kms"].ToString();
            HtmlTableCell cell3 = new HtmlTableCell();
            Literal lit = new Literal();
            TextBox box1 = new TextBox();
            lit = new Literal();
            HtmlTable table = new HtmlTable();
            System.Text.StringBuilder sb = new System.Text.StringBuilder("");
            System.IO.StringWriter tw = new System.IO.StringWriter(sb);
            HtmlTextWriter hw = new HtmlTextWriter(tw);
            lit.Text = @"<input type='text' class='textbox' name='txtdistance' id='txtdistance_" + i + "' runat='server' value='" + dts_kms + "' size='5' />";
            sb = new System.Text.StringBuilder("");
            tw = new System.IO.StringWriter(sb);
            hw = new HtmlTextWriter(tw);
            box1.RenderControl(hw);
            cell3.Controls.Add(lit);
            r.Cells.Add(cell3);

            HtmlTableCell cell4 = new HtmlTableCell();
            Button b1 = new Button();
            lit = new Literal();
            lit.Text = @"<input type='button' id='btnadd' value=' + ' class='btnSave' onclick='_AdRowByCurrElem(this)' />";
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