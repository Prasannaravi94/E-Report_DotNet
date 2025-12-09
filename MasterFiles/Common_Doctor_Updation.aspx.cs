using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;
using System.IO;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

public partial class MasterFiles_Common_Doctor_Updation : System.Web.UI.Page
{
    DataSet dsListedDR = null;
    DataSet dsdiv = null;

    string div_code = string.Empty;
    string strdiv_code = string.Empty;
    string sf_type = string.Empty;
    string Common_DR_Code = string.Empty;
    string strsf_code = string.Empty;
    string str_terrcode = string.Empty;
    string AllocateIds = string.Empty;
    string Allocate = string.Empty;
    ListItem liTerr = new ListItem();
    DataTable dtDoctor;
    DataTable dtCatg = new DataTable();
    DataTable dtDoctor1;
    DataTable dtDoctor2;
    DataTable dtDoctor3;
    DataTable dtDoctor4;
    DataTable dtDoctor5;
    DataTable dtDoctor6;
    DataSet dsTP = null;
    string[] sfCode;
    string[] terrCode;
    int maxrows = 0;
    int currow = 0;
    int i = 0;
    int request_type = -1;
    int iIndex;
    int iIndexterr;
    int request_doctor = -1;
    protected void Page_Load(object sender, EventArgs e)
    {
        sf_type = Session["sf_type"].ToString();
        if (sf_type == "3")
        {
            div_code = Session["division_code"].ToString();
        }
        else
        {
            div_code = Session["div_code"].ToString();
        }

        if (div_code.Contains(','))
        {
            div_code = div_code.Remove(div_code.Length - 1);
        }

        //  div_code = Session["div_code"].ToString();
        Division dv = new Division();
        // //// menu1.FindControl("btnBack").Visible = false;
        menu1.Title = Page.Title;
        Session["backurl"] = "Common_Doctor_List.aspx";
        string[] strDivSplit = div_code.Split(',');
        foreach (string strdiv in strDivSplit)
        {
            if (strdiv != "")
            {
                dsdiv = dv.getDivisionHO(strdiv);

                liTerr.Value = dsdiv.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                liTerr.Text = dsdiv.Tables[0].Rows[0].ItemArray.GetValue(1).ToString();
                //  ddlDivision.Items.Add(liTerr);

                break;
            }
        }

        if (!Page.IsPostBack)
        {

            FillCategory();
            FillSpeciality();
            FillClass();
            FillQualification();
            FillDoc();
            // FillDiv();
            if (Request.QueryString["type"] != null)
            {
                if ((Request.QueryString["type"].ToString() == "1"))
                {
                    request_doctor = Convert.ToInt16(Request.QueryString["C_Doctor_Code"]);
                    request_type = Convert.ToInt16(Request.QueryString["type"]);
                    LoadDoctor(request_type, request_doctor);
                }

            }
        }
    }
    private void LoadDoctor(int request_type, int request_doctor)
    {
        ListedDR lst = new ListedDR();
        dsListedDR = lst.getCommonDr_List_Edit(request_doctor);
        if (dsListedDR.Tables[0].Rows.Count > 0)
        {
            Common_DR_Code = dsListedDR.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
            txtName.Text = dsListedDR.Tables[0].Rows[0].ItemArray.GetValue(1).ToString();
            txtAddress.Text = dsListedDR.Tables[0].Rows[0].ItemArray.GetValue(2).ToString();
            txtHosAddress.Text = dsListedDR.Tables[0].Rows[0].ItemArray.GetValue(3).ToString();
            txtMobile.Text = dsListedDR.Tables[0].Rows[0].ItemArray.GetValue(4).ToString();
            ddlQual.SelectedValue = dsListedDR.Tables[0].Rows[0].ItemArray.GetValue(5).ToString();
            ddlQual.SelectedItem.Text = dsListedDR.Tables[0].Rows[0].ItemArray.GetValue(6).ToString();
            ddlClass.SelectedValue = dsListedDR.Tables[0].Rows[0].ItemArray.GetValue(7).ToString();
            ddlClass.SelectedItem.Text = dsListedDR.Tables[0].Rows[0].ItemArray.GetValue(8).ToString();
            ddlSpec.SelectedValue = dsListedDR.Tables[0].Rows[0].ItemArray.GetValue(9).ToString();
            ddlSpec.SelectedItem.Text = dsListedDR.Tables[0].Rows[0].ItemArray.GetValue(10).ToString();
            ddlCatg.SelectedValue = dsListedDR.Tables[0].Rows[0].ItemArray.GetValue(11).ToString();
            ddlCatg.SelectedItem.Text = dsListedDR.Tables[0].Rows[0].ItemArray.GetValue(12).ToString();
            strsf_code = dsListedDR.Tables[0].Rows[0].ItemArray.GetValue(15).ToString();
            str_terrcode = dsListedDR.Tables[0].Rows[0].ItemArray.GetValue(17).ToString();
            txtHQ.Text = dsListedDR.Tables[0].Rows[0].ItemArray.GetValue(21).ToString();
            foreach (GridViewRow gridRow in grdDoctor1.Rows)
            {
                CheckBox chkSelect = (CheckBox)gridRow.FindControl("chkSelect");
                Label lblSf_code = (Label)gridRow.FindControl("lblSf_code");
                DropDownList ddlterr = (DropDownList)gridRow.FindControl("ddlterr");
                if (strsf_code != "")
                {
                    iIndex = -1;
                    sfCode = strsf_code.Split(',');
                    foreach (string st in sfCode)
                    {
                        if (lblSf_code.Text == st)
                        {
                            chkSelect.Checked = true;
                            chkSelect.Attributes.Add("style", "Color: Red; font-weight:Bold");
                            gridRow.Attributes.Add("style", "background-color: LightBlue");
                        }
                    }

                }
                if (str_terrcode != "")
                {
                    iIndexterr = -1;
                    terrCode = str_terrcode.Split(',');
                    foreach (string st1 in terrCode)
                    {

                        ddlterr.SelectedValue = st1;

                    }

                }
            }
            foreach (GridViewRow gridRow in grdDoctor2.Rows)
            {
                CheckBox chkSelect = (CheckBox)gridRow.FindControl("chkSelect");
                Label lblSf_code = (Label)gridRow.FindControl("lblSf_code");
                DropDownList ddlterr = (DropDownList)gridRow.FindControl("ddlterr");
                if (strsf_code != "")
                {
                    iIndex = -1;
                    sfCode = strsf_code.Split(',');
                    foreach (string st in sfCode)
                    {
                        if (lblSf_code.Text == st)
                        {
                            chkSelect.Checked = true;
                            chkSelect.Attributes.Add("style", "Color: Red; font-weight:Bold");
                            gridRow.Attributes.Add("style", "background-color: LightBlue");
                        }
                    }

                }
                if (str_terrcode != "")
                {
                    iIndexterr = -1;
                    terrCode = str_terrcode.Split(',');
                    foreach (string st1 in terrCode)
                    {

                        ddlterr.SelectedValue = st1;

                    }

                }
            }
        }
    }
    private void FillCategory()
    {
        ListedDR lstDR = new ListedDR();
        dsListedDR = lstDR.Common_Category(liTerr.Value);
        ddlCatg.DataTextField = "Doc_Cat_SName";
        ddlCatg.DataValueField = "Doc_Cat_Code";
        ddlCatg.DataSource = dsListedDR;
        ddlCatg.DataBind();
    }

