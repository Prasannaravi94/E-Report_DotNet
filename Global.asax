<%@ Application Language="C#" %>
<%@ import Namespace="System.Web.Routing" %>
<%@ Import Namespace="System.Web.Http" %>
<%@ Import Namespace="System.Net.Http" %>
<script runat="server">

    void Application_Start(object sender, EventArgs e)
    {
        // Code that runs on application startup
        RouteTable.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = System.Web.Http.RouteParameter.Optional }
        );

        GlobalConfiguration.Configuration.Formatters.JsonFormatter.MediaTypeMappings
            .Add(new System.Net.Http.Formatting.RequestHeaderMapping("Accept",
                              "text/html",
                              StringComparison.InvariantCultureIgnoreCase,
                              false,
                              "application/json"));

    }

    void Application_End(object sender, EventArgs e)
    {
        //  Code that runs on application shutdown

    }

    void Application_Error(object sender, EventArgs e)
    {
        // Code that runs when an unhandled error occurs

        string div_code = "";
        string sf_type = "";
        string sf_code = "";
        string sf_name = "";
        // string desig_name = "";
        string div_name = "";
        string Corporate = "";
        string Designation_Short_Name = "";
        string HO_ID = "";
        string Sub_HO_ID = "";


        string sf_hq = "";

        if (Session["div_code"] == null || Session["sf_type"] == null || Session["sf_code"] == null || Session["Sf_HQ"] == null || Session["sf_name"] == null || Session["Corporate"] == null || Session["Designation_Short_Name"] == null || Session["HO_ID"] == null || Session["division_code"] == null || Session["div_Name"] == null || Session["Sub_HO_ID"] == null)
        {
            try
            {
                div_code = Session["div_code"].ToString();
                sf_type = Session["sf_type"].ToString();
                sf_code = Session["sf_code"].ToString();
                div_code = Session["division_code"].ToString();
                sf_hq = Session["Sf_HQ"].ToString();
                sf_name = Session["sf_name"].ToString();
                div_name = Session["div_name"].ToString();
                Corporate = Session["Corporate"].ToString();
                Designation_Short_Name = Session["Designation_Short_Name"].ToString();
                HO_ID = Session["HO_ID"].ToString();
                Sub_HO_ID = Session["Sub_HO_ID"].ToString();
            }
            catch (Exception ex)
            {
//Response.Redirect("http://www.torssfa.info//Index.aspx");
                Response.Write(ex.Message);
            }
        }



    }

    void Session_Start(object sender, EventArgs e)
    {
        // Code that runs when a new session is started


    }

    void Session_End(object sender, EventArgs e)
    {
        // Code that runs when a session ends. 
        // Note: The Session_End event is raised only when the sessionstate mode
        // is set to InProc in the Web.config file. If session mode is set to StateServer 
        // or SQLServer, the event is not raised.



    }

</script>
