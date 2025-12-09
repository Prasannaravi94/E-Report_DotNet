//var selectedSubDivision=[],selectedBrandVal=[],selectedBrandText=[],selectedProductVal=[],selectedProductText=[],selectedSpecVal=[],selectedSpecText=[],selectedTherapVal=[],selectedTherapText=[],DD_SfCode="",DD_SfType="",DD_DivCode="",DD_View="",arrSubBrnd=[],arrSlidePriority=[],modePriority="",modePreview="",objUddlProd="",objUddlSpec="",objUddlThrp="",objVddlBrnd="",objVddlProd="",objVddlSpec="",objVddlThrp="",arrVUploadView=[],PriorityVal="";function loadUploader(){var e=new $("#uploader").pluploadQueue({runtimes:"html5,flash,silverlight,html4",url:"DD_Slide_Upload.ashx",max_file_size:"200mb",chunk_size:"1mb",filters:[{title:"Image files",extensions:"jpg,gif,png"},{title:"Zip files",extensions:"zip"},{title:"application/pdf",extensions:"pdf"},{title:"Video File",extensions:"mp4,ogv,avi,mov,flv,3gp"}],rename:!0,sortable:!0,dragdrop:!0,views:{list:!0,thumbs:!0,active:"thumbs"},flash_swf_url:"/plupload/js/Moxie.swf",silverlight_xap_url:"/plupload/js/Moxie.xap",preinit:{Init:function(e,d){},UploadFile:function(e,d){}},init:{PostInit:function(){document.getElementsByClassName("plupload_start").onclick=function(){return e.start(),!1}},Browse:function(e){},Refresh:function(e){},StateChanged:function(e){},QueueChanged:function(e){},OptionChanged:function(e,d,t,i){},BeforeUpload:function(e,d){return"Nothing selected"!=$("#ddlUProduct").next().attr("title")||"Nothing selected"!=$("#ddlUSpeciality").next().attr("title")||"Nothing selected"!=$("#ddlUTherapy").next().attr("title")||($.alert("Please Select atleast one Category!","Alert!"),$("#btnUView").trigger("click"),!1)},UploadProgress:function(e,d){Upload_Files(e,d)},FileFiltered:function(e,d){},FilesAdded:function(e,d){},FilesRemoved:function(e,d){},FileUploaded:function(e,d,t){},ChunkUploaded:function(e,d,t){},UploadComplete:function(e,d){$.alert("Upload Complete!","Alert!")||window.location.reload(),$("#btnUView").trigger("click")},Destroy:function(e){},Error:function(e,d){}}})}function uploaderRefresh(){$("#uploader").pluploadQueue("getUploader");$("#uploader").empty(),loadUploader()}function BindDivision_DDL(){$.ajax({type:"POST",contentType:"application/json; charset=utf-8",url:"../MR/webservice/EDetailingWebService.asmx/GetDivision",data:"{}",dataType:"json",success:function(e){DD_DivCode=$("#DD_DivCode").val(),"U"==DD_View?($("#ddlUDivision").empty(),$("#ddlUDivision").append("<option value='0'>---Select---</option>"),$.each(e.d,function(){$("#ddlUDivision").append($("<option/>").val(this.Division_Code).text(this.Division_Name))}),$("#ddlUDivision option[value="+DD_DivCode+"]").attr("selected","selected"),$("#ddlUDivision").selectpicker("refresh")):"V"==DD_View&&($("#ddlVDivision").empty(),$("#ddlVDivision").append("<option value='0'>---Select---</option>"),$.each(e.d,function(){$("#ddlVDivision").append($("<option/>").val(this.Division_Code).text(this.Division_Name))}),$("#ddlVDivision option[value="+DD_DivCode+"]").attr("selected","selected"),$("#ddlVDivision").selectpicker("refresh"))},error:function(e){}})}function BindSubDivision_DDL(){$.ajax({type:"POST",contentType:"application/json; charset=utf-8",url:"../MR/webservice/EDetailingWebService.asmx/GetSubDivision",data:"{}",dataType:"json",success:function(e){"U"==DD_View?($("#ddlUSubdivision").empty(),$.each(e.d,function(){$("#ddlUSubdivision").append($("<option/>").val(this.SubDivision_Code).text(this.SubDivision_Name))}),$("#ddlUSubdivision").selectpicker("refresh")):"V"==DD_View?($("#ddlVSubdivision").empty(),$("#ddlVSubdivision").append("<option value='0'>---Select---</option>"),$.each(e.d,function(){$("#ddlVSubdivision").append($("<option/>").val(this.SubDivision_Code).text(this.SubDivision_Name))}),$("#ddlVSubdivision").selectpicker("refresh")):"P"==DD_View?($("#ddlPSubdivision").empty(),$("#ddlPSubdivision").append("<option value='0'>---Select---</option>"),$.each(e.d,function(){$("#ddlPSubdivision").append($("<option/>").val(this.SubDivision_Code).text(this.SubDivision_Name))}),$("#ddlPSubdivision").selectpicker("refresh")):"Pre"==DD_View&&($("#ddlPreSubdivision").empty(),$("#ddlPreSubdivision").append("<option value='0'>---Select---</option>"),$.each(e.d,function(){$("#ddlPreSubdivision").append($("<option/>").val(this.SubDivision_Code).text(this.SubDivision_Name))}),$("#ddlPreSubdivision").selectpicker("refresh"))},error:function(e){}})}function BindBrand_DDL(){$.ajax({type:"POST",contentType:"application/json; charset=utf-8",url:"../MR/webservice/EDetailingWebService.asmx/GetBrand",data:"{}",dataType:"json",success:function(e){DD_DivCode=$("#DD_DivCode").val(),"U"==DD_View?($("#ddlUBrand").empty(),$.each(e.d,function(){0!=this.Brand_Code&&$("#ddlUBrand").append($("<option/>").val(this.Brand_Code).text(this.Brand_Name))}),$("#ddlUBrand").selectpicker("refresh")):"V"==DD_View&&($("#ddlVBrand").empty(),$("#ddlVBrand").append("<option value='0'>---Select---</option>"),$.each(e.d,function(){$("#ddlVBrand").append($("<option/>").val(this.Brand_Code).text(this.Brand_Name))}),$("#ddlVBrand").selectpicker("refresh"),objVddlBrnd=e.d)},error:function(e){}})}function Bind_GroupDetails(){if("U"==DD_View){DD_DivCode=$("#DD_DivCode").val();var e=$("#ddlUSubdivision option:selected");selectedSubDivision=[],$(e).each(function(e,d){selectedSubDivision.push([$(this).val()])});var d=$("#ddlUBrand option:selected");selectedBrands=[],$(d).each(function(e,d){selectedBrands.push([$(this).val()])}),(arrSubBrnd=[]).push(selectedSubDivision.join(",")),arrSubBrnd.push(selectedBrands.join(",")),Bind_ProductGroup(arrSubBrnd.join("^"))}else"V"==DD_View&&(DD_DivCode=$("#DD_DivCode").val(),(arrSubBrnd=[]).push($("#ddlVSubdivision").val()),arrSubBrnd.push($("#ddlVBrand").val()),Bind_ProductGroup(arrSubBrnd.join("^")))}function Bind_ProductGroup(e){$.ajax({type:"POST",contentType:"application/json; charset=utf-8",url:"../MR/webservice/EDetailingWebService.asmx/GetProductGroup",data:"{objSubBrnd:"+JSON.stringify(e)+"}",dataType:"json",success:function(e){if("U"==DD_View)$("#ddlUProduct").empty(),$("#ddlUProduct").append("<option value='0'  disabled='true'>---Select---</option>"),$.each(e.d,function(){$("#ddlUProduct").append($("<option/>").val(this.Product_Code).text(this.Product_Name))}),$("#ddlUProduct").selectpicker("refresh"),objUddlProd=e.d;else if("V"==DD_View)$("#ddlVProduct").empty(),$("#ddlVProduct").append("<option value='0'>---Select---</option>"),$.each(e.d,function(){$("#ddlVProduct").append($("<option/>").val(this.Product_Code).text(this.Product_Name))}),$("#ddlVProduct").selectpicker("val","0"),$("#ddlVProduct").attr("disabled","disabled"),$("#ddlVProduct").selectpicker("refresh"),objVddlProd=e.d;else{if("P"==DD_View)return $("#ddlPMode").empty(),$("#ddlPMode").append("<option value='0'>---Select---</option>"),$.each(e.d,function(){$("#ddlPMode").append($("<option/>").val(this.Product_Code).text(this.Product_Name))}),$("#ddlPMode").selectpicker("val","0"),void $("#ddlPMode").selectpicker("refresh");if("Pre"==DD_View)return $("#ddlPreMode").empty(),$("#ddlPreMode").append("<option value='0'>---Select---</option>"),$.each(e.d,function(){$("#ddlPreMode").append($("<option/>").val(this.Product_Code).text(this.Product_Name))}),$("#ddlPreMode").selectpicker("val","0"),void $("#ddlPreMode").selectpicker("refresh")}Bind_SpecGroup()},error:function(e){}})}function Bind_SpecGroup(){$.ajax({type:"POST",contentType:"application/json; charset=utf-8",url:"../MR/webservice/EDetailingWebService.asmx/GetSpecGroup",data:"{}",dataType:"json",success:function(e){if("U"==DD_View)$("#ddlUSpeciality").selectpicker(),$("#ddlUSpeciality").empty(),$("#ddlUSpeciality").append("<option value='0'  disabled='true'>---Select---</option>"),$.each(e.d,function(){$("#ddlUSpeciality").append($("<option/>").val(this.Spec_Code).text(this.Spec_Name))}),$("#ddlUSpeciality").selectpicker("refresh"),objUddlSpec=e.d;else if("V"==DD_View)$("#ddlVSpeciality").selectpicker(),$("#ddlVSpeciality").empty(),$("#ddlVSpeciality").append("<option value='0'>---Select---</option>"),$.each(e.d,function(){$("#ddlVSpeciality").append($("<option/>").val(this.Spec_Code).text(this.Spec_Name))}),$("#ddlVSpeciality").selectpicker("val","0"),$("#ddlVSpeciality").attr("disabled","disabled"),$("#ddlVSpeciality").selectpicker("refresh"),objVddlSpec=e.d;else{if("P"==DD_View)return $("#ddlPMode").selectpicker(),$("#ddlPMode").empty(),$("#ddlPMode").append("<option value='0'>---Select---</option>"),$.each(e.d,function(){$("#ddlPMode").append($("<option/>").val(this.Spec_Code).text(this.Spec_Name))}),$("#ddlPMode").selectpicker("val","0"),void $("#ddlPMode").selectpicker("refresh");if("Pre"==DD_View)return $("#ddlPreMode").selectpicker(),$("#ddlPreMode").empty(),$("#ddlPreMode").append("<option value='0'>---Select---</option>"),$.each(e.d,function(){$("#ddlPreMode").append($("<option/>").val(this.Spec_Code).text(this.Spec_Name))}),$("#ddlPreMode").selectpicker("val","0"),void $("#ddlPreMode").selectpicker("refresh")}Bind_TherapyGroup()},error:function(e){}})}function Bind_TherapyGroup(){$.ajax({type:"POST",contentType:"application/json; charset=utf-8",url:"../MR/webservice/EDetailingWebService.asmx/GetTherapyGroup",data:"{}",dataType:"json",success:function(e){if("U"==DD_View)$("#ddlUTherapy").empty(),$.each(e.d,function(){$("#ddlUTherapy").append($("<option/>").val(this.Therapy_Code).text(this.Therapy_Name))}),$('#ddlUTherapy option[value="0"]').prop("disabled",!0),$("#ddlUTherapy").selectpicker("refresh"),objUddlThrp=e.d,$("#uploader").empty(),loadUploader();else if("V"==DD_View){$("#ddlVTherapy").empty(),$.each(e.d,function(){$("#ddlVTherapy").append($("<option/>").val(this.Therapy_Code).text(this.Therapy_Name))});$('#ddlVTherapy option[value="0"]');$("#ddlVTherapy").selectpicker("val","0"),$("#ddlVTherapy").attr("disabled","disabled"),$("#ddlVTherapy").selectpicker("refresh"),objVddlThrp=e.d,DD_DivCode=$("#DD_DivCode").val();var d=$("#ddlVSubdivision").val(),t=$("#ddlVBrand").val();(arrSubBrnd=[]).push(d),arrSubBrnd.push(t),arrSubBrnd.push(""),arrSubBrnd.push(""),arrSubBrnd.push(""),Get_Upload_View(arrSubBrnd.join("^"))}else{if("P"==DD_View){$("#ddlPMode").empty(),$.each(e.d,function(){$("#ddlPMode").append($("<option/>").val(this.Therapy_Code).text(this.Therapy_Name))});$('#ddlPMode option[value="0"]');return void $("#ddlPMode").selectpicker("refresh")}if("Pre"==DD_View){$("#ddlPreMode").empty(),$.each(e.d,function(){$("#ddlPreMode").append($("<option/>").val(this.Therapy_Code).text(this.Therapy_Name))});$('#ddlPreMode option[value="0"]');return void $("#ddlPreMode").selectpicker("refresh")}}},error:function(e){}})}function Upload_Files(e,d){var t=$("#ddlUDivision option:selected").val(),i=$("#ddlUSubdivision option:selected");selectedSubDivision=[],$(i).each(function(e,d){selectedSubDivision.push([$(this).val()])});var r=$("#ddlUBrand option:selected");selectedBrandVal=[],selectedBrandText=[],$(r).each(function(e,d){selectedBrandVal.push([$(this).val()]),selectedBrandText.push([$(this).text()])});var l=$("#ddlUProduct option:selected");selectedProductVal=[],selectedProductText=[],$(l).each(function(e,d){selectedProductVal.push([$(this).val()]),selectedProductText.push([$(this).text()])});var o=$("#ddlUSpeciality option:selected");selectedSpecVal=[],selectedSpecText=[],$(o).each(function(e,d){selectedSpecVal.push([$(this).val()]),selectedSpecText.push([$(this).text()])});var a=$("#ddlUTherapy option:selected");selectedTherapVal=[],selectedTherapText=[],$(a).each(function(e,d){selectedTherapVal.push([$(this).val()]),selectedTherapText.push([$(this).text()])});var n=[];n.push(selectedProductVal.join(",")),n.push(selectedProductText.join(",")),n.push(selectedSpecVal.join(",")),n.push(selectedSpecText.join(",")),n.push(selectedTherapVal.join(",")),n.push(selectedTherapText.join(",")),n.push(d.name),n.push(t),n.push(selectedSubDivision.join(",")),n.push(selectedBrandVal.join(",")),n.push(selectedBrandText.join(",")),$.ajax({type:"POST",contentType:"application/json; charset=utf-8",url:"../MR/webservice/EDetailingWebService.asmx/Upload_Files",data:"{objUpload_Files:"+JSON.stringify(n.join("^"))+"}",dataType:"json",success:function(e){},error:function(e){}})}function Get_Upload_View(e){$.ajax({type:"POST",contentType:"application/json; charset=utf-8",url:"../MR/webservice/EDetailingWebService.asmx/GetUploadView",data:"{objSubBrnd:"+JSON.stringify(e)+"}",dataType:"json",success:function(e){if($.trim(e.d)){$("#grdUpload").empty(),$("#grdUpload").append("<thead><tr style='background-color:White;'><th scope='col'>S.No</th><th class='grdHeader' scope='col'>ImgS.No</th><th class='grdHeader' scope='col'>ImgSrc</th><th scope='col'>Image</th><th scope='col'>Brand</th><th class='grdHeader' scope='col'>Product</th><th scope='col'>Speciality</th><th scope='col'>Therapy</th><th scope='col'>Edit</th><th scope='col'><div class='custom-control custom-checkbox'><input type='checkbox' class='custom-control-input' id='chkDelete_All'><label class='custom-control-label' for='chkDelete_All'>Delete</label></div>&nbsp;<a class='deleteAll' id='btnDelete_All' title='Delete All' href='#' data-toggle='tooltip'><i class=\"fa fa-trash\"></i></a></th><th scope='col'><div class='custom-control custom-checkbox'><input type='checkbox' class='custom-control-input' id='chkCommon_All'><label class='custom-control-label' for='chkCommon_All'>Common</label></div></th><tbody>");for(var d=0;d<e.d.length;d++){var t=d+1;$("#grdUpload").append("<tr><td><span id='lblSNo_"+d+"'>"+t+"</span></td> <td class='grdHeader'><span id='imgSI_NO_"+d+"'>"+e.d[d].SI_NO+"</span></td>  <td class='grdHeader'><span id='imgName_"+d+"'>"+e.d[d].Img_Name+"</span></td> <td><img id='imgSrc_"+d+"'  src='"+e.d[d].Img_Src+"' title='"+e.d[d].Img_Name+"' style='height:120px;width:150px;'></td> <td><select id='ddlGBrands' data-selected-text-format='count>1' disabled='disabled' class='selectpicker form-control ddlGBrands_"+d+"' data-live-search='true' multiple=''></select></td><td class='grdHeader'><select id='ddlGProduct' data-selected-text-format='count>1' class='selectpicker form-control ddlGProduct_"+d+"' data-live-search='true' multiple=''></select></td><td><select id='ddlGSpeciality' data-selected-text-format='count>1' disabled='disabled' class='selectpicker form-control ddlGSpeciality_"+d+"' data-live-search='true' multiple=''></select></td> <td><select id='ddlGTherapy' data-selected-text-format='count>1' disabled='disabled' class='selectpicker form-control ddlGTherapy_"+d+"' data-live-search='true' multiple=''></select></td> <td style=text-align:center;><a class='edit' title='Edit' data-toggle='modal' data-target='#myModal' href='#myModal'><i class=\"fa fa-pencil\"></i></a></td><td style=text-align:center;><a class='delete' id='btnDelete_"+d+"' title='Delete' href='#' data-toggle='tooltip'><i class=\"fa fa-trash\"></i></a>&nbsp;<div class='custom-control custom-checkbox'><input type='checkbox' class='custom-control-input' id='chkDelete_"+d+"'><label class='custom-control-label' for='chkDelete_"+d+"'></label></div></td> <td style=text-align:center;><div class='custom-control custom-checkbox'><input type='checkbox' class='custom-control-input' id='chkCommon_"+d+"'><label class='custom-control-label' for='chkCommon_"+d+"'></label></div></td></tr> "),BindBrand(d)}$("#grdUpload").append("</tbody>"),$("#grdUpload").DataTable({destroy:!0,paging:!1,ordering:!1,info:!1,searching:!1});for(d=0;d<e.d.length;d++){var i,r,l,o,a,n="",s="",c="",p="";n=e.d[""+d].Product_Brand_Code,s=e.d[""+d].Product_Detail_Code,c=e.d[""+d].Doc_Special_Code,p=e.d[""+d].Product_Grp_Code,a=e.d[""+d].File_type,","==n[n.length-1]&&(n=n.slice(0,-1)),","==s[s.length-1]&&(s=s.slice(0,-1)),","==c[c.length-1]&&(c=c.slice(0,-1)),","==p[p.length-1]&&(p=p.slice(0,-1)),i=$.map(n.split(","),function(e){return parseInt(e,10)}),r=$.map(s.split(","),function(e){return parseInt(e,10)}),l=$.map(c.split(","),function(e){return parseInt(e,10)}),o=$.map(p.split(","),function(e){return parseInt(e,10)}),i&&($(".ddlGBrands_"+d).selectpicker(),$(".ddlGBrands_"+d).selectpicker("val",i)),r&&($(".ddlGProduct_"+d).selectpicker(),$(".ddlGProduct_"+d).selectpicker("val",r)),l&&($(".ddlGSpeciality_"+d).selectpicker(),$(".ddlGSpeciality_"+d).selectpicker("val",l)),o&&($(".ddlGTherapy_"+d).selectpicker(),$(".ddlGTherapy_"+d).selectpicker("val",o)),"C"==a?$("#chkCommon_"+d).prop("checked",!0):$("#chkCommon_"+d).prop("checked",!1)}$(".Common").show(),$("#grdUpload").show()}else $(".vCategory").hide(),$(".Common").hide(),$("#grdUpload").empty(),$("#grdUpload").hide(),$.alert("No Records Found!","Alert!")},failure:function(e){},error:function(e){}})}function BindBrand(e){$(".ddlGBrands_"+e).empty(),$.each(objVddlBrnd,function(){$(".ddlGBrands_"+e).append($("<option/>").val(this.Brand_Code).text(this.Brand_Name))}),$(".ddlGBrands_"+e).selectpicker("refresh"),BindProduct(e)}function BindProduct(e){$(".ddlGProduct_"+e).empty(),$(".ddlGProduct_"+e).append("<option value='0'  disabled='true'>---Select---</option>"),$.each(objVddlProd,function(){$(".ddlGProduct_"+e).append($("<option/>").val(this.Product_Code).text(this.Product_Name))}),$(".ddlGProduct_"+e).selectpicker("refresh"),BindSpeciality(e)}function BindSpeciality(e){$(".ddlGSpeciality_"+e).empty(),$(".ddlGSpeciality_"+e).append("<option value='0'  disabled='true'>---Select---</option>"),$.each(objVddlSpec,function(){$(".ddlGSpeciality_"+e).append($("<option/>").val(this.Spec_Code).text(this.Spec_Name))}),$(".ddlGSpeciality_"+e).selectpicker("refresh"),BindTherapy(e)}function BindTherapy(e){$(".ddlGTherapy_"+e).empty(),$.each(objVddlThrp,function(){$(".ddlGTherapy_"+e).append($("<option/>").val(this.Therapy_Code).text(this.Therapy_Name))}),$(".ddlGTherapy_"+e+' option[value="0"]').prop("disabled",!0),$(".ddlGTherapy_"+e).selectpicker("refresh")}function BindBrandPriority(e){$.ajax({type:"POST",contentType:"application/json; charset=utf-8",url:"../MR/webservice/EDetailingWebService.asmx/GetPriority",data:"{objmodePriority:"+JSON.stringify(e)+"}",dataType:"json",success:function(e){if($.trim(e.d)){$("#grdPriority").empty(),$("#grdPriority").append("<thead><tr style='background-color:White;'><th scope='col'>S.No</th><th class='grdHeader' scope='col'>Brand Code</th><th scope='col'>Name</th><th scope='col'>Slide Priority</th><th scope='col'>Priority</th><tbody>");for(var d=0;d<e.d.length;d++){var t=d+1;$("#grdPriority").append("<tr><td><span id='lblSNo_"+d+"'>"+t+"</span></td><td class='grdHeader'><span id='lblBrandCode_"+d+"'>"+e.d[d].Product_Brd_Code+"</span></td> <td><span id='lblBrandName_"+d+"'>"+e.d[d].Product_Brd_Name+"</span></td><td align='center'><a class='spView' title='View Slides' href='#'>View &nbsp; <i class=\"fa fa-eye\"></i></a></td><td><select id='ddlMasPriority' class='form-control ddlMasPriority"+d+"'></select></td></tr> ")}$("#grdPriority").append("</tbody>");var i=$("#grdPriority tr").length;$("#grdPriority > tbody  > tr").each(function(){var e=$(this).find("select:eq(0)");e.empty();for(var d=1;d<i;d++)e.append($("<option/>").val(d).text(d));e.selectpicker(),e.selectpicker("val",$(this).index()+1)}),$("#grdPriority").DataTable({destroy:!0,paging:!1,ordering:!1,info:!1,searching:!1})}else $("#grdPriority").empty(),$(".grdPriority").hide(),$.alert("No Records Found!","Alert!")},failure:function(e){},error:function(e){}})}function BindBrandPreview(e){$.ajax({type:"POST",contentType:"application/json; charset=utf-8",url:"../MR/webservice/EDetailingWebService.asmx/GetPriority",data:"{objmodePriority:"+JSON.stringify(e)+"}",dataType:"json",success:function(e){if($.trim(e.d)){$("#grdPreview").empty(),$("#grdPreview").append("<thead><tr style='background-color:White;'><th scope='col'>S.No</th><th class='grdHeader' scope='col'>Brand Code</th><th scope='col'>Name</th><th scope='col'>Preview</th><tbody>");for(var d=0;d<e.d.length;d++){var t=d+1;$("#grdPreview").append("<tr><td><span id='lblSNo_"+d+"'>"+t+"</span></td> <td class='grdHeader'><span id='lblBrandCode_"+d+"'>"+e.d[d].Product_Brd_Code+"</span></td> <td><span id='lblBrandName_"+d+"'>"+e.d[d].Product_Brd_Name+"</span></td><td align='center'><a class='sPreView' title='Preview' href='#'>Preview &nbsp; <i class=\"fa fa-eye\"></i></a></td></tr> ")}$("#grdPreview").append("</tbody>"),$("#grdPreview").DataTable({destroy:!0,paging:!1,ordering:!1,info:!1,searching:!1})}else $("#grdPreview").empty(),$(".grdPreview").hide(),$.alert("No Records Found!","Alert!")},failure:function(e){},error:function(e){}})}$(document).ready(function(){$(document).ajaxStart(function(){$("#loader").css("display","block")}).ajaxStop(function(){$("#loader").css("display","none")}),$("#divUpload").hide(),$("#divPriority").hide(),$("#divPreview").hide(),$("#uploader").hide(),$("#divView").show(),$(".uCategory").hide(),$(".vCategory").hide(),$(".Common").hide(),$("#grdUpload").hide(),DD_SfCode=$("#DD_SfCode").val(),DD_SfType=$("#DD_SfType").val(),DD_DivCode=$("#DD_DivCode").val(),DD_View="",$("#btnUpload").click(function(e){$("#ddlUDivision").empty(),$("#ddlUSubdivision").empty(),$("#ddlUSubdivision").selectpicker("refresh"),$("#ddlUBrand").empty(),$("#ddlUBrand").selectpicker("refresh"),$("#ddlUProduct").empty(),$("#ddlUProduct").selectpicker("refresh"),$("#ddlUSpeciality").empty(),$("#ddlUSpeciality").selectpicker("refresh"),$("#ddlUTherapy").empty(),$("#ddlUTherapy").selectpicker("refresh"),$(".uCategory").hide(),$("#uploader").empty(),$("#uploader").hide(),DD_View="U",BindDivision_DDL(),BindSubDivision_DDL(),BindBrand_DDL(),$("#divUpload").show(),$("#divView").hide(),$("#divPriority").hide(),$("#divPreview").hide(),$(this).addClass("active"),$("#btnView").removeClass("active"),$("#btnPriority").removeClass("active"),$("#btnPreview").removeClass("active"),$(this).hasClass("btn-default")&&($(this).addClass("btn-primary").removeClass("btn-default"),$("#btnView").addClass("btn-default").removeClass("btn-primary"),$("#btnPriority").addClass("btn-default").removeClass("btn-primary"),$("#btnPreview").addClass("btn-default").removeClass("btn-primary")),e.preventDefault()}),$("#btnView").click(function(e){$("#ddlVDivision").empty(),$("#ddlVSubdivision").empty(),$("#ddlVBrand").empty(),$("#ddlVProduct").empty(),$("#ddlVProduct").selectpicker("refresh"),$("#ddlVSpeciality").empty(),$("#ddlVSpeciality").selectpicker("refresh"),$("#ddlVTherapy").empty(),$("#ddlVTherapy").selectpicker("refresh"),$(".vCategory").hide(),$(".Common").hide(),$("#grdUpload").hide(),DD_View="V",BindDivision_DDL(),BindSubDivision_DDL(),BindBrand_DDL(),$("#grdUpload").empty(),$("#divPriority").hide(),$("#divPreview").hide(),$("#divUpload").hide(),$("#divView").show(),$(this).addClass("active"),$("#btnUpload").removeClass("active"),$("#btnPriority").removeClass("active"),$("#btnPreview").removeClass("active"),$(this).hasClass("btn-default")&&($(this).addClass("btn-primary").removeClass("btn-default"),$("#btnUpload").addClass("btn-default").removeClass("btn-primary"),$("#btnPriority").addClass("btn-default").removeClass("btn-primary"),$("#btnPreview").addClass("btn-default").removeClass("btn-primary")),e.preventDefault()}),$("#btnPriority").click(function(e){DD_View="P",BindSubDivision_DDL(),$("#lblPSubdivision").show(),$("#ddlPSubdivision").selectpicker("show"),$("#lblPMode").hide(),$("#ddlPMode").empty(),$("#ddlPMode").selectpicker("hide"),$(".prdPriority").show(),$("#divUpload").hide(),$("#divView").hide(),$("#divPriority").show(),$("#divPreview").hide(),$(this).addClass("active"),$("#btnView").removeClass("active"),$("#btnUpload").removeClass("active"),$("#btnPreview").removeClass("active"),$(this).hasClass("btn-default")&&($(this).addClass("btn-primary").removeClass("btn-default"),$("#btnView").addClass("btn-default").removeClass("btn-primary"),$("#btnUpload").addClass("btn-default").removeClass("btn-primary"),$("#btnPreview").addClass("btn-default").removeClass("btn-primary")),$("#rdBrandP").prop("checked",!0),$("#grdPriority").empty(),$(".grdPriority").hide(),e.preventDefault()}),$("#btnPreview").click(function(e){DD_View="Pre",BindSubDivision_DDL(),$("#lblPreSubdivision").show(),$("#ddlPreSubdivision").selectpicker("show"),$("#lblPreMode").hide(),$("#ddlPreMode").empty(),$("#ddlPreMode").selectpicker("hide"),$(".prdPreview").show(),$("#divUpload").hide(),$("#divView").hide(),$("#divPriority").hide(),$("#divPreview").show(),$(this).addClass("active"),$("#btnView").removeClass("active"),$("#btnUpload").removeClass("active"),$("#btnPriority").removeClass("active"),$(this).hasClass("btn-default")&&($(this).addClass("btn-primary").removeClass("btn-default"),$("#btnView").addClass("btn-default").removeClass("btn-primary"),$("#btnUpload").addClass("btn-default").removeClass("btn-primary"),$("#btnPriority").addClass("btn-default").removeClass("btn-primary")),$("#rdBrandPre").prop("checked",!0),$("#grdPreview").empty(),$(".grdPreview").hide(),e.preventDefault()}),$("#btnView").is(".btn-primary")&&(DD_View="V",BindDivision_DDL(),BindSubDivision_DDL(),BindBrand_DDL()),$("#btnUView").click(function(e){DD_View="U",$(document).ajaxStart(function(){$("#loader").css("display","block")}).ajaxStop(function(){$("#loader").css("display","none")}),!0===Validation()&&(Bind_GroupDetails(),$("#uploader").show(),$(".uCategory").show()),e.preventDefault()}),$("#btnVView").click(function(e){DD_View="V",$(document).ajaxStart(function(){$("#loader").css("display","block")}).ajaxStop(function(){$("#loader").css("display","none")}),!0===Validation()&&(Bind_GroupDetails(),$(".vCategory").show()),e.preventDefault()}),$("#btnPView").click(function(e){if(DD_View="P",$(document).ajaxStart(function(){$("#loader").css("display","block")}).ajaxStop(function(){$("#loader").css("display","none")}),!0===Validation()){if($("#rdBrandP").prop("checked")){var d=$("#ddlPSubdivision option:selected").val(),t="";modePriority="0",(arrSubBrnd=[]).push(d),arrSubBrnd.push(t),arrSubBrnd.push(modePriority),BindBrandPriority(arrSubBrnd.join("^"))}else if($("#rdProductP").prop("checked")){d=$("#ddlPSubdivision option:selected").val(),t=$("#ddlPMode option:selected").val();modePriority="1",(arrSubBrnd=[]).push(d),arrSubBrnd.push(t),arrSubBrnd.push(modePriority),BindBrandPriority(arrSubBrnd.join("^"))}else if($("#rdSpecialityP").prop("checked")){d=$("#ddlPSubdivision option:selected").val(),t=$("#ddlPMode option:selected").val();modePriority="2",(arrSubBrnd=[]).push(d),arrSubBrnd.push(t),arrSubBrnd.push(modePriority),BindBrandPriority(arrSubBrnd.join("^"))}else if($("#rdTherapyP").prop("checked")){d=$("#ddlPSubdivision option:selected").val(),t=$("#ddlPMode option:selected").val();modePriority="3",(arrSubBrnd=[]).push(d),arrSubBrnd.push(t),arrSubBrnd.push(modePriority),BindBrandPriority(arrSubBrnd.join("^"))}$(".grdPriority").show()}e.preventDefault()}),$("#btnPreViewGo").click(function(e){if(DD_View="Pre",$(document).ajaxStart(function(){$("#loader").css("display","block")}).ajaxStop(function(){$("#loader").css("display","none")}),!0===Validation()){if($("#rdBrandPre").prop("checked")){var d=$("#ddlPreSubdivision option:selected").val();modePriority="0",(arrSubBrnd=[]).push(d),arrSubBrnd.push(""),arrSubBrnd.push(modePriority),BindBrandPreview(arrSubBrnd.join("^"))}else if($("#rdProductPre").prop("checked")){d=$("#ddlPreSubdivision option:selected").val();var t=$("#ddlPreMode option:selected").val();modePreview="1",(arrSubBrnd=[]).push(d),arrSubBrnd.push(t),arrSubBrnd.push(modePreview),BindBrandPreview(arrSubBrnd.join("^"))}else if($("#rdSpecialityPre").prop("checked")){d=$("#ddlPreSubdivision option:selected").val(),t=$("#ddlPreMode option:selected").val();modePreview="2",(arrSubBrnd=[]).push(d),arrSubBrnd.push(t),arrSubBrnd.push(modePreview),BindBrandPreview(arrSubBrnd.join("^"))}else if($("#rdTherapyPre").prop("checked")){d=$("#ddlPreSubdivision option:selected").val(),t=$("#ddlPreMode option:selected").val();modePreview="3",(arrSubBrnd=[]).push(d),arrSubBrnd.push(t),arrSubBrnd.push(modePreview),BindBrandPreview(arrSubBrnd.join("^"))}$(".grdPreview").show()}e.preventDefault()})}),$(document).on("click","#chkCommon_All",function(e){var d=$(e.target).closest("table");$("td input:checkbox[id*=chkCommon]",d).prop("checked",this.checked)}),$(document).on("click","#chkDelete_All",function(e){var d=$(e.target).closest("table");$("td input:checkbox[id*=chkDelete]",d).prop("checked",this.checked)}),$(document).on("click",".edit",function(){var e=$(this).closest("tr"),d=(e.find("span:eq(0)"),e.find("span:eq(1)")),t=e.find("span:eq(2)"),i=e.find("img:eq(0)"),r=e.find("select:eq(0)"),l=e.find("select:eq(1)"),o=e.find("select:eq(2)"),a=e.find("select:eq(3)"),n=e.find("input[type=checkbox]:eq(1)").attr("id"),s=$('<form role="form" name="modalForm" action="" method="post" enctype="multipart/form-data"></form>'),c=$('<div class="form-group"></div>');c.append('<center><div class="row"><div class="col-md-12 col-sm-12"><img id="imgMSrc" src='+i.attr("src")+" title="+i.attr("title")+' style="height: 220px; width: 250px;"></div></div></center><br />'),c.append('<div class="row"><div class="col-md-3 col-sm-3"><span id="lblMPBrand">Brand :</span></div><div class="col-md-9 col-sm-9"><select id="ddlMBrand" class="selectpicker form-control ddlMBrand" disabled="disabled" data-live-search="true" multiple=""></select></div></div><br />'),c.append('<div class="row"><div class="col-md-3 col-sm-3"><span id="lblMProduct">Product :</span></div><div class="col-md-9 col-sm-9"><select id="ddlMProduct" class="selectpicker form-control ddlMProduct" data-live-search="true" multiple=""></select></div></div><br />'),c.append('<div class="row"><div class="col-md-3 col-sm-3"><span id="lblMSpeciality">Speciality :</span></div><div class="col-md-9 col-sm-9"><select id="ddlMSpeciality" class="selectpicker form-control ddlMSpeciality" data-live-search="true" multiple=""></select></div></div><br />'),c.append('<div class="row"><div class="col-md-3 col-sm-3"><span id="lblMTherapy">Therapy :</span></div><div class="col-md-9 col-sm-9"><select id="ddlMTherapy" class="selectpicker form-control ddlMTherapy" data-live-search="true" multiple=""></select></div></div><br />'),c.append('<div class="row"><div class="col-md-3 col-sm-3"><span id="lblMImage">Replace :</span></div><div class="col-md-9 col-sm-9"><input type="file" name="file" id="mFile" class="form-control"></div></div><br />'),c.append('<div class="row"><div class="col-md-3 col-sm-3"><span id="lblMCommon">Common :</span></div><div class="col-md-9 col-sm-9"><div class="custom-control custom-checkbox"><input type="checkbox" class="custom-control-input" id="chkMCommon"><label class="custom-control-label" for="chkMCommon"></label></div></div></div><br />'),c.append('<span id="lblMImgName" class="grdHeader">'+t.text()+'</span><span id="lblMImgSlNo" class="grdHeader">'+d.text()+"</span>"),s.append(c),$(".modal-body").html(c),$("#ddlMBrand").empty(),$.each(objVddlBrnd,function(){$("#ddlMBrand").append($("<option/>").val(this.Brand_Code).text(this.Brand_Name))}),$("#ddlMBrand").selectpicker("refresh"),$("#ddlMProduct").empty(),$("#ddlMProduct").append("<option value='0'  disabled='true'>---Select---</option>"),$.each(objVddlProd,function(){$("#ddlMProduct").append($("<option/>").val(this.Product_Code).text(this.Product_Name))}),$("#ddlMProduct").selectpicker("refresh"),$("#ddlMSpeciality").empty(),$("#ddlMSpeciality").append("<option value='0'  disabled='true'>---Select---</option>"),$.each(objVddlSpec,function(){$("#ddlMSpeciality").append($("<option/>").val(this.Spec_Code).text(this.Spec_Name))}),$("#ddlMSpeciality").selectpicker("refresh"),$("#ddlMTherapy").empty(),$.each(objVddlThrp,function(){$("#ddlMTherapy").append($("<option/>").val(this.Therapy_Code).text(this.Therapy_Name))}),$('#ddlMTherapy option[value="0"]').prop("disabled",!0),$("#ddlMTherapy").selectpicker("refresh");var p=$("#"+r.attr("id")+" option:selected"),u=[];$(p).each(function(e,d){u.push([$(this).val()])});var h=$("#"+l.attr("id")+" option:selected"),v=[];$(h).each(function(e,d){v.push([$(this).val()])});var P=$("#"+o.attr("id")+" option:selected"),b=[];$(P).each(function(e,d){b.push([$(this).val()])});var y=$("#"+a.attr("id")+" option:selected"),f=[];$(y).each(function(e,d){f.push([$(this).val()])}),$("#ddlMBrand").selectpicker(),$("#ddlMBrand").selectpicker("val",u),$("#ddlMProduct").selectpicker(),$("#ddlMProduct").selectpicker("val",v),$("#ddlMSpeciality").selectpicker(),$("#ddlMSpeciality").selectpicker("val",b),$("#ddlMTherapy").selectpicker(),$("#ddlMTherapy").selectpicker("val",f),$("#"+n).prop("checked")?$("#chkMCommon").prop("checked",!0):$("#chkMCommon").prop("checked",!1)}),$(document).on("click",".modal-footer .btn-primary",function(e){e.preventDefault();var d=document.getElementById("mFile"),t="",i=$("#lblMImgSlNo").text();if(0==d.files.length)t=$("#lblMImgName").text();else{t=document.getElementById("mFile").files[0].name;var r=new FormData;r.append("file",$("#mFile")[0].files[0]),$.ajax({url:"DD_Slide_Upload.ashx",type:"POST",data:r,processData:!1,contentType:!1,success:function(e){}})}var l=$("#ddlVDivision option:selected").val(),o=$("#ddlVSubdivision option:selected").val(),a=$("#ddlVBrand option:selected").val(),n=$("#ddlVBrand option:selected").text(),s=$("#ddlMBrand option:selected");selectedBrandVal=[],$(s).each(function(e,d){selectedBrandVal.push([$(this).val()])});var c=$("#ddlMProduct option:selected");selectedProductVal=[],selectedProductText=[],$(c).each(function(e,d){selectedProductVal.push([$(this).val()]),selectedProductText.push([$(this).text()])});var p=$("#ddlMSpeciality option:selected");selectedSpecVal=[],selectedSpecText=[],$(p).each(function(e,d){selectedSpecVal.push([$(this).val()]),selectedSpecText.push([$(this).text()])});var u=$("#ddlMTherapy option:selected");selectedTherapVal=[],selectedTherapText=[],$(u).each(function(e,d){selectedTherapVal.push([$(this).val()]),selectedTherapText.push([$(this).text()])});var h=$("#chkMCommon").prop("checked"),v=[];v.push(selectedProductVal.join(",")),v.push(selectedProductText.join(",")),v.push(selectedSpecVal.join(",")),v.push(selectedSpecText.join(",")),v.push(selectedTherapVal.join(",")),v.push(selectedTherapText.join(",")),v.push(t),v.push(l),v.push(o),v.push(a),v.push(n),v.push(i),v.push(h);$.ajax({type:"POST",contentType:"application/json; charset=utf-8",url:"../MR/webservice/EDetailingWebService.asmx/Update_Files",data:"{objUpload_Files:"+JSON.stringify(v.join("^"))+"}",dataType:"json",success:function(e){$.alert("Update Success!","Alert!")&&($("#myModal").modal("toggle"),$("#btnVView").trigger("click"))},error:function(e){}})}),$(document).on("click",".delete",function(e){var d=$(this).closest("tr"),t=d.find("span:eq(1)");d.find("span:eq(2)");$.confirm({title:"Confirm!",content:"Are you sure? You want to delete?",buttons:{confirm:function(){$.ajax({type:"POST",contentType:"application/json; charset=utf-8",url:"../MR/webservice/EDetailingWebService.asmx/Delete_Files",data:"{objSl_No:"+JSON.stringify(t.text())+"}",dataType:"json",success:function(e){$.alert("Record Deleted!","Alert!")&&$("#btnVView").trigger("click")},error:function(e){}})},cancel:function(){$.alert("Canceled!")}}})}),$(document).on("click",".spView",function(e){e.preventDefault();var d="",t="",i="",r="",l="",o="",a=$(this).closest("tr"),n=a.find("span:eq(1)"),s=a.find("span:eq(2)");$("#btnPriority").is(".btn-primary")&&($("#rdProductP").prop("checked")?(d=$("#ddlPSubdivision option:selected").val(),t=$("#ddlPSubdivision option:selected").text(),i=$("#ddlPMode option:selected").val(),o=$("#ddlPMode option:selected").text(),modePriority="1"):$("#rdSpecialityP").prop("checked")?(d=$("#ddlPSubdivision option:selected").val(),t=$("#ddlPSubdivision option:selected").text(),r=$("#ddlPMode option:selected").val(),o=$("#ddlPMode option:selected").text(),modePriority="2"):$("#rdTherapyP").prop("checked")?(d=$("#ddlPSubdivision option:selected").val(),t=$("#ddlPSubdivision option:selected").text(),l=$("#ddlPMode option:selected").val(),o=$("#ddlPMode option:selected").text(),modePriority="3"):$("#rdBrandP").prop("checked")&&(d=$("#ddlPSubdivision option:selected").val(),t=$("#ddlPSubdivision option:selected").text(),modePriority="0"),(arrSlidePriority=[]).push(d),arrSlidePriority.push(n.text()),arrSlidePriority.push(i),arrSlidePriority.push(r),arrSlidePriority.push(l),arrSlidePriority.push(modePriority),arrSlidePriority.push(t),arrSlidePriority.push(o),arrSlidePriority.push(s.text()),showModalPopUp(arrSlidePriority.join(",")))}),$("#ddlUSubdivision").change(function(){$("#ddlUProduct").empty(),$("#ddlUProduct").selectpicker("refresh"),$(".uCategory").hide()}),$("#ddlUBrand").change(function(e){$("#ddlUProduct").empty(),$("#ddlUProduct").selectpicker("refresh"),$(".uCategory").hide()}),$("#ddlVSubdivision").change(function(e){$("#ddlVProduct").empty(),$("#ddlVProduct").selectpicker("refresh"),$(".vCategory").hide(),$(".Common").hide(),$("#grdUpload").hide()}),$("#ddlVBrand").change(function(e){$("#ddlVProduct").empty(),$("#ddlVProduct").selectpicker("refresh"),$(".vCategory").hide(),$(".Common").hide(),$("#grdUpload").hide()}),$("#ddlVProduct").change(function(e){DD_DivCode=$("#DD_DivCode").val();var d=$("#ddlVSubdivision").val(),t=$("#ddlVBrand").val(),i=$("#ddlVProduct").val();(arrSubBrnd=[]).push(d),arrSubBrnd.push(t),arrSubBrnd.push(i),arrSubBrnd.push(""),arrSubBrnd.push(""),Get_Upload_View(arrSubBrnd.join("^"))}),$("#ddlVSpeciality").change(function(e){DD_DivCode=$("#DD_DivCode").val();var d=$("#ddlVSubdivision").val(),t=$("#ddlVBrand").val(),i=$("#ddlVSpeciality").val();(arrSubBrnd=[]).push(d),arrSubBrnd.push(t),arrSubBrnd.push(""),arrSubBrnd.push(i),arrSubBrnd.push(""),Get_Upload_View(arrSubBrnd.join("^"))}),$("#ddlVTherapy").change(function(e){DD_DivCode=$("#DD_DivCode").val();var d=$("#ddlVSubdivision").val(),t=$("#ddlVBrand").val(),i=$("#ddlVTherapy").val();(arrSubBrnd=[]).push(d),arrSubBrnd.push(t),arrSubBrnd.push(""),arrSubBrnd.push(""),arrSubBrnd.push(i),Get_Upload_View(arrSubBrnd.join("^"))}),$("#ddlPSubdivision").change(function(e){$("#grdPriority").empty(),$(".grdPriority").hide();var d=$(this).val();$("#rdProductP").prop("checked")&&("0"==d?($("#ddlPMode").empty(),$("#ddlPMode").selectpicker("refresh")):(DD_View="P",DD_DivCode=$("#DD_DivCode").val(),(arrSubBrnd=[]).push($(this).val()),arrSubBrnd.push(""),Bind_ProductGroup(arrSubBrnd.join("^"))))}),$("#ddlPMode").change(function(e){$("#grdPriority").empty(),$(".grdPriority").hide()}),$("#ddlPreSubdivision").change(function(e){$("#grdPreview").empty(),$(".grdPreview").hide();var d=$(this).val();$("#rdProductPre").prop("checked")&&("0"==d?($("#ddlPreMode").empty(),$("#ddlPreMode").selectpicker("refresh")):(DD_View="Pre",DD_DivCode=$("#DD_DivCode").val(),(arrSubBrnd=[]).push($(this).val()),arrSubBrnd.push(""),Bind_ProductGroup(arrSubBrnd.join("^"))))}),$("#ddlPreMode").change(function(e){$("#grdPreview").empty(),$(".grdPreview").hide()}),$("input[type=radio][name=filter]").change(function(){if(0==$(this).val()){$("#ddlVProduct").attr("disabled","disabled"),$("#ddlVProduct").selectpicker("refresh"),$("#ddlVSpeciality").attr("disabled","disabled"),$("#ddlVSpeciality").selectpicker("refresh"),$("#ddlVTherapy").attr("disabled","disabled"),$("#ddlVTherapy").selectpicker("refresh"),DD_DivCode=$("#DD_DivCode").val();var e=$("#ddlVSubdivision").val(),d=$("#ddlVBrand").val();(arrSubBrnd=[]).push(e),arrSubBrnd.push(d),arrSubBrnd.push(""),arrSubBrnd.push(""),arrSubBrnd.push(""),Get_Upload_View(arrSubBrnd.join("^"))}else 1==$(this).val()?($("#ddlVProduct").removeAttr("disabled"),$("#ddlVProduct").selectpicker("refresh"),$("#ddlVSpeciality").attr("disabled","disabled"),$("#ddlVSpeciality").selectpicker("refresh"),$("#ddlVTherapy").attr("disabled","disabled"),$("#ddlVTherapy").selectpicker("refresh")):2==$(this).val()?($("#ddlVProduct").attr("disabled","disabled"),$("#ddlVProduct").selectpicker("refresh"),$("#ddlVSpeciality").removeAttr("disabled"),$("#ddlVSpeciality").selectpicker("refresh"),$("#ddlVTherapy").attr("disabled","disabled"),$("#ddlVTherapy").selectpicker("refresh")):3==$(this).val()&&($("#ddlVProduct").attr("disabled","disabled"),$("#ddlVProduct").selectpicker("refresh"),$("#ddlVSpeciality").attr("disabled","disabled"),$("#ddlVSpeciality").selectpicker("refresh"),$("#ddlVTherapy").removeAttr("disabled"),$("#ddlVTherapy").selectpicker("refresh"))}),$("input[type=radio][name=Priority]").change(function(){modePriority=$(this).val(),0==$(this).val()?(DD_View="P",BindSubDivision_DDL(),$("#lblPSubdivision").show(),$("#ddlPSubdivision").selectpicker("show"),$("#lblPMode").hide(),$("#ddlPMode").empty(),$("#ddlPMode").selectpicker("hide"),$(".prdPriority").show(),$("#grdPriority").empty(),$(".grdPriority").hide()):1==$(this).val()?(DD_View="P",BindSubDivision_DDL(),$("#lblPSubdivision").show(),$("#ddlPSubdivision").selectpicker("show"),$("#lblPMode").empty(),$("#lblPMode").text("Product"),$("#lblPMode").show(),$("#ddlPMode").empty(),$("#ddlPMode").selectpicker("show"),$("#ddlPMode").selectpicker("refresh"),$(".prdPriority").show(),$("#grdPriority").empty(),$(".grdPriority").hide()):2==$(this).val()?(DD_View="P",BindSubDivision_DDL(),Bind_SpecGroup(),$("#lblPSubdivision").show(),$("#ddlPSubdivision").selectpicker("show"),$("#lblPMode").empty(),$("#lblPMode").text("Speciality"),$("#ddlPMode").selectpicker("show"),$(".prdPriority").show(),$("#grdPriority").empty(),$(".grdPriority").hide()):3==$(this).val()&&(DD_View="P",BindSubDivision_DDL(),Bind_TherapyGroup(),$("#lblPSubdivision").show(),$("#ddlPSubdivision").selectpicker("show"),$("#lblPMode").empty(),$("#lblPMode").text("Therapy"),$("#ddlPMode").selectpicker("show"),$(".prdPriority").show(),$("#grdPriority").empty(),$(".grdPriority").hide())}),$("input[type=radio][name=Preview]").change(function(){modePreview=$(this).val(),0==$(this).val()?(DD_View="Pre",BindSubDivision_DDL(),$("#lblPreSubdivision").show(),$("#ddlPreSubdivision").selectpicker("show"),$("#lblPreMode").hide(),$("#ddlPreMode").empty(),$("#ddlPreMode").selectpicker("hide"),$(".prdPreview").show(),$("#grdPreview").empty(),$(".grdPreview").hide()):1==$(this).val()?(DD_View="Pre",BindSubDivision_DDL(),$("#lblPreSubdivision").show(),$("#ddlPreSubdivision").selectpicker("show"),$("#lblPreMode").empty(),$("#lblPreMode").text("Product"),$("#lblPreMode").show(),$("#ddlPreMode").empty(),$("#ddlPreMode").selectpicker("show"),$("#ddlPreMode").selectpicker("refresh"),$(".prdPreview").show(),$("#grdPreview").empty(),$(".grdPreview").hide()):2==$(this).val()?(DD_View="Pre",BindSubDivision_DDL(),Bind_SpecGroup(),$("#lblPreSubdivision").show(),$("#ddlPreSubdivision").selectpicker("show"),$("#lblPreMode").empty(),$("#lblPreMode").text("Speciality"),$("#ddlPreMode").selectpicker("show"),$(".prdPreview").show(),$("#grdPreview").empty(),$(".grdPreview").hide()):3==$(this).val()&&(DD_View="Pre",BindSubDivision_DDL(),Bind_TherapyGroup(),$("#lblPreSubdivision").show(),$("#ddlPreSubdivision").selectpicker("show"),$("#lblPreMode").empty(),$("#lblPreMode").text("Therapy"),$("#ddlPreMode").selectpicker("show"),$(".prdPreview").show(),$("#grdPreview").empty(),$(".grdPreview").hide())}),$(document).on("change","#ddlMasPriority",function(){var e=$(this).parents("tr:first"),d=e.find("select:eq(0)"),t=[];for(i=1;i<=$("#grdPriority > tbody > tr").length;i++)t.push(i);var r=[];r=$("#grdPriority > tbody > tr").map(function(){return $(this).find("select:eq(0)").val()}).get(),r=$.unique(r.sort());var l={},o=[];for(let e of r)l[e]=!0;for(let e of t)l[e]||o.push(e);$("#grdPriority > tbody > tr").each(function(){var t=$(this),i=t.find("select:eq(0)");d.val()==i.val()&&e.index()!=t.index()&&(i.selectpicker(),i.selectpicker("val",o[0]))})}),$(document).on("click","#btnDelete_All",function(e){e.preventDefault();var d=[];d=$("#grdUpload > tbody > tr").map(function(){var e=$(this),d="";return e.find("input[type=checkbox]:eq(0)").prop("checked")&&(d=e.find("span:eq(1)").text()),d}).get(),$.ajax({type:"POST",contentType:"application/json; charset=utf-8",url:"../MR/webservice/EDetailingWebService.asmx/Delete_Files",data:"{objSl_No:"+JSON.stringify(d.join(","))+"}",dataType:"json",success:function(e){$.alert("Records Deleted!","Alert!")&&$("#btnVView").trigger("click")},error:function(e){}})}),$(".Common").click(function(e){e.preventDefault();var d=[];d=$("#grdUpload > tbody > tr").map(function(){var e=$(this);return e.find("span:eq(1)").text()+"^"+e.find("input[type=checkbox]:eq(1)").prop("checked")}).get(),$.ajax({type:"POST",contentType:"application/json; charset=utf-8",url:"../MR/webservice/EDetailingWebService.asmx/Update_Common",data:"{objImg_Common:"+JSON.stringify(d.join(","))+"}",dataType:"json",success:function(e){$.alert("Update Success!","Alert!")},error:function(e){}})}),$(".MasPriority").click(function(e){e.preventDefault();var d="",t="";$("#rdProductP").prop("checked")?(d=$("#ddlPSubdivision option:selected").val(),t=$("#ddlPMode option:selected").val(),modePriority="1"):$("#rdSpecialityP").prop("checked")?(d=$("#ddlPSubdivision option:selected").val(),t=$("#ddlPMode option:selected").val(),modePriority="2"):$("#rdTherapyP").prop("checked")?(d=$("#ddlPSubdivision option:selected").val(),t=$("#ddlPMode option:selected").val(),modePriority="3"):$("#rdBrandP").prop("checked")&&(d=$("#ddlPSubdivision option:selected").val(),modePriority="0");var i=[];i=$("#grdPriority > tbody > tr").map(function(){var e=$(this);return e.find("span:eq(1)").text()+"^"+e.find("select:eq(0)").val()+"^"+modePriority+"^"+d+"^"+t}).get(),$.ajax({type:"POST",contentType:"application/json; charset=utf-8",url:"../MR/webservice/EDetailingWebService.asmx/Update_Priority",data:"{objPriority:"+JSON.stringify(i.join(","))+"}",dataType:"json",success:function(e){$.alert("Update Success!","Alert!")},error:function(e){}})});var popUpObj,fixHelperModified=function(e,d){var t=d.children(),i=d.clone();return i.children().each(function(e){$(this).width(t.eq(e).width())}),i},updateIndex=function(e,d){$("td.index",d.item.parent()).each(function(e){$(this).html(e+1)})};function Validation(){if("U"==DD_View)return"Nothing selected"==$("#ddlUSubdivision").next().attr("title")?($.alert("Please Select Sub-Division!","Alert!"),!1):"Nothing selected"!=$("#ddlUBrand").next().attr("title")||($.alert("Please Select Brand!","Alert!"),!1);if("V"==DD_View)return 0==$("#ddlVSubdivision").val()?($.alert("Please Select Sub-Division!","Alert!"),!1):0!=$("#ddlVBrand").val()||($.alert("Please Select Brand!","Alert!"),!1);if("P"==DD_View){if($("#rdBrandP").prop("checked"))return 0!=$("#ddlPSubdivision").val()||($.alert("Please Select Sub-Division!","Alert!"),!1);if($("#rdProductP").prop("checked"))return 0==$("#ddlPSubdivision").val()?($.alert("Please Select Sub-Division!","Alert!"),!1):0!=$("#ddlPMode").val()&&"Nothing selected"!=$("#ddlPMode").next().attr("title")||($.alert("Please Select Product!","Alert!"),!1);if($("#rdSpecialityP").prop("checked"))return 0!=$("#ddlPMode").val()||($.alert("Please Select Speciality!","Alert!"),!1);if($("#rdTherapyP").prop("checked"))return 0!=$("#ddlPMode").val()||($.alert("Please Select Therapy!","Alert!"),!1)}else if("Pre"==DD_View){if($("#rdProductPre").prop("checked"))return 0==$("#ddlPreSubdivision").val()?($.alert("Please Select Sub-Division!","Alert!"),!1):0!=$("#ddlPreMode").val()||($.alert("Please Select Product!","Alert!"),!1);if($("#rdSpecialityPre").prop("checked"))return 0!=$("#ddlPreMode").val()||($.alert("Please Select Speciality!","Alert!"),!1);if($("#rdTherapyPre").prop("checked"))return 0!=$("#ddlPreMode").val()||($.alert("Please Select Therapy!","Alert!"),!1)}}function ValidationUpload(){return"Nothing selected"==$("#ddlUProduct").next().attr("title")?($.alert("Please Select Product!","Alert!"),!1):"Nothing selected"==$("#ddlUSpeciality").next().attr("title")?($.alert("Please Select Speciality!","Alert!"),!1):"Nothing selected"!=$("#ddlUTherapy").next().attr("title")||($.alert("Please Select Therapy!","Alert!"),!1)}function showModalPopUp(e){(popUpObj=window.open("DD_Slide_Priority.aspx?objPriority="+e,"ModalPopUp","toolbar=no,scrollbars=yes,location=no,statusbar=no,menubar=no,addressbar=no,resizable=yes,width=800,height=600,left = 0,top=0")).focus(),$(popUpObj.document.body).ready(function(){$(document).ajaxStart(function(){$("#loader").css("display","block")}).ajaxStop(function(){$("#loader").css("display","none")})})}

