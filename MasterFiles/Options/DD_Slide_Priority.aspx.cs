using System;
using System.Data;
using Bus_EReport;

public partial class MasterFiles_Options_DD_Slide_Priority : System.Web.UI.Page
{
    #region Declaration
    Product prd = new Product();
    DataSet dsPrd = new DataSet();
    string div_code = string.Empty;
    string objPriority = string.Empty;
    string objDDBrand = string.Empty;
    string objSubDiv = string.Empty;
    string objDDProduct = string.Empty;
    string objDDSpec = string.Empty;
    string objDDTherapy = string.Empty;
    string objDDPriority = string.Empty;
    string objSubDivTxt = string.Empty;
    string objModeTxt = string.Empty;
    string objDDBrandTxt = string.Empty;
    #endregion

    #region Page_Events
    protected void Page_Load(object sender, EventArgs e)
    {
        div_code = Session["div_code"].ToString();
        objPriority = Request.QueryString["objPriority"];

        objDDBrand = objPriority.Split(',')[1];
        objSubDiv = objPriority.Split(',')[0];
        objDDProduct = objPriority.Split(',')[2];
        objDDSpec = objPriority.Split(',')[3];
        objDDTherapy = objPriority.Split(',')[4];
        objDDPriority = objPriority.Split(',')[5];
        objSubDivTxt = objPriority.Split(',')[6];
        objModeTxt = objPriority.Split(',')[7];
        objDDBrandTxt = objPriority.Split(',')[8];

        DD_SubDiv.Value = objSubDiv;
        DD_Brand.Value = objDDBrand;
        DD_Product.Value = objDDProduct;
        DD_Spec.Value = objDDSpec;
        DD_Therapy.Value = objDDTherapy;
        DD_Mode.Value = objDDPriority;
        DD_SubDivTxt.Value = objSubDivTxt;
        DD_ModeTxt.Value = objModeTxt;
        DD_BrandTxt.Value = objDDBrandTxt;
    }
    #endregion
}