    private void FillSpeciality()
    {
        ListedDR lstDR = new ListedDR();
        dsListedDR = lstDR.Common_Speciality(liTerr.Value);
        ddlSpec.DataTextField = "Doc_Special_SName";
        ddlSpec.DataValueField = "Doc_Special_Code";
        ddlSpec.DataSource = dsListedDR;
        ddlSpec.DataBind();
    }

    private void FillQualification()
    {
        ListedDR lstDR = new ListedDR();
        dsListedDR = lstDR.Common_Qualification(liTerr.Value);
        ddlQual.DataTextField = "Doc_QuaName";
        ddlQual.DataValueField = "Doc_QuaCode";
        ddlQual.DataSource = dsListedDR;
        ddlQual.DataBind();
    }
    private void FillClass()
    {
        ListedDR lstDR = new ListedDR();
        dsListedDR = lstDR.Common_Class(liTerr.Value);
        ddlClass.DataTextField = "Doc_ClsSName";
        ddlClass.DataValueField = "Doc_ClsCode";
        ddlClass.DataSource = dsListedDR;
        ddlClass.DataBind();
    }
    private void FillDiv()
    {

        for (int j = 0; j < dsdiv.Tables[0].Rows.Count; j++)
        {
            GridView gv = new GridView();
            gv.CssClass = "aclass";
            gv.Attributes.Add("class", "aclass");

            Label lbl = new Label();
            lbl.CssClass = "lbl";
            lbl.Attributes.Add("class", "lbl");

            if (dsdiv.Tables[0].Rows.Count > 0)
            {
                lbl.Text = dsdiv.Tables[0].Rows[j]["Division_Name"].ToString();
                // state_code = dsdiv.Tables[0].Rows[j]["State_Code"].ToString();
            }

            gv.DataSource = dsdiv;
            gv.DataBind();
            if (dsdiv.Tables[0].Rows.Count > 0)
            {
                lbl.Attributes.Add("align", "center");
                pnl.Controls.Add(lbl);
                pnl.Controls.Add(gv);

            }
        }
    }


