using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AjaxControlToolkit;
using Bus_EReport;

public partial class MasterFiles_RCPAList : System.Web.UI.Page
{
    #region "Declaration"
    string div_code = string.Empty;
    string sf_Code = string.Empty;
    string sf_type = string.Empty;
    string EntryDt = string.Empty;
    string RCPADt = string.Empty;
    string Chemists_Code = string.Empty;
    string ListedDrCode = string.Empty;
    string CmptrName = string.Empty;
    string CmptrBrnd = string.Empty;
    string CmptrPriz = string.Empty;
    string ourBrndCode = string.Empty;
    string ourBrndNm = string.Empty;
    string Remark = string.Empty;
    string Division_Code = string.Empty;
    string CmptrQty = string.Empty;
    string CmptrPOB = string.Empty;
    string ChmName = string.Empty;
    string DrName = string.Empty;
    string OurQty = string.Empty;
    string OurPrice = string.Empty;
    string fieldForce = string.Empty;
    string drNameRCPA = string.Empty;
    DataSet dsDoc = null;
    DataSet dsChemists = null;
    DataSet dsTerritory = null;
    DataSet dsProd = null;
    DataSet st = null;
    string divcode = string.Empty;
    string subdiv_sname = string.Empty;
    string subdiv_name = string.Empty;
    string state_code = string.Empty;
    string distPrice = string.Empty;
    DateTime ServerStartTime;
    DateTime ServerEndTime;
    int time;
    #endregion

    #region PageEvents
    protected void Page_Load(object sender, EventArgs e)
    {
        Page.Header.DataBind();

        sf_Code = Session["sf_code"].ToString();
        if (Session["sf_code"] != null && Session["sf_code"].ToString() != "")
        {
            sf_Code = Session["sf_code"].ToString();
        }

        lblSelect.Visible = true;

        if (!Page.IsPostBack)
        {
            ServerStartTime = DateTime.Now;
            base.OnPreInit(e);
            if (Session["sf_type"].ToString() == "1")
            {
                div_code = Session["div_code"].ToString();
                UserControl_MR_Menu c1 =
               (UserControl_MR_Menu)LoadControl("~/UserControl/MR_Menu.ascx");
                ModalPopupExtender mpe = (ModalPopupExtender)c1.FindControl("mpeTimeout");
                c1.Controls.Remove(mpe);
                ToolkitScriptManager tsm = (ToolkitScriptManager)c1.FindControl("ToolkitScriptManager1");
                c1.Controls.Remove(tsm);
                Divid.Controls.Add(c1);
                c1.Title = Page.Title;
                // c1.FindControl("btnBack").Visible = false;
                this.getddlSF_Code();
                ddlSFCode_SelectedIndexChanged(sender, e);
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
                ModalPopupExtender mpe = (ModalPopupExtender)c1.FindControl("mpeTimeout");
                c1.Controls.Remove(mpe);
                ToolkitScriptManager tsm = (ToolkitScriptManager)c1.FindControl("ToolkitScriptManager1");
                c1.Controls.Remove(tsm);
                Divid.Controls.Add(c1);
                c1.Title = Page.Title;
                // c1.FindControl("btnBack").Visible = false;
                this.getddlSF_Code();
            }
        }
        else
        {
            if (Session["sf_type"].ToString() == "1")
            {
                div_code = Session["div_code"].ToString();
                UserControl_MR_Menu c1 =
               (UserControl_MR_Menu)LoadControl("~/UserControl/MR_Menu.ascx");
                ModalPopupExtender mpe = (ModalPopupExtender)c1.FindControl("mpeTimeout");
                c1.Controls.Remove(mpe);
                ToolkitScriptManager tsm = (ToolkitScriptManager)c1.FindControl("ToolkitScriptManager1");
                c1.Controls.Remove(tsm);
                Divid.Controls.Add(c1);
                c1.Title = Page.Title;
                // c1.FindControl("btnBack").Visible = false;
            }
            else if (Session["sf_type"].ToString() == "" || Session["sf_type"].ToString() == "3")
            {
                sf_Code = ddlSFCode.SelectedValue;
                div_code = Session["div_code"].ToString();
                if (div_code.Contains(','))
                {
                    div_code = div_code.Remove(div_code.Length - 1);
                }
                UserControl_MenuUserControl c1 =
               (UserControl_MenuUserControl)LoadControl("~/UserControl/MenuUserControl.ascx");
                ModalPopupExtender mpe = (ModalPopupExtender)c1.FindControl("mpeTimeout");
                c1.Controls.Remove(mpe);
                ToolkitScriptManager tsm = (ToolkitScriptManager)c1.FindControl("ToolkitScriptManager1");
                c1.Controls.Remove(tsm);
                Divid.Controls.Add(c1);
                c1.Title = Page.Title;
                // c1.FindControl("btnBack").Visible = false;
            }
        }
    }

