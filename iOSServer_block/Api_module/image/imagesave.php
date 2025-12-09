<?php
function imagesave() {
	global $data;
	$div_codes = (string) $data['division_code'];	
	$div_code = explode(",", $div_codes.",");
	// need to get app setup's
	switch ( strtolower( $data[ 'tableName' ] ) ) {
    case "uploadscribble":
        $response = array();
        if (isset($_FILES["ScribbleImg"])) 
        {
        	if (move_uploaded_file($_FILES["ScribbleImg"]["tmp_name"], "../Scribbles/" .basename($_FILES["ScribbleImg"]["name"]))) 
            {
              $success = true;
              $upload_url = basename($_FILES["ScribbleImg"]["name"]);
              $message = "Scribble image Has Been Updated" ;
            }
           else 
           {
             $upload_url="";
             $success = false;
             $message = "Error while uploading";
           }
        }
        else 
        {
	         $message =  "No File...";
             $success = true;
         }
 
         $response["success"] = $success;
         $response["msg"] = $message;
         $response["url"] = $upload_url;
         outputJSON($response); 
            break;        
    case "uploadsign":
          $response = array();
        if (isset($_FILES["file"])) 
        {
        	if (move_uploaded_file($_FILES["file"]["tmp_name"], "iOSServer/Signs/" .basename($_FILES["file"]["name"]))) 
            {
              $success = true;
              $upload_url = basename($_FILES["file"]["name"]);
              $message = "Profile Has Been Updated" ;
            }
           else 
           {
             $upload_url="";
             $success = false;
             $message = "Error while uploading";
           }
        }
        else 
        {
	         $message =  "No File...";
             $success = true;
         }
 
         $response["success"] = $success;
         $response["msg"] = $message;
         $response["url"] = $upload_url;
         outputJSON($response);  
            break; 
    case "imgupload":
		$response = array();
		  if (isset($_FILES["UploadImg"])) 
        {
        	if (move_uploaded_file($_FILES["UploadImg"]["tmp_name"], "../photos/" .basename($_FILES["UploadImg"]["name"]))) 
            {
              $success = true;
              $upload_url = basename($_FILES["UploadImg"]["name"]);
              $message = "Photo Has Been Updated" ;
            }
           else 
           {
             $upload_url="";
             $success = false;
             $message = "Error while uploading";
           }
        }
        else 
        {
	         $message =  "No File...";
             $success = true;
         }
 
         $response["success"] = $success;
         $response["msg"] = $message;
         $response["url"] = $upload_url;
         outputJSON($response); 
            break; 
    case "uploadphoto":
		$response = array();
		  if (isset($_FILES["EventImg"])) 
        {
        	if (move_uploaded_file($_FILES["EventImg"]["tmp_name"], "../photos/" .basename($_FILES["EventImg"]["name"]))) 
            {
              $success = true;
              $upload_url = basename($_FILES["EventImg"]["name"]);
              $message = "Photo Has Been Updated" ;
            }
           else 
           {
             $upload_url="";
             $success = false;
			 $message = "Error while uploading";
           }
        }
        else 
        {
	         $message =  "No File...";
             $success = true;
         }
 
         $response["success"] = $success;
         $response["msg"] = $message;
         $response["url"] = $upload_url;
         outputJSON($response); 
			break;
    case "savedcract":
	 	 $response = array();
        if (isset($_FILES["ActivityFile"])) 
        {
        	if (move_uploaded_file($_FILES["ActivityFile"]["tmp_name"], "../Activity/" .basename($_FILES["ActivityFile"]["name"]))) 
            {
              $success = true;
              $upload_url = basename($_FILES["ActivityFile"]["name"]);
              $message = "File Has Been Updated" ;
            }
           else 
           {
             $upload_url="";
             $success = false;
             $message = "Error while uploading";
           }
        }
        else 
        {
	         $message =  "No File...";
             $success = true;
         }
 
         $response["success"] = $success;
         $response["msg"] = $message;
         $response["url"] = $upload_url;
         outputJSON($response);
			break;
	 case "coachingupl":
	 	 $response = array();
        if (isset($_FILES["CoachingFile"])) 
        {
        	if (move_uploaded_file($_FILES["CoachingFile"]["tmp_name"], "../Coaching/" .basename($_FILES["CoachingFile"]["name"]))) 
            {
              $success = true;
              $upload_url = basename($_FILES["CoachingFile"]["name"]);
              $message = "File Has Been Updated" ;
            }
           else 
           {
             $upload_url="";
             $success = false;
             $message = "Error while uploading";
           }
        }
        else 
        {
	         $message =  "No File...";
             $success = true;
         }
 
         $response["success"] = $success;
         $response["msg"] = $message;
         $response["url"] = $upload_url;
         outputJSON($response);
			break;		
    default:
            $result = array();
			$result['success'] = false;
			$result['msg'] = 'Try Again';
			outputJSON($result);
            break;
    } 

}
?>


