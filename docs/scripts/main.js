var imageTag = "default";

function changeImage() {
    var featureImage = document.getElementById("featureImage");    
    if (imageTag == "default")
    {
        featureImage.src = "images/mysnipittool_selection.PNG";
        featureImage.alt = "Screenshot of MySnipItTool selecting an area of the screen to capture.";
        imageTag = "selection";
    }     
    else if (imageTag == "selection") 
    {
        featureImage.src = "images/mysnipittool_drawing_tools.PNG";
        featureImage.alt = "Screenshot of MySnipItTool selecting an area of the screen to capture.";
        imageTag = "drawing";
    }    
    else {
        imageTag = "default";
        featureImage.src = "images/mysnipittool_default.PNG";
    }
}