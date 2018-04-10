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
//za razliku od  //$(".delete").click ovo se odnosi i na elemente koji će se pojaviti u budućnosti 
//dinamičkim učitavanjem
$(function () {
  $(document).on('click', '.delete', function (event) {
    if (!confirm("Obrisati zapis?")) {
      event.preventDefault();
    }
  });
});


