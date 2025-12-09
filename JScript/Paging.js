function PagingCommon(controlid) {
    var tblid = controlid + " tbody tr";
   // var rdbtn = controlid + " tbody tr td input[name=something]:radio";

    
    
    //  alert(tblid);
    // consider adding an id to your table,
    // just incase a second table ever enters the picture..?
    var items = $(tblid);
       
//    var totalRowCount = $("[id*=grdPreviewQuestion] tr").length;
//    var rowCount = $("[id*=grdPreviewQuestion] td").closest("tr").length;
     

    //alert(totalRowCount);
    var numItems = items.length;


    var perPage =4;

    // only show the first 2 (or "first per_page") items initially
    items.slice(perPage).hide(); //items.slice(perPage-1).hide();

    // now setup your pagination
    // you need that .pagination-page div before/after your table
    $("#UlPaging").pagination({
        items: numItems,
        itemsOnPage: perPage,
        cssStyle: "light-theme",
        onPageClick: function (pageNumber) { // this is where the magic happens
            // someone changed page, lets hide/show trs appropriately
            var showFrom = perPage * (pageNumber - 1);
            var showTo = showFrom + perPage;

            items.hide() // first hide everything, then show for the new page
                 .slice(showFrom, showTo).show();
            //.slice(showFrom+1, showTo).show();
        }
    });
}