    private void FillDoc()
    {
        int icount = 1;
        int maxrows1 = 0;
        int maxrows2 = 0;
        int maxrows3 = 0;
        int maxrows4 = 0;
        int maxrows5 = 0;
        int maxrows6 = 0;

        dtDoctor1 = null;
        dtDoctor2 = null;
        dtDoctor3 = null;
        dtDoctor4 = null;
        dtDoctor5 = null;
        dtDoctor6 = null;
        //  string[] strDivSplit = div_code.Split(',');
        Division dv = new Division();
        //foreach (string strdiv in strDivSplit)
        //{
        //    if (strdiv != "")
        //    {
        //        dsdiv = dv.getDivisionHO(strdiv);

        //        liTerr.Value = dsdiv.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
        //        liTerr.Text = dsdiv.Tables[0].Rows[0].ItemArray.GetValue(1).ToString();
        //        //  ddlDivision.Items.Add(liTerr)
        //        dtCatg.Rows.Add(liTerr);
        //       // dtCatg.Rows.Add(liTerr.Text);
        //    }

        //}
        dtCatg = dv.getDivision_New(div_code);
        //Division LstDoc = new Division();
        //dtCatg = LstDoc.getDivisionHO_New(liTerr.Value);
        ViewState["dt_catg"] = dtCatg;
        if (dtCatg != null)
        {
            ViewState["dt_catg"] = dtCatg;
            // btnSubmit.Visible = true;
            foreach (DataRow drFF in dtCatg.Rows)
            {
                if (icount == 1)
                {
                    lblCatg1.Text = "<span style='font-weight: bold;color:Red; font-size:22px'>  " + drFF["Division_Name"].ToString() + "</span>";

                    SalesForce sf = new SalesForce();
                    dtDoctor = sf.Common_Dr_GetMr(drFF["Division_Code"].ToString(), "admin");
                    //   dtDoctor = LstDoc.get_ListedDoctor_Territory_Catg(sf_code, drFF["Doc_Cat_Code"].ToString());

                    if (dtDoctor != null)
                    {
                        //grdDoctor1.Visible = true;
                        //grdDoctor1.DataSource = dtDoctor;
                        //grdDoctor1.DataBind();
                        dtDoctor1 = dtDoctor;
                        maxrows1 = dtDoctor.Rows.Count;
                        maxrows = maxrows1;
                    }

                }
                else if (icount == 2)
                {
                    lblCatg2.Text = "<span style='font-weight: bold;color:Red; font-size:22px'>  " + drFF["Division_Name"].ToString() + "</span>";

                    SalesForce sf = new SalesForce();
                    dtDoctor = sf.Common_Dr_GetMr(drFF["Division_Code"].ToString(), "admin");

                    if (dtDoctor != null)
                    {
                        //grdDoctor2.Visible = true;
                        //grdDoctor2.DataSource = dtDoctor;
                        //grdDoctor2.DataBind();
                        dtDoctor2 = dtDoctor;
                        maxrows2 = dtDoctor.Rows.Count;
                        if (maxrows < maxrows2)
                            maxrows = maxrows2;
                    }

                }
                else if (icount == 3)
                {
                    lblCatg3.Text = "<span style='font-weight: bold;color:Red; font-size:22px'>  " + drFF["Division_Name"].ToString() + "</span>";

                    SalesForce sf = new SalesForce();
                    dtDoctor = sf.Common_Dr_GetMr(drFF["Division_Code"].ToString(), "admin");

                    if (dtDoctor != null)
                    {
                        //grdDoctor3.Visible = true;
                        //grdDoctor3.DataSource = dtDoctor;
                        //grdDoctor3.DataBind();
                        dtDoctor3 = dtDoctor;
                        maxrows3 = dtDoctor.Rows.Count;
                        if (maxrows < maxrows3)
                            maxrows = maxrows3;
                    }

                }
                else if (icount == 4)
                {
                    lblCatg4.Text = "<span style='font-weight: bold;color:Red; font-size:22px'>  " + drFF["Division_Name"].ToString() + "</span>";

                    SalesForce sf = new SalesForce();
                    dtDoctor = sf.Common_Dr_GetMr(drFF["Division_Code"].ToString(), "admin");

                    if (dtDoctor != null)
                    {
                        //grdDoctor4.Visible = true;
                        //grdDoctor4.DataSource = dtDoctor;
                        //grdDoctor4.DataBind();
                        dtDoctor4 = dtDoctor;
                        maxrows4 = dtDoctor.Rows.Count;
                        if (maxrows < maxrows4)
                            maxrows = maxrows4;

                    }

                }
                else if (icount == 5)
                {
                    lblCatg5.Text = "<span style='font-weight: bold;color:Red; font-size:22px'>  " + drFF["Division_Name"].ToString() + "</span>";

                    SalesForce sf = new SalesForce();
                    dtDoctor = sf.Common_Dr_GetMr(drFF["Division_Code"].ToString(), "admin");

                    if (dtDoctor != null)
                    {
                        //grdDoctor5.Visible = true;
                        //grdDoctor5.DataSource = dtDoctor;
                        //grdDoctor5.DataBind();
                        dtDoctor5 = dtDoctor;
                        maxrows5 = dtDoctor.Rows.Count;
                        if (maxrows < maxrows5)
                            maxrows = maxrows5;

                    }

                }
                else if (icount == 6)
                {
                    lblCatg6.Text = "<span style='font-weight: bold;color:Red; font-size:22px'>  " + drFF["Division_Name"].ToString() + "</span>";

                    SalesForce sf = new SalesForce();
                    dtDoctor = sf.Common_Dr_GetMr(drFF["Division_Code"].ToString(), "admin");
                    if (dtDoctor != null)
                    {
                        //grdDoctor6.Visible = true;
                        //grdDoctor6.DataSource = dtDoctor;
                        //grdDoctor6.DataBind();
                        dtDoctor6 = dtDoctor;
                        maxrows6 = dtDoctor.Rows.Count;
                        if (maxrows < maxrows6)
                            maxrows = maxrows6;

                    }
                }

                icount = icount + 1;

            }

        }

        if (dtDoctor1 != null)
        {
            //if (maxrows > dtDoctor1.Rows.Count)
            //{
            //    currow = dtDoctor1.Rows.Count;
            //    while (currow < maxrows)
            //    {
            //        //dtDoctor1.Rows.Add(0, "", "");
            //        //currow = currow + 1;
            //    }
            //}

            grdDoctor1.Visible = true;
            grdDoctor1.DataSource = dtDoctor1;
            grdDoctor1.DataBind();
        }

        if (dtDoctor2 != null)
        {
            //if (maxrows > dtDoctor2.Rows.Count)
            //{
            //    currow = dtDoctor2.Rows.Count;
            //    while (currow < maxrows)
            //    {
            //        //dtDoctor2.Rows.Add(0, "", "");
            //        //currow = currow + 1;
            //    }
            //}

            grdDoctor2.Visible = true;
            grdDoctor2.DataSource = dtDoctor2;
            grdDoctor2.DataBind();
        }

        if (dtDoctor3 != null)
        {
            //if (maxrows > dtDoctor3.Rows.Count)
            //{
            //    currow = dtDoctor3.Rows.Count;
            //    while (currow < maxrows)
            //    {
            //        dtDoctor3.Rows.Add(0, "", "");
            //        currow = currow + 1;
            //    }
            //}

            grdDoctor3.Visible = true;
            grdDoctor3.DataSource = dtDoctor3;
            grdDoctor3.DataBind();
        }
        if (dtDoctor4 != null)
        {
            //if (maxrows > dtDoctor4.Rows.Count)
            //{
            //    currow = dtDoctor4.Rows.Count;
            //    while (currow < maxrows)
            //    {
            //        dtDoctor4.Rows.Add(0, "", "");
            //        currow = currow + 1;
            //    }
            //}

            grdDoctor4.Visible = true;
            grdDoctor4.DataSource = dtDoctor4;
            grdDoctor4.DataBind();
        }
        if (dtDoctor5 != null)
        {
            //if (maxrows > dtDoctor5.Rows.Count)
            //{
            //    currow = dtDoctor5.Rows.Count;
            //    while (currow < maxrows)
            //    {
            //        dtDoctor5.Rows.Add(0, "", "");
            //        currow = currow + 1;
            //    }
            //}

            grdDoctor5.Visible = true;
            grdDoctor5.DataSource = dtDoctor5;
            grdDoctor5.DataBind();
        }
        if (dtDoctor6 != null)
        {
            //if (maxrows > dtDoctor6.Rows.Count)
            //{
            //    currow = dtDoctor6.Rows.Count;
            //    while (currow < maxrows)
            //    {
            //        dtDoctor6.Rows.Add(0, "", "");
            //        currow = currow + 1;
            //    }
            //}

            grdDoctor6.Visible = true;
            grdDoctor6.DataSource = dtDoctor6;
            grdDoctor6.DataBind();
        }


    }