    protected override void OnLoadComplete(EventArgs e)
    {
        ServerEndTime = DateTime.Now;
        TrackPageTime();//It will give you page load time  
    }

    public void TrackPageTime()
    {
        TimeSpan serverTimeDiff = ServerEndTime.Subtract(ServerStartTime);
        time = serverTimeDiff.Minutes;
    }
    #endregion

    #region ddlSF_Code
    public void getddlSF_Code()
    {
        Territory terr = new Territory();
        dsTerritory = terr.getSFCode(div_code);

        if (dsTerritory.Tables[0].Rows.Count > 0)
        {
            if (Session["sf_type"].ToString() == "1")
            {
                var rsTr_Name = from row in dsTerritory.Tables[0].AsEnumerable()
                                where row.Field<string>("sf_code") == Session["sf_Code"].ToString()
                                select new
                                {
                                    Sf_Code = row.Field<string>("sf_code"),
                                    Sf_Name = row.Field<string>("Sf_Name"),
                                };

                ddlSFCode.DataTextField = "Sf_Name";
                ddlSFCode.DataValueField = "Sf_Code";
                ddlSFCode.DataSource = rsTr_Name.ToArray();
                ddlSFCode.DataBind();
                ddlSFCode.Items.Insert(0, new ListItem("---Select---", "0"));
                ddlSFCode.SelectedIndex = 1;
                ddlSFCode.Enabled = false;
            }
            else
            {
                ddlSFCode.DataTextField = "Sf_Name";
                ddlSFCode.DataValueField = "Sf_Code";
                ddlSFCode.DataSource = dsTerritory;
                ddlSFCode.DataBind();
                if (Session["sf_Code"] == null || Session["sf_Code"].ToString() == "admin")
                {
                    ddlSFCode.SelectedIndex = 0;
                    sf_Code = ddlSFCode.SelectedValue.ToString();
                    Session["sf_Code"] = sf_Code;
                }
            }
        }
    }
    #endregion

    #region FillDocColor()
    private void FillDocColor()
    {
        Product pro = new Product();
        DataTable dt = new DataTable();

        if (ddlSFCode.SelectedValue != "")
        {
            dsProd = pro.getRCPADoc(ddlSFCode.SelectedValue);
            string[] selectedColumn = new[] { "DrCode" };
            dt = new DataView(dsProd.Tables[0]).ToTable(true, selectedColumn);

            for (int k = 0; k < dt.Rows.Count; k++)
            {
                string doc = dt.Rows[k]["DrCode"].ToString();
                ListItem ddl = ddlDocName.Items.FindByValue(doc);
                if (ddl != null)
                {
                    ddl.Attributes.Add("style", "background-color: #F5AB35 !important");
                }
            }
        }
    }
    #endregion

    #region FillChemist()
    private void FillChemist()
    {
        Chemist chem = new Chemist();
        for (int i = 1; i < ddlSFCode.Items.Count; i++)
        {
            sf_Code = Session["sf_code"].ToString();
            if (ddlSFCode.Items[i].Value == sf_Code)
            {
                ddlSFCode.SelectedIndex = i;
            }
        }

        dsChemists = chem.getChemistsTerr(sf_Code);
        if (dsChemists.Tables[0].Rows.Count > 0)
        {
            var result = from data in dsChemists.Tables[0].AsEnumerable()
                         select new
                         {
                             Ch_Name = data.Field<string>("Chemists_Name"),
                             Ch_Code = data.Field<int>("Chemists_Code")
                         };
            var listOfGrades = result.ToList();
            cblChemistList.Visible = true;
            cblChemistList.DataSource = listOfGrades;
            cblChemistList.DataTextField = "Ch_Name";
            cblChemistList.DataValueField = "Ch_Code";
            cblChemistList.DataBind();
        }
    }

