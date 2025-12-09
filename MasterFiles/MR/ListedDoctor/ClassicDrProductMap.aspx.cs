using Bus_EReport;
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class MasterFiles_MR_ListedDoctor_ClassicDrProductMap : System.Web.UI.Page
{
    string div_code = string.Empty;
    string sf_code = string.Empty;
    DataSet dsDCR = null;
    DataSet dsProd = null;
    DataSet dsProdDR = null;
    DataSet dsPrd = new DataSet();
    protected void Page_Load(object sender, EventArgs e)
    {
        div_code = Session["div_code"].ToString();

        //if (!Page.IsPostBack)
        //{
        //    UserControl_MenuUserControl Usc_Menu =
        //        (UserControl_MenuUserControl)LoadControl("~/UserControl/MenuUserControl.ascx");
        //    Divid.Controls.Add(Usc_Menu);
        //    Usc_Menu.Title = this.Page.Title;
        //    Session["backurl"] = "LstDoctorList.aspx";

        //    FillGrid();


        //}
        //else
        //{
        //    UserControl_MenuUserControl Usc_Menu =
        //           (UserControl_MenuUserControl)LoadControl("~/UserControl/MenuUserControl.ascx");
        //    Divid.Controls.Add(Usc_Menu);
        //    Session["backurl"] = "LstDoctorList.aspx";
        //}
        if (!Page.IsPostBack)
        {
            if (Session["sf_type"].ToString() == "1")
            {
                UserControl_MR_Menu Usc_MR =
                (UserControl_MR_Menu)LoadControl("~/UserControl/MR_Menu.ascx");
                Divid.Controls.Add(Usc_MR);
                Usc_MR.Title = this.Page.Title;
                sf_code = Session["sf_code"].ToString();
                lblTerrritory.Text = "( " + "<span style='font-weight: bold;color:Maroon;'>For " + Session["selectedSf_code"] + " </span> )";
            }
            else if (Session["sf_type"].ToString() == "2")
            {
                UserControl_MGR_Menu Usc_MGR =
                (UserControl_MGR_Menu)LoadControl("~/UserControl/MGR_Menu.ascx");
                Divid.Controls.Add(Usc_MGR);
                Usc_MGR.Title = this.Page.Title;
                sf_code = Request.QueryString["sf_code"].ToString();
                Session["backurl"] = "LstDoctorList.aspx";
                lblTerrritory.Text = "( " + "<span style='font-weight: bold;color:Maroon;'>For " + Session["selectedSf_code"] + " </span> )";
            }
            else
            {
                UserControl_MenuUserControl Usc_Menu =
                (UserControl_MenuUserControl)LoadControl("~/UserControl/MenuUserControl.ascx");
                Divid.Controls.Add(Usc_Menu);
                Usc_Menu.Title = this.Page.Title;
                Session["backurl"] = "LstDoctorList.aspx";
                sf_code = Request.QueryString["sf_code"].ToString();
                lblTerrritory.Text = "( " + "<span style='font-weight: bold;color:Maroon;'>For " + Session["selectedSf_code"] + " </span> )";

            }
            FillGrid();
        }
        else
        {
            if (Session["sf_type"].ToString() == "1")
            {
                UserControl_MR_Menu Usc_MR =
                (UserControl_MR_Menu)LoadControl("~/UserControl/MR_Menu.ascx");
                Divid.Controls.Add(Usc_MR);
                Usc_MR.Title = this.Page.Title;
                sf_code = Session["sf_code"].ToString();
            }
            else if (Session["sf_type"].ToString() == "2")
            {
                UserControl_MGR_Menu Usc_MGR =
                    (UserControl_MGR_Menu)LoadControl("~/UserControl/MGR_Menu.ascx");
                Divid.Controls.Add(Usc_MGR);
                Usc_MGR.Title = this.Page.Title;
                sf_code = Request.QueryString["sf_code"].ToString();
                Session["backurl"] = "LstDoctorList.aspx";

            }
            else
            {
                UserControl_MenuUserControl Usc_Menu =
                   (UserControl_MenuUserControl)LoadControl("~/UserControl/MenuUserControl.ascx");
                Divid.Controls.Add(Usc_Menu);
                Session["backurl"] = "LstDoctorList.aspx";
                sf_code = Request.QueryString["sf_code"].ToString();
            }

        }
    }
    private void FillGrid()
    {
        ListedDR dr = new ListedDR();
        Division Div = new Division();
        dsProd = Div.FetchProduct(Session["div_code"].ToString());
        FillProduct();

        //sf_code = Request.QueryString["sf_code"].ToString();

        dsDCR = dr.getGrid_DR_MPL(div_code, sf_code);

        if (dsDCR.Tables[0].Rows.Count > 0)
        {
            dsPrd = dr.getPrd_Priority(div_code);

            if (dsPrd.Tables[0].Rows.Count > 0)
            {
                hdnPriYesNo.Value = dsPrd.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                hdnPriCnt.Value = dsPrd.Tables[0].Rows[0].ItemArray.GetValue(1).ToString();
            }

            grdDCR.Visible = true;
            grdDCR.DataSource = dsDCR;
            grdDCR.DataBind();

        }
        else
        {
            grdDCR.DataSource = null;
            grdDCR.DataBind();
        }
    }
    protected DataSet FillProduct()
    {
        return dsProd;
    }
    protected void grdDCR_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        Division Div = new Division();

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //sf_code = Request.QueryString["sf_code"].ToString();
            Label lblDrcode = (e.Row.FindControl("lblDrcode") as Label);
            CheckBox chkRjtDCR = (e.Row.FindControl("chkRjtDCR") as CheckBox);
            HiddenField hdnProdCodeSelected = (e.Row.FindControl("hdnProdCodeSelected") as HiddenField);
            HiddenField hdnCampignTag = (e.Row.FindControl("hdnCampignTag") as HiddenField);
            TextBox txtProducts = (e.Row.FindControl("txtProducts") as TextBox);

            Product prd = new Product();
            dsProdDR = prd.getprdfor_Mappdr(lblDrcode.Text, sf_code);

            int indexRow = e.Row.RowIndex + 2;
            Panel pnlList = (e.Row.FindControl("pnlList") as Panel);

            FillProduct();

            if (dsProd.Tables[0].Rows.Count > 0)
            {
                DataTable dtProducts = dsProd.Tables[0];

                TextBox txt;
                HtmlInputCheckBox hck;
                Label lbl;
                HiddenField hdnProductCode;

                HtmlTable htmltbl = new HtmlTable();
                HtmlTableRow row;
                HtmlTableCell cell;
                HtmlTableCell cell1;
                HtmlTableCell cell2;
                string prodtext;
                string prodvalue;
                string JoinTxtCode = string.Empty;
                string JoinTxtName = string.Empty;
                for (int i = 0; i < dtProducts.Rows.Count; i++)
                {
                    DataRow drProduct = dtProducts.Rows[i];
                    prodtext = Convert.ToString(drProduct["Product_Detail_Name"]);
                    prodvalue = Convert.ToString(drProduct["Product_Code_SlNo"]);


                    txt = new TextBox();

                    hck = new HtmlInputCheckBox();
                    lbl = new Label();

                    hdnProductCode = new HiddenField();

                    if (hdnPriYesNo.Value != "0")
                    {

                        txt.ID = "txtNew" + i.ToString();
                        txt.Text = "0";
                        txt.Width = Unit.Pixel(50);
                        txt.Style.Add("display", "none");
                        txt.Attributes.Add("type", "number");
                        txt.Attributes.Add("min", "0");
                        txt.Attributes.Add("max", hdnPriCnt.Value);
                        txt.Attributes.Add("onchange", "ControlVisibility(" + i.ToString() + "," + indexRow + ");");
                    }

                    lbl.Text = prodtext;
                    hdnProductCode.ID = "hdnProductCode" + i.ToString();
                    hdnProductCode.Value = prodvalue;

                    htmltbl.ID = "tbl";

                    row = new HtmlTableRow();
                    cell = new HtmlTableCell();
                    cell1 = new HtmlTableCell();
                    cell2 = new HtmlTableCell();
                    hck.ID = "chkNew" + i.ToString();

                    if (dsProdDR.Tables[0].Rows.Count > 0)
                    {
                        //chkRjtDCR.Checked = true;
                        e.Row.BackColor = System.Drawing.Color.FromName("#edd1d1");

                        foreach (DataRow dt in dsProdDR.Tables[0].Rows)
                        {
                            if (dt.ItemArray[0].ToString() == prodvalue)
                            {
                                hck.Checked = true;
                                JoinTxtCode += prodvalue + "~~" + dt.ItemArray[2].ToString() + "#";
                                JoinTxtName += prodtext + ", ";
                                txt.Style.Add("display", hdnPriYesNo.Value == "0" ? "none" : "block");
                                txt.Text = dt.ItemArray[2].ToString();
                                hdnCampignTag.Value = "1";
                            }
                        }
                    }

                    hck.Attributes.Add("onclick", "ControlVisibility(" + i.ToString() + "," + indexRow + ");");

                    cell.Controls.Add(hck);
                    cell1.Controls.Add(lbl);
                    if (hdnPriYesNo.Value != "0")
                    {
                        cell2.Controls.Add(txt);
                    }
                    cell2.Controls.Add(hdnProductCode);

                    cell1.Align = "left";
                    row.Controls.Add(cell);
                    row.Controls.Add(cell1);
                    row.Controls.Add(cell2);
                    htmltbl.Controls.Add(row);
                    pnlList.Controls.Add(htmltbl);
                    if (dsProdDR.Tables[0].Rows.Count < 1)
                    {
                        pnlList.Attributes["class"] = "effectPnl";
                    }
                }
                hdnProdCodeSelected.Value = JoinTxtCode;
                txtProducts.Text = JoinTxtName;
            }
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        //bool ValidPrio = false;
        //if (hdnPriYesNo.Value != "0")
        //{
        //    if (hdnValidPrio.Value == "1")
        //    {
        //        FillGrid();
        //    }
        //    else {
        ListedDR lst = new ListedDR();
        foreach (GridViewRow gridRow in grdDCR.Rows)
        {
            string strAllProdSelected = string.Empty;
            CheckBox chkRjtDCR = (CheckBox)gridRow.Cells[0].FindControl("chkRjtDCR");

            Label lblDrcode = (Label)gridRow.Cells[3].FindControl("lblDrcode");
            Label lblDrname = (Label)gridRow.Cells[4].FindControl("lblDrname");
            Label lblArea = (Label)gridRow.Cells[6].FindControl("lblArea");
            HiddenField Prod = (HiddenField)gridRow.FindControl("hdnProdCodeSelected");

            string[] splitProdPri = Prod.Value.Split('#');
            foreach (string prodOne in splitProdPri)
            {
                string[] Separate = prodOne.Split(new string[] { "~~" }, StringSplitOptions.None);
                if (Separate[0].ToString() != string.Empty)
                {
                    int iReturn = lst.RecordAdd_ProductMap_New(lblDrcode.Text, Separate[0], lblDrname.Text + " - " + lblArea.Text, sf_code, div_code, Separate[1]);
                    strAllProdSelected += Separate[0] + ",";
                }
            }

            if (strAllProdSelected != string.Empty)
            {
                int iReturn = lst.Delete_ProductMapUnselected(lblDrcode.Text, strAllProdSelected.TrimEnd(','), sf_code, div_code);
                int iReturn1 = lst.DocProd_RecordUpdate(lblDrcode.Text, strAllProdSelected, sf_code, div_code);
            }
            else
            {
                int iReturn = lst.Delete_DrProductMapUnselected(lblDrcode.Text, sf_code, div_code);
                int iReturn1 = lst.DocProd_RecordUpdate(lblDrcode.Text, string.Empty, sf_code, div_code);
            }
        }

        ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Updated successfully');</script>");
        FillGrid();
    }
}