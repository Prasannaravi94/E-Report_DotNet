using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Bus_EReport;
using System.Data;
using System.Xml;
using System.Drawing;
using System.Configuration;

public partial class MasterFiles_MR_DCR_MR_DCR_Approval : System.Web.UI.Page
{
    #region "Declaration"
    string mgr_sfCode = string.Empty;
    DateTime dtDCR;
    DataSet dsDCR;
    string sf_code = string.Empty;
    string trans_slno = string.Empty;
    string sCurDate = string.Empty;
    string sFile = string.Empty;
    DateTime ServerStartTime;
    DateTime ServerEndTime;
    DataSet dsxml;
    int time;
    int sslno;
    int iret = 0;
    string sQryStr = string.Empty;
    string sfcode = string.Empty;
    string lblcurdate = string.Empty;
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        mgr_sfCode = Session["sf_code"].ToString();
        sf_code = Request.QueryString["sfcode"].ToString();
        sCurDate = Request.QueryString["Activity_Date"].ToString();
        trans_slno = Request.QueryString["Trans_Slno"].ToString();
        DCR dc = new DCR();
        SalesForce sf = new SalesForce();
        DataSet dssf = dc.getSfName_HQ(sf_code);
        if (dssf.Tables[0].Rows.Count > 0)
        {
            lblText.Text = lblText.Text + "<span style='color:Red; font-weight: bold; back-color=Yellow'>" + Convert.ToString(dssf.Tables[0].Rows[0].ItemArray.GetValue(0).ToString()) + " - " + Convert.ToString(dssf.Tables[0].Rows[0].ItemArray.GetValue(2).ToString()) + " - " + Convert.ToString(dssf.Tables[0].Rows[0].ItemArray.GetValue(1).ToString()) + "</span>" + ":-";
        }
        dtDCR = Convert.ToDateTime(sCurDate);
        lblHeader.Text = sCurDate + " - " + dtDCR.DayOfWeek.ToString();
       
        lblcurdate = dtDCR.ToString("MM/dd/yyyy");
        ViewState["curdate"] = dtDCR.Day.ToString() + dtDCR.Month.ToString() + dtDCR.Year.ToString();

        CreateHeader();
        CreateListedDr();
        CreateChem();
        CreateStk();
        CreateUnLstdDr();
        CreateHos();

        FillDoc();
        Preview_Chem();
        Preview_Stk();
        FillUnlstDoc();
        Preview_Hos();
        DCR dcr = new DCR();
        DataSet dsdcr = dcr.getRemarks(sf_code, trans_slno);
        if (dsdcr.Tables[0].Rows.Count > 0)
        {
            RevPreview.Text = dsdcr.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
            lblwt.Text = dsdcr.Tables[0].Rows[0].ItemArray.GetValue(1).ToString();
            if (RevPreview.Text != "")
                lblRemarks.Visible = true;
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
    private void CreateHeader()
    {

        DataSet dsHdr = new DataSet();
        DCR_New dc = new DCR_New();
        //Creating Header
        sFile = sf_code + "_" + ViewState["curdate"].ToString() + "_Header.xml";
        string FilePath = Server.MapPath(sFile);

        if (!File.Exists(FilePath))
        {
            //Start writer
            XmlTextWriter dr_writer = new XmlTextWriter(Server.MapPath(sFile), System.Text.Encoding.UTF8);

            //Start XM DOcument
            dr_writer.WriteStartDocument(true);
            dr_writer.Formatting = Formatting.Indented;
            dr_writer.Indentation = 2;

            //ROOT Element
            dr_writer.WriteStartElement("DCR");
            dr_writer.WriteEndElement();
            //End XML Document
            dr_writer.WriteEndDocument();
            //Close writer
            dr_writer.Close();
        }

        XmlDocument xmldoc = new XmlDocument();
        xmldoc.Load(Server.MapPath(sFile));
        //Verify the XML for child records (i.e., Empty (or) Not) by Sridevi on 09/15/15
        dsHdr.ReadXml(Server.MapPath(sFile));
        if (!(dsHdr != null && dsHdr.HasChanges()))
        {
            XmlElement parentelement = xmldoc.CreateElement("DCR");

            XmlElement xmlworktype = xmldoc.CreateElement("worktype");
            XmlElement xmlsdp = xmldoc.CreateElement("sdp");
            XmlElement xmlrem = xmldoc.CreateElement("remarks");
            XmlElement xmldate = xmldoc.CreateElement("date");


            dsxml = dc.get_Trans_Head(sf_code, lblcurdate);

            if (dsxml.Tables[0].Rows.Count > 0)
            {
                xmlworktype.InnerText = dsxml.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                xmlsdp.InnerText = dsxml.Tables[0].Rows[0].ItemArray.GetValue(1).ToString();
                xmlrem.InnerText = dsxml.Tables[0].Rows[0].ItemArray.GetValue(2).ToString();
                if (dsxml.Tables[0].Rows[0].ItemArray.GetValue(3).ToString() != "")
                    xmldate.InnerText = dsxml.Tables[0].Rows[0].ItemArray.GetValue(3).ToString();
                else
                    xmldate.InnerText = DateTime.Now.ToString();
            }

            parentelement.AppendChild(xmlworktype);
            parentelement.AppendChild(xmlsdp);
            parentelement.AppendChild(xmlrem);
            parentelement.AppendChild(xmldate);

            xmldoc.DocumentElement.AppendChild(parentelement);

            xmldoc.Save(Server.MapPath(sFile));
        }
    }

    private void CreateListedDr()
    {
        DCR_New dc = new DCR_New();
        DataSet dsdoc = new DataSet();
        sFile = sf_code + "_" + ViewState["curdate"].ToString() + "_ListedDR.xml";
        string FilePath = Server.MapPath(sFile);

        if (!File.Exists(FilePath))
        {
            //Start writer
            XmlTextWriter dr_writer = new XmlTextWriter(Server.MapPath(sFile), System.Text.Encoding.UTF8);

            //Start XM DOcument
            dr_writer.WriteStartDocument(true);
            dr_writer.Formatting = Formatting.Indented;
            dr_writer.Indentation = 2;

            //ROOT Element
            dr_writer.WriteStartElement("DCR");
            dr_writer.WriteEndElement();
            //End XML Document
            dr_writer.WriteEndDocument();
            //Close writer
            dr_writer.Close();
        }
        XmlDocument xmldoc = new XmlDocument();
        xmldoc.Load(Server.MapPath(sFile));

        //Verify the XML for child records (i.e., Empty (or) Not) by Sridevi on 09/15/15
        dsdoc.ReadXml(Server.MapPath(sFile));
        if (!(dsdoc != null && dsdoc.HasChanges()))
        {
            dsxml = dc.get_Lst_Trans(sf_code, lblcurdate);

            if (dsxml.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow drFF in dsxml.Tables[0].Rows)
                {
                    //sFile = sf_code + "_" + ViewState["curdate"].ToString() + "_ListedDR.xml";

                    //XmlDocument xmldoc = new XmlDocument();
                    //xmldoc.Load(Server.MapPath(sFile));

                    XmlElement parentelement = xmldoc.CreateElement("DCR");

                    XmlElement xmlsession = xmldoc.CreateElement("session");
                    XmlElement xmltime = xmldoc.CreateElement("time");

                    XmlElement xmlDR = xmldoc.CreateElement("drcode");
                    XmlElement xmlworkwith = xmldoc.CreateElement("workwith");
                    XmlElement xmlsdpname = xmldoc.CreateElement("sdpname");
                    XmlElement xmlprod1 = xmldoc.CreateElement("prod1");
                    XmlElement xmlqty1 = xmldoc.CreateElement("qty1");
                    XmlElement xmlprod_pob1 = xmldoc.CreateElement("prod_pob1");
                    XmlElement xmlprod2 = xmldoc.CreateElement("prod2");
                    XmlElement xmlqty2 = xmldoc.CreateElement("qty2");
                    XmlElement xmlprod_pob2 = xmldoc.CreateElement("prod_pob2");
                    XmlElement xmlprod3 = xmldoc.CreateElement("prod3");
                    XmlElement xmlqty3 = xmldoc.CreateElement("qty3");
                    XmlElement xmlprod_pob3 = xmldoc.CreateElement("prod_pob3");
                    XmlElement xmlAddProd = xmldoc.CreateElement("AddProd");
                    XmlElement xmlAddProdCode = xmldoc.CreateElement("AddProdCode");
                    XmlElement xmlgift = xmldoc.CreateElement("gift");
                    XmlElement xmlgqty = xmldoc.CreateElement("gqty");
                    XmlElement xmlAddGift = xmldoc.CreateElement("AddGift");
                    XmlElement xmlAddGiftCode = xmldoc.CreateElement("AddGiftCode");
                    XmlElement xmldr_code = xmldoc.CreateElement("dr_code");
                    XmlElement xmlsf_code = xmldoc.CreateElement("sf_code");
                    XmlElement xmlsess_code = xmldoc.CreateElement("sess_code");
                    XmlElement xmlminute = xmldoc.CreateElement("minute");
                    XmlElement xmlseconds = xmldoc.CreateElement("seconds");
                    XmlElement xmlprod1_code = xmldoc.CreateElement("prod1_code");
                    XmlElement xmlprod2_code = xmldoc.CreateElement("prod2_code");
                    XmlElement xmlprod3_code = xmldoc.CreateElement("prod3_code");
                    XmlElement xmlgiftcode = xmldoc.CreateElement("gift_code");
                    XmlElement xmlremarks = xmldoc.CreateElement("remarks");

                    xmlsession.InnerText = drFF["Session"].ToString();
                    xmlsess_code.InnerText = drFF["Session_Code"].ToString();
                    xmltime.InnerText = drFF["Time"].ToString();
                    xmlminute.InnerText = drFF["Minutes"].ToString();

                    xmlseconds.InnerText = drFF["Seconds"].ToString();
                    xmlDR.InnerText = drFF["Trans_Detail_Name"].ToString();
                    xmldr_code.InnerText = drFF["Trans_Detail_Info_Code"].ToString();
                    xmlworkwith.InnerText = drFF["Worked_with_Name"].ToString();

                    xmlsf_code.InnerText = drFF["Worked_with_Name"].ToString();
                    xmlsdpname.InnerText = drFF["Sdp_Name"].ToString();
                    //13~2$#13~4$#4$
                    string prod = drFF["Product_Code"].ToString();
                    string[] addprod = prod.Split('#');
                    int index = 0;
                    foreach (string aprod in addprod)
                    {
                        //Levox~1$ # LAPP~2$#
                        if (aprod != "")
                        {
                            index = index + 1;
                            string prodcode = aprod.Substring(0, aprod.IndexOf("~")); //aprod.EndsWith('~');
                            string Qty = aprod.Substring(aprod.IndexOf("~") + 1, (aprod.Length - (prodcode.Length + 2)));
                            // string Qty = aprod.Substring(aprod.IndexOf("~") + 1, aprod.IndexOf("$"));

                            if (index == 1)
                            {
                                xmlprod1.InnerText = prodcode;
                                xmlprod1_code.InnerText = prodcode;
                                xmlqty1.InnerText = Qty;
                                xmlprod_pob1.InnerText = "";
                            }
                            else if (index == 2)
                            {
                                xmlprod2.InnerText = prodcode;
                                xmlprod2_code.InnerText = prodcode;
                                xmlqty2.InnerText = Qty;
                                xmlprod_pob2.InnerText = "";
                            }
                            else if (index == 3)
                            {
                                xmlprod3.InnerText = prodcode;
                                xmlprod3_code.InnerText = prodcode;
                                xmlqty3.InnerText = Qty;
                                xmlprod_pob3.InnerText = "";
                            }
                        }
                    }
                    if (index == 1)
                    {
                        xmlprod2.InnerText = "0";
                        xmlprod2_code.InnerText = "0";
                        xmlqty2.InnerText = "";
                        xmlprod_pob2.InnerText = "";
                    }
                    if (index == 2)
                    {
                        xmlprod3.InnerText = "0";
                        xmlprod3_code.InnerText = "0";
                        xmlqty3.InnerText = "";
                        xmlprod_pob3.InnerText = "";
                    }

                    //13~2$#13~4$#4$
                    string proddet = drFF["Product_Detail"].ToString();
                    string[] addproddet = proddet.Split('#');
                    int indexdet = 0;
                    foreach (string aprod in addproddet)
                    {
                        //Levox~1$ # LAPP~2$#
                        if (aprod != "")
                        {
                            indexdet = indexdet + 1;
                            string proddetail = aprod.Substring(0, aprod.IndexOf("~")); //aprod.EndsWith('~');

                            if (indexdet == 1)
                            {
                                xmlprod1.InnerText = proddetail;

                            }
                            else if (indexdet == 2)
                            {
                                xmlprod2.InnerText = proddetail;

                            }
                            else if (indexdet == 3)
                            {
                                xmlprod3.InnerText = proddetail;

                            }
                        }
                    }
                    if (indexdet == 1)
                    {
                        xmlprod2.InnerText = "";

                    }
                    if (indexdet == 2)
                    {
                        xmlprod3.InnerText = "";

                    }

                    xmlAddProd.InnerText = drFF["Additional_Prod_Dtls"].ToString();
                    xmlAddProdCode.InnerText = drFF["Additional_Prod_Code"].ToString();

                    xmlgift.InnerText = drFF["Gift_Name"].ToString();
                    xmlgqty.InnerText = drFF["Gift_Qty"].ToString();
                    xmlgiftcode.InnerText = drFF["Gift_Code"].ToString();

                    xmlAddGift.InnerText = drFF["Additional_Gift_Dtl"].ToString();
                    xmlAddGiftCode.InnerText = drFF["Additional_Gift_Code"].ToString();
                    xmlremarks.InnerText = drFF["Activity_Remarks"].ToString();

                    parentelement.AppendChild(xmlsession);
                    parentelement.AppendChild(xmltime);
                    parentelement.AppendChild(xmlDR);
                    parentelement.AppendChild(xmlworkwith);
                    parentelement.AppendChild(xmlsdpname);
                    parentelement.AppendChild(xmlprod1);
                    parentelement.AppendChild(xmlqty1);
                    parentelement.AppendChild(xmlprod_pob1);
                    parentelement.AppendChild(xmlprod2);
                    parentelement.AppendChild(xmlqty2);
                    parentelement.AppendChild(xmlprod_pob2);
                    parentelement.AppendChild(xmlprod3);
                    parentelement.AppendChild(xmlqty3);
                    parentelement.AppendChild(xmlprod_pob3);
                    parentelement.AppendChild(xmlAddProd);
                    parentelement.AppendChild(xmlAddProdCode);
                    parentelement.AppendChild(xmlgift);
                    parentelement.AppendChild(xmlgqty);
                    parentelement.AppendChild(xmlAddGift);
                    parentelement.AppendChild(xmlAddGiftCode);
                    parentelement.AppendChild(xmldr_code);
                    parentelement.AppendChild(xmlsf_code);
                    parentelement.AppendChild(xmlsess_code);
                    parentelement.AppendChild(xmlminute);
                    parentelement.AppendChild(xmlseconds);
                    parentelement.AppendChild(xmlprod1_code);
                    parentelement.AppendChild(xmlprod2_code);
                    parentelement.AppendChild(xmlprod3_code);
                    parentelement.AppendChild(xmlgiftcode);
                    parentelement.AppendChild(xmlremarks);

                    xmldoc.DocumentElement.AppendChild(parentelement);
                    //xmldoc.Save(Server.MapPath("DailCalls.xml"));
                    xmldoc.Save(Server.MapPath(sFile));
                }

            }
        }
    }

