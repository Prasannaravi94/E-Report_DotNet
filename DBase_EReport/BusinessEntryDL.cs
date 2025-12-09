using Bus_Objects;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
/// <summary>
/// Summary description for DCREntryDL
/// </summary>
namespace DBase_EReport
{
    public class BusinessEntryDL
    {


        public BusinessEntry.SFDetails get_SFDetails(string SFCode)
        {
            BusinessEntry.SFDetails SFDets = new BusinessEntry.SFDetails();
            DB_EReporting DL = new DB_EReporting();
            try
            {
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@SF", SFCode)
                };
                //DataTable SFDet = DL.Exec_DataTableWithParam("spGetActvSFDetails", CommandType.StoredProcedure, parameters);
                DataTable SFDet = DL.Exec_DataTableWithParam("spGetActvSFDetails_BusinessEntry", CommandType.StoredProcedure, parameters);
                var SF = SFDet.AsEnumerable().FirstOrDefault();
                SFDets.SFCode = SF.Field<string>("SF_Code");
                SFDets.SFName = SF.Field<string>("SF_Name");
                SFDets.SFtype = SF.Field<int>("SF_Type");
                SFDets.State = SF.Field<byte>("State_Code");
                SFDets.Div = SF.Field<int>("OwnDiv");
                SFDets.Divs = SF.Field<string>("Division_Code");
                SFDets.sf_Hq = SF.Field<string>("Sf_HQ");
                //SFDets.sf_TP_Active_Dt = SF.Field<DateTime>("sf_TP_Active_Dt");

            }
            catch { throw; }
            finally { DL = null; }
            return SFDets;
        }