var selectedSubDivision = [],
    selectedBrandVal = [],
    selectedBrandText = [],
    selectedProductVal = [],
    selectedProductText = [],
    selectedSpecVal = [],
    selectedSpecText = [],
    selectedTherapVal = [],
    selectedTherapText = [],
    DD_SfCode = "",
    DD_SfType = "",
    DD_DivCode = "",
    DD_View = "",
    arrSubBrnd = [],
    arrSlidePriority = [],
    modePriority = "",
    modePreview = "",
    objUddlProd = "",
    objUddlSpec = "",
    objUddlThrp = "",
    objVddlBrnd = "",
    objVddlProd = "",
    objVddlSpec = "",
    objVddlThrp = "",
    arrVUploadView = [],
    PriorityVal = "";

function loadUploader() {
    var e = new $("#uploader").pluploadQueue({
        runtimes: "html5,flash,silverlight,html4",
        url: "DD_Slide_Upload.ashx",
        max_file_size: "200mb",
        chunk_size: "1mb",
        filters: [{
            title: "Image files",
            extensions: "jpg,gif,png"
        }, {
            title: "Zip files",
            extensions: "zip"
        }, {
            title: "application/pdf",
            extensions: "pdf"
        }, {
            title: "Video File",
            extensions: "mp4,ogv,avi,mov,flv,3gp"
        }],
        rename: !0,
        sortable: !0,
        dragdrop: !0,
        views: {
            list: !0,
            thumbs: !0,
            active: "thumbs"
        },
        flash_swf_url: "/plupload/js/Moxie.swf",
        silverlight_xap_url: "/plupload/js/Moxie.xap",
        preinit: {
            Init: function (e, d) { },
            UploadFile: function (e, d) { }
        },
        init: {
            PostInit: function () {
                document.getElementsByClassName("plupload_start").onclick = function () {
                    return e.start(), !1
                }
            },
            Browse: function (e) { return "Nothing selected" == $("#ddlUProduct").next().attr("title") ? ($.alert("Please Select Product", "Alert!"), !1) : "Nothing selected" == $("#ddlUSpeciality").next().attr("title") ? ($.alert("Please Select Speciality", "Alert!"), !1) : "Nothing selected" == $("#ddlUTherapy").next().attr("title") ? ($.alert("Please Select Therapy", "Alert!"), !1) : $("#ddlUTherapy").next().attr("title") },
            Refresh: function (e) { },
            StateChanged: function (e) { },
            QueueChanged: function (e) { },
            OptionChanged: function (e, d, t, i) { },
            BeforeUpload: function (e, d) {
                return "Nothing selected" != $("#ddlUProduct").next().attr("title") || "Nothing selected" != $("#ddlUSpeciality").next().attr("title") || "Nothing selected" != $("#ddlUTherapy").next().attr("title") || ($.alert("Please Select atleast one Category!", "Alert!"), $("#btnUView").trigger("click"), !1)
            },
            UploadProgress: function (e, d) {
                Upload_Files(e, d)
            },
            FileFiltered: function (e, d) {
                var arr = [];
                for (i = 0; i < e.files.length; i++) {
                    arr.push({ fName: e.files[i].name })
                }
                for (j = 0; j < e.files.length; j++) {
                    filteredNames = arr.filter(function (idx) {
                        return idx.fName == e.files[j].name
                    });
                    if (filteredNames.length > 1) { $.alert("(" + e.files[j].name + ") :File name should difference", "File Name!"), e.removeFile(e.files[j]) }
                }
            },
            FilesAdded: function (e, d) {
                //var isFile = 0;
                //plupload.each(d, function (file) {
                //    //log('  File:', file.name);
                //    var pattern = /^(.*[a-zA-Z])(?=.*[0-9])[a-zA-Z0-9]+$/;
                //    var filename = file.name.split('.')[0];
                //    if (!pattern.test(filename)) {
                //        isFile += 1;
                //        e.removeFile(file)
                //    }
                //}); 
                //if (isFile != 0) { ($.alert("File name should be letter and number only", "Alert!"), !1); }
                Upload_FilesExist(e, d)
            },
            FilesRemoved: function (e, d) { },
            FileUploaded: function (e, d, t) { },
            ChunkUploaded: function (e, d, t) { },
            UploadComplete: function (e, d) {
                $.alert("Upload Complete!", "Alert!") || window.location.reload(), $("#btnUView").trigger("click"), BindSubDivision_DDL(), $("#ddlUBrand").empty(), $("#ddlUBrand").selectpicker("refresh"), $(".uCategory").hide()//BindBrand_DDL(),
            },
            Destroy: function (e) { },
            Error: function (e, d) { }
        }
    })
}

