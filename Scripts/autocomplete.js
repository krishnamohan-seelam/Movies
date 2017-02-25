        


/*$(document).ready(function ()
{
    console.log("Entered");
    $("#search").autocomplete({
           source: function (request, response) {
            console.log(request.term);
            $.ajax({
                url: '/Movies/Movies/AutoComplete',
                type: "GET",
                dataType: "json",
                data: { term: request.term },
                success: function (data) {
                    response($.map(data, function (item) {
                        console.log(data);
                        return { label: item.search, value: item.search };
                    }))

                }
                 })
            },
        messages: {
            noResults: "", results: ""
        }
    });
})
*/


$(document).ready(function () {

    $("#search").autocomplete({
        source: function (request, response) {
            $.ajax({
                url: "/Movies/Movies/AutoComplete",
                type:"GET",
                dataType: "json",

                data: { term: request.term },

                success: function (data) {
                    response($.map(data, function (item) {
                        return { label:item.title, value: item.title };
                    }))

                }
            })
        },
        messages: {
            noResults: "", results: ""
        }
    });
})