//on load
$(function () {
    var req = $.get("/MedicinskaSestra/GetDoktori/");
    req.done(function (rez) {
        $("#selectKorisnik").empty();
        //$("#selectKorisnik").append("<option value='0' class='form-control'>@localizer['IzborLekara']</option>");
        $.each(rez, function (index, data) {
            $("#selectKorisnik").append("<option value=" + data.idKorisnik + " class='form-control'>" + data.ime + " "+data.prezime+"</option>");
        });
    });
});


function ZakaziPregledPacijentu() {
    var DatumIvremePregleda = $("#DatumIvremePregleda").val();
    var IdKarton = $("#IdKarton").val();

    var req = $.post("/MedicinskaSestra/ZakazivanjePregleda/?DatumIvremePregleda=" + DatumIvremePregleda + "&IdKarton=" + IdKarton + "");
    $(".err").text("");
    req.done(function (rez) {
        if (rez == "1") {

            $("#datumVreme").text(datumIVremeObavezno);
        }
        else if (rez == "2") {
            $("#idKarton").text(BrojKnjiziceNeSmeBitiPrazan);
        }
        else if (rez == "3") {
            $("#idKarton").text(BrojKnjiziceNePOstojiUBazi);
        }
        else if (rez == "4") {
            $("#uspex").text(UspehPregled);
            $("#DatumIvremePregleda").text("");
            $("#IdKarton").text("");
            $("#datumVreme").text("");
            $("#idKarton").text("");
        }
        else if (rez == "5") {
            $("#neuspeh").text(NeuspehGreska);
        }
        
        //$("#otkazi").click();
    });
}

$("#unesi").click(function () {
    ZakaziPregledPacijentu();
    $("#DatumIvremePregleda").val("");
    $("#IdKarton").val("");
    
});