function uploaderRefresh() {
    $("#uploader").pluploadQueue("getUploader");
    $("#uploader").empty(), loadUploader()
}

function BindDivision_DDL() {
    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        url: "../MR/webservice/EDetailingWebService.asmx/GetDivision",
        data: "{}",
        dataType: "json",
        success: function (e) {
            DD_DivCode = $("#DD_DivCode").val(), "U" == DD_View ? ($("#ddlUDivision").empty(), $("#ddlUDivision").append("<option value='0'>---Select---</option>"), $.each(e.d, function () {
                $("#ddlUDivision").append($("<option/>").val(this.Division_Code).text(this.Division_Name))
            }), $("#ddlUDivision option[value=" + DD_DivCode + "]").attr("selected", "selected"), $("#ddlUDivision").selectpicker("refresh")) : "V" == DD_View && ($("#ddlVDivision").empty(), $("#ddlVDivision").append("<option value='0'>---Select---</option>"), $.each(e.d, function () {
                $("#ddlVDivision").append($("<option/>").val(this.Division_Code).text(this.Division_Name))
            }), $("#ddlVDivision option[value=" + DD_DivCode + "]").attr("selected", "selected"), $("#ddlVDivision").selectpicker("refresh"))
        },
        error: function (e) { }
    })
}

