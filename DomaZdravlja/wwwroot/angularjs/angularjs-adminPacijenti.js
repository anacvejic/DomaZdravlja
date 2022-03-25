var app = angular.module('myApp', []);
app.controller('myPatient', function ($scope, $http) {


    getPacijenti();
    function getPacijenti() {
        $http({
            method: 'GET',
            url: '/Administrator/GetPacijenti'
        }).then(function (result) {
            $scope.pacijenti = result.data;
        });
    }


    $scope.deleteKorisnik = function (x) {
        var pom = confirm(DaLiSteSigurniUBrisanjeTotalno.daLiSteSigurniUBrisanje + ' ' + x.ime + ' ' + x.prezime + ' ' + PacijentaBrisanje.pacijentaBrisanje);
        if (pom) {
            var response = $http({
                method: 'POST',
                url: '/Administrator/BrisanjePacijenta?id=' + x.idKarton
            }).then(function (data) {
                angular.forEach(data, function (a) {
                    if (a == "1") {
                        alert(ErrorPregledPacijenta.errorPregledPacijenta);
                    }
                    else if (a == "2") {
                        alert(BrisanjeTotalnoPacijenta.brisanjeTotalnoPacijenta);
                        getPacijenti();
                    }
                    else if (a == "3") {
                        alert(NeuspesnoBrisanje.neuspesnoBrisanje);
                    }
                });
            });
        }
    }

});