        public List<BusinessEntry.Clusters> getClusters(string SFCode, string div_code)
        {
            List<BusinessEntry.Clusters> Twns = new List<BusinessEntry.Clusters>();
            DB_EReporting DL = new DB_EReporting();
            try
            {
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@SF", SFCode),
                     new SqlParameter("@div_code", div_code)
                };
                DataTable WrkTy = DL.Exec_DataTableWithParam("sp_Get_Territory", CommandType.StoredProcedure, parameters);
                var WTs = (from w in WrkTy.AsEnumerable() select w);
                foreach (var WT in WTs)
                {
                    BusinessEntry.Clusters nTwn = new BusinessEntry.Clusters();
                    nTwn.Code = WT.Field<string>("Territory_Code");
                    nTwn.Name = WT.Field<string>("territory_Name");
                    Twns.Add(nTwn);
                }
            }
            catch { throw; }
            finally { DL = null; }
            return Twns;
        }
        public string getBusinessProdsJSON(string Div_Code, string State_Code)
        {
            DB_EReporting DL = new DB_EReporting();
            try
            {
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@divcode", Div_Code),
                    new SqlParameter("@st_code", State_Code)
                };
                DataTable Prods = Prods = DL.Exec_DataTableWithParam("sp_Get_Business_Product", CommandType.StoredProcedure, parameters);
                return JsonConvert.SerializeObject(Prods);
            }
            catch { throw; }
            finally { DL = null; }
        }

        public List<BusinessEntry.SFDetails> get_BaseSFs(string SFCode, string Div_Code)
        {
            List<BusinessEntry.SFDetails> BSFs = new List<BusinessEntry.SFDetails>();
            DB_EReporting DL = new DB_EReporting();
            try
            {
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@SF", SFCode),
                    new SqlParameter("@div_code", Div_Code),
                };
                DataTable SFDet = DL.Exec_DataTableWithParam("getBaseLvlSFs_Web_Modify", CommandType.StoredProcedure, parameters);
                var SFs = (from w in SFDet.AsEnumerable() select w);
                foreach (var SF in SFs)
                {
                    BusinessEntry.SFDetails nSF = new BusinessEntry.SFDetails();

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

        public string getDoctor_JSON(string SFCode)
        {
            DB_EReporting DL = new DB_EReporting();
            try
            {
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@SF", SFCode)
                };
                DataTable tbDrs = DL.Exec_DataTableWithParam("spGetDoctors_Web_Business_Entry", CommandType.StoredProcedure, parameters);
                return JsonConvert.SerializeObject(tbDrs);
            }
            catch { throw; }
            finally { DL = null; }
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
                DataTable tbTwn = DL.Exec_DataTableWithParam("spGetClusters_BusinessEntry", CommandType.StoredProcedure, parameters);
                return JsonConvert.SerializeObject(tbTwn);
            }
            catch { throw; }
            finally { DL = null; }
        }

        public string SaveBusinessEntry(string SFCode, int Type, int Div, BusinessEntry.DetailData DtaDet, string Trans_Month, string Trans_Year)
        {

            DB_EReporting DL = new DB_EReporting();
            try
            {
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@SF", SFCode),
                    new SqlParameter("@Div_Code", Div),
                    new SqlParameter("@Cus_Code", DtaDet.cus.val),
                    new SqlParameter("@Cus_Name", DtaDet.cus.txt),
                    new SqlParameter("@Prod_Detail",DtaDet.prd.val),
                    new SqlParameter("@Terri_Code",DtaDet.twn.val),
                    new SqlParameter("@Terri_Name",DtaDet.twn.txt),
                    new SqlParameter("@Total_Business_Value",DtaDet.B_Val.val),
                    new SqlParameter("@Trans_Month",Trans_Month),
                    new SqlParameter("@Trans_Year",Trans_Year),
                    new SqlParameter("@Speciality_Name",DtaDet.spec.val),
                    new SqlParameter("@Category_Name",DtaDet.cat.val)

                };

                DL.Exec_NonQueryWithParam("sv_Business_Entry_Details", CommandType.StoredProcedure, parameters);

                return "{\"mslCd\":\"" + parameters[0].Value.ToString() + "}";
            }
            catch { throw; }
            finally { DL = null; }
        }

        public string GetBusinessEntry_TerritoryWise(string SF, string Month, string Year, string Terri_code)
        {
            string DCRJSON = "{";
            DB_EReporting DL = new DB_EReporting();
            try
            {
                //SqlParameter[] parameters = new SqlParameter[]
                //{
                //    new SqlParameter("@SF", SF),
                //    new SqlParameter("@Month", Month),
                //    new SqlParameter("@Year", Year),
                //    new SqlParameter("@Terri_code", Terri_code)
                //};

                //DataTable tbHD = DL.Exec_DataTableWithParam("getDCRHead_Web_New", CommandType.StoredProcedure, parameters);

                DCRJSON += "\"Head\":{\"SFCode\":\"" + @SF + "\",\"Month\":\"" + Month + "\",\"Year\":\"" + Year + "\"}";
                //if (tbHD.Rows[0].Field<string>("FieldWork_Indicator") == "F")
                //{
                string[] DtaCap = new string[] { "Msl" };
                for (int il = 0; il < DtaCap.Length; il++)
                    DCRJSON += ",\"" + DtaCap[il] + "\":[" + convertDataString((il + 1), SF, Month, Year, Terri_code) + "]";
                //}
                DCRJSON += "}";
                return DCRJSON;
            }
            catch { throw; }
            finally
            {
                DL = null;
            }
        }


        private string convertDataString(int typ, string SF, string Month, string Year, string Terri_code)
        {
            DB_EReporting DL = new DB_EReporting();
            try
            {
                string sMsl = "";

                SqlParameter[] parameters = new SqlParameter[]
           {
                    new SqlParameter("@SF", SF),
                    new SqlParameter("@Month", Month),
                    new SqlParameter("@Year", Year),
                    new SqlParameter("@Terri_code", Terri_code)
           };

                //DataTable tbDet = DL.Exec_DataTableWithParam("getDetailDatas", CommandType.StoredProcedure, parameters);
                DataTable tbDet = DL.Exec_DataTableWithParam("get_BusinessEntry_Details_TerritoryWise", CommandType.StoredProcedure, parameters);

                var msls = (from w in tbDet.AsEnumerable() select w);
                foreach (var msl in msls)
                {
                    if (sMsl != "") sMsl += ",";
                    //sMsl += "{\"sf\":{\"val\":\"" + msl.Field<string>("SF") + "\"},\"twn\":{\"val\":\"" + msl.Field<string>("SDP") + "\",\"txt\":\"" + msl.Field<string>("SDP_Name").Replace("\t", " ").Replace("\n", " ") + "\"}," +
                    //    "\"ses\":{\"val\":\"" + msl.Field<string>("Session") + "\",\"txt\":\"" + ((msl.Field<string>("Session") == "M") ? "Morning" : (msl.Field<string>("Session") == "E") ? "Evening" : "") + "\"}," +
                    //        "\"tm\":{\"val\":\"" + msl.Field<string>("tm") + "\",\"txt\":\"" + msl.Field<string>("tm") + "\"}," +
                    //        "\"cus\":{\"val\":\"" + msl.Field<decimal>("Trans_Detail_Info_Code") + "\",\"txt\":\"" + msl.Field<string>("Trans_Detail_Name").Replace("\t", " ").Replace("\n", " ") + "\"}," +
                    //        "\"pob\":{\"val\":\"" + msl.Field<double>("POB") + "\",\"txt\":\"" + msl.Field<double>("POB") + "\"}," +
                    //        "\"jw\":{\"val\":\"" + msl.Field<string>("Worked_with_Code") + "\",\"txt\":\"" + msl.Field<string>("Worked_with_Name") + "\"}," +
                    //        "\"prd\":{\"val\":\"" + msl.Field<string>("Product_Code") + "\",\"txt\":\"" + msl.Field<string>("Product_Detail") + "\"}," +
                    //        "\"inp\":{\"val\":\"" + msl.Field<string>("Gft") + "\",\"txt\":\"" + msl.Field<string>("gft_Detail") + "\"}," +
                    //        "\"rem\":\"" + msl.Field<string>("Activity_Remarks") + "\"," +
                    //        "\"fedbk\":{\"val\":\"" + msl.Field<string>("Rx") + "\",\"txt\":\"" + msl.Field<string>("Rxn") + "\"}}";


                    sMsl += "{\"sf\":{\"val\":\"" + msl.Field<string>("sf_code") + "\"},\"twn\":{\"val\":\"" + msl.Field<string>("Territory_Code") + "\",\"txt\":\"" + msl.Field<string>("Territory_Name").Replace("\t", " ").Replace("\n", " ") + "\"}," +
                            "\"cus\":{\"val\":\"" + msl.Field<decimal>("ListedDrCode") + "\",\"txt\":\"" + msl.Field<string>("ListedDr_Name").Replace("\t", " ").Replace("\n", " ") + "\"}," +
                            "\"prd\":{\"val\":\"" + msl.Field<string>("ProductDetail") + "\",\"txt\":\"" + msl.Field<string>("ProductDetail_Name") + "\"}," +
                            "\"spec\":{\"val\":\"" + msl.Field<string>("Speciality_Name") + "\",\"txt\":\"" + msl.Field<string>("Speciality_Name") + "\"}," +
                            "\"cat\":{\"val\":\"" + msl.Field<string>("Category_Name") + "\",\"txt\":\"" + msl.Field<string>("Category_Name") + "\"}," +
                            "\"sub_area\":{\"val\":\"" + msl.Field<string>("Territory_Code") + "\",\"txt\":\"" + msl.Field<string>("Territory_Name").Replace("\t", " ").Replace("\n", " ") + "\"}," +
                            "\"B_Val\":{\"val\":\"" + msl.Field<double>("TotalValue") + "\",\"txt\":\"" + msl.Field<double>("TotalValue") + "\"}}";
                }

                return sMsl;
            }
            catch { throw; }
            finally
            {
                DL = null;
            }
        }

        public string Get_AllReport(string SF, string Month, string Year)
        {
            string SFCJSON = "[";
            DB_EReporting DL = new DB_EReporting();
            try
            {
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@SF", SF),
                    new SqlParameter("@Month", Month),
                    new SqlParameter("@Year", Year)
                };

                DataTable tbHD = DL.Exec_DataTableWithParam("get_BusinessEntry_Details_All", CommandType.StoredProcedure, parameters);
                if (tbHD.Rows.Count > 0)
                {

                    for (int j = 0; j < tbHD.Rows.Count; j++)
                    {
                        if (j > 0)
                        {
                            SFCJSON += ",";
                        }
                        SFCJSON += "{\"ListedDr_Name\":\"" + tbHD.Rows[j].Field<string>("ListedDr_Name").Replace("\t", " ").Replace("\n", " ") + "\",\"Product_Detail_Name\":\"" + tbHD.Rows[j].Field<string>("ProductDetail_Name").Replace("\t", " ").Replace("\n", " ") + "\",\"Speciality_Name\":\"" + tbHD.Rows[j].Field<string>("Speciality_Name") + "\",\"Category_Name\":\"" + tbHD.Rows[j].Field<string>("Category_Name") + "\",\"Territory_Name\":\"" + tbHD.Rows[j].Field<string>("Territory_Name") + "\",\"TotalValue\":\"" + tbHD.Rows[j].Field<double>("TotalValue") + "\"}";
                    }

                }

                SFCJSON += "]";
                return SFCJSON;

            }
            catch { throw; }
            finally
            {
                DL = null;
            }
        }
    }
}