function BindSubDivision_DDL() {
    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        url: "../MR/webservice/EDetailingWebService.asmx/GetSubDivision",
        data: "{}",
        dataType: "json",
        success: function (e) {
            "U" == DD_View ? ($("#ddlUSubdivision").empty(), $("#ddlUSubdivision").append("<option value='0'>---Select---</option>"), $.each(e.d, function () {
                $("#ddlUSubdivision").append($("<option/>").val(this.SubDivision_Code).text(this.SubDivision_Name))
            }), $("#ddlUSubdivision").selectpicker("refresh")) : "V" == DD_View ? ($("#ddlVSubdivision").empty(), $("#ddlVSubdivision").append("<option value='0'>---Select---</option>"), $.each(e.d, function () {
                $("#ddlVSubdivision").append($("<option/>").val(this.SubDivision_Code).text(this.SubDivision_Name))
            }), $("#ddlVSubdivision").selectpicker("refresh")) : "P" == DD_View ? ($("#ddlPSubdivision").empty(), $("#ddlPSubdivision").append("<option value='0'>---Select---</option>"), $.each(e.d, function () {
                $("#ddlPSubdivision").append($("<option/>").val(this.SubDivision_Code).text(this.SubDivision_Name))
            }), $("#ddlPSubdivision").selectpicker("refresh")) : "Pre" == DD_View && ($("#ddlPreSubdivision").empty(), $("#ddlPreSubdivision").append("<option value='0'>---Select---</option>"), $.each(e.d, function () {
                $("#ddlPreSubdivision").append($("<option/>").val(this.SubDivision_Code).text(this.SubDivision_Name))
            }), $("#ddlPreSubdivision").selectpicker("refresh"))
        },
        error: function (e) { }
    })
}

