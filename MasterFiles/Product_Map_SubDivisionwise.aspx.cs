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
using System.Drawing;
using System.Web.Services;
using System.Web.Script.Services;


public partial class MasterFiles_Frm_Product_SubDiv_Test : System.Web.UI.Page
{
    string div_code = string.Empty;
    string sub_division = string.Empty;
    string subDivCode = string.Empty;
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
    DataList dl1;

    int iIndex = -1;
    static string[] numbers;
    static string[] StCode;

    string subDivName = string.Empty;
    CheckBox chkSelect;

    protected void Page_Load(object sender, EventArgs e)
    {


        div_code = Session["div_code"].ToString();
        sf_code = Session["sf_code"].ToString();

        btnSubmit.Attributes.Add("style", "visibility: hidden");
        lblSelect.Attributes.Add("style", "visibility: hidden");
        pnl.Attributes.Add("style", "visibility: hidden");


        if (!Page.IsPostBack)
        {
            Session["backurl"] = "ProductList.aspx";
            menu1.Title = this.Page.Title;

            ServerStartTime = DateTime.Now;
            base.OnPreInit(e);

            // ddlStateAll();
            ddlSubDivision();

            btnSubmit.Attributes.Add("style", "visibility: hidden");
            lblSelect.Attributes.Add("style", "visibility: hidden");
            pnl.Attributes.Add("style", "visibility: hidden");

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

    //private void ddlStateAll()
    //{
    //    lblSelect.Visible = true;
    //    lblSelect.Text = "Select State wise Product";
    //    Division dv = new Division();
    //    dsDivision = dv.getStatePerDivision(div_code);

    //    if (dsDivision.Tables[0].Rows.Count > 0)
    //    {
    //        int i = 0;
    //        state_cd = string.Empty;
    //        sState = dsDivision.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
    //        statecd = sState.Split(',');
    //        foreach (string st_cd in statecd)
    //        {
    //            if (i == 0)
    //            {
    //                state_cd = state_cd + st_cd;
    //            }
    //            else
    //            {
    //                if (st_cd.Trim().Length > 0)
    //                {
    //                    state_cd = state_cd + "," + st_cd;
    //                }
    //            }
    //            i++;
    //        }

    //        State st = new State();
    //        dsState = st.getStateChkBox(state_cd);
    //        ddlStateProduct.DataTextField = "statename";
    //        ddlStateProduct.DataValueField = "state_code";
    //        ddlStateProduct.DataSource = dsState;
    //        ddlStateProduct.DataBind();
    //        ddlStateProduct.Items.Insert(0, new ListItem("---Select---", "0"));

    //    }
    //}


    private void ddlSubDivision()
    {

        ddlSubDivision1.Items.Clear();

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
        bindProductRecords();

    }

    [WebMethod(EnableSession = true)]
    [ScriptMethod]

    public static void AddProductDetail(SunDivProduct ProductData)
    {


        string SubDivCode = string.Empty;
        string StateCode = string.Empty;


        string SubDivName = ProductData.SubDivName;
        string ProductName = ProductData.ProductName;
        string ProductCode = ProductData.ProductCodeSlNo;
        string DivCode = HttpContext.Current.Session["div_code"].ToString();

        string SubDivId = ProductData.SubDivID + ",";
        string StateId = ProductData.StateID + ",";

        int ddlSubDivId = Convert.ToInt32(ProductData.SubDivID);
        int ddlStateId = Convert.ToInt32(ProductData.StateID);

        Product objProduct = new Product();
        DataSet ds_Sub_State_Split = objProduct.getSubDiv_StateSplit(DivCode, SubDivName, ProductCode);

        if (ds_Sub_State_Split.Tables[0].Rows.Count > 0)
        {
            string StateCode_A = "";
            string SubDiv_Code = "";

            for (int i = 0; i < ds_Sub_State_Split.Tables[0].Rows.Count; i++)
            {
                SubDivCode = ds_Sub_State_Split.Tables[0].Rows[i].ItemArray.GetValue(0).ToString();
                StateCode = ds_Sub_State_Split.Tables[0].Rows[i].ItemArray.GetValue(1).ToString();

                SubDivCode = SubDivCode.TrimEnd(',');

                string[] Prd_code = SubDivCode.Split(',');
                numbers = Prd_code;

                StateCode = StateCode.TrimEnd(',');

                string[] State_Code = StateCode.Split(',');
                StCode = State_Code;


                int count = 1;

                for (int k = 0; k < Prd_code.Length; k++)
                {
                    int K1 = Convert.ToInt32(Prd_code[k]);
                    if (K1 == ddlSubDivId)
                    {


                        int numToRemove = Convert.ToInt32(Prd_code[k]);
                        List<string> tmp = new List<string>(numbers);
                        tmp.RemoveAt(k);
                        numbers = tmp.ToArray();

                    }

                }

                for (int State = 0; State < State_Code.Length; State++)
                {
                    int K1 = Convert.ToInt32(State_Code[State]);
                    if (K1 == ddlStateId)
                    {
                        int numToRemove = Convert.ToInt32(State_Code[State]);
                        List<string> tmp = new List<string>(StCode);
                        tmp.RemoveAt(State);
                        StCode = tmp.ToArray();

                    }

                }

            }

            for (int i = 0; i < numbers.Length; i++)
            {
                SubDiv_Code += numbers[i] + ',';

            }

            for (int StId = 0; StId < StCode.Length; StId++)
            {
                StateCode_A += StCode[StId] + ',';

            }

            SubDiv_Code += SubDivId;

            StateCode_A += StateId;

            int _res = objProduct.Rec_ProductSubDiv_Update(ProductCode, SubDiv_Code, StateCode_A);

            if (_res > 0)
            {

            }
            else
            {

            }

        }

    }

    [WebMethod(EnableSession = true)]
    [ScriptMethod]

    public static void AddProductData(List<string> objProductData)
    {
        string U_Data;

        for (int d = 0; d < objProductData.Count; d++)
        {
            U_Data = objProductData[d].ToString();

            string[] values = U_Data.Split(',');

            string SubDivCode = string.Empty;
            string StateCode = string.Empty;


            string SubDivName = values[1].ToString();
            string ProductName = values[0].ToString();
            string ProductCode = values[2].ToString();
            string DivCode = HttpContext.Current.Session["div_code"].ToString();

            string SubDivId = values[3].ToString() + ",";
            string StateId = values[4].ToString() + ",";

            int ddlSubDivId = Convert.ToInt32(values[3].ToString());
            int ddlStateId = Convert.ToInt32(values[4].ToString());

            Product objProduct = new Product();
            DataSet ds_Sub_State_Split = objProduct.getSubDiv_StateSplit(DivCode, SubDivName, ProductCode);

            if (ds_Sub_State_Split.Tables[0].Rows.Count > 0)
            {
                string StateCode_A = "";
                string SubDiv_Code = "";

                for (int i = 0; i < ds_Sub_State_Split.Tables[0].Rows.Count; i++)
                {
                    SubDivCode = ds_Sub_State_Split.Tables[0].Rows[i].ItemArray.GetValue(0).ToString();
                    StateCode = ds_Sub_State_Split.Tables[0].Rows[i].ItemArray.GetValue(1).ToString();

                    SubDivCode = SubDivCode.TrimEnd(',');

                    string[] Prd_code = SubDivCode.Split(',');
                    numbers = Prd_code;

                    StateCode = StateCode.TrimEnd(',');

                    string[] State_Code = StateCode.Split(',');
                    StCode = State_Code;


                    int count = 1;

                    for (int k = 0; k < Prd_code.Length; k++)
                    {
                        int K1 = Convert.ToInt32(Prd_code[k]);
                        if (K1 == ddlSubDivId)
                        {


                            int numToRemove = Convert.ToInt32(Prd_code[k]);
                            List<string> tmp = new List<string>(numbers);
                            tmp.RemoveAt(k);
                            numbers = tmp.ToArray();

                        }

                    }

                    for (int State = 0; State < State_Code.Length; State++)
                    {
                        int K1 = Convert.ToInt32(State_Code[State]);
                        if (K1 == ddlStateId)
                        {
                            int numToRemove = Convert.ToInt32(State_Code[State]);
                            List<string> tmp = new List<string>(StCode);
                            tmp.RemoveAt(State);
                            StCode = tmp.ToArray();

                        }

                    }

                }

                for (int i = 0; i < numbers.Length; i++)
                {
                    SubDiv_Code += numbers[i] + ',';

                }

                for (int StId = 0; StId < StCode.Length; StId++)
                {
                    StateCode_A += StCode[StId] + ',';

                }

                SubDiv_Code += SubDivId;

                StateCode_A += StateId;

                int _res = objProduct.Rec_ProductSubDiv_Update(ProductCode, SubDiv_Code, StateCode_A);

                if (_res > 0)
                {

                }
                else
                {

                }

            }
        }


    }


    [WebMethod(EnableSession = true)]
    [ScriptMethod]

    public static void AddProductData_2(List<string> objProductData)
    {
        string U_Data;

        for (int d = 0; d < objProductData.Count; d++)
        {
            U_Data = objProductData[d].ToString();

            string[] values = U_Data.Split(',');


            // string StateCode = string.Empty;

            string SubDivCode = string.Empty;

            string SubDivName = values[1].ToString();
            string ProductName = values[0].ToString();
            string ProductCode = values[2].ToString();
            string DivCode = HttpContext.Current.Session["div_code"].ToString();

            string SubDivId = values[3].ToString() + ",";
            //  string StateId = values[4].ToString() + ",";

            int ddlSubDivId = Convert.ToInt32(values[3].ToString());
            // int ddlStateId = Convert.ToInt32(values[4].ToString());

            string Sub_Code = values[4].ToString() + ",";

            Product objProduct = new Product();
            DataSet ds_Sub_State_Split = objProduct.getSubDiv_StateSplit(DivCode, Sub_Code, ProductCode);

            if (ds_Sub_State_Split.Tables[0].Rows.Count > 0)
            {
                string StateCode_A = "";
                string SubDiv_Code = "";

                for (int i = 0; i < ds_Sub_State_Split.Tables[0].Rows.Count; i++)
                {
                    SubDivCode = ds_Sub_State_Split.Tables[0].Rows[i].ItemArray.GetValue(0).ToString();
                    // StateCode = ds_Sub_State_Split.Tables[0].Rows[i].ItemArray.GetValue(1).ToString();

                    SubDivCode = SubDivCode.TrimEnd(',');

                    string[] Prd_code = SubDivCode.Split(',');
                    numbers = Prd_code;

                    // StateCode = StateCode.TrimEnd(',');

                    // string[] State_Code = StateCode.Split(',');
                    // StCode = State_Code;


                    int count = 1;

                    for (int k = 0; k < Prd_code.Length; k++)
                    {
                        int K1 = Convert.ToInt32(Prd_code[k]);
                        if (K1 == ddlSubDivId)
                        {


                            int numToRemove = Convert.ToInt32(Prd_code[k]);
                            List<string> tmp = new List<string>(numbers);
                            tmp.RemoveAt(k);
                            numbers = tmp.ToArray();

                        }

                    }

                    //for (int State = 0; State < State_Code.Length; State++)
                    //{
                    //    int K1 = Convert.ToInt32(State_Code[State]);
                    //    if (K1 == ddlStateId)
                    //    {
                    //        int numToRemove = Convert.ToInt32(State_Code[State]);
                    //        List<string> tmp = new List<string>(StCode);
                    //        tmp.RemoveAt(State);
                    //        StCode = tmp.ToArray();

                    //    }

                    //}

                }

                for (int i = 0; i < numbers.Length; i++)
                {
                    SubDiv_Code += numbers[i] + ',';

                }

                //for (int StId = 0; StId < StCode.Length; StId++)
                //{
                //    StateCode_A += StCode[StId] + ',';

                //}

                SubDiv_Code += SubDivId;

                // StateCode_A += StateId;

                int _res = objProduct.Rec_ProductSubDiv_Update(ProductCode, SubDiv_Code, StateCode_A);

                if (_res > 0)
                {

                }
                else
                {

                }

            }
        }


    }


    private void bindProductRecords()
    {
        try
        {

            btnSubmit.Attributes.Add("style", "visibility: visible");
            lblSelect.Attributes.Add("style", "visibility: visible");
            pnl.Attributes.Add("style", "visibility: visible");

            lblSelect.Text = "Select SubDivision wise Product";

            Product objProduct = new Product();
            dsSubDivision = objProduct.getSubDivisionDDL(div_code);

            for (int i = 0; i < dsSubDivision.Tables[0].Rows.Count; i++)
            {
                subDivCode = dsSubDivision.Tables[0].Rows[i]["subdivision_code"].ToString();
                sub_division = dsSubDivision.Tables[0].Rows[i]["subdivision_name"].ToString();

                Session["SubDivCode"] = string.Empty;
                Session["SubDivCode"] = subDivCode;

                dsProduct = objProduct.getProduct_SubDivision_Select(div_code, subDivCode);
                DataColumn dc = dsProduct.Tables[0].Columns.Add("ID", typeof(int));
                for (int j = 0; j < dsProduct.Tables[0].Rows.Count; j++)
                {
                    dsProduct.Tables[0].Rows[j]["ID"] = j + 1;
                }

                if (dsProduct.Tables[0].Rows.Count > 0)
                {

                    pnl.Controls.Add(new LiteralControl("<br>"));

                    string ID = "Chkbox-" + i;
                    pnl.Controls.Add(new LiteralControl("<input type='checkbox' id=" + ID + " class='selectAll' /><lable style='font-size:14px;font-weight:bold;font-family:Arial;'>Select All</lable>"));
                    pnl.Controls.Add(new LiteralControl("<br/>"));
                    pnl.Controls.Add(new LiteralControl("<br>"));

                    dl1 = new DataList();

                    dl1.RepeatDirection = RepeatDirection.Vertical;
                    dl1.RepeatColumns = 3;

                    dl1.HeaderStyle.BackColor = Color.DarkSeaGreen;
                    dl1.HeaderTemplate = new MyTemplate(ListItemType.Header);
                    MyTemplate headTemplate = new MyTemplate(ListItemType.Header);
                    headTemplate.CategoryName = sub_division + "^" + subDivCode;
                    dl1.HeaderTemplate = headTemplate;

                    //MyTemplate headTemplate1 = new MyTemplate(ListItemType.Header);
                    //headTemplate.SubDivCode = subDivCode;
                    //dl1.HeaderTemplate = headTemplate1;

                    dl1.FooterTemplate = new MyTemplate(ListItemType.Footer);
                    dl1.ItemTemplate = new MyTemplate(ListItemType.Item);
                    MyTemplate ItemTemplate = new MyTemplate(ListItemType.Item);

                    dl1.SelectedItemTemplate = new MyTemplate(ListItemType.SelectedItem);
                    dl1.Width = Unit.Percentage(85);

                    dl1.GridLines = GridLines.Both;
                    dl1.BorderWidth = Unit.Pixel(1);

                    dl1.ID = "DataList-" + i;

                    dl1.Attributes["style"] = "border:1px solid black";


                    dl1.DataSource = dsProduct;

                    dl1.DataBind();

                    pnl.Controls.Add(dl1);
                }
                else
                {

                }
            }
        }
        catch (Exception ex)
        {
        }
    }

    private void Clear()
    {
        //ddlStateProduct.SelectedValue = "0";
        ddlSubDivision1.SelectedValue = "0";
        pnl.Visible = false;
        btnSubmit.Visible = false;
        lblSelect.Visible = false;

    }


}

public class SunDivProduct
{
    public string SubDivName { get; set; }
    public string ProductCodeSlNo { get; set; }
    public string ProductName { get; set; }
    public string ProductSaleUnit { get; set; }
    public string DivisionCode { get; set; }
    public string SubDivID { get; set; }
    public string StateID { get; set; }
}



public class MyTemplate : ITemplate
{
    private string _categoryName;
    private string _Product_Code_SlNo;
    private string _Product_Detail_Name;
    private string _Product_Sale_Unit;
    private string _SubCode;

    public string CategoryName
    {
        get
        {
            return _categoryName;
        }
        set
        {
            _categoryName = value;
        }
    }

    public string SubDivCode
    {
        get
        {
            return _SubCode;
        }
        set
        {
            _SubCode = value;
        }
    }

    public string Product_Code_SlNo
    {
        get
        {
            return _Product_Code_SlNo;
        }
        set
        {
            _Product_Code_SlNo = value;
        }
    }
    public string Product_Detail_Name
    {
        get
        {
            return _Product_Detail_Name;
        }
        set
        {
            _Product_Detail_Name = value;
        }
    }
    public string Product_Sale_Unit
    {
        get
        {
            return _Product_Sale_Unit;
        }
        set
        {
            _Product_Sale_Unit = value;
        }
    }

    ListItemType ItemType;
    public MyTemplate(ListItemType _ItemType)
    {
        ItemType = _ItemType;
    }

    #region ITemplate Members
    public void InstantiateIn(Control container)
    {
        Label lc = new
        Label();


        CheckBox chkP = new CheckBox();
        Label ls = new Label();

        Label lblHeader = new Label();

        Label lblProductID = new Label();

        Label lblItem = new Label();

        switch (ItemType)
        {

            case ListItemType.Header:

                string[] Data = CategoryName.Split('^');


                lblHeader.Text = "<div id='divHeader' style='text-align:center;background-color:#666699;font-size:14px;color:White; height:32px;font-weight:bold;'>" + Data[0] + "</div>";
                lblHeader.Text += "<input type='hidden' id='hdnSubDiv' value='" + Data[1] + "'/>";
                lblHeader.Text += "<div style='height: 30px; width: 100%;background-color:LightSeaGreen;font-size:13px;color:White; height:32px;font-weight:bold;'>"
                                      + "<div style='width: 33%; float: left; height: 30px'>"
                                              + "<table style='width: 100%'>"
                                              + "<tr>"
                                              + "<td style='min-width:65px;max-width:75px'>SL No</td>"
                                              + "<td style='min-width:260px;max-width:310px'><div style='margin-top: 6px; margin-left: 25px'>Product Name</div></td>"
                                              + "<td style='min-width:65px;max-width:75px'><div style='margin-top: 6px; margin-left:15px'>Pack</div></td>"
                                              + "</tr>"
                                              + "</table>"
                                      + "</div>"
                                      + "<div style='width: 33%; float: left; height: 30px;'>"
                                              + "<table style='width: 100%'>"
                                              + "<tr>"
                                              + "<td style='min-width:65px;max-width:75px'>SL No</td>"
                                              + "<td style='min-width:260px;max-width:310px'><div style='margin-top: 6px; margin-left: 25px'>Product Name</div></td>"
                                              + "<td style='min-width:65px;max-width:75px'><div style='margin-top: 6px; margin-left:15px'>Pack</div></td>"
                                              + "</tr>"
                                              + "</table>"
                                      + "</div>"
                                      + "<div style='width: 33%; float: left; height: 30px;'>"
                                              + "<table style='width: 100%'>"
                                              + "<tr>"
                                              + "<td style='min-width:65px;max-width:75px'>SL No</td>"
                                              + "<td style='min-width:260px;max-width:310px'><div style='margin-top: 6px; margin-left: 25px'>Product Name</div></td>"
                                              + "<td style='min-width:65px;max-width:75px'><div style='margin-top: 6px; margin-left:15px'>Pack</div></td>"
                                              + "</tr>"
                                              + "</table>"
                                      + "</div>"
                                  + "</div>";

                container.Controls.Add(lblHeader);
                break;
            case
            ListItemType.Item:


                lc.DataBinding += new EventHandler(TemplateControl_DataBinding);
                chkP.DataBinding += new EventHandler(CHKControl_DataBinding);
                ls.DataBinding += new EventHandler(ProductSale_DataBinding);
                lblProductID.DataBinding += new EventHandler(ProductSNo_DataBinding);

                container.Controls.Add(lc);
                container.Controls.Add(lblProductID);
                container.Controls.Add(chkP);
                container.Controls.Add(ls);


                break;

        }


    }

    private void TemplateControl_DataBinding(object sender, System.EventArgs e)
    {
        Label lc = (Label)sender;
        DataListItem container = (DataListItem)lc.NamingContainer;

        lc.Text = "<div style='width:400px; float: left; height: 30px;border:1px solid black'><div style='min-width:65px;max-width:75px; float: left; height: 30px;'>"
           + "<div style='margin-top: 6px; margin-left: 5px'>" + DataBinder.Eval(container.DataItem, "ID") + "</div></div>";

    }

    private void ProductSNo_DataBinding(object sender, System.EventArgs e)
    {
        Label lc = (Label)sender;
        DataListItem container = (DataListItem)lc.NamingContainer;

        lc.Text = "<div style='width:10px; float: left; height: 30px'>"
         + "<div id='myDiv' style='margin-top: 6px; margin-left: 5px;visibility:hidden'>" + DataBinder.Eval(container.DataItem, "Product_Code_SlNo") + "</div></div>";


    }

    private void CHKControl_DataBinding(object sender, System.EventArgs e)
    {
        CheckBox chkProduct = (CheckBox)sender;
        DataListItem container1 = (DataListItem)chkProduct.NamingContainer;


        chkProduct.Text = DataBinder.Eval(container1.DataItem, "Product_Detail_Name").ToString();


    }

    private void ProductSale_DataBinding(object sender, System.EventArgs e)
    {
        Label lc = (Label)sender;
        DataListItem container = (DataListItem)lc.NamingContainer;

        lc.Text = "<div style='min-width:65px;max-width:75px; float: right; height: 30px'>"
         + "<div style='margin-top: 6px; margin-left: 5px'>" + DataBinder.Eval(container.DataItem, "Product_Sale_Unit") + "</div></div></div>";

    }

    #endregion
}


