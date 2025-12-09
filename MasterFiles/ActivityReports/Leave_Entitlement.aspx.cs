using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;
using System.Web.UI.HtmlControls;
using System.Web.Services;
using DBase_EReport;


public partial class MasterFiles_ActivityReports_Leave_Entitlement : System.Web.UI.Page
{
    string div_code = string.Empty;
    DataSet dsSalesForce = null;    
    DataSet dsDoc = null;  
   
      DataSet dsTP = null;
    string tot_dr = string.Empty;
    string tot_FWD = string.Empty;
    string tot_dcr_dr = string.Empty;   
    string sCurrentDate = string.Empty;
    string sf_code = string.Empty;
 
    string sf_type = string.Empty;
    string MultiSf_Code = string.Empty;
    
    DateTime ServerStartTime;
    DateTime ServerEndTime;
    string sfCode = string.Empty;
    DCRBusinessEntry objDCRBusiness = new DCRBusinessEntry();
    SalesForce objSample = new SalesForce();
    DataSet dsdoc = null;
   
    Territory objTerritory = new Territory();
  
    ListedDR lstDR = new ListedDR();

    
  
    protected void Page_Init(object sender, EventArgs e)
    {
        div_code = Session["div_code"].ToString();
        //menu1.FindControl("btnBack").Visible = false;

    }
    protected void Page_Load(object sender, EventArgs e)
    {
        sf_code = Session["sf_code"].ToString();
        sf_type = Session["sf_type"].ToString();
        div_code = Session["div_code"].ToString();

        if (sf_type == "3")
        {
            div_code = Session["div_code"].ToString();
        }
        else
        {
            div_code = Session["div_code"].ToString();
        }
        if (!Page.IsPostBack)
        {
            ServerStartTime = DateTime.Now;
            base.OnPreInit(e);
            submit1.Visible = false;
           
            if (Session["sf_type"].ToString() == "2")
            {
                div_code = Session["div_code"].ToString();
                UserControl_MGR_Menu c1 =
               (UserControl_MGR_Menu)LoadControl("~/UserControl/MGR_Menu.ascx");
                Divid.Controls.Add(c1);
                c1.Title = Page.Title;
                c1.FindControl("btnBack").Visible = false;
                //ddlFFType.Visible = false;

               
                FillMRManagers1();
                FillYear();
                FillColor();
            }

            else if (Session["sf_type"].ToString() == "" || Session["sf_type"].ToString() == "3")
            {
                div_code = Session["div_code"].ToString();
                if (div_code.Contains(','))
                {
                    div_code = div_code.Remove(div_code.Length - 1);
                }
                UserControl_MenuUserControl c1 =
               (UserControl_MenuUserControl)LoadControl("~/UserControl/MenuUserControl.ascx");
                Divid.Controls.Add(c1);
                c1.Title = Page.Title;
                //c1.FindControl("btnBack").Visible = false;              

                FillManagers();
                FillYear();
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
            else if (Session["sf_type"].ToString() == "" || Session["sf_type"].ToString() == "3")
            {
                div_code = Session["div_code"].ToString();
                if (div_code.Contains(','))
                {
                    div_code = div_code.Remove(div_code.Length - 1);
                }
                UserControl_MenuUserControl c1 =
               (UserControl_MenuUserControl)LoadControl("~/UserControl/MenuUserControl.ascx");
                Divid.Controls.Add(c1);
                c1.Title = Page.Title;
                //c1.FindControl("btnBack").Visible = false;

            }

        }
        FillColor();
    }

    private void FillMRManagers1()
    {
        SalesForce sf = new SalesForce();

        dsSalesForce = sf.SalesForceListMgrGet_MgrOnly(div_code, sf_code);
        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            ddlFieldForce.DataTextField = "sf_name";
            ddlFieldForce.DataValueField = "sf_code";
            ddlFieldForce.DataSource = dsSalesForce;
            ddlFieldForce.DataBind();

                      ddlSF.DataTextField = "des_color";
            ddlSF.DataValueField = "sf_code";
            ddlSF.DataSource = dsSalesForce;
            ddlSF.DataBind();
        }
        FillColor();


    }