function BindBrand_DDL(e) {
    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        url: "../MR/webservice/EDetailingWebService.asmx/GetBrand",
        data: "{objSubBrnd:" + JSON.stringify(e) + "}",
        dataType: "json",
        success: function (e) {
            DD_DivCode = $("#DD_DivCode").val(), "U" == DD_View ? ($("#ddlUBrand").empty(), $("#ddlUBrand").append("<option value='0'>---Select---</option>"), $.each(e.d, function () {
                0 != this.Brand_Code && $("#ddlUBrand").append($("<option/>").val(this.Brand_Code).text(this.Brand_Name))
            }), $("#ddlUBrand").selectpicker("refresh")) : "V" == DD_View && ($("#ddlVBrand").empty(), $("#ddlVBrand").append("<option value='0'>---Select---</option>"), $.each(e.d, function () {
                $("#ddlVBrand").append($("<option/>").val(this.Brand_Code).text(this.Brand_Name))
            }), $("#ddlVBrand").selectpicker("refresh"), objVddlBrnd = e.d)
        },
        error: function (e) { }
    })
}

function Bind_GroupDetails() {
    if ("U" == DD_View) {
        DD_DivCode = $("#DD_DivCode").val();
        var e = $("#ddlUSubdivision option:selected");
        selectedSubDivision = [], $(e).each(function (e, d) {
            selectedSubDivision.push([$(this).val()])
        });
        var d = $("#ddlUBrand option:selected");
        selectedBrands = [], $(d).each(function (e, d) {
            selectedBrands.push([$(this).val()])
        }), (arrSubBrnd = []).push(selectedSubDivision.join(",")), arrSubBrnd.push(selectedBrands.join(",")), Bind_ProductGroup(arrSubBrnd.join("^"))
    } else "V" == DD_View && (DD_DivCode = $("#DD_DivCode").val(), (arrSubBrnd = []).push($("#ddlVSubdivision").val()), arrSubBrnd.push($("#ddlVBrand").val()), Bind_ProductGroup(arrSubBrnd.join("^")))
}

function Bind_ProductGroup(e) {
    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        url: "../MR/webservice/EDetailingWebService.asmx/GetProductGroup",
        data: "{objSubBrnd:" + JSON.stringify(e) + "}",
        dataType: "json",
        success: function (e) {
            if ("U" == DD_View) $("#ddlUProduct").empty(), $("#ddlUProduct").append("<option value='0'  disabled='true'>---Select---</option>"), $.each(e.d, function () {
                $("#ddlUProduct").append($("<option/>").val(this.Product_Code).text(this.Product_Name))
            }), $("#ddlUProduct").selectpicker("refresh"), objUddlProd = e.d;
            else if ("V" == DD_View) $("#ddlVProduct").empty(), $("#ddlVProduct").append("<option value='0'>---Select---</option>"), $.each(e.d, function () {
                $("#ddlVProduct").append($("<option/>").val(this.Product_Code).text(this.Product_Name))
            }), $("#ddlVProduct").selectpicker("val", "0"), $("#ddlVProduct").attr("disabled", "disabled"), $("#ddlVProduct").selectpicker("refresh"), objVddlProd = e.d;
            else {
                if ("P" == DD_View) return $("#ddlPMode").empty(), $("#ddlPMode").append("<option value='0'>---Select---</option>"), $.each(e.d, function () {
                    $("#ddlPMode").append($("<option/>").val(this.Product_Code).text(this.Product_Name))
                }), $("#ddlPMode").selectpicker("val", "0"), void $("#ddlPMode").selectpicker("refresh");
                if ("Pre" == DD_View) return $("#ddlPreMode").empty(), $("#ddlPreMode").append("<option value='0'>---Select---</option>"), $.each(e.d, function () {
                    $("#ddlPreMode").append($("<option/>").val(this.Product_Code).text(this.Product_Name))
                }), $("#ddlPreMode").selectpicker("val", "0"), void $("#ddlPreMode").selectpicker("refresh")
            }
            Bind_SpecGroup()
        },
        error: function (e) { }
    })
}

function Bind_SpecGroup() {
    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        url: "../MR/webservice/EDetailingWebService.asmx/GetSpecGroup",
        data: "{}",
        dataType: "json",
        success: function (e) {
            if ("U" == DD_View) $("#ddlUSpeciality").selectpicker(), $("#ddlUSpeciality").empty(), $("#ddlUSpeciality").append("<option value='0'  disabled='true'>---Select---</option>"), $.each(e.d, function () {
                $("#ddlUSpeciality").append($("<option/>").val(this.Spec_Code).text(this.Spec_Name))
            }), $("#ddlUSpeciality").selectpicker("refresh"), objUddlSpec = e.d;
            else if ("V" == DD_View) $("#ddlVSpeciality").selectpicker(), $("#ddlVSpeciality").empty(), $("#ddlVSpeciality").append("<option value='0'>---Select---</option>"), $.each(e.d, function () {
                $("#ddlVSpeciality").append($("<option/>").val(this.Spec_Code).text(this.Spec_Name))
            }), $("#ddlVSpeciality").selectpicker("val", "0"), $("#ddlVSpeciality").attr("disabled", "disabled"), $("#ddlVSpeciality").selectpicker("refresh"), objVddlSpec = e.d;
            else {
                if ("P" == DD_View) return $("#ddlPMode").selectpicker(), $("#ddlPMode").empty(), $("#ddlPMode").append("<option value='0'>---Select---</option>"), $.each(e.d, function () {
                    $("#ddlPMode").append($("<option/>").val(this.Spec_Code).text(this.Spec_Name))
                }), $("#ddlPMode").selectpicker("val", "0"), void $("#ddlPMode").selectpicker("refresh");
                if ("Pre" == DD_View) return $("#ddlPreMode").selectpicker(), $("#ddlPreMode").empty(), $("#ddlPreMode").append("<option value='0'>---Select---</option>"), $.each(e.d, function () {
                    $("#ddlPreMode").append($("<option/>").val(this.Spec_Code).text(this.Spec_Name))
                }), $("#ddlPreMode").selectpicker("val", "0"), void $("#ddlPreMode").selectpicker("refresh")
            }
            Bind_TherapyGroup()
        },
        error: function (e) { }
    })
}

function Bind_TherapyGroup() {
    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        url: "../MR/webservice/EDetailingWebService.asmx/GetTherapyGroup",
        data: "{}",
        dataType: "json",
        success: function (e) {
            if ("U" == DD_View) $("#ddlUTherapy").empty(), $.each(e.d, function () {
                $("#ddlUTherapy").append($("<option/>").val(this.Therapy_Code).text(this.Therapy_Name))
            }), $('#ddlUTherapy option[value="0"]').prop("disabled", !0), $("#ddlUTherapy").selectpicker("refresh"), objUddlThrp = e.d, $("#uploader").empty(), loadUploader();
            else if ("V" == DD_View) {
                $("#ddlVTherapy").empty(), $.each(e.d, function () {
                    $("#ddlVTherapy").append($("<option/>").val(this.Therapy_Code).text(this.Therapy_Name))
                });
                $('#ddlVTherapy option[value="0"]');
                $("#ddlVTherapy").selectpicker("val", "0"), $("#ddlVTherapy").attr("disabled", "disabled"), $("#ddlVTherapy").selectpicker("refresh"), objVddlThrp = e.d, DD_DivCode = $("#DD_DivCode").val();
                var d = $("#ddlVSubdivision").val(),
                    t = $("#ddlVBrand").val();
                (arrSubBrnd = []).push(d), arrSubBrnd.push(t), arrSubBrnd.push(""), arrSubBrnd.push(""), arrSubBrnd.push(""), Get_Upload_View(arrSubBrnd.join("^"))
            } else {
                if ("P" == DD_View) {
                    $("#ddlPMode").empty(), $.each(e.d, function () {
                        $("#ddlPMode").append($("<option/>").val(this.Therapy_Code).text(this.Therapy_Name))
                    });
                    $('#ddlPMode option[value="0"]');
                    return void $("#ddlPMode").selectpicker("refresh")
                }
                if ("Pre" == DD_View) {
                    $("#ddlPreMode").empty(), $.each(e.d, function () {
                        $("#ddlPreMode").append($("<option/>").val(this.Therapy_Code).text(this.Therapy_Name))
                    });
                    $('#ddlPreMode option[value="0"]');
                    return void $("#ddlPreMode").selectpicker("refresh")
                }
            }
        },
        error: function (e) { }
    })
}

function Upload_Files(e, d) {
    var t = $("#ddlUDivision option:selected").val(),
        i = $("#ddlUSubdivision option:selected");
    selectedSubDivision = [], $(i).each(function (e, d) {
        selectedSubDivision.push([$(this).val()])
    });
    var r = $("#ddlUBrand option:selected");
    selectedBrandVal = [], selectedBrandText = [], $(r).each(function (e, d) {
        selectedBrandVal.push([$(this).val()]), selectedBrandText.push([$(this).text()])
    });
    var l = $("#ddlUProduct option:selected");
    selectedProductVal = [], selectedProductText = [], $(l).each(function (e, d) {
        selectedProductVal.push([$(this).val()]), selectedProductText.push([$(this).text()])
    });
    var o = $("#ddlUSpeciality option:selected");
    selectedSpecVal = [], selectedSpecText = [], $(o).each(function (e, d) {
        selectedSpecVal.push([$(this).val()]), selectedSpecText.push([$(this).text()])
    });
    var a = $("#ddlUTherapy option:selected");
    selectedTherapVal = [], selectedTherapText = [], $(a).each(function (e, d) {
        selectedTherapVal.push([$(this).val()]), selectedTherapText.push([$(this).text()])
    });
    var n = [];
    n.push(selectedProductVal.join(",")), n.push(selectedProductText.join(",")), n.push(selectedSpecVal.join(",")), n.push(selectedSpecText.join(",")), n.push(selectedTherapVal.join(",")), n.push(selectedTherapText.join(",")), n.push(d.name), n.push(t), n.push(selectedSubDivision.join(",")), n.push(selectedBrandVal.join(",")), n.push(selectedBrandText.join(",")), $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        url: "../MR/webservice/EDetailingWebService.asmx/Upload_Files",
        data: "{objUpload_Files:" + JSON.stringify(n.join("^")) + "}",
        dataType: "json",
        success: function (e) { },
        error: function (e) { }
    })
}
function Upload_FilesExist(e, d) {
    var isFile = 1;
    var filenameMsgJoin = '';
    var DecreaseFile = d.length;
    plupload.each(d, function (file) {

        //var pattern = /(?:(.*[a-zA-Z])[][a-zA-Z0-9]+$)(.*[a-zA-Z])[a-zA-Z0-9]+$|(.*[a-zA-Z])(?=.*[0-9])[a-zA-Z0-9]+$|(.*[a-zA-Z])[_](?=.*[0-9])[a-zA-Z0-9]+$|(.*[a-zA-Z0-9])[_](?=.*[0-9])[a-zA-Z0-9]+$/;
        //var pattern = /(.*[a-zA-Z0-9])+$|(.*[a-zA-Z])[_](?=.*[0-9])[a-zA-Z0-9]+$/g;
        var NoSpace = /^[a-zA-Z0-9_]+$/;
        var alphanumberorder = /^[a-zA-Z]{1}/;
        var filename = file.name.split('.')[0];
        //if ((!pattern.test(filename) || !NoSpace.test(filename)) || !alphanumberorder.test(filename)) {
        if (!NoSpace.test(filename) || !alphanumberorder.test(filename)) {
            filenameMsgJoin = filenameMsgJoin + "( " + file.name + " ): File name should be [Alphabet only] or [Aplhanumberic] or [alpha,underscore and number] or [alphanumeric,underscore and number] or [No Whitespace]) <br>";
            e.removeFile(file)
            if (DecreaseFile == isFile && filenameMsgJoin != '') {
                $.alert(filenameMsgJoin, "Alphanumeric!")
            }
            DecreaseFile -= 1;
        }

        else {
            var t = $("#ddlUDivision option:selected").val();
            var n = [];
            n.push(file.name), n.push(t)

            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                url: "../MR/webservice/EDetailingWebService.asmx/Upload_FilesExist",
                data: "{objUpload_Files:" + JSON.stringify(n.join("^")) + "}",
                dataType: "json",
                success: function (g) {
                    if ($.trim(g.d)) {
                        if (g.d[0].Flag == 0) { filenameMsgJoin = filenameMsgJoin + "( " + file.name + " ): Slide Name already Exists! <br>"; }
                        else { filenameMsgJoin = filenameMsgJoin + "( " + file.name + " ): Deactivation slide Name already Exists! <br>"; }
                        e.removeFile(file);
                    }
                    if (DecreaseFile == isFile && filenameMsgJoin != '') {
                        $.alert(filenameMsgJoin, "Exist!")
                    }
                    isFile += 1;
                    return filenameMsgJoin;
                },
                error: function (e) { }
            })
        }
    });
}

