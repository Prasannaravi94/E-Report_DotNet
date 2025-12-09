using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Bus_EReport;

public partial class MasterFiles_RCPA_View : System.Web.UI.Page
{
    #region "Declaration"
    string div_code = string.Empty;
    string sf_Code = string.Empty;
    string sf_type = string.Empty;
    string Product_Code = string.Empty;
    DataSet dsTerritory = null;
    DataSet dsProd = null;
    string divcode = string.Empty;
    string subdiv_sname = string.Empty;
    string subdiv_name = string.Empty;
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        div_code = Session["div_code"].ToString();
        sf_Code = Session["sf_code"].ToString();
        sf_type = Session["sf_type"].ToString();

        if (Session["sf_Code"] != null && Session["sf_Code"].ToString() != "")
        {
            sf_Code = Session["sf_Code"].ToString();
        }
        menu1.Title = this.Page.Title;
        if (!IsPostBack)
        {
            getddlSF_Code();
        }
        //menu1.FindControl("btnBack").Visible = false;
    }
    //protected void linkcheck_Click(object sender, EventArgs e)
    //{
    //    ddlFieldForce.Visible = true;
    //    //txtNew.Visible = true;
    //    linkcheck.Visible = false;
    //    getddlSF_Code();
    //}
    private void FillProduct()
    {
        Product.Visible = true;
        Product pro = new Product();
        DataTable dt = new DataTable();
        sf_Code = ddlFieldForce.SelectedValue;

        if (sf_Code != string.Empty)
        {
            dsProd = pro.getProdByDivSubDState(sf_Code, div_code);
            string[] selectedColumns = new[] { "Product_Detail_Name", "Product_Code_SlNo" };
            dt = new DataView(dsProd.Tables[0]).ToTable(true, selectedColumns);

            if (dt.Rows.Count > 0)
            {
                var result = from data in dt.AsEnumerable()
                             select new
                             {
                                 Ch_Name = data.Field<string>("Product_Detail_Name"),
                                 Ch_Code = data.Field<decimal>("Product_Code_SlNo")
                             };
                var listOfGrades = result.ToList();
                cblProductList.Visible = true;
                cblProductList.DataSource = listOfGrades;
                cblProductList.DataTextField = "Ch_Name";
                cblProductList.DataValueField = "Ch_Code";
                cblProductList.DataBind();
                foreach (ListItem li in cblProductList.Items)
                {
                    li.Attributes.Add("dvalue", li.Value);
                }
            }
        }
        else
        {
            cblProductList.ClearSelection();
            Product.Visible = false;
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
    public void getddlSF_Code()
    {
        Territory terr = new Territory();
        dsTerritory = terr.getSFCode(div_code);
        if (dsTerritory.Tables[0].Rows.Count > 0)
        {
            ddlFieldForce.DataTextField = "Sf_Name";
            ddlFieldForce.DataValueField = "Sf_Code";
            ddlFieldForce.DataSource = dsTerritory;
            ddlFieldForce.DataBind();
            if (Session["sf_Code"] == null || Session["sf_Code"].ToString() == "admin")
            {
                ddlFieldForce.SelectedIndex = 0;
                sf_Code = ddlFieldForce.SelectedValue.ToString();
                Session["sf_Code"] = sf_Code;
            }
        }
    }
    protected void ddlFieldForce_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillProduct();
    }
    protected void txtNew_TextChanged(object sender, EventArgs e)
    {
        ddlFieldForce_SelectedIndexChanged(sender, e);
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.AbsoluteUri);
    }
}