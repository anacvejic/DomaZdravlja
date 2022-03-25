var app = angular.module('myApp', []);
app.controller('myAdmin', function ($scope, $http) {
    var str = 1;
    var brPrikaz = 3;
    var broj;

    GetRadnici(str);
    function GetRadnici(str) {
        $http({
            method: 'GET',
            url: '/Administrator/GetDoktori/?str=' + str + '&brPrikaz=' + brPrikaz
        }).then(function (result) {
            $scope.details = result.data;
        });
    }

    function Test() {
        //broj = $.get("/Administrator/BrojStrana/");
        var broj = 0;
        $http({
            method: 'GET',
            url: '/Administrator/BrojStrana/'
        }).then(function (rez) {
            angular.forEach(rez, function (a, b) {
                broj++;
                if (broj == 1) {
                    rez = a;
                }
            });
            $("#page-selection").bootpag({
                //Ne vidi brojeve?
                total: rez
                //total: 5
                //num je broj strane koji je kliknut
            }).on("page", function (event, num) {
                str = num;
                GetRadnici(str);
            })

        });
     }

    $(function () {
        Test();
    });

    $scope.SaveLekar = function () {
        const ime = $("#myModalS #Ime").val();
        const prezime = $("#myModalS #Prezime").val();
        const email = $("#myModalS #Email").val();
        const pass = $("#myModalS #Password").val();
        const specijalnost = $("#myModalS #Specijalnost").val();

        $http({
            method: 'POST',
            url: '/Administrator/DodajDoktora?ime=' + ime + '&prezime=' + prezime + '&email=' + email + '&password=' + pass + '&specijalnost=' + specijalnost
        }).then(function (data) {
            angular.forEach(data, function (a) {
                if (a == "1") {
                    $("#ime").text(ImeKorisnika);
                }
                else if (a == "2") {
                    $("#ime").text(ImeRegex);
                }
                else if (a == "3") {
                    $("#prezime").text(PrezimeKorisnika);
                }
                else if (a == "4") {
                    $("#prezime").text(PrezimeRegex);
                }
                else if (a == "5") {
                    $("#email").text(EmailKorisnika);
                }
                else if (a == "6") {
                    $("#email").text(EmailRegex);
                }
                else if (a == "7") {
                    $("#pass").text(PasswordKorisnika);
                }
                else if (a == "8") {
                    $("#pass").text(PasswordRegex);
                }
                else if (a == "9") {
                    $("#spec").text(SpecijalnostKorisnika);
                }
                else if (a == "10") {
                    $("#spec").text(SpecijalnostRegex);
                }
                else if (a == "11") {
                    $("#bingo").text(Greska);
                }
                else if (a == "12") {
                    $("#otkazi").click();
                    GetRadnici();
                    alert(DodavanjeLekaraUspeh.dodavanjeLekaraUspeh);

                    $("#Ime").val("");
                    $("#Prezime").val("");
                    $("#Email").val("");
                    $("#Password").val("");
                    $("#Specijalnost").val("");
                    $("#ime").text("");
                    $("#prezime").text("");
                    $("#email").text("");
                    $("#pass").text("");
                    $("#spec").text("");
                }
            });
            }).error(function (rez) {
            alert(rez);
        });
    }

    $scope.deleteKorisnik = function (x) {
        var pom = confirm(PotvrdaBrisanjaLekar1.potvrdaBrisanjaLekar1+ ' ' + x.ime + ' ' + x.prezime + ' ' + PotvrdaBrisanjaLekar2.potvrdaBrisanjaLekar2);
        if (pom) {
            var response = $http({
                method: 'POST',
                url: '/Administrator/BrisanjeDoktora?id=' + x.idKorisnik
            }).then(function () {
                alert(LekarNeaktivan.lekarNeaktivan);
                GetRadnici();
            });
        }
    }



});