function Edit_FilesExist() {
    var FileInput = document.getElementById('mFile');
    var fileCnt = FileInput.value.split('\\').length;
    var file = FileInput.value.split('\\')[fileCnt - 1];

    var NoSpace = /^[a-zA-Z0-9_]+$/;
    var alphanumberorder = /^[a-zA-Z]{1}/;
    var filename = file.split('.')[0];
    if (!NoSpace.test(filename) || !alphanumberorder.test(filename)) {
        FileInput.value = '';
        $.alert("( " + file + " ): File name should be [Alphabet only] or [Aplhanumberic] or [alpha,underscore and number] or [alphanumeric,underscore and number] or [No Whitespace]) <br>", "Alphanumeric!");
    }
    else {
        var t = $("#ddlVDivision option:selected").val();
        var n = [];
        n.push(file), n.push(t)

        $.ajax({
            type: "POST",
            contentType: "application/json; charset=utf-8",
            url: "../MR/webservice/EDetailingWebService.asmx/Upload_FilesExist",
            data: "{objUpload_Files:" + JSON.stringify(n.join("^")) + "}",
            dataType: "json",
            success: function (g) {
                if ($.trim(g.d)) {
                    FileInput.value = '';
                    if (g.d[0].Flag == 0) { $.alert("( " + file + " ): Slide Name already Exists! <br>", "Exist!") }
                    else { $.alert("( " + file + " ): Deactivation slide Name already Exists! <br>", "Exist!")}
                }
            },
            error: function (e) { }
        })
    }
}
function Get_Upload_View(e) {
    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        url: "../MR/webservice/EDetailingWebService.asmx/GetUploadView",
        data: "{objSubBrnd:" + JSON.stringify(e) + "}",
        dataType: "json",
        success: function (e) {
            if ($.trim(e.d)) {
                $("#grdUpload").empty(), $("#grdUpload").append("<thead><tr style='background-color:White;'><th scope='col'>S.No</th><th class='grdHeader' scope='col'>ImgS.No</th><th class='grdHeader' scope='col'>ImgSrc</th><th scope='col'>Image</th><th scope='col'>Brand</th><th class='grdHeader' scope='col'>Product</th><th scope='col'>Speciality</th><th scope='col'>Therapy</th><th scope='col'>Edit</th><th scope='col'><div class='custom-control custom-checkbox'><input type='checkbox' class='custom-control-input' id='chkDelete_All'><label class='custom-control-label' for='chkDelete_All'>Delete</label></div>&nbsp;<a class='deleteAll' id='btnDelete_All' title='Delete All' href='#' data-toggle='tooltip'><i class=\"fa fa-trash\"></i></a></th><th scope='col'><div class='custom-control custom-checkbox'><input type='checkbox' class='custom-control-input' id='chkCommon_All'><label class='custom-control-label' for='chkCommon_All'>Common</label></div></th><tbody>");
                for (var d = 0; d < e.d.length; d++) {
                    var t = d + 1;
                    $("#grdUpload").append("<tr><td><span id='lblSNo_" + d + "'>" + t + "</span></td> <td class='grdHeader'><span id='imgSI_NO_" + d + "'>" + e.d[d].SI_NO + "</span></td>  <td class='grdHeader'><span id='imgName_" + d + "'>" + e.d[d].Img_Name + "</span></td> <td><img id='imgSrc_" + d + "'  src='" + e.d[d].Img_Src + "' title='" + e.d[d].Img_Name + "' style='height:120px;width:150px;'></td> <td><select id='ddlGBrands' data-selected-text-format='count>1' disabled='disabled' class='selectpicker form-control ddlGBrands_" + d + "' data-live-search='true' multiple=''></select></td><td class='grdHeader'><select id='ddlGProduct' data-selected-text-format='count>1' class='selectpicker form-control ddlGProduct_" + d + "' data-live-search='true' multiple=''></select></td><td><select id='ddlGSpeciality' data-selected-text-format='count>1' disabled='disabled' class='selectpicker form-control ddlGSpeciality_" + d + "' data-live-search='true' multiple=''></select></td> <td><select id='ddlGTherapy' data-selected-text-format='count>1' disabled='disabled' class='selectpicker form-control ddlGTherapy_" + d + "' data-live-search='true' multiple=''></select></td> <td style=text-align:center;><a class='edit' title='Edit' data-toggle='modal' data-target='#myModal' href='#myModal'><i class=\"fa fa-pencil\"></i></a></td><td style=text-align:center;><a class='delete' id='btnDelete_" + d + "' title='Delete' href='#' data-toggle='tooltip'><i class=\"fa fa-trash\"></i></a>&nbsp;<div class='custom-control custom-checkbox'><input type='checkbox' class='custom-control-input' id='chkDelete_" + d + "'><label class='custom-control-label' for='chkDelete_" + d + "'></label></div></td> <td style=text-align:center;><div class='custom-control custom-checkbox'><input type='checkbox' class='custom-control-input' id='chkCommon_" + d + "'><label class='custom-control-label' for='chkCommon_" + d + "'></label></div></td></tr> "), BindBrand(d)
                }
                $("#grdUpload").append("</tbody>"), $("#grdUpload").DataTable({
                    destroy: !0,
                    paging: !1,
                    ordering: !1,
                    info: !1,
                    searching: !1
                });
                for (d = 0; d < e.d.length; d++) {
                    var i, r, l, o, a, n = "",
                        s = "",
                        c = "",
                        p = "";
                    n = e.d["" + d].Product_Brand_Code, s = e.d["" + d].Product_Detail_Code, c = e.d["" + d].Doc_Special_Code, p = e.d["" + d].Product_Grp_Code, a = e.d["" + d].File_type, "," == n[n.length - 1] && (n = n.slice(0, -1)), "," == s[s.length - 1] && (s = s.slice(0, -1)), "," == c[c.length - 1] && (c = c.slice(0, -1)), "," == p[p.length - 1] && (p = p.slice(0, -1)), i = $.map(n.split(","), function (e) {
                        return parseInt(e, 10)
                    }), r = $.map(s.split(","), function (e) {
                        return parseInt(e, 10)
                    }), l = $.map(c.split(","), function (e) {
                        return parseInt(e, 10)
                    }), o = $.map(p.split(","), function (e) {
                        return parseInt(e, 10)
                    }), i && ($(".ddlGBrands_" + d).selectpicker(), $(".ddlGBrands_" + d).selectpicker("val", i)), r && ($(".ddlGProduct_" + d).selectpicker(), $(".ddlGProduct_" + d).selectpicker("val", r)), l && ($(".ddlGSpeciality_" + d).selectpicker(), $(".ddlGSpeciality_" + d).selectpicker("val", l)), o && ($(".ddlGTherapy_" + d).selectpicker(), $(".ddlGTherapy_" + d).selectpicker("val", o)), "C" == a ? $("#chkCommon_" + d).prop("checked", !0) : $("#chkCommon_" + d).prop("checked", !1)
                }
                $(".Common").show(), $("#grdUpload").show()
            } else $(".vCategory").hide(), $(".Common").hide(), $("#grdUpload").empty(), $("#grdUpload").hide(), $.alert("No Records Found!", "Alert!")
        },
        failure: function (e) { },
        error: function (e) { }
    })
}

function BindBrand(e) {
    $(".ddlGBrands_" + e).empty(), $.each(objVddlBrnd, function () {
        $(".ddlGBrands_" + e).append($("<option/>").val(this.Brand_Code).text(this.Brand_Name))
    }), $(".ddlGBrands_" + e).selectpicker("refresh"), BindProduct(e)
}

function BindProduct(e) {
    $(".ddlGProduct_" + e).empty(), $(".ddlGProduct_" + e).append("<option value='0'  disabled='true'>---Select---</option>"), $.each(objVddlProd, function () {
        $(".ddlGProduct_" + e).append($("<option/>").val(this.Product_Code).text(this.Product_Name))
    }), $(".ddlGProduct_" + e).selectpicker("refresh"), BindSpeciality(e)
}

function BindSpeciality(e) {
    $(".ddlGSpeciality_" + e).empty(), $(".ddlGSpeciality_" + e).append("<option value='0'  disabled='true'>---Select---</option>"), $.each(objVddlSpec, function () {
        $(".ddlGSpeciality_" + e).append($("<option/>").val(this.Spec_Code).text(this.Spec_Name))
    }), $(".ddlGSpeciality_" + e).selectpicker("refresh"), BindTherapy(e)
}

function BindTherapy(e) {
    $(".ddlGTherapy_" + e).empty(), $.each(objVddlThrp, function () {
        $(".ddlGTherapy_" + e).append($("<option/>").val(this.Therapy_Code).text(this.Therapy_Name))
    }), $(".ddlGTherapy_" + e + ' option[value="0"]').prop("disabled", !0), $(".ddlGTherapy_" + e).selectpicker("refresh")
}

function BindBrandPriority(e) {
    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        url: "../MR/webservice/EDetailingWebService.asmx/GetPriority",
        data: "{objmodePriority:" + JSON.stringify(e) + "}",
        dataType: "json",
        success: function (e) {
            if ($.trim(e.d)) {
                var g = 0;
                $("#grdPriority").empty(), $("#grdPriority").append("<thead><tr style='background-color:White;'><th scope='col'>S.No</th><th class='grdHeader' scope='col'>Brand Code</th><th scope='col'>Name</th><th scope='col'>Slide Priority</th><th scope='col'>Priority</th><tbody>");
                for (var d = 0; d < e.d.length; d++) {
                    var t = d + 1;
                    $("#grdPriority").append("<tr><td><span id='lblSNo_" + d + "'>" + t + "</span></td><td class='grdHeader'><span id='lblBrandCode_" + d + "'>" + e.d[d].Product_Brd_Code + "</span></td> <td><span id='lblBrandName_" + d + "'>" + e.d[d].Product_Brd_Name + "</span></td><td align='center'><a class='spView' title='View Slides' href='#'>View &nbsp; <i class=\"fa fa-eye\"></i></a></td><td><select id='ddlMasPriority' class='form-control ddlMasPriority" + d + "'></select></td></tr> ")
                    if (e.d[d].Priority != '')
                        g = g + 1;
                }
                $("#grdPriority").append("</tbody>");
                var i = $("#grdPriority tr").length;
                $("#grdPriority > tbody  > tr").each(function () {
                    //var e = $(this).find("select:eq(0)");
                    //e.empty();
                    //for (var d = 1; d < i; d++) e.append($("<option/>").val(d).text(d));
                    //e.selectpicker(), e.selectpicker("val", $(this).index() + 1)

                    var e = $(this).find("select:eq(0)");
                    var o = 0;
                    e.empty();
                    for (var d = 1; d < i; d++) {
                        e.append($("<option/>").val(d).text(d));
                        if (d <= g) {
                            e.selectpicker(), e.selectpicker("val", $(this).index() + 1)
                            o = 1;
                        }
                        else {
                            if (o == 0) {
                                e.selectpicker(), //e.selectpicker("val", 0)
                                    e.selectpicker({ title: 'Nothing selected' }).selectpicker('render');
                            }
                        }
                    }
                }), $("#grdPriority").DataTable({
                    destroy: !0,
                    paging: !1,
                    ordering: !1,
                    info: !1,
                    searching: !1
                })
            } else $("#grdPriority").empty(), $(".grdPriority").hide(), $.alert("No Records Found!", "Alert!")
        },
        failure: function (e) { },
        error: function (e) { }
    })
}

