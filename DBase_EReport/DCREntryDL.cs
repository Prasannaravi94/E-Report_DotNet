using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using Bus_Objects;
using Newtonsoft.Json;
using System.Collections;
/// <summary>
/// Summary description for DCREntryDL
/// </summary>
namespace DBase_EReport
{
    public class DCREntryDL
    {
        private string strQry = string.Empty;
        public DCREntry.SFDetails get_SFDetails(string SFCode)
        {
            DCREntry.SFDetails SFDets = new DCREntry.SFDetails();
            DB_EReporting DL = new DB_EReporting();
            try
            {
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@SF", SFCode)
                };
                DataTable SFDet = DL.Exec_DataTableWithParam("spGetActvSFDetails", CommandType.StoredProcedure, parameters);
                var SF = SFDet.AsEnumerable().FirstOrDefault();
                SFDets.SFCode = SF.Field<string>("SF_Code");
                SFDets.SFName = SF.Field<string>("SF_Name");
                SFDets.SFtype = SF.Field<int>("SF_Type");
                SFDets.State = SF.Field<byte>("State_Code");
                SFDets.Div = SF.Field<int>("OwnDiv");
                SFDets.Divs = SF.Field<string>("Division_Code");
            }
            catch { throw; }
            finally { DL = null; }
            return SFDets;
        }
        public DCREntry.DCRSetup getSetups(DCREntry.SFDetails SF)
        {

            DCREntry.DCRSetup Setup = new DCREntry.DCRSetup();
            DB_EReporting DL = new DB_EReporting();
            try
            {
                SqlParameter[] parameters = new SqlParameter[]
                {

                    new SqlParameter("@SFTyp", SF.SFtype),
                    new SqlParameter("@Div", SF.Div),
                    new SqlParameter("@SF", SF.SFCode)
                };
                DataTable SFDet = DL.Exec_DataTableWithParam("spGetSetups", CommandType.StoredProcedure, parameters);
                var Row = SFDet.AsEnumerable().FirstOrDefault();

                Setup.TPBased = Row.Field<byte>("TpBased");
                Setup.DCRAppr = Row.Field<byte>("Approval_System");
                Setup.DlyNeed = Row.Field<string>("DelayedSystem_Required_Status");
                Setup.DlyDays = Row.Field<byte>("No_Of_Days_Delay");
                Setup.DlyHoli = Row.Field<string>("Delay_Holiday_Status");
                Setup.HosNeed = 0;//Row.Field<byte>("OwnDiv");
                Setup.StkNeed = 1;//Row.Field<byte>("ListedStockist_Entry_Permission");
                Setup.UdrNeed = Row.Field<byte>("NonDrNeeded");
                Setup.ProdMand = Row.Field<byte>("DCRProd_Mand");
                Setup.ProdSel = Row.Field<byte>("No_Of_Product_selection_Allowed_in_Dcr");
                Setup.PQtyZro = Row.Field<byte>("SampleProQtyDefaZero");
                Setup.POBtype = Row.Field<string>("POBType");
                Setup.TmNeed = Row.Field<byte>("DCRTime_Entry_Permission");
                Setup.TmMand = Row.Field<byte>("DCRTime_Mand");
                Setup.SesNeed = Row.Field<byte>("DCRSess_Entry_Permission");
                Setup.SesMand = Row.Field<byte>("DCRSess_Mand");
                Setup.NoOfDrs = Row.Field<byte>("No_Of_DCR_Drs_Count");
                Setup.NoOfChm = Row.Field<byte>("No_Of_DCR_Chem_Count");
                Setup.NoOfStk = Row.Field<byte>("No_Of_DCR_Stockist_Count");
                Setup.NoOfHos = Row.Field<byte>("No_of_dcr_Hosp");
                Setup.NoOfUdr = Row.Field<byte>("No_Of_DCR_Ndr_Count");
                Setup.RemLen = Row.Field<int>("Remarks_length_Allowed");
                Setup.HoliAuto = Row.Field<string>("AutoPost_Holiday_Status");
                Setup.WkOffAuto = Row.Field<string>("AutoPost_Sunday_Status");
                Setup.DrRem = Row.Field<byte>("DCRLDR_Remarks");
                Setup.NChm = Row.Field<byte>("DCRNew_Chem");
                Setup.NUdr = Row.Field<byte>("DCRNew_ULDr");

                Setup.CPOBM = Row.Field<byte>("ChemPOB_Updation");
                Setup.DPOBM = Row.Field<byte>("LstPOB_Updation");

                Setup.DRxFed = Row.Field<byte>("ProductFeedback_Needed");
                Setup.DRxQty = Row.Field<byte>("PrdRx_Qty_Needed");

                Setup.SDPCap = Row.Field<string>("wrk_area_Name");

                Setup.TotDlyNeed = Row.Field<byte>("LockSystem_Needed");
                Setup.RDlylimit = Row.Field<int>("LockSystem_Timelimit");

                Setup.MultiDr = Row.Field<byte>("MultiDr");
                Setup.ShowPatchOnly = Row.Field<byte>("ShowPatchOnly");
                Setup.DCRDrMand = Row.Field<byte>("DCR_Dr_Mandatory");
                Setup.DCRJointWrk = Row.Field<string>("JointWrk_WithMR");

                Setup.TPBaseDCRDr = Row.Field<byte>("TpBasesd_DCR");
                Setup.TPDCRDeviation = Row.Field<byte>("TPDCR_Deviation");
                Setup.TPDCR_MGRAppr = Row.Field<byte>("TPDCR_MGRAppr");
                Setup.No_of_TP_View = Row.Field<byte>("No_of_TP_View");
                Setup.ChemPOBQty = Row.Field<byte>("ChemPOB_Qty_Needed");
                Setup.ProdQtyCaption = Row.Field<string>("PrdRx_Qty_Caption");
                Setup.ChemQtyCaption = Row.Field<string>("ChemPOB_Qty_Caption");

                Setup.PrdSampleQty = Row.Field<byte>("PrdSample_Qty_Needed");
                Setup.ChemSampleQty = Row.Field<byte>("ChemSample_Qty_Needed");

                Setup.DrSampleQtyCaption = Row.Field<string>("PrdSample_Qty_Caption");
                Setup.ChemSampleQtyCaption = Row.Field<string>("ChemSample_Qty_Caption");

                Setup.DrPOBQtyZero = Row.Field<byte>("Dr_POBQty_DefaZero");
                Setup.ChemSampleQtyZero = Row.Field<byte>("Chem_SampleQty_DefaZero");
                Setup.ChemPOBQtyZero = Row.Field<byte>("Chem_POBQty_DefaZero");
                if (SF.SFtype == 1)
                {
                    Setup.InputMand = Row.Field<byte>("Input_Mand");                  
                }
                Setup.FFWiseDelay = Row.Field<byte>("FieldForceWise_Delay");
                Setup.FFWiseDelay_Days = Row.Field<byte>("FFWise_Delay_Days");
                Setup.ProdSampleQtyValid = Row.Field<int>("Prod_SampleQty_Validation_Needed");
                Setup.InputQtyValid = Row.Field<int>("InputQty_Validation_Needed");

                SqlParameter[] param = new SqlParameter[]
                            {
                    new SqlParameter("@Div", SF.Div)
                            };
                DataTable DSDT = DL.Exec_DataTableWithParam("getDCRSetup", CommandType.StoredProcedure, param);
                string sJsonE = "", sJsonV = "";
                var DSRows = (from w in DSDT.AsEnumerable() select w);
                foreach (var rw in DSRows)
                {
                    if (sJsonE != "") { sJsonE += ","; sJsonV += ","; }
                    sJsonE += rw.Field<string>("ES");
                    sJsonV += rw.Field<string>("VS");
                }
                Setup.SDCRE = "{" + sJsonE + "}";
                Setup.SDCRV = "{" + sJsonV + "}";
            }
            catch { throw; }
            finally { DL = null; }
            return Setup;
        }


