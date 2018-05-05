tableLangSettings = {
    search: "Pretraga",
    info: "_START_ - _END_ od ukupno _TOTAL_ zapisa",
    lengthMenu: "Prikaži _MENU_ zapisa",
    paginate: {
        first: "Prva",
        previous: "Prethodna",
        next: "Sljedeća",
        last: "Zadnja"
    },
    emptyTable: "Nema podataka za prikaz",
    infoEmpty: "Nema podataka za prikaz",
    infoFiltered: "(filtrirano od ukupno _MAX_ zapisa)",
};


//Pri svakom kliku kontrole koja ima css klasu delete zatraži potvrdu
//za razliku od  //$(".delete").click ovo se odnosi i na elemente koji se mogu pojaviti u budućnosti 
//dinamičkim učitavanjem
$(function () {
    $(document).on('click', '.delete', function (event) {
        if (!confirm("Obrisati zapis?")) {
            event.preventDefault();
        }
    });    
});

function SetDeleteAjax(selector, url, paramname) {
    $(document).on('click', selector, function (event) {

        event.preventDefault(); //u slučaju da se radi o nekom submit buttonu, inače nije nužno        
        var paramval = $(this).data(paramname);
        var tr = $(this).parents("tr");
        var aktivan = $(tr).data('aktivan');
        if (aktivan != true) { //da spriječimo dva brza klika...
            $(tr).data('aktivan', true);
            if (confirm('Obrisati zapis?')) {
                var token = $('input[name="__RequestVerificationToken"]').first().val();
                $("#tempmessage").siblings().remove();
                $("#tempmessage").removeClass("alert-success");
                $("#tempmessage").removeClass("alert-danger");
                $("#tempmessage").html('');
                $.post(url, { id: paramval, __RequestVerificationToken: token }, function (data) {
                    if (data.successful) {
                        $(tr).remove();
                    }
                    $("#tempmessage").addClass(data.successful ? "alert-success" : "alert-danger");
                    $("#tempmessage").html(data.message);
                }).fail(function (jqXHR) {
                    alert(jqXHR.status + " : " + jqXHR.responseText);
                });
            }
            $(tr).data('aktivan', false);
        }
    });
}


//Svim elementima koji zadovoljavaju selector definirat će se ponašanje na klik na način
//da se redak u kojem se nalazi element sakrije te se pozove url sa servera koji vrati novi redak 
//u kojem se može ažurirati element
//url na serveru ovisi o parametru nekog data attributa (paramname)
//Parametar će na server biti poslan kao id
function SetEditAjax(selector, url, paramname) {
    $(document).on('click', selector, function (event) {
        event.preventDefault(); //u slučaju da se radi o nekom submit buttonu, inače nije nužno        
        var paramval = $(this).data(paramname);
        var tr = $(this).parents("tr");
        var aktivan = $(tr).data('aktivan');
        if (aktivan != true) { //da spriječimo dva brza klika...
            $(tr).data('aktivan', true);
            $.get(url, { id: paramval }, function (data) {
                tr.toggle(); //sakrij trenutni redak
                var inserted = $(data).insertAfter(tr); //iza skrivenog retka dodaj redak koji je došao sa servera
                SetCancelAndSaveBehaviour(tr, inserted, url);
            })
                .fail(function (jqXHR) {
                    alert(jqXHR.status + " : " + jqXHR.responseText);
                });
        }
    });
}

function SetCancelAndSaveBehaviour(hiddenRow, insertedData, url) {
    //nađi cancel button 
    insertedData.find(".cancel").click(function (event) {
        insertedData.remove(); //ukloni umetnuti redak
        hiddenRow.toggle(); //vrati vidljivost originalnom retku
        $(hiddenRow).data('aktivan', false);
    });

    //nađi save button 
    insertedData.find(".save").click(function (event) {
        event.preventDefault(); //u slučaju da se radi o nekom submit buttonu, inače nije nužno

        //pripremi podatke i spremi ih u json
        var formData = new FormData();
        //pronađi sve elemente koji imaju data-save (mogli bi tražiti i data-val='true', ali što kao ASP.Net promijeni način označavanja
        insertedData.find("[data-save]").each(function (index, element) {
            //dodaj vrijednost elementa u object data koji će kasnije biti poslan na server 
            if ($(element).is(':checkbox')) {
                formData.append($(element).attr('name'), $(element).is(':checked') == true);
            }
            else if ($(element).is("input[type=file]")) {
                var files = $(element).get(0).files;
                if (files.length > 0) {
                    formData.append($(element).attr('name'), files[0]);
                }
            }
            else {
                var val = $.trim($(element).val());
                if (val != '') {
                    formData.append($(element).attr('name'), $(element).val());
                }
            }
        });
        //find antiforgery token
        var token = $('input[name="__RequestVerificationToken"]').first().val();
        formData.append("__RequestVerificationToken", token);

        $.ajax({
            type: "POST",
            url: url,
            contentType: false,
            processData: false,
            data: formData,
            success: function (data, textStatus, jqXHR) {
                insertedData.remove();
                var inserted = $(data).insertAfter(hiddenRow);
                SetCancelAndSaveBehaviour(hiddenRow, inserted, url);
            },
            error: function (jqXHR) {

                if (jqXHR.status == 302) { //data saved and redirect
                    insertedData.remove();
                    $.get(jqXHR.responseText, {}, function (refreshedRow) {
                        $(hiddenRow).replaceWith(refreshedRow);
                    })
                }
                else {
                    alert(jqXHR.status + " : " + jqXHR.responseText);
                }
            }
        });
    });
}