function BindBrandPreview(e) {
    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        url: "../MR/webservice/EDetailingWebService.asmx/GetPriority",
        data: "{objmodePriority:" + JSON.stringify(e) + "}",
        dataType: "json",
        success: function (e) {
            if ($.trim(e.d)) {
                $("#grdPreview").empty(), $("#grdPreview").append("<thead><tr style='background-color:White;'><th scope='col'>S.No</th><th class='grdHeader' scope='col'>Brand Code</th><th scope='col'>Name</th><th scope='col'>Preview</th><tbody>");
                for (var d = 0; d < e.d.length; d++) {
                    var t = d + 1;
                    $("#grdPreview").append("<tr><td><span id='lblSNo_" + d + "'>" + t + "</span></td> <td class='grdHeader'><span id='lblBrandCode_" + d + "'>" + e.d[d].Product_Brd_Code + "</span></td> <td><span id='lblBrandName_" + d + "'>" + e.d[d].Product_Brd_Name + "</span></td><td align='center'><a class='sPreView' title='Preview' href='#'>Preview &nbsp; <i class=\"fa fa-eye\"></i></a></td></tr> ")
                }
                $("#grdPreview").append("</tbody>"), $("#grdPreview").DataTable({
                    destroy: !0,
                    paging: !1,
                    ordering: !1,
                    info: !1,
                    searching: !1
                })
            } else $("#grdPreview").empty(), $(".grdPreview").hide(), $.alert("No Records Found!", "Alert!")
        },
        failure: function (e) { },
        error: function (e) { }
    })
}
$(document).ready(function () {
    $(document).ajaxStart(function () {
        $("#loader").css("display", "block")
    }).ajaxStop(function () {
        $("#loader").css("display", "none")
    }), $("#divUpload").hide(), $("#divPriority").hide(), $("#divPreview").hide(), $("#uploader").hide(), $("#divView").show(), $(".uCategory").hide(), $(".vCategory").hide(), $(".Common").hide(), $("#grdUpload").hide(), DD_SfCode = $("#DD_SfCode").val(), DD_SfType = $("#DD_SfType").val(), DD_DivCode = $("#DD_DivCode").val(), DD_View = "", $("#btnUpload").click(function (e) {
        $("#ddlUDivision").empty(), $("#ddlUSubdivision").empty(), $("#ddlUSubdivision").selectpicker("refresh"), $("#ddlUBrand").empty(), $("#ddlUBrand").selectpicker("refresh"), $("#ddlUProduct").empty(), $("#ddlUProduct").selectpicker("refresh"), $("#ddlUSpeciality").empty(), $("#ddlUSpeciality").selectpicker("refresh"), $("#ddlUTherapy").empty(), $("#ddlUTherapy").selectpicker("refresh"), $(".uCategory").hide(), $("#uploader").empty(), $("#uploader").hide(), DD_View = "U", BindDivision_DDL(), BindSubDivision_DDL(), BindBrand_DDL(), $("#divUpload").show(), $("#divView").hide(), $("#divPriority").hide(), $("#divPreview").hide(), $(this).addClass("active"), $("#btnView").removeClass("active"), $("#btnPriority").removeClass("active"), $("#btnPreview").removeClass("active"), $(this).hasClass("btn-default") && ($(this).addClass("btn-primary").removeClass("btn-default"), $("#btnView").addClass("btn-default").removeClass("btn-primary"), $("#btnPriority").addClass("btn-default").removeClass("btn-primary"), $("#btnPreview").addClass("btn-default").removeClass("btn-primary")), e.preventDefault()
    }), $("#btnView").click(function (e) {
        $("#ddlVDivision").empty(), $("#ddlVSubdivision").empty(), $("#ddlVBrand").empty(), $("#ddlVProduct").empty(), $("#ddlVProduct").selectpicker("refresh"), $("#ddlVSpeciality").empty(), $("#ddlVSpeciality").selectpicker("refresh"), $("#ddlVTherapy").empty(), $("#ddlVTherapy").selectpicker("refresh"), $(".vCategory").hide(), $(".Common").hide(), $("#grdUpload").hide(), DD_View = "V", BindDivision_DDL(), BindSubDivision_DDL(), BindBrand_DDL(), $("#grdUpload").empty(), $("#divPriority").hide(), $("#divPreview").hide(), $("#divUpload").hide(), $("#divView").show(), $(this).addClass("active"), $("#btnUpload").removeClass("active"), $("#btnPriority").removeClass("active"), $("#btnPreview").removeClass("active"), $(this).hasClass("btn-default") && ($(this).addClass("btn-primary").removeClass("btn-default"), $("#btnUpload").addClass("btn-default").removeClass("btn-primary"), $("#btnPriority").addClass("btn-default").removeClass("btn-primary"), $("#btnPreview").addClass("btn-default").removeClass("btn-primary")), e.preventDefault()
    }), $("#btnPriority").click(function (e) {
        DD_View = "P", BindSubDivision_DDL(), $("#lblPSubdivision").show(), $("#ddlPSubdivision").selectpicker("show"), $("#lblPMode").hide(), $("#ddlPMode").empty(), $("#ddlPMode").selectpicker("hide"), $(".prdPriority").show(), $("#divUpload").hide(), $("#divView").hide(), $("#divPriority").show(), $("#divPreview").hide(), $(this).addClass("active"), $("#btnView").removeClass("active"), $("#btnUpload").removeClass("active"), $("#btnPreview").removeClass("active"), $(this).hasClass("btn-default") && ($(this).addClass("btn-primary").removeClass("btn-default"), $("#btnView").addClass("btn-default").removeClass("btn-primary"), $("#btnUpload").addClass("btn-default").removeClass("btn-primary"), $("#btnPreview").addClass("btn-default").removeClass("btn-primary")), $("#rdBrandP").prop("checked", !0), $("#grdPriority").empty(), $(".grdPriority").hide(), e.preventDefault()
    }), $("#btnPreview").click(function (e) {
        DD_View = "Pre", BindSubDivision_DDL(), $("#lblPreSubdivision").show(), $("#ddlPreSubdivision").selectpicker("show"), $("#lblPreMode").hide(), $("#ddlPreMode").empty(), $("#ddlPreMode").selectpicker("hide"), $(".prdPreview").show(), $("#divUpload").hide(), $("#divView").hide(), $("#divPriority").hide(), $("#divPreview").show(), $(this).addClass("active"), $("#btnView").removeClass("active"), $("#btnUpload").removeClass("active"), $("#btnPriority").removeClass("active"), $(this).hasClass("btn-default") && ($(this).addClass("btn-primary").removeClass("btn-default"), $("#btnView").addClass("btn-default").removeClass("btn-primary"), $("#btnUpload").addClass("btn-default").removeClass("btn-primary"), $("#btnPriority").addClass("btn-default").removeClass("btn-primary")), $("#rdBrandPre").prop("checked", !0), $("#grdPreview").empty(), $(".grdPreview").hide(), e.preventDefault()
    }), $("#btnView").is(".btn-primary") && (DD_View = "V", BindDivision_DDL(), BindSubDivision_DDL(), BindBrand_DDL()), $("#btnUView").click(function (e) {
        DD_View = "U", $(document).ajaxStart(function () {
            $("#loader").css("display", "block")
        }).ajaxStop(function () {
            $("#loader").css("display", "none")
        }), !0 === Validation() && (Bind_GroupDetails(), $("#uploader").show(), $(".uCategory").show()), e.preventDefault()
        //setTimeout(() => {
        //        $(".uCategory > div:nth-child(4) > div:nth-child(1) > div:nth-child(3) > div:nth-child(2) > div:nth-child(1) > button:nth-child(1)").click();
        //}, 500);
    }), $("#btnVView").click(function (e) {
        DD_View = "V", $(document).ajaxStart(function () {
            $("#loader").css("display", "block")
        }).ajaxStop(function () {
            $("#loader").css("display", "none")
        }), !0 === Validation() && (Bind_GroupDetails(), $(".vCategory").show()), e.preventDefault()
    }), $("#btnPView").click(function (e) {
        if (DD_View = "P", $(document).ajaxStart(function () {
                $("#loader").css("display", "block")
        }).ajaxStop(function () {
                $("#loader").css("display", "none")
        }), !0 === Validation()) {
            if ($("#rdBrandP").prop("checked")) {
                var d = $("#ddlPSubdivision option:selected").val(),
                    t = "";
                modePriority = "0", (arrSubBrnd = []).push(d), arrSubBrnd.push(t), arrSubBrnd.push(modePriority), BindBrandPriority(arrSubBrnd.join("^"))
            } else if ($("#rdProductP").prop("checked")) {
                d = $("#ddlPSubdivision option:selected").val(), t = $("#ddlPMode option:selected").val();
                modePriority = "1", (arrSubBrnd = []).push(d), arrSubBrnd.push(t), arrSubBrnd.push(modePriority), BindBrandPriority(arrSubBrnd.join("^"))
            } else if ($("#rdSpecialityP").prop("checked")) {
                d = $("#ddlPSubdivision option:selected").val(), t = $("#ddlPMode option:selected").val();
                modePriority = "2", (arrSubBrnd = []).push(d), arrSubBrnd.push(t), arrSubBrnd.push(modePriority), BindBrandPriority(arrSubBrnd.join("^"))
            } else if ($("#rdTherapyP").prop("checked")) {
                d = $("#ddlPSubdivision option:selected").val(), t = $("#ddlPMode option:selected").val();
                modePriority = "3", (arrSubBrnd = []).push(d), arrSubBrnd.push(t), arrSubBrnd.push(modePriority), BindBrandPriority(arrSubBrnd.join("^"))
            }
            $(".grdPriority").show()
        }
        e.preventDefault()
    }), $("#btnPreViewGo").click(function (e) {
        if (DD_View = "Pre", $(document).ajaxStart(function () {
                $("#loader").css("display", "block")
        }).ajaxStop(function () {
                $("#loader").css("display", "none")
        }), !0 === Validation()) {
            if ($("#rdBrandPre").prop("checked")) {
                var d = $("#ddlPreSubdivision option:selected").val();
                modePriority = "0", (arrSubBrnd = []).push(d), arrSubBrnd.push(""), arrSubBrnd.push(modePriority), BindBrandPreview(arrSubBrnd.join("^"))
            } else if ($("#rdProductPre").prop("checked")) {
                d = $("#ddlPreSubdivision option:selected").val();
                var t = $("#ddlPreMode option:selected").val();
                modePreview = "1", (arrSubBrnd = []).push(d), arrSubBrnd.push(t), arrSubBrnd.push(modePreview), BindBrandPreview(arrSubBrnd.join("^"))
            } else if ($("#rdSpecialityPre").prop("checked")) {
                d = $("#ddlPreSubdivision option:selected").val(), t = $("#ddlPreMode option:selected").val();
                modePreview = "2", (arrSubBrnd = []).push(d), arrSubBrnd.push(t), arrSubBrnd.push(modePreview), BindBrandPreview(arrSubBrnd.join("^"))
            } else if ($("#rdTherapyPre").prop("checked")) {
                d = $("#ddlPreSubdivision option:selected").val(), t = $("#ddlPreMode option:selected").val();
                modePreview = "3", (arrSubBrnd = []).push(d), arrSubBrnd.push(t), arrSubBrnd.push(modePreview), BindBrandPreview(arrSubBrnd.join("^"))
            }
            $(".grdPreview").show()
        }
        e.preventDefault()
    })
}), $(document).on("click", "#chkCommon_All", function (e) {
    var d = $(e.target).closest("table");
    $("td input:checkbox[id*=chkCommon]", d).prop("checked", this.checked)
}), $(document).on("click", "#chkDelete_All", function (e) {
    var d = $(e.target).closest("table");
    $("td input:checkbox[id*=chkDelete]", d).prop("checked", this.checked)
}), $(document).on("click", ".edit", function () {
    var e = $(this).closest("tr"),
        d = (e.find("span:eq(0)"), e.find("span:eq(1)")),
        t = e.find("span:eq(2)"),
        i = e.find("img:eq(0)"),
        r = e.find("select:eq(0)"),
        l = e.find("select:eq(1)"),
        o = e.find("select:eq(2)"),
        a = e.find("select:eq(3)"),
        n = e.find("input[type=checkbox]:eq(1)").attr("id"),
        s = $('<form role="form" name="modalForm" action="" method="post" enctype="multipart/form-data"></form>'),
        c = $('<div class="form-group"></div>');
    c.append('<center><div class="row"><div class="col-md-12 col-sm-12"><img id="imgMSrc" src=' + i.attr("src") + " title=" + i.attr("title") + ' style="height: 220px; width: 250px;"></div></div></center><br />'), c.append('<div class="row"><div class="col-md-3 col-sm-3"><span id="lblMPBrand">Brand :</span></div><div class="col-md-9 col-sm-9"><select id="ddlMBrand" class="selectpicker form-control ddlMBrand" disabled="disabled" data-live-search="true" multiple=""></select></div></div><br />'), c.append('<div class="row"><div class="col-md-3 col-sm-3"><span id="lblMProduct">Product :</span></div><div class="col-md-9 col-sm-9"><select id="ddlMProduct" class="selectpicker form-control ddlMProduct" data-live-search="true" multiple=""></select></div></div><br />'), c.append('<div class="row"><div class="col-md-3 col-sm-3"><span id="lblMSpeciality">Speciality :</span></div><div class="col-md-9 col-sm-9"><select id="ddlMSpeciality" class="selectpicker form-control ddlMSpeciality" data-live-search="true" multiple=""></select></div></div><br />'), c.append('<div class="row"><div class="col-md-3 col-sm-3"><span id="lblMTherapy">Therapy :</span></div><div class="col-md-9 col-sm-9"><select id="ddlMTherapy" class="selectpicker form-control ddlMTherapy" data-live-search="true" multiple=""></select></div></div><br />'), c.append('<div class="row"><div class="col-md-3 col-sm-3"><span id="lblMImage">Replace :</span></div><div class="col-md-9 col-sm-9"><input type="file" name="file" id="mFile" class="form-control" onchange="return Edit_FilesExist()"></div></div><br />'), c.append('<div class="row"><div class="col-md-3 col-sm-3"><span id="lblMCommon">Common :</span></div><div class="col-md-9 col-sm-9"><div class="custom-control custom-checkbox"><input type="checkbox" class="custom-control-input" id="chkMCommon"><label class="custom-control-label" for="chkMCommon"></label></div></div></div><br />'), c.append('<span id="lblMImgName" class="grdHeader">' + t.text() + '</span><span id="lblMImgSlNo" class="grdHeader">' + d.text() + "</span>"), s.append(c), $(".modal-body").html(c), $("#ddlMBrand").empty(), $.each(objVddlBrnd, function () {
        $("#ddlMBrand").append($("<option/>").val(this.Brand_Code).text(this.Brand_Name))
    }), $("#ddlMBrand").selectpicker("refresh"), $("#ddlMProduct").empty(), $("#ddlMProduct").append("<option value='0'  disabled='true'>---Select---</option>"), $.each(objVddlProd, function () {
        $("#ddlMProduct").append($("<option/>").val(this.Product_Code).text(this.Product_Name))
    }), $("#ddlMProduct").selectpicker("refresh"), $("#ddlMSpeciality").empty(), $("#ddlMSpeciality").append("<option value='0'  disabled='true'>---Select---</option>"), $.each(objVddlSpec, function () {
        $("#ddlMSpeciality").append($("<option/>").val(this.Spec_Code).text(this.Spec_Name))
    }), $("#ddlMSpeciality").selectpicker("refresh"), $("#ddlMTherapy").empty(), $.each(objVddlThrp, function () {
        $("#ddlMTherapy").append($("<option/>").val(this.Therapy_Code).text(this.Therapy_Name))
    }), $('#ddlMTherapy option[value="0"]').prop("disabled", !0), $("#ddlMTherapy").selectpicker("refresh");
    var p = $("." + r.attr('class').split(' ').pop() + " option:selected"),
        u = [];
    $(p).each(function (e, d) {
        u.push([$(this).val()])
    });
    var h = $("." + l.attr('class').split(' ').pop() + " option:selected"),
        v = [];
    $(h).each(function (e, d) {
        v.push([$(this).val()])
    });
    var P = $("." + o.attr('class').split(' ').pop() + " option:selected"),
        b = [];
    $(P).each(function (e, d) {
        b.push([$(this).val()])
    });
    var y = $("." + a.attr('class').split(' ').pop() + " option:selected"),
        f = [];
    $(y).each(function (e, d) {
        f.push([$(this).val()])
    }), $("#ddlMBrand").selectpicker(), $("#ddlMBrand").selectpicker("val", u), $("#ddlMProduct").selectpicker(), $("#ddlMProduct").selectpicker("val", v), $("#ddlMSpeciality").selectpicker(), $("#ddlMSpeciality").selectpicker("val", b), $("#ddlMTherapy").selectpicker(), $("#ddlMTherapy").selectpicker("val", f), $("#" + n).prop("checked") ? $("#chkMCommon").prop("checked", !0) : $("#chkMCommon").prop("checked", !1)
}), $(document).on("click", ".modal-footer .btn-primary", function (e) {
    e.preventDefault();
    var d = document.getElementById("mFile"),
        t = "",
        i = $("#lblMImgSlNo").text();
 
    var l = $("#ddlVDivision option:selected").val(),
        o = $("#ddlVSubdivision option:selected").val(),
        a = $("#ddlVBrand option:selected").val(),
        n = $("#ddlVBrand option:selected").text(),
        s = $("#ddlMBrand option:selected");
    selectedBrandVal = [], $(s).each(function (e, d) {
        selectedBrandVal.push([$(this).val()])
    });
    var c = $("#ddlMProduct option:selected");
    selectedProductVal = [], selectedProductText = [], $(c).each(function (e, d) {
        selectedProductVal.push([$(this).val()]), selectedProductText.push([$(this).text()])
    });
    var p = $("#ddlMSpeciality option:selected");
    selectedSpecVal = [], selectedSpecText = [], $(p).each(function (e, d) {
        selectedSpecVal.push([$(this).val()]), selectedSpecText.push([$(this).text()])
    });
    var u = $("#ddlMTherapy option:selected");
    selectedTherapVal = [], selectedTherapText = [], $(u).each(function (e, d) {
        selectedTherapVal.push([$(this).val()]), selectedTherapText.push([$(this).text()])
    });
    var h = $("#chkMCommon").prop("checked"),
        v = [];
    if (selectedProductVal.join(",") == '') { $.alert("Please Select the Product", "Alert!") && $("#myModal").modal("show") }
    else if (selectedSpecVal.join(",") == '') { $.alert("Please Select the Speciality", "Alert!") && $("#myModal").modal("show") }
    else if (selectedTherapVal.join(",") == '') { $.alert("Please Select the Therapy", "Alert!") && $("#myModal").modal("show") }
    else {
        if (0 == d.files.length) t = $("#lblMImgName").text();
        else {
            t = document.getElementById("mFile").files[0].name;
            var r = new FormData;
            r.append("file", $("#mFile")[0].files[0]), $.ajax({
                url: "DD_Slide_Upload.ashx",
                type: "POST",
                data: r,
                processData: !1,
                contentType: !1,
                success: function (e) { }
            })
        }
        v.push(selectedProductVal.join(",")), v.push(selectedProductText.join(",")), v.push(selectedSpecVal.join(",")), v.push(selectedSpecText.join(",")), v.push(selectedTherapVal.join(",")), v.push(selectedTherapText.join(",")), v.push(t), v.push(l), v.push(o), v.push(a), v.push(n), v.push(i), v.push(h);
        $.ajax({
            type: "POST",
            contentType: "application/json; charset=utf-8",
            url: "../MR/webservice/EDetailingWebService.asmx/Update_Files",
            data: "{objUpload_Files:" + JSON.stringify(v.join("^")) + "}",
            dataType: "json",
            success: function (e) {
                $.alert("Update Success!", "Alert!") && ($("#myModal").modal("toggle"), $("#btnVView").trigger("click"))
            },
            error: function (e) { }
        })
    } 
}), $(document).on("click", ".delete", function (e) {
    var d = $(this).closest("tr"),
        t = d.find("span:eq(1)");
    d.find("span:eq(2)");
    $.confirm({
        title: "Confirm!",
        content: "Are you sure? You want to delete?",
        buttons: {
            confirm: function () {
                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    url: "../MR/webservice/EDetailingWebService.asmx/Delete_Files",
                    data: "{objSl_No:" + JSON.stringify(t.text()) + "}",
                    dataType: "json",
                    success: function (e) {
                        $.alert("Record Deleted!", "Alert!") && $("#btnVView").trigger("click")
                    },
                    error: function (e) { }
                })
            },
            cancel: function () {
                $.alert("Canceled!")
            }
        }
    })
}), $(document).on("click", ".spView", function (e) {
    e.preventDefault();
    var d = "",
        t = "",
        i = "",
        r = "",
        l = "",
        o = "",
        a = $(this).closest("tr"),
        n = a.find("span:eq(1)"),
        s = a.find("span:eq(2)");
    $("#btnPriority").is(".btn-primary") && ($("#rdProductP").prop("checked") ? (d = $("#ddlPSubdivision option:selected").val(), t = $("#ddlPSubdivision option:selected").text(), i = $("#ddlPMode option:selected").val(), o = $("#ddlPMode option:selected").text(), modePriority = "1") : $("#rdSpecialityP").prop("checked") ? (d = $("#ddlPSubdivision option:selected").val(), t = $("#ddlPSubdivision option:selected").text(), r = $("#ddlPMode option:selected").val(), o = $("#ddlPMode option:selected").text(), modePriority = "2") : $("#rdTherapyP").prop("checked") ? (d = $("#ddlPSubdivision option:selected").val(), t = $("#ddlPSubdivision option:selected").text(), l = $("#ddlPMode option:selected").val(), o = $("#ddlPMode option:selected").text(), modePriority = "3") : $("#rdBrandP").prop("checked") && (d = $("#ddlPSubdivision option:selected").val(), t = $("#ddlPSubdivision option:selected").text(), modePriority = "0"), (arrSlidePriority = []).push(d), arrSlidePriority.push(n.text()), arrSlidePriority.push(i), arrSlidePriority.push(r), arrSlidePriority.push(l), arrSlidePriority.push(modePriority), arrSlidePriority.push(t), arrSlidePriority.push(o), arrSlidePriority.push(s.text()), showModalPopUp(arrSlidePriority.join(",")))
}), $("#ddlUSubdivision").change(function () {
    var e = $("#ddlUSubdivision option:selected");
    selectedSubDivision = [], $(e).each(function (e, d) {
        selectedSubDivision.push([$(this).val()])
    });
    (arrSubBrnd = []).push(selectedSubDivision.join(",")), BindBrand_DDL(arrSubBrnd.join("^")),
    $("#ddlUProduct").empty(), $("#ddlUProduct").selectpicker("refresh"), $(".uCategory").hide()
}), $("#ddlUBrand").change(function (e) {
    $("#ddlUProduct").empty(), $("#ddlUProduct").selectpicker("refresh"), $(".uCategory").hide()
}), $("#ddlVSubdivision").change(function (e) {
    var e = $("#ddlVSubdivision option:selected");
    selectedSubDivision = [], $(e).each(function (e, d) {
        selectedSubDivision.push([$(this).val()])
    });
    (arrSubBrnd = []).push(selectedSubDivision.join(",")), BindBrand_DDL(arrSubBrnd.join("^")),
    $("#ddlVProduct").empty(), $("#ddlVProduct").selectpicker("refresh"), $(".vCategory").hide(), $(".Common").hide(), $("#grdUpload").hide()
}), $("#ddlVBrand").change(function (e) {
    $("#ddlVProduct").empty(), $("#ddlVProduct").selectpicker("refresh"), $(".vCategory").hide(), $(".Common").hide(), $("#grdUpload").hide()
}), $("#ddlVProduct").change(function (e) {
    DD_DivCode = $("#DD_DivCode").val();
    var d = $("#ddlVSubdivision").val(),
        t = $("#ddlVBrand").val(),
        i = $("#ddlVProduct").val();
    (arrSubBrnd = []).push(d), arrSubBrnd.push(t), arrSubBrnd.push(i), arrSubBrnd.push(""), arrSubBrnd.push(""), Get_Upload_View(arrSubBrnd.join("^"))
}), $("#ddlVSpeciality").change(function (e) {
    DD_DivCode = $("#DD_DivCode").val();
    var d = $("#ddlVSubdivision").val(),
        t = $("#ddlVBrand").val(),
        i = $("#ddlVSpeciality").val();
    (arrSubBrnd = []).push(d), arrSubBrnd.push(t), arrSubBrnd.push(""), arrSubBrnd.push(i), arrSubBrnd.push(""), Get_Upload_View(arrSubBrnd.join("^"))
}), $("#ddlVTherapy").change(function (e) {
    DD_DivCode = $("#DD_DivCode").val();
    var d = $("#ddlVSubdivision").val(),
        t = $("#ddlVBrand").val(),
        i = $("#ddlVTherapy").val();
    (arrSubBrnd = []).push(d), arrSubBrnd.push(t), arrSubBrnd.push(""), arrSubBrnd.push(""), arrSubBrnd.push(i), Get_Upload_View(arrSubBrnd.join("^"))
}), $("#ddlPSubdivision").change(function (e) {
    $("#grdPriority").empty(), $(".grdPriority").hide();
    var d = $(this).val();
    $("#rdProductP").prop("checked") && ("0" == d ? ($("#ddlPMode").empty(), $("#ddlPMode").selectpicker("refresh")) : (DD_View = "P", DD_DivCode = $("#DD_DivCode").val(), (arrSubBrnd = []).push($(this).val()), arrSubBrnd.push(""), Bind_ProductGroup(arrSubBrnd.join("^"))))
}), $("#ddlPMode").change(function (e) {
    $("#grdPriority").empty(), $(".grdPriority").hide()
}), $("#ddlPreSubdivision").change(function (e) {
    $("#grdPreview").empty(), $(".grdPreview").hide();
    var d = $(this).val();
    $("#rdProductPre").prop("checked") && ("0" == d ? ($("#ddlPreMode").empty(), $("#ddlPreMode").selectpicker("refresh")) : (DD_View = "Pre", DD_DivCode = $("#DD_DivCode").val(), (arrSubBrnd = []).push($(this).val()), arrSubBrnd.push(""), Bind_ProductGroup(arrSubBrnd.join("^"))))
}), $("#ddlPreMode").change(function (e) {
    $("#grdPreview").empty(), $(".grdPreview").hide()
}), $("input[type=radio][name=filter]").change(function () {
    if (0 == $(this).val()) {
        $("#ddlVProduct").attr("disabled", "disabled"), $("#ddlVProduct").selectpicker("refresh"), $("#ddlVSpeciality").attr("disabled", "disabled"), $("#ddlVSpeciality").selectpicker("refresh"), $("#ddlVTherapy").attr("disabled", "disabled"), $("#ddlVTherapy").selectpicker("refresh"), DD_DivCode = $("#DD_DivCode").val();
        var e = $("#ddlVSubdivision").val(),
            d = $("#ddlVBrand").val();
        (arrSubBrnd = []).push(e), arrSubBrnd.push(d), arrSubBrnd.push(""), arrSubBrnd.push(""), arrSubBrnd.push(""), Get_Upload_View(arrSubBrnd.join("^"))
    } else 1 == $(this).val() ? ($("#ddlVProduct").removeAttr("disabled"), $("#ddlVProduct").selectpicker("refresh"), $("#ddlVSpeciality").attr("disabled", "disabled"), $("#ddlVSpeciality").selectpicker("refresh"), $("#ddlVTherapy").attr("disabled", "disabled"), $("#ddlVTherapy").selectpicker("refresh")) : 2 == $(this).val() ? ($("#ddlVProduct").attr("disabled", "disabled"), $("#ddlVProduct").selectpicker("refresh"), $("#ddlVSpeciality").removeAttr("disabled"), $("#ddlVSpeciality").selectpicker("refresh"), $("#ddlVTherapy").attr("disabled", "disabled"), $("#ddlVTherapy").selectpicker("refresh")) : 3 == $(this).val() && ($("#ddlVProduct").attr("disabled", "disabled"), $("#ddlVProduct").selectpicker("refresh"), $("#ddlVSpeciality").attr("disabled", "disabled"), $("#ddlVSpeciality").selectpicker("refresh"), $("#ddlVTherapy").removeAttr("disabled"), $("#ddlVTherapy").selectpicker("refresh"))
}), $("input[type=radio][name=Priority]").change(function () {
    modePriority = $(this).val(), 0 == $(this).val() ? (DD_View = "P", BindSubDivision_DDL(), $("#lblPSubdivision").show(), $("#ddlPSubdivision").selectpicker("show"), $("#lblPMode").hide(), $("#ddlPMode").empty(), $("#ddlPMode").selectpicker("hide"), $(".prdPriority").show(), $("#grdPriority").empty(), $(".grdPriority").hide()) : 1 == $(this).val() ? (DD_View = "P", BindSubDivision_DDL(), $("#lblPSubdivision").show(), $("#ddlPSubdivision").selectpicker("show"), $("#lblPMode").empty(), $("#lblPMode").text("Product"), $("#lblPMode").show(), $("#ddlPMode").empty(), $("#ddlPMode").selectpicker("show"), $("#ddlPMode").selectpicker("refresh"), $(".prdPriority").show(), $("#grdPriority").empty(), $(".grdPriority").hide()) : 2 == $(this).val() ? (DD_View = "P", BindSubDivision_DDL(), Bind_SpecGroup(), $("#lblPSubdivision").show(), $("#ddlPSubdivision").selectpicker("show"), $("#lblPMode").empty(), $("#lblPMode").text("Speciality"), $("#ddlPMode").selectpicker("show"), $(".prdPriority").show(), $("#grdPriority").empty(), $(".grdPriority").hide()) : 3 == $(this).val() && (DD_View = "P", BindSubDivision_DDL(), Bind_TherapyGroup(), $("#lblPSubdivision").show(), $("#ddlPSubdivision").selectpicker("show"), $("#lblPMode").empty(), $("#lblPMode").text("Therapy"), $("#ddlPMode").selectpicker("show"), $(".prdPriority").show(), $("#grdPriority").empty(), $(".grdPriority").hide())
}), $("input[type=radio][name=Preview]").change(function () {
    modePreview = $(this).val(), 0 == $(this).val() ? (DD_View = "Pre", BindSubDivision_DDL(), $("#lblPreSubdivision").show(), $("#ddlPreSubdivision").selectpicker("show"), $("#lblPreMode").hide(), $("#ddlPreMode").empty(), $("#ddlPreMode").selectpicker("hide"), $(".prdPreview").show(), $("#grdPreview").empty(), $(".grdPreview").hide()) : 1 == $(this).val() ? (DD_View = "Pre", BindSubDivision_DDL(), $("#lblPreSubdivision").show(), $("#ddlPreSubdivision").selectpicker("show"), $("#lblPreMode").empty(), $("#lblPreMode").text("Product"), $("#lblPreMode").show(), $("#ddlPreMode").empty(), $("#ddlPreMode").selectpicker("show"), $("#ddlPreMode").selectpicker("refresh"), $(".prdPreview").show(), $("#grdPreview").empty(), $(".grdPreview").hide()) : 2 == $(this).val() ? (DD_View = "Pre", BindSubDivision_DDL(), Bind_SpecGroup(), $("#lblPreSubdivision").show(), $("#ddlPreSubdivision").selectpicker("show"), $("#lblPreMode").empty(), $("#lblPreMode").text("Speciality"), $("#ddlPreMode").selectpicker("show"), $(".prdPreview").show(), $("#grdPreview").empty(), $(".grdPreview").hide()) : 3 == $(this).val() && (DD_View = "Pre", BindSubDivision_DDL(), Bind_TherapyGroup(), $("#lblPreSubdivision").show(), $("#ddlPreSubdivision").selectpicker("show"), $("#lblPreMode").empty(), $("#lblPreMode").text("Therapy"), $("#ddlPreMode").selectpicker("show"), $(".prdPreview").show(), $("#grdPreview").empty(), $(".grdPreview").hide())
}), $(document).on("change", "#ddlMasPriority", function () {
    var e = $(this).parents("tr:first"),
        d = e.find("select:eq(0)"),
        t = [];
    for (i = 1; i <= $("#grdPriority > tbody > tr").length; i++) t.push(i);
    var r = [];
    r = $("#grdPriority > tbody > tr").map(function () {
        return $(this).find("select:eq(0)").val()
    }).get(), r = $.unique(r.sort());
    var l = {},
        o = [];
    for (let e of r) l[e] = !0;
    for (let e of t) l[e] || o.push(e);
    $("#grdPriority > tbody > tr").each(function () {
        var t = $(this),
            i = t.find("select:eq(0)");
        d.val() == i.val() && e.index() != t.index() && (i.selectpicker(), i.selectpicker("val", o[0]))
    })
}), $(document).on("click", "#btnDelete_All", function (e) {
    e.preventDefault();
    var d = [];
    d = $("#grdUpload > tbody > tr").map(function () {
        var e = $(this),
            d = "";
        return e.find("input[type=checkbox]:eq(0)").prop("checked") && (d = e.find("span:eq(1)").text()), d
    }).get(), $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        url: "../MR/webservice/EDetailingWebService.asmx/Delete_Files",
        data: "{objSl_No:" + JSON.stringify(d.join(",")) + "}",
        dataType: "json",
        success: function (e) {
            $.alert("Records Deleted!", "Alert!") && $("#btnVView").trigger("click")
        },
        error: function (e) { }
    })
}), $(".Common").click(function (e) {
    e.preventDefault();
    var d = [];
    d = $("#grdUpload > tbody > tr").map(function () {
        var e = $(this);
        return e.find("span:eq(1)").text() + "^" + e.find("input[type=checkbox]:eq(1)").prop("checked")
    }).get(), $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        url: "../MR/webservice/EDetailingWebService.asmx/Update_Common",
        data: "{objImg_Common:" + JSON.stringify(d.join(",")) + "}",
        dataType: "json",
        success: function (e) {
            $.alert("Update Success!", "Alert!")
        },
        error: function (e) { }
    })
}), $(".MasPriority").click(function (e) {
    e.preventDefault();
    var d = "",
        t = "";
    $("#rdProductP").prop("checked") ? (d = $("#ddlPSubdivision option:selected").val(), t = $("#ddlPMode option:selected").val(), modePriority = "1") : $("#rdSpecialityP").prop("checked") ? (d = $("#ddlPSubdivision option:selected").val(), t = $("#ddlPMode option:selected").val(), modePriority = "2") : $("#rdTherapyP").prop("checked") ? (d = $("#ddlPSubdivision option:selected").val(), t = $("#ddlPMode option:selected").val(), modePriority = "3") : $("#rdBrandP").prop("checked") && (d = $("#ddlPSubdivision option:selected").val(), modePriority = "0");
    var i = [];
    var flagselect = 0;
    i = $("#grdPriority > tbody > tr").map(function () {
        var e = $(this);
        if (e.find("td:nth-child(5) > div > button").text().trim() == 'Nothing selected') {
            flagselect = flagselect + 1;
        }
        else {
            return e.find("span:eq(1)").text() + "^" + e.find("select:eq(0)").val() + "^" + modePriority + "^" + d + "^" + t
        }
    }).get()
    if (flagselect == 0) {
        $.ajax({
            type: "POST",
            contentType: "application/json; charset=utf-8",
            url: "../MR/webservice/EDetailingWebService.asmx/Update_Priority",
            data: "{objPriority:" + JSON.stringify(i.join(",")) + "}",
            dataType: "json",
            success: function (e) {
                $.alert("Update Success!", "Alert!")
            },
            error: function (e) { }
        })
    }
    else {
        alert("Please select the priority order properly and click the update button.");
        return false;
    }
});
var popUpObj, fixHelperModified = function (e, d) {
    var t = d.children(),
        i = d.clone();
    return i.children().each(function (e) {
        $(this).width(t.eq(e).width())
    }), i
},
    updateIndex = function (e, d) {
        $("td.index", d.item.parent()).each(function (e) {
            $(this).html(e + 1)
        })
    };

