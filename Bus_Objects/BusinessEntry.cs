using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
/// <summary>
/// Summary description for DCREntry
/// </summary>

namespace Bus_Objects
{
    public class BusinessEntry
    {
        public class SFDetails
        {
            public string SFCode { get; set; }
            public string SFName { get; set; }
            public int SFtype { get; set; }
            public byte State { get; set; }
            public int Div { get; set; }
            public string Divs { get; set; }
            public string ETyp { get; set; }
            public string SysIP { get; set; }
            public string sf_Hq { get; set; }
            //public DateTime sf_TP_Active_Dt { get; set; }

            //public string Place { get; set; }
            //public string Alias_Name { get; set; }

        }

        public class Worktypes
        {
            public int Code { get; set; }
            public string Name { get; set; }
            public string ETabs { get; set; }
            public string FWFlg { get; set; }
        }
        public class Clusters
        {
            public string Code { get; set; }
            public string Name { get; set; }
            //public string Terri_Cat { get; set; }
            //public string Alias_Name { get; set; }
            //public string Meeting_Place { get; set; }

        }
        public class DtDetsDCR
        {
            public DateTime DCR_Date { get; set; }
            public string DTRem { get; set; }
            public byte Type { get; set; }
            public DateTime CurrDt { get; set; }
            public DateTime STime { get; set; }
            public string DtMsg { get; set; }
            public string Indicator { get; set; }
            public string II_Indicator { get; set; }

        }

        public class DCRSetup
        {
            public byte TPBased { get; set; }
            public byte DCRAppr { get; set; }
            public string DlyNeed { get; set; }
            public byte DlyDays { get; set; }
            public string DlyHoli { get; set; }
            public byte HosNeed { get; set; }
            public byte StkNeed { get; set; }
            public byte UdrNeed { get; set; }
            public byte ProdMand { get; set; }
            public byte ProdSel { get; set; }
            public byte PQtyZro { get; set; }
            public string POBtype { get; set; }
            public byte TmNeed { get; set; }
            public byte TmMand { get; set; }
            public byte SesNeed { get; set; }
            public byte SesMand { get; set; }
            public byte NoOfDrs { get; set; }
            public byte NoOfChm { get; set; }
            public byte NoOfStk { get; set; }
            public byte NoOfUdr { get; set; }
            public byte NoOfHos { get; set; }
            public int RemLen { get; set; }
            public string HoliAuto { get; set; }
            public string WkOffAuto { get; set; }
            public byte DrRem { get; set; }
            public byte NChm { get; set; }
            public byte NUdr { get; set; }

            public byte DPOBM { get; set; }
            public byte CPOBM { get; set; }
            public byte DRxFed { get; set; }
            public byte DRxQty { get; set; }

            public string SDPCap { get; set; }

            public string SDCRE { get; set; }
            public string SDCRV { get; set; }

            public byte TotDlyNeed { get; set; }
            public int RDlylimit { get; set; }

            public byte MultiDr { get; set; }
            public byte ShowPatchOnly { get; set; }
            public byte DCRDrMand { get; set; }
            public string DCRJointWrk { get; set; }

            public byte TPBaseDCRDr { get; set; }
            public byte TPDCRDeviation { get; set; }
            public byte TPDCR_MGRAppr { get; set; }
            public byte No_of_TP_View { get; set; }
            public byte ChemPOBQty { get; set; }
            public string ProdQtyCaption { get; set; }
            public string ChemQtyCaption { get; set; }

            public byte PrdSampleQty { get; set; }
            public byte ChemSampleQty { get; set; }
            public byte InputMand { get; set; }
            public string DrSampleQtyCaption { get; set; }
            public string ChemSampleQtyCaption { get; set; }
            public byte DrPOBQtyZero { get; set; }
            public byte ChemSampleQtyZero { get; set; }
            public byte ChemPOBQtyZero { get; set; }
            public byte JointCalls_Needed { get; set; }
            public byte STP_basedDCR { get; set; }
            public byte DrWiseVisitRestrict { get; set; }