    protected void grdDoctor1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        TP_New tp = new TP_New();
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DropDownList Territory = (DropDownList)e.Row.FindControl("ddlterr");
            Label lblsfCode = (Label)e.Row.FindControl("lblSf_code");
            if (Territory != null)
            {
                dsTP = tp.FetchTerritory(lblsfCode.Text);
                Territory.DataSource = dsTP;
                Territory.DataBind();
            }
        }
    }

    protected void grdDoctor2_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        TP_New tp = new TP_New();
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DropDownList Territory = (DropDownList)e.Row.FindControl("ddlterr");
            Label lblsfCode = (Label)e.Row.FindControl("lblSf_code");
            if (Territory != null)
            {
                dsTP = tp.FetchTerritory(lblsfCode.Text);
                Territory.DataSource = dsTP;
                Territory.DataBind();
            }
        }
    }
    protected void grdDoctor3_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        TP_New tp = new TP_New();
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DropDownList Territory = (DropDownList)e.Row.FindControl("ddlterr");
            Label lblsfCode = (Label)e.Row.FindControl("lblSf_code");
            if (Territory != null)
            {
                dsTP = tp.FetchTerritory(lblsfCode.Text);
                Territory.DataSource = dsTP;
                Territory.DataBind();
            }
        }
    }
    protected void grdDoctor4_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        TP_New tp = new TP_New();
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DropDownList Territory = (DropDownList)e.Row.FindControl("ddlterr");
            Label lblsfCode = (Label)e.Row.FindControl("lblSf_code");
            if (Territory != null)
            {
                dsTP = tp.FetchTerritory(lblsfCode.Text);
                Territory.DataSource = dsTP;
                Territory.DataBind();
            }
        }
    }
    protected void grdDoctor5_RowDataBound(object sender, GridViewRowEventArgs e)
    {
    }
    protected void grdDoctor6_RowDataBound(object sender, GridViewRowEventArgs e)
    {
    }
    protected void btnsubmit_Click(object sender, EventArgs e)
    {
        string DR_Name = txtName.Text.Trim();

        string DR_Catg = ddlCatg.SelectedValue;
        string DR_Cat_Name = ddlCatg.SelectedItem.Text;
        string DR_Qual = ddlQual.SelectedValue;
        string DR_Qual_Name = ddlQual.SelectedItem.Text;
        string DR_Spe = ddlSpec.SelectedValue;
        string DR_Spe_Name = ddlSpec.SelectedItem.Text;
        string DR_Class = ddlClass.SelectedValue;
        string DR_Class_Name = ddlClass.SelectedItem.Text;
        string DR_Mobile = txtMobile.Text.ToString();
        string DR_Address = txtAddress.Text.ToString();
        string Hosp_Address = txtHosAddress.Text.Trim();
        string DR_Hq = txtHQ.Text.Trim();
        string all_sfcode = string.Empty;
        string all_sfName = string.Empty;
        string terr_code = string.Empty;
        string terr_Name = string.Empty;
        string all_sfcode_2 = string.Empty;
        string all_sfName_2 = string.Empty;
        string terr_code_2 = string.Empty;
        string terr_Name_2 = string.Empty;
        string all_sfcode_3 = string.Empty;
        string all_sfName_3 = string.Empty;
        string terr_code_3 = string.Empty;
        string terr_Name_3 = string.Empty;
        string Final_Sf_Code = string.Empty;
        string Final_Sf_Name = string.Empty;
        string Final_Terr_Code = string.Empty;
        string Final_Terr_Name = string.Empty;
        string Deact_Code = string.Empty;
        //   ListedDR lstDR = new ListedDR();
        int iReturn = -1;
        if (Request.QueryString["type"] != null)
        {
            if ((Request.QueryString["type"].ToString() == "1"))
            {
                request_doctor = Convert.ToInt16(Request.QueryString["C_Doctor_Code"]);
                request_type = Convert.ToInt16(Request.QueryString["type"]);
                if (request_doctor != null)
                {
                    ListedDR lst = new ListedDR();
                    dsListedDR = lst.getCommonDr_List_Edit(request_doctor);
                    if (dsListedDR.Tables[0].Rows.Count > 0)
                    {
                        AllocateIds = dsListedDR.Tables[0].Rows[0].ItemArray.GetValue(15).ToString();
                    }
                }
            }
            else
            {
                request_doctor =0;
            }
        }
        foreach (GridViewRow gridRow in grdDoctor1.Rows)
        {
            CheckBox chkDR = (CheckBox)gridRow.Cells[0].FindControl("chkSelect");
            bool bCheck = chkDR.Checked;
            Label lblsfCode = (Label)gridRow.Cells[1].FindControl("lblSf_code");
            string SfCode = lblsfCode.Text.ToString();
            Label lblsfName = (Label)gridRow.Cells[1].FindControl("lblsfName");
            string SfName = lblsfName.Text.ToString();
            DropDownList ddlterr = (DropDownList)gridRow.Cells[1].FindControl("ddlterr");
            string terr = ddlterr.SelectedValue;
            string terrname = ddlterr.SelectedItem.Text;
            if ((bCheck == true))
            {
          
            //   AllocateIds += Allocate + ",";
              
                all_sfcode += SfCode + ",";
                terr_code += terr + ",";
                all_sfName += SfName + ",";
                terr_Name += terrname + ",";
            }


        }
        foreach (GridViewRow gridRow in grdDoctor2.Rows)
        {
            CheckBox chkDR = (CheckBox)gridRow.Cells[0].FindControl("chkSelect");
            bool bCheck = chkDR.Checked;
            Label lblsfCode = (Label)gridRow.Cells[1].FindControl("lblSf_code");
            string SfCode = lblsfCode.Text.ToString();
            Label lblsfName = (Label)gridRow.Cells[1].FindControl("lblsfName");
            string SfName = lblsfName.Text.ToString();
            DropDownList ddlterr = (DropDownList)gridRow.Cells[1].FindControl("ddlterr");
            string terr = ddlterr.SelectedValue;
            string terrname = ddlterr.SelectedItem.Text;
            if ((bCheck == true))
            {
                all_sfcode_2 += SfCode + ",";
                terr_code_2 += terr + ",";
                all_sfName_2 += SfName + ",";
                terr_Name_2 += terrname + ",";
            }


        }
        foreach (GridViewRow gridRow in grdDoctor3.Rows)
        {
            CheckBox chkDR = (CheckBox)gridRow.Cells[0].FindControl("chkSelect");
            bool bCheck = chkDR.Checked;
            Label lblsfCode = (Label)gridRow.Cells[1].FindControl("lblSf_code");
            string SfCode = lblsfCode.Text.ToString();
            Label lblsfName = (Label)gridRow.Cells[1].FindControl("lblsfName");
            string SfName = lblsfName.Text.ToString();
            DropDownList ddlterr = (DropDownList)gridRow.Cells[1].FindControl("ddlterr");
            string terr = ddlterr.SelectedValue;
            string terrname = ddlterr.SelectedItem.Text;
            if ((bCheck == true))
            {
                all_sfcode_3 += SfCode + ",";
                terr_code_3 += terr + ",";
                all_sfName_3 += SfName + ",";
                terr_Name_3 += terrname + ",";
            }


        }
        Final_Sf_Code = all_sfcode + all_sfcode_2 + all_sfcode_3;
        Final_Sf_Code = Final_Sf_Code.TrimEnd(',');
        Final_Sf_Name = all_sfName + all_sfName_2 + all_sfName_3;
        Final_Sf_Name = Final_Sf_Name.TrimEnd(',');
        Final_Terr_Code = terr_code + terr_code_2 + terr_code_3;
        Final_Terr_Code = Final_Terr_Code.TrimEnd(',');
        Final_Terr_Name = terr_Name + terr_Name_2 + terr_Name_3;
        Final_Terr_Name = Final_Terr_Name.TrimEnd(',');
        ListedDR lstDR = new ListedDR();
        Allocate = AllocateIds.Replace(Final_Sf_Code + ",", "");
        string[] Allocate_1 = Allocate.Split(',');
        foreach (string strcode in Allocate_1)
         {
             if (strcode != "")
             {
                 if (Final_Sf_Code.Contains(strcode))
                 {

                 }
                 else
                 {
                     Deact_Code += strcode + ",";
                 }
             }
            
         }
        Deact_Code = Deact_Code.TrimEnd(',');
        iReturn = lstDR.Com_Doc_ADD(DR_Name, DR_Address, DR_Catg, DR_Cat_Name, DR_Qual, DR_Qual_Name, DR_Spe, DR_Spe_Name, DR_Class, DR_Class_Name, Hosp_Address, DR_Mobile, DR_Hq, Final_Terr_Code, Final_Terr_Name, Final_Sf_Code, Final_Sf_Name, div_code, request_doctor, Deact_Code);
        {
            if (iReturn > 0)
            {
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Created Successfully');window.location='Common_Doctor_List.aspx';</script>");
            }
        }
    }
}