function Validation() {
    //if ("U" == DD_View) return "Nothing selected" == $("#ddlUSubdivision").next().attr("title") ? ($.alert("Please Select Sub-Division!", "Alert!"), !1) : "Nothing selected" != $("#ddlUBrand").next().attr("title") || ($.alert("Please Select Brand!", "Alert!"), !1);

    if ("U" == DD_View) return 0 == $("#ddlUSubdivision").val() ? ($.alert("Please Select Sub-Division!", "Alert!"), !1) : 0 != $("#ddlUBrand").val() || ($.alert("Please Select Brand!", "Alert!"), !1);
    if ("V" == DD_View) return 0 == $("#ddlVSubdivision").val() ? ($.alert("Please Select Sub-Division!", "Alert!"), !1) : 0 != $("#ddlVBrand").val() || ($.alert("Please Select Brand!", "Alert!"), !1);
    if ("P" == DD_View) {
        if ($("#rdBrandP").prop("checked")) return 0 != $("#ddlPSubdivision").val() || ($.alert("Please Select Sub-Division!", "Alert!"), !1);
        if ($("#rdProductP").prop("checked")) return 0 == $("#ddlPSubdivision").val() ? ($.alert("Please Select Sub-Division!", "Alert!"), !1) : 0 != $("#ddlPMode").val() && "Nothing selected" != $("#ddlPMode").next().attr("title") || ($.alert("Please Select Product!", "Alert!"), !1);
        if ($("#rdSpecialityP").prop("checked")) return 0 != $("#ddlPMode").val() || ($.alert("Please Select Speciality!", "Alert!"), !1);
        if ($("#rdTherapyP").prop("checked")) return 0 != $("#ddlPMode").val() || ($.alert("Please Select Therapy!", "Alert!"), !1)
    } else if ("Pre" == DD_View) {
        if ($("#rdProductPre").prop("checked")) return 0 == $("#ddlPreSubdivision").val() ? ($.alert("Please Select Sub-Division!", "Alert!"), !1) : 0 != $("#ddlPreMode").val() || ($.alert("Please Select Product!", "Alert!"), !1);
        if ($("#rdSpecialityPre").prop("checked")) return 0 != $("#ddlPreMode").val() || ($.alert("Please Select Speciality!", "Alert!"), !1);
        if ($("#rdTherapyPre").prop("checked")) return 0 != $("#ddlPreMode").val() || ($.alert("Please Select Therapy!", "Alert!"), !1)
    }
}

function ValidationUpload() {
    return "Nothing selected" == $("#ddlUProduct").next().attr("title") ? ($.alert("Please Select Product!", "Alert!"), !1) : "Nothing selected" == $("#ddlUSpeciality").next().attr("title") ? ($.alert("Please Select Speciality!", "Alert!"), !1) : "Nothing selected" != $("#ddlUTherapy").next().attr("title") || ($.alert("Please Select Therapy!", "Alert!"), !1)
}

function showModalPopUp(e) {
    (popUpObj = window.open("DD_Slide_Priority.aspx?objPriority=" + e, "ModalPopUp", "toolbar=no,scrollbars=yes,location=no,statusbar=no,menubar=no,addressbar=no,resizable=yes,width=800,height=600,left = 0,top=0")).focus(), $(popUpObj.document.body).ready(function () {
        $(document).ajaxStart(function () {
            $("#loader").css("display", "block")
        }).ajaxStop(function () {
            $("#loader").css("display", "none")
        })
    })
}