        public List<DCREntry.SFDetails> get_BaseSFs(string SFCode)
        {
            List<DCREntry.SFDetails> BSFs = new List<DCREntry.SFDetails>();
            DB_EReporting DL = new DB_EReporting();
            try
            {
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@SF", SFCode)
                };
                DataTable SFDet = DL.Exec_DataTableWithParam("getBaseLvlSFs_APP", CommandType.StoredProcedure, parameters);
                var SFs = (from w in SFDet.AsEnumerable() select w);
                foreach (var SF in SFs)
                {
                    DCREntry.SFDetails nSF = new DCREntry.SFDetails();

                    nSF.SFCode = SF.Field<string>("id");
                    nSF.SFName = SF.Field<string>("name");
                    nSF.Div = SF.Field<int>("OwnDiv");
                    nSF.Divs = SF.Field<string>("Division_Code");
                    BSFs.Add(nSF);
                }
            }
            catch { throw; }
            finally { DL = null; }
            return BSFs;
        }
        public Boolean deleteEntryTemp(string SFCode, DateTime Dt)
        {
            Boolean Flag;
            Flag = false;
            DB_EReporting DL = new DB_EReporting();
            try
            {
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@SF", SFCode),
                    new SqlParameter("@Dt", Dt)
                };
                DL.Exec_NonQueryWithParam("DelDCRTempByDt", CommandType.StoredProcedure, parameters);
                Flag = true;
            }
            catch { throw; }
            finally { DL = null; }
            return Flag;
        }
        public Boolean isLeaveDtPost(DCREntry.SFDetails SF, DateTime Dt)
        {
            Boolean Flag = false;
            int LvId = 0;
            DB_EReporting DL = new DB_EReporting();
            try
            {
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@LvId", LvId) { Direction = ParameterDirection.Output },
                    new SqlParameter("@SF", SF.SFCode),
                    new SqlParameter("@SFTyp", SF.SFtype),
                    new SqlParameter("@Div", SF.Div),
                    new SqlParameter("@DCRDt", Dt),
                    new SqlParameter("@SysIP", ""),
                    new SqlParameter("@ETyp", "")
                };
                DL.Exec_NonQueryWithParam("ChkandPostLeaveDt", CommandType.StoredProcedure, parameters);
                if ((int)parameters[0].Value > 0)
                {

                    Flag = true;
                }
            }
            catch { throw; }
            finally { DL = null; }
            return Flag;
        }
        public Boolean isHolidDtPost(DCREntry.SFDetails SF, DateTime Dt)
        {
            Boolean Flag = false;
            int HoliId = 0;
            DB_EReporting DL = new DB_EReporting();
            try
            {
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@HoliId", HoliId) { Direction = ParameterDirection.Output },
                    new SqlParameter("@SF", SF.SFCode),
                    new SqlParameter("@SFTyp", SF.SFtype),
                    new SqlParameter("@State", SF.State),
                    new SqlParameter("@Div", SF.Div),
                    new SqlParameter("@DCRDt", Dt),
                    new SqlParameter("@SysIP", ""),
                    new SqlParameter("@ETyp", "")
                };
                DL.Exec_NonQueryWithParam("ChkandPostHoliDt", CommandType.StoredProcedure, parameters);
                if ((int)parameters[0].Value > 0)
                {
                    Flag = true;
                }
            }
            catch { throw; }
            finally { DL = null; }
            return Flag;
        }
        public Boolean isWkoffDtPost(DCREntry.SFDetails SF, DateTime Dt)
        {
            Boolean Flag = false;
            int WkId = 0;

            DB_EReporting DL = new DB_EReporting();
            try
            {
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@WkInsFl",WkId) { Direction = ParameterDirection.Output },
                    new SqlParameter("@SF", SF.SFCode),
                    new SqlParameter("@SFTyp", SF.SFtype),
                    new SqlParameter("@Div", SF.Div),
                    new SqlParameter("@State", SF.State),
                    new SqlParameter("@DCRDt", Dt),
                    new SqlParameter("@SysIP", ""),
                    new SqlParameter("@ETyp", "")
                };
                DL.Exec_NonQueryWithParam("ChkandPostWkOffDt", CommandType.StoredProcedure, parameters);
                if ((int)parameters[0].Value > 0)
                {
                    Flag = true;
                }
            }
            catch { throw; }
            finally { DL = null; }
            return Flag;
        }
        public Boolean PostDly(string SFCode, DateTime Dt, int Div)
        {
            DB_EReporting DL = new DB_EReporting();
            Boolean Flag = false;
            try
            {
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@SF", SFCode),
                    new SqlParameter("@Div", Div),
                    new SqlParameter("@Dt", Dt)
                };
                DL.Exec_NonQueryWithParam("PostDlyDt", CommandType.StoredProcedure, parameters);
                Flag = true;
            }
            catch { throw; }
            finally { DL = null; }
            return Flag;
        }
        public Boolean udpDCRDt(string SFCode, DateTime Dt)
        {
            DB_EReporting DL = new DB_EReporting();
            Boolean Flag = false;
            try
            {
                SqlParameter[] param = new SqlParameter[]
                            {
                                new SqlParameter("@SF", SFCode),
                                new SqlParameter("@DCRDt", Dt)
                            };
                DL.Exec_NonQueryWithParam("updDCRDate", CommandType.StoredProcedure, param);
                Flag = true;
            }
            catch { throw; }
            finally { DL = null; }
            return Flag;
        }
        public Boolean ApproveDCR(string SFCode, DateTime Dt)
        {
            DB_EReporting DL = new DB_EReporting();
            Boolean Flag = false;
            try
            {
                SqlParameter[] param = new SqlParameter[]
                            {
                                new SqlParameter("@SF", SFCode),
                                new SqlParameter("@Dt", Dt)
                            };
                DL.Exec_NonQueryWithParam("ApproveDCRByDt", CommandType.StoredProcedure, param);
                Flag = true;
            }
            catch { throw; }
            finally { DL = null; }
            return Flag;
        }
        private Boolean ChkIsAutoPost(DCREntry.SFDetails SF, DCREntry.DCRSetup Setup, DateTime Dt, byte Typ)
        {
            Boolean Flag = false;
            if (isLeaveDtPost(SF, Dt) == true) Flag = true;
            if (Setup.HoliAuto == "1" && Flag == false && (Typ < 1 || Typ > 2)) if (isHolidDtPost(SF, Dt) == true)
                {
                    Flag = true;
                    ApproveDCR(SF.SFCode, Dt);
                }
            if (Setup.WkOffAuto == "1" && Flag == false && (Typ < 1 || Typ > 2)) if (isWkoffDtPost(SF, Dt) == true)
                {
                    Flag = true;
                    ApproveDCR(SF.SFCode, Dt);
                }
            return Flag;
        }

        public Boolean updateType(string SFCode, DateTime Dt, byte type)
        {
            DB_EReporting DL = new DB_EReporting();
            Boolean Flag = false;
            try
            {
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@SF", SFCode),
                    new SqlParameter("@Dt", Dt),
                    new SqlParameter("@Typ", type)
                };
                DL.Exec_NonQueryWithParam("UpdDCRFlags", CommandType.StoredProcedure, parameters);
                Flag = true;
            }
            catch { throw; }
            finally { DL = null; }
            return Flag;
        }
        private DCREntry.DtDetsDCR getEntryDt(string SFCode, int Div)
        {

            DCREntry.DtDetsDCR DtDets = new DCREntry.DtDetsDCR();
            DB_EReporting DL = new DB_EReporting();
            try
            {
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@SF", SFCode),
                    new SqlParameter("@Div", Div)
                };
                DataTable DtDet = DL.Exec_DataTableWithParam("GetDlyorReEntry", CommandType.StoredProcedure, parameters);

                var Row = DtDet.AsEnumerable().FirstOrDefault();

                DtDets.DCR_Date = Row.Field<DateTime>("DCRDt");
                DtDets.DTRem = Row.Field<string>("Rem");
                DtDets.Type = Row.Field<byte>("Typ");
                DtDets.CurrDt = DateTime.Today; // DateTime.Now.ToString("M/d/yyyy");
                DtDets.STime = DateTime.Now;
                DtDets.DtMsg = Row.Field<string>("DtMsg"); ;
            }
            catch { throw; }
            finally { DL = null; }

            return DtDets;
        }

        public DCREntry.DtDetsDCR GetDCRDtDet(DCREntry.SFDetails SF, DCREntry.DCRSetup Setup)
        {
            DCREntry.DtDetsDCR DtDets = new DCREntry.DtDetsDCR();
            DB_EReporting DL = new DB_EReporting();
            try
            {
            DoA:
                DtDets = getEntryDt(SF.SFCode, SF.Div);
                Boolean iFlag = false;
                if (DtDets.Type != 0)
                {
                    iFlag = ChkIsAutoPost(SF, Setup, DtDets.DCR_Date, DtDets.Type);
                    if (iFlag == true)
                    {
                        updateType(SF.SFCode, DtDets.DCR_Date, DtDets.Type);
                        goto DoA;
                    }
                }
                else
                {
                    if (Setup.DlyNeed == "1")
                    {
                        int iReturn = 0;
                        //iReturn = Setup.DlyDays;
                        if (Setup.FFWiseDelay == 1)
                        {
                            iReturn = Setup.FFWiseDelay_Days;
                        }
                        else
                        {
                            iReturn = Setup.DlyDays;
                        }

                        if (Setup.DlyHoli == "1")
                        {
                            string strQry = "exec GetDlyHoliDyCnt " + SF.State + "," + SF.Div + "," + iReturn;
                            iReturn = iReturn + DL.Exec_Scalar(strQry);
                        }
                        //if ((DateTime.Now - DtDets.DCR_Date).TotalDays > iReturn && iReturn > 0)
                        if ((DateTime.Now.Date - DtDets.DCR_Date.Date).Days > iReturn && iReturn > 0) //Changes done by Vasanthi
                        {
                            //while ((DateTime.Now - DtDets.DCR_Date).TotalDays > iReturn)
                            while ((DateTime.Now.Date - DtDets.DCR_Date.Date).Days > iReturn)
                            {
                                iFlag = ChkIsAutoPost(SF, Setup, DtDets.DCR_Date, DtDets.Type);
                                if (iFlag == true) { goto enLop; }
                                PostDly(SF.SFCode, DtDets.DCR_Date, SF.Div);
                            enLop:
                                DtDets.DCR_Date = DtDets.DCR_Date.AddDays(1);
                            }
                            udpDCRDt(SF.SFCode, DtDets.DCR_Date);
                        }
                        if (Setup.TotDlyNeed == 1)
                        {
                            string psMsg = "";
                            SqlParameter[] parameters = new SqlParameter[]
                            {
                                new SqlParameter("@SF", SF.SFCode),
                                new SqlParameter("@rlimt", Setup.RDlylimit),
                                new SqlParameter("@Msg",SqlDbType.VarChar,500) { Direction = ParameterDirection.Output }
                            };
                            DL.Exec_NonQueryWithParam("GetTotalDlyState", CommandType.StoredProcedure, parameters);
                            if ((string)parameters[2].Value != "")
                            {
                                DtDets.DtMsg = (string)parameters[2].Value;
                            }
                        }
                    }
                    iFlag = ChkIsAutoPost(SF, Setup, DtDets.DCR_Date, DtDets.Type);
                    if (iFlag == true)
                    {
                        DtDets.DCR_Date = DtDets.DCR_Date.AddDays(1);
                        udpDCRDt(SF.SFCode, DtDets.DCR_Date);
                        goto DoA;
                    }
                }
            }
            catch { throw; }
            finally { DL = null; }
            return DtDets;
        }
        public List<DCREntry.Worktypes> get_WorkTypes(string SFCode)
        {
            List<DCREntry.Worktypes> WrkTys = new List<DCREntry.Worktypes>();
            DB_EReporting DL = new DB_EReporting();
            try
            {
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@SFCd", SFCode)
                };
                //DataTable WrkTy = DL.Exec_DataTableWithParam("GetWorkTypes_App", CommandType.StoredProcedure, parameters);
                DataTable WrkTy = DL.Exec_DataTableWithParam("GetWorkTypes_Web", CommandType.StoredProcedure, parameters);
                var WTs = (from w in WrkTy.AsEnumerable() select w);
                foreach (var WT in WTs)
                {
                    DCREntry.Worktypes nWTy = new DCREntry.Worktypes();
                    nWTy.Code = WT.Field<int>("id");
                    nWTy.Name = WT.Field<string>("name");
                    nWTy.ETabs = WT.Field<string>("ETabs");
                    nWTy.FWFlg = WT.Field<string>("FWFlg");
                    WrkTys.Add(nWTy);
                }
            }
            catch { throw; }
            finally { DL = null; }
            return WrkTys;
        }
        public List<DCREntry.Clusters> gethalf_Daywrk(string Divcode)
        {
            List<DCREntry.Clusters> WrkTys = new List<DCREntry.Clusters>();
            DB_EReporting DL = new DB_EReporting();
            try
            {
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@divcode", Divcode)
                };
                //DataTable WrkTy = DL.Exec_DataTableWithParam("GetWorkTypes_App", CommandType.StoredProcedure, parameters);
                DataTable WrkTy = DL.Exec_DataTableWithParam("gethalf_Daywrk", CommandType.StoredProcedure, parameters);
                var WTs = (from w in WrkTy.AsEnumerable() select w);
                foreach (var WT in WTs)
                {
                    DCREntry.Clusters nWTy = new DCREntry.Clusters();
                    nWTy.Code = WT.Field<int>("Code");
                    nWTy.Name = WT.Field<string>("Name");                    
                    WrkTys.Add(nWTy);
                }
            }
            catch { throw; }
            finally { DL = null; }
            return WrkTys;
        }

        public List<DCREntry.Clusters> getClusters(string SFCode)
        {
            List<DCREntry.Clusters> Twns = new List<DCREntry.Clusters>();
            DB_EReporting DL = new DB_EReporting();
            try
            {
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@SF", SFCode)
                };
                DataTable WrkTy = DL.Exec_DataTableWithParam("spGetClusters_Web", CommandType.StoredProcedure, parameters);
                var WTs = (from w in WrkTy.AsEnumerable() select w);
                foreach (var WT in WTs)
                {
                    DCREntry.Clusters nTwn = new DCREntry.Clusters();
                    nTwn.Code = WT.Field<decimal>("id");
                    nTwn.Name = WT.Field<string>("name");
                    //nTwn.PM_Visit = WT.Field<decimal>("PM_Visit"
                    //nTwn.CM_Visit = WT.Field<decimal>("CM_Visit");
                    nTwn.PM_Visit=Convert.ToDecimal((WT.Field<decimal?>("PM_Visit")!=null) ? WT.Field<decimal?>("PM_Visit") : Convert.ToDecimal("0"));
                    nTwn.CM_Visit = Convert.ToDecimal((WT.Field<decimal?>("CM_Visit") != null) ? WT.Field<decimal?>("CM_Visit") : Convert.ToDecimal("0"));
                    nTwn.Territory_Cat = WT.Field<string>("Territory_Cat");
                    Twns.Add(nTwn);
                  
                }
            }
            catch { throw; }
            finally { DL = null; }
            return Twns;
        }

        public string getClustersJSON(string SFCode)
        {
            DB_EReporting DL = new DB_EReporting();
            try
            {
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@SF", SFCode)
                };
                DataTable tbTwn = DL.Exec_DataTableWithParam("spGetClusters", CommandType.StoredProcedure, parameters);
                return JsonConvert.SerializeObject(tbTwn);
            }
            catch { throw; }
            finally { DL = null; }
        }
        public string getProdsJSON(string SFCode, int SampleValid)
        {
            DB_EReporting DL = new DB_EReporting();
            try
            {
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@SF_Code", SFCode)
                };
                //DataTable Prods = DL.Exec_DataTableWithParam("getAppProd", CommandType.StoredProcedure, parameters);

                DataTable Prods;
                if (SampleValid == 1)
                {
                    Prods = DL.Exec_DataTableWithParam("getWebProd_WithSampleQty", CommandType.StoredProcedure, parameters);
                }
                else
                {
                    Prods = DL.Exec_DataTableWithParam("getWebProd", CommandType.StoredProcedure, parameters);
                }


                return JsonConvert.SerializeObject(Prods);
            }
            catch { throw; }
            finally { DL = null; }
        }
        public string getInputJSON(string SFCode, DateTime Dt, int InputValid)
        {
            DB_EReporting DL = new DB_EReporting();
            try
            {
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@SF_Code", SFCode),
                    new SqlParameter("@Dt", Dt)
                };
                //DataTable Inputs = DL.Exec_DataTableWithParam("getAppGift", CommandType.StoredProcedure, parameters);
                DataTable Inputs;
                if (InputValid == 1)
                {
                    Inputs = DL.Exec_DataTableWithParam("getWebGift_WithQty", CommandType.StoredProcedure, parameters);
                }
                else
                {
                    Inputs = DL.Exec_DataTableWithParam("getWebGift", CommandType.StoredProcedure, parameters);
                }

                return JsonConvert.SerializeObject(Inputs);
            }
            catch { throw; }
            finally { DL = null; }
        }
        public string getFeedBkJSON(int div)
        {
            DB_EReporting DL = new DB_EReporting();
            try
            {
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@div", div)
                };
                DataTable Feedbks = DL.Exec_DataTableWithParam("getFeedBk", CommandType.StoredProcedure, parameters);
                return JsonConvert.SerializeObject(Feedbks);
            }
            catch { throw; }
            finally { DL = null; }
        }
        public string getProdFeedBkJSON(int div)
        {
            DB_EReporting DL = new DB_EReporting();
            try
            {
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@div", div)
                };
                DataTable Feedbks = DL.Exec_DataTableWithParam("getProductFeedbk", CommandType.StoredProcedure, parameters);
                return JsonConvert.SerializeObject(Feedbks);
            }
            catch { throw; }
            finally { DL = null; }
        }
        public string getCateJSON(int div)
        {
            DB_EReporting DL = new DB_EReporting();
            try
            {
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@div", div)
                };
                DataTable Cats = DL.Exec_DataTableWithParam("getDocCats", CommandType.StoredProcedure, parameters);
                return JsonConvert.SerializeObject(Cats);
            }
            catch { throw; }
            finally { DL = null; }
        }
        public string getSpecJSON(int div)
        {
            DB_EReporting DL = new DB_EReporting();
            try
            {
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@div", div)
                };
                DataTable Specs = DL.Exec_DataTableWithParam("getDocSpec", CommandType.StoredProcedure, parameters);
                return JsonConvert.SerializeObject(Specs);
            }
            catch { throw; }
            finally { DL = null; }
        }
        public string getClaJSON(int div)
        {
            DB_EReporting DL = new DB_EReporting();
            try
            {
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@div", div)
                };
                DataTable Clas = DL.Exec_DataTableWithParam("getDocClass", CommandType.StoredProcedure, parameters);
                return JsonConvert.SerializeObject(Clas);
            }
            catch { throw; }
            finally { DL = null; }
        }
        public string getQualJSON(int div)
        {
            DB_EReporting DL = new DB_EReporting();
            try
            {
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@div", div)
                };
                DataTable Quals = DL.Exec_DataTableWithParam("getDocQual", CommandType.StoredProcedure, parameters);
                return JsonConvert.SerializeObject(Quals);
            }
            catch { throw; }
            finally { DL = null; }
        }
        public string getDoctorJSON(string SFCode)
        {
            DB_EReporting DL = new DB_EReporting();
            try
            {
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@SF", SFCode)
                };
                // DataTable tbDrs = DL.Exec_DataTableWithParam("spGetDoctors", CommandType.StoredProcedure, parameters);
                DataTable tbDrs = DL.Exec_DataTableWithParam("spGetDoctors_Web", CommandType.StoredProcedure, parameters);
                return JsonConvert.SerializeObject(tbDrs);
            }
            catch { throw; }
            finally { DL = null; }
        }
        public string getChemistJSON(string SFCode)
        {
            DB_EReporting DL = new DB_EReporting();
            try
            {
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@SF", SFCode)
                };
                DataTable tbChm = DL.Exec_DataTableWithParam("spGetChemists", CommandType.StoredProcedure, parameters);
                return JsonConvert.SerializeObject(tbChm);
            }
            catch { throw; }
            finally { DL = null; }
        }
        public string getStockistJSON(string SFCode)
        {
            DB_EReporting DL = new DB_EReporting();
            try
            {
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@SF", SFCode)
                };
                //DataTable tbStk = DL.Exec_DataTableWithParam("spGetStockist", CommandType.StoredProcedure, parameters);
                DataTable tbStk = DL.Exec_DataTableWithParam("spGetStockist_Web", CommandType.StoredProcedure, parameters);
                return JsonConvert.SerializeObject(tbStk);
            }
            catch { throw; }
            finally { DL = null; }
        }
        public string getHospJSON(string SFCode)
        {
            DB_EReporting DL = new DB_EReporting();
            try
            {
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@SF", SFCode)
                };
                DataTable tbHos = DL.Exec_DataTableWithParam("spGetHospital", CommandType.StoredProcedure, parameters);
                return JsonConvert.SerializeObject(tbHos);
            }
            catch { throw; }
            finally { DL = null; }
        }
        public string getUnlistedDrJSON(string SFCode)
        {
            DB_EReporting DL = new DB_EReporting();
            try
            {
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@SF", SFCode)
                };
                DataTable tbUnld = DL.Exec_DataTableWithParam("spGetUnlistedDr", CommandType.StoredProcedure, parameters);
                return JsonConvert.SerializeObject(tbUnld);
            }
            catch { throw; }
            finally { DL = null; }
        }
        public string getJntWrkJSON(string BSF, string ESF)
        {
            DB_EReporting DL = new DB_EReporting();
            try
            {
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@BSF", BSF),
                    new SqlParameter("@ESF", ESF)
                };
                DataTable tbJW = DL.Exec_DataTableWithParam("getJointWork_App", CommandType.StoredProcedure, parameters);
                return JsonConvert.SerializeObject(tbJW);
            }
            catch { throw; }
            finally { DL = null; }
        }
        private string convertDataString(int typ, string ARCd)
        {
            DB_EReporting DL = new DB_EReporting();
            try
            {
                string sMsl = "";
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@ARCd", ARCd),
                    new SqlParameter("@Typ", typ)
                };
                DataTable tbDet = DL.Exec_DataTableWithParam("getDetailDatas", CommandType.StoredProcedure, parameters);
                var msls = (from w in tbDet.AsEnumerable() select w);
                foreach (var msl in msls)
                {
                    if (sMsl != "") sMsl += ",";
                    sMsl += "{\"sf\":{\"val\":\"" + msl.Field<string>("SF") + "\"},\"twn\":{\"val\":\"" + msl.Field<string>("SDP") + "\",\"txt\":\"" + msl.Field<string>("SDP_Name") + "\"}," +
                        "\"ses\":{\"val\":\"" + msl.Field<string>("Session") + "\",\"txt\":\"" + ((msl.Field<string>("Session") == "M") ? "Morning" : (msl.Field<string>("Session") == "E") ? "Evening" : "") + "\"}," +
                            "\"tm\":{\"val\":\"" + msl.Field<string>("tm") + "\",\"txt\":\"" + msl.Field<string>("tm") + "\"}," +
                            "\"cus\":{\"val\":\"" + msl.Field<decimal>("Trans_Detail_Info_Code") + "\",\"txt\":\"" + msl.Field<string>("Trans_Detail_Name") + "\"}," +
                            "\"pob\":{\"val\":\"" + msl.Field<double>("POB") + "\",\"txt\":\"" + msl.Field<double>("POB") + "\"}," +
                            "\"jw\":{\"val\":\"" + msl.Field<string>("Worked_with_Code") + "\",\"txt\":\"" + msl.Field<string>("Worked_with_Name") + "\"}," +
                            "\"prd\":{\"val\":\"" + msl.Field<string>("Product_Code") + "\",\"txt\":\"" + msl.Field<string>("Product_Detail") + "\"}," +
                            "\"inp\":{\"val\":\"" + msl.Field<string>("Gft") + "\",\"txt\":\"" + msl.Field<string>("gft_Detail") + "\"}," +
                            "\"rem\":\"" + msl.Field<string>("Activity_Remarks") + "\"," +
                            "\"fedbk\":{\"val\":\"" + msl.Field<string>("Rx") + "\",\"txt\":\"" + msl.Field<string>("Rxn") + "\"}}";
                }

                return sMsl;
            }
            catch { throw; }
            finally
            {
                DL = null;
            }
        }
        public string getDCRDetails(string SF, DateTime Dt)
        {
            string DCRJSON = "{";
            DB_EReporting DL = new DB_EReporting();
            try
            {
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@SF", SF),
                    new SqlParameter("@Dt", Dt)
                };
                DataTable tbHD = DL.Exec_DataTableWithParam("getDCRHead", CommandType.StoredProcedure, parameters);
                DCRJSON += "\"Head\":{\"SFCode\":\"" + tbHD.Rows[0].Field<string>("SFCode") + "\",\"EDate\":\"" + tbHD.Rows[0].Field<string>("EDate") + "\",\"Rem\":\"" + tbHD.Rows[0].Field<string>("Rem") + "\",\"Wtyp\":\"" + tbHD.Rows[0].Field<decimal>("Wtyp") + "\"}";
                if (tbHD.Rows[0].Field<string>("FieldWork_Indicator") == "F")
                {
                    string[] DtaCap = new string[] { "Msl", "Chm", "Stk", "Udr", "Hos" };
                    for (int il = 0; il < DtaCap.Length; il++)
                        DCRJSON += ",\"" + DtaCap[il] + "\":[" + convertDataString((il + 1), tbHD.Rows[0].Field<string>("Trans_SlNo")) + "]";
                }
                DCRJSON += "}";
                return DCRJSON;
            }
            catch { throw; }
            finally
            {
                DL = null;
            }
        }
        public string SaveDCRTemp(DCREntry.DCRDatas DtaHead)
        {
            string ARCd = "";
            DB_EReporting DL = new DB_EReporting();
            try
            {
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@ARCd", SqlDbType.VarChar,25) { Direction = ParameterDirection.Output },
                    new SqlParameter("@SF", DtaHead.SFCode),
                    new SqlParameter("@STy", DtaHead.SFTyp),
                    new SqlParameter("@ADt", DtaHead.EDate),
                    new SqlParameter("@Wtyp", DtaHead.Wtyp),
                    new SqlParameter("@TwnCd", ""),//DtaHead.TwnCd),
	                new SqlParameter("@div", DtaHead.Div),
                    new SqlParameter("@Rmks", DtaHead.rem),
                    new SqlParameter("@STm", DtaHead.STime),
                    new SqlParameter("@ETm", DtaHead.ETime),
                    new SqlParameter("@SysIP", DtaHead.SysIP),
                    new SqlParameter("@ETyp", DtaHead.ETyp)
                };
                DL.Exec_NonQueryWithParam("svDCRTemp", CommandType.StoredProcedure, parameters);
                return parameters[0].Value.ToString();
            }
            catch { throw; }
            finally { DL = null; }
        }

        //gowsi start
        public string SaveDCRTemp1(DCREntry.DCRDatas DtaHead,string selvalues)
        {
            string ARCd = "";
            DB_EReporting DL = new DB_EReporting();
            try
            {
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@ARCd", SqlDbType.VarChar,25) { Direction = ParameterDirection.Output },
                    new SqlParameter("@SF", DtaHead.SFCode),
                    new SqlParameter("@STy", DtaHead.SFTyp),
                    new SqlParameter("@ADt", DtaHead.EDate),
                    new SqlParameter("@Wtyp", DtaHead.Wtyp),
                    new SqlParameter("@TwnCd", ""),//DtaHead.TwnCd),
	                new SqlParameter("@div", DtaHead.Div),
                    new SqlParameter("@halfday", selvalues),
                    new SqlParameter("@Rmks", DtaHead.rem),
                    new SqlParameter("@STm", DtaHead.STime),
                    new SqlParameter("@ETm", DtaHead.ETime),
                    new SqlParameter("@SysIP", DtaHead.SysIP),
                    new SqlParameter("@ETyp", DtaHead.ETyp)
                };
                DL.Exec_NonQueryWithParam("svDCRTemp1", CommandType.StoredProcedure, parameters);
                return parameters[0].Value.ToString();
            }
            catch { throw; }
            finally { DL = null; }
        }
        //gowsi end
        public string Create_NewCus(DCREntry.NewCus nCus, DateTime EDt)
        {

            DB_EReporting DL = new DB_EReporting();
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@Cd", SqlDbType.Decimal) { Direction = ParameterDirection.Output },
                new SqlParameter("@ID", nCus.id),
                new SqlParameter("@name", nCus.name),
                new SqlParameter("@cAdd",nCus.addr),
                new SqlParameter("@TCode", nCus.twn.val),
                new SqlParameter("@TName", nCus.twn.txt),
                new SqlParameter("@CatCd", nCus.Cat.val),
                new SqlParameter("@CatNm", nCus.Cat.txt),
                new SqlParameter("@SpcCd", nCus.Spc.val),
                new SqlParameter("@SpcNm", nCus.Spc.txt),
                new SqlParameter("@ClaCd", nCus.Cla.val),
                new SqlParameter("@ClaNm", nCus.Cla.txt),
                new SqlParameter("@QuaCd", nCus.Qua.val),
                new SqlParameter("@QuaNm", nCus.Qua.txt),
                new SqlParameter("@vTyp", nCus.typ),
                new SqlParameter("@SF", nCus.sf),
                new SqlParameter("@CrBy", "DCR"),
                new SqlParameter("@DCRDt", EDt)
            };
            DL.Exec_NonQueryWithParam("svNewCustomer", CommandType.StoredProcedure, parameters);
            return parameters[0].Value.ToString();
        }
        public string SaveDCRDetTemp(string ARCd, string SFCode, byte Typ, byte Div, string ETyp, DateTime EDt, DCREntry.DetailData DtaDet, DCREntry.NewCus nCus = null)
        {
            DtaDet.prd.txt = DtaDet.prd.txt.Replace(" ) ( ", "$").Replace(" ( ", "~").Replace(" ), ", "#");
            string NCusID = "";
            string NCust = "{}";
            string cGft = "";
            string nGft = "";

            string cProd = "";
            string nProd = "";
            string cAProd = "";
            string nAProd = "";

            string sGfCd = "";
            string sGfQt = "";
            string sGfNm = "";

            string cAGft = "";
            string nAGft = "";
            if (Typ == 1 || Typ == 4)
            {
                /*
                ArrayList spProd = new ArrayList(DtaDet.prd.val.Split(new[] { "#" }, StringSplitOptions.None));
                ArrayList spProdn = new ArrayList(DtaDet.prd.txt.Split(new[] { "#" }, StringSplitOptions.None));
                if (DtaDet.prd.val != "")
                {
                    for (int il = 0; il < spProd.Count; il++)
                    {
                        if (il < 3)
                        {
                            cProd = cProd + ((cProd != "") ? "#" : "") + spProd[0];
                            nProd = nProd + ((nProd != "") ? "#" : "") + spProdn[0];
                            spProd.RemoveAt(0);
                            spProdn.RemoveAt(0);
                        }
                    }
                }
                cAProd = string.Join("#", spProd.ToArray());
                nAProd = string.Join("#", spProdn.ToArray());
            	*/

                cProd = DtaDet.prd.val;
                nProd = DtaDet.prd.txt;

                DtaDet.inp.txt = DtaDet.inp.txt.Replace(" ( ", "~").Replace(" ), ", "#");
                ArrayList spGft = new ArrayList(DtaDet.inp.val.Split(new[] { "#" }, StringSplitOptions.None));
                ArrayList spGftn = new ArrayList(DtaDet.inp.txt.Split(new[] { "#" }, StringSplitOptions.None));

                if (DtaDet.inp.val != "")
                {
                    cGft = spGft[0].ToString();
                    nGft = spGftn[0].ToString();

                    spGft.RemoveAt(0);
                    spGftn.RemoveAt(0);

                    string[] acGft = cGft.Split('~');
                    string[] anGft = nGft.Split('~');

                    sGfCd = acGft[0];
                    sGfQt = acGft[1];
                    sGfNm = anGft[0];
                }
                cAGft = string.Join("#", spGft.ToArray());
                nAGft = string.Join("#", spGftn.ToArray());
            }
            else
            {

                cAProd = DtaDet.prd.val;
                nAProd = DtaDet.prd.txt;
                cAGft = DtaDet.inp.val;
                nAGft = DtaDet.inp.txt;
            }
            if (nCus != null)
            {
                NCusID = Create_NewCus(nCus, EDt);
                NCust = "{\"oCd\":\"" + nCus.id + "\",\"nCd\":\"" + NCusID + "\",\"sf\":\"" + nCus.sf + "\",\"typ\":\"" + nCus.typ + "\",\"EDt\":\"" + EDt + "\"}";
                DtaDet.cus.val = NCusID;
            }

            DB_EReporting DL = new DB_EReporting();
            try
            {
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@ARCd", ARCd),
                    new SqlParameter("@SF", SFCode),
                    new SqlParameter("@vTyp", Typ),
                    new SqlParameter("@vCode", DtaDet.cus.val),
                    new SqlParameter("@CusNm", DtaDet.cus.txt),
                    new SqlParameter("@ses", DtaDet.ses.val),
                    new SqlParameter("@vTime", DtaDet.tm.val),
                    new SqlParameter("@POB", DtaDet.pob.val),
                    new SqlParameter("@WW", DtaDet.jw.val.Replace(",","$$")),
                    new SqlParameter("@cProd", cProd),
                    new SqlParameter("@nProd", nProd),
                    new SqlParameter("@GC", sGfCd),
                    new SqlParameter("@GN", sGfNm),
                    new SqlParameter("@GQ", sGfQt),
                    new SqlParameter("@cAProd",cAProd),
                    new SqlParameter("@nAProd",nAProd),
                    new SqlParameter("@cAGft",cAGft),
                    new SqlParameter("@nAGft",nAGft),
                    new SqlParameter("@TwnCd",DtaDet.twn.val),
                    new SqlParameter("@TwnNm",DtaDet.twn.txt),
                    new SqlParameter("@Rmks",DtaDet.rem),
                    new SqlParameter("@Rx",DtaDet.fedbk.val),
                    new SqlParameter("@DtOwner",DtaDet.sf.val),
                    new SqlParameter("@div",Div),
                    new SqlParameter("@Etyp",ETyp)
                    // new SqlParameter("@PrdSampleQtyValid",PrdSampleValid), 
                    //new SqlParameter("@InputQtyValid",inputQtyValid)
                };
                DL.Exec_NonQueryWithParam("svDCRDetTemp", CommandType.StoredProcedure, parameters);
                //DL.Exec_NonQueryWithParam("svDCRDetTemp_New", CommandType.StoredProcedure, parameters);
                return "{\"mslCd\":\"" + parameters[0].Value.ToString() + "\",\"cus\":" + NCust + "}";
            }
            catch { throw; }
            finally { DL = null; }
        }
        public string getDCRDetails_Trans(string SF, DateTime Dt)
        {
            string DCRJSON = "{";
            DB_EReporting DL = new DB_EReporting();
            try
            {
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@SF", SF),
                    new SqlParameter("@Dt", Dt)
                };
                DataTable tbHD = DL.Exec_DataTableWithParam("getDCRHead_Trans", CommandType.StoredProcedure, parameters);

                if (tbHD.Rows.Count > 0)
                {
                    DCRJSON += "\"Head\":{\"SFCode\":\"" + tbHD.Rows[0].Field<string>("SFCode") + "\",\"EDate\":\"" + tbHD.Rows[0].Field<string>("EDate") + "\",\"Rem\":\"" + tbHD.Rows[0].Field<string>("Rem") + "\",\"Wtyp\":\"" + tbHD.Rows[0].Field<decimal>("Wtyp") + "\"}";

                    if (tbHD.Rows[0].Field<string>("FieldWork_Indicator") == "F")
                    {
                        string[] DtaCap = new string[] { "Msl", "Chm", "Stk", "Udr", "Hos" };
                        for (int il = 0; il < DtaCap.Length; il++)
                            DCRJSON += ",\"" + DtaCap[il] + "\":[" + convert_JontWorkDataString((il + 1), SF, Dt) + "]";
                    }
                    //DCRJSON += ",";
                }
                DCRJSON += "}";
                return DCRJSON;


            }
            catch { throw; }
            finally
            {
                DL = null;
            }
        }

        private string convert_JontWorkDataString(int Typ, string SF, DateTime Dt)
        {
            DB_EReporting DL = new DB_EReporting();
            try
            {
                string sMsl = "";
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@SF", SF),
                    new SqlParameter("@Dt", Dt),
                    new SqlParameter("@Typ", Typ)
                };
                DataTable tbDet = DL.Exec_DataTableWithParam("getJointWork_DetailDatas", CommandType.StoredProcedure, parameters);
                var msls = (from w in tbDet.AsEnumerable() select w);
                foreach (var msl in msls)
                {
                    if (sMsl != "") sMsl += ",";
                    sMsl += "{\"sf\":{\"val\":\"" + msl.Field<string>("SF") + "\"},\"twn\":{\"val\":\"" + msl.Field<string>("SDP") + "\",\"txt\":\"" + msl.Field<string>("SDP_Name") + "\"}," +
                        "\"ses\":{\"val\":\"" + msl.Field<string>("Session") + "\",\"txt\":\"" + ((msl.Field<string>("Session") == "M") ? "Morning" : (msl.Field<string>("Session") == "E") ? "Evening" : "") + "\"}," +
                            "\"tm\":{\"val\":\"" + msl.Field<string>("tm") + "\",\"txt\":\"" + msl.Field<string>("tm") + "\"}," +
                            "\"cus\":{\"val\":\"" + msl.Field<decimal>("Trans_Detail_Info_Code") + "\",\"txt\":\"" + msl.Field<string>("Trans_Detail_Name") + "\"}," +
                            "\"pob\":{\"val\":\"" + msl.Field<double>("POB") + "\",\"txt\":\"" + msl.Field<double>("POB") + "\"}," +
                            "\"jw\":{\"val\":\"" + msl.Field<string>("Worked_with_Code") + "\",\"txt\":\"" + msl.Field<string>("Worked_with_Name") + "\"}," +
                            "\"prd\":{\"val\":\"" + msl.Field<string>("Product_Code") + "\",\"txt\":\"" + msl.Field<string>("Product_Detail") + "\"}," +
                            "\"inp\":{\"val\":\"" + msl.Field<string>("Gft") + "\",\"txt\":\"" + msl.Field<string>("gft_Detail") + "\"}," +
                            "\"rem\":\"" + msl.Field<string>("Activity_Remarks") + "\"," +
                            "\"fedbk\":{\"val\":\"" + msl.Field<string>("Rx") + "\",\"txt\":\"" + msl.Field<string>("Rxn") + "\"}}";
                }

                return sMsl;
            }
            catch { throw; }
            finally
            {
                DL = null;
            }
        }

        public DCREntry.TPBaseDCR get_TPBaseDCR(string SFCode, DateTime Dt)
        {
            DCREntry.TPBaseDCR SFDets = new DCREntry.TPBaseDCR();
            DB_EReporting DL = new DB_EReporting();
            try
            {
                //string dt = Dt.ToString("mm/");
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@SF_Code", SFCode),
                    new SqlParameter("@TPDate", Dt)
                };
                DataTable SFDet = DL.Exec_DataTableWithParam("spGetTPBaseDCR", CommandType.StoredProcedure, parameters);
                var SF = SFDet.AsEnumerable().FirstOrDefault();
                if (SFDet.Rows.Count > 0)
                {
                    SFDets.Territory_Code = SF.Field<string>("Terr_WorkType_Code");
                    SFDets.WorkType = SF.Field<string>("worktype");
                    SFDets.FWFlg = SF.Field<string>("FWFlg");
                    SFDets.SF_Code = SF.Field<string>("sf_code");//newly added
                }


            }
            catch { throw; }
            finally { DL = null; }
            return SFDets;
        }

        public List<DCREntry.Clusters> getClustFilter(string SFCode, DateTime Dt)
        {
            List<DCREntry.Clusters> Twns = new List<DCREntry.Clusters>();
            DB_EReporting DL = new DB_EReporting();
            try
            {
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@SF_Code", SFCode),
                    new SqlParameter("@TPDate", Dt)
                };
                DataTable WrkTy = DL.Exec_DataTableWithParam("GetClustFilter_Web", CommandType.StoredProcedure, parameters);
                var WTs = (from w in WrkTy.AsEnumerable() select w);
                foreach (var WT in WTs)
                {
                    DCREntry.Clusters nTwn = new DCREntry.Clusters();
                    nTwn.Code = WT.Field<decimal>("id");
                    nTwn.Name = WT.Field<string>("name");
                    //nTwn.PM_Visit = WT.Field<decimal>("PM_Visit");
                    //nTwn.CM_Visit = WT.Field<decimal>("CM_Visit");
                    nTwn.PM_Visit = Convert.ToDecimal((WT.Field<decimal?>("PM_Visit") != null) ? WT.Field<decimal?>("PM_Visit") : Convert.ToDecimal("0"));
                    nTwn.CM_Visit = Convert.ToDecimal((WT.Field<decimal?>("CM_Visit") != null) ? WT.Field<decimal?>("CM_Visit") : Convert.ToDecimal("0"));
                    nTwn.Territory_Cat = WT.Field<string>("Territory_Cat");
                    Twns.Add(nTwn);
                }
            }
            catch { throw; }
            finally { DL = null; }
            return Twns;
        }
        public string SaveDeviate(string Sf_Code, string Activity_Date, string Div_Code, string Dev_Reason, string TP_Area, string TP_Worktype, string SF_Name, int TPDCR_MGRAppr, string sTerritory_Code)
        {
            string ARCd = "";

            DB_EReporting DL = new DB_EReporting();
            try
            {
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@Sf_Code", Sf_Code),
                    new SqlParameter("@Activity_Date", Activity_Date),
                    new SqlParameter("@Div_Code", Div_Code),
                    new SqlParameter("@Dev_Reason", Dev_Reason),
                    new SqlParameter("@TP_Area", TP_Area),
                    new SqlParameter("@TP_Worktype", TP_Worktype),
                    new SqlParameter("@SF_Name", SF_Name),
                    new SqlParameter("@TPDCR_MGRAppr",TPDCR_MGRAppr),
                    new SqlParameter("@TP_Area_Code",sTerritory_Code)


                };
                DL.Exec_NonQueryWithParam("sp_InsertDeviate", CommandType.StoredProcedure, parameters);
                return parameters[0].Value.ToString();
            }
            catch { throw; }
            finally { DL = null; }
        }

        public Boolean Sample_Input_Update(string SFCode, string Code, int Mode)
        {
            DB_EReporting DL = new DB_EReporting();
            Boolean Flag = false;
            try
            {
                SqlParameter[] param = new SqlParameter[]
                            {
                                new SqlParameter("@SF", SFCode),
                                new SqlParameter("@Code", Code),
                                new SqlParameter("@Mode", Mode)
                            };
                DL.Exec_NonQueryWithParam("svSample_Input_Update", CommandType.StoredProcedure, param);
                Flag = true;
            }
            catch { throw; }
            finally { DL = null; }
            return Flag;
        }
    }
}