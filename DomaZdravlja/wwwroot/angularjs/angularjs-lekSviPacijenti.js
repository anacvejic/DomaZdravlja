var app = angular.module('pregledi', []);

app.controller('lekaPacijenti', function ($scope, $http) {

    GetPacijenti();
    function GetPacijenti() {
        $http({
            method: 'GET',
            url: '/Lekar/LekarPacijenti'
        }).then(function (result) {
            $scope.pacijenti = result.data;
        });
    }

    $scope.stornirajPregled = function (x) {
        var pom = confirm(StorniraniPregled1.storniraniPregled1);
        if (pom) {
            var response = $http({
                method: 'POST',
                url: '/Lekar/StornirajPregled?id=' + x.idPregleda
            }).then(function () {
                alert(PacijentNeaktivan.pacijentNeaktivan);
                GetPacijenti();
            });
        }
    }


    $scope.sacuvaj = function () {
        const nazivVrsteIzvestaja = $("#myForm #NazivVrsteIzvestaja").val();
        const opisIzvestaj = $("#myForm #OpisIzvestaj").val();
        //const nazivUstanove = $("#myForm #NazivUstanove").val();
        const lekarSpecijalista = $("#myForm #LekarSpecijalista").val();
        const imePacijenta = $("#myForm #ImePacijenta").val();
        const prezimePacijenta = $("#myForm #PrezimePacijenta").val();

        $http({
            method: 'POST',
            url: '/Lekar/SacuvajIzvestaj?nazivVrsteIzvestaja=' + nazivVrsteIzvestaja +
                '&opisIzvestaj=' + opisIzvestaj +
                '&lekarSpecijalista=' + lekarSpecijalista + '&imePacijenta=' + imePacijenta +
                '&prezimePacijenta=' + prezimePacijenta
        }).then(function (data) {
            angular.forEach(data, function (a) {
                if (a == "1") {
                    $("#errorNI").text(nazivVrsteIzvestajaa);
                }
                else if (a == "2") {
                    $("#errorNI").text(nazivVrsteIzvestajaError);
                }
                else if (a == "3") {
                    $("#errorOpis").text(ErrorOpis);
                }
                else if (a == "4") {
                    $("#errorimePacijenta").text(ErrorimePacijenta);
                }
                else if (a == "5") {
                    $("#errorimePacijenta").text(RegexErrorimePacijenta);
                }
                else if (a == "6") {
                    $("#errorprezimePacijenta").text(ErrorprezPrezimePacijenta);
                }
                else if (a == "7") {
                    $("#errorprezimePacijenta").text(PrezimeRegex);
                }
                else if (a == "8") {
                    alert(Greska.greska);
                }
                else if (a == "9") {
                    $("#otkazi").click();
                    alert(UnesenIzvestaj.unesenIzvestaj);
                    $("#NazivVrsteIzvestaja").val("");
                    $("#OpisIzvestaj").val("");
                    $("#ImePacijenta").val("");
                    $("#PrezimePacijenta").val("");
                    $("#errorNI").text("");
                    $("#errorOpis").text("");
                    $("#errorimePacijenta").text("");
                    $("#errorprezimePacijenta").text("");
                }
            });
            }).error(function (rez) {
                alert(rez);
        });
    }
});