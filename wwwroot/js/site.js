$(document).ready(function () {
    $("#generateButton").click(function () {
        var urlData = document.getElementById("urlInput").value;

        $.ajax({
            url: string = `${location.origin}/tinyurlpage/add`,   // Controller/Action
            type: 'POST',
            contentType: 'application/x-www-form-urlencoded; charset=UTF-8',
            data: { url: urlData },
            success: function (result) {
                if (result.redirectUrl) {
                    window.location.href = result.redirectUrl;
                }
            },
            error: function (xhr, status, error) {
                console.error("Error:", error);
            }
        });
    });

    $(document).on("click", "#copyButton", function () {
        var url = $("#shortUrl").prop("href");  // get href of the URL

        // Copy to clipboard
        navigator.clipboard.writeText(url).then(function () {
            alert("URL copied successfully");
        }).catch(function (err) {
            alert("Failed to copy URL: " + err);
        });
    });

    $(document).on('click', '#deleteButton', function () {
        var div = $(this).closest('.short-url-div'); // find the parent div of clicked button
        var fullUrl = div.find('.shortUrlLink').attr('href'); // get href

        // Extract 6-digit (or alphanumeric) code at the end of URL
        var codeMatch = fullUrl.match(/urlcode=([A-Za-z0-9]{6})$/);
        var urlCode = codeMatch[1];

        // AJAX call to delete
        $.ajax({
            url: `${location.origin}/tinyurlpage/delete`, // controller action
            type: 'POST',
            data: { urlcode: urlCode },
            success: function () {    
                    alert("Deleted Successfully");
                    div.remove(); // remove the div from DOM
            },
            error: function () {
                alert('Error deleting URL.');
            }
        });
    });
});