    private void FillYear()
    {
        TourPlan tp = new TourPlan();
        dsTP = tp.Get_TP_Edit_Year(div_code);
        if (dsTP.Tables[0].Rows.Count > 0)
        {
            for (int k = Convert.ToInt16(dsTP.Tables[0].Rows[0]["Year"]); k <= DateTime.Now.Year + 1; k++)
            {
                ddlFYear.Items.Add(k.ToString());
            
                ddlFYear.SelectedValue = DateTime.Now.Year.ToString();
             
            }
        }

     
    }

    private void FillManagers()
    {
        SalesForce sf = new SalesForce();

        //if (ddlFFType.SelectedValue.ToString() == "1")
        //{
        //    ddlAlpha.Visible = false;
            dsSalesForce = sf.UserList_Hierarchy(div_code, "admin");
        //}
        //else if (ddlFFType.SelectedValue.ToString() == "0")
        //{
        //    FillSF_Alpha();


        //    ddlAlpha.Visible = true;
        //    dsSalesForce = sf.UserList_Alpha(div_code, "admin");
        //}

        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            ddlFieldForce.DataTextField = "sf_name";
            ddlFieldForce.DataValueField = "sf_code";
            ddlFieldForce.DataSource = dsSalesForce;
            ddlFieldForce.DataBind();

            ddlSF.DataTextField = "des_color";
            ddlSF.DataValueField = "sf_code";
            ddlSF.DataSource = dsSalesForce;
            ddlSF.DataBind();

        }
    }
    //private void FillSF_Alpha()
    //{
    //    SalesForce sf = new SalesForce();
    //    dsSalesForce = sf.getSalesForcelist_Alphabet(div_code);
    //    if (dsSalesForce.Tables[0].Rows.Count > 0)
    //    {
    //        ddlAlpha.DataTextField = "sf_name";
    //        ddlAlpha.DataValueField = "val";
    //        ddlAlpha.DataSource = dsSalesForce;
    //        ddlAlpha.DataBind();
    //        ddlAlpha.SelectedIndex = 0;
    //    }
    //}

    //protected void ddlAlpha_SelectedIndexChanged(object sender, EventArgs e)
    //{

    //    SalesForce sf = new SalesForce();
    //    dsSalesForce = sf.UserList_Alphasearch(div_code, "admin", ddlAlpha.SelectedValue);
    //    if (dsSalesForce.Tables[0].Rows.Count > 0)
    //    {
    //        ddlFieldForce.DataTextField = "sf_name";
    //        ddlFieldForce.DataValueField = "sf_code";
    //        ddlFieldForce.DataSource = dsSalesForce;
    //        ddlFieldForce.DataBind();

    //        ddlSF.DataTextField = "des_color";
    //        ddlSF.DataValueField = "sf_code";
    //        ddlSF.DataSource = dsSalesForce;
    //        ddlSF.DataBind();

    //    }
    //    FillColor();

    //}

    protected void ddlFFType_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblFF.Text = "Field Force";
        FillManagers();
        FillColor();
    }

    private void FillColor()
    {
        int j = 0;


        foreach (ListItem ColorItems in ddlSF.Items)
        {
          

            string bcolor = "#" + ColorItems.Text;
            ddlFieldForce.Items[j].Attributes.Add("style", "background-color:" + bcolor);

            j = j + 1;

        }
    }
    protected void BindGridd()
    {

        SalesForce sf = new SalesForce();

        DataSet ff = new DataSet();

        //ff = sf.sp_UserList_leave_Entitlement(div_code, ddlFieldForce.SelectedValue);

        ff = sf.sp_UserList_leave_Entitlement_New(div_code, ddlFieldForce.SelectedValue);



        if (ff.Tables.Count > 0)
        {
            if (ff.Tables[0].Rows.Count > 0)
            {

                grdLeave.DataSource = ff;
                grdLeave.DataBind();
             
                //DataList1.DataSource = ff;
                //DataList1.DataBind();
               

            }
        }






        foreach (GridViewRow item in grdLeave.Rows)
        {
            string scode = string.Empty;
            Label hd = (Label)item.FindControl("labelsf_code");
            scode = hd.Text;
            TextBox clday = item.FindControl("clcount") as TextBox;
            TextBox plday = item.FindControl("plcount") as TextBox;
            TextBox slday = item.FindControl("slcount") as TextBox;
            TextBox lopday = item.FindControl("lopcount") as TextBox;
            TextBox tlday = item.FindControl("tlcount") as TextBox;

            HiddenField hiddCL = item.FindControl("hiddCL") as HiddenField;
            HiddenField hiddPL = item.FindControl("hiddPL") as HiddenField;
            HiddenField hiddSL = item.FindControl("hiddSL") as HiddenField;
            HiddenField hiddLOP = item.FindControl("hiddLOP") as HiddenField;
            HiddenField hiddTL = item.FindControl("hiddTL") as HiddenField;

            Label clbal = item.FindControl("clbal") as Label;
            Label plbal = item.FindControl("plbal") as Label;
            Label slbal = item.FindControl("slbal") as Label;
            Label lopbal = item.FindControl("lopbal") as Label;
            Label tlbal = item.FindControl("tlbal") as Label;


            dsdoc = objSample.get_cl_days(scode, div_code, Convert.ToInt16(ddlFYear.SelectedValue));
            if (dsdoc.Tables[0].Rows.Count > 0)
            {
                clday.Text = dsdoc.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                hiddCL.Value = dsdoc.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                clbal.Text = dsdoc.Tables[0].Rows[0]["Leave_Balance_Days"].ToString();

            }
            dsdoc = objSample.get_pl_days(scode, div_code, Convert.ToInt16(ddlFYear.SelectedValue));
            if (dsdoc.Tables[0].Rows.Count > 0)
            {

                plday.Text = dsdoc.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                hiddPL.Value = dsdoc.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();

                plbal.Text = dsdoc.Tables[0].Rows[0]["Leave_Balance_Days"].ToString();
            }
            dsdoc = objSample.get_sl_days(scode, div_code, Convert.ToInt16(ddlFYear.SelectedValue));
            if (dsdoc.Tables[0].Rows.Count > 0)
            {

                slday.Text = dsdoc.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                hiddSL.Value = dsdoc.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                slbal.Text = dsdoc.Tables[0].Rows[0]["Leave_Balance_Days"].ToString();
            }
            dsdoc = objSample.get_lop_days(scode, div_code, Convert.ToInt16(ddlFYear.SelectedValue));
            if (dsdoc.Tables[0].Rows.Count > 0)
            {

                lopday.Text = dsdoc.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                hiddLOP.Value = dsdoc.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                lopbal.Text = dsdoc.Tables[0].Rows[0]["Leave_Balance_Days"].ToString();
            }

            dsdoc = objSample.get_tl_days(scode, div_code, Convert.ToInt16(ddlFYear.SelectedValue));
            if (dsdoc.Tables[0].Rows.Count > 0)
            {

                tlday.Text = dsdoc.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                hiddTL.Value = dsdoc.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                tlbal.Text = dsdoc.Tables[0].Rows[0]["Leave_Balance_Days"].ToString();
            }
        }
    }

    protected void btnGo_Click(object sender, EventArgs e)
    {

       
      
                string sURL = string.Empty;
                BindGridd();
                submit1.Visible = true;
               
            }

    protected void submit1_Click(object sender, EventArgs e)
    {

        string sfname = string.Empty;
        //string cl = string.Empty;
        //string pl = string.Empty;
        //string sl = string.Empty;
        //string lop = string.Empty;
        string type = string.Empty;
        string cltype = string.Empty;
        string sltype = string.Empty;
        string pltype = string.Empty;
        string loptype = string.Empty;
        string tltype = string.Empty;
        string prdtsampleunt = string.Empty;
        string code = string.Empty;

        string sf_emp_id = string.Empty;
        string strQry = string.Empty;
        int sl_no = -1;
        bool err = false;



        int CL_Value_Check;
        int PL_Value_Check;
        int SL_Value_Check;
        int LOP_Value_Check;
        int TL_Value_Check;

        foreach (GridViewRow item in this.grdLeave.Rows)
        {
            sfname = (item.FindControl("feildforcename") as Label).Text;
            //cl = (item.FindControl("clcount") as TextBox).Text;
            //pl = (item.FindControl("plcount") as TextBox).Text;
            //sl = (item.FindControl("slcount") as TextBox).Text;
            //lop = (item.FindControl("lopcount") as TextBox).Text;
            code = (item.FindControl("labelsf_code") as Label).Text;

            TextBox cl = item.FindControl("clcount") as TextBox;
            TextBox pl = item.FindControl("plcount") as TextBox;
            TextBox sl = item.FindControl("slcount") as TextBox;
            TextBox lop = item.FindControl("lopcount") as TextBox;
            TextBox tl = item.FindControl("tlcount") as TextBox;
            //Label hd = (Label)item.FindControl("labelsf_code");
            //  code = hd.Text;

            sf_emp_id = (item.FindControl("lblsf_emp_id") as Label).Text;

            HiddenField hiddCL = (HiddenField)item.FindControl("hiddCL");
            HiddenField hiddPL = (HiddenField)item.FindControl("hiddPL");
            HiddenField hiddSL = (HiddenField)item.FindControl("hiddSL");
            HiddenField hiddLOP = (HiddenField)item.FindControl("hiddLOP");
            HiddenField hiddTL = (HiddenField)item.FindControl("hiddTL");


            Label clbal = item.FindControl("clbal") as Label;
            Label plbal = item.FindControl("plbal") as Label;
            Label slbal = item.FindControl("slbal") as Label;
            Label lopbal = item.FindControl("lopbal") as Label;
            Label tlbal = item.FindControl("tlbal") as Label;

            if (hiddCL.Value == "")
            {
                hiddCL.Value = "0";
            }
            if (hiddPL.Value == "")
            {
                hiddPL.Value = "0";
            }
            if (hiddSL.Value == "")
            {
                hiddSL.Value = "0";
            }
            if (hiddLOP.Value == "")
            {
                hiddLOP.Value = "0";
            }

            if (hiddTL.Value == "")
            {
                hiddTL.Value = "0";
            }



            if (clbal.Text == "")
            {
                clbal.Text = "0";
            }

            if (plbal.Text == "")
            {
                plbal.Text = "0";
            }

            if (slbal.Text == "")
            {
                slbal.Text = "0";
            }

            if (lopbal.Text == "")
            {
                lopbal.Text = "0";
            }

            if (tlbal.Text == "")
            {
                tlbal.Text = "0";
            }

            if (cl.Text == "")
            {
                cl.Text = "0";
            }
            if (pl.Text == "")
            {
                pl.Text = "0";
            }

            if (sl.Text == "")
            {
                sl.Text = "0";
            }
            if (lop.Text == "")
            {
                lop.Text = "0";
            }

            if (tl.Text == "")
            {
                tl.Text = "0";
            }

            cl.BackColor = System.Drawing.Color.White;
            pl.BackColor = System.Drawing.Color.White;
            sl.BackColor = System.Drawing.Color.White;
            lop.BackColor = System.Drawing.Color.White;
            tl.BackColor = System.Drawing.Color.White;

            if (cl.Text.Length > 0 || pl.Text.Length > 0 || sl.Text.Length > 0 || lop.Text.Length > 0 || tl.Text.Length > 0)
            {

                CL_Value_Check = Convert.ToInt16(hiddCL.Value) - Convert.ToInt16(clbal.Text);
                PL_Value_Check = Convert.ToInt16(hiddPL.Value) - Convert.ToInt16(plbal.Text);
                SL_Value_Check = Convert.ToInt16(hiddSL.Value) - Convert.ToInt16(slbal.Text);
                LOP_Value_Check = Convert.ToInt16(hiddLOP.Value) - Convert.ToInt16(lopbal.Text);
                TL_Value_Check = Convert.ToInt16(hiddTL.Value) - Convert.ToInt16(tlbal.Text);

               // if ((hiddCL.Value != cl.Text) && (hiddCL.Value != clbal.Text))
                if ((Convert.ToInt16(cl.Text) < CL_Value_Check) && (hiddCL.Value != cl.Text) && (hiddCL.Value != clbal.Text))
                {

                    Page.ClientScript.RegisterStartupScript(this.GetType(), "SuccessMsg", "alert('Not able to Update CL!');", true);
                    cl.Focus();
                    cl.BackColor = System.Drawing.Color.Orchid;
                    err = true;
                    break;

                }
                //else if ((hiddPL.Value != pl.Text) && (hiddPL.Value != plbal.Text))
                else if ((Convert.ToInt16(pl.Text) < PL_Value_Check) && (hiddPL.Value != pl.Text) && (hiddPL.Value != plbal.Text))
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "SuccessMsg", "alert('Not able to Update PL!');", true);
                    pl.Focus();
                    pl.BackColor = System.Drawing.Color.Orchid;
                    err = true;
                    break;
                }

                //else if ((hiddSL.Value != sl.Text) && (hiddSL.Value != slbal.Text))
                else if ((Convert.ToInt16(sl.Text) < SL_Value_Check) && (hiddSL.Value != sl.Text) && (hiddSL.Value != slbal.Text))
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "SuccessMsg", "alert('Not able to Update SL!');", true);
                    sl.Focus();
                    sl.BackColor = System.Drawing.Color.Orchid;
                    err = true;
                    break;
                }

               // else if ((hiddLOP.Value != lop.Text) && (hiddLOP.Value != lopbal.Text))
                else if ((Convert.ToInt16(lop.Text) < LOP_Value_Check) && (hiddLOP.Value != lop.Text) && (hiddLOP.Value != lopbal.Text))
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "SuccessMsg", "alert('Not able to Update LOP!');", true);
                    lop.Focus();
                    lop.BackColor = System.Drawing.Color.Orchid;
                    err = true;
                    break;
                }

                else if ((Convert.ToInt16(tl.Text) < TL_Value_Check) && (hiddTL.Value != tl.Text) && (hiddTL.Value != tlbal.Text))
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "SuccessMsg", "alert('Not able to Update TL!');", true);
                    tl.Focus();
                    tl.BackColor = System.Drawing.Color.Orchid;
                    err = true;
                    break;
                }


            }
        }

        if (err == false)
        {

            foreach (GridViewRow item in this.grdLeave.Rows)
            {
                sfname = (item.FindControl("feildforcename") as Label).Text;
                //cl = (item.FindControl("clcount") as TextBox).Text;
                //pl = (item.FindControl("plcount") as TextBox).Text;
                //sl = (item.FindControl("slcount") as TextBox).Text;
                //lop = (item.FindControl("lopcount") as TextBox).Text;
                code = (item.FindControl("labelsf_code") as Label).Text;

                TextBox cl = item.FindControl("clcount") as TextBox;
                TextBox pl = item.FindControl("plcount") as TextBox;
                TextBox sl = item.FindControl("slcount") as TextBox;
                TextBox lop = item.FindControl("lopcount") as TextBox;
                TextBox tl = item.FindControl("tlcount") as TextBox;
                //Label hd = (Label)item.FindControl("labelsf_code");
                //  code = hd.Text;

                sf_emp_id = (item.FindControl("lblsf_emp_id") as Label).Text;

                HiddenField hiddCL = (HiddenField)item.FindControl("hiddCL");
                HiddenField hiddPL = (HiddenField)item.FindControl("hiddPL");
                HiddenField hiddSL = (HiddenField)item.FindControl("hiddSL");
                HiddenField hiddLOP = (HiddenField)item.FindControl("hiddLOP");
                HiddenField hiddTL = (HiddenField)item.FindControl("hiddTL");

                if (hiddCL.Value == "")
                {
                    hiddCL.Value = "0";
                }
                if (hiddPL.Value == "")
                {
                    hiddPL.Value = "0";
                }
                if (hiddSL.Value == "")
                {
                    hiddSL.Value = "0";
                }
                if (hiddLOP.Value == "")
                {
                    hiddLOP.Value = "0";
                }
                if (hiddTL.Value == "")
                {
                    hiddTL.Value = "0";
                }


                Label clbal = item.FindControl("clbal") as Label;
                Label plbal = item.FindControl("plbal") as Label;
                Label slbal = item.FindControl("slbal") as Label;
                Label lopbal = item.FindControl("lopbal") as Label;
                Label tlbal = item.FindControl("tlbal") as Label;

                if (clbal.Text == "")
                {
                    clbal.Text = "0";
                }

                if (plbal.Text == "")
                {
                    plbal.Text = "0";
                }

                if (slbal.Text == "")
                {
                    slbal.Text = "0";
                }

                if (lopbal.Text == "")
                {
                    lopbal.Text = "0";
                }
                if (tlbal.Text == "")
                {
                    tlbal.Text = "0";
                }


                if (cl.Text.Length > 0 || pl.Text.Length > 0 || sl.Text.Length > 0 || lop.Text.Length > 0 || tl.Text.Length > 0)
                {

                    DB_EReporting db = new DB_EReporting();
                    strQry = "SELECT Sl_No FROM Trans_Leave_Entitle_Head where sf_code='" + code + "' and sf_emp_id='" + sf_emp_id + "' and active_flag=0 and Trans_Year='" + ddlFYear.SelectedValue + "' and Division_code='" + div_code + "' ";
                    sl_no = db.Exec_Scalar(strQry);


                    if (sl_no > 0)
                    {
                        cltype = "CL";
                        pltype = "PL";
                        sltype = "SL";
                        loptype = "LOP";
                        tltype = "TL";


                        CL_Value_Check = Convert.ToInt16(hiddCL.Value) - Convert.ToInt16(clbal.Text);
                        PL_Value_Check = Convert.ToInt16(hiddPL.Value) - Convert.ToInt16(plbal.Text);
                        SL_Value_Check = Convert.ToInt16(hiddSL.Value) - Convert.ToInt16(slbal.Text);
                        LOP_Value_Check = Convert.ToInt16(hiddLOP.Value) - Convert.ToInt16(lopbal.Text);
                        TL_Value_Check = Convert.ToInt16(hiddTL.Value) - Convert.ToInt16(tlbal.Text);

                        if (cl.Text.Length > 0)
                        {

                            //if (Convert.ToInt16(hiddCL.Value) > Convert.ToInt16(clbal.Text))
                            //{
                            //    objSample.RecordDetails_Leave_Entitle_Update(sl_no, code, cltype, cl.Text, div_code, "1");
                            //}
                            //else
                            //{

                            objSample.RecordDetails_Leave_Entitle_Update(sl_no, code, cltype, cl.Text, div_code, "2", CL_Value_Check);
                            //}
                        }
                        if (pl.Text.Length > 0)
                        {

                            //if (Convert.ToInt16(hiddPL.Value) > Convert.ToInt16(plbal.Text))
                            //{
                            //    objSample.RecordDetails_Leave_Entitle_Update(sl_no, code, pltype, pl.Text, div_code, "1");
                            //}
                            //else
                            //{
                            objSample.RecordDetails_Leave_Entitle_Update(sl_no, code, pltype, pl.Text, div_code, "2", PL_Value_Check);
                            //}
                        }
                        if (sl.Text.Length > 0)
                        {
                            //if (Convert.ToInt16(hiddSL.Value) > Convert.ToInt16(slbal.Text))
                            //{

                            //    objSample.RecordDetails_Leave_Entitle_Update(sl_no, code, sltype, sl.Text, div_code, "1");
                            //}
                            //else
                            //{
                            objSample.RecordDetails_Leave_Entitle_Update(sl_no, code, sltype, sl.Text, div_code, "2", SL_Value_Check);
                          //  }
                        }
                        if (lop.Text.Length > 0)
                        {
                            //if (Convert.ToInt16(hiddLOP.Value) > Convert.ToInt16(lopbal.Text))
                            //{
                            //    objSample.RecordDetails_Leave_Entitle_Update(sl_no, code, loptype, lop.Text, div_code, "1");
                            //}
                            //else
                            //{
                            objSample.RecordDetails_Leave_Entitle_Update(sl_no, code, loptype, lop.Text, div_code, "2", LOP_Value_Check);
                            //}
                        }

                        if (tl.Text.Length > 0)
                        {
                            //if (Convert.ToInt16(hiddLOP.Value) > Convert.ToInt16(lopbal.Text))
                            //{
                            //    objSample.RecordDetails_Leave_Entitle_Update(sl_no, code, loptype, lop.Text, div_code, "1");
                            //}
                            //else
                            //{
                            objSample.RecordDetails_Leave_Entitle_Update(sl_no, code, tltype, tl.Text, div_code, "2", TL_Value_Check);
                            //}
                        }
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "SuccessMsg", "alert('Saved Sucessfully!');", true);
                    }


                    else
                    {
                        string output = objSample.RecordHeadAdd_Leave_Entitle(code, div_code, ddlFYear.SelectedValue, sf_emp_id);
                        if (output != "0")
                        {

                            cltype = "CL";
                            pltype = "PL";
                            sltype = "SL";
                            loptype = "LOP";
                            tltype = "TL";


                            if (cl.Text.Length > 0)
                            {
                                objSample.RecordDetailsAdd_Leave_Entitle(output, code, cltype, cl.Text, div_code);
                            }
                            if (pl.Text.Length > 0)
                            {
                                objSample.RecordDetailsAdd_Leave_Entitle(output, code, pltype, pl.Text, div_code);
                            }
                            if (sl.Text.Length > 0)
                            {
                                objSample.RecordDetailsAdd_Leave_Entitle(output, code, sltype, sl.Text, div_code);
                            }
                            if (lop.Text.Length > 0)
                            {
                                objSample.RecordDetailsAdd_Leave_Entitle(output, code, loptype, lop.Text, div_code);
                            }

                            if (tl.Text.Length > 0)
                            {
                                objSample.RecordDetailsAdd_Leave_Entitle(output, code, tltype, tl.Text, div_code);
                            }

                            Page.ClientScript.RegisterStartupScript(this.GetType(), "SuccessMsg", "alert('Saved Sucessfully!');", true);
                          //  grdLeave.Visible = false;

                        }

                    }
                }
               

            }

        }


        //this.ClearControls();
    }
    protected void grdLeave_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            GridView objGridView = (GridView)sender;
            GridViewRow objgridviewrow = new GridViewRow(1, 0, DataControlRowType.Header, DataControlRowState.Insert);
            //Creating a table cell object
            TableCell objtablecell = new TableCell();

            GridViewRow objgridviewrow1 = new GridViewRow(2, 0, DataControlRowType.Header, DataControlRowState.Insert);
            //Creating a table cell object
            TableCell objtablecell1 = new TableCell();


            AddMergedCells(objgridviewrow, objtablecell, 0, "#", "#F1F5F8", true);
            AddMergedCells(objgridviewrow, objtablecell, 0, "Field Force Name", "#F1F5F8", true);
            AddMergedCells(objgridviewrow, objtablecell, 0, "HQ", "#F1F5F8", true);
            AddMergedCells(objgridviewrow, objtablecell, 0, "Designation", "#F1F5F8", true);
            AddMergedCells(objgridviewrow, objtablecell, 0, "Employee Code", "#F1F5F8", true);
            AddMergedCells(objgridviewrow, objtablecell, 0, "Date of Joining", "#F1F5F8", true);

            AddMergedCells(objgridviewrow, objtablecell, 5, "Leave Balance", "#F1F5F8", true);
            AddMergedCells(objgridviewrow1, objtablecell1, 0, "CL", "#BEC8F0", false);
            AddMergedCells(objgridviewrow1, objtablecell1, 0, "PL", "#BEC8F0", false);
            AddMergedCells(objgridviewrow1, objtablecell1, 0, "SL", "#BEC8F0", false);
            AddMergedCells(objgridviewrow1, objtablecell1, 0, "LOP", "#BEC8F0", false);
            AddMergedCells(objgridviewrow1, objtablecell1, 0, "TL", "#BEC8F0", false);


            AddMergedCells(objgridviewrow, objtablecell, 5, "Leave Eligibility", "#F1F5F8", true);
            AddMergedCells(objgridviewrow1, objtablecell1, 0, "CL", "#BEC8F0", false);
            AddMergedCells(objgridviewrow1, objtablecell1, 0, "PL", "#BEC8F0", false);
            AddMergedCells(objgridviewrow1, objtablecell1, 0, "SL", "#BEC8F0", false);
            AddMergedCells(objgridviewrow1, objtablecell1, 0, "LOP", "#BEC8F0", false);
            AddMergedCells(objgridviewrow1, objtablecell1, 0, "TL", "#BEC8F0", false);


            objGridView.Controls[0].Controls.AddAt(0, objgridviewrow);
            objGridView.Controls[0].Controls.AddAt(1, objgridviewrow1);
        }
    }

    protected void AddMergedCells(GridViewRow objgridviewrow, TableCell objtablecell, int colspan, string celltext, string backcolor, bool bRowspan)
    {
        objtablecell = new TableCell();
        objtablecell.Text = celltext;
        objtablecell.ColumnSpan = colspan;
        if ((colspan == 0) && bRowspan)
        {
            objtablecell.RowSpan = 2;
        }
        objtablecell.Style.Add("background-color", backcolor);
        objtablecell.Style.Add("color", "#636d73");
        objtablecell.Style.Add("font-weight", "401");
        //objtablecell.Style.Add("BorderWidth", "1px");
        // objtablecell.Style.Add("BorderStyle", "solid");
        // objtablecell.Style.Add("BorderColor", "Black");

        objtablecell.HorizontalAlign = HorizontalAlign.Center;
        objtablecell.Wrap = false;
        objgridviewrow.Cells.Add(objtablecell);
    }



    }

  