    [Serializable]
    public class CheckboxItem
    {
        public string Text { get; set; }
        public string Value { get; set; }

        public CheckboxItem(string value, string text)
        {
            Value = value;
            Text = text;
        }

        public override string ToString()
        {
            return Text;
        }
    }

    private void FIllChemSelected()
    {
        Product pr = new Product();
        string chmCode = string.Empty;

        dsProd = pr.Trans_RCPA_HeadExist(ddlSFCode.SelectedValue, ddlDocName.SelectedValue);

        if (dsProd.Tables[0].Rows.Count > 0)
        {
            chmCode = dsProd.Tables[0].Rows[0]["ChmCode"].ToString();
            string[] chmCodes = chmCode.Split(',').ToArray();

            foreach (string chm in chmCodes)
            {
                ListItem ddl = cblChemistList.Items.FindByValue(chm);
                if (ddl != null)
                {
                    ddl.Attributes.Add("style", "background-color: #F5AB35 !important");
                    cblChemistList.Items.FindByValue(chm).Selected = true;
                }
            }
        }
    }
    #endregion

    #region FillProduct()
    private void FillProduct()
    {
        Product dv = new Product();
        sf_Code = ddlSFCode.SelectedValue;

        if (sf_Code != string.Empty)
        {
            dsProd = dv.FetchProduct(sf_Code, div_code);

            ddlProduct.DataSource = dsProd;
            ddlProduct.DataTextField = "Product_Detail_Name";
            ddlProduct.DataValueField = "ourBrndCode";
            ddlProduct.DataBind();
        }
    }
    #endregion

    #region FillddlProductColor()
    private void FillddlProductColor()
    {
        if (gvProductVal.Rows.Count > 0)
        {
            foreach (GridViewRow row in gvProductVal.Rows)
            {
                HiddenField hdnProductCode = (HiddenField)row.FindControl("hdnOPCode");
                ListItem ddl = ddlProduct.Items.FindByValue(hdnProductCode.Value);
                if (ddl != null)
                {
                    ddl.Attributes.Add("style", "background-color: #F5AB35 !important");
                }
            }
        }
    }
    #endregion

    #region ddlSFCode_SelectedIndexChanged
    protected void ddlSFCode_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlDocName.Enabled = true;
        btnGo.Visible = true;

        sf_Code = ddlSFCode.SelectedValue;
        fieldForce = ddlSFCode.SelectedValue;

        Session["sf_code"] = sf_Code;
        ListedDR LstDoc = new ListedDR();
        for (int i = 1; i < ddlSFCode.Items.Count; i++)
        {
            sf_Code = Session["sf_Code"].ToString();
            if (ddlSFCode.Items[i].Value == sf_Code)
            {
                ddlSFCode.SelectedIndex = i;

            }
        }
        dsDoc = LstDoc.getListedDr_Spec_Area(sf_Code);
        if (dsDoc.Tables[0].Rows.Count > 0)
        {
            var result = from data in dsDoc.Tables[0].AsEnumerable()
                         select new
                         {
                             Dr_Name = data.Field<string>("ListedDr_Name"),
                             Dr_Code = data.Field<decimal>("ListedDrCode")
                         };
            var listDoc = result.ToList();

            ddlDocName.DataTextField = "Dr_Name";
            ddlDocName.DataValueField = "Dr_Code";
            ddlDocName.DataSource = listDoc;
            ddlDocName.DataBind();
            ddlDocName.SelectedValue = "0";
        }

