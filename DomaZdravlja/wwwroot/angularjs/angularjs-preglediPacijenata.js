
var app = angular.module('pregledi', []);

app.controller('preglediPacijenata', function ($scope, $http) {

    getPreglediPacijenata();

    function getPreglediPacijenata() {
        $http({
            method: 'GET',
            url: '/MedicinskaSestra/PreglediPacijenataKarton'
        }).then(function (result) {
            $scope.pregledi = result.data;
        });
    }
});