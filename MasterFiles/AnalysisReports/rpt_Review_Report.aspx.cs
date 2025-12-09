using System;
using System.Web.UI.HtmlControls;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Windows;
using System.IO;
using System.Data;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.xml.simpleparser;
using iTextSharp.text.xml;
using iTextSharp.text.io;
using Bus_EReport;
using DBase_EReport;
using System.Net;
using System.Data;
using System.Data.SqlClient;
public partial class MasterFiles_AnalysisReports_rpt_Review_Report : System.Web.UI.Page
{
    string sf_code = string.Empty;
    string FMonth = string.Empty;
    string FYear = string.Empty;
    string TMonth = string.Empty;
    string TYear = string.Empty;
    string div_code = string.Empty;
    string strFieledForceName = string.Empty;
    string stk_Erp = string.Empty;
    string Primay_Head = string.Empty;
    DataSet dsDet = new DataSet();
    string sDate = string.Empty;
    ListedDR Cat = new ListedDR();
    DataSet dsCat = new DataSet();
    DataSet dsOneVisit = new DataSet();
    DataSet dsTwoVisit = new DataSet();
    DataSet dsThreeVisit = new DataSet();
    DataSet dsmore = new DataSet();
    DataSet dsSS = new DataSet();
    DataSet dscatVisit = new DataSet();
    DataSet dscatVisitSeen = new DataSet();
    DataSet dsHq = new DataSet();
    DataSet dsEx = new DataSet();
    DataSet dsOs = new DataSet();
    DataSet dsMiscel = new DataSet();
    DataSet dsExpTot = new DataSet();
    DataSet dsadj = new DataSet();
    DataSet dsdetail = new DataSet();
    DataSet dsrx = new DataSet();
    DataSet dsprod = new DataSet();
    DataSet dsCallAd = new DataSet();
    DataSet dstotSale = new DataSet();
    DataSet dsService = new DataSet();
    DataSet dsStok = new DataSet();
    DataSet dsprimHead = new DataSet();
    DataSet dsprimDet = new DataSet();
    double exphq = 0.0;
    double expex = 0.0;
    double expos = 0.0;
    double expmis = 0.0;
    double othtot = 0.0;
    DataSet dsoth = new DataSet();
    int adj = 0;
    int amt = 0;
    string typ = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!Page.IsPostBack)
        {

            div_code = Request.QueryString["div_code"].ToString();
            //sMode = Request.QueryString["cMode"].ToString();
            sf_code = Request.QueryString["sf_code"].ToString();
            FMonth = Request.QueryString["FMonth"].ToString();
            FYear = Request.QueryString["Fyear"].ToString();
            sDate = FMonth + "-01-" + FYear;
            strFieledForceName = Request.QueryString["sf_name"].ToString();
            SalesForce sf = new SalesForce();
            string strFrmMonth = sf.getMonthName(FMonth);
            //string strToMonth = sf.getMonthName(TMonth);
            lblHead.Text = "Review Report for Baselevel Employee for the Month of " + strFrmMonth + " " + FYear;

            dsDet = sf.GetMr_Det(div_code, sf_code);
            if (dsDet.Tables[0].Rows.Count > 0)
            {
                LblForceName.Text = " " + strFieledForceName;

                lblDesig.Text = " " + dsDet.Tables[0].Rows[0].ItemArray.GetValue(2).ToString();
                lblHQ.Text = " " + dsDet.Tables[0].Rows[0].ItemArray.GetValue(1).ToString();
                lblEmp.Text = " " + dsDet.Tables[0].Rows[0].ItemArray.GetValue(3).ToString();
                lblSt.Text = " " + dsDet.Tables[0].Rows[0].ItemArray.GetValue(5).ToString();
                lblDiv.Text = " " + dsDet.Tables[0].Rows[0].ItemArray.GetValue(4).ToString();


            }
            if (Request.QueryString["Dashboard"] != null)//Dashboard
            {
                pnlbutton.Visible = false;
            }

            FillList();
        }
    }
    private void FillList()
    {
        SalesForce sf = new SalesForce();
        DCR dcc = new DCR();
        DB_EReporting db = new DB_EReporting();
        string strConn = System.Configuration.ConfigurationManager.ConnectionStrings["Ereportcon"].ToString();
        SqlConnection con = new SqlConnection(strConn);
        con.Open();
        string sProc_Name = "";

        sProc_Name = "Review_Report";

        //string sqry = "EXEC visit_fixation_Cat " + Convert.ToInt32(div_code) + ",'" + ddlFieldForce.SelectedValue.ToString() + "',"+dtMnYr+"";
        SqlCommand cmd = new SqlCommand(sProc_Name, con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Div_Code", Convert.ToInt32(div_code));
        cmd.Parameters.AddWithValue("@Msf_code", sf_code);
        cmd.Parameters.AddWithValue("@cMnth", FMonth);
        cmd.Parameters.AddWithValue("@cYrs", FYear);
        cmd.CommandTimeout = 600;
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet dsts = new DataSet();
        da.Fill(dsts);
        if (dsts.Tables[0].Rows.Count > 0)
        {
            lblWork.Text = dsts.Tables[0].Rows[0]["1_D_TTL"].ToString();
            lblHol.Text = dsts.Tables[0].Rows[0]["1_DD_TTL"].ToString();
            lblLeave.Text = dsts.Tables[0].Rows[0]["1_G_TTL"].ToString();
            lblFW.Text = dsts.Tables[0].Rows[0]["1_E_TTL"].ToString();
            lblNFW.Text = dsts.Tables[0].Rows[0]["1_F_TTL"].ToString();
            
            lbldrs.Text = dsts.Tables[0].Rows[0]["1_0_AAT"].ToString();
            lblChem.Text = dsts.Tables[0].Rows[0]["1_0_ACT"].ToString();
            lblUnlst.Text = dsts.Tables[0].Rows[0]["1_0_ADT"].ToString();
            lblHosp.Text = dsts.Tables[0].Rows[0]["1_0_AET"].ToString();
            lblstk.Text = dsts.Tables[0].Rows[0]["1_0_AFT"].ToString();

            lblCMet.Text = dsts.Tables[0].Rows[0]["1_0_AGT"].ToString();
            lblCSeen.Text = dsts.Tables[0].Rows[0]["1_0_AHT"].ToString();

            lblTPHq.Text = dsts.Tables[0].Rows[0]["1_V1_TTL"].ToString();
            lblTPEx.Text = dsts.Tables[0].Rows[0]["1_V2_TTL"].ToString();
            lblTPOs.Text = dsts.Tables[0].Rows[0]["1_V3_TTL"].ToString();
            lblDcrHq.Text = dsts.Tables[0].Rows[0]["1_V4_TTL"].ToString();
            lblDcrEx.Text = dsts.Tables[0].Rows[0]["1_V5_TTL"].ToString();
            lblDcrOs.Text = dsts.Tables[0].Rows[0]["1_V6_TTL"].ToString();


            if (lblChem.Text != "0")
            {
                lblCMiss.Text = Convert.ToString(Convert.ToInt32(lblChem.Text) - Convert.ToInt32(lblCMet.Text));
            }
            else
            {
                lblCMiss.Text = "-";
            }
            lblUMet.Text = dsts.Tables[0].Rows[0]["1_0_AIT"].ToString();
            lblUseen.Text = dsts.Tables[0].Rows[0]["1_0_AJT"].ToString();
            if (lblUnlst.Text != "0")
            {
                lblUmiss.Text = Convert.ToString(Convert.ToInt32(lblUnlst.Text) - Convert.ToInt32(lblUMet.Text));
            }
            else
            {
                lblUmiss.Text = "-";
            }
            lblmet.Text = dsts.Tables[0].Rows[0]["1_0_ABT"].ToString();
            lblSeen.Text = dsts.Tables[0].Rows[0]["1_H_TTL"].ToString();
            if (lbldrs.Text != "0")
            {
            lblCov.Text = (Decimal.Divide((Convert.ToInt32(lblmet.Text) * 100), Convert.ToInt32(lbldrs.Text))).ToString("0.##");
            }
            if(lblFW.Text != "0")
            {
              lblCallAvg.Text =  (Decimal.Divide(Convert.ToInt32(lblSeen.Text), Convert.ToInt32(lblFW.Text))).ToString("0.##");
              lblCCall.Text = (Decimal.Divide(Convert.ToInt32(lblCSeen.Text), Convert.ToInt32(lblFW.Text))).ToString("0.##");
              lblUCall.Text = (Decimal.Divide(Convert.ToInt32(lblUseen.Text), Convert.ToInt32(lblFW.Text))).ToString("0.##");
            }

            if (lbldrs.Text != "0")
            {
                lblmiss.Text = Convert.ToString(Convert.ToInt32(lbldrs.Text) - Convert.ToInt32(lblmet.Text));
            }
            else
            {
                lblmiss.Text = "-";
            }

            lblJWDays.Text = dsts.Tables[0].Rows[0]["1_L_TTL"].ToString();
            lblJWMet.Text = dsts.Tables[0].Rows[0]["1_M_TTL"].ToString();
            lblJWSeen.Text = dsts.Tables[0].Rows[0]["1_N_TTL"].ToString();
            if (lblJWDays.Text != "0")
            {
                lblJWCall.Text = (Decimal.Divide(Convert.ToInt32(lblJWSeen.Text), Convert.ToInt32(lblJWDays.Text))).ToString("0.##");
            }

            lblSamQty.Text = dsts.Tables[0].Rows[0]["1_R_TTL"].ToString();
            lblSamDrs.Text = dsts.Tables[0].Rows[0]["1_S_TTL"].ToString();
            lblSamProd.Text = dsts.Tables[0].Rows[0]["1_T_TTL"].ToString();

            lblInQty.Text = dsts.Tables[0].Rows[0]["1_U1_TTL"].ToString();
            lblInDrs.Text = dsts.Tables[0].Rows[0]["1_U2_TTL"].ToString();
            lblInProd.Text = dsts.Tables[0].Rows[0]["1_U3_TTL"].ToString();


            Expense exp = new Expense();
            dsHq = exp.getFare_HQ(sf_code, Convert.ToInt32(FMonth), Convert.ToInt32(FYear));
            if (dsHq.Tables[0].Rows.Count > 0)
            {
                exphq = Convert.ToDouble(dsHq.Tables[0].Rows[0]["cnt"].ToString());               
                lblExpHQ.Text = dsHq.Tables[0].Rows[0]["cnt"].ToString();               
                lblHdays.Text = dsHq.Tables[0].Rows[0]["days"].ToString() + " Days";               
                
            }
            else
            {
                lblHdays.Text = " - ";
            }
            dsEx = exp.getFare_EX(sf_code, Convert.ToInt32(FMonth), Convert.ToInt32(FYear));
            if (dsEx.Tables[0].Rows.Count > 0)
            {
                expex = Convert.ToDouble(dsEx.Tables[0].Rows[0]["cnt"].ToString());
                lblExpEx.Text = dsEx.Tables[0].Rows[0]["cnt"].ToString();

                lblEdays.Text = dsEx.Tables[0].Rows[0]["days"].ToString() + " Days";              
               
            }
           else
            { 
                lblEdays.Text = " - ";
            }
            dsOs = exp.getFare_OS(sf_code, Convert.ToInt32(FMonth), Convert.ToInt32(FYear));
            if (dsOs.Tables[0].Rows.Count > 0)
            {
                expos = Convert.ToDouble(dsOs.Tables[0].Rows[0]["cnt"].ToString());   
                lblExpOs.Text = dsOs.Tables[0].Rows[0]["cnt"].ToString();              
                lblOdays.Text = dsOs.Tables[0].Rows[0]["days"].ToString() + " Days";              
                
            }
            else
            {
                lblOdays.Text = " - ";
            }

            dsMiscel = exp.getmisel(sf_code, FMonth, FYear);
            if (dsMiscel.Tables[0].Rows.Count > 0)
            {
                expmis = Convert.ToDouble(dsMiscel.Tables[0].Rows[0]["mis_Amt"].ToString());       
               // lblmiscel.Text = dsMiscel.Tables[0].Rows[0]["mis_Amt"].ToString();       
            }

            SecSale sale = new SecSale();
            dstotSale = sale.Get_tot_SSale(div_code, sf_code, Convert.ToInt32(FMonth), Convert.ToInt32(FYear));
            if (dstotSale.Tables[0].Rows.Count > 0)
            {
                lbltotsale.Text = dstotSale.Tables[0].Rows[0]["cnt"].ToString();    
            }
            ListedDR lsted = new ListedDR();
            dsService = lsted.Get_Doc_Servie_Tot(sf_code, div_code, Convert.ToInt32(FMonth), Convert.ToInt32(FYear));
            if (dsService.Tables[0].Rows.Count > 0)
            {
                lblDrservice.Text = dsService.Tables[0].Rows[0]["Service_Amt"].ToString();
            }
            else
            {
                lblDrservice.Text = " - ";
            }

            dsStok = sale.getStockiestDet(sf_code, div_code);
            if (dsStok.Tables[0].Rows.Count > 0)
            {

                foreach (DataRow dr in dsStok.Tables[0].Rows)
                {
                    string stk = dr["Stockist_Designation"].ToString();

                    stk_Erp += stk + "','";
                }
                stk_Erp = "'" + stk_Erp;
                stk_Erp = stk_Erp.Remove(stk_Erp.Length - 2); 
                

                dsprimHead = sale.Primary_SSale_Head(div_code, sf_code, Convert.ToInt32(FMonth), Convert.ToInt32(FYear), stk_Erp);
                if (dsprimHead.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in dsprimHead.Tables[0].Rows)
                    {
                        string Trans_Sl_No = dr["Trans_Sl_No"].ToString();

                        Primay_Head += Trans_Sl_No + ',';
                    }
                    Primay_Head = Primay_Head.Remove(Primay_Head.Length - 1);
                    dsprimDet = sale.Primary_Tot_Amt(div_code, Primay_Head);
                    if (dsprimDet.Tables[0].Rows.Count > 0)
                    {
                        lblPrimSale.Text = dsprimDet.Tables[0].Rows[0]["Amount"].ToString();
                    }
                }
             
            }

            dsoth = exp.exp_others(sf_code, FMonth, FYear);
            if (dsoth.Tables[0].Rows.Count > 0)
            {
                othtot = Convert.ToDouble(dsoth.Tables[0].Rows[0]["cnt"].ToString());      
            }
                dsExpTot = exp.EXP_Total(sf_code, FMonth, FYear);
                if(dsExpTot.Tables[0].Rows.Count > 0)
                {
                   
                       dsadj = exp.get_exp_info_adj(sf_code, FMonth, FYear);
                       if (dsadj.Tables[0].Rows.Count > 0)
                       {
                           typ = dsadj.Tables[0].Rows[0]["Typ"].ToString();
                           amt = Convert.ToInt32(dsadj.Tables[0].Rows[0]["Amt"].ToString());
                       }
                       
                    if (typ == "-")
                    {
                        double tot = Convert.ToDouble(dsExpTot.Tables[0].Rows[0]["cnt"].ToString()) - amt;
                        lblexpSpent.Text = Convert.ToString(tot);

                        double misc = othtot - amt;
                        lblmiscel.Text = Convert.ToString(misc);
                        double exptot = exphq + expex + expos + misc;
                        lbltotalexp.Text = Convert.ToString(exptot);
                    }
                    else
                    {
                        double tot = Convert.ToDouble(dsExpTot.Tables[0].Rows[0]["cnt"].ToString()) + amt;
                        lblexpSpent.Text = Convert.ToString(tot);
                        double misc = othtot +  amt;
                        lblmiscel.Text = Convert.ToString(misc);
                        double exptot = exphq + expex + expos + misc;
                        lbltotalexp.Text = Convert.ToString(exptot);
                    }
                }
           

            dsDet = Cat.GetCat_DRs(sf_code, div_code, sDate);
            //TableRow tr_header = new TableRow();
        

            //TableCell tc_DR_Name = new TableCell();
          
            //Literal lit_DR_Name = new Literal();
            //lit_DR_Name.Text = "DR CAT INFO";
        
            //tc_DR_Name.Controls.Add(lit_DR_Name);
            //tr_header.Cells.Add(tc_DR_Name);
            DCR dc = new DCR();
            dsOneVisit = dc.visit_cnt_1(sf_code, div_code, Convert.ToInt32(FMonth), Convert.ToInt32(FYear));
            if (dsOneVisit.Tables[0].Rows.Count > 0)
            {
                lbl1Vis.Text = " " + dsOneVisit.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
            }
            dsTwoVisit = dc.visit_cnt_2(sf_code, div_code, Convert.ToInt32(FMonth), Convert.ToInt32(FYear));
            if (dsTwoVisit.Tables[0].Rows.Count > 0)
            {
                lbl2Vis.Text = " " + dsTwoVisit.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
            }
            dsThreeVisit = dc.visit_cnt_3(sf_code, div_code, Convert.ToInt32(FMonth), Convert.ToInt32(FYear));
            if (dsThreeVisit.Tables[0].Rows.Count > 0)
            {
                lbl3Vis.Text = " " + dsThreeVisit.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
            }
            dsmore = dc.visit_cnt_more(sf_code, div_code, Convert.ToInt32(FMonth), Convert.ToInt32(FYear));
            if (dsmore.Tables[0].Rows.Count > 0)
            {
                lblmore.Text = " " + dsmore.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
            }
            if (dsDet.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < dsDet.Tables[0].Rows.Count; i++)
                {
                    TableRow tr_det = new TableRow();
                    TableCell tc_det_FF1 = new TableCell();
                    Literal lit_det_FF1 = new Literal();
                    lit_det_FF1.Text = "" + dsDet.Tables[0].Rows[i]["cat"].ToString() + " drs in List";
              
                    tc_det_FF1.Controls.Add(lit_det_FF1);
                 
                    tr_det.Cells.Add(tc_det_FF1);

                    TableCell tc_det_FF2 = new TableCell();
                    Literal lit_det_FF2 = new Literal();
                    lit_det_FF2.Text = "" + dsDet.Tables[0].Rows[i]["cnt"].ToString();
                   
                    tc_det_FF2.Controls.Add(lit_det_FF2);
           
                    tr_det.Cells.Add(tc_det_FF2);
                    tbl.Rows.Add(tr_det);

                    TableRow tr_det2 = new TableRow();

                    TableCell tc_det_FF3 = new TableCell();
                    Literal lit_det_FF3 = new Literal();
                    lit_det_FF3.Text = "" + dsDet.Tables[0].Rows[i]["cat"].ToString() + " drs Visit";

                    tc_det_FF3.Controls.Add(lit_det_FF3);

                    tr_det2.Cells.Add(tc_det_FF3);

                    TableCell tc_det_FF4 = new TableCell();
                    Literal lit_det_FF4 = new Literal();
                    lit_det_FF4.Text = "" + dsDet.Tables[0].Rows[i]["No_of_visit"].ToString() +" times";

                    tc_det_FF4.Controls.Add(lit_det_FF4);

                    tr_det2.Cells.Add(tc_det_FF4);

                    tbl.Rows.Add(tr_det2);

                  

                }
            }
            dscatVisit = Cat.Get_Cat_Met(sf_code, div_code, Convert.ToInt32(FMonth), Convert.ToInt32(FYear));
            if (dscatVisit.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < dscatVisit.Tables[0].Rows.Count; i++)
                {
                    TableRow tr_de = new TableRow();
                    TableCell tc_de_F = new TableCell();
                    Literal lit_de_F = new Literal();
                    lit_de_F.Text = "" + dscatVisit.Tables[0].Rows[i]["Doc_Cat_SName"].ToString() + " drs Met";

                    tc_de_F.Controls.Add(lit_de_F);

                    tr_de.Cells.Add(tc_de_F);

                    TableCell tc_det_F2 = new TableCell();
                    Literal lit_det_F2 = new Literal();
                    lit_det_F2.Text = "" + dscatVisit.Tables[0].Rows[i]["met"].ToString();

                    tc_det_F2.Controls.Add(lit_det_F2);

                    tr_de.Cells.Add(tc_det_F2);
                    tblcat.Rows.Add(tr_de);

                    TableRow tr_det2 = new TableRow();

                    TableCell tc_det_FF3 = new TableCell();
                    Literal lit_det_FF3 = new Literal();
                    lit_det_FF3.Text = "" + dscatVisit.Tables[0].Rows[i]["Doc_Cat_SName"].ToString() + " drs Seen";

                    tc_det_FF3.Controls.Add(lit_det_FF3);

                    tr_det2.Cells.Add(tc_det_FF3);

                    TableCell tc_det_FF4 = new TableCell();
                    Literal lit_det_FF4 = new Literal();
                    lit_det_FF4.Text = "" + dscatVisit.Tables[0].Rows[i]["seen"].ToString();

                    tc_det_FF4.Controls.Add(lit_det_FF4);

                    tr_det2.Cells.Add(tc_det_FF4);

                    tblcat.Rows.Add(tr_det2);

                    TableRow tr_det3 = new TableRow();

                    TableCell tc_det_FF5 = new TableCell();
                    Literal lit_det_FF5 = new Literal();
                    lit_det_FF5.Text = "" + dscatVisit.Tables[0].Rows[i]["Doc_Cat_SName"].ToString() + " drs Coverage";

                    tc_det_FF5.Controls.Add(lit_det_FF5);

                    tr_det3.Cells.Add(tc_det_FF5);

                    TableCell tc_det_FF6 = new TableCell();
                    Literal lit_det_FF6 = new Literal();
                    lit_det_FF6.Text = "" + dscatVisit.Tables[0].Rows[i]["avg"].ToString();

                    tc_det_FF6.Controls.Add(lit_det_FF6);

                    tr_det3.Cells.Add(tc_det_FF6);

                    tblcat.Rows.Add(tr_det3);
                }
            }
          
            SecSale ss = new SecSale();
            dsSS = ss.Get_top_SSale(div_code, sf_code, Convert.ToInt32(FMonth),Convert.ToInt32(FYear));
            if (dsSS.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < dsSS.Tables[0].Rows.Count; i++)
                {
                    TableRow tr_det = new TableRow();
                    TableCell tc_det_FF1 = new TableCell();
                    Literal lit_det_FF1 = new Literal();
                    lit_det_FF1.Text = "" + dsSS.Tables[0].Rows[i]["Product_Detail_Name"].ToString();

                    tc_det_FF1.Controls.Add(lit_det_FF1);

                    tr_det.Cells.Add(tc_det_FF1);

                    TableCell tc_det_FF2 = new TableCell();
                    Literal lit_det_FF2 = new Literal();
                    lit_det_FF2.Text = "" + dsSS.Tables[0].Rows[i]["cnt"].ToString();

                    tc_det_FF2.Controls.Add(lit_det_FF2);

                    tr_det.Cells.Add(tc_det_FF2);
                    tblSec.Rows.Add(tr_det);
                }
            }

            else
            {
                lblNoRecord.Visible = true;
            }
            Product prod =new Product();
            dsdetail = prod.Product_Det_drs(div_code, sf_code, FMonth, FYear);
            if (dsdetail.Tables[0].Rows.Count > 0)
            {
                lblProdDet.Text = dsdetail.Tables[0].Rows[0]["cnt"].ToString();   
            }
            dsrx = prod.Rx_drs(div_code, sf_code, FMonth, FYear);
            if (dsrx.Tables[0].Rows.Count > 0)
            {
                lblPres.Text = dsrx.Tables[0].Rows[0]["cnt"].ToString();
            }

            dsprod = prod.top_Product_Det_drs(div_code, sf_code, FMonth, FYear);
            if (dsprod.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < dsprod.Tables[0].Rows.Count; i++)
                {
                    TableRow tr_det = new TableRow();
                    TableCell tc_det_FF1 = new TableCell();
                    Literal lit_det_FF1 = new Literal();
                    lit_det_FF1.Text = "" + dsprod.Tables[0].Rows[i]["Product_Detail_Name"].ToString();

                    tc_det_FF1.Controls.Add(lit_det_FF1);

                    tr_det.Cells.Add(tc_det_FF1);

                    TableCell tc_det_FF2 = new TableCell();
                    Literal lit_det_FF2 = new Literal();
                    lit_det_FF2.Text = "" + dsprod.Tables[0].Rows[i]["cntT"].ToString();

                    tc_det_FF2.Controls.Add(lit_det_FF2);

                    tr_det.Cells.Add(tc_det_FF2);
                    tblProddet.Rows.Add(tr_det);
                }
            }

            else
            {
                lblNO.Visible = true;
            }
            ListedDR lst = new ListedDR();
            dsCallAd = lst.Get_Cat_Visit_days(sf_code, div_code, Convert.ToInt32(FMonth), Convert.ToInt32(FYear));
            if (dsCallAd.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < dsCallAd.Tables[0].Rows.Count; i++)
                {
                    TableRow tr_de = new TableRow();
                    TableCell tc_de_F = new TableCell();
                    Literal lit_de_F = new Literal();
                    lit_de_F.Text = "" + dsCallAd.Tables[0].Rows[i]["Doc_Cat_SName"].ToString() + " drs Met ( " + dsCallAd.Tables[0].Rows[i]["visit"].ToString() + " times )";

                    tc_de_F.Controls.Add(lit_de_F);

                    tr_de.Cells.Add(tc_de_F);

                    TableCell tc_det_F2 = new TableCell();
                    Literal lit_det_F2 = new Literal();
                    lit_det_F2.Text = "" + dsCallAd.Tables[0].Rows[i]["met"].ToString();

                    tc_det_F2.Controls.Add(lit_det_F2);

                    tr_de.Cells.Add(tc_det_F2);
                    tblcatVisit.Rows.Add(tr_de);

                    TableRow tr_det2 = new TableRow();

                    TableCell tc_det_FF3 = new TableCell();
                    Literal lit_det_FF3 = new Literal();
                    lit_det_FF3.Text = "" + dsCallAd.Tables[0].Rows[i]["Doc_Cat_SName"].ToString() + " drs Coverage";

                    tc_det_FF3.Controls.Add(lit_det_FF3);

                    tr_det2.Cells.Add(tc_det_FF3);

                    TableCell tc_det_FF4 = new TableCell();
                    Literal lit_det_FF4 = new Literal();
                    lit_det_FF4.Text = "" + dsCallAd.Tables[0].Rows[i]["avg"].ToString();

                    tc_det_FF4.Controls.Add(lit_det_FF4);

                    tr_det2.Cells.Add(tc_det_FF4);

                    tblcatVisit.Rows.Add(tr_det2);

                    TableRow tr_det3 = new TableRow();

                    TableCell tc_det_FF5 = new TableCell();
                    Literal lit_det_FF5 = new Literal();
                    lit_det_FF5.Text = "" + dsCallAd.Tables[0].Rows[i]["Doc_Cat_SName"].ToString() + " drs Missed";

                    tc_det_FF5.Controls.Add(lit_det_FF5);

                    tr_det3.Cells.Add(tc_det_FF5);

                    TableCell tc_det_FF6 = new TableCell();
                    Literal lit_det_FF6 = new Literal();
                    lit_det_FF6.Text = "" + dsCallAd.Tables[0].Rows[i]["miss"].ToString();

                    tc_det_FF6.Controls.Add(lit_det_FF6);

                    tr_det3.Cells.Add(tc_det_FF6);

                    tblcatVisit.Rows.Add(tr_det3);
                }
            }
          
        }
    }
}