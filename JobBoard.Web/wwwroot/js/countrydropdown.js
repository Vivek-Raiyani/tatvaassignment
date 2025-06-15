function GetDropdownData(url, data, element) {
    $.ajax({
        url: url,
        type: "GET",
        data: data,
        success: function (response) {
            console.log(response);
            element.empty();
            // let newOption = $('<option>', {
            //     value: 0,
            //     text: 'select ',
            // });
            let newOption = `<option selected disabled value="0">Select</option>`;
            element.append(newOption);
            response.data.forEach(function (data) {
                let newOption = $('<option>', {
                    value: data.id,
                    text: data.name
                });
                element.append(newOption);
            });
        },
        error: function (request, status, error) {
            alert(request.responseText);
        }
    });
}

$(document).on("change", "#CountryId", function () {
    const state = $("#StateId");
    GetDropdownData(url = '/Base/GetStates/', data = { "countryId": $("#CountryId").val() }, element = state);
    setTimeout(() => {
        let intStateValue = state.data("value");
        if (intStateValue != undefined) {
            state.val(intStateValue).change();
        }
    }, 250)
});

$(document).on("change", "#StateId", function () {
    const city = $("#CityId")
    GetDropdownData(url = '/Base/GetCities/', data = { "stateId": $("#StateId").val() }, element = city);
    setTimeout(() => {
        let intCityValue = city.data("value");
        if (intCityValue != undefined) {
            city.val(intCityValue);
        }
    }, 250)
});

$(document).ready(function () {
    const country = $("#CountryId")
    GetDropdownData(url = '/Base/GetCountries/', data = {}, element = country);
    setTimeout(() => {
        let intCountryValue = country.data("value");
        if (intCountryValue != undefined) {
            console.log(intCountryValue)
            country.val(intCountryValue).change();
        }
    }, 250)

    const category = $("#CategoryId");
    GetDropdownData(url = "/Base/GetJobCategories/", data = {}, element = category)
    setTimeout(() => {
        let intCategoryValue = category.data("value");
        if (intCategoryValue != undefined) {
            console.log(intCategoryValue)
            category.val(intCategoryValue).change();
        }
    }, 250)

});