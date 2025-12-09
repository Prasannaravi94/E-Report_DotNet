using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Bus_EReport;
using System.Data;
using System.Web.UI.HtmlControls;
using System.Web.Services;


public partial class MasterFiles_ActivityReports_Doctobusinessentry : System.Web.UI.Page
{
    string sfCode = string.Empty;
    string state_code = string.Empty;
    DCRBusinessEntry objDCRBusiness = new DCRBusinessEntry();
    DataSet dsDoc = null;
    DataSet dsdoc = null;
    DataSet ff = null;
    DataSet st = null;
    int search = 0;
    int time = 0;
    string hh;
    Territory objTerritory = new Territory();
    DataSet dsSalesForce = null;
    ListedDR lstDR = new ListedDR();
    DataSet dsTP = null;
    DataSet dsListedDR = null;

    string div_code = string.Empty;
    protected void Page_Init(object sender, EventArgs e)
    {
        div_code = Session["div_code"].ToString();
        //menu1.FindControl("btnBack").Visible = false;

    }
    protected void Page_Load(object sender, EventArgs e)
    {


        foreach (DataListItem item in DataList1.Items)
        {
            Button sh = (item.FindControl("btnShow")) as Button;
            sh.Attributes.Add("onchange", "return Show()");
        }
        sfCode = Session["sf_code"].ToString();
        if (!Page.IsPostBack)
        {


            if (Session["sf_type"].ToString() == "1")
            {
                div_code = Session["div_code"].ToString();
                UserControl_MR_Menu c1 =
               (UserControl_MR_Menu)LoadControl("~/UserControl/MR_Menu.ascx");
                Divid.Controls.Add(c1);
                c1.Title = Page.Title;
                c1.FindControl("btnBack").Visible = false;

                lblType.Visible = false;
                Panel1.Visible = false;
                ddlSrch.Visible = false;
                totval.Visible = false;
                totalval.Visible = false;
                tot.Visible = false;
                submit2.Visible = false;
                submit1.Visible = false;
                FillMRManagers();


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
                c1.FindControl("btnBack").Visible = false;

                lblType.Visible = false;
                Panel1.Visible = false;
                ddlSrch.Visible = false;
                totval.Visible = false;
                totalval.Visible = false;
                tot.Visible = false;
                submit2.Visible = false;
                submit1.Visible = false;
                this.FillMasterList_adm();

            }


            TourPlan tp = new TourPlan();
            dsTP = tp.Get_TP_Edit_Year(div_code);
            if (dsTP.Tables[0].Rows.Count > 0)
            {
                for (int k = Convert.ToInt16(dsTP.Tables[0].Rows[0]["Year"]); k <= DateTime.Now.Year + 1; k++)
                {
                    ddlYear.Items.Add(k.ToString());
                    ddlYear.SelectedValue = DateTime.Now.Year.ToString();
                    ddlMonth.SelectedValue = DateTime.Now.AddMonths(-1).Month.ToString();

                }
            }

        }
          else
        {
            if (Session["sf_type"].ToString() == "1")
            {
                div_code = Session["div_code"].ToString();
                UserControl_MR_Menu c1 =
               (UserControl_MR_Menu)LoadControl("~/UserControl/MR_Menu.ascx");
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
                c1.FindControl("btnBack").Visible = false;

            }
        }

        
    }