        FillDocColor();
        cblChemistList.Items.Clear();
        FillChemist();
    }
    #endregion

    #region ddlDocName_SelectedIndexChanged
    protected void ddlDocName_SelectedIndexChanged(object sender, EventArgs e)
    {
        drNameRCPA = ddlDocName.SelectedItem.Text.ToString();
        FillDocColor();

        FIllChemSelected();
    }
    #endregion

    #region ddlProduct_SelectedIndexChanged
    protected void ddlProduct_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblSelect.Visible = false;

        string Product = string.Empty;
        Product dr = new Product();
        SalesForce sff = new SalesForce();
        DataSet dsProducts = null;

        if (ddlProduct.SelectedValue != "0")
        {
            dsProd = dr.Trans_RCPA_Product(ddlSFCode.SelectedValue, ddlDocName.SelectedValue, ddlProduct.SelectedValue);

            if (dsProd.Tables[0].Rows.Count > 0)
            {
                txtOurVal.Text = dsProd.Tables[0].Rows[0]["OPRate"].ToString();
                txtQtyperMonth.Text = dsProd.Tables[0].Rows[0]["OPQty"].ToString();
                txtTotal.Text = dsProd.Tables[0].Rows[0]["OPValue"].ToString();
            }
            else
            {
                st = sff.CheckStatecode(ddlSFCode.SelectedValue);
                if (st.Tables[0].Rows.Count > 0)
                {
                    state_code = st.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                }
                dsProducts = dr.getDocPrdDistPrice(ddlProduct.SelectedValue, div_code, state_code);

                if (dsProducts.Tables[0].Rows.Count > 0)
                {
                    distPrice = dsProducts.Tables[0].Rows[0].ItemArray.GetValue(3).ToString();

                    txtOurVal.Text = distPrice;
                    txtQtyperMonth.Text = string.Empty;
                    txtTotal.Text = string.Empty;
                }
                else
                {
                    txtOurVal.Text = string.Empty;
                    txtQtyperMonth.Text = string.Empty;
                    txtTotal.Text = string.Empty;
                }
            }
        }
        else
        {
            txtOurVal.Text = string.Empty;
            txtQtyperMonth.Text = string.Empty;
            txtTotal.Text = string.Empty;
        }

        FillddlProductColor();
        ClearComp();
        dvProduct.Visible = false;
    }
    #endregion

    #region ddlCompName_SelectedIndexChanged
    protected void ddlCompName_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblSelect.Visible = false;

        Product dp = new Product();
        DataSet dsCom = null;

        DropDownList ddlComp = (DropDownList)sender;
        GridViewRow gvRow = (GridViewRow)ddlComp.NamingContainer;
        int rowID = gvRow.RowIndex;
        DropDownList ddlCompPName = (DropDownList)gvCompetitor.Rows[rowID].Cells[1].FindControl("ddlCompPName");
        TextBox txtCPRate = (TextBox)gvCompetitor.Rows[rowID].Cells[3].FindControl("txtCPRate");

        txtCPRate.Text = txtOurVal.Text.ToString().Trim();

        dsCom = dp.getMapCompetitorProduct(ddlComp.SelectedValue, div_code);
        if (ddlComp.SelectedValue != "0"
            && dsCom.Tables[0].Rows.Count > 0)
        {
            ddlCompPName.DataSource = dsCom;
            ddlCompPName.DataTextField = "Comp_Prd_Name";
            ddlCompPName.DataValueField = "Comp_Prd_Sl_No";
            ddlCompPName.DataBind();
            ddlCompPName.Items.Insert(0, new ListItem("---Select---", "0"));
        }
        else if (ddlComp.SelectedValue == "0")
        {
            ddlCompPName.Items.Clear();
            ddlCompPName.DataSource = null;
            ddlCompPName.DataBind();
            ddlCompPName.Items.Insert(0, new ListItem("---Select---", "0"));
        }

    }
    #endregion

    #region BindProductVal
    private void BindProductVal()
    {
        gvProductVal.Visible = true;

        Product pr = new Product();

        dsProd = pr.Trans_RCPA_HeadExist(ddlSFCode.SelectedValue, ddlDocName.SelectedValue);

        if (dsProd.Tables[0].Rows.Count > 0)
        {
            gvProductVal.DataSource = dsProd;
            gvProductVal.DataBind();
        }
        else
        {
            gvProductVal.DataSource = dsProd;
            gvProductVal.DataBind();
        }
    }
    #endregion

    #region BindComp
    private void BindComp()
    {
        dvProduct.Visible = true;

        Product objProduct = new Product();
        DataSet dsProducts = null;

        dsProducts = objProduct.Trans_RCPA_PrdComp(ddlSFCode.SelectedValue, ddlDocName.SelectedValue, ddlProduct.SelectedValue);

        if (dsProducts.Tables[0].Rows.Count > 0)
        {
            gvCompetitor.EnableViewState = true;
            gvCompetitor.DataSource = objProduct.getEmptyRCPA();
            gvCompetitor.DataBind();

            #region Variable
            DropDownList CompName;
            HiddenField hdnCompName;
            DropDownList CompPName;
            TextBox CPRate;
            TextBox CPQty;
            TextBox CPValue;
            #endregion

            int count = dsProducts.Tables[0].Rows.Count;

            foreach (GridViewRow gridrow in gvCompetitor.Rows)
            {
                #region Variables
                CompName = (DropDownList)gridrow.FindControl("ddlCompName");
                hdnCompName = (HiddenField)gridrow.FindControl("hdnCompName");
                CompPName = (DropDownList)gridrow.FindControl("ddlCompPName");
                CPRate = (TextBox)gridrow.FindControl("txtCPRate");
                CPQty = (TextBox)gridrow.FindControl("txtCPQty");
                CPValue = (TextBox)gridrow.FindControl("txtCPValue");
                #endregion

                if (gridrow.RowIndex < count)
                {
                    CompName.SelectedValue = dsProducts.Tables[0].Rows[gridrow.RowIndex]["CompCode"].ToString();
                    hdnCompName.Value = dsProducts.Tables[0].Rows[gridrow.RowIndex]["CompCode"].ToString();

                    Product dp = new Product();
                    DataSet dsCom = null;

                    dsCom = dp.getMapCompetitorProduct(dsProducts.Tables[0].Rows[gridrow.RowIndex]["CompCode"].ToString(), div_code);
                    if (dsCom.Tables[0].Rows.Count > 0)
                    {
                        CompPName.DataSource = dsCom;
                        CompPName.DataTextField = "Comp_Prd_Name";
                        CompPName.DataValueField = "Comp_Prd_Sl_No";
                        CompPName.DataBind();
                        CompPName.Items.Insert(0, new ListItem("---Select---", "0"));
                    }
                    else
                    {
                        CompPName.Items.Clear();
                        CompPName.DataSource = null;
                        CompPName.DataBind();
                        CompPName.Items.Insert(0, new ListItem("---Select---", "0"));
                    }

                    CompPName.SelectedValue = dsProducts.Tables[0].Rows[gridrow.RowIndex]["CompPCode"].ToString();
                    CPQty.Text = dsProducts.Tables[0].Rows[gridrow.RowIndex]["CPQty"].ToString();
                    CPRate.Text = dsProducts.Tables[0].Rows[gridrow.RowIndex]["CPRate"].ToString();
                    CPValue.Text = dsProducts.Tables[0].Rows[gridrow.RowIndex]["CPValue"].ToString();
                }
            }
        }
        else
        {
            dsProducts = objProduct.getEmptyRCPA();
            gvCompetitor.DataSource = dsProducts;
            gvCompetitor.DataBind();
        }
    }
    #endregion

    #region btnGo_Click
    protected void btnGo_Click(object sender, EventArgs e)
    {
        lblSelect.Visible = false;
        ddlSFCode.Enabled = false;
        ddlDocName.Enabled = false;
        btnGo.Enabled = false;

        if (ddlSFCode.SelectedIndex > -1)
        {
            ddlSFCode.Enabled = false;
        }

        BindProductVal();
        FillProduct();
        FillddlProductColor();
        tbRCPA.Visible = true;
    }
    #endregion

    #region btnClear_Click
    protected void btnClear_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.AbsoluteUri);
    }
    #endregion

    #region lblbtnEdit_Click
    protected void lblbtnEdit_Click(object sender, EventArgs e)
    {
        lblSelect.Visible = false;

        LinkButton lb = (LinkButton)sender;
        GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
        int rowID = gvRow.RowIndex;
        HiddenField hdnListedDr_Code = (HiddenField)gvProductVal.Rows[rowID].Cells[2].FindControl("hdnOPCode");
        Label lblOPRate = (Label)gvProductVal.Rows[rowID].Cells[3].FindControl("lblOPRate");
        Label lblOPQty = (Label)gvProductVal.Rows[rowID].Cells[4].FindControl("lblOPQty");
        Label lblOPValue = (Label)gvProductVal.Rows[rowID].Cells[5].FindControl("lblOPValue");

        ddlProduct.SelectedValue = hdnListedDr_Code.Value;
        txtOurVal.Text = lblOPRate.Text;
        txtQtyperMonth.Text = lblOPQty.Text;
        txtTotal.Text = lblOPValue.Text;

        BindComp();
        FillddlProductColor();
    }
    #endregion

    #region lnkbtnCompMap_Click
    protected void lnkbtnCompMap_Click(object sender, EventArgs e)
    {
        lblSelect.Visible = false;
        decimal total = (Convert.ToDecimal(txtOurVal.Text.Trim()) * Convert.ToDecimal(txtQtyperMonth.Text.Trim()));
        txtTotal.Text = total.ToString();

        //FillCompBrnd();

        BindComp();
    }
    #endregion

    #region gvProductVal_RowDataBound
    protected void gvProductVal_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        Product pr = new Product();
        DataSet dsComp = null;
        string FK_PK_ID = string.Empty;

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            HiddenField hdnOPCode = (HiddenField)e.Row.Cells[2].FindControl("hdnOPCode");

            dsComp = pr.getRCPAEntryView(ddlSFCode.SelectedValue, ddlDocName.SelectedValue, hdnOPCode.Value);

            GridView gvComp = e.Row.FindControl("gvComp") as GridView;
            gvComp.DataSource = dsComp;
            gvComp.DataBind();
        }
    }
    #endregion

    #region gvProductVal_RowDeleting
    protected void gvProductVal_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        System.Threading.Thread.Sleep(time);

        Product lst = new Product();

        try
        {
            //
            #region Variable
            HiddenField hdnPrd_Code;
            int iReturn = 0;
            #endregion
            //

            #region Variables
            hdnPrd_Code = (HiddenField)gvProductVal.Rows[e.RowIndex].Cells[2].FindControl("hdnOPCode");
            #endregion

            iReturn = lst.Delete_RCPAEntry(ddlSFCode.SelectedValue, ddlDocName.SelectedValue, hdnPrd_Code.Value);

            if (iReturn > 0)
            {
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Deleted Successfully');</script>");
            }

            lblSelect.Visible = false;
            dvProduct.Visible = false;
            BindProductVal();
        }
        catch (Exception ex)
        {
            //Response.Write(ex.Message);
        }
    }
    #endregion

    #region gvCompetitor_RowDataBound
    protected void gvCompetitor_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        Doctor dv = new Doctor();
        DataSet dsCom = null;
        DropDownList ddlCompName;

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            dsCom = dv.getCompetitor(div_code);
            if (dsCom.Tables[0].Rows.Count > 0)
            {
                ddlCompName = (DropDownList)e.Row.Cells[0].FindControl("ddlCompName");
                ddlCompName.DataSource = dsCom;
                ddlCompName.DataTextField = "Comp_Name";
                ddlCompName.DataValueField = "Comp_Sl_No";
                ddlCompName.DataBind();
                ddlCompName.Items.Insert(0, new ListItem("---Select---", "0"));
            }
            else
            {
                ddlCompName = (DropDownList)e.Row.Cells[0].FindControl("ddlCompName");
                ddlCompName.DataSource = dsCom;
                ddlCompName.DataTextField = "Comp_Name";
                ddlCompName.DataValueField = "Comp_Sl_No";
                ddlCompName.DataBind();
                ddlCompName.Items.Insert(0, new ListItem("---Select---", "0"));
            }
        }
    }
    #endregion

    #region btnSave_Click
    protected void btnSave_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(time);

        int iReturn = -1;
        Product pro = new Product();
        DataTable dt = new DataTable();

        sf_Code = ddlSFCode.SelectedValue;
        Chemists_Code = String.Join(",", cblChemistList.Items.OfType<ListItem>().Where(r => r.Selected)
        .Select(r => r.Value));
        ListedDrCode = ddlDocName.SelectedValue.ToString();
        ChmName = String.Join(",", cblChemistList.Items.OfType<ListItem>().Where(r => r.Selected)
        .Select(r => r.Text));
        DrName = ddlDocName.SelectedItem.Text.Trim().ToString();

        try
        {
            //
            #region Variable
            DropDownList ddlCompName;
            HiddenField hdnCompName;
            DropDownList ddlCompPName;
            TextBox txtCPRate;
            TextBox txtCPQty;
            TextBox txtCPValue;
            #endregion
            //

            foreach (GridViewRow gridrow in gvCompetitor.Rows)
            {
                #region Variables
                ddlCompName = (DropDownList)gridrow.FindControl("ddlCompName");
                hdnCompName = (HiddenField)gridrow.FindControl("hdnCompName");
                ddlCompPName = (DropDownList)gridrow.FindControl("ddlCompPName");
                txtCPRate = (TextBox)gridrow.FindControl("txtCPRate");
                txtCPQty = (TextBox)gridrow.FindControl("txtCPQty");
                txtCPValue = (TextBox)gridrow.FindControl("txtCPValue");
                #endregion

                iReturn = InsertRCPAEntry(sf_Code, Chemists_Code, ListedDrCode, ChmName, DrName, ddlProduct, txtOurVal, txtQtyperMonth,
                    txtTotal, ddlCompName, hdnCompName, ddlCompPName, txtCPRate, txtCPQty, txtCPValue);
            }

            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Saved Successfully');</script>");

            lblSelect.Visible = false;
            dvProduct.Visible = false;
            BindProductVal();
            FillddlProductColor();
            ClearProduct();
            ClearComp();
        }
        catch (Exception ex)
        {
            //Response.Write(ex.Message);
        }
    }
    #endregion

    int i = 0;
    #region InsertRCPAEntry
    private int InsertRCPAEntry(string sf_Code, string Chemists_Code, string ListedDrCode, string ChmName, string DrName,
        DropDownList ddlProduct, TextBox txtOurVal, TextBox txtQtyperMonth, TextBox txtTotal, DropDownList ddlCompName, HiddenField hdnCompName,
        DropDownList ddlCompPName, TextBox txtCPRate, TextBox txtCPQty, TextBox txtCPValue)
    {
        int iReturn = -1;
        decimal tot = Convert.ToDecimal(txtOurVal.Text.Trim()) * Convert.ToDecimal(txtQtyperMonth.Text.Trim());

        Product lst = new Product();
        DataSet dsRCPA = null;
        DataSet dsRCPAPrdComp = null;

        dsProd = lst.Trans_RCPA_Product(sf_Code, ListedDrCode, ddlProduct.SelectedValue);
        dsRCPA = lst.getRCPAEntryView(sf_Code, ListedDrCode, ddlProduct.SelectedValue);
        dsRCPAPrdComp = lst.getRCPAPrdComp(sf_Code, ListedDrCode, ddlProduct.SelectedValue, hdnCompName.Value);

        if (ddlCompName.SelectedValue != "0" && ddlCompPName.SelectedValue != "0" && txtCPRate.Text != string.Empty &&
            txtCPQty.Text != string.Empty && txtCPValue.Text != string.Empty)
        {
            if (dsRCPAPrdComp.Tables[0].Rows.Count == 0)
            {
                i++;
                if (i == 1)
                {
                    if (dsProd.Tables[0].Rows.Count == 0)
                    {
                        iReturn = lst.RecordAddRCPA_Head(sf_Code, ddlSFCode.SelectedItem.Text.Trim(), ListedDrCode, DrName, Chemists_Code, ChmName,
                            ddlProduct.SelectedValue.Trim(), ddlProduct.SelectedItem.Text.Trim(), Convert.ToDecimal(txtQtyperMonth.Text.Trim()),
                            Convert.ToDecimal(txtOurVal.Text.Trim()), tot);
                    }
                    else
                    {
                        iReturn = lst.RecordUpdRCPA_Head(sf_Code, ListedDrCode, ddlProduct.SelectedValue, Chemists_Code, ChmName,
                               Convert.ToDecimal(txtQtyperMonth.Text.Trim()), tot);
                    }
                }
                iReturn = lst.RecordAddRCPA_Details(sf_Code, ListedDrCode, ddlProduct.SelectedValue, ddlCompName.SelectedValue.Trim(), ddlCompName.SelectedItem.Text.Trim(), ddlCompPName.Text.Trim(),
                    Convert.ToDecimal(txtCPRate.Text.Trim()), Convert.ToDecimal(txtCPQty.Text.Trim()), Convert.ToDecimal(txtCPValue.Text.Trim()));
            }
            else
            {
                i++;
                if (i == 1)
                {
                    if (dsProd.Tables[0].Rows.Count > 0)
                    {
                        iReturn = lst.RecordUpdRCPA_Head(sf_Code, ListedDrCode, ddlProduct.SelectedValue, Chemists_Code, ChmName,
                               Convert.ToDecimal(txtQtyperMonth.Text.Trim()), tot);
                    }
                }
                iReturn = lst.RecordUpdRCPADetails(sf_Code, ListedDrCode, ddlProduct.SelectedValue, ddlCompName.SelectedValue.Trim(), ddlCompName.SelectedItem.Text.Trim(), hdnCompName.Value,
                   ddlCompPName.Text.Trim(), Convert.ToDecimal(txtCPRate.Text.Trim()), Convert.ToDecimal(txtCPQty.Text.Trim()), Convert.ToDecimal(txtCPValue.Text.Trim()));
            }
        }
        else if (ddlCompName.SelectedValue != "0" && hdnCompName.Value != string.Empty)
        {
            if (dsRCPA.Tables[0].Rows.Count == 1)
            {
                iReturn = lst.Delete_RCPAEntry(sf_Code, ListedDrCode, ddlProduct.SelectedValue);
            }
            else if (dsRCPA.Tables[0].Rows.Count > 1)
            {
                iReturn = lst.Delete_RCPACompDetails(sf_Code, ListedDrCode, ddlProduct.SelectedValue, hdnCompName.Value.Trim());
            }
        }
        return iReturn;
    }
    #endregion

    #region btnCancel_Click
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        lblSelect.Visible = false;
        ClearComp();
        ClearProduct();
        dvProduct.Visible = false;
        BindProductVal();
    }
    #endregion

    #region Clear
    private void ClearProduct()
    {
        ddlProduct.SelectedValue = "0";
        txtOurVal.Text = "";
        txtQtyperMonth.Text = "";
        txtTotal.Text = "";
    }

    private void ClearComp()
    {
        #region Variable
        DropDownList CompName;
        HiddenField hdnCompName;
        DropDownList CompPName;
        TextBox CPRate;
        TextBox CPQty;
        TextBox CPValue;
        #endregion

        foreach (GridViewRow gridrow in gvCompetitor.Rows)
        {
            #region Variables
            CompName = (DropDownList)gridrow.FindControl("ddlCompName");
            hdnCompName = (HiddenField)gridrow.FindControl("hdnCompName");
            CompPName = (DropDownList)gridrow.FindControl("ddlCompPName");
            CPRate = (TextBox)gridrow.FindControl("txtCPRate");
            CPQty = (TextBox)gridrow.FindControl("txtCPQty");
            CPValue = (TextBox)gridrow.FindControl("txtCPValue");
            #endregion

            CPQty.Text = string.Empty;
            CPRate.Text = string.Empty;
            CPValue.Text = string.Empty;
        }
    }
    #endregion
}