    private void CreateChem()
    {
        DataSet dschem = new DataSet();
        DCR_New dc = new DCR_New();
        sFile = sf_code + "_" + ViewState["curdate"].ToString() + "_Chem.xml";
        string FilePath = Server.MapPath(sFile);

        if (!File.Exists(FilePath))
        {
            //Start writer
            XmlTextWriter dr_writer = new XmlTextWriter(Server.MapPath(sFile), System.Text.Encoding.UTF8);

            //Start XM DOcument
            dr_writer.WriteStartDocument(true);
            dr_writer.Formatting = Formatting.Indented;
            dr_writer.Indentation = 2;

            //ROOT Element
            dr_writer.WriteStartElement("DCR");
            dr_writer.WriteEndElement();
            //End XML Document
            dr_writer.WriteEndDocument();
            //Close writer
            dr_writer.Close();
        }
        XmlDocument xmldoc = new XmlDocument();
        xmldoc.Load(Server.MapPath(sFile));

        //Verify the XML for child records (i.e., Empty (or) Not) by Sridevi on 09/15/15
        dschem.ReadXml(Server.MapPath(sFile));
        if (!(dschem != null && dschem.HasChanges()))
        {
            dsxml = dc.get_Che_Trans(sf_code, lblcurdate);

            if (dsxml.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow drFF in dsxml.Tables[0].Rows)
                {

                    XmlElement parentelement = xmldoc.CreateElement("DCR");

                    XmlElement xmlsf_code = xmldoc.CreateElement("sf_code");
                    XmlElement xmlchemists = xmldoc.CreateElement("chemists");
                    XmlElement xmlchemww = xmldoc.CreateElement("chemww");
                    XmlElement xmlpobno = xmldoc.CreateElement("POBNo");

                    XmlElement xmlchem_code = xmldoc.CreateElement("chem_code");
                    XmlElement xmlterr_code = xmldoc.CreateElement("terr_code");
                    XmlElement xmlnew = xmldoc.CreateElement("new");

                    xmlsf_code.InnerText = sf_code;
                    xmlchemists.InnerText = drFF["Trans_Detail_Name"].ToString();
                    xmlchem_code.InnerText = drFF["Trans_Detail_Info_Code"].ToString();
                    xmlchemww.InnerText = drFF["Worked_with_Name"].ToString();
                    xmlpobno.InnerText = drFF["POB"].ToString();
                    xmlterr_code.InnerText = drFF["SDP"].ToString();
                    xmlnew.InnerText = "";

                    parentelement.AppendChild(xmlsf_code);
                    parentelement.AppendChild(xmlchemists);
                    parentelement.AppendChild(xmlchemww);
                    parentelement.AppendChild(xmlpobno);

                    parentelement.AppendChild(xmlchem_code);
                    parentelement.AppendChild(xmlterr_code);
                    parentelement.AppendChild(xmlnew);

                    xmldoc.DocumentElement.AppendChild(parentelement);

                    xmldoc.Save(Server.MapPath(sFile));
                }

            }
        }
    }

    private void CreateStk()
    {
        DataSet dsStock = new DataSet();
        DCR_New dc = new DCR_New();
        sFile = sf_code + "_" + ViewState["curdate"].ToString() + "_Stockiest.xml";
        string FilePath = Server.MapPath(sFile);

        if (!File.Exists(FilePath))
        {
            //Start writer
            XmlTextWriter dr_writer = new XmlTextWriter(Server.MapPath(sFile), System.Text.Encoding.UTF8);

            //Start XM DOcument
            dr_writer.WriteStartDocument(true);
            dr_writer.Formatting = Formatting.Indented;
            dr_writer.Indentation = 2;

            //ROOT Element
            dr_writer.WriteStartElement("DCR");
            dr_writer.WriteEndElement();
            //End XML Document
            dr_writer.WriteEndDocument();
            //Close writer
            dr_writer.Close();
        }
        XmlDocument xmldoc = new XmlDocument();
        xmldoc.Load(Server.MapPath(sFile));

        //Verify the XML for child records (i.e., Empty (or) Not) by Sridevi on 09/15/15
        dsStock.ReadXml(Server.MapPath(sFile));
        if (!(dsStock != null && dsStock.HasChanges()))
        {
            dsxml = dc.get_Stk_Trans(sf_code, lblcurdate);

            if (dsxml.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow drFF in dsxml.Tables[0].Rows)
                {
                    XmlElement parentelement = xmldoc.CreateElement("DCR");

                    XmlElement xmlstockiest = xmldoc.CreateElement("stockiest");
                    XmlElement xmlstockiestww = xmldoc.CreateElement("stockww");
                    XmlElement xmlpob = xmldoc.CreateElement("pob");
                    XmlElement xmlvisit = xmldoc.CreateElement("visit");
                    XmlElement xmlstockiest_code = xmldoc.CreateElement("stockiest_code");
                    XmlElement xmlvisit_code = xmldoc.CreateElement("visit_code");


                    xmlstockiest.InnerText = drFF["Trans_Detail_Name"].ToString();
                    xmlstockiest_code.InnerText = drFF["Trans_Detail_Info_Code"].ToString();
                    xmlstockiestww.InnerText = drFF["Worked_with_Name"].ToString();
                    xmlpob.InnerText = drFF["POB"].ToString();
                    xmlvisit.InnerText = drFF["Visit_Type"].ToString();
                    xmlvisit_code.InnerText = drFF["Visit_Type"].ToString();

                    parentelement.AppendChild(xmlstockiest);
                    parentelement.AppendChild(xmlstockiestww);
                    parentelement.AppendChild(xmlpob);
                    parentelement.AppendChild(xmlvisit);
                    parentelement.AppendChild(xmlstockiest_code);
                    parentelement.AppendChild(xmlvisit_code);

                    xmldoc.DocumentElement.AppendChild(parentelement);

                    xmldoc.Save(Server.MapPath(sFile));
                }

            }
        }
    }

    private void CreateHos()
    {
        DataSet dsHosp = new DataSet();
        DCR_New dc = new DCR_New();
        sFile = sf_code + "_" + ViewState["curdate"].ToString() + "_Hospital.xml";
        string FilePath = Server.MapPath(sFile);

        if (!File.Exists(FilePath))
        {
            //Start writer
            XmlTextWriter dr_writer = new XmlTextWriter(Server.MapPath(sFile), System.Text.Encoding.UTF8);

            //Start XM DOcument
            dr_writer.WriteStartDocument(true);
            dr_writer.Formatting = Formatting.Indented;
            dr_writer.Indentation = 2;

            //ROOT Element
            dr_writer.WriteStartElement("DCR");
            dr_writer.WriteEndElement();
            //End XML Document
            dr_writer.WriteEndDocument();
            //Close writer
            dr_writer.Close();
        }
        XmlDocument xmldoc = new XmlDocument();
        xmldoc.Load(Server.MapPath(sFile));

        //Verify the XML for child records (i.e., Empty (or) Not) by Sridevi on 09/15/15
        dsHosp.ReadXml(Server.MapPath(sFile));
        if (!(dsHosp != null && dsHosp.HasChanges()))
        {
            dsxml = dc.get_Hos_Trans(sf_code, lblcurdate);

            if (dsxml.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow drFF in dsxml.Tables[0].Rows)
                {

                    XmlElement parentelement = xmldoc.CreateElement("DCR");

                    XmlElement xmlhospital = xmldoc.CreateElement("hospital");
                    XmlElement xmlhospitalww = xmldoc.CreateElement("hosww");
                    XmlElement xmlpob = xmldoc.CreateElement("pob");
                    XmlElement xmlhospital_code = xmldoc.CreateElement("hospital_code");


                    xmlhospital.InnerText = drFF["Trans_Detail_Name"].ToString();
                    xmlhospital_code.InnerText = drFF["Trans_Detail_Info_Code"].ToString();
                    xmlhospitalww.InnerText = drFF["Worked_with_Name"].ToString();
                    xmlpob.InnerText = drFF["POB"].ToString();

                    parentelement.AppendChild(xmlhospital);
                    parentelement.AppendChild(xmlhospitalww);
                    parentelement.AppendChild(xmlpob);
                    parentelement.AppendChild(xmlhospital_code);

                    xmldoc.DocumentElement.AppendChild(parentelement);
                    xmldoc.Save(Server.MapPath(sFile));

                }

            }
        }
    }

    private void CreateUnLstdDr()
    {
        DataSet dsUnDoc = new DataSet();
        DCR_New dc = new DCR_New();
        sFile = sf_code + "_" + ViewState["curdate"].ToString() + "UnLstDR.xml";
        string FilePath = Server.MapPath(sFile);

        if (!File.Exists(FilePath))
        {
            //Start writer
            XmlTextWriter dr_writer = new XmlTextWriter(Server.MapPath(sFile), System.Text.Encoding.UTF8);

            //Start XM DOcument
            dr_writer.WriteStartDocument(true);
            dr_writer.Formatting = Formatting.Indented;
            dr_writer.Indentation = 2;

            //ROOT Element
            dr_writer.WriteStartElement("DCR");
            dr_writer.WriteEndElement();
            //End XML Document
            dr_writer.WriteEndDocument();
            //Close writer
            dr_writer.Close();
        }
        XmlDocument xmldoc = new XmlDocument();
        xmldoc.Load(Server.MapPath(sFile));

        //Verify the XML for child records (i.e., Empty (or) Not) by Sridevi on 09/15/15
        dsUnDoc.ReadXml(Server.MapPath(sFile));
        if (!(dsUnDoc != null && dsUnDoc.HasChanges()))
        {

            dsxml = dc.get_UnLst_Trans(sf_code, lblcurdate);

            if (dsxml.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow drFF in dsxml.Tables[0].Rows)
                {

                    XmlElement parentelement = xmldoc.CreateElement("DCR");

                    XmlElement xmlsession = xmldoc.CreateElement("session");
                    XmlElement xmltime = xmldoc.CreateElement("time");
                    XmlElement xmlmin = xmldoc.CreateElement("min");
                    XmlElement xmlsec = xmldoc.CreateElement("sec");
                    XmlElement xmlDR = xmldoc.CreateElement("drcode");
                    XmlElement xmlworkwith = xmldoc.CreateElement("workwith");
                    XmlElement xmlprod1 = xmldoc.CreateElement("prod1");
                    XmlElement xmlqty1 = xmldoc.CreateElement("qty1");
                    XmlElement xmlprod_pob1 = xmldoc.CreateElement("prod_pob1");
                    XmlElement xmlprod2 = xmldoc.CreateElement("prod2");
                    XmlElement xmlqty2 = xmldoc.CreateElement("qty2");
                    XmlElement xmlprod_pob2 = xmldoc.CreateElement("prod_pob2");
                    XmlElement xmlprod3 = xmldoc.CreateElement("prod3");
                    XmlElement xmlqty3 = xmldoc.CreateElement("qty3");
                    XmlElement xmlprod_pob3 = xmldoc.CreateElement("prod_pob3");
                    XmlElement xmlgift = xmldoc.CreateElement("gift");
                    XmlElement xmlgqty = xmldoc.CreateElement("gqty");
                    XmlElement xmldr_code = xmldoc.CreateElement("dr_code");
                    XmlElement xmlsf_code = xmldoc.CreateElement("sf_code");

                    XmlElement xmlsess_code = xmldoc.CreateElement("sess_code");
                    XmlElement xmltime_code = xmldoc.CreateElement("time_code");

                    XmlElement xmlprod1_code = xmldoc.CreateElement("prod1_code");
                    XmlElement xmlprod2_code = xmldoc.CreateElement("prod2_code");
                    XmlElement xmlprod3_code = xmldoc.CreateElement("prod3_code");
                    XmlElement xmlgiftcode = xmldoc.CreateElement("gift_code");
                    XmlElement xmlAddProdCode = xmldoc.CreateElement("AddProdCode");
                    XmlElement xmlAddProd = xmldoc.CreateElement("AddProd");
                    XmlElement xmlAddGiftCode = xmldoc.CreateElement("AddGiftCode");
                    XmlElement xmlAddGift = xmldoc.CreateElement("AddGift");

                    XmlElement xmlterr = xmldoc.CreateElement("terr");
                    XmlElement xmlspec = xmldoc.CreateElement("spec");
                    XmlElement xmlcat = xmldoc.CreateElement("cat");
                    XmlElement xmlclass = xmldoc.CreateElement("class");
                    XmlElement xmlqual = xmldoc.CreateElement("qual");
                    XmlElement xmladd = xmldoc.CreateElement("add");
                    XmlElement xmlnew = xmldoc.CreateElement("new");
                    XmlElement xmlsfcode = xmldoc.CreateElement("sfcode");

                    xmlsession.InnerText = drFF["Session"].ToString();
                    xmlsess_code.InnerText = drFF["Session_Code"].ToString();
                    xmltime.InnerText = drFF["Time"].ToString();
                    xmlmin.InnerText = drFF["Minutes"].ToString();

                    xmlsec.InnerText = drFF["Seconds"].ToString();
                    xmlDR.InnerText = drFF["Trans_Detail_Name"].ToString();
                    xmldr_code.InnerText = drFF["Trans_Detail_Info_Code"].ToString();
                    xmlworkwith.InnerText = drFF["Worked_with_Name"].ToString();

                    xmlsf_code.InnerText = drFF["Worked_with_Name"].ToString();
                    //13~2$#13~4$#4$
                    string prod = drFF["Product_Code"].ToString();
                    string[] addprod = prod.Split('#');
                    int index = 0;
                    foreach (string aprod in addprod)
                    {
                        //Levox~1$ # LAPP~2$#
                        if (aprod != "")
                        {
                            index = index + 1;
                            string prodcode = aprod.Substring(0, aprod.IndexOf("~")); //aprod.EndsWith('~');
                            string Qty = aprod.Substring(aprod.IndexOf("~") + 1, (aprod.Length - (prodcode.Length + 2)));
                            // string Qty = aprod.Substring(aprod.IndexOf("~") + 1, aprod.IndexOf("$"));

                            if (index == 1)
                            {
                                xmlprod1.InnerText = prodcode;
                                xmlprod1_code.InnerText = prodcode;
                                xmlqty1.InnerText = Qty;
                                xmlprod_pob1.InnerText = "";
                            }
                            else if (index == 2)
                            {
                                xmlprod2.InnerText = prodcode;
                                xmlprod2_code.InnerText = prodcode;
                                xmlqty2.InnerText = Qty;
                                xmlprod_pob2.InnerText = "";
                            }
                            else if (index == 3)
                            {
                                xmlprod3.InnerText = prodcode;
                                xmlprod3_code.InnerText = prodcode;
                                xmlqty3.InnerText = Qty;
                                xmlprod_pob3.InnerText = "";
                            }
                        }
                    }
                    if (index == 1)
                    {
                        xmlprod2.InnerText = "0";
                        xmlprod2_code.InnerText = "0";
                        xmlqty2.InnerText = "";
                        xmlprod_pob2.InnerText = "";
                    }
                    if (index == 2)
                    {
                        xmlprod3.InnerText = "0";
                        xmlprod3_code.InnerText = "0";
                        xmlqty3.InnerText = "";
                        xmlprod_pob3.InnerText = "";
                    }

                    //13~2$#13~4$#4$
                    string proddet = drFF["Product_Detail"].ToString();
                    string[] addproddet = proddet.Split('#');
                    int indexdet = 0;
                    foreach (string aprod in addproddet)
                    {
                        //Levox~1$ # LAPP~2$#
                        if (aprod != "")
                        {
                            indexdet = indexdet + 1;
                            string proddetail = aprod.Substring(0, aprod.IndexOf("~")); //aprod.EndsWith('~');

                            if (indexdet == 1)
                            {
                                xmlprod1.InnerText = proddetail;

                            }
                            else if (indexdet == 2)
                            {
                                xmlprod2.InnerText = proddetail;

                            }
                            else if (indexdet == 3)
                            {
                                xmlprod3.InnerText = proddetail;

                            }
                        }
                    }
                    if (indexdet == 1)
                    {
                        xmlprod2.InnerText = "";

                    }
                    if (indexdet == 2)
                    {
                        xmlprod3.InnerText = "";
                    }

                    xmlAddProd.InnerText = drFF["Additional_Prod_Dtls"].ToString();
                    xmlAddProdCode.InnerText = drFF["Additional_Prod_Code"].ToString();
                    xmlgift.InnerText = drFF["Gift_Name"].ToString();

                    xmlgqty.InnerText = drFF["Gift_Qty"].ToString();
                    xmlgiftcode.InnerText = drFF["Gift_Code"].ToString();

                    xmlAddGift.InnerText = drFF["Additional_Gift_Dtl"].ToString();
                    xmlAddGiftCode.InnerText = drFF["Additional_Gift_Code"].ToString();

                    xmladd.InnerText = "";
                    xmlterr.InnerText = drFF["SDP"].ToString();
                    xmlspec.InnerText = "0";
                    xmlcat.InnerText = "0";
                    xmlclass.InnerText = "0";
                    xmlqual.InnerText = "0";
                    xmlnew.InnerText = "";
                    xmlsfcode.InnerText = sf_code;


                    parentelement.AppendChild(xmlsession);
                    parentelement.AppendChild(xmltime);
                    parentelement.AppendChild(xmlDR);
                    parentelement.AppendChild(xmlworkwith);
                    parentelement.AppendChild(xmlprod1);
                    parentelement.AppendChild(xmlqty1);
                    parentelement.AppendChild(xmlprod_pob1);
                    parentelement.AppendChild(xmlprod2);
                    parentelement.AppendChild(xmlqty2);
                    parentelement.AppendChild(xmlprod_pob2);
                    parentelement.AppendChild(xmlprod3);
                    parentelement.AppendChild(xmlqty3);
                    parentelement.AppendChild(xmlprod_pob3);
                    parentelement.AppendChild(xmlAddProd);
                    parentelement.AppendChild(xmlAddProdCode);
                    parentelement.AppendChild(xmlgift);
                    parentelement.AppendChild(xmlgqty);
                    parentelement.AppendChild(xmlAddGift);
                    parentelement.AppendChild(xmlAddGiftCode);
                    parentelement.AppendChild(xmldr_code);
                    parentelement.AppendChild(xmlsf_code);
                    parentelement.AppendChild(xmlsess_code);
                    parentelement.AppendChild(xmltime_code);
                    parentelement.AppendChild(xmlmin);
                    parentelement.AppendChild(xmlsec);
                    parentelement.AppendChild(xmlprod1_code);
                    parentelement.AppendChild(xmlprod2_code);
                    parentelement.AppendChild(xmlprod3_code);
                    parentelement.AppendChild(xmlgiftcode);

                    parentelement.AppendChild(xmlterr);
                    parentelement.AppendChild(xmlspec);
                    parentelement.AppendChild(xmlcat);
                    parentelement.AppendChild(xmlclass);
                    parentelement.AppendChild(xmlqual);
                    parentelement.AppendChild(xmladd);
                    parentelement.AppendChild(xmlnew);
                    parentelement.AppendChild(xmlsfcode);

                    xmldoc.DocumentElement.AppendChild(parentelement);
                    //xmldoc.Save(Server.MapPath("DailCalls.xml"));
                    xmldoc.Save(Server.MapPath(sFile));
                }

            }
        }
    }
    private void FillDoc()
    {

        DataSet ds = new DataSet();
        //DataTable dt = new DataTable();

        sFile = sf_code + "_" + ViewState["curdate"].ToString() + "_ListedDR.xml";

        string FilePath = Server.MapPath(sFile);
        if (!File.Exists(FilePath))
        {
            //Start writer
            XmlTextWriter dr_writer = new XmlTextWriter(Server.MapPath(sFile), System.Text.Encoding.UTF8);

            //Start XM DOcument
            dr_writer.WriteStartDocument(true);
            dr_writer.Formatting = Formatting.Indented;
            dr_writer.Indentation = 2;

            //ROOT Element
            dr_writer.WriteStartElement("DCR");
            dr_writer.WriteEndElement();
            //End XML Document
            dr_writer.WriteEndDocument();
            //Close writer
            dr_writer.Close();
        }

        ds.ReadXml(Server.MapPath(sFile));
        if (ds != null && ds.HasChanges())
        {
            if (ds.Tables[0].Rows.Count > 0)
            {

                TableRow tr_header = new TableRow();
                tr_header.BorderStyle = BorderStyle.Solid;
                tr_header.BorderWidth = 1;

                TableCell tc_Ses = new TableCell();
                tc_Ses.BorderStyle = BorderStyle.Solid;
                tc_Ses.BorderWidth = 1;
                tc_Ses.Width = 30;
                Literal lit_Ses = new Literal();
                lit_Ses.Text = "Ses";
                tc_Ses.Controls.Add(lit_Ses);
                tr_header.Cells.Add(tc_Ses);
                //tr_header.BackColor = System.Drawing.Color.Pink;        
                tr_header.BackColor = System.Drawing.Color.FromName("#FDD7E4");
                TableCell tc_Time = new TableCell();
                tc_Time.BorderStyle = BorderStyle.Solid;
                tc_Time.BorderWidth = 1;
                tc_Time.Width = 40;
                Literal lit_Time = new Literal();
                lit_Time.Text = "<center>Time</center>";
                tc_Time.Controls.Add(lit_Time);
                tr_header.Cells.Add(tc_Time);

                TableCell tc_DR_Name = new TableCell();
                tc_DR_Name.BorderStyle = BorderStyle.Solid;
                tc_DR_Name.BorderWidth = 1;
                tc_DR_Name.Width = 170;
                Literal lit_DR_Name = new Literal();
                lit_DR_Name.Text = "<center>Listed Doctor Name</center>";
                tc_DR_Name.Controls.Add(lit_DR_Name);
                tr_header.Cells.Add(tc_DR_Name);

                TableCell tc_terr_Name = new TableCell();
                tc_terr_Name.BorderStyle = BorderStyle.Solid;
                tc_terr_Name.BorderWidth = 1;
                tc_terr_Name.Width = 250;
                Literal lit_terr_Name = new Literal();
                lit_terr_Name.Text = "<center>Territory Name</center>";
                tc_terr_Name.Controls.Add(lit_terr_Name);
                tr_header.Cells.Add(tc_terr_Name);

                TableCell tc_ww = new TableCell();
                tc_ww.BorderStyle = BorderStyle.Solid;
                tc_ww.BorderWidth = 1;
                tc_ww.Width = 300;
                Literal lit_ww = new Literal();
                lit_ww.Text = "<center>Worked With</center>";
                tc_ww.Controls.Add(lit_ww);
                tr_header.Cells.Add(tc_ww);

                TableCell tc_prod = new TableCell();
                tc_prod.BorderStyle = BorderStyle.Solid;
                tc_prod.BorderWidth = 1;
                tc_prod.Width = 460;
                //   tc_prod.ColumnSpan = 6;
                Literal lit_prod = new Literal();
                lit_prod.Text = "<center>Product Promoted / Sampled</center>";
                tc_prod.Controls.Add(lit_prod);
                tr_header.Cells.Add(tc_prod);

                TableCell tc_Gift = new TableCell();
                tc_Gift.BorderStyle = BorderStyle.Solid;
                tc_Gift.BorderWidth = 1;
                tc_Gift.Width = 240;
                Literal lit_Gift = new Literal();
                lit_Gift.Text = "<center>Gift</center>";
                tc_Gift.Controls.Add(lit_Gift);
                tr_header.Cells.Add(tc_Gift);

                TableCell tc_remarks = new TableCell();
                tc_remarks.BorderStyle = BorderStyle.Solid;
                tc_remarks.BorderWidth = 1;
                tc_remarks.Width = 240;
                Literal lit_remarks = new Literal();
                lit_remarks.Text = "<center>Remarks</center>";
                tc_remarks.Controls.Add(lit_remarks);
                tr_header.Cells.Add(tc_remarks);

                tbl.Rows.Add(tr_header);

                //Details Section
                foreach (DataRow drFF in ds.Tables[0].Rows)
                {
                    TableRow tr_det = new TableRow();
                    TableCell tc_det_Ses = new TableCell();
                    Literal lit_det_Ses = new Literal();
                    lit_det_Ses.Text = "&nbsp;" + drFF["session"].ToString();
                    tc_det_Ses.BorderStyle = BorderStyle.Solid;
                    tc_det_Ses.BorderWidth = 1;
                    tc_det_Ses.Controls.Add(lit_det_Ses);
                    tr_det.Cells.Add(tc_det_Ses);

                    TableCell tc_det_time = new TableCell();
                    Literal lit_det_time = new Literal();
                    lit_det_time.Text = "&nbsp;" + drFF["time"].ToString();
                    tc_det_time.BorderStyle = BorderStyle.Solid;
                    tc_det_time.BorderWidth = 1;
                    tc_det_time.Controls.Add(lit_det_time);
                    tr_det.Cells.Add(tc_det_time);

                    TableCell tc_det_doc_name = new TableCell();
                    Literal lit_det_doc_name = new Literal();
                    lit_det_doc_name.Text = "&nbsp;" + drFF["drcode"].ToString();
                    tc_det_doc_name.BorderStyle = BorderStyle.Solid;
                    tc_det_doc_name.BorderWidth = 1;
                    tc_det_doc_name.Controls.Add(lit_det_doc_name);
                    tr_det.Cells.Add(tc_det_doc_name);

                    TableCell tc_det_terr_name = new TableCell();
                    Literal lit_det_terr_name = new Literal();
                    lit_det_terr_name.Text = "&nbsp;" + drFF["sdpname"].ToString();
                    tc_det_terr_name.BorderStyle = BorderStyle.Solid;

                    tc_det_terr_name.BorderWidth = 1;
                    tc_det_terr_name.Controls.Add(lit_det_terr_name);
                    tr_det.Cells.Add(tc_det_terr_name);

                    TableCell tc_det_ww = new TableCell();
                    Literal lit_det_ww = new Literal();
                    lit_det_ww.Text = "&nbsp;" + drFF["sf_code"].ToString();
                    tc_det_ww.BorderStyle = BorderStyle.Solid;
                    tc_det_ww.BorderWidth = 1;
                    tc_det_ww.Controls.Add(lit_det_ww);
                    tr_det.Cells.Add(tc_det_ww);

                    TableCell tc_det_prod1 = new TableCell();
                    Literal lit_det_prod1 = new Literal();
                    drFF["AddProd"] = drFF["AddProd"].ToString().Replace("~", " ( ").Trim();
                    drFF["AddProd"] = drFF["AddProd"].ToString().Replace("$", " ) ").Trim();
                    drFF["AddProd"] = drFF["AddProd"].ToString().Replace("#", " / ").Trim();
                    if (drFF["prod1"].ToString().Length > 0)
                    {
                        if (drFF["qty1"].ToString().Length > 0)
                        {
                            drFF["prod1"] = drFF["prod1"].ToString().Replace("~", "").Trim() + " ( " + drFF["qty1"].ToString() + " ) ";
                        }
                        else
                        {
                            drFF["prod1"] = drFF["prod1"].ToString().Replace("~", "").Trim();
                        }

                    }
                    if (drFF["prod2"].ToString().Length > 0)
                    {
                        if (drFF["qty2"].ToString().Length > 0)
                        {
                            drFF["prod2"] = "/" + drFF["prod2"].ToString().Replace("~", "").Trim() + " ( " + drFF["qty2"].ToString() + " ) ";
                        }
                        else
                        {
                            drFF["prod2"] = "/" + drFF["prod2"].ToString().Replace("~", "").Trim();
                        }

                    }
                    if (drFF["prod3"].ToString().Length > 0)
                    {
                        if (drFF["qty3"].ToString().Length > 0)
                        {
                            drFF["prod3"] = "/" + drFF["prod3"].ToString().Replace("~", "").Trim() + " ( " + drFF["qty3"].ToString() + " ) ";
                        }
                        else
                        {
                            drFF["prod3"] = "/" + drFF["prod3"].ToString().Replace("~", "").Trim();
                        }
                    }
                    lit_det_prod1.Text = "&nbsp;" + drFF["prod1"].ToString() + "&nbsp;" + drFF["prod2"].ToString() + "&nbsp;" + drFF["prod3"].ToString() + "&nbsp;" + drFF["AddProd"].ToString();
                    tc_det_prod1.BorderStyle = BorderStyle.Solid;
                    tc_det_prod1.BorderWidth = 1;
                    tc_det_prod1.Width = 120;
                    tc_det_prod1.Controls.Add(lit_det_prod1);
                    tc_det_prod1.HorizontalAlign = HorizontalAlign.Left;
                    tr_det.Cells.Add(tc_det_prod1);


                    TableCell tc_det_gift = new TableCell();
                    Literal lit_det_gift = new Literal();
                    drFF["AddGift"] = drFF["AddGift"].ToString().Replace("~", " ( ").Trim();
                    drFF["AddGift"] = drFF["AddGift"].ToString().Replace("$", " ) ").Trim();
                    drFF["AddGift"] = drFF["AddGift"].ToString().Replace("#", " / ").Trim();
                    if (drFF["gqty"].ToString().Length > 0)
                    {
                        drFF["gift"] = drFF["gift"].ToString().Replace("~", "").Trim() + " ( " + drFF["gqty"].ToString() + " ) ";
                    }
                    else
                    {
                        drFF["gift"] = drFF["gift"].ToString().Replace("~", "").Trim();
                    }
                    if (drFF["gift"].ToString() != "")
                    {
                        lit_det_gift.Text = "&nbsp;" + drFF["gift"].ToString() + " / " + "&nbsp;" + drFF["AddGift"].ToString();
                    }
                    tc_det_gift.BorderStyle = BorderStyle.Solid;
                    tc_det_gift.BorderWidth = 1;
                    tc_det_gift.Controls.Add(lit_det_gift);
                    tc_det_gift.HorizontalAlign = HorizontalAlign.Left;
                    tr_det.Cells.Add(tc_det_gift);

                    TableCell tc_det_rm = new TableCell();
                    Literal lit_det_rm = new Literal();
                    lit_det_rm.Text = "&nbsp;" + drFF["remarks"].ToString();
                    tc_det_rm.BorderStyle = BorderStyle.Solid;
                    tc_det_rm.BorderWidth = 1;
                    tc_det_rm.Controls.Add(lit_det_rm);
                    tr_det.Cells.Add(tc_det_rm);


                    tbl.Rows.Add(tr_det);
                }
            }
            else
            {
                lblld.Visible = false;
            }

        }
        else
        {
            lblld.Visible = false;
        }

    }



    private void Preview_Chem()
    {
        DataSet ds = new DataSet();

        sFile = sf_code + "_" + ViewState["curdate"].ToString() + "_Chem.xml";

        string FilePath = Server.MapPath(sFile);
        if (!File.Exists(FilePath))
        {
            //Start writer
            XmlTextWriter dr_writer = new XmlTextWriter(Server.MapPath(sFile), System.Text.Encoding.UTF8);

            //Start XM DOcument
            dr_writer.WriteStartDocument(true);
            dr_writer.Formatting = Formatting.Indented;
            dr_writer.Indentation = 2;

            //ROOT Element
            dr_writer.WriteStartElement("DCR");
            dr_writer.WriteEndElement();
            //End XML Document
            dr_writer.WriteEndDocument();
            //Close writer
            dr_writer.Close();
        }

        ds.ReadXml(Server.MapPath(sFile));

        if (ds != null && ds.HasChanges())
        {
            if (ds.Tables[0].Rows.Count > 0)
            {

                TableRow tr_header = new TableRow();
                tr_header.BorderStyle = BorderStyle.Solid;
                tr_header.BorderWidth = 1;

                TableCell tc_Ses = new TableCell();
                tc_Ses.BorderStyle = BorderStyle.Solid;
                tc_Ses.BorderWidth = 1;
                tc_Ses.Width = 300;
                Literal lit_Ses = new Literal();
                lit_Ses.Text = "Chemists Name";
                tc_Ses.Controls.Add(lit_Ses);
                tr_header.Cells.Add(tc_Ses);
                // tr_header.BackColor = System.Drawing.Color.Pink;
                tr_header.BackColor = System.Drawing.Color.FromName("#FDD7E4");
                TableCell tc_ww = new TableCell();
                tc_ww.BorderStyle = BorderStyle.Solid;
                tc_ww.BorderWidth = 1;
                tc_ww.Width = 300;
                Literal lit_ww = new Literal();
                lit_ww.Text = "<center>Worked With</center>";
                tc_ww.Controls.Add(lit_ww);
                tr_header.Cells.Add(tc_ww);

                TableCell tc_prod = new TableCell();
                tc_prod.BorderStyle = BorderStyle.Solid;
                tc_prod.BorderWidth = 1;
                tc_prod.Width = 420;
                Literal lit_prod = new Literal();
                lit_prod.Text = "<center>POB</center>";
                tc_prod.Controls.Add(lit_prod);
                tr_header.Cells.Add(tc_prod);

                tblChem.Rows.Add(tr_header);

                //Details Section
                foreach (DataRow drFF in ds.Tables[0].Rows)
                {
                    TableRow tr_det = new TableRow();
                    TableCell tc_det_Ses = new TableCell();
                    Literal lit_det_Ses = new Literal();
                    lit_det_Ses.Text = "&nbsp;" + drFF["chemists"].ToString();
                    tc_det_Ses.BorderStyle = BorderStyle.Solid;
                    tc_det_Ses.BorderWidth = 1;
                    tc_det_Ses.Controls.Add(lit_det_Ses);
                    tr_det.Cells.Add(tc_det_Ses);

                    TableCell tc_det_doc_name = new TableCell();
                    Literal lit_det_doc_name = new Literal();
                    lit_det_doc_name.Text = "&nbsp;" + drFF["chemww"].ToString();
                    tc_det_doc_name.BorderStyle = BorderStyle.Solid;
                    tc_det_doc_name.BorderWidth = 1;
                    tc_det_doc_name.Controls.Add(lit_det_doc_name);
                    tr_det.Cells.Add(tc_det_doc_name);

                    TableCell tc_det_ww = new TableCell();
                    Literal lit_det_ww = new Literal();
                    lit_det_ww.Text = "&nbsp;" + drFF["POBNo"].ToString();
                    tc_det_ww.BorderStyle = BorderStyle.Solid;
                    tc_det_ww.BorderWidth = 1;
                    tc_det_ww.Controls.Add(lit_det_ww);
                    tr_det.Cells.Add(tc_det_ww);

                    tblChem.Rows.Add(tr_det);
                }
            }
            else
            {
                lblch.Visible = false;
            }

        }
        else
        {
            lblch.Visible = false;
        }

    }

    private void Preview_Stk()
    {
        DataSet ds = new DataSet();

        sFile = sf_code + "_" + ViewState["curdate"].ToString() + "_Stockiest.xml";

        string FilePath = Server.MapPath(sFile);
        if (!File.Exists(FilePath))
        {
            //Start writer
            XmlTextWriter dr_writer = new XmlTextWriter(Server.MapPath(sFile), System.Text.Encoding.UTF8);

            //Start XM DOcument
            dr_writer.WriteStartDocument(true);
            dr_writer.Formatting = Formatting.Indented;
            dr_writer.Indentation = 2;

            //ROOT Element
            dr_writer.WriteStartElement("DCR");
            dr_writer.WriteEndElement();
            //End XML Document
            dr_writer.WriteEndDocument();
            //Close writer
            dr_writer.Close();
        }

        ds.ReadXml(Server.MapPath(sFile));

        if (ds != null && ds.HasChanges())
        {
            if (ds.Tables[0].Rows.Count > 0)
            {

                TableRow tr_header = new TableRow();
                tr_header.BorderStyle = BorderStyle.Solid;
                tr_header.BorderWidth = 1;

                TableCell tc_Ses = new TableCell();
                tc_Ses.BorderStyle = BorderStyle.Solid;
                tc_Ses.BorderWidth = 1;
                tc_Ses.Width = 300;
                Literal lit_Ses = new Literal();
                lit_Ses.Text = "Stockiest Name";
                tc_Ses.Controls.Add(lit_Ses);
                tr_header.Cells.Add(tc_Ses);
                //tr_header.BackColor = System.Drawing.Color.Pink;
                tr_header.BackColor = System.Drawing.Color.FromName("#FDD7E4");
                TableCell tc_ww = new TableCell();
                tc_ww.BorderStyle = BorderStyle.Solid;
                tc_ww.BorderWidth = 1;
                tc_ww.Width = 300;
                Literal lit_ww = new Literal();
                lit_ww.Text = "<center>Worked With</center>";
                tc_ww.Controls.Add(lit_ww);
                tr_header.Cells.Add(tc_ww);

                TableCell tc_prod = new TableCell();
                tc_prod.BorderStyle = BorderStyle.Solid;
                tc_prod.BorderWidth = 1;
                tc_prod.Width = 420;
                Literal lit_prod = new Literal();
                lit_prod.Text = "<center>POB</center>";
                tc_prod.Controls.Add(lit_prod);
                tr_header.Cells.Add(tc_prod);

                TableCell tc_visit = new TableCell();
                tc_visit.BorderStyle = BorderStyle.Solid;
                tc_visit.BorderWidth = 1;
                tc_visit.Width = 420;
                Literal lit_visit = new Literal();
                lit_visit.Text = "<center>Visit</center>";
                tc_visit.Controls.Add(lit_visit);
                tr_header.Cells.Add(tc_visit);

                tblstk.Rows.Add(tr_header);

                //Details Section
                foreach (DataRow drFF in ds.Tables[0].Rows)
                {
                    TableRow tr_det = new TableRow();
                    TableCell tc_det_Ses = new TableCell();
                    Literal lit_det_Ses = new Literal();
                    lit_det_Ses.Text = "&nbsp;" + drFF["stockiest"].ToString();
                    tc_det_Ses.BorderStyle = BorderStyle.Solid;
                    tc_det_Ses.BorderWidth = 1;
                    tc_det_Ses.Controls.Add(lit_det_Ses);
                    tr_det.Cells.Add(tc_det_Ses);

                    TableCell tc_det_doc_name = new TableCell();
                    Literal lit_det_doc_name = new Literal();
                    lit_det_doc_name.Text = "&nbsp;" + drFF["stockww"].ToString();
                    tc_det_doc_name.BorderStyle = BorderStyle.Solid;
                    tc_det_doc_name.BorderWidth = 1;
                    tc_det_doc_name.Controls.Add(lit_det_doc_name);
                    tr_det.Cells.Add(tc_det_doc_name);

                    TableCell tc_det_ww = new TableCell();
                    Literal lit_det_ww = new Literal();
                    lit_det_ww.Text = "&nbsp;" + drFF["pob"].ToString();
                    tc_det_ww.BorderStyle = BorderStyle.Solid;
                    tc_det_ww.BorderWidth = 1;
                    tc_det_ww.Controls.Add(lit_det_ww);
                    tr_det.Cells.Add(tc_det_ww);

                    TableCell tc_det_vst = new TableCell();
                    Literal lit_det_vst = new Literal();
                    lit_det_vst.Text = "&nbsp;" + drFF["visit"].ToString();
                    tc_det_vst.BorderStyle = BorderStyle.Solid;
                    tc_det_vst.BorderWidth = 1;
                    tc_det_vst.Controls.Add(lit_det_vst);
                    tr_det.Cells.Add(tc_det_vst);

                    tblstk.Rows.Add(tr_det);
                }
            }
            else
            {
                lblst.Visible = false;
            }
        }
        else
        {
            lblst.Visible = false;
        }

    }

    private void FillUnlstDoc()
    {

        DataSet ds = new DataSet();
        //DataTable dt = new DataTable();

        sFile = sf_code + "_" + ViewState["curdate"].ToString() + "UnLstDR.xml";

        string FilePath = Server.MapPath(sFile);
        if (!File.Exists(FilePath))
        {
            //Start writer
            XmlTextWriter dr_writer = new XmlTextWriter(Server.MapPath(sFile), System.Text.Encoding.UTF8);

            //Start XM DOcument
            dr_writer.WriteStartDocument(true);
            dr_writer.Formatting = Formatting.Indented;
            dr_writer.Indentation = 2;

            //ROOT Element
            dr_writer.WriteStartElement("DCR");
            dr_writer.WriteEndElement();
            //End XML Document
            dr_writer.WriteEndDocument();
            //Close writer
            dr_writer.Close();
        }

        ds.ReadXml(Server.MapPath(sFile));
        if (ds != null && ds.HasChanges())
        {
            if (ds.Tables[0].Rows.Count > 0)
            {

                TableRow tr_header = new TableRow();
                tr_header.BorderStyle = BorderStyle.Solid;
                tr_header.BorderWidth = 1;

                TableCell tc_Ses = new TableCell();
                tc_Ses.BorderStyle = BorderStyle.Solid;
                tc_Ses.BorderWidth = 1;
                tc_Ses.Width = 30;
                Literal lit_Ses = new Literal();
                lit_Ses.Text = "Ses";
                tc_Ses.Controls.Add(lit_Ses);
                tr_header.Cells.Add(tc_Ses);
                //tr_header.BackColor = System.Drawing.Color.Pink;
                tr_header.BackColor = System.Drawing.Color.FromName("#FDD7E4");

                TableCell tc_Time = new TableCell();
                tc_Time.BorderStyle = BorderStyle.Solid;
                tc_Time.BorderWidth = 1;
                tc_Time.Width = 40;
                Literal lit_Time = new Literal();
                lit_Time.Text = "<center>Time</center>";
                tc_Time.Controls.Add(lit_Time);
                tr_header.Cells.Add(tc_Time);

                TableCell tc_DR_Name = new TableCell();
                tc_DR_Name.BorderStyle = BorderStyle.Solid;
                tc_DR_Name.BorderWidth = 1;
                tc_DR_Name.Width = 300;
                Literal lit_DR_Name = new Literal();
                lit_DR_Name.Text = "<center>UnListed Doctor Name</center>";
                tc_DR_Name.Controls.Add(lit_DR_Name);
                tr_header.Cells.Add(tc_DR_Name);

                TableCell tc_ww = new TableCell();
                tc_ww.BorderStyle = BorderStyle.Solid;
                tc_ww.BorderWidth = 1;
                tc_ww.Width = 300;
                Literal lit_ww = new Literal();
                lit_ww.Text = "<center>Worked With</center>";
                tc_ww.Controls.Add(lit_ww);
                tr_header.Cells.Add(tc_ww);

                TableCell tc_prod = new TableCell();
                tc_prod.BorderStyle = BorderStyle.Solid;
                tc_prod.BorderWidth = 1;
                tc_prod.Width = 420;
                // tc_prod.ColumnSpan = 6;
                Literal lit_prod = new Literal();
                lit_prod.Text = "<center>Product Promoted / Sampled</center>";
                tc_prod.Controls.Add(lit_prod);
                tr_header.Cells.Add(tc_prod);

                TableCell tc_Gift = new TableCell();
                tc_Gift.BorderStyle = BorderStyle.Solid;
                tc_Gift.BorderWidth = 1;
                tc_Gift.Width = 200;
                Literal lit_Gift = new Literal();
                lit_Gift.Text = "<center>Gift</center>";
                tc_Gift.Controls.Add(lit_Gift);
                tr_header.Cells.Add(tc_Gift);

                tblunlst.Rows.Add(tr_header);

                //Details Section
                foreach (DataRow drFF in ds.Tables[0].Rows)
                {
                    TableRow tr_det = new TableRow();
                    TableCell tc_det_Ses = new TableCell();
                    Literal lit_det_Ses = new Literal();
                    lit_det_Ses.Text = "&nbsp;" + drFF["session"].ToString();
                    tc_det_Ses.BorderStyle = BorderStyle.Solid;
                    tc_det_Ses.BorderWidth = 1;
                    tc_det_Ses.Controls.Add(lit_det_Ses);
                    tr_det.Cells.Add(tc_det_Ses);

                    TableCell tc_det_time = new TableCell();
                    Literal lit_det_time = new Literal();
                    lit_det_time.Text = "&nbsp;" + drFF["time"].ToString();
                    tc_det_time.BorderStyle = BorderStyle.Solid;
                    tc_det_time.BorderWidth = 1;
                    tc_det_time.Controls.Add(lit_det_time);
                    tr_det.Cells.Add(tc_det_time);

                    TableCell tc_det_doc_name = new TableCell();
                    Literal lit_det_doc_name = new Literal();
                    lit_det_doc_name.Text = "&nbsp;" + drFF["drcode"].ToString();
                    tc_det_doc_name.BorderStyle = BorderStyle.Solid;
                    tc_det_doc_name.BorderWidth = 1;
                    tc_det_doc_name.Controls.Add(lit_det_doc_name);
                    tr_det.Cells.Add(tc_det_doc_name);

                    TableCell tc_det_ww = new TableCell();
                    Literal lit_det_ww = new Literal();
                    lit_det_ww.Text = "&nbsp;" + drFF["workwith"].ToString();
                    tc_det_ww.BorderStyle = BorderStyle.Solid;
                    tc_det_ww.BorderWidth = 1;
                    tc_det_ww.Controls.Add(lit_det_ww);
                    tr_det.Cells.Add(tc_det_ww);

                    TableCell tc_det_prod1 = new TableCell();
                    Literal lit_det_prod1 = new Literal();
                    drFF["AddProd"] = drFF["AddProd"].ToString().Replace("~", " ( ").Trim();
                    drFF["AddProd"] = drFF["AddProd"].ToString().Replace("$", " ) ").Trim();
                    drFF["AddProd"] = drFF["AddProd"].ToString().Replace("#", " / ").Trim();
                    if (drFF["prod1"].ToString().Length > 0)
                    {
                        if (drFF["qty1"].ToString().Length > 0)
                        {
                            drFF["prod1"] = drFF["prod1"].ToString().Replace("~", "").Trim() + " ( " + drFF["qty1"].ToString() + " ) ";
                        }
                        else
                        {
                            drFF["prod1"] = drFF["prod1"].ToString().Replace("~", "").Trim();
                        }

                    }
                    if (drFF["prod2"].ToString().Length > 0)
                    {
                        if (drFF["qty2"].ToString().Length > 0)
                        {
                            drFF["prod2"] = "/" + drFF["prod2"].ToString().Replace("~", "").Trim() + " ( " + drFF["qty2"].ToString() + " ) ";
                        }
                        else
                        {
                            drFF["prod2"] = "/" + drFF["prod2"].ToString().Replace("~", "").Trim();
                        }

                    }
                    if (drFF["prod3"].ToString().Length > 0)
                    {
                        if (drFF["qty3"].ToString().Length > 0)
                        {
                            drFF["prod3"] = "/" + drFF["prod3"].ToString().Replace("~", "").Trim() + " ( " + drFF["qty3"].ToString() + " ) ";
                        }
                        else
                        {
                            drFF["prod3"] = "/" + drFF["prod3"].ToString().Replace("~", "").Trim();
                        }
                    }
                    lit_det_prod1.Text = "&nbsp;" + drFF["prod1"].ToString() + "&nbsp;" + drFF["prod2"].ToString() + "&nbsp;" + drFF["prod3"].ToString() + "&nbsp;" + drFF["AddProd"].ToString();
                    tc_det_prod1.BorderStyle = BorderStyle.Solid;
                    tc_det_prod1.BorderWidth = 1;
                    tc_det_prod1.Width = 120;
                    tc_det_prod1.Controls.Add(lit_det_prod1);
                    tc_det_prod1.HorizontalAlign = HorizontalAlign.Left;
                    tr_det.Cells.Add(tc_det_prod1);


                    TableCell tc_det_gift = new TableCell();
                    Literal lit_det_gift = new Literal();
                    drFF["AddGift"] = drFF["AddGift"].ToString().Replace("~", " ( ").Trim();
                    drFF["AddGift"] = drFF["AddGift"].ToString().Replace("$", " ) ").Trim();
                    drFF["AddGift"] = drFF["AddGift"].ToString().Replace("#", " / ").Trim();
                    if (drFF["gqty"].ToString().Length > 0)
                    {
                        drFF["gift"] = drFF["gift"].ToString().Replace("~", "").Trim() + " ( " + drFF["gqty"].ToString() + " ) ";
                    }
                    else
                    {
                        drFF["gift"] = drFF["gift"].ToString().Replace("~", "").Trim();
                    }
                    if (drFF["gift"].ToString() != "")
                    {
                        lit_det_gift.Text = "&nbsp;" + drFF["gift"].ToString() + " / " + "&nbsp;" + drFF["AddGift"].ToString();
                    }
                    tc_det_gift.BorderStyle = BorderStyle.Solid;
                    tc_det_gift.BorderWidth = 1;
                    tc_det_gift.Controls.Add(lit_det_gift);
                    tc_det_gift.HorizontalAlign = HorizontalAlign.Left;
                    tr_det.Cells.Add(tc_det_gift);

                    tblunlst.Rows.Add(tr_det);
                }
            }
            else
            {
                lblunls.Visible = false;
            }

        }
        else
        {
            lblunls.Visible = false;
        }
    }
    private void Preview_Hos()
    {
        DataSet ds = new DataSet();

        sFile = sf_code + "_" + ViewState["curdate"].ToString() + "_Hospital.xml";

        string FilePath = Server.MapPath(sFile);
        if (!File.Exists(FilePath))
        {
            //Start writer
            XmlTextWriter dr_writer = new XmlTextWriter(Server.MapPath(sFile), System.Text.Encoding.UTF8);

            //Start XM DOcument
            dr_writer.WriteStartDocument(true);
            dr_writer.Formatting = Formatting.Indented;
            dr_writer.Indentation = 2;

            //ROOT Element
            dr_writer.WriteStartElement("DCR");
            dr_writer.WriteEndElement();
            //End XML Document
            dr_writer.WriteEndDocument();
            //Close writer
            dr_writer.Close();
        }

        ds.ReadXml(Server.MapPath(sFile));

        if (ds != null && ds.HasChanges())
        {
            if (ds.Tables[0].Rows.Count > 0)
            {

                TableRow tr_header = new TableRow();
                tr_header.BorderStyle = BorderStyle.Solid;
                tr_header.BorderWidth = 1;

                TableCell tc_Ses = new TableCell();
                tc_Ses.BorderStyle = BorderStyle.Solid;
                tc_Ses.BorderWidth = 1;
                tc_Ses.Width = 300;
                Literal lit_Ses = new Literal();
                lit_Ses.Text = "Hospital Name";
                tc_Ses.Controls.Add(lit_Ses);
                tr_header.Cells.Add(tc_Ses);
                // tr_header.BackColor = System.Drawing.Color.Pink;
                tr_header.BackColor = System.Drawing.Color.FromName("#FDD7E4");
                TableCell tc_ww = new TableCell();
                tc_ww.BorderStyle = BorderStyle.Solid;
                tc_ww.BorderWidth = 1;
                tc_ww.Width = 300;
                Literal lit_ww = new Literal();
                lit_ww.Text = "<center>Worked With</center>";
                tc_ww.Controls.Add(lit_ww);
                tr_header.Cells.Add(tc_ww);

                TableCell tc_prod = new TableCell();
                tc_prod.BorderStyle = BorderStyle.Solid;
                tc_prod.BorderWidth = 1;
                tc_prod.Width = 420;
                Literal lit_prod = new Literal();
                lit_prod.Text = "<center>POB</center>";
                tc_prod.Controls.Add(lit_prod);
                tr_header.Cells.Add(tc_prod);


                tblhos.Rows.Add(tr_header);

                //Details Section
                foreach (DataRow drFF in ds.Tables[0].Rows)
                {
                    TableRow tr_det = new TableRow();
                    TableCell tc_det_Ses = new TableCell();
                    Literal lit_det_Ses = new Literal();
                    lit_det_Ses.Text = "&nbsp;" + drFF["hospital"].ToString();
                    tc_det_Ses.BorderStyle = BorderStyle.Solid;
                    tc_det_Ses.BorderWidth = 1;
                    tc_det_Ses.Controls.Add(lit_det_Ses);

                    tr_det.Cells.Add(tc_det_Ses);

                    TableCell tc_det_doc_name = new TableCell();
                    Literal lit_det_doc_name = new Literal();
                    lit_det_doc_name.Text = "&nbsp;" + drFF["hosww"].ToString();
                    tc_det_doc_name.BorderStyle = BorderStyle.Solid;
                    tc_det_doc_name.BorderWidth = 1;
                    tc_det_doc_name.Controls.Add(lit_det_doc_name);
                    tr_det.Cells.Add(tc_det_doc_name);

                    TableCell tc_det_ww = new TableCell();
                    Literal lit_det_ww = new Literal();
                    lit_det_ww.Text = "&nbsp;" + drFF["pob"].ToString();
                    tc_det_ww.BorderStyle = BorderStyle.Solid;
                    tc_det_ww.BorderWidth = 1;
                    tc_det_ww.Controls.Add(lit_det_ww);
                    tr_det.Cells.Add(tc_det_ww);

                    tblhos.Rows.Add(tr_det);
                }
            }
            else
            {
                lblhos.Visible = false;
            }
        }
        else
        {
            lblhos.Visible = false;
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        DCR_New dc = new DCR_New();

        sslno = dc.get_Trans_SlNo_toIns(sf_code, sCurDate);
        if (sslno > 0)
        {
            iret = dc.Create_DCRHead_Trans(sf_code, sslno);
            if (iret > 0)
            {
                removexml();
                //ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('DCR Approved Successfully');location.href='~/MasterFiles/MGR/DCR_Bulk_Approval.aspx?sfcode=" + sf_code + "&Mon=" + dtDCR.Month.ToString() + "&Year=" + dtDCR.Year.ToString() + "'</script>");
                //Response.Redirect("~/MasterFiles/MGR/DCR_Bulk_Approval.aspx?sfcode=" + sf_code + "&Mon=" + dtDCR.Month.ToString() + "&Year=" + dtDCR.Year.ToString());
                Response.Write("<SCRIPT LANGUAGE='javascript'>alert('DCR Approved Successfully');location.href='../../MGR/DCR_Bulk_Approval.aspx?sfcode=" + sf_code + "&Mon=" + dtDCR.Month.ToString() + "&Year=" + dtDCR.Year.ToString() + "'</script>");
            }
        }
    }
    private void removexml()
    {
        //Delete the Listed Doctor XML file
        //string FilePath = Server.MapPath("DailCalls.xml");
        //sFile = sf_code + sCurDate + "ListedDR.xml";

        string sFileHeader = sf_code + "_" + ViewState["curdate"].ToString() + "_Header.xml";
        string headerFilePath = Server.MapPath(sFileHeader);
        if (File.Exists(headerFilePath))
            File.Delete(headerFilePath);

        sFile = sf_code + "_" + ViewState["curdate"].ToString() + "_ListedDR.xml";
        string FilePath = Server.MapPath(sFile);
        if (File.Exists(FilePath))
            File.Delete(FilePath);

        //Delete the Chemists XML file
        //FilePath = Server.MapPath("Chem_DCR.xml");
        string sChemFile = sf_code + "_" + ViewState["curdate"].ToString() + "_Chem.xml";
        string chemFilePath = Server.MapPath(sChemFile);
        if (File.Exists(chemFilePath))
            File.Delete(chemFilePath);

        //Delete the Stockiest XML file
        string sStockFile = sf_code + "_" + ViewState["curdate"].ToString() + "_Stockiest.xml";
        string stockFilePath = Server.MapPath(sStockFile);
        if (File.Exists(stockFilePath))
            File.Delete(stockFilePath);

        //Delete the Hospital XML file
        string sHosFile = sf_code + "_" + ViewState["curdate"].ToString() + "_Hospital.xml";
        string hosFilePath = Server.MapPath(sHosFile);
        if (File.Exists(hosFilePath))
            File.Delete(hosFilePath);


        //Delete the Un-:isted XML file                
        string sUnLstFile = sf_code + "_" + ViewState["curdate"].ToString() + "UnLstDR.xml";
        string unlstFilePath = Server.MapPath(sUnLstFile);
        if (File.Exists(unlstFilePath))
            File.Delete(unlstFilePath);
    }
    protected void btnReject_Click(object sender, EventArgs e)
    {
        txtReason.Visible = true;
        btnSave.Enabled = false;
        btnReject.Enabled = false;
        btnSubmit.Visible = true;
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        if (txtReason.Text.Trim() != "")
        {
            DCR dc = new DCR();
            sslno = dc.get_Trans_SlNo_toIns(sf_code, sCurDate);
            if (sslno > 0)
            {
                iret = dc.Reject_DCR(sf_code, sslno, txtReason.Text);
                if (iret > 0)
                {
                    //ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('DCR Rejected Successfully');</script>");
                    //Response.Redirect("~/MasterFiles/MGR/DCR_Bulk_Approval.aspx?sfcode=" + sf_code + "&Mon=" + dtDCR.Month.ToString() + "&Year=" + dtDCR.Year.ToString());
                    Response.Write("<SCRIPT LANGUAGE='javascript'>alert('DCR Rejected Successfully');location.href='../../MGR/DCR_Bulk_Approval.aspx?sfcode=" + sf_code + "&Mon=" + dtDCR.Month.ToString() + "&Year=" + dtDCR.Year.ToString() + "'</script>");
                }
            }
        }
        else
        {
            txtReason.Focus();
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Enter the Reason')</script>");
        }

    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/MasterFiles/MGR/DCR_Bulk_Approval.aspx?sfcode=" + sf_code + "&Mon=" + dtDCR.Month.ToString() + "&Year=" + dtDCR.Year.ToString());
        // Response.Redirect("~/MasterFiles/MGR/DCR_Bulk_Approval.aspx?sfcode=" + sfcode + "?Mon = "
    }

}