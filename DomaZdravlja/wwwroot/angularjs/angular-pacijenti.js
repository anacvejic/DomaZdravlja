var app = angular.module('myApp', []);
app.controller('myPatient', function ($scope, $http) {

    //var str = 1;
    //var brPrikaz = 3;
    //var broj;

    getPacijenti();
    function getPacijenti() {
        $http({
            method: 'GET',
            url: '/MedicinskaSestra/Pacijenti'
        }).then(function (result) {
            $scope.pacijenti = result.data;
        });
    }

    getPacijenti(str);
    //function getPacijenti(str) {
    //    $http({
    //        method: 'GET',
    //        url: '/MedicinskaSestra/Pacijenti?str=' + str + '&brPrikaz=' + brPrikaz
    //    }).then(function (result) {
    //        $scope.pacijenti = result.data;
    //    });
    //}

    //function Test() {
    //    var broj = 0;
    //    $http({
    //        method: 'GET',
    //        url: '/Administrator/BrojStranaPacijenti/'
    //    }).then(function (rez) {
    //        angular.forEach(rez, function (a, b) {
    //            broj++;
    //            if (broj == 1) {
    //                rez = a;
    //            }
    //        });
    //        $("#page-selection").bootpag({
    //            Ne vidi brojeve?
    //            total: rez
    //            total: 5
    //            num je broj strane koji je kliknut
    //        }).on("page", function (event, num) {
    //            str = num;
    //            getPacijenti(str);
    //        })

    //    });
    //}

    //$(function () {
    //    Test();
    //});


    $scope.deleteKorisnik = function (x) {
        var pom = confirm(DaLiSteSigurniUBrisanje.daLiSteSigurniUBrisanje + ' ' + x.ime + ' ' + x.prezime + ' ' + Pacijenta.pacijenta);
        if (pom) {
            var response = $http({
                method: 'POST',
                url: '/MedicinskaSestra/BrisanjePacijenta?id=' + x.idKarton
            }).then(function () {
                alert(UspehBrisanje.uspehBrisanje);
                getPacijenti();
            });
        }
    }

    $scope.openEditPacijentModal = function (x) {
        $('#editModal #IdKarton').val(x.idKarton);
        $('#editModal #EditbrojKnjizice').val(x.brojKnjizice);
        $('#editModal #EditPrezime').val(x.prezime);
        $('#selectKorisnik option:selected').val(x.idKorisnik);
        $('#editModal').modal('show');
    }

    $scope.editKartonPacijenta = function () {

        const idKarton = $('#editModal #IdKarton').val();
        const brojKnjizice = $('#editModal #EditbrojKnjizice').val();
        const prezime = $('#editModal #EditPrezime').val();
        const idKorisnik = $('#selectKorisnik option:selected').val();

        $http({
            method: 'POST',
            url: '/MedicinskaSestra/IzmeniKartonPacijenta?idKarton=' + idKarton + '&brojKnjizice=' + brojKnjizice + '&prezime=' + prezime + '&idKorisnik=' + idKorisnik
        }).then(function (data) {
            angular.forEach(data, function (a) {
                if (a == "1") {
                    $("#errorEditbrojKnjizice").text(BrojKnjiziceObavezan);
                }
                else if (a == "2") {
                    $("#errorEditbrojPrezime").text(PrezimePacijenta);
                }
                else if (a == "3") {
                    $("#errorEditbrojPrezime").text(PrezimeRegex);
                }
                else if (a == "4") {

                }
                else if (a == "5") {
                    alert(IzmenaPodatkapacijenata.izmenaPodatkapacijenata);
                    $('#Edit').click();
                    getPacijenti();
                }
                else if (a == "6") {
                    $("#errorEditbrojKnjizice").text(BrojKnjiziceBroj);
                }
                else if (a == "7") {
                    $("#errorEditbrojKnjizice").text(BrojKnjiziceRegexJS);
                }
            });
        });

    }

    GetLekari();
    function GetLekari() {
        $http({
            method: 'GET',
            url: '/MedicinskaSestra/Getlekari'
        }).then(function (result) {
            $scope.lekari = result.data;
        });
    }
});