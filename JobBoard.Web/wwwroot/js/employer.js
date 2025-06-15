$(document).on("click",".Deletebtn", function(){
            console.log($(this).data("id"));
            $.ajax({
                url: "/Employer/DeleteJob",
                type: "POST",
                data: { "jobId": $(this).data("id") },
                success: function (response) {
                    console.log(response);
                },
                error: function (request, status, error) {
                    alert(request.responseText);
                }
            });
        });