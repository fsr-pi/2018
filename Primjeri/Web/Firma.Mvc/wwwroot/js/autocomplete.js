$(function () {
    //$(".datum").datepicker({
    //    // dateFormat: "dd.mm.yy"            
    //});

    $("[data-autocomplete]").each(function (index, element) {
        var url = $(element).data('autocomplete');
        var resultplaceholder = $(element).data('autocomplete-result');
        if (resultplaceholder === undefined)
            resultplaceholder = url;
        $(element).blur(function () {
            if ($(element).val().length === 0) {
                $("[data-autocomplete-result-for='" + resultplaceholder + "']").val('');
            }
        });

        $(element).autocomplete({
            source: "/autocomplete/" + url,
            autoFocus: true,
            minLength: 1,
            select: function (event, ui) {
                $(element).val(ui.item.label);               
                $("[data-autocomplete-result-for='" + resultplaceholder + "']").val(ui.item.id);                
            }
        });
    });

    $("[data-autocomplete-artikl]").each(function (index, element) {
        var url = $(element).data('autocomplete-artikl');
        $(element).blur(function () {
            if ($(element).val().length === 0) {
                $("[data-autocomplete-artikl-id='" + url + "']").val('');
                $("[data-autocomplete-artikl-cijena='" + url + "']").val('');                
            }
        });

        $(element).autocomplete({
            source: "/autocomplete/" + url,
            autoFocus: true,
            minLength: 1,
            select: function (event, ui) {               
                $(element).val(ui.item.label);
                $("[data-autocomplete-id='" + url + "']").val(ui.item.id);
                $("[data-autocomplete-cijena='" + url + "']").val(ui.item.cijena);                
            }
        });
    });
});