    private void FillMasterList()
    {
        SalesForce sf = new SalesForce();
        Doctor objDoctor = new Doctor();

        dsSalesForce = sf.UserList_getMRalpha(div_code, sfCode);

        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            ddlFieldForce.DataTextField = "sf_name";
            ddlFieldForce.DataValueField = "sf_code";
            ddlFieldForce.DataSource = dsSalesForce;
            ddlFieldForce.DataBind();
            ddlFieldForce.Items.Insert(0, new ListItem("--Select--", ""));
        }



    }

    private void FillMasterList_adm()
    {
        SalesForce sf = new SalesForce();
        Doctor objDoctor = new Doctor();

        dsSalesForce = sf.UserList_getMRalpha(div_code, "admin");

        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            ddlFieldForce.DataTextField = "sf_name";
            ddlFieldForce.DataValueField = "sf_code";
            ddlFieldForce.DataSource = dsSalesForce;
            ddlFieldForce.DataBind();
            ddlFieldForce.Items.Insert(0, new ListItem("--Select--", ""));
        }



    }


    protected void BindGridd()
    {

        ListedDR gg = new ListedDR();
        DataSet ff = new DataSet();

        ff = gg.getDoctorDetailsBysfName(div_code, ddlFieldForce.SelectedValue);


        if (ff.Tables.Count > 0)
        {
            if (ff.Tables[0].Rows.Count > 0)
            {


                DataList1.DataSource = ff;
                DataList1.DataBind();


            }
            else
            {
                NoRecord.Visible = true;
                Panel1.Visible = false;
                lblType.Visible = false;
                ddlSrch.Visible = false;
                submit2.Visible = false;
                submit1.Visible = false;
                totval.Visible = false;
                totalval.Visible = false;
                tot.Visible = false;
            }
        }
    }






    protected void btnSubmit_Click(object sender, EventArgs e)
    {


        string tt = string.Empty;

        Product objProduct = new Product();
        DataSet dsProducts = null;
        dsProducts = objProduct.getProdstatedoctor(div_code, sfCode);
        string doctorname = string.Empty;
        string doccode = string.Empty;

        foreach (DataListItem item in DataList1.Items)
        {

            doctorname = (item.FindControl("lblInput") as Label).Text;
            HiddenField hd = (HiddenField)item.FindControl("HiddenField1");
            doccode = hd.Value;
            GridView gv = item.FindControl("GridView1") as GridView;
            int str = gv.Rows.Count;
            for (int j = 0; j < str; j++)
            {

                string quan = ((TextBox)gv.Rows[j].FindControl("txtprice")).Text;
                hh += quan;
                if (hh == "")
                {
                    hh = "0";
                }

            }
            if (hh == "0")
            {
                string r = objDCRBusiness.RecordbusiHeadAddentry(ddlFieldForce.SelectedValue, div_code, ddlMonth.SelectedValue, ddlYear.SelectedValue, doccode);

            }


            if (hh != "0")
            {



                int strCount = gv.Rows.Count;

                string output = objDCRBusiness.RecordbusiHeadAdd(ddlFieldForce.SelectedValue, div_code, ddlMonth.SelectedValue, ddlYear.SelectedValue, doccode);
                if (output != "0")
                {
                    for (int i = 0; i < strCount; i++)
                    {
                        string rt = ((Label)gv.Rows[i].FindControl("detailname")).Text.Trim();
                        string retail = ((Label)gv.Rows[i].FindControl("product")).Text.Trim();
                        string t = ((TextBox)gv.Rows[i].FindControl("txtprice")).Text;



                        if (t != "")
                        {

                            Int32 val1 = Convert.ToInt32(t);
                            Int32 val2 = Convert.ToInt32(retail);
                            Int32 val3 = val1 * val2;
                            objDCRBusiness.RecordDetailsAdddoctorbusiness(output, ddlFieldForce.SelectedValue, div_code, doccode, rt, t, retail, val3);


                            item.BackColor = System.Drawing.Color.LightYellow;



                            TextBox value = item.FindControl("fsum") as TextBox;

                            dsdoc = objDCRBusiness.getprice(ddlFieldForce.SelectedValue, div_code, doccode, ddlYear.SelectedValue, ddlMonth.SelectedValue);
                            if (dsdoc.Tables[0].Rows.Count > 0)
                            {
                                value.Text = dsdoc.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();


                            }

                        }
                        hh = "0";
                    }

                }

            }


        }
        Page.ClientScript.RegisterStartupScript(this.GetType(), "SuccessMsg", "alert('Doctor Business Entry Saved Successfully!');", true); 

    }
    protected void dlList_ItemCommand(object source, DataListCommandEventArgs e)
    {


    }



    protected void gri()
    {
        SalesForce sff = new SalesForce();

        st = sff.CheckStatecode(ddlFieldForce.SelectedValue);
        if (st.Tables[0].Rows.Count > 0)
        {
            state_code = st.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();


        }

        foreach (DataListItem item in DataList1.Items)
        {

            string dc;
            HiddenField hd = (HiddenField)item.FindControl("HiddenField1");
            dc = hd.Value;
            string nc;
            nc = (item.FindControl("lblInput") as Label).Text;
            Label dn = item.FindControl("docname") as Label;
            dn.Text = nc;

            Product objProduct = new Product();
            DataSet dsProducts = null;

            TextBox value = item.FindControl("fsum") as TextBox;
            TextBox grid = item.FindControl("gridsum") as TextBox;
            TextBox sale = item.FindControl("Label4") as TextBox;
            dsdoc = objDCRBusiness.getprice(ddlFieldForce.SelectedValue, div_code, dc, ddlYear.SelectedValue, ddlMonth.SelectedValue);
            if (dsdoc.Tables[0].Rows.Count > 0)
            {
                value.Text = dsdoc.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                grid.Text = dsdoc.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                if (value.Text != "")
                {
                    sale.Text = "Completed";
                    item.BackColor = System.Drawing.Color.LightYellow;
                }


            }
            ff = objDCRBusiness.getvalue(ddlFieldForce.SelectedValue, div_code, dc, ddlYear.SelectedValue, ddlMonth.SelectedValue);
            if (ff.Tables[0].Rows.Count > 0)
            {
                totval.Text = ff.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                tot.Text = ff.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();



            }


            dsProducts = objProduct.getProdstadoctor(div_code, state_code);

            if (dsProducts.Tables.Count > 0)
            {
                if (dsProducts.Tables[0].Rows.Count > 0)
                {



                    GridView gv = item.FindControl("GridView1") as GridView;


                    gv.DataSource = dsProducts;

                    gv.DataBind();
                    //dsdoc = objProduct.getretailprice(ddlFieldForce.SelectedValue, div_code, dc, ddlYear.SelectedValue, ddlMonth.SelectedValue, rt);
                    //if (dsdoc.Tables[0].Rows.Count > 0)
                    //{
                    //    ((Label)gv.Rows[i].FindControl("Retailor_Price")).Text = dsdoc.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();

                    //}
                    int trans = objDCRBusiness.RecordheadExistgrid(ddlFieldForce.SelectedValue, div_code, ddlMonth.SelectedValue, ddlYear.SelectedValue, dc);
                    if (trans > 0)
                    {
                        int strCount1 = gv.Rows.Count;
                        if (strCount1 > 0)
                        {
                            for (int i = 0; i < strCount1; i++)
                            {

                                string rt = ((Label)gv.Rows[i].FindControl("detailname")).Text.Trim();

                                dsdoc = objProduct.uu(ddlFieldForce.SelectedValue, div_code, dc, ddlYear.SelectedValue, ddlMonth.SelectedValue, rt);
                                if (dsdoc.Tables[0].Rows.Count > 0)
                                {
                                    ((TextBox)gv.Rows[i].FindControl("txtprice")).Text = dsdoc.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();

                                }
                                dsdoc = objProduct.uuu(ddlFieldForce.SelectedValue, div_code, dc, ddlYear.SelectedValue, ddlMonth.SelectedValue, rt);
                                if (dsdoc.Tables[0].Rows.Count > 0)
                                {
                                    ((TextBox)gv.Rows[i].FindControl("ffvalue")).Text = dsdoc.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();

                                }
                            }


                        }
                    }

                }
            }
        }
    }




    protected void ddlFieldForce_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void btnGo_Click(object sender, EventArgs e)
    {

        if (ddlFieldForce.SelectedIndex > -1)
        {

            ddlFieldForce.Enabled = false;

            ddlFieldForce.Attributes.Add("title", "Disabled!! Click clear list to enable");

            ddlFieldForce.Style["cursor"] = "not-allowed";
        }
        Panel1.Visible = true;
        submit2.Visible = true;
        submit1.Visible = true;
        totval.Visible = true;
        tot.Visible = true;
        totalval.Visible = true;
        lblType.Visible = true;
        ddlSrch.Visible = true;
        DataList1.Visible = true;
        this.BindGridd();
        this.gri();
    }




    protected void DataList1_SelectedIndexChanged(object sender, EventArgs e)
    {




    }
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        if (ddlFieldForce.SelectedIndex > -1)
        {
            ddlFieldForce.Enabled = true;
            ddlFieldForce.Attributes.Add("title", "select Fieldforce");
            ddlFieldForce.Style["cursor"] = "pointer";
        }

     
        ddlFieldForce.SelectedIndex = -1;

        ddlYear.SelectedIndex = 0;
        ddlMonth.SelectedIndex = 0;
        DataList1.Visible = false;
        submit2.Visible = false;
        submit1.Visible = false;
        totval.Visible = false;
        totalval.Visible = false;
        tot.Visible = false;
        NoRecord.Visible = false;
        Panel1.Visible = false;

        ddlYear.SelectedValue = DateTime.Now.Year.ToString();
        ddlMonth.SelectedValue = DateTime.Now.AddMonths(-1).Month.ToString();

    }
    protected void ddlSrch_SelectedIndexChanged(object sender, EventArgs e)
    {
        search = Convert.ToInt32(ddlSrch.SelectedValue);
        ListedDR DFF = new ListedDR();

        if (search == 1)
        {
            txtsearch.Visible = false;
            ddlSrc2.Visible = false;
            Btnsrc.Visible = true;
            ddlSrc2.Visible = false;
            dsDoc = DFF.getDoctorDetailsBysvlno(div_code, ddlFieldForce.SelectedValue);
            if (dsDoc.Tables[0].Rows.Count > 0)
            {
                DataList1.Visible = true;
                submit2.Visible = true;
                submit1.Visible = true;
                DataList1.DataSource = dsDoc;
                DataList1.DataBind();
                this.gri();
            }
            else
            {
                submit2.Visible = false;
                submit1.Visible = false;
                totval.Visible = false;
                totalval.Visible = false;
                tot.Visible = false;
                DataList1.DataSource = dsDoc;
                DataList1.DataBind();
                this.gri();
            }

        }
        if (search == 2)
        {
            FillSpl();
            ddlSrc2.Visible = true;
            Btnsrc.Visible = true;
            txtsearch.Visible = false;
            dsDoc = DFF.getDoctorDetailsByspeciality(div_code, ddlFieldForce.SelectedValue);
            if (dsDoc.Tables[0].Rows.Count > 0)
            {
                DataList1.Visible = true;
                submit2.Visible = true;
                submit1.Visible = true;
                totval.Visible = true;
                totalval.Visible = true;
                tot.Visible = true;
                DataList1.DataSource = dsDoc;
                DataList1.DataBind();
                this.gri();
            }
            else
            {
                submit2.Visible = false;
                submit1.Visible = false;
                totval.Visible = false;
                totalval.Visible = false;
                tot.Visible = false;
                DataList1.DataSource = dsDoc;
                DataList1.DataBind();
                this.gri();
            }
        }
        if (search == 3)
        {
            FillCat();
            ddlSrc2.Visible = true;
            Btnsrc.Visible = true;
            txtsearch.Visible = false;
            dsDoc = DFF.getDoctorDetailsBycatogory(div_code, ddlFieldForce.SelectedValue);
            if (dsDoc.Tables[0].Rows.Count > 0)
            {
                DataList1.Visible = true;
                submit2.Visible = true;
                submit1.Visible = true;
                totval.Visible = true;
                totalval.Visible = true;
                tot.Visible = true;
                DataList1.DataSource = dsDoc;
                DataList1.DataBind();
                this.gri();
            }
            else
            {
                submit2.Visible = false;
                submit1.Visible = false;
                totval.Visible = false;
                totalval.Visible = false;
                tot.Visible = false;
                DataList1.DataSource = dsDoc;
                DataList1.DataBind();
                this.gri();
            }
        }
        if (search == 4)
        {
            txtsearch.Visible = true;
            ddlSrc2.Visible = false;
            ddlSrc2.SelectedIndex = 0;
            Btnsrc.Visible = true;
            ddlSrc2.Visible = false;
            txtsearch.Visible = true;
            dsDoc = DFF.getDoctorDetailsBydoctorname(div_code, ddlFieldForce.SelectedValue);
            if (dsDoc.Tables[0].Rows.Count > 0)
            {
                DataList1.Visible = true;
                submit2.Visible = true;
                submit1.Visible = true;
                totval.Visible = true;
                totalval.Visible = true;
                tot.Visible = true;
                DataList1.DataSource = dsDoc;
                DataList1.DataBind();
                this.gri();
            }
            else
            {
                submit2.Visible = false;
                submit1.Visible = false;
                totval.Visible = false;
                totalval.Visible = false;
                tot.Visible = false;
                DataList1.DataSource = dsDoc;
                DataList1.DataBind();
                this.gri();
            }

        }
        if (search == 5)
        {
            FillTerritory();
            ddlSrc2.Visible = true;
            Btnsrc.Visible = true;
            txtsearch.Visible = false;
            dsDoc = DFF.getDoctorDetailsBysubarea(div_code, ddlFieldForce.SelectedValue);
            if (dsDoc.Tables[0].Rows.Count > 0)
            {
                DataList1.Visible = true;
                submit2.Visible = true;
                submit1.Visible = true;
                totval.Visible = true;
                totalval.Visible = true;
                tot.Visible = true;
                DataList1.DataSource = dsDoc;
                DataList1.DataBind();
                this.gri();
            }
            else
            {
                submit2.Visible = false;
                submit1.Visible = false;
                totval.Visible = false;
                totalval.Visible = false;
                tot.Visible = false;
                DataList1.DataSource = dsDoc;
                DataList1.DataBind();
                this.gri();
            }

        }


    }
    private void FillCat()
    {
        ListedDR lstDR = new ListedDR();
        dsListedDR = lstDR.FetchCategory(ddlFieldForce.SelectedValue);
        if (dsListedDR.Tables[0].Rows.Count > 0)
        {
            ddlSrc2.DataTextField = "Doc_Cat_SName";
            ddlSrc2.DataValueField = "Doc_Cat_Code";
            ddlSrc2.DataSource = dsListedDR;
            ddlSrc2.DataBind();
        }

    }
    private void FillSpl()
    {
        ListedDR lstDR = new ListedDR();
        dsListedDR = lstDR.FetchSpeciality(ddlFieldForce.SelectedValue);
        if (dsListedDR.Tables[0].Rows.Count > 0)
        {
            ddlSrc2.DataTextField = "Doc_Special_SName";
            ddlSrc2.DataValueField = "Doc_Special_Code";
            ddlSrc2.DataSource = dsListedDR;
            ddlSrc2.DataBind();
        }

    }


    private void FillTerritory()
    {
        ListedDR lstDR = new ListedDR();
        dsListedDR = lstDR.FetchTerritory(ddlFieldForce.SelectedValue);
        if (dsListedDR.Tables[0].Rows.Count > 0)
        {
            ddlSrc2.DataTextField = "Territory_Name";
            ddlSrc2.DataValueField = "Territory_Code";
            ddlSrc2.DataSource = dsListedDR;
            ddlSrc2.DataBind();
        }

    }

    protected void ddlSrc2_SelectedIndexChanged(object sender, EventArgs e)
    {

    }


    protected void Btnsrc_Click(object sender, EventArgs e)
    {
        ListedDR LstDoc = new ListedDR();
        System.Threading.Thread.Sleep(time);
        search = Convert.ToInt32(ddlSrch.SelectedValue);
        string sub = ddlSrc2.SelectedValue;
        if (sub != "0")
        {


            if (search == 2)
            {
                dsDoc = LstDoc.getListedDrforSpl(ddlFieldForce.SelectedValue, ddlSrc2.SelectedValue);
                if (dsDoc.Tables[0].Rows.Count > 0)
                {
                    DataList1.Visible = true;
                    submit2.Visible = true;
                    submit1.Visible = true;
                    totval.Visible = true;
                    totalval.Visible = true;
                    tot.Visible = true;
                    DataList1.DataSource = dsDoc;
                    DataList1.DataBind();
                    this.gri();
                }
                else
                {
                    submit2.Visible = false;
                    submit1.Visible = false;
                    tot.Visible = false;
                    totalval.Visible = false;
                    totval.Visible = false;
                    DataList1.DataSource = dsDoc;
                    DataList1.DataBind();
                    this.gri();
                }
            }
            if (search == 3)
            {
                dsDoc = LstDoc.getListedDrforCat(ddlFieldForce.SelectedValue, ddlSrc2.SelectedValue);
                if (dsDoc.Tables[0].Rows.Count > 0)
                {
                    DataList1.Visible = true;
                    submit2.Visible = true;
                    submit1.Visible = true;
                    totval.Visible = true;
                    totalval.Visible = true;
                    tot.Visible = true;
                    DataList1.DataSource = dsDoc;
                    DataList1.DataBind();
                    this.gri();
                }
                else
                {
                    submit2.Visible = false;
                    submit1.Visible = false;
                    DataList1.DataSource = dsDoc;
                    DataList1.DataBind();
                    this.gri();
                }
            }

            if (search == 5)
            {
                dsDoc = LstDoc.getListedDrforTerr(ddlFieldForce.SelectedValue, ddlSrc2.SelectedValue);
                if (dsDoc.Tables[0].Rows.Count > 0)
                {
                    DataList1.Visible = true;
                    submit2.Visible = true;
                    submit1.Visible = true;
                    totval.Visible = true;
                    totalval.Visible = true;
                    tot.Visible = true;
                    DataList1.DataSource = dsDoc;
                    DataList1.DataBind();
                    this.gri();
                }
                else
                {
                    submit2.Visible = false;
                    submit1.Visible = false;
                    tot.Visible = false;
                    totalval.Visible = false;
                    totval.Visible = false;
                    DataList1.DataSource = dsDoc;
                    DataList1.DataBind();
                    this.gri();
                }
            }

            //End 
        }
        else if (txtsearch.Text != "" && sub == "0")
        {
            if (search == 4)
            {
                ddlSrc2.Visible = false;
                txtsearch.Visible = true;
                dsDoc = LstDoc.getListedDrforName(ddlFieldForce.SelectedValue, txtsearch.Text);
                if (dsDoc.Tables[0].Rows.Count > 0)
                {
                    DataList1.Visible = true;
                    submit2.Visible = true;
                    submit1.Visible = true;
                    tot.Visible = true;
                    totalval.Visible = true;
                    totval.Visible = true;
                    DataList1.DataSource = dsDoc;
                    DataList1.DataBind();
                    this.gri();
                }
                else
                {
                    submit2.Visible = false;
                    submit1.Visible = false;
                    tot.Visible = false;
                    totalval.Visible = false;
                    totval.Visible = false;
                    DataList1.DataSource = dsDoc;
                    DataList1.DataBind();
                    this.gri();
                }
            }

        }
        else
        {
            //tot.Visible = false;
            //totalval.Visible = false;
            //totval.Visible = false;
        }

    }

    private void FillMRManagers()
    {
        SalesForce sf = new SalesForce();
        dsSalesForce = sf.SalesForceListMgrGet(div_code, sfCode);
        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            ddlFieldForce.DataTextField = "sf_name";
            ddlFieldForce.DataValueField = "sf_code";
            ddlFieldForce.DataSource = dsSalesForce;
            ddlFieldForce.DataBind();

        }
    }


}