            public int ProdSampleQtyValid { get; set; }
            public int InputQtyValid { get; set; }
            public byte StayPlace { get; set; }

        }
        public class NewCus
        {
            public string id { get; set; }
            public string name { get; set; }
            public string addr { get; set; }
            public vals twn { get; set; }
            public vals Cat { get; set; }
            public vals Spc { get; set; }
            public vals Cla { get; set; }
            public vals Qua { get; set; }
            public string typ { get; set; }
            public string sf { get; set; }
        }
        public class vals
        {
            public string val { get; set; }
            public string txt { get; set; }

        }
        public class DetailData
        {
            [JsonProperty("ses")]
            public vals ses { get; set; }
            [JsonProperty("tm")]
            public vals tm { get; set; }
            [JsonProperty("cus")]
            public vals cus { get; set; }
            [JsonProperty("pob")]
            public vals pob { get; set; }
            [JsonProperty("jw")]
            public vals jw { get; set; }
            [JsonProperty("prd")]
            public vals prd { get; set; }
            [JsonProperty("inp")]
            public vals inp { get; set; }
            public string rem { get; set; }
            [JsonProperty("fedbk")]
            public vals fedbk { get; set; }
            [JsonProperty("twn")]
            public vals twn { get; set; }
            [JsonProperty("sf")]
            public vals sf { get; set; }
            [JsonProperty("DtlProd")]
            public vals DtlProd { get; set; }

            [JsonProperty("B_Val")]
            public vals B_Val { get; set; }
            [JsonProperty("spec")]
            public vals spec { get; set; }
            [JsonProperty("cat")]
            public vals cat { get; set; }

        }
        public class HeadData
        {
            public string SFCode { get; set; }
            public string SFTyp { get; set; }
            public DateTime EDate { get; set; }
            public int Wtyp { get; set; }
            public string TwnCd { get; set; }
            public string rem { get; set; }
            public string SysIP { get; set; }
            public DateTime STime { get; set; }
            public DateTime ETime { get; set; }
            public byte Div { get; set; }
            public byte DCRType { get; set; }
            public string ETyp { get; set; }
            public string FWFlg { get; set; }
            public string ETabs { get; set; }
        }
        public class DCRDatas : HeadData
        {
            [JsonProperty("Msl")]
            public List<DetailData> MslData { get; set; }
            [JsonProperty("Chm")]
            public List<DetailData> ChmData { get; set; }
            [JsonProperty("Stk")]
            public List<DetailData> StkData { get; set; }
            [JsonProperty("Hos")]
            public List<DetailData> HosData { get; set; }
            [JsonProperty("Udr")]
            public List<DetailData> UdrData { get; set; }

            public string Month { get; set; }
            public string Year { get; set; }
        }
        public class TPBaseDCR
        {
            public string SF_Code { get; set; }
            public string FWFlg { get; set; }
            public DateTime DCR_Date { get; set; }
            public string Territory_Code { get; set; }
            public string WorkType { get; set; }

        }

        public class STP_BaseDCR
        {
            public string SF_Code { get; set; }
            public string FWFlg { get; set; }
            public DateTime DCR_Date { get; set; }
            public string Territory_Code { get; set; }
            public string WorkType { get; set; }
            public string Dr_Code { get; set; }
            public string Chem_Code { get; set; }
            public string Others_Name { get; set; }
            public string Others_Code { get; set; }

        }
        public class STPName
        {
            public string Code { get; set; }
            public string Name { get; set; }

        }
        public class DCRRoute
        {
            public string Sf_codee { get; set; }
            public string Wrktype_Name { get; set; }
            public string Wrktype_Code { get; set; }
            public string Div_codee { get; set; }
            public string Dcr_datee { get; set; }
            public string Terri_Code { get; set; }
            public string Terri_name { get; set; }
            public string Stay_terri_Code { get; set; }
            public string Stay_terri_name { get; set; }
            public string MRCode { get; set; }
            public string Stay_MR_Code { get; set; }
            // public string DayStatus { get; set; }
            public string Worked_With_Name { get; set; }
            public string DrCount { get; set; }
            public string ChmCount { get; set; }
            public string Route_SlNo { get; set; }

        }


        public class MeetingPlace
        {
            public string id { get; set; }
            public string name { get; set; }
        }

    }

}