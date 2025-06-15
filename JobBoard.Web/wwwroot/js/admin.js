

$(document).on("click", ".Approve", function () {
            console.log($(this).data("id"))
            $.ajax({
                url: "/Admin/ApproveEmployer",
                type: "POST",
                data: { "employerId": $(this).data("id") },
                success: function (response) {
                    console.log(response);
                    location.reload()
                },
                error: function (request, status, error) {
                    alert(request.responseText);
                }
            });
        });