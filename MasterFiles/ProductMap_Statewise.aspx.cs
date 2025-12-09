using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using Bus_EReport;
using System.Linq;
using System.Collections.Generic;


public partial class MasterFiles_ProductMap_Statewise : System.Web.UI.Page
{

    string div_code = string.Empty;
    string sub_division = string.Empty;
    DataSet dsState = null;
    DataSet dsDivision = null;
    DataSet dsProduct = null;
    DataSet dsSubDivision = null;
    string[] statecd;
    string state_cd = string.Empty;
    string sState = string.Empty;
    string state_code = string.Empty;
    string sf_code = string.Empty;
    string str_CateCode = string.Empty;
    DateTime ServerStartTime;
    DateTime ServerEndTime;
    int time;
    int sub_DivCode;
    int state_Code;
    int DivisionCode;

    int iIndex = -1;
    string[] numbers;

    protected void Page_Load(object sender, EventArgs e)
    {

        div_code = Session["div_code"].ToString();
        sf_code = Session["sf_code"].ToString();

        if (!Page.IsPostBack)
        {

            Session["backurl"] = "ProductList.aspx";
            menu1.Title = this.Page.Title;

            ServerStartTime = DateTime.Now;
            base.OnPreInit(e);

            ddlStateAll();
            ddlSubDivision();
            lblSelect.Visible = false;
            chkHeaderProduct.Visible = false;
            btnSubmit.OnClientClick = "return CheckBoxSelectionValidation();";
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
    private void ddlStateAll()
    {
        lblSelect.Visible = true;
        lblSelect.Text = "Select State wise Product";
        Division dv = new Division();
        dsDivision = dv.getStatePerDivision(div_code);
        if (dsDivision.Tables[0].Rows.Count > 0)
        {
            int i = 0;
            state_cd = string.Empty;
            sState = dsDivision.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
            statecd = sState.Split(',');
            foreach (string st_cd in statecd)
            {
                if (i == 0)
                {
                    state_cd = state_cd + st_cd;
                }
                else
                {
                    if (st_cd.Trim().Length > 0)
                    {
                        state_cd = state_cd + "," + st_cd;
                    }
                }
                i++;
            }

            State st = new State();
            dsState = st.getStateChkBox(state_cd);
            ddlStateProduct.DataTextField = "statename";
            ddlStateProduct.DataValueField = "state_code";
            ddlStateProduct.DataSource = dsState;
            ddlStateProduct.DataBind();
            ddlStateProduct.Items.Insert(0, new ListItem("---Select---", "0"));
        }
    }


    private void ddlSubDivision()
    {
        lblSelect.Visible = true;
        lblSelect.Text = "Select State wise Product";
        Product objProduct = new Product();
        dsSubDivision = objProduct.getSubDivisionDDL(div_code);
        ddlSubDivision1.DataTextField = "subdivision_name";
        ddlSubDivision1.DataValueField = "subdivision_code";
        ddlSubDivision1.DataSource = dsSubDivision;
        ddlSubDivision1.DataBind();
        ddlSubDivision1.Items.Insert(0, new ListItem("---Select---", "0"));


    }

    protected void btnGo_Click(object sender, EventArgs e)
    {
        lblSelect.Visible = true;
        chkHeaderProduct.Visible = true;
        DataList1.Visible = true;
        bindDataListProduct();
    }


    private void bindDataListProduct()
    {
        int sub_division = Convert.ToInt32(ddlSubDivision1.SelectedValue);
        int state_ID = Convert.ToInt32(ddlStateProduct.SelectedValue);
        int DivisionCode = Convert.ToInt32(div_code);

        lblSelect.Text = "Select State wise Product";
        btnSubmit.Visible = true;

        Product prod = new Product();
        // dsProduct=prod.getProduct_Selected_State(div_code,sub_division,state_ID);
        dsProduct = prod.getProductStatewise(DivisionCode, sub_division);

        if (dsProduct.Tables[0].Rows.Count > 0)
        {
            //btnSave.Visible = true;
            DataList1.Visible = true;
            DataList1.DataSource = dsProduct;
            DataList1.DataBind();
        }
        else
        {
            DataList1.DataSource = dsProduct;
            DataList1.DataBind();
        }

        string str_CateCode = "";

        DataSet dsProductState;
        Product objProduct = new Product();
        dsProductState = objProduct.getProduct_Selected_State(DivisionCode, sub_division, state_ID);

        if (dsProductState.Tables[0].Rows.Count > 0)
        {

            for (int i = 0; i < dsProductState.Tables[0].Rows.Count; i++)
            {
                str_CateCode = dsProductState.Tables[0].Rows[i].ItemArray.GetValue(0).ToString();

                foreach (DataListItem grid in DataList1.Items)
                {
                    Label chk = (Label)grid.FindControl("lblPrdCode");

                    string[] Salesforce;
                    if (str_CateCode != "")
                    {
                        iIndex = -1;
                        Salesforce = str_CateCode.Split(',');
                        foreach (string sf in Salesforce)
                        {

                            CheckBox chkCatName = (CheckBox)grid.FindControl("chkCatName");
                            Label hf = (Label)grid.FindControl("lblPrdCode");

                            if (sf == hf.Text)
                            {
                                chkCatName.Checked = true;
                                chkCatName.Attributes.Add("style", "Color: Red; font-weight:Bold; font-size:12px; ");
                            }
                        }
                    }
                }
            }
        }

        else
        {

        }


    }

    protected void btnclr_Click(object sender, EventArgs e)
    {
        Clear();
    }

    private void Clear()
    {
        ddlStateProduct.SelectedValue = "0";
        ddlSubDivision1.SelectedValue = "0";
        //ddlStateAll();
        DataList1.Visible = false;
        btnSubmit.Visible = false;
        lblSelect.Visible = false;
        chkHeaderProduct.Visible = false;
        chkHeaderProduct.Checked = false;
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        string strPrd = "";
        string srtpd = "";
        int ProductCode;

        //  string sub_div = ddlSubDivision1.SelectedValue.ToString() + ",";
        string stateCod = ddlStateProduct.SelectedValue.ToString() + ",";

        sub_DivCode = Convert.ToInt32(ddlSubDivision1.SelectedValue);
        int stCode = Convert.ToInt32(ddlStateProduct.SelectedValue);
        DivisionCode = Convert.ToInt32(div_code);

        foreach (DataListItem grid in DataList1.Items)
        {
            Label chk = (Label)grid.FindControl("lblPrdCode");
            CheckBox chkCatName = (CheckBox)grid.FindControl("chkCatName");

            strPrd = chk.Text;
            ProductCode = Convert.ToInt32(chk.Text);
            Product objProduct = new Product();
            DataSet dsStateSplit = objProduct.getProduct_StateSplit(DivisionCode, sub_DivCode, ProductCode);

            int Statec = Convert.ToInt32(ddlStateProduct.SelectedValue);

            if (dsStateSplit.Tables[0].Rows.Count > 0)
            {
                string StateCode_A = "";

                for (int i = 0; i < dsStateSplit.Tables[0].Rows.Count; i++)
                {
                    str_CateCode = dsStateSplit.Tables[0].Rows[i].ItemArray.GetValue(0).ToString();

                    if (str_CateCode == "")
                    {
                        str_CateCode = "0";
                    }

                    str_CateCode = str_CateCode.TrimEnd(',');

                    string[] Prd_code = str_CateCode.Split(',');
                    numbers = Prd_code;

                    int count = 1;
                    for (int k = 0; k < Prd_code.Length; k++)
                    {
                        int K1 = Convert.ToInt32(Prd_code[k]);
                        if (K1 == Statec)
                        {

                            //numbers =Prd_code;
                            int numToRemove = Convert.ToInt32(Prd_code[k]);
                            // int numIdx = Array.IndexOf(numbers, numToRemove);
                            List<string> tmp = new List<string>(numbers);
                            tmp.RemoveAt(k);
                            numbers = tmp.ToArray();

                        }
                    }
                }

                for (int i = 0; i < numbers.Length; i++)
                {
                    if (numbers[i] == "0")
                    {

                        int indexToRemove = i;
                        numbers = numbers.Where((source, index) => index != indexToRemove).ToArray();
                        StateCode_A = "";
                    }
                    else
                    {
                        StateCode_A += numbers[i] + ',';
                    }

                }

                if (chkCatName.Checked == true)
                {
                    StateCode_A += stateCod;
                }

                int _res = objProduct.RecordProduct_Update(strPrd, StateCode_A);

                if (_res > 0)
                {
                    //ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'> createCustomAlert('Mapped Successfully');</script>");
                    ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'> alert('Tagged Successfully');</script>");
                    DataList1.Visible = false;
                    btnSubmit.Visible = false;
                    // lblSelect.Text = "Select State wise Product";
                }
                else
                {
                   // ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'> createCustomAlert(' Not Mapped ');</script>");
                    ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'> alert('Not Tagged');</script>");
                }

            }

        }

        //bindDataListProduct();

        Clear();

    }




    protected void btnBack_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(time);
        Response.Redirect("ProductList.aspx");
    }
}