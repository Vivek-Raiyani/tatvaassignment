$(document).ready(function () {
    let currentpage = 1;
    let totalpage = 1;
    let isLoading = false;

    function GetJobs(data) {
        if (currentpage > totalpage || isLoading) {
            return;
        }
        isLoading = true;
        console.log(data)
        $.ajax({
            url: '/Base/PaginatedList/',
            type: "GET",
            data: data,
            success: function (response) {
                console.log(response)
                response.items.forEach(function (item) {
                    let section = `
                    <article class="content">
                        <section class="row m-0">
                            <section class="col-12">
                                <section class="info-box">
                                    <span class="info-box-icon bg-info"><img src="${item.companyLogo}" alt="Company logo"></span>
                                    <section class="info-box-content ms-3">
                                        <h5 class="info-box-text ">${item.title}</h5>
                                        <p class="info-box-text">${item.companyName}</p>
                                        <p class="info-box-text">${item.description}</p>
                                        <p class="info-box-text">Location : ${item.city},${item.state},${item.country}</p>
                                    </section>
                                </section>
                            </section>
                        </section>
                    </article> `;
                    $("#JobListings").append(section);
                });
                currentpage++;
                totalpage = response.totalPages;
                isLoading = false;
            },
            error: function (request, status, error) {
                alert(request.responseText);
                isLoading = false;
            }
        });
    }

    function AddNewOpenings() {
        let windowHeight = $(window).height();
        let scroll = $(window).scrollTop();
        let pageHeight = $(document).height();
        if (
            (scroll + windowHeight >= pageHeight * 0.8) &&
            !isLoading &&
            currentpage <= totalpage
        ) {
            console.log("Requesting new openings (page " + currentpage + ")");
            let data = {
                "Page": currentpage,
                "PageSize": 3
            };
            GetJobs(data);
        }
    }


    let data = {
        "Page": currentpage,
        "PageSize": 3,
    };
    GetJobs(data);

    $(window).on("scroll", AddNewOpenings);


    $(document).on("click", ".content", function () {
        console.log('redirect to job details page');

    });

});