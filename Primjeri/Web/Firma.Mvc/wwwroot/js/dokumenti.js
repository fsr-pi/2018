$(document).on('click', '.deleterow', function () {
    event.preventDefault();
    var tr = $(this).parents("tr");
    tr.remove();

    //očisti staru poruku da je npr. dokument uspješno snimljen
    $("#tempmessage").siblings().remove();
    $("#tempmessage").removeClass("alert-success");
    $("#tempmessage").removeClass("alert-danger");
    $("#tempmessage").html('');
});

$(function () {  
    $(".form-control").bind('keydown', function (event) {
        var keycode = (event.keyCode ? event.keyCode : event.which);
        if (keycode == 13) {
            event.preventDefault();
        }
    });

    $("#artikl-kolicina, #artikl-rabat").bind('keydown', function (event) {
        var keycode = (event.keyCode ? event.keyCode : event.which);
        if (keycode == 13) {
            event.preventDefault();
            DodajArtikl();
        }
    });  


    $("#artikl-dodaj").click(function () {
        event.preventDefault();
        DodajArtikl();
    });   
});

function DodajArtikl() {
    var sifra = $("#artikl-sifra").val();
    if (sifra != '') {
        if ($("[name='Stavka[" + sifra + "].SifArtikla'").length > 0) {
            alert('Artikl je već u dokumentu');
            return;
        }
       
        var kolicina = parseFloat($("#artikl-kolicina").val().replace(',', '.')); //treba biti točka, a ne zarez za parseFloat
        if (isNaN(kolicina))
            kolicina = 1;

        var rabat = parseFloat($("#artikl-rabat").val().replace(',', '.')); //treba točka, a ne zarez za parseFloat
        if (isNaN(rabat))
            rabat = 0;        

        var cijena = parseFloat($("#artikl-cijena").val());
        var template = $('#template').html();
        var naziv = $("#artikl-naziv").val();
        var iznos = kolicina * cijena * (1 - rabat);
        iznos = iznos.toFixed(2).replace('.', ',').replace(/(\d)(?=(\d\d\d)+(?!\d))/g, "$1.") + ' kn';
        var cijena_formatirana = cijena.toFixed(2).replace('.', ',').replace(/(\d)(?=(\d\d\d)+(?!\d))/g, "$1.") + ' kn'

        //Alternativa ako su hr postavke sa zarezom //http://haacked.com/archive/2011/03/19/fixing-binding-to-decimals.aspx/
        //ili ovo http://intellitect.com/custom-model-binding-in-asp-net-core-1-0/

        template = template.replace(/--sifra--/g, sifra)
                            .replace(/--kolicina--/g, kolicina.toFixed(2).replace('.', ','))
                            .replace(/--cijena--/g, cijena)
                            .replace(/--cijena_formatirana--/g, cijena_formatirana)
                            .replace(/--naziv--/g, naziv)
                            .replace(/--iznos--/g, iznos)
                            .replace(/--rabat--/g, rabat.toFixed(2).replace('.', ','));
        $(template).find('tr').insertBefore($("#table-stavke").find('tr').last());

        $("#artikl-sifra").val('');
        $("#artikl-kolicina").val('');
        $("#artikl-rabat").val('');
        $("#artikl-cijena").val('');
        $("#artikl-naziv").val('');

        //očisti staru poruku da je npr. dokument uspješno snimljen
        $("#tempmessage").siblings().remove();
        $("#tempmessage").removeClass("alert-success");
        $("#tempmessage").removeClass("alert-danger");
        $("#tempmessage